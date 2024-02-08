using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5_uzd_2
{
    internal class Program
    {
        const string PD = "Duomenys.txt";
        const string RZ = "Rezultatai.txt";

        static void Main(string[] args)
        {
            int nr = 0;
            string skyrikliai = " .,!?:;()\t'";
            Skaitymas(PD, out nr, skyrikliai);
            Spausdinti(PD, RZ, nr, skyrikliai);
            Console.WriteLine("Tuscios eilutes nr: {0,2:d}", nr + 1);
        }

        /// <summary>
        /// Skaito duomenis
        /// </summary>
        /// <param name="fv">Failo vardas</param>
        /// <param name="nr">Tuscios eilutes numeris</param>
        static void Skaitymas(string fv, out int nr, string skyrikliai)
        {
            string[] lines = File.ReadAllLines(fv, 
                Encoding.UTF8);
            int tuscia = 0;
            nr = 0;
            int nreil = 0;
            foreach(string line in lines)
            {
                if(line != skyrikliai && line.Length == tuscia)
                {
                    tuscia = line.Length;
                    nr = nreil;
                }
                nreil++;
            }
        }

        /// <summary>
        /// Spausdina rezultatus be tusciu eiluciu
        /// </summary>
        /// <param name="fv">Duomenu failas</param>
        /// <param name="fvb">Rezultatu failas</param>
        /// <param name="nr">Tuscios eilutes nr</param>
        static void Spausdinti(string fv, string fvb, int nr, string skyrikliai)
        {
            string[] lines = File.ReadAllLines(fv, 
                Encoding.UTF8);
            int nreil = 0;
            using(var fr = File.CreateText(fvb))
            {
                foreach(string line in lines)
                {
                    if(nr != nreil)
                    {
                        fr.WriteLine(line);
                    }
                    nreil++;
                }
            }
        }
    }
}
