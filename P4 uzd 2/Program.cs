using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4_uzd_2
{
    //DUOMENU KLASES
    /// <summary>
    /// KLASE STUDENTO DUOMENIMS SAUGOTI
    /// </summary>
    class Studentas
    {
        private string pavarde;//studento pavarde
        private string vardas;//studento vardas
        private string grupe;//studento grupe
        private int pazymiuKiekis;//turimi studento pazymiai
        private ArrayList pazymiai;//pazymiu eilute

        public Studentas()
        {
            pavarde = "";
            vardas = "";
            grupe = "";
            pazymiuKiekis = 0;
            pazymiai = new ArrayList();
        }

        /// <summary>
        /// Duomenu irasymas
        /// </summary>
        /// <param name="pavarde"></param>
        /// <param name="vardas"></param>
        /// <param name="grupe"></param>
        /// <param name="pazymiuKiekis"></param>
        /// <param name="pazymiai"></param>
        public void Deti(string pavarde, string vardas, string grupe,
            int pazymiuKiekis, ArrayList pazymiai)
        {
            this.pavarde = pavarde;
            this.vardas = vardas;
            this.grupe = grupe;
            this.pazymiuKiekis = pazymiuKiekis;
            foreach(int sk in pazymiai)
                this.pazymiai.Add(sk);
        }

        public string ImtiPavarde() { return pavarde; }

        public string ImtiVarda() { return vardas; }

        public string ImtiGrupe() { return grupe; }

        public int ImtiPazymiuKieki() { return pazymiuKiekis; }

        public ArrayList ImtiPazymius() { return pazymiai; }

        public override string ToString()
        {
            string line;
            line = string.Format("|{0,-9}|{1,-8}|{2,-8}|{3,8:d}|",
                pavarde, vardas, grupe, pazymiuKiekis);
            foreach (int sk in pazymiai)
                line = line + string.Format(" {0,2:d}", sk);
            return line;
        }
    }

    /// <summary>
    /// KLASE GRUPES DUOMENIMS SAUGOTI
    /// </summary>
    class Grupe
    {
        private string grupe;
        private double pazymiuVidurkis;

        public Grupe()
        {
            grupe = "";
            pazymiuVidurkis = 0;
        }

        public string ImtiGrupe() { return grupe; }

        public double ImtiVidurki() { return pazymiuVidurkis; }

        /// <summary>
        /// Duomenu irasymas
        /// </summary>
        /// <param name="grupe"></param>
        /// <param name="vidurkis"></param>
        public void Deti(string grupe, double vidurkis)
        {
            this.grupe = grupe;
            pazymiuVidurkis = vidurkis;
        }

        public override string ToString()
        {
            string line;
            line = string.Format("{0} {1,4:f2}", grupe, pazymiuVidurkis);
            return line;
        }

        //uzkloti operatoriai
        public static bool operator >=(Grupe st1, Grupe st2)
        {
            int poz = String.Compare(st1.grupe, st2.grupe,
                StringComparison.CurrentCulture);
            if((st1.pazymiuVidurkis > st2.pazymiuVidurkis) || 
                (st1.pazymiuVidurkis == st2.pazymiuVidurkis) && (poz > 0))
                return true;
            else
                return false;
        }

        public static bool operator <=(Grupe st1, Grupe st2)
        {
            int poz = String.Compare(st1.grupe, st2.grupe,
                StringComparison.CurrentCulture);
            if ((st1.pazymiuVidurkis > st2.pazymiuVidurkis) ||
                (st1.pazymiuVidurkis == st2.pazymiuVidurkis) && (poz > 0))
                return true;
            else
                return false;
        }
    }

    //KONTEINERINES KLASES
    /// <summary>
    /// KONTEINERINE KLASE STUDENTO DUOMENIMS SAUGOTI
    /// </summary>
    class Fakultetas
    {
        const int CMax = 100;
        private Studentas[] S;
        private int kiek;

        public Fakultetas()
        {
            S = new Studentas[CMax];
            kiek = 0;
        }

        public int Imti() { return kiek; }

        public Studentas Imti(int i) { return S[i]; }

        public void Deti(Studentas st) { S[kiek++] = st; }
    }

    /// <summary>
    /// KONTEINERINE KLASE GRUPES DUOMENIMS SAUGOTI
    /// </summary>
    class GrupesMasyvas
    {
        const int CMax = 100;
        private Grupe[] G;
        private int kiek;

        public GrupesMasyvas()
        {
            G = new Grupe[CMax];
            kiek = 0;
        }

        public int Imti() { return kiek; }

        public Grupe Imti(int i) { return G[i]; }

        public void Deti(Grupe gr) { G[kiek++] = gr; }

        /// <summary>
        /// Rykiavimas pagal nurodymus
        /// </summary>
        public void Rikiuoti()
        {
            for(int i = 0; i < kiek - 1; i++)
            {
                Grupe min = G[i];
                int im = i;
                for(int j = i + 1; j < kiek; j++)
                    if (G[j] <= min)
                    {
                        min = G[j];
                        im = j;
                    }
                G[im] = G[i];
                G[i] = min;
            }
        }
    }

    internal class Program
    {
        const int Cn = 100;
        const string PD = "Fakultetas.txt";
        const string RZ = "Rezultatai.txt";

        static void Main(string[] args)
        {
            Fakultetas F = new Fakultetas();//konteinerine studentu klase
            string fakultetas;//fakulteto pavadinimas
            GrupesMasyvas GM = new GrupesMasyvas();//konteinerine grupes klase
            if (File.Exists(RZ))
                File.Delete(RZ);

            //skaito ir spausdina pradinius duomenis
            Skaityti(ref F, PD, out fakultetas);
            Spausdinti(F, RZ, fakultetas);

            //ivedamas ir spausdinamas vidurkis
            Vidurkis(F, GM);
            GM.Rikiuoti();
            SpausdintiVidurki(GM, RZ, "Studentu pazymiu vidurkiai");  
        }

        /// <summary>
        /// Skaito duomenis is failo
        /// </summary>
        /// <param name="F">studentu klase</param>
        /// <param name="fv">failo vardas</param>
        static void Skaityti(ref Fakultetas F, string fv, out string fVardas)
        {
            string vardas, pavarde, grupe;
            int pazimiuKiekis;
            ArrayList pazymiai = new ArrayList();
            using (StreamReader reader = new StreamReader(fv))
            {
                string line;
                int i = 0;
                line = reader.ReadLine();
                fVardas = line;
                while ((line = reader.ReadLine()) != null && (i < Cn))
                {
                    string[] parts = line.Split(';');
                    pavarde = parts[0];
                    vardas = parts[1];
                    grupe = parts[2];
                    pazimiuKiekis = int.Parse(parts[3]);
                    //skaito turimus pazymius
                    string[] eil = parts[4].Trim().Split(new[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries);
                    pazymiai.Clear();
                    foreach(string eilute in eil)
                    {
                        int paz = int.Parse(eilute);
                        pazymiai.Add(paz);
                    }

                    Studentas st = new Studentas();
                    st.Deti(pavarde, vardas, grupe, pazimiuKiekis, pazymiai);
                    F.Deti(st);                        
                }
            }

        }

        /// <summary>
        /// Spausidina duomenis i faila
        /// </summary>
        /// <param name="F">studentu klase</param>
        /// <param name="fv">failo vardas</param>
        /// <param name="kom">lenetels pavadinimas</param>
        static void Spausdinti(Fakultetas F, string fv, string kom)
        {
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine(kom);
                fr.WriteLine("|------------------------------------------------------------|");
                fr.WriteLine("| Pavarde | Vardas | Grupe  | Kiekis |        Pazymiai       |");
                fr.WriteLine("|------------------------------------------------------------|");
                for (int i = 0; i < F.Imti(); i++)
                {
                    fr.WriteLine("{0}", F.Imti(i).ToString());
                }
                fr.WriteLine("|------------------------------------------------------------|");
                fr.WriteLine("");
            }
        }

        /// <summary>
        /// Pazymiu vidurkio skaiciavimas
        /// </summary>
        /// <param name="F">studentu konteineris</param>
        /// <param name="GM">grupes konteineris</param>
        static void Vidurkis(Fakultetas F, GrupesMasyvas GM)
        {
            double vidurkis;
            double suma = 0;
            string pav;
            for(int i = 0; i < F.Imti(); i++)
            {
                foreach (int skaicius in F.Imti(i).ImtiPazymius())
                    suma += skaicius;
                vidurkis = suma / F.Imti(i).ImtiPazymiuKieki();
                suma = 0;
                pav = F.Imti(i).ImtiGrupe();
                Grupe gr = new Grupe();
                gr.Deti(pav, vidurkis);
                GM.Deti(gr);
            }
        }

        /// <summary>
        /// Spausdina pazymiu vidurkius
        /// </summary>
        /// <param name="GM"></param>
        /// <param name="fv"></param>
        /// <param name="kom"></param>
        static void SpausdintiVidurki(GrupesMasyvas GM, string fv, string kom)
        {
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine(kom);
                for(int i = 0; i < GM.Imti(); i++)
                {
                    fr.WriteLine("{0}", GM.Imti(i).ToString());
                }
            }
        }
    }
}
