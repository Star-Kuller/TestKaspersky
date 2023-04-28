using System.Text.RegularExpressions;

namespace TestKaspersky;
class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите путь к файлу: ");
        string path = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException();
        Console.Write("Введите регулярное выражение: ");
        string regex = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(regex))
            regex = @"\w*";
        FileSelector fileSelector = new FileSelector(new Regex(regex));
        Task task = fileSelector.ScanDirectory(path);
        task.Wait();
        fileSelector.ShowAll();
    }
}