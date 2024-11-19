using ConsoleApp.Domain.OnlineServices;

namespace ConsoleApp.Domain.Transactions;

internal class OnlineServiceTransaction : Transaction
{
    public OnlineService OnlineService { get; set; } = null!;
    public string ServiceReceiptNumber { get; set; } = string.Empty;


    public Dictionary<string, (int Count, double Price)> GetItems(){
        //get receipt from request to online service by receipt number
        //parse items list
        //return items
        return [];
    }
}

