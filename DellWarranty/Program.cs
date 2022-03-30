IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<DellWarrantyService>();
        services.AddSingleton<Worker>();
    })
    .Build();

await host.Services.GetRequiredService<Worker>().ExecuteAsync();
