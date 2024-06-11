// See https://aka.ms/new-console-template for more information

using UglyToad.PdfPig.Content;
using UglyToad.PdfPig;
using static System.Net.Mime.MediaTypeNames;

internal class TicketsAggregator
{
    private readonly string _ticketsFolder;

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
            string[] splittedText = page.Text.Split(new string[] { "Title:", "Date:", "Time:", "Visit us:" },
                                                    StringSplitOptions.TrimEntries);
            

            for (int i = 1; i < splittedText.Length - 3; i += 3)
            {
                // Hard-coded
                string title = splittedText[i];
                string date = splittedText[i + 1];
                string time = splittedText[i + 2];
            }
        }
    }
}