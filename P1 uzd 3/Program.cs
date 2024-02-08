using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1_uzd_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //5 nr.
            double fx;
            double x;
            Console.WriteLine("Įveskite x reikšmę:");
            x = double.Parse(Console.ReadLine());
            Console.Clear();
            Console.SetCursorPosition(5, 6);
            if (-1 <= x && x < 0)
            {
                fx = 1 / (x - 5);
                Console.WriteLine("Pirmos salygos reiksme x = {0,6:f3}, fx = {1,8:f3}", x, fx);
            }
            else
            if (0 <= x && x < 1)
            {
                fx = x + 1;
                Console.WriteLine("Antros salygos reiksme x = {0,6:f3}, fx = {1,8:f3}", x, fx);
            }

            else
            if (x * x - x - 1 >= 0)
            {
                fx = Math.Pow(x * x - x - 1, 0.5);
                Console.WriteLine("Kitais atvejais reiksme x = {0,6:f3}, fx = {1,8:f3}", x, fx);
            }
            else
                Console.WriteLine("Funkcija neegzistuoja");
        }
    }
}
