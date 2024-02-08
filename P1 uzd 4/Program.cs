using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1_uzd_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char sim; ///simbolis
            double s,
                s1;///realūs skaičiai
            double s2 = 0;///Operacijom atlikti
            Console.WriteLine("Įveskite bet kokį realu skaičių: ");
            s = double.Parse(Console.ReadLine());
            Console.WriteLine("Įveskite antrą bet kokį realu skaičių: ");
            s1 = double.Parse(Console.ReadLine());
            Console.WriteLine("Įveskite bet kokią operaciją(+, -, * arba /): ");
            sim = (char)Console.Read();

            if (sim == '+')
            {
                s2 = s + s1;
                Console.WriteLine("Pirma reikšmė:{0,3:f2}, antra reikšmė:{1,3:f2}, " +
                    "gautas rezultatas:{2,3:f2}", s, s1, s2);
            }

            else
                if (sim == '-')
            {
                s2 = s - s1;
                Console.WriteLine("Pirma reikšmė:{0,3:f2}, antra reikšmė:{1,3:f2}, " +
                    "gautas rezultatas:{2,3:f2}", s, s1, s2);
            }

            else
                if (sim == '*')
            {
                s2 = s * s1;
                Console.WriteLine("Pirma reikšmė:{0,3:f2}, antra reikšmė:{1,3:f2}, " +
                    "gautas rezultatas:{2,3:f2}", s, s1, s2);
            }

            else
                if (s1 > 0 && sim == '/')
            {
                s2 = s / s1;
                Console.WriteLine("Pirma reikšmė:{0,3:f2}, antra reikšmė:{1,3:f2}, " +
                    "gautas rezultatas:{2,3:f2}", s, s1, s2);
            }
            else
                if(sim != '+' && sim != '-' && sim != '*' && sim != '/')
            {
                Console.WriteLine("Pasirinkote zenkla, kuris neegzistuoja");
            }
            else
                Console.WriteLine("Matematine klaida");

        }
    }
}
