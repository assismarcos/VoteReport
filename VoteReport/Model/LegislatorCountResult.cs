using CsvHelper.Configuration.Attributes;

namespace VoteReport.Model;

public class LegislatorCountResult
{
    [Name("id")]
    public int Id { get; set; }
    [Name("name")] 
    public string Name { get; set; } = string.Empty;
    [Name("num_supported_bills")]
    public int SupportedBillsCount { get; set; }
    [Name("num_opposed_bills")]
    public int OpposedBillsCount { get; set; }
}