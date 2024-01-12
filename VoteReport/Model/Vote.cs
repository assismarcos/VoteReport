using CsvHelper.Configuration.Attributes;

namespace VoteReport.Model;

public class Vote
{
    [Name("id")]
    public int Id { get; set; }
    [Name("bill_id")]
    public int BillId { get; set; }
}