using TicketsDataAggregator.FileAccess;

// See https://aka.ms/new-console-template for more information

const string TicketsFolder = @"..\..\..\..\..\Tickets";

try
{
    
    var ticketsAggregator = new TicketsAggregator(TicketsFolder, new FileWriter(), new PDFReader());

    ticketsAggregator.Run();
}

catch (Exception ex)
{
    Console.WriteLine("An exception occurred. Exception message: {0}", ex.Message);
}

