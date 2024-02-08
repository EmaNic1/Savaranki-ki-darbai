using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savarankiskos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char simbolis;///koks simbolis
            int kiekis,///simbolių kiekis
                kiekeil,///kiek simbloių eilutėje
                psimboliu = 0,///kiek simbolių parašyta
                kartai;

            Console.Write("Įveskite norimą simbolį:");
            simbolis = (char)Console.Read();
            Console.ReadLine();

            Console.Write("Įveskite kiek norite, kad būtų išspausdinta simbolių:");
            kiekis = int.Parse(Console.ReadLine());

            Console.Write("Įveskite spausdinamą simbolių kiekį eilutėje:");
            kiekeil = int.Parse(Console.ReadLine());
            Console.Clear();///sakinys rašomas kai norima išvalyti langą

            kartai = kiekis / kiekeil;

            for (int i = 0; i < kartai; i++)
            {
                for (int j = 0; j < kiekeil; j++)
                {
                    Console.Write(simbolis);
                    psimboliu++;
                }
                Console.WriteLine("");
            }
            while (kiekis != psimboliu)
            {
                Console.Write(simbolis);
                psimboliu++;
            }
            Console.Write("");
        }
    }
}
