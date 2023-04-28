using System.Text.RegularExpressions;

namespace TestKaspersky;

public class FileSelector
{
    private readonly Regex _regex;
    private readonly IReportGenerator _reportGenerator = new ReportGenerator();

    public FileSelector(Regex regex)
    {
        _regex = regex;
    }

    public async Task ScanDirectory(string path)
    {
        await Task.Run(() =>
        {
            int progress = 0;
            string[] allFiles = Directory.GetFiles(path);
            foreach (var file in allFiles)
            {
                string[] filePathWords = file.Split(new []{'\\', '/'}, StringSplitOptions.RemoveEmptyEntries);
                if (_regex.IsMatch(filePathWords[filePathWords.Length - 1]))
                    _reportGenerator.Generate(file);
                Console.WriteLine($"{++progress}/{allFiles.Length}");
            }
        });
    }

    public void ShowAll()
    {
        foreach (var report in _reportGenerator.ServiceReports)
        {
            report.ReportToConsole();
        }
    }
}