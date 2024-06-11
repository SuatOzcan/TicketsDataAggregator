using UglyToad;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

// See https://aka.ms/new-console-template for more information

 const string TicketsFolder = @"..\..\..\..\..\Tickets";

try
{
    
    var ticketsAggregator = new TicketsAggregator(TicketsFolder);

    ticketsAggregator.Run();
}

catch (Exception ex)
{
    Console.WriteLine("An exception occurred. Exception message: {0}", ex.Message);
}

