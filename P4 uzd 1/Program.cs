using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4_uzd_1
{
    /// <summary>
    /// KLASE SKIRTA KAMBARIO DUOMENIMS SAUGOTI
    /// </summary>
    class Kambarys
    {
        private int butoNumeris;//buto numeris
        private int plotas;//kambario plotas
        private int kambariuSkaicius;//kambariu skaicius
        private int kaina;//kambario kaina
        private long telefonoNumeris;//telefono numeris

        public Kambarys()
        {

        }

        /// <summary>
        /// Duomenu irasymas
        /// </summary>
        /// <param name="butoNumeris"></param>
        /// <param name="plotas"></param>
        /// <param name="kambariuSkaicius"></param>
        /// <param name="kaina"></param>
        /// <param name="telefonoNumeris"></param>
        public void Deti(int butoNumeris, int plotas, int kambariuSkaicius, 
            int kaina, long telefonoNumeris)
        {
            this.butoNumeris = butoNumeris;
            this.plotas = plotas;
            this.kambariuSkaicius = kambariuSkaicius;
            this.kaina = kaina;
            this.telefonoNumeris = telefonoNumeris;
        }

        public int ImtiButa() { return butoNumeris; }

        public int ImtiPlota() { return plotas; }

        public int ImtiKambarius() { return kambariuSkaicius; }

        public int ImtiKaina() { return kaina; }

        public long ImtiTelefona() { return telefonoNumeris; }

        /// <summary>
        /// Ispausdinama duomenu string eilute
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string line;
            line = string.Format("|{0,14:d}|{1,8:d}|{2,19:d}|{3,7:d}|{4,18:d}|",
                butoNumeris, plotas, kambariuSkaicius, kaina, telefonoNumeris);
            return line;
        }
    }

    /// <summary>
    /// KONTEINERINE KLASE BUTAS
    /// </summary>
    class Butas
    {
        const int CMax = 100;
        private Kambarys[] K;
        private int kiek;

        public Butas()
        {
            K = new Kambarys[CMax];
            kiek = 0;
        }

        //grazina butu skaiciu
        public int Imti() { return kiek; }

        //grazina nurodyto indekso buto objekta
        public Kambarys Imti(int i) { return K[i]; }

        //ideda i buto objekta nauja buta ir padidina vienetu
        public void Deti(Kambarys kb) { K[kiek++] = kb; }
    }

    internal class Program
    {
        const int Cn = 100;
        const string PD = "Butas.txt";
        const string RZ = "Rezultatai.txt";

        static void Main(string[] args)
        {
            //ivedami pradiniai duomenys
            Butas B = new Butas();//pradiniu duomenu konteineris
            Skaitymas(ref B, PD);
            if(File.Exists(RZ))
                File.Delete(RZ);
            Spausdinimas(B, RZ, "Kamabriu informacija");

            //ivedami nurodymai
            int kambariuSk;
            int kaina;
            Console.WriteLine("Iveskite norima kambariu skaiciu: ");
            kambariuSk = int.Parse(Console.ReadLine());
            Console.WriteLine("Iveskite norima buto kaina: ");
            kaina = int.Parse(Console.ReadLine());
            using (var fr = File.AppendText(RZ))
            {
                fr.WriteLine("Jusu pasirinktas kambariu skaicius: {0,2:d}", kambariuSk);
                fr.WriteLine("Jusu pasirinkta buto kaina: {0,2:d}", kaina);
                fr.WriteLine("");
            }

            //ivedami kambariai, kurie atitinka reikalavimus
            Butas B1 = new Butas();//tinkamu kambariu konteineris
            TinkamasKambarys(B, ref B1, kambariuSk, kaina);
            if(B1.Imti() > 0)
                Spausdinimas(B1, RZ, "Atrinki kambariai pagal nurodymus");
            else
                using (var fr = File.AppendText(RZ))
                {
                    fr.WriteLine("Kambariu pagal jusu nurodymus nera.");
                }
        }

        /// <summary>
        /// Skaito duomenis is failo
        /// </summary>
        /// <param name="B">buto konteineris</param>
        /// <param name="fv">failo vardas</param>
        static void Skaitymas(ref Butas B, string fv)
        {
            using (StreamReader reader = new StreamReader(fv))
            {
                string line;
                int i = 0;
                while((line = reader.ReadLine()) != null && (i < Cn))
                {
                    string[] parts = line.Split(';');
                    int butoNr = int.Parse(parts[0]);
                    int plotas = int.Parse(parts[1]);
                    int kambariuSk = int.Parse(parts[2]);
                    int kaina = int.Parse(parts[3]);
                    long telefonoNr = long.Parse(parts[4]);
                    Kambarys K = new Kambarys();
                    K.Deti(butoNr, plotas, kambariuSk, kaina, telefonoNr);
                    B.Deti(K);
                }
            }
        }

        /// <summary>
        /// Spausdina duomenis is konteinerio i faila
        /// </summary>
        /// <param name="B">buto konteineris</param>
        /// <param name="fv">failo vardas</param>
        static void Spausdinimas(Butas B, string fv, string kom)
        {
            using(var fr = File.AppendText(fv))
            {
                fr.WriteLine(kom);
                fr.WriteLine("|-------------------------------------------" +
                    "---------------------------|");
                fr.WriteLine("| Buto Numeris | Plotas | Kambariu Skaicius " +
                    "| Kaina | Telefono Numeris |");
                fr.WriteLine("|-------------------------------------------" +
                    "---------------------------|");
                for (int i = 0; i < B.Imti(); i++)
                {
                    fr.WriteLine("{0:d}", B.Imti(i).ToString());
                }
                fr.WriteLine("|-------------------------------------------" +
                    "---------------------------|");
                fr.WriteLine("");
            }
        }

        /// <summary>
        /// Randamas tinkamas kambarys
        /// </summary>
        /// <param name="B">buto konteineris</param>
        /// <param name="B1">tinkamo buto konteineris</param>
        /// <param name="kambarioSk">kambariu skaicius</param>
        /// <param name="kaina">buto kaina</param>
        static void TinkamasKambarys(Butas B, ref Butas B1, int kambarioSk, int kaina)
        {
            for(int i = 0; i < B.Imti(); i++)
            {
                if (kambarioSk == B.Imti(i).ImtiKambarius() && kaina >= B.Imti(i).ImtiKaina())
                    B1.Deti(B.Imti(i));
            }
        }
    }
}
