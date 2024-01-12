using CsvHelper.Configuration.Attributes;

namespace VoteReport.Model;

public class VoteResult
{
    [Name("id")]
    public int Id { get; set; }
    [Name("legislator_id")]
    public int LegislatorId { get; set; }
    [Name("vote_id")]
    public int VoteId { get; set; }
    [Name("vote_type")]
    public VoteType VoteType { get; set; }
}