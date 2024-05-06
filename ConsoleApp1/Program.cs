using System.Diagnostics;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Process.Start(new ProcessStartInfo("steam://launch/730/Dialog") { UseShellExecute = true });
        }
    }
}
