﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Content;

namespace TicketsDataAggregator.FileAccess
{
    internal interface IPDFReader
    {
        IEnumerable<string> ReadPDF(string ticketsFolder);
    }
}
