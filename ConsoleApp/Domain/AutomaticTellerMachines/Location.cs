namespace ConsoleApp.Domain.AutomaticTellerMachines;

internal class Location
{
    public int Id { get; set; }
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public List<ATM> ATMs { get; set; } = [];
}
