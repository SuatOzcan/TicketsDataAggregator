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
        foreach (string pdfFilePath in Directory.GetFiles(_ticketsFolder, " *.pdf"))
        {
            using PdfDocument document = PdfDocument.Open(pdfFilePath);
            Page page1 = document.GetPage(1);
            string page1Content = page1.Text;
        }
    }
}