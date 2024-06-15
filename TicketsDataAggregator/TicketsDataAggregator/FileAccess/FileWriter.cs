using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDataAggregator.FileAccess
{
    internal class FileWriter : IFileWriter
    {
        public void Write(string content, params string[] pathParts)
        {
            string resultPath = Path.Combine(pathParts);
            File.WriteAllText(resultPath, content);
            Console.WriteLine("The results have been saved to aggregatedTickets.txt file.");
        }
    }
}
