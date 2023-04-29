namespace TestKaspersky;

public class ServiceReport
{
    private struct Category
    {
        public string Name { get; }
        public int Amount = 1;

        public Category(string name)
        {
            this.Name = name;
        }

        public void IncrementAmount()
        {
            Amount++;
        }
    }

    public string ServiceName { get; }
    private readonly List<Category> _categorys = new List<Category>();
    private DateTime _earliest;
    private DateTime _latest;
    private int _rotation;
    public int Rotation
    {
        get => _rotation;
        set => _rotation = ++value > _rotation ? value : _rotation;
    }

    public ServiceReport(string serviceName)
    {
        this.ServiceName = serviceName;
    }

    public void AddDate(DateTime dateTime)
    {
        if (_earliest == new DateTime())
            _earliest = dateTime;
        if (_latest == new DateTime())
            _latest = dateTime;
        if (dateTime < _earliest)
            _earliest = dateTime;
        if (dateTime > _latest)
            _latest = dateTime;
    }

    public void AddCategory(string name)
    {
        bool needToAdd = true;
        for (int i = 0; i < _categorys.Count; i++)
        {
            if (_categorys[i].Name == name)
            {
                Category c = _categorys[i];
                c.IncrementAmount();
                _categorys[i] = c;
                needToAdd = false;
            }
        }

        if (needToAdd == true)
        {
            _categorys.Add(new Category(name));
        }
    }

    public void ReportToConsole()
    {
        Console.WriteLine($"\nСервис: {ServiceName}\n" +
                          $"Самая ранняя запись: {_earliest.ToString($"dd.MM.yyyy HH:mm:ss.fff")}\n" +
                          $"Самая поздняя запись: {_latest.ToString($"dd.MM.yyyy HH:mm:ss.fff")}");
        Console.Write("Всего записей:[");
        foreach (var c in _categorys)
        {
            Console.Write($" {c.Name}:{c.Amount}");
        }
        Console.WriteLine(" ]\n" +
                          $"Количество ротаций: {Rotation}\n");
    }
}