using VoteReport;
using VoteReport.Repository;

var filesLocation = Directory.GetCurrentDirectory();
switch (args.Length)
{
    case 1 when args[0] == "--help":
        Console.WriteLine("Usage: VoteReport [--files-location <String>] [--help]\n");
        Console.WriteLine("VoteReport {0}\n", typeof(Program).Assembly.GetName().Version);
        Console.WriteLine("Options:");
        Console.WriteLine("  --files-location <String>  Directory of CSV source files. Default value is the application directory");
        Console.WriteLine("  --help                     Show help message\n");
        return;
    case 2 when args[0] == "--files-location":
        Console.WriteLine("VoteReport {0}\n", typeof(Program).Assembly.GetName().Version);
        filesLocation = args[1];
        break;
}

var voteReportService = new VoteReportService(new CsvRepository(), filesLocation);

Console.WriteLine("Validating CSV files");
var validationResult = voteReportService.ValidateFiles();
if (validationResult.Any())
{
    Console.WriteLine("Errors found:");
    validationResult.ForEach(x => Console.WriteLine($" {x}"));
    Console.WriteLine("CSV files not generated\n");
    return;
}

Console.WriteLine("Generating CSV files at {0}", filesLocation);
voteReportService.GenerateReports();
Console.WriteLine("CSV files generated.\n");