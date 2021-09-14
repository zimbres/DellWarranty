global using DellWarranty.Models;
global using DellWarranty.Services;
global using DellWarranty.Settings;
using System.Reflection;

namespace DellWarranty;

public class Worker
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;

    public Worker(IConfiguration configuration, ILogger<Worker> logger)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task ExecuteAsync()
    {
        Console.WriteLine($"Software version: {Assembly.GetExecutingAssembly().GetName().Version}");

        var serviceProvider = ServicesSettings.GetServices(_configuration);
        var dellWarrantyService = serviceProvider.GetService<DellWarrantyService>();

        Console.Write("Type 1 for manually enter Service Tags or 2 for load it from a file: ");
        var input = Console.ReadLine();

        if (input != "1" & input != "2")
        {
            Console.WriteLine("You must choice '1' or '2'!");
            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
            return;
        }

        var choice = int.Parse(input);

        var tags = Array.Empty<string>();
        if (choice == 1)
        {
            Console.Write("Enter Service Tags separeted by comma (,): ");

            tags = Console.ReadLine().Replace(" ", "").Split(",");
            if (tags.Length < 1 || (tags.First() == string.Empty))
            {
                Console.WriteLine("You must enter at list one Service Tag!");
            }
            while (tags.Length < 1 || (tags.First() == string.Empty))
            {
                Console.Write("Tag(s): ");
                tags = Console.ReadLine().Replace(" ", "").Split(",");
            }
        }
        else if (choice == 2)
        {
            Console.WriteLine("File must be named as tags.txt with one Service Tag per line!");
            Console.WriteLine("Press ENTER to continue!");
            Console.ReadLine();
            try
            {
                tags = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "tags.txt"));
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File was not found! Ensure that 'tags.txt' is in the same folder of this program!");
                Console.WriteLine("Press ENTER to exit.");
                Console.ReadLine();
                return;
            }
        }
        else
        {
            Console.WriteLine("Invalid input");
        }

        Console.Write("Processing..");

        var result = new List<DellWarrantyPayload>();
        var tagList = string.Empty;
        var lenght = 99;
        for (int i = 0; i < tags.Length; i++)
        {
            foreach (var tag in tags.Skip(i).Take(lenght))
            {
                tagList += tag + ",";
            }
            i += (lenght - 1);

            var serviceTags = tagList.Remove(tagList.Length - 1, 1);
            result.AddRange(await dellWarrantyService.GetDellWarranty(serviceTags));

            tagList = string.Empty;

            Console.Write(".");
        }

        ExportToFileService.Export(result);
        Console.WriteLine("Press ENTER to exit.");
        Console.ReadLine();
    }
}
