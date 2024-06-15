using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDataAggregator.FileAccess
{
    internal interface IFileWriter
    {
        void Write(string content, params string[] pathParts);
    }
}
