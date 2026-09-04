using System.Globalization;
using System.Text;

string salesDirectory = Path.Combine(
    AppContext.BaseDirectory,
    "sales-data"
);

string reportPath = Path.Combine(
    AppContext.BaseDirectory,
    "SalesSummary.txt"
);

Console.WriteLine("Sales File Processing Application");
Console.WriteLine("----------------------------------");

if (!Directory.Exists(salesDirectory))
{
    Directory.CreateDirectory(salesDirectory);
    Console.WriteLine("Sales directory created.");
}

string[] salesFiles = Directory.GetFiles(
    salesDirectory,
    "*.txt"
);

Console.WriteLine();
Console.WriteLine($"Number of sales files: {salesFiles.Length}");

foreach (string file in salesFiles)
{
    Console.WriteLine($"File found: {Path.GetFileName(file)}");
}

GenerateSalesSummary(salesDirectory, reportPath);

Console.WriteLine();
Console.WriteLine("Sales summary created at:");
Console.WriteLine(reportPath);

static void GenerateSalesSummary(
    string salesDirectory,
    string reportPath)
{
    if (!Directory.Exists(salesDirectory))
    {
        throw new DirectoryNotFoundException(
            $"Sales directory was not found: {salesDirectory}"
        );
    }

    string[] salesFiles = Directory
        .GetFiles(salesDirectory, "*.txt")
        .OrderBy(Path.GetFileName)
        .ToArray();

    decimal grandTotal = 0m;

    var fileTotals =
        new List<(string FileName, decimal Total)>();

    foreach (string file in salesFiles)
    {
        decimal fileTotal = 0m;

        foreach (string line in File.ReadLines(file))
        {
            if (decimal.TryParse(
                line,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out decimal amount))
            {
                fileTotal += amount;
            }
        }

        grandTotal += fileTotal;

        fileTotals.Add(
            (
                Path.GetFileName(file),
                fileTotal
            )
        );
    }

    StringBuilder report = new StringBuilder();

    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine(
        $"Total Sales: {grandTotal:C}"
    );

    report.AppendLine();
    report.AppendLine("Details:");

    foreach (var item in fileTotals)
    {
        report.AppendLine(
            $"{item.FileName}: {item.Total:C}"
        );
    }

    File.WriteAllText(
        reportPath,
        report.ToString()
    );
}