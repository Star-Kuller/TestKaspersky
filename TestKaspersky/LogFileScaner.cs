namespace TestKaspersky;
using System.Text.RegularExpressions;

public class LogFileScaner
{
    private readonly List<ServiceReport> _serviceReports = new List<ServiceReport>();
    private readonly Regex _pathRegex = new Regex(@"(\w*)\.(\d*)\.log");
    private Regex _regex;

    public LogFileScaner(Regex regex)
    {
        _regex = regex;
    }

    public void Scan(string path)
    {
        Match fileName = _pathRegex.Match(path);
        string[] fileNameWords = fileName.ToString().Split('.', StringSplitOptions.RemoveEmptyEntries);
        int index = AddService(fileNameWords[0]);
        Console.WriteLine(fileNameWords[0]);
        Console.WriteLine(fileNameWords[1]);
        Console.WriteLine(fileNameWords[2]);
        _serviceReports[index].Rotation = Convert.ToInt32(fileNameWords[1]);
        string[] lines = File.ReadAllLines(path);
        foreach (var line in lines)
        {
            if (_regex.IsMatch(line))
            {
                string[] parts = line.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                _serviceReports[index].AddDate(DateTime.ParseExact(parts[0], $"dd.MM.yyyy HH:mm:ss.fff", null));
            }
        }

        _serviceReports[index].ReportToConsole();
    }
    
    public int AddService(string name)
    {
        for (int i = 0; i <= _serviceReports.Count; i++)
        {
            if(i == _serviceReports.Count)
                _serviceReports.Add(new ServiceReport(name));
            if (_serviceReports[i].ServiceName == name)
            {
                return i;
            }
        }
        return _serviceReports.Count;
    }
}