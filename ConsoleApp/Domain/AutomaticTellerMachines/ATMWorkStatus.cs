
namespace ConsoleApp.Domain.AutomaticTellerMachines;

internal enum ATMWorkStatus
{
    Online,
    Offline,
    OutOfService,
    OutOfCash,
    Maintenance,
    LowCash,
    PrinterError,
    CardReaderError,
    SoftwareUpdate,
}
