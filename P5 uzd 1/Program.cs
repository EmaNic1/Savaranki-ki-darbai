using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace P5_uzd_1
{
    class RaidziuDazniai
    {
        private const int CMax = 500;
        private int[] Rn;
        public string eil { get; set; }
        public char[] ABCM = { 'ą', 'č', 'ę', 'ė', 'į', 'š', 'ų', 'ū', 'ž', 
            'Ą', 'Č', 'Ę', 'Ė', 'Į', 'Š', 'Ų', 'Ū', 'Ž' };//Alfabetas mažosiom raidėm

        /// <summary>
        /// Iraso duomenis
        /// </summary>
        public RaidziuDazniai()
        {
            eil = "";
            Rn = new int[CMax];
            for(int i = 0; i < CMax; i++)
                Rn[i] = 0;
        }

        /// <summary>
        /// Ima simboli
        /// </summary>
        /// <param name="sim">Simbolis</param>
        /// <returns></returns>
        public int Imti(char sim) { return Rn[sim]; }

        /// <summary>
        /// Skaiciuoja raidziu pasikartojimus
        /// </summary>
        public void Kiek()
        {
            for (int i = 0; i < eil.Length; i++)
            {
                if (('a' <= eil[i] && eil[i] <= 'z') ||
                    ('A' <= eil[i] && eil[i] <= 'Z') ||
                    ABCM.Contains(char.ToLower(eil[i])) ||
                    ABCM.Contains(char.ToUpper(eil[i])))
                {
                    Rn[eil[i]]++;
                }
            }

        }
    }

    internal class Program
    {
        const string PD = "Duomenys.txt";
        const string RZ = "Rezultatai.txt";

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            RaidziuDazniai eil = new RaidziuDazniai();
            Daznis(PD, eil);
            Spausdinti(RZ, eil);
            DazniausiaRaide(eil, RZ);
        }

        /// <summary>
        /// Spausdina i faila raidziu daznius dviem eilutem
        /// </summary>
        /// <param name="fv">Failo vardas</param>
        /// <param name="eil">Eilute</param>
        static void Spausdinti(string fv, RaidziuDazniai eil)
        {
            using (var fr = File.CreateText(fv))
            {
                for (char sim = 'a'; sim <= 'ž'; sim++)
                    fr.WriteLine("{0, 3:c} {1, 4:d} |{2, 3:c} {3, 4:d}| {4, 3:c} {5, 4:d} |{6, 3:c} {7, 4:d}",
                    sim, eil.Imti(sim),
                    Char.ToUpper(sim), eil.Imti(Char.ToUpper(sim)));
                fr.WriteLine("");
            }
        }

        static void Daznis(string fv, RaidziuDazniai eil)
        {
            using (StreamReader reader = new StreamReader(fv))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    eil.eil = line;
                    eil.Kiek();
                }
            }
        }

        /// <summary>
        /// Daugiausiai pasikartojanti raide
        /// </summary>
        /// <param name="eil"></param>
        static void DazniausiaRaide(RaidziuDazniai eil, string fv)
        {
            int max = 0;
            char maxraide = 'a';
            for (char sim = 'a'; sim <= 'z'; sim++)
            {
                if (eil.Imti(sim) > max)
                {
                    max = eil.Imti(sim);
                    maxraide = sim;
                }
            }

            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine("Dazniausiai pasikartojanti raide: ");
                fr.WriteLine("{0,3:d} karta/us: {1,2:c} raide", max, maxraide);
            }
        }
    }
}
