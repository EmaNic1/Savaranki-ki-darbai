using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1_uzd_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double fxy;
            double y,
                   x;
            Console.WriteLine("Įveskite x  reikšmę:");
            x = double.Parse(Console.ReadLine());

            Console.WriteLine("Įveskite y reikšmę:");
            y = double.Parse(Console.ReadLine());


            if(x*x*x-y != 0)
            {
                fxy = (Math.Pow(y, 2) - 2 * y * x + Math.Pow(x, 2)) / (Math.Pow(x, 3) - y);
                Console.WriteLine("x={0}  y={1}  f(x,y)={2,5:f2}", x, y, fxy);
            }
            else
                Console.WriteLine("Funkcijos nėra");
        }
    }
}
