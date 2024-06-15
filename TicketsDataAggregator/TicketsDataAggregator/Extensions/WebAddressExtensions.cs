using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDataAggregator.Extensions
{
    internal static class WebAddressExtensions
    {
        public static string ExtractDomain(this string webAddress)
        {
            int lastDotIndex = webAddress.LastIndexOf('.');
            return webAddress.Substring(lastDotIndex);
        }
    }
}
