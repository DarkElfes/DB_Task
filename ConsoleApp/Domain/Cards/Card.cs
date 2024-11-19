using ConsoleApp.Domain.Users;
using ConsoleApp.Domain.Transactions;

namespace ConsoleApp.Domain.Cards;

internal class Card
{
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public string Number { get; set; } = string.Empty;
    public string CVV { get; set; } = string.Empty;
    public string Pin { get; set; } = string.Empty;
    public bool IsBlocked { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public Currency Currency { get; set; } = Currency.UAH;
    public User User { get; set; } = null!;

    public List<Transaction> OwnTransactions { get; set; } = [];
    public List<TransferTransaction> ReceivedTransactions { get; set; } = [];
}
