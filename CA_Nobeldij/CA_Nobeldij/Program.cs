using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace CA_Nobeldij
{
    class Elethossz
    {
        private int Tol { get; set; }
        private int Ig { get; set; }
        public int ElethosszEvekben => Tol == -1 || Ig == -1 ? -1 : Ig - Tol;

        public bool IsmertAzElethossz => ElethosszEvekben != -1;

        public Elethossz(string SzuletesHalalozas)
        {
            string[] m = SzuletesHalalozas.Split('-');
            try
            {
                Tol = int.Parse(m[0]);
            }
            catch (Exception)
            {
                Tol = -1;
            }
            try
            {
                Ig = int.Parse(m[1]);
            }
            catch (Exception)
            {
                Ig = -1;
            }
        }
    }
    class Program
    {
        static void Main()
        {
            var sr = new StreamReader(
                path:@"..\..\..\src\orvosi_nobeldijak.txt",
                encoding: Encoding.UTF8);

            var nobeldijak = new List<Nobeldij>();

            _ = sr.ReadLine();

            while (!sr.EndOfStream)
                nobeldijak.Add(new Nobeldij(sr.ReadLine()));

            Console.WriteLine($"3. feladat: Díjazottak száma: {nobeldijak.Count} fő");

            var f4 = nobeldijak
                .Where(n => n.Ev == nobeldijak.Max(n => n.Ev))
                .First();

            Console.WriteLine($"4. feladat: Utolsó év: {f4.Ev}");

            Console.Write("5. feladat: Kérem adja meg egy ország kódját: ");
            string kod = Console.ReadLine().ToLower();

            var f5DB = nobeldijak
                .Where(n => n.Orszagkod.ToLower() == kod)
                .Count();

            if (f5DB == 0)
                Console.WriteLine("A megadott országból nem volt díjazott!");
            else if (f5DB == 1) 
            {
                var f5 = nobeldijak
                    .Where(n => n.Orszagkod.ToLower() == kod)
                    .First();
                Console.WriteLine("A megadott ország díjazottja:");
                Console.WriteLine($"\tNév: {f5.Nev}");
                Console.WriteLine($"\tÉv: {f5.Ev}");
                Console.WriteLine($"\tSz/H: {f5.Szuleteshalalozas}");
            }
            else if (f5DB > 1)
                Console.WriteLine($"A megadott országból {f5DB} fő díjazott volt!");

            var f6dic = new Dictionary<string, int>();
            var f6 = nobeldijak
                .GroupBy(n => n.Orszagkod);

            foreach (var item in f6)
            {
                f6dic.Add(item.Key, item.Count());
            }

            var f6List = f6dic
                .Where(f => f.Value > 5);

            Console.WriteLine("6. Statisztika");

            foreach (var item in f6List)
                Console.WriteLine("\t" + item.Key + " - " + item.Value + " fő");

            var f7List = new List<Elethossz>();

            foreach (var item in nobeldijak)
            {
                f7List.Add(new Elethossz(item.Szuleteshalalozas));
            }

            var f7 = f7List
                .Where(f => f.IsmertAzElethossz)
                .Average(f => f.ElethosszEvekben);

            Console.WriteLine($"7. feladat: A keresett átlag: {f7:0.0} év");


            Console.ReadKey();
        }
    }
}
