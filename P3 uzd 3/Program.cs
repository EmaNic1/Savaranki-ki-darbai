using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Du_objektu_rinkiniai
{
    class Dviratis
    {
        private int metai;//pagaminimo metai
        private double kaina;//pinigine verte
        private int kiek;
        private string pav;

        public Dviratis(string pav, int kiek, int metai, double kaina)
        {
            this.metai = metai;
            this.kaina = kaina;
            this.kiek = kiek;
            this.pav = pav;
        }



        //grazina metus
        public int ImtiMetus() { return metai; }

        //grazina kaina
        public double ImtiKaina() { return kaina; }

        //grazina kieki
        public int ImtiKieki() { return kiek; }

        //grazina pavadinima
        public string ImtiPav() { return pav; }

        public void PapilditiKieki(int k) { kiek = kiek + k; }
    }

    internal class Program
    {
        const int Cn = 100;
        const string PD = "Duomenys1.txt";
        const string PD1 = "Duomenys2.txt";
        const string RZ = "Rezultatai.txt";

        static void Main(string[] args)
        {
            //Pirmas duomenu failas
            Dviratis[] D1 = new Dviratis[Cn];
            int n1;
            string pav1;
            Skaityti(D1, PD, out n1, out pav1);

            //Antras duomenu failas
            Dviratis[] D2 = new Dviratis[Cn];
            int n2;
            string pav2;
            Skaityti(D2, PD1, out n2, out pav2);

            if (File.Exists(RZ))
                File.Delete(RZ);
            Spausdinti(D1, RZ, n1, pav1);
            Spausdinti(D2, RZ, n2, pav2);

            using (var fr = File.AppendText(RZ))
            {
                if (D1[Seniausias(D1, n1)].ImtiMetus() < D2[Seniausias(D2, n2)].ImtiMetus())
                    fr.WriteLine("Seniausias dviratis nuomos punkte {0}", pav1);
                else
                    fr.WriteLine("Seniausias dviratis nuomos punkte {0}", pav2);
            }

            Dviratis[] Dr = new Dviratis[Cn];
            int nr = 0;
            Formuoti(D1, n1, Dr, ref nr);
            Formuoti(D2, n2, Dr, ref nr);
            Spausdinti(Dr, RZ, nr, "Modeliu sarasas");

            //veiksmai, kurie randa brangiausia dvirati
            using (var fr = File.AppendText(RZ))
            {
                fr.WriteLine("Brangiausias dviratis pirmame punkte kainuoja {0,3:f2} euru", BrangiausiasDviratis(D1, n1));
                fr.WriteLine("Brangiausias dviratis antrame punkte kainuoja {0,3:f2} euru", BrangiausiasDviratis(D2, n2));
                fr.WriteLine("");

                if (BrangiausiasDviratis(D1, n1) > BrangiausiasDviratis(D2, n2))
                    fr.WriteLine("Brangiausias dviratis yra pirmame nuomos punkte, jo kaina {0,3:f2} euru",
                        BrangiausiasDviratis(D1, n1));
                else
                if (BrangiausiasDviratis(D1, n1) < BrangiausiasDviratis(D2, n2))
                    fr.WriteLine("Brangiausias dviratis yra antrame nuomos punke, jo kaina {0,3:f2} euru",
                            BrangiausiasDviratis(D2, n2));
                else
                    if (BrangiausiasDviratis(D1, n1) == BrangiausiasDviratis(D2, n2))
                    fr.WriteLine("Brangiausias dviratis yra abiejuose nuomos punktuose, ju kaina {0,3:f2}",
                        BrangiausiasDviratis(D1, n1));
            }
        }

        /// <summary>
        /// Skaito duomenis is failo
        /// </summary>
        /// <param name="fv">failo vardas</param>
        /// <param name="D">objektu rinkinys Dviratis</param>
        /// <param name="n">dviraciu skaicius</param>
        static void Skaityti(Dviratis[] D, string fv, out int n, out string pav)
        {
            using (StreamReader reader = new StreamReader(fv))
            {
                string eil;
                int kiekn;
                int metain;
                double kainan;
                string line;
                line = reader.ReadLine();
                string[] parts;
                pav = line;
                line = reader.ReadLine();
                n = int.Parse(line);
                for (int i = 0; i < n; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(';');
                    eil = parts[0];
                    kiekn = int.Parse(parts[1]);
                    metain = int.Parse(parts[2]);
                    kainan = double.Parse(parts[3]);
                    D[i] = new Dviratis(eil, kiekn, metain, kainan);
                }
            }
        }

        /// <summary>
        /// Spausdina duomenis is failo i faila
        /// </summary>
        /// <param name="D"></param>
        /// <param name="fv"></param>
        /// <param name="kiek"></param>
        /// <param name="pav"></param>
        static void Spausdinti(Dviratis[] D, string fv, int kiek, string pav)
        {
            const string top =
                "|-----------------|------------|-----------------|------------|\r\n"
              + "| Pavadinimas     | Kiekis     | Pagaminimo      | Kaina      | \r\n"
              + "|                 |            | metai           | (eurų)     | \r\n"
              + "|-----------------|------------|-----------------|------------|";

            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine("Nuomos firma: {0}", pav);
                fr.WriteLine(top);
                Dviratis tarp;
                for (int i = 0; i < kiek; i++)
                {
                    tarp = D[i];
                    fr.WriteLine("| {0,-15} |  {1,8}  |   {2,5:d}         |   {3,7:f2}  |",
                        tarp.ImtiPav(), tarp.ImtiKieki(), tarp.ImtiMetus(),
                        tarp.ImtiKaina());
                }
                fr.WriteLine("--------------------------------------------------------------");
                fr.WriteLine("");
            }
        }

        /// <summary>
        /// Grazina seniausia dvirati (indeksa)
        /// </summary>
        /// <param name="D"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        static int Seniausias(Dviratis[] D, int n)
        {
            int k = 0;
            for (int i = 0; i < n; i++)
            {
                if (D[i].ImtiMetus() < D[i].ImtiMetus())
                    k = i;
            }
            return k;
        }

        /// <summary>
        /// Suranda dviracio indeksa
        /// </summary>
        /// <param name="D"></param>
        /// <param name="n"></param>
        /// <param name="pav"></param>
        /// <param name="metai"></param>
        /// <returns></returns>
        static int YraModelis(Dviratis[] D, int n, string pav, int metai)
        {
            for (int i = 0; i < n; i++)
            {
                if (D[i].ImtiPav() == pav &&
                    D[i].ImtiMetus() == metai)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// suformuoja nauja saras
        /// </summary>
        /// <param name="D"></param>
        /// <param name="n"></param>
        /// <param name="Dr"></param>
        /// <param name="nr"></param>
        static void Formuoti(Dviratis[] D, int n, Dviratis[] Dr, ref int nr)
        {
            int k;
            for (int i = 0; i < n; i++)
            {
                k = YraModelis(Dr, nr, D[i].ImtiPav(), D[i].ImtiMetus());
                if (k >= 0)
                    Dr[k].PapilditiKieki(D[i].ImtiKieki());//didinamas kiekis
                else
                {
                    Dr[nr] = D[i];//papildomas rinkinys
                    nr++;
                }
            }
        }

        /// <summary>
        /// suranda brangiausia dvirati
        /// </summary>
        /// <param name="D"></param>
        /// <param name="n"></param>
        static double BrangiausiasDviratis(Dviratis[] D, int kiek)
        {
            double maxInd = 0.0;
            double maxSkaic = D[0].ImtiKaina();
            for (int i = 1; i < kiek; i++)
                if (D[i].ImtiKaina() > maxSkaic)
                {
                    maxInd = i;
                    maxSkaic = D[i].ImtiKaina();
                }
            return maxSkaic;
        }
    }
}
