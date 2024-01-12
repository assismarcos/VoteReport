using CsvHelper.Configuration.Attributes;

namespace VoteReport.Model;

public class Bill 
{
    [Name("id")]
    public int Id { get; set; }
    [Name("title")]
    public string Title { get; set; } = string.Empty;
    [Name("sponsor_id")]
    public int SponsorId { get; set; }
}