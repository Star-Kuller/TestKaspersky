namespace TestKaspersky;

public interface IReportGenerator
{
    public List<ServiceReport> ServiceReports { get; }
    public void Generate(string path);
}