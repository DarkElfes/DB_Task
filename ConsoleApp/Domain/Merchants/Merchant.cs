using ConsoleApp.Domain.OnlineServices;

namespace ConsoleApp.Domain.Merchants;

internal class Merchant
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public List<OnlineService> OnlineServices { get; set; } = [];
}
