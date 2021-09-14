namespace DellWarranty.Settings;
public class ServicesSettings
{
    public static IServiceProvider GetServices(IConfiguration configuration)
    {
        var container = new ServiceCollection();

        container.AddSingleton(configuration);
        container.AddSingleton<DellWarrantyService>();
        return container.BuildServiceProvider();
    }
}
