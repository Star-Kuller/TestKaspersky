using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TestKaspersky;

public class CommandParcer : IParcer
{
    private List<FileSelector> _tasks = new List<FileSelector>();
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
                _tasks.Add(new FileSelector(_tasks.Count));
                _tasks[_tasks.Count - 1].ScanDirectory(commandWords[1], new Regex(commandWords[2]));
                Console.WriteLine($"Отчёт {_tasks.Count-1} создан");
                break;
            case "Помощь":
            case "помощь":
                Console.WriteLine("Помощь - Список комманд\n" +
                                  "Создать <Путь> <Шаблон> - Создаёт отчёт на основе логов из папки и шаблона (путь указывать без пробелов)\n" +
                                  "Статус <ID> - Узнать состояние отчёта под номером ID\n" +
                                  "Статус - Узнать состояние всех отчётов\n" +
                                  "Завершить - Завершает работу программы");
                break;
            case "Статус":
            case "статус":
                if (commandWords.Length != 2)
                {
                    Console.WriteLine($"Всего отчётов: {_tasks.Count}");
                    foreach (var task in _tasks)
                    {
                        task.GetStatus();
                    }
                    break;
                }
                    if (_tasks.Count > Convert.ToInt32(commandWords[1]))
                {
                    _tasks[Convert.ToInt32(commandWords[1])].GetStatus();
                    break;
                }
                Console.WriteLine("Такого отчёта нет");
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