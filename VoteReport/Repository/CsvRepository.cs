using System.Globalization;
using CsvHelper;

namespace VoteReport.Repository;

public class CsvRepository
{
    public IEnumerable<T> ReadFile<T>(string fileName)
    {
        using var reader = new StreamReader(fileName);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var result = csv.GetRecords<T>().ToList();
        return result;
    }

    public void SaveFile<T>(string fileName, IEnumerable<T> records)
    {
        using var writer = new StreamWriter(fileName) ;
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(records);
    }
}