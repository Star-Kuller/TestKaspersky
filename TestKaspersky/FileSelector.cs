using System.Text.RegularExpressions;

namespace TestKaspersky;

public static class FileSelector
{
    public static async Task ScanDirectory(string path, Regex regex, int id)
    {
        await Task.Run(() =>
        {
            IReportGenerator reportGenerator = new ReportGenerator();
            int progress = 0;
            string[] allFiles = Directory.GetFiles(path);
            foreach (var file in allFiles)
            {
                string[] filePathWords = file.Split(new []{'\\', '/'}, StringSplitOptions.RemoveEmptyEntries);
                if (regex.IsMatch(filePathWords[filePathWords.Length - 1]))
                    reportGenerator.Generate(file);
                Console.WriteLine($"Отчёт {id} - готовность: {++progress}/{allFiles.Length}");
                Thread.Sleep(10000);
            }
            ShowAll(reportGenerator, id);
        });
    }

    private static void ShowAll(IReportGenerator reportGenerator, int id)
    {
        Console.WriteLine($"Отчёт {id} - готов");
        foreach (var report in reportGenerator.ServiceReports)
        {
            report.ReportToConsole();
        }
    }
}