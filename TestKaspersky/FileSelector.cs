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

    public void ScanDirectory(string path)
    {
        string[] allFiles = Directory.GetFiles(path);
        foreach (var file in allFiles)
        {
            if(_regex.IsMatch(file))
                _reportGenerator.Generate($"{file}");
        }
    }

    public void ShowAll()
    {
        foreach (var report in _reportGenerator.ServiceReports)
        {
            report.ReportToConsole();
        }
    }
}