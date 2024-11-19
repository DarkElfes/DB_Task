namespace ConsoleApp.Domain.AutomaticTellerMachines;

internal class ATM
{
    public int Id { get; set; }
    public ATMWorkStatus WorkStatus { get; set; }
    public decimal Balance { get; set; }
    public Location Location { get; set; } = null!;
}
