using ConsoleApp.Domain.Merchants;

namespace ConsoleApp.Domain.OnlineServices;

internal class OnlineService
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Merchant Merchant { get; set; } = null!;
}
