using System;
using System.Drawing;

namespace BlackJack1
{
    class Kort
    {
        public string Kulør { get; set; }
        public string Rang { get; set; }

        public Kort(string kulør, string rang)
        {
            Kulør = kulør;
            Rang = rang;
        }

        public virtual void VisKortSæt()
        {
            Console.WriteLine($" {Kulør} {Rang}");
        }
    }

    // Klasse for et nummereret kort (1-10)
    class Kortnummer : Kort
    {
        public Kortnummer(string kulør, int number) : base(kulør, number.ToString())
        {
        }
    }

    // Klasse for billedkort (knægt, dame, konge og Es)
    class Billedkort : Kort
    {
        public Billedkort(string kulør, string rang) : base(kulør, rang)
        {
        }
    }

    class Program
    {
        static void Main()
        {
            // Opret et sæt spillekort alså en list
            List<Kort> dæk = new List<Kort>();

            // Tilføj nummererede kort (1-10) for hver kulør
            foreach (var kulør in new[] { "Hjerter", "Spar", "Ruder", "Klør" })
            {
                for (int i = 1; i <= 10; i++)
                {
                    dæk.Add(new Kortnummer(kulør, i));
                }

                // Tilføj ansigtskort for hver kulør (Knægt, Dame, Konge og Es)
                dæk.Add(new Billedkort(kulør, "Knægt"));
                dæk.Add(new Billedkort(kulør, "Dame"));
                dæk.Add(new Billedkort(kulør, "Konge"));
                dæk.Add(new Billedkort(kulør, "Es"));
            }

            Console.WriteLine("Sæt af spillekort:");
            foreach (var Kort in dæk)
            {
                Kort.VisKortSæt();
            }
        }
    }
}