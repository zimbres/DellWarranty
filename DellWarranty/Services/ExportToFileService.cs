namespace DellWarranty.Services;
public static class ExportToFileService
{
    public static void Export(List<DellWarrantyPayload> result)
    {
        using StreamWriter srEntitlements = new(Path.Combine(Environment.CurrentDirectory, "entitlements.csv"));
        srEntitlements.WriteLine("Service Tag;Product Description;Country;Service Description;Entitlement Type;Start Date;End Date");
        using StreamWriter srDevices = new(Path.Combine(Environment.CurrentDirectory, "devices.csv"));
        srDevices.WriteLine("Service Tag;Country;Product Description;Ship Date;Age in Months");

        foreach (var item in result)
        {
            if (!item.Invalid)
            {
                srDevices.WriteLine($"{item.ServiceTag};{item.CountryCode};{item.ProductLineDescription};{DateOnly.FromDateTime(item.ShipDate.DateTime)};{(DateTime.Now.Year - item.ShipDate.Year) * 12 + DateTime.Now.Month - item.ShipDate.Month}");
            }
            else
            {
                srDevices.WriteLine($"{item.ServiceTag};Tag not available;;;");
            }
            if (!item.Entitlements.Any())
            {
                srEntitlements.WriteLine($"{item.ServiceTag};Tag not available;;;;;");
            }
            foreach (var entitlements in item.Entitlements)
            {
                srEntitlements.WriteLine($"{item.ServiceTag};{item.ProductLineDescription};{item.CountryCode};{entitlements.ServiceLevelDescription};{entitlements.EntitlementType};{entitlements.StartDate:dd/MM/yyyy};{entitlements.EndDate:dd/MM/yyyy}");
            }
        }
        Console.WriteLine();
        Console.WriteLine($"Warranties data exported to: {Path.Combine(Environment.CurrentDirectory, "entitlements.csv")}");
        Console.WriteLine($"Devices data exported to: {Path.Combine(Environment.CurrentDirectory, "devices.csv")}");
    }
}
