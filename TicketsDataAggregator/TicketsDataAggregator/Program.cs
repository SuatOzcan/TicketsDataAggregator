// See https://aka.ms/new-console-template for more information
const string TicketsFolder = @"F:\C#Projeleri\UltimateC#MasterClass\Section12Strings\TicketsDataAggregator\Tickets";
// const string TicketsFolder = @"..\..\Tickets";

try
{
    var ticketsAggregator = new TicketsAggregator(TicketsFolder);

    ticketsAggregator.Run();
}

catch (Exception ex)
{
    Console.WriteLine("An exception occurred. Exception message: {0}", ex.Message);
}

