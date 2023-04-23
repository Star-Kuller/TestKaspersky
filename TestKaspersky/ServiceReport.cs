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
    private string _serviceName;
    private List<Category> _categorys = new List<Category>();
    private DateTime _earliest;
    private DateTime _latest;
    private int _rotationCounter = 1;
    
    public ServiceReport(string serviceName)
    {
        _serviceName = serviceName;
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
        bool needToAdd = false;
        for (int i = 0; i < _categorys.Count; i++)
        {
            if (_categorys[i].Name == name)
            {
                Category c = _categorys[i];
                c.IncrementAmount();
                _categorys[i] = c;
                needToAdd = true;
            }
        }

        if (needToAdd == false)
        {
            _categorys.Add(new Category(name));
        }
    }

    public void IncrementRotation()
    {
        _rotationCounter++;
    }
    
    public void ReportToConsole()
    {
        Console.WriteLine($"\nСервис: {_serviceName}\n" +
                          $"Самая ранняя запись: {_earliest}\n" +
                          $"Самая поздняя запись: {_latest}");
        Console.Write("Всего записей:[");
        foreach (var c in _categorys)
        {
            Console.Write($" {c.Name}:{c.Amount}");
        }
        Console.WriteLine(" ]\n" +
                          $"Количество ротаций: {_rotationCounter}");
    }
}