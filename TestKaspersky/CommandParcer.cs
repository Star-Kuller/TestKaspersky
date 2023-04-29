using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TestKaspersky;

public class CommandParcer : IParcer
{
    private List<Task> _tasks = new List<Task>();
    public void Parce(string command)
    {
        if(string.IsNullOrWhiteSpace(command))
            Console.WriteLine("Пустая строка");
        string[] commandWords = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        switch (commandWords[0])
        {
            case "Создать":
            case "создать":
                if (commandWords.Length != 3)
                    throw new ArgumentException("Неверное количество аргументов");
                _tasks.Add(FileSelector.ScanDirectory(commandWords[1], new Regex(commandWords[2]), _tasks.Count));
                Console.WriteLine($"ID: {_tasks.Count-1}");
                break;
            case "Помощь":
            case "помощь":
                Console.WriteLine("Помощь - Список комманд\n" +
                                  "Создать <Путь> <Шаблон> - Создаёт отчёт на основе логов из папки и шаблона (путь указывать без пробелов)\n" +
                                  "Статус <ID> - Узнать состояние отчёта под номером ID\n" +
                                  "Завершить - Завершает работу программы");
                break;
            case "Статус":
            case "статус":
                Console.WriteLine("WIP");
                break;
            case "Завершить":
            case "завершить":
                Process.GetCurrentProcess().Kill();
                break;
            default:
                Console.WriteLine("Неизвестная комманда");
                break;
        }
        
    }
}