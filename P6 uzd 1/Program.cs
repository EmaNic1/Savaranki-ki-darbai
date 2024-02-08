using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P6_uzd_1
{
    class Matrica
    {
        const int CMaxEil = 10;//didziausias galimas eiluciu(kasu) skaicius 
        const int CMaxSt = 100;//didziausias galimas stulpeliu(pirkeju) skacius
        private int[,] A;//duomenu matrica
        public int n { get; set; }//eiluciu skaicius
        public int m { get; set; }//stlupeliu skaicius

        public Matrica()
        {
            n = 0;
            m = 0;
            A = new int[CMaxEil, CMaxSt];
        }

        /// <summary>
        /// Priskiria klases matricos kintamaja reiksme
        /// </summary>
        /// <param name="i">Eilutes indeksas</param>
        /// <param name="j">Stulpelio indeksas</param>
        /// <param name="pirk">Pirkeju skaicius</param>
        public void Deti(int i, int j, int pirk)
        {
            A[i, j] = pirk;
        }

        /// <summary>
        /// Grazina pirkeju kieki
        /// </summary>
        /// <param name="i">Eilutes indeksas</param>
        /// <param name="j">Stulpelio indeksas</param>
        /// <returns></returns>
        public int ImtiReiksme(int i, int j)
        {
            return A[i, j];
        }
    }

    internal class Program
    {
        const string PD = "Duomenys.txt";
        const string RZ = "Rezultatai.txt";
        static void Main(string[] args)
        {
            Matrica prekybosBaze = new Matrica();
            Skaityti(PD, ref prekybosBaze);
            if (File.Exists(RZ))
                File.Delete(RZ);
            Spausdinti(RZ, prekybosBaze, "Pradiniai duomenys:");

            //Surasomi suskaiciuoti rezultatai
            using (var fr = File.AppendText(RZ))
            {
                fr.WriteLine("Rezultatai:");
                fr.WriteLine(" Is viso aptarnauta: {0} klientu",
                    VisoAptarnauta(prekybosBaze));
                fr.WriteLine(" Daugiausiai pirkeju aptarnavo {0} kasa",
                    KasosNumerisMaxPirkeju(prekybosBaze));
                fr.WriteLine("-----------------------------------------------------------");
                fr.WriteLine("Pirmos savarankiskos uzduoties rezultatai:");
                fr.WriteLine(" Vidutiniskai viena kasa per viena diena aptarnavo: {0,2:f2}",
                    KiekVidutiniskai(prekybosBaze));
                fr.WriteLine();
            }
            KiekDienuNedirbo(prekybosBaze, RZ);
            KiekKievienaKasaAptarnavo(RZ, prekybosBaze);
            KiekvienaDienaAptarnauta(RZ, prekybosBaze);

            //Darbas su masyvais
            int[] kasuSumos = new int[prekybosBaze.n];
            int[] dienuSumos = new int[prekybosBaze.m];
            KiekvienaKasaAptarnavo(prekybosBaze, kasuSumos);
            SpausdintiSumas(RZ, kasuSumos, prekybosBaze.n, "Kasos");

            KiekvienaDienaAptarnauta(prekybosBaze, dienuSumos);
            SpausdintiSumas(RZ, dienuSumos, prekybosBaze.m, "Dienos");

            using (var fr = File.AppendText(RZ))
            {
                fr.WriteLine("-----------------------------------------------------------");
                fr.WriteLine("Antros savarankiskos uzduoties rezultatai:");
            }
            MaziausiaiAptarnautaDiena(prekybosBaze, RZ);
        }

        /// <summary>
        /// Failo duomenis suraso i konteineri
        /// </summary>
        /// <param name="fv">Duomenu failas</param>
        /// <param name="prekybosBaze">Dvimatis konteineris</param>
        static void Skaityti(string fv, ref Matrica prekybosBaze)
        {
            string line;
            using (StreamReader reader = new StreamReader(fv))
            {
                line = reader.ReadLine();
                string[] parts;
                int nn = int.Parse(line);
                line = reader.ReadLine();
                int mm = int.Parse(line);
                prekybosBaze.n = nn;
                prekybosBaze.m = mm;
                for(int i = 0; i < nn; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(';');
                    for(int j = 0; j < mm; j++)
                    {
                        int skaicius = int.Parse(parts[j]);
                        prekybosBaze.Deti(i, j, skaicius);
                    }
                }
            }
        }

        /// <summary>
        /// Spausdina konteinerio duomenis
        /// </summary>
        /// <param name="fv">Rezultatu failas</param>
        /// <param name="prekybosBaze">Dvimatis konteineris</param>
        /// <param name="antraste">Lenteles uzrasas</param>
        static void Spausdinti(string fv, Matrica prekybosBaze, string antraste)
        {
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine(antraste);
                fr.WriteLine(" Kasų kiekis {0}", prekybosBaze.n);
                fr.WriteLine(" Darbo dienų kiekis {0}", prekybosBaze.m);
                fr.WriteLine(" Aptarnautų klientų kiekiai");
                for (int i = 0; i < prekybosBaze.n; i++)
                {
                    for (int j = 0; j < prekybosBaze.m; j++)
                        fr.Write("{0,4:d}", prekybosBaze.ImtiReiksme(i, j));
                    fr.WriteLine();
                }
            }
        }

        /// <summary>
        /// Suskaiciuoja ir grazina prekybos bazeje aptarnautu pirkeju skaiciu
        /// </summary>
        /// <param name="A">Konteinerio vardas</param>
        /// <returns></returns>
        static int VisoAptarnauta(Matrica A)
        {
            int suma = 0;
            for (int i = 0; i < A.n; i++)
                for (int j = 0; j < A.m; j++)
                    suma += A.ImtiReiksme(i, j);
            return suma;
        }
        
        //----------------------------------------------------------------------
        //PIRMA SAVARANKISKA

        /// <summary>
        /// Suskaiciuoja kiek vidutiniskai viena kasa aptarnavo per viena diena
        /// </summary>
        /// <param name="A">Konteinerio vardas</param>
        /// <returns></returns>
        static double KiekVidutiniskai(Matrica A)
        {
            double vidurkis;
            double suma = 0;
            double kiek = 0;
            for(int i = 0; i < A.n; i++)
            {
                for(int j = 0; j < A.m; j++)
                {
                    suma += A.ImtiReiksme(i, j);
                    kiek++;
                }
            }
            vidurkis = suma / kiek;
            return vidurkis;
        }

        /// <summary>
        /// Suskaiciuoja ir isspausdina, kiek kuri kasa nedirbo
        /// </summary>
        /// <param name="A">Konteinerio vardas</param>
        static void KiekDienuNedirbo(Matrica A, string fv)
        {
            using (var fr = File.AppendText(RZ))
            {
                for (int i = 0; i < A.n; i++)
                {
                    int dienos = 0;
                    for (int j = 0; j < A.m; j++)
                        if (A.ImtiReiksme(i, j) == 0)                        
                            dienos++;             
                    if (dienos != 0)
                        fr.WriteLine(" {0} kasa nedirbo {1} diena/as ", i + 1, dienos);                    
                }
                fr.WriteLine("-----------------------------------------------------------");
                fr.WriteLine();
            }
        }

        //----------------------------------------------------------------------

        /// <summary>
        /// Suskaiciuoja ir isspausdina, kiek pirkeju aptarnavo kiekviena kasa
        /// </summary>
        /// <param name="fv">Rezultatu failas</param>
        /// <param name="A">Konteinerio vardas</param>
        static void KiekKievienaKasaAptarnavo(string fv, Matrica A)
        {
            using (var fr = File.AppendText(fv))
            {
                for(int i = 0; i < A.n; i++)
                {
                    int suma = 0;
                    for (int j = 0; j < A.m; j++)
                        suma += A.ImtiReiksme(i, j);
                    fr.WriteLine(" {0} kasa aptarnavo {1} klientu", i + 1, suma);
                }
                fr.WriteLine();
            }
        }

        /// <summary>
        /// Suskaiciuoja ir isspausdina, kiek kieviena diena buvo aptarnauta klientu
        /// </summary>
        /// <param name="fv">Rezultatu failas</param>
        /// <param name="A">Konteinerio vardas</param>
        static void KiekvienaDienaAptarnauta(string fv, Matrica A)
        {
            using (var fr = File.AppendText(fv))
            {
                for(int j = 0; j < A.m; j++)
                {
                    int suma = 0;
                    for (int i = 0; i < A.n; i++)
                        suma += A.ImtiReiksme(i, j);
                    fr.WriteLine(" {0} diena aptarnauta {1} klientu", j + 1, suma);
                }
                fr.WriteLine();
            }
        }

        /// <summary>
        /// Suskaiciuoja ir iraso i masyva, kiek pirkeju aptarnavo kiekviena kasa
        /// </summary>
        /// <param name="A">Konteinerio vardas</param>
        /// <param name="sumos">Sumos masyvas</param>
        static void KiekvienaKasaAptarnavo(Matrica A, int[] sumos)
        {
            for(int i = 0; i < A.n; i++)
            {
                int suma = 0;
                for (int j = 0; j < A.m; j++)
                    suma += A.ImtiReiksme(i, j);
                sumos[i] = suma;
            }
        }

        /// <summary>
        /// Suskaiciuoja ir iraso i masyva, kiek pirkeju buvo aptarnauta kieviena diena
        /// </summary>
        /// <param name="A">Konteinerio vardas</param>
        /// <param name="sumos">Sumos masyvas</param>
        static void KiekvienaDienaAptarnauta(Matrica A, int[] sumos)
        {
            for (int j = 0; j < A.m; j++)
            {
                int suma = 0;
                for (int i = 0; i < A.n; i++)
                    suma += A.ImtiReiksme(i, j);
                sumos[j] = suma;
            }
        }

        /// <summary>
        /// Spausdina visas sumas
        /// </summary>
        /// <param name="fv">Rezultatu failas</param>
        /// <param name="sumos">Sumos masyvas</param>
        /// <param name="n">Sumos masyvo dydis</param>
        /// <param name="pav">Dienos ar kasos</param>
        static void SpausdintiSumas(string fv, int[] sumos, int n, string pav)
        {
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine("Rezultatai per masyva:");
                for(int i = 0; i < n; i++)
                {
                    if (pav == "Dienos")
                        fr.WriteLine(" {0} diena aptarnauta {1} klientu", i + 1, sumos[i]);
                    else
                        fr.WriteLine(" {0} kasa aptarnavo {1} kielntu", i + 1, sumos[i]);
                }
                fr.WriteLine();
            }
        }

        /// <summary>
        /// Suranda ir grazina, kuri kasa aptarnavo daugiausiai
        /// </summary>
        /// <param name="A">Konteinerio vardas</param>
        /// <returns></returns>
        static int KasosNumerisMaxPirkeju(Matrica A)
        {
            int max = 0;
            int numeris = 0;
            for(int i = 0; i < A.n; i++)
            {
                int suma = 0;
                for(int j = 0; j < A.m; j++)
                    suma += A.ImtiReiksme(i, j);
                if(suma > max)
                {
                    max = suma;
                    numeris = i + 1;
                }
                
            }
            return numeris;
        }

        //----------------------------------------------------------------------
        //ANTRA SAVARANKISKA

        /// <summary>
        /// Suskaiciuoja ir grazina, kuria diena buvo aptarnauta maziausiai klientu
        /// </summary>
        /// <param name="A">Kobteinerio vardas</param>
        /// <returns></returns>
        static int MaziausiaAptarnautaPirkeju(Matrica A)
        {
            int min = int.MaxValue;
            for (int j = 0; j < A.m; j++)
            {
                int suma = 0;
                for (int i = 0; i < A.n; i++)
                    suma += A.ImtiReiksme(i, j);
                if (min > suma)
                    min = suma;
            }
            return min;
        }

        /// <summary>
        /// Suskaiciuoja ir isspausdina kuria diena ir kiek pirkeju buvo maziausia
        /// </summary>
        /// <param name="A">Konteinerio vardas</param>
        /// <param name="fv">Rezultatu failas</param>
        /// <returns></returns>
        static void MaziausiaiAptarnautaDiena(Matrica A, string fv)
        {
            using (var fr = File.AppendText(fv))
            {
                int min = MaziausiaAptarnautaPirkeju(A);
                for (int j = 0; j < A.m; j++)
                {
                    int suma = 0;
                    for (int i = 0; i < A.n; i++)
                        suma += A.ImtiReiksme(i, j);
                    if (suma == min)
                        fr.WriteLine(" Maziausiai klientu buvo {0} diena. Aptarnauti {1} prikejai/as", j + 1, min);
                }
                fr.WriteLine("-----------------------------------------------------------");
            }
        }


        //----------------------------------------------------------------------

    }
}
