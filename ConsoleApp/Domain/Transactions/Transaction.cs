using ConsoleApp.Domain.Cards;

namespace ConsoleApp.Domain.Transactions;

internal class Transaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime TimeStamp { get; set; }
    public TransactionStatus Status { get; set; }
    public Card Card { get; set; } = null!;
    public decimal CardBalance { get; set; }
}
