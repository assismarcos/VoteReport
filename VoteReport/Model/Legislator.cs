using CsvHelper.Configuration.Attributes;

namespace VoteReport.Model;

public class Legislator
{
    [Name("id")]
    public int Id { get; set; }
    [Name("name")]
    public string Name { get; set; } = string.Empty;
}