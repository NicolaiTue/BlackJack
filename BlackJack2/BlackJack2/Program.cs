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
            Points = Points;
        }

        public virtual void VisKortSæt()
        {
            Console.WriteLine($" {Kulør} {Rang}");
        }
    }

    class BlackJackbord
    {
        public List<Kort> dæk = new List<Kort>();

        List<Kort> Dealer = new List<Kort>();

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
            ShuffleDæk();
        }

        public int BeregnVærdi(List<Kort> spiller)
        {

            // her skal kortene på hånden udregnes husk Es værdi

            return 0;
        }

        public void ShuffleDæk()
        {
            // Her skal dækket blandes
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



            Console.WriteLine("Sæt af spillekort:");
            foreach (var Kort in bord.dæk)
            {
                Kort.VisKortSæt();
            }
        }
    }
}
