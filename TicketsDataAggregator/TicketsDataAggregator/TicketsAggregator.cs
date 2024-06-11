// See https://aka.ms/new-console-template for more information

using UglyToad.PdfPig.Content;
using UglyToad.PdfPig;
using static System.Net.Mime.MediaTypeNames;
using System.Globalization;

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
        foreach (string pdfFilePath in Directory.GetFiles(_ticketsFolder, "*.pdf"))
        {
            using PdfDocument document = PdfDocument.Open(pdfFilePath);
            Page page = document.GetPage(1);
            //string pageContent = page.Text;
            string[] splittedText = page.Text.Split(new string[] { 
                                                    "Title:", "Date:", "Time:", "Visit us:" },
                                                    StringSplitOptions.TrimEntries);
            string webAddress = splittedText.Last();
            string domain = ExtractDomain(webAddress); // returns one of ".com", ".fr", ".jp".
            string ticketCulture = _domainToCultureMapping[domain];

            for (int i = 1; i < splittedText.Length - 3; i += 3)
            {
                // Hard-coded
                string title = splittedText[i];
                string dateAsString = splittedText[i + 1];
                string timeAsString = splittedText[i + 2];

                DateTime date = DateTime.Parse(dateAsString, new CultureInfo(ticketCulture));
                //DateOnly dateOnly = DateOnly.Parse(dateAsString, new CultureInfo(ticketCulture));
                TimeOnly timeOnly = TimeOnly.Parse(timeAsString, new CultureInfo(ticketCulture));

            }
        }
    }

    private static string ExtractDomain(string webAddress)
    {
        int lastDotIndex = webAddress.LastIndexOf('.');
        return webAddress.Substring(lastDotIndex);
    }
}