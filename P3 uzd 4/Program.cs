using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_uzd_4
{
    class Keliai
    {
        private string pav;//kelio pavadinimas
        private int ilgis;//kelio ilgis
        private int greitis;//leistinas greitis

        public Keliai(string pav, int ilgis, int greitis)
        {
            this.pav = pav;
            this.ilgis = ilgis;
            this.greitis = greitis;
        }

        //grazina pavadinima
        public string ImtiPav() { return pav; }

        //grazina kelio ilgi
        public int ImtiIlgi() { return ilgis; }

        //grazina leistina greiti
        public int ImtiGreiti() { return greitis; }
    }

    internal class Program
    {
        const int Cn = 100;
        const string PD = "Duomenys.txt";
        const string RZ = "Rezultatai.txt";

        static void Main(string[] args)
        {
            Keliai[] K = new Keliai[Cn];
            int n;//keliu kiekis
            if(File.Exists(RZ))
                File.Delete(RZ);
            Skaityti(K, PD, out n);
            Spausdinti(K, RZ, n);
        }

        /// <summary>
        /// Skaito duomenis is failo
        /// </summary>
        /// <param name="K">Keliu duomenys</param>
        /// <param name="fv">failo vardas</param>
        /// <param name="kiek">keliu kiekis</param>
        static void Skaityti(Keliai[] K, string fv, out int kiek)
        {
            using (StreamReader reader = new StreamReader(fv))
            {
                string pav;
                int ilgis;
                int greitis;
                string line;
                line = reader.ReadLine();
                string[] parts;
                kiek = int.Parse(line);
                for (int i = 0; i < kiek; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(';');
                    pav = parts[0];
                    ilgis = int.Parse(parts[1]);
                    greitis = int.Parse(parts[2]);
                    K[i] = new Keliai(pav, ilgis, greitis);
                }
            }
        }

        /// <summary>
        /// Spausdina rezultatus is failo i faila
        /// </summary>
        /// <param name="K">keliu duomenys</param>
        /// <param name="fv">failo vardas</param>
        /// <param name="kiek">keliu kiekis</param>
        static void Spausdinti(Keliai[] K, string fv, int kiek)
        {
            const string top =
                "|-------------------|-------------|-------------------|\r\n"
              + "| Pavadinimas       | Kelio       |Leistinas greitis  | \r\n"
              + "|                   | ilgis       |(km/h)             | \r\n"
              + "|-------------------|-------------|-------------------|";

            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine("Keliu kiekis {0}", kiek);
                fr.WriteLine("Keliu sarasas:");
                fr.WriteLine(top);
                Keliai tarp;
                for (int i = 0; i < kiek; i++)
                {
                    tarp = K[i];
                    fr.WriteLine("|  {0,-15}  |  {1,-9}  |{2,19:d}|",
                        tarp.ImtiPav(), tarp.ImtiIlgi(), tarp.ImtiGreiti());
                }
                fr.WriteLine("-------------------------------" +
                    "------------------------");
                fr.WriteLine("");
            }
        }
    }
}
