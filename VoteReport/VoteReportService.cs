using VoteReport.Model;
using VoteReport.Repository;

namespace VoteReport;

public class VoteReportService
{
    private readonly Dictionary<string, string> _files;

    private readonly CsvRepository _csvRepository;
    private readonly string _csvFilesLocation;
    private IEnumerable<Legislator> _legislators = new List<Legislator>();
    private IEnumerable<VoteResult> _voteResults = new List<VoteResult>();
    private IEnumerable<Bill> _bills = new List<Bill>();
    private IEnumerable<Vote> _votes = new List<Vote>();
    
    public VoteReportService(CsvRepository csvRepository, string? csvFilesLocation)
    {
        _csvRepository = csvRepository;
        _csvFilesLocation = csvFilesLocation ?? Directory.GetCurrentDirectory();
        
        _files = new Dictionary<string, string>
        {
            {"legislators", Path.Combine(_csvFilesLocation, "legislators.csv")}, 
            {"vote_results", Path.Combine(_csvFilesLocation, "vote_results.csv")}, 
            {"bills", Path.Combine(_csvFilesLocation, "bills.csv")}, 
            {"votes", Path.Combine(_csvFilesLocation, "votes.csv")}, 
        };
    }
    
    private IEnumerable<LegislatorCountResult> GetLegislatorsSupportOrOpposeCount()
    {
        var query =	
            from l in _legislators
            orderby l.Id
            select new LegislatorCountResult
            {
                Id = l.Id,
                Name = l.Name,
                SupportedBillsCount = _voteResults.Count(vr => vr.LegislatorId == l.Id && vr.VoteType == VoteType.Yea),
                OpposedBillsCount = _voteResults.Count(vr => vr.LegislatorId == l.Id && vr.VoteType == VoteType.Nay),
            };
        
        return query.ToList();
    }

    private IEnumerable<BillCountResult> GetBillSupportersCount()
    {
        var query = 
            from v in _votes
            join vr in _voteResults on v.Id equals vr.VoteId into voteVoteResult
            join b in _bills on v.BillId equals b.Id
		
            let suppCount = voteVoteResult.Count(vvr => vvr.VoteId == v.Id && vvr.VoteType == VoteType.Yea)
            let oppCounter = voteVoteResult.Count(vvr => vvr.VoteId == v.Id && vvr.VoteType == VoteType.Nay)
		
            join l in _legislators on b.SponsorId equals l.Id into billsLegislator
            from bl in billsLegislator.DefaultIfEmpty()
	 
            select new BillCountResult
            {
                Id = v.BillId,
                Title = b.Title,
                SupporterCount = suppCount,
                OpposerCount = oppCounter,
                PrimarySponsor = bl == null ? "Unknown" : bl.Name
            };

        return query.ToList();
    }

    private void LoadFiles()
    {
        _legislators = _csvRepository.ReadFile<Legislator>(_files["legislators"]);
        _bills = _csvRepository.ReadFile<Bill>(_files["bills"]);
        _votes = _csvRepository.ReadFile<Vote>(_files["votes"]);
        _voteResults = _csvRepository.ReadFile<VoteResult>(_files["vote_results"]);
    }

    public List<string> ValidateFiles()
    {
        return (
            from file in _files 
            where !File.Exists(file.Value) 
            select $"{file.Value} could not be found.").ToList();
    }

    public void GenerateReports()
    {
        LoadFiles();
        
        var legislatorsCount = GetLegislatorsSupportOrOpposeCount();
        var billsCount = GetBillSupportersCount();
        
        _csvRepository.SaveFile(Path.Combine(_csvFilesLocation, "legislators-support-oppose-count.csv"), legislatorsCount);
        _csvRepository.SaveFile(Path.Combine(_csvFilesLocation, "bills-count.csv"), billsCount);
    }
}