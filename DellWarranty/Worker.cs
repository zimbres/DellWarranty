using System.Text;

namespace DellWarranty;

public class Worker
{
    private readonly DellWarrantyService _warrantyService;

    public Worker(DellWarrantyService warrantyService)
    {
        _warrantyService = warrantyService;
    }

    public async Task ExecuteAsync()
    {
        Console.WriteLine($"Software version: {Assembly.GetExecutingAssembly().GetName().Version}");

        Console.Write("Type 1 for manually enter Service Tags or 2 for load it from a file: ");
        var input = Console.ReadLine();

        if (input != "1" && input != "2")
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
        if (choice == 2)
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
    
        Console.Write("Processing..");

        var result = new List<DellWarrantyPayload>();
        StringBuilder tagList = new();
        var lenght = 99;
        for (int i = 0; i < tags.Length; i++)
        {
            foreach (var tag in tags.Skip(i).Take(lenght))
            {
                tagList.Append(tag + ",");
            }
            i += (lenght - 1);

            var serviceTags = tagList.Remove(tagList.Length - 1, 1).ToString();
            result.AddRange(await _warrantyService.GetDellWarranty(serviceTags));

            tagList.Clear();

            Console.Write(".");
        }

        ExportToFileService.Export(result);
        Console.WriteLine("Press ENTER to exit.");
        Console.ReadLine();
    }
}
