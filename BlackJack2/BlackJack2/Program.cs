using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack2
{
    class Kort
    {
        
        public string Kulør { get; set; }
        public string Rang { get; set; }
        public int Points { get; set; }

        public Kort(string kulør, string rang, int points)
        {
            Kulør = kulør;
            Rang = rang;
            Points = points;
        }

        public virtual void VisKortSæt()
        {
            Console.WriteLine($" {Kulør} {Rang}");
        }
    }


    class BlackJackbord
    {
        public List<Kort> dæk = new List<Kort>();

        public List<Kort> Dealer = new List<Kort>();

        public List<Kort> hånd = new List<Kort>();

        public BlackJackbord(int antalDæk)
        {
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
                dæk.Add(new Es(kulør));
            }
            // blander kortene
            ShuffleDæk();
        }

        public int BeregnVærdi(List<Kort> spiller)
        {
            int sum = 0;
            int antalEs = 0;

            // Beregner summen af kortværdierne
            foreach (var kort in spiller)
            {
                // Hvis kortet er et Es, tæl antallet af Es'er og fortsæt til næste trin
                if (kort.Rang == "Es")
                {
                    antalEs++;
                }
                // Hvis kortet er et nummerkort eller et billedkort, tilføj dens værdi til summen
                sum += kort.Points;
            }

            // Behandling af Es'erne
            for (int i = 0; i < antalEs; i++)
            {
                // Hvis summen med Es'et talt som 11 ikke overstiger 21, så tilføj 10 til summen (da Es normalt tælles som 1)
                if (sum + 10 <= 21)
                {
                    sum += 10;
                }
            }
            return sum;
        }

        // funktion der blander kortene, ved at tage et kort fra dækket og ligge det et andet sted i bunken. det gør den 100 gange
        public void ShuffleDæk()
        {
            Random rand = new Random();

            for(int i = 0; i < 100; i++)
            {
                int s = rand.Next(52);
                Kort k = dæk[s];
                dæk.RemoveAt(s);
                dæk.Add(k);
            }
        }
        
        public void UddelStartHånd()
        {
            // den skal  give kort til dealer, spiller og vise dealerens første kort.
            for (int i = 0; i < 2; i++)
            {
                Dealer.Add(dæk[0]);
                dæk.RemoveAt(0);
            }
            for (int i = 0; i < 2; i++)//skal ændres i tilfælde af flere hænder spillere
            {
                hånd.Add(dæk[0]);
                dæk.RemoveAt(0);
            }
            //print info
        }

        public void Hit()
        {
            // Den skal gøre så spiller kan kører den hver gang de vil have et ekstra kort
        }
    }

    // Klasse for et nummereret kort (1-10)
    class Kortnummer : Kort
    {
        public Kortnummer(string kulør, int number) : base(kulør, number.ToString(), number)
        {
        }
    }

    // Klasse for billedkort (knægt, dame, konge og Es)
    class Billedkort : Kort
    {
        public Billedkort(string kulør, string rang) : base(kulør, rang, 10)
        {
        }
    }

    class Es : Kort
    {
        // es points er sat til 1 for man skal tjekke den hver gang den regner pointsne sammen og hvis det er under 21 plusser den med 10
        public Es(string kulør) : base(kulør, "Es", 1)
        {
        }
    }

    class Program
    {
        static void Main()
        {

            BlackJackbord bord = new BlackJackbord(2);

            // ud deler start hånd til spiller og dealer
            bord.UddelStartHånd();

            // Beregn værdien af spillerens hånd
            int spillerVærdi = bord.BeregnVærdi(bord.hånd);

            // Udskriver spillerens hånd og  håndens værdi
            Console.WriteLine("Spillerens hånd:");
            foreach (var kort in bord.hånd)
            {
                kort.VisKortSæt();
            }

            Console.WriteLine($"Samlet værdi af spillerens hånd: {spillerVærdi}");

            // Beregn værdien af dealerens hånd
            int dealerVærdi = bord.BeregnVærdi(bord.Dealer);

            
        }
    }
}
