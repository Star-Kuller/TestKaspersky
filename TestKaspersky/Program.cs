

namespace TestKaspersky;
class Program
{
    static void Main(string[] args)
    {
        IParcer parcer = new CommandParcer();
        Console.WriteLine("Вводите комманды: ");
        while (true)
        {
            parcer.Parce(Console.ReadLine());
        }
    }
}