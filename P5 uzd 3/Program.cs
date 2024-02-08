using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5_uzd_3
{
    internal class Program
    {
        const string PD = "Duomenys.txt";
        const string RZ = "Rezultatai.txt";
        const string AN = "Analize.txt";

        static void Main(string[] args)
        {
            string skyrikliai = " .,!?:;()\t'";
            Apdoroti(PD, RZ, AN, skyrikliai);
        }

        /// <summary>
        /// Skaito, analizuoja ir iraso i skirtingus failus
        /// </summary>
        /// <param name="fvd">Duomenu failas</param>
        /// <param name="fvr">Rezultatu failas</param>
        /// <param name="fva">Analizes failas</param>
        static void Apdoroti(string fvd, string fvr, string fva, string skyrikliai)
        {
            string[] lines = File.ReadAllLines(fvd, Encoding.UTF8);
            using(var fr = File.CreateText(fvr))
            {
                using(var fa = File.CreateText(fva))
                {
                    foreach(string line in lines)
                    {
                        if (line.Length > 0)
                        {
                            string nauja = line;
                            if (BeKomentaru(line, out nauja, skyrikliai))
                                fa.WriteLine(line);
                            if (nauja.Length > 0)
                                fr.WriteLine(nauja);
                        }
                        else
                            fr.WriteLine(line);
                    }
                }
            }
        }

        /// <summary>
        /// Pasalina is eilutes komentarus ir grazina pozimi, ar salino
        /// </summary>
        /// <param name="line">eilute su komentarais</param>
        /// <param name="nauja">eilute be komentaru</param>
        /// <returns></returns>
        static bool BeKomentaru(string line, out string nauja, string skyrikliai)
        {
            nauja = line;
            for(int i = 0; i < line.Length; i++)
            {
                if (line != skyrikliai && line[i] == '/' && line[i + 1] == '/' ||
                    line[i] == '/' && line[i + 1] == '*')
                {
                    nauja = line.Remove(i);
                    return true;
                }
            }
            return false;
        }
    }
}
