using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5_uzd_4
{
    internal class Program
    {
        const string PD = "Duomenys.txt";

        static void Main(string[] args)
        {
            char[] skyrikliai = { ' ', '.', ',', '!', '?', 
                ':', ';', '(', ')', '\t' };
            Console.WriteLine("Sutampanciu zodziu {0,3:d}",
                Apdoroti(PD, skyrikliai));
        }

        /// <summary>
        /// Spausdina zodzius
        /// </summary>
        /// <param name="fv">failo vardas</param>
        /// <param name="skyrikliai">skyrikliai</param>
        /// <returns></returns>
        static int Apdoroti(string fv, char[] skyrikliai)
        {
            string[] lines = File.ReadAllLines(fv);
            int sutampa = 0;
            foreach (string line in lines)
                if (line.Length > 0)
                    sutampa += Zodziai(line, skyrikliai);
            return sutampa;
        }

        /// <summary>
        /// Patikrina ar apsuktas zodis sutampa su normaliu zodziu
        /// </summary>
        /// <param name="eilute">eilute</param>
        /// <param name="skyrikliai">Skyrikliai</param>
        /// <returns></returns>
        static int Zodziai(string eilute, char[] skyrikliai)
        {
            string[] parts = eilute.Split(skyrikliai, 
                StringSplitOptions.RemoveEmptyEntries);
            int sutampa = 0;
            for(int i = 0; i < parts.Length; i++)
            {
                if (Apsukti(parts[i]).Equals(parts[i]))
                {
                    sutampa++;
                    Console.WriteLine("{0}", Apsukti(parts[i]));    
                }
            }
            return sutampa;
        }

        /// <summary>
        /// Apsuka zodi
        /// </summary>
        /// <param name="tekstas">Tekstas</param>
        /// <returns></returns>
        static string Apsukti(string tekstas)
        {
            char[] cArray = tekstas.ToCharArray();
            string apsukti = String.Empty;
            for(int i = cArray.Length - 1; i > -1; i--)
                apsukti += cArray[i];
            return apsukti;
        }
    }
}
