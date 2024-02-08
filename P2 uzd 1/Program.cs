using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_uzd_1
{

    class Plyta
    {
        private int ilgis; // plytos ilgis, milimetrais
        private int plotis; // plytos plotis, milimetrais
        private int aukštis; // plytos aukštis, milimetrais

        public Plyta(int ilgis, int plotis, int aukštis)
        {
            this.ilgis = ilgis;
            this.plotis = plotis;
            this.aukštis = aukštis;
        }

        // grąžina plytos ilgį 
        public int ImtiPlytosIlgį() { return ilgis; }

        // grąžina plytos plotį
        public int ImtiPlytosPlotį() { return plotis; }

        //grąžina plytos aukštį 
        public int ImtiPlytosAukštį() { return aukštis; }

    }

    class Siena
    {
        private double ilgis; // sienos ilgis, milimetrais
        private int plotis; // sienos plotis, milimetrais
        private double aukštis; // sienos aukštis, milimetrais

        public Siena(double ilgis, int plotis, double aukštis)
        {
            this.ilgis = ilgis;
            this.plotis = plotis;
            this.aukštis = aukštis;
        }

        // grąžina ilgį 
        public double ImtiSienosIlgį() { return ilgis; }

        // grąžina plotį
        public int ImtiSienosPlotį() { return plotis; }

        //grąžina aukštį 
        public double ImtiSienosAukštį() { return aukštis; }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //pirmo tipo plyta
            Plyta p1;
            p1 = new Plyta(250, 120, 88);

            //antro tipo plyta
            Plyta p2;
            p2 = new Plyta(200, 115, 71);

            Console.WriteLine("Pirmo tipo plyta: ");
            Console.WriteLine("Ilgis: {0}mm \nPlotis: {1}mm \nAukstis: {2}mm", p1.ImtiPlytosIlgį(), 
                p1.ImtiPlytosPlotį() ,p1.ImtiPlytosAukštį());
            Console.WriteLine("");

            Console.WriteLine("Antro tipo plyta: ");
            Console.WriteLine("Ilgis: {0}mm \nPlotis: {1}mm \nAukstis: {2}mm", p2.ImtiPlytosIlgį(),
                p2.ImtiPlytosPlotį(), p2.ImtiPlytosAukštį());
            Console.WriteLine("");

            //isorines sienos
            Siena s1;
            s1 = new Siena(5.4, p1.ImtiPlytosIlgį(), 2.5);            
            Siena s2;
            s2 = new Siena(3.7, p1.ImtiPlytosIlgį(), 3.2);          
            Siena s3;
            s3 = new Siena(6.2, p1.ImtiPlytosIlgį(), 3.0);          
            Siena s4;
            s4 = new Siena(4.7, p1.ImtiPlytosIlgį(), 2.7);           

            //vidines sienos
            Siena s11;
            s1 = new Siena(5.4, p2.ImtiPlytosIlgį(), 2.5);
            Siena s22;
            s2 = new Siena(3.7, p2.ImtiPlytosIlgį(), 3.2);
            Siena s33;
            s3 = new Siena(6.2, p2.ImtiPlytosIlgį(), 3.0);
            Siena s44;
            s4 = new Siena(4.7, p2.ImtiPlytosIlgį(), 2.7);

            Console.WriteLine("Pirmai isorinei sienai ismurinti reikia: {0} plytu - vidinei sienai reikia: {1} plytu",
                ReikiaPlytų(p1, s1), ReikiaPlytų(p2, s1));
            Console.WriteLine("Antrai isorinei sienai ismurinti reikia: {0} plytu - vidinei sienai reikia: {1} plytu",
               ReikiaPlytų(p1, s2), ReikiaPlytų(p2, s2));
            Console.WriteLine("Treciai isorinei sienai ismurinti reikia: {0} plytu - vidinei sienai reikia: {1} plytu",
               ReikiaPlytų(p1, s3), ReikiaPlytų(p2, s3));
            Console.WriteLine("Ketvirtai isorinei sienai ismurinti reikia: {0} plytu - vidinei sienai reikia: {1} plytu",
               ReikiaPlytų(p1, s4), ReikiaPlytų(p2, s4));
            Console.WriteLine("");

            Console.WriteLine("Visam namui ismurinti reikia: {0}", ReikiaPlytų(p1, s1)+ReikiaPlytų(p2, s1)+
                ReikiaPlytų(p1, s2)+ReikiaPlytų(p2, s2) + ReikiaPlytų(p1, s3)+ReikiaPlytų(p2, s3)+
                 ReikiaPlytų(p1, s4)+ReikiaPlytų(p2, s4));
            
        }

        /// <summary>
        /// Skaiciuoja kiek reikia plytu vienai sienai ismuryti
        /// </summary>
        /// <param name="p">plytos duomenys</param>
        /// <param name="s">sienos duomenys</param>
        /// <returns></returns>
        static int ReikiaPlytų(Plyta p, Siena s)
        {
            return (int)(s.ImtiSienosIlgį() * 1000 / p.ImtiPlytosIlgį() *
            s.ImtiSienosPlotį() * 1000 / p.ImtiPlytosPlotį() *
            s.ImtiSienosAukštį() * 1000 / p.ImtiPlytosAukštį());
        }

    }
}
