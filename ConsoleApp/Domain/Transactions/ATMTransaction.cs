using ConsoleApp.Domain.AutomaticTellerMachines;

namespace ConsoleApp.Domain.Transactions;

internal class ATMTransaction : Transaction
{
    public ATM ATM { get; set; } = null!;
    public ATMTransactionType ATMTransactionType { get; set; } = default!;
}