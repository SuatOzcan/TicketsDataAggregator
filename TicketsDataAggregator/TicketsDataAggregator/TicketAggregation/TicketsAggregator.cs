// See https://aka.ms/new-console-template for more information

using UglyToad.PdfPig.Content;
using UglyToad.PdfPig;
using System.Globalization;
using System.Text;
using TicketsDataAggregator.Extensions;
using TicketsDataAggregator.FileAccess;

internal class TicketsAggregator
{
    private readonly string _ticketsFolder;
    private readonly IFileWriter _fileWriter;
    private readonly IPDFReader _pdfReader;
    private readonly Dictionary<string, string> _domainToCultureMapping = new Dictionary<string, string>
    {
        [".com"] = "en-US", // en is the language, US is the country. en-Us is called a culture.
        [".fr"] = "fr-FR",  // fr-FR is a culture.
        [".jp"] = "jp-JP",
    };

    public TicketsAggregator(string ticketsFolder, IFileWriter fileWriter, IPDFReader pdfReader)
    {
        this._ticketsFolder = ticketsFolder;
        this._fileWriter = fileWriter;
        this._pdfReader = pdfReader;
    }

    internal void Run()
    {
        StringBuilder strongBuilder = new StringBuilder();
        var ticketDocuments = _pdfReader.ReadPDF(_ticketsFolder);
        foreach (var ticketDocument in ticketDocuments)
        {
            IEnumerable<string> lines = ProcessDocument(ticketDocument);
            strongBuilder.AppendLine(string.Join(Environment.NewLine, lines));
        }
    
    _fileWriter.Write(strongBuilder.ToString(), _ticketsFolder,"aggregatedTickets.txt");
    }

    private IEnumerable<string> ProcessDocument(string document)
    {
        string[] splitText = document.Split(new string[] {
                                                    "Title:", "Date:", "Time:", "Visit us:" },
                                                            StringSplitOptions.TrimEntries);
        string webAddress = splitText.Last();
        string domain = webAddress.ExtractDomain() ; // returns one of ".com", ".fr", ".jp".
        string ticketCulture = _domainToCultureMapping[domain];

        for (int i = 1; i < splitText.Length - 3; i += 3)
        {
            
            yield return BuildTicketData(splitText,i, ticketCulture);
        }
    }
    private string BuildTicketData(string[] splitText, int i, string ticketCulture)
    {
        // Hard-coded
        string title = splitText[i];
        string dateAsString = splitText[i + 1];
        string timeAsString = splitText[i + 2];

        DateTime date = DateTime.Parse(dateAsString, new CultureInfo(ticketCulture)); // Changes the string to standard
                                                                                      // DateTime d/m/y format.
                                                                                      // So I can infer that DateTime only holds values in d/m/y format.
        DateTime dt = new DateTime(3, 2, 1);
        string g = dt.ToString();
        // The output string of the standard dateTimeObject.ToString() method will be in format d/m/y.
        // I think this is because of the culture my computer is in.
        int day = date.Day;
        //DateOnly dateOnly = DateOnly.Parse(dateAsString, new CultureInfo(ticketCulture));
        TimeOnly timeOnly = TimeOnly.Parse(timeAsString, new CultureInfo(ticketCulture));

        //date.ToString("en-US");
        string dateInStringInvariantCulture = date.ToString(CultureInfo.InvariantCulture); // Changes the date to m/d/y format.
        string timeStringInInvariantCulture = timeOnly.ToString(CultureInfo.InvariantCulture);

        string ticketData = $"{title,-40}|{dateInStringInvariantCulture}" +
            $"|{timeStringInInvariantCulture}";

        return ticketData;
    }

}