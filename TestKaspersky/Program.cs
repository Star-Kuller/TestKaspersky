using System.Text.RegularExpressions;

namespace TestKaspersky;
class Program
{
    static void Main(string[] args)
    {
        FileSelector fileSelector = new FileSelector(new Regex(@"(\w*)"));
        fileSelector.ScanDirectory(@"C:\MyProjects\TestKaspersky\TestKaspersky\TestData");
        fileSelector.ShowAll();
    }
}