using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Vasmegye
{
   class Program
   {
      static List<SzemelyiSzam> szemelyiszamok = new List<SzemelyiSzam>();
      static void Main(string[] args)
      {
         Console.WriteLine("2. feladat: Adatok beolvasása, tárolása");
         adatokbeolvasasa("vas.txt");
         Console.WriteLine("\n4. feladat: Ellenörzés");
         feladat04();
         Console.WriteLine($"\n5. feladat: Vas megyében a vizsgált évek alatt {szemelyiszamok.Count} csecsemő született.");
         Console.WriteLine($"\n6. feladat: Fiúk száma {szemelyiszamok.FindAll(a => a.Szam[0] == '1' || a.Szam[0] == '3').Count}");
         Console.WriteLine($"\n7. feladat: Vizsgált időszak: {szemelyiszamok.Min(a => a.evszam())} - {szemelyiszamok.Max(a => a.evszam())}");
         if (szokoevbenszuletett())
         {
            Console.WriteLine("\n8. feladat: Szökőnapon született baba!");
         }
         else
         {
            Console.WriteLine("\n8. feladat: Szökőnapon nem született baba!");
         }
         feladat09();
         Console.WriteLine("\nProgram vége");
         Console.ReadKey();
      }

      private static void feladat09()
      {
         Console.WriteLine("\n 9. feladat: Statisztika");
         var statisztika = szemelyiszamok.GroupBy(a => a.evszam()).Select(b => new { ev = b.Key, fo = b.Count() });
         foreach (var item in statisztika)
         {
            Console.WriteLine($"\t{item.ev} - {item.fo} fő");
         }
      }

      private static bool szokoevbenszuletett()
      {
         var szokoevi = szemelyiszamok.Find(a => a.evszam() % 4 == 0 && a.Szam.Substring(4, 4) == ("0224"));
         return szokoevi != null;
      }

      private static void feladat04()
      {
         List<SzemelyiSzam> hibasSzamok = szemelyiszamok.FindAll(a => !CdvEll(a.Szam));
         foreach (SzemelyiSzam item in hibasSzamok)
         {
            Console.WriteLine($"Hibás a {item.Szam} személyi azonosító!");
            szemelyiszamok.Remove(item);
         }
      }

      public static bool CdvEll(string szam)
      {
         //-- 3. feladat
         string szamNumeric = new string(szam.Where(a => char.IsDigit(a)).ToArray());
         if (szamNumeric.Length != 11)
         {
            return false;
         }
         double szum = 0;
         for (int i = 0; i < szamNumeric.Length - 1; i++)
         {
            szum += char.GetNumericValue(szamNumeric[i]) * (10 - i);
         }
         return char.GetNumericValue(szamNumeric[10]) == szum % 11;
      }

      private static void adatokbeolvasasa(string adatFile)
      {
         if (!File.Exists(adatFile))
         {
            Console.WriteLine("A forrás adatok hiányoznak!");
            Console.ReadLine();
            Environment.Exit(0);
         }
         using (StreamReader sr = new StreamReader(adatFile))
         {
            while (!sr.EndOfStream)
            {
               szemelyiszamok.Add(new SzemelyiSzam(sr.ReadLine()));
            }
         }
      }

  
   }

}
