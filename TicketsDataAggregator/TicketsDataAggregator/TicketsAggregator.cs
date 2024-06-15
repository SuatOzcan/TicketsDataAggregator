// See https://aka.ms/new-console-template for more information

using UglyToad.PdfPig.Content;
using UglyToad.PdfPig;
using System.Globalization;
using System.Text;

internal class TicketsAggregator
{
    private readonly string _ticketsFolder;
    private readonly Dictionary<string, string> _domainToCultureMapping = new Dictionary<string, string>
    {
        [".com"] = "en-US", // en is the language, US is the country. en-Us is called a culture.
        [".fr"] = "fr-FR",  // fr-FR is a culture.
        [".jp"] = "jp-JP",
    };

    public TicketsAggregator(string ticketsFolder)
    {
        this._ticketsFolder = ticketsFolder;
    }

    internal void Run()
    {
        StringBuilder strongBuilder = new StringBuilder();
        foreach (string pdfFilePath in Directory.GetFiles(_ticketsFolder, "*.pdf"))
        {
            using PdfDocument document = PdfDocument.Open(pdfFilePath);
            Page page = document.GetPage(1);
            //string pageContent = page.Text;
            IEnumerable<string> lines = ProcessPage(page);
            strongBuilder.AppendLine(string.Join(Environment.NewLine, lines));
        }
        SaveTicketsData(strongBuilder);
    }

    

    private IEnumerable<string> ProcessPage(Page page)
    {
        string[] splittedText = page.Text.Split(new string[] {
                                                    "Title:", "Date:", "Time:", "Visit us:" },
                                                            StringSplitOptions.TrimEntries);
        string webAddress = splittedText.Last();
        string domain = ExtractDomain(webAddress); // returns one of ".com", ".fr", ".jp".
        string ticketCulture = _domainToCultureMapping[domain];

        for (int i = 1; i < splittedText.Length - 3; i += 3)
        {
            
            yield return BuildTicketData(splittedText,i, ticketCulture);
        }
    }

    private string BuildTicketData(string[] splittedText, int i, string ticketCulture)
    {
        // Hard-coded
        string title = splittedText[i];
        string dateAsString = splittedText[i + 1];
        string timeAsString = splittedText[i + 2];

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

    private void SaveTicketsData(StringBuilder strongBuilder)
    {
        string resultPath = Path.Combine(_ticketsFolder, "aggregatedTickets.txt");
        File.WriteAllText(resultPath, strongBuilder.ToString());
        Console.WriteLine("The results have been saved to aggregatedTickets.txt file.");
    }

    private static string ExtractDomain(string webAddress)
    {
        int lastDotIndex = webAddress.LastIndexOf('.');
        return webAddress.Substring(lastDotIndex);
    }
}