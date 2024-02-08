using System;
using System.IO;

namespace P3_uzd_2
{
    //VALIUTOS DUOMENIMS SAUGOTI
    class Valiuta
    {
        private string valiutosPavadinimas;
        private int doleriai;
        private int centai;
        private double kursas;

        public Valiuta(string valiutosPavadinimas, int valiuta, int centai, double kursas)
        {
            this.valiutosPavadinimas = valiutosPavadinimas;
            this.doleriai = valiuta;
            this.centai = centai;
            this.kursas = kursas;
        }

        //grazina valiutos pavadinima
        public string ImtiValPav() { return valiutosPavadinimas; }

        //grazina valiuta
        public int ImtiValiuta() { return doleriai; }

        //grazina centus
        public int ImtiCentus() { return centai; }

        //grazina kursa
        public double ImtiKursa() { return kursas; }
    }

    internal class Program
    {

        const int Cn = 100;
        const string PDA = "A.txt";
        const string PDB = "B.txt";

        static void Main(string[] args)
        {
            Valiuta[] V1 = new Valiuta[Cn];
            int n1;
            string vardas1;

            Valiuta[] V2 = new Valiuta[Cn];
            int n2;
            string vardas2;

            Skaityti(V1, PDA, out n1, out vardas1);
            Skaityti(V2, PDB, out n2, out vardas2);

            Console.WriteLine(vardas1);
            Console.WriteLine("| Valiutos Pav        |  Valiuta |  Centai  |  Kursas  ");
            Console.WriteLine("-------------------------------------------------------");
            Spausdinit(V1, n1);

            Console.WriteLine("");
            Console.WriteLine(vardas2);
            Console.WriteLine("| Valiutos Pav        |  Valiuta |  Centai  |  Kursas  ");
            Console.WriteLine("-------------------------------------------------------");
            Spausdinit(V2, n2);

            Console.WriteLine("");
            int pinigaiE;
            double centaiE;
            AnuproBarborosPinigai(V1, n1, out pinigaiE, out centaiE);
            int pinigaiA = pinigaiE;
            double centaiA = centaiE;
            Console.WriteLine("Anupro pinigai: {0,3:d} euru ir {1,3:f2} centu", pinigaiE, centaiE);
            Console.WriteLine("");
            AnuproBarborosPinigai(V2, n2, out pinigaiE, out centaiE);
            int pinigaiB = pinigaiE;
            double centaiB = centaiE;
            Console.WriteLine("Barboros pinigai: {0,3:d} euru ir {1,3:f2} centu", pinigaiE, centaiE);
            Console.WriteLine("");
            Console.WriteLine("Bendrai turi: {0,3:d} euru ir {1,3:f2} centu",
                (pinigaiA + pinigaiB), (centaiA + centaiB));
        }

        /// <summary>
        /// Skaito duomenis is failo
        /// </summary>
        /// <param name="V">objektas</param>
        /// <param name="fv">failo vardas</param>
        /// <param name="n">valiutu turimas kiekis</param>
        static void Skaityti(Valiuta[] V, string fv, out int n, out string vardas)
        {
            using (StreamReader reader = new StreamReader(fv))
            {
                string line;
                int i = 0;
                line = reader.ReadLine();
                vardas = line;
                while ((line = reader.ReadLine()) != null && (i < Cn))
                {
                    string[] parts = line.Split(';');
                    string valPavad = parts[0];
                    int valiuta = int.Parse(parts[1]);
                    int centai = int.Parse(parts[2]);
                    double kursas = double.Parse(parts[3]);
                    V[i++] = new Valiuta(valPavad, valiuta, centai, kursas);
                }
                n = i;
            }
        }

        /// <summary>
        /// Spausdina duomenis i console
        /// </summary>
        /// <param name="V">valiutos objektas</param>
        /// <param name="n">valiutos kiekis</param>
        static void Spausdinit(Valiuta[] V, int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("|{0,-15}      |       {1,3:d}|       {2,3:d}|       {3,3:f2}",
                    V[i].ImtiValPav(), V[i].ImtiValiuta(), V[i].ImtiCentus(), V[i].ImtiKursa());
            }
        }

        /// <summary>
        /// Skaiciuoja kiek turi Anupras ir Barbora pinigu
        /// </summary>
        /// <param name="V">valiutos objektas</param>
        /// <param name="n">valiutos kiekis</param>
        /// <param name="pinigaiA">kiek is viso</param>
        static void AnuproBarborosPinigai(Valiuta[] V, int n, out int valiutaA, out double centaiA)
        {
            int pinigai = 0;
            double centai = 0;
            for (int i = 0; i < n; i++)
            {
                pinigai = V[i].ImtiValiuta() * V[i].ImtiKursa();
                centai = (V[i].ImtiCentus() * 0.01) * V[i].ImtiKursa();
            }
            valiutaA = pinigai;
            centaiA = centai;
        }
    }
}
