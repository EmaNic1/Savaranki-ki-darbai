using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_uzd_1
{
    //KLASE SKIRTA TURISTO PINIGAMS SAUGOTI
    class Turistas
    {
        private int eurai;//turimi turisto eurai
        private int centai;//turimi turisto centai

        public Turistas(int eurai, int centai)
        {
            this.eurai = eurai;
            this.centai = centai;
        }

        //grazina eurus
        public int ImtiEurus() { return eurai; }

        //grazina centus
        public int ImtiCentus() { return centai; }
    }

    internal class Program
    {
        const int Cn = 100;
        const string PD = "Duomenys.txt";

        static void Main(string[] args)
        {
            Turistas[] T = new Turistas[Cn];//turistu duomenys - objektas
            int n;//turistu skaicius
            Skaityti(T, PD, out n);
            Console.WriteLine("Turistu turimi pinigai:");
            Console.WriteLine("| Eurai |  Centai |");
            Console.WriteLine("-------------------");
            Spausdinti(T, n);
            Pinigai(T, n, out int kiekEuru, out int kiekCentu,
                out double kiekvienamPinigu, out double kiekPinigu, out double bendrosIslaidos);
            Console.WriteLine("Kiekvienam turistui tenka {0,3:f2} eurai vidutiniskai\n",
                kiekvienamPinigu);
            Console.WriteLine("Is viso turistai turi: {0,3:f2} eurus", kiekPinigu);
            Console.WriteLine("Bendros islaidos: {0,3:f2}", bendrosIslaidos);
            Console.WriteLine("");
        }

        /// <summary>
        /// Skaito duomenis is failo
        /// </summary>
        /// <param name="T">objektas</param>
        /// <param name="fv">failo pavadinimas</param>
        /// <param name="kiek">turistu skaicius</param>
        static void Skaityti(Turistas[] T, string fv, out int kiek)
        {
            int eurai;
            int centai;
            using (StreamReader reader = new StreamReader(fv))
            {
                string line;
                int i = 0;
                line = reader.ReadLine();
                while((line = reader.ReadLine()) != null && (i < Cn))
                {
                    string[] parts;
                    parts = line.Split(';');
                    eurai = int.Parse(parts[0]);
                    centai = int.Parse(parts[1]);
                    T[i++] = new Turistas(eurai, centai);
                }
                kiek = i;
            }
        }

        /// <summary>
        /// Spausdina duomenis i konsole
        /// </summary>
        /// <param name="T"></param>
        /// <param name="kiek"></param>
        static void Spausdinti(Turistas[] T, int kiek)
        {
            for(int i = 0; i < kiek; i++)
                Console.WriteLine("|  {0,2:d}   | {1,7:d} |",
                    T[i].ImtiEurus(), T[i].ImtiCentus());
            Console.WriteLine("");
        }

        /// <summary>
        /// Suskaiciuoja pinigus
        /// </summary>
        /// <param name="T">objektas</param>
        /// <param name="kiek">kiek turistu</param>
        /// <param name="kiekEuru">kiek turi euru</param>
        /// <param name="kiekCentu">kiek turi centu</param>
        /// <param name="kiekvienamPinigu">kiek kiekvienam pinigu</param>
        /// <param name="kiekPinigu">kiek is viso</param>
        static void Pinigai(Turistas[] T, int kiek, out int kiekEuru, out int kiekCentu,
            out double kiekvienamPinigu, out double kiekPinigu, out double turistoPinigai)
        {
            kiekEuru = 0;
            kiekCentu = 0;
            kiekPinigu = 0.0;
            kiekvienamPinigu = 0.0;
            turistoPinigai = 0.0;
            for(int i = 0; i < kiek; i++)
            {
                kiekEuru = kiekEuru + T[i].ImtiEurus();
                kiekCentu = kiekCentu + T[i].ImtiCentus();
                kiekPinigu = (kiekEuru + (kiekCentu * 0.01));
                kiekvienamPinigu = kiekPinigu / kiek;
                turistoPinigai = (T[i].ImtiEurus() + (T[i].ImtiCentus() * 0.01)) / 0.25;
            }
        }
    }
}
