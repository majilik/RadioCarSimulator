using System;

namespace RadioCarSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            new Simulator(Console.WriteLine, Console.ReadLine).Run();
        }
    }
}
