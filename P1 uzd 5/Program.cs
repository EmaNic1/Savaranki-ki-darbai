using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1_uzd_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double y,
                   x;
            Console.WriteLine("Įveskite x  reikšmę:");
            x = double.Parse(Console.ReadLine());

            Console.WriteLine("Įveskite y reikšmę:");
            y = double.Parse(Console.ReadLine());
            if (x * x * x - y != 0)
            { 
                Console.WriteLine("x={0}  y={1}  f(x,y)={2,5:f2}", x, y, Skaiciavimai(x, y));
            }
            else
                Console.WriteLine("Funkcijos nėra");
        }

        static double Skaiciavimai(double x, double y)
        {
            return (Math.Pow(y, 2) - 2 * y * x + Math.Pow(x, 2)) / (Math.Pow(x, 3) - y);
        }
    }
}
