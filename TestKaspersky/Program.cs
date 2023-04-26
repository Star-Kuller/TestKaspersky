using System.Text.RegularExpressions;
using TestKaspersky;

namespace WriteAndReadTextFile;
class Program
{
    static void Main(string[] args)
    {
        ServiceReport serviceReport1 = new ServiceReport("TestService1");
        ServiceReport serviceReport2 = new ServiceReport("TestService2");
        LogFileScaner logFileScaner = new LogFileScaner(new Regex(@"\w*"));
        logFileScaner.Scan(@"C:\MyProjects\TestKaspersky\TestKaspersky\TestData\serviceA.log");
        logFileScaner.Scan(@"C:\MyProjects\TestKaspersky\TestKaspersky\TestData\serviceB.1.log");
        serviceReport1.AddCategory("TestCategory");
        serviceReport1.AddCategory("TestCategory");
        serviceReport1.AddCategory("TestCategory");
        serviceReport1.AddCategory("TestCategory2");
        serviceReport1.AddCategory("TestCategory2");
        serviceReport2.AddCategory("TestCategory3");
        serviceReport2.AddCategory("TestCategory4");
        serviceReport1.AddDate(new DateTime(2008, 3, 1, 7, 1, 9, 2));
        serviceReport1.AddDate(new DateTime(2101, 1, 2, 2, 2, 3, 100));
        serviceReport2.AddDate(new DateTime(2018, 3, 1, 7, 3, 0, 2));
        serviceReport2.AddDate(new DateTime(2111, 1, 2, 2, 2, 3, 100));
        serviceReport1.Rotation = 4;
        serviceReport2.Rotation = 2;
        serviceReport1.ReportToConsole();
        serviceReport2.ReportToConsole();
    }
}