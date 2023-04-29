using System.Text.RegularExpressions;

namespace TestKaspersky;

public class FileSelector
{
    private readonly int _id;
    private int _progress = 0;
    private int _max = 0;
    private readonly IReportGenerator _reportGenerator = new ReportGenerator();
    private const int ProgressBarLenght = 50;

    public FileSelector(int id)
    {
        _id = id;
    }

    public async Task ScanDirectory(string path, Regex regex)
    {
        await Task.Run(() =>
        {
            string[] allFiles = Directory.GetFiles(path);
            _max = allFiles.Length;
            foreach (var file in allFiles)
            {
                string[] filePathWords = file.Split(new []{'\\', '/'}, StringSplitOptions.RemoveEmptyEntries);
                if (regex.IsMatch(filePathWords[filePathWords.Length - 1]))
                    _reportGenerator.Generate(file);
                _progress++;
                Thread.Sleep(5000);
            }
            Console.WriteLine($"Отчёт {_id} - готов");
        });
    }

    public void GetStatus()
    {
        if (_progress == _max)
        {
            ShowAll();
            return;
        }
        Console.WriteLine($"Отчёт {_id} - готовность {_progress}/{_max}");
        Console.Write("[");
        float progressBar = (float)_progress / _max;
        for (int i = 0; i < ProgressBarLenght; i++)
        {
            if (i < progressBar*ProgressBarLenght)
            {
                Console.Write("=");
            }
            else
            {
                Console.Write(" ");
            }
        }
        Console.WriteLine("]");
    }
    
    private void ShowAll()
    {
        Console.WriteLine($"Отчёт {_id}: ");
        if (_reportGenerator.ServiceReports.Count == 0)
        {
            Console.WriteLine("Отчёт пуст");
        }
            foreach (var report in _reportGenerator.ServiceReports)
        {
            report.ReportToConsole();
        }
    }
}