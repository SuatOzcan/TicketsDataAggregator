using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace TicketsDataAggregator.FileAccess
{
    internal class PDFReader : IPDFReader
    {
        public IEnumerable<string> ReadPDF(string ticketsFolder)
        {
            foreach (string pdfFilePath in Directory.GetFiles(ticketsFolder, "*.pdf"))
            {
                using PdfDocument document = PdfDocument.Open(pdfFilePath);
                Page page = document.GetPage(1);
                //string pageContent = page.Text;
                yield return page.Text;
            }
        }
    }
}
