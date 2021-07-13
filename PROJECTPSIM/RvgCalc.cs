using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECTPSIM
{
    class RvgCalc
    {
        public static double weibull_1b()
        {
            Random rnd = new Random();
            double angka = rnd.NextDouble();
            double a = 3;
            double b = 2.7;
            double g = 9;
            double wibull = (Math.Pow((-1 * Math.Log10(1 - angka)), 1 / a) * b) + g;
            return wibull;
        }
        public static double rayleigh_1a()
        {
            Random rnd = new Random();
            double angka = rnd.NextDouble();
            double a = 2;
            double g = 12;
            double rayleigh = a * (Math.Sqrt(-2 * Math.Log10(1 - angka))) + g;
            return rayleigh;
        }
        public static double rayleigh_b1()
        {
            Random rnd = new Random();
            double angka = rnd.NextDouble();
            double a = 2;
            double g = 1.1;
            double rayleigh = a * (Math.Sqrt(-2 * Math.Log10(1 - angka))) + g;
            return rayleigh;
        }
        public static double burr_b2()
        {
            Random rnd = new Random();
            double angka = rnd.NextDouble();
            double k = 5;
            double a = 2.8;
            double b = 2;
            double burr = Math.Pow(1 / Math.Pow(1 - angka, 1 / k) - 1, 1 / a) * b;
            return burr;
        }
        public static double dagum_a1()
        {
            Random rnd = new Random();
            double angka = rnd.NextDouble();
            double k = 1;
            double a = 2;
            double b = 1;
            double dagum = Math.Pow(1 / ((Math.Pow(1 / angka, 1 / k) - 1)), 1 / a) * b;
            return dagum;
        }
        public static double gamma_a2()
        {
            Random rnd = new Random();
            double angka = rnd.NextDouble();
            double b = 1.3;
            double gamma = Math.Log10(1 - angka) / -b;
            return gamma;
        }
        public static double exponen()
        {
            Random rnd = new Random();
            double angka = rnd.NextDouble();
            double lmd = 1.2;
            double t = 1;
            double exponens = -(1 / lmd) * Math.Log10(1 - angka);
            return exponens;
        }
    }
}
