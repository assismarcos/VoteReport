using CsvHelper.Configuration.Attributes;

namespace VoteReport.Model;

public class BillCountResult
{
    [Name("id")]
    public int Id { get; set; }
    [Name("title")]
    public string Title { get; set; } = string.Empty;
    [Name("supporter_count")]
    public int SupporterCount { get; set; }
    [Name("opposer_count")]
    public int OpposerCount { get; set; }
    [Name("primary_sponsor")]
    public string PrimarySponsor { get; set; } = string.Empty;
}