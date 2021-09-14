namespace DellWarranty.Services;
public class ExportToFileService
{
    public static void Export(List<DellWarrantyPayload> result)
    {
        using StreamWriter sr = new(Path.Combine(Environment.CurrentDirectory, "results.csv"));
        sr.WriteLine("Service Tag;Product Description;Country;Service Description;Entitlement Type;Start Date;End Date");
        foreach (var item in result)
        {
            if (!item.Entitlements.Any())
            {
                sr.WriteLine($"{item.ServiceTag};Tag not available;;;;;");
            }
            foreach (var entitlements in item.Entitlements)
            {
                sr.WriteLine($"{item.ServiceTag};{item.ProductLineDescription};{item.CountryCode};{entitlements.ServiceLevelDescription};{entitlements.EntitlementType};{entitlements.StartDate:dd/MM/yyyy};{entitlements.EndDate:dd/MM/yyyy}");
            }
        }
        Console.WriteLine();
        Console.WriteLine($"Warranties data exported to: {Path.Combine(Environment.CurrentDirectory, "results.csv")}");
    }
}
