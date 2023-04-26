namespace TestKaspersky;
using System.Text.RegularExpressions;

public class ReportGenerator : IReportGenerator
{
    private readonly List<ServiceReport> _serviceReports = new List<ServiceReport>();
    private readonly Regex _pathRegex = new Regex(@"(\w*)\.(\d*)\.log");
    private readonly Regex _currentPathRegex = new Regex(@"(\w*)\.log");

    public List<ServiceReport> ServiceReports => _serviceReports;

    public void Generate(string path)
    {
        if ( !(_currentPathRegex.IsMatch(path) || _pathRegex.IsMatch(path)) )
            return;
        bool isNotCurrentPath = _pathRegex.IsMatch(path);
        Match fileName = isNotCurrentPath ? _pathRegex.Match(path) : _currentPathRegex.Match(path);
        string[] fileNameWords = fileName.ToString().Split('.', StringSplitOptions.RemoveEmptyEntries);
        int serviceIndex = GetServiceIndex(fileNameWords[0]);
        _serviceReports[serviceIndex].Rotation = isNotCurrentPath ? Convert.ToInt32(fileNameWords[1]) : 0;
        string[] lines = File.ReadAllLines(path);
        foreach (var line in lines)
        {
            string[] parts = line.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            _serviceReports[serviceIndex].AddDate(DateTime.ParseExact(parts[0],
                $"dd.MM.yyyy HH:mm:ss.fff", null));
            _serviceReports[serviceIndex].AddCategory(parts[1]);
        }
    }
    
    private int GetServiceIndex(string name)
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