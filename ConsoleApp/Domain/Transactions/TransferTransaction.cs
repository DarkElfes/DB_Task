using ConsoleApp.Domain.Cards;

namespace ConsoleApp.Domain.Transactions;

internal class TransferTransaction : Transaction
{
    public Card RecipientCard { get; set; } = null!;
    public decimal RecipientCardBalance { get; set; }
}
