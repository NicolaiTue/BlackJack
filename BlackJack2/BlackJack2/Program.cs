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

        public BlackJackbord(int antalDæk)// antalDæk gør ikke noget, men kan give mulighed for viderudvikling
        {
            // Tilføj nummererede kort (1-10) for hver kulør
            foreach (var kulør in new[] { "Hjerter", "Spar", "Ruder", "Klør" })
            {
                for (int i = 2; i <= 10; i++)
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

        /* en bedre måde at blande kortsætet på
        public void shuffleDeck()
        {
            Random rand = new Random();

            for (int i = dæk.size() - 1; i > 0; i--)
            {
                int j = rand.nextInt(i + 1);
                Kort temp = dæk.get(i);
                dæk.set(i, dæk.get(j));
                dæk.set(j, temp);
            }
        }
        */

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
            for (int i = 0; i < 1; i++)//skal ændres i tilfælde af flere hænder spillere
            {
                hånd.Add(dæk[0]);
                dæk.RemoveAt(0);
            }
        }

        public void HitDealer()
        {
            for (int i = 0; i < 1; i++)//skal ændres i tilfælde af flere hænder spillere
            {
                Dealer.Add(dæk[0]);
                dæk.RemoveAt(0);
            }
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
            string response;

            int spillerVærdi; // Deklareret spillerVærdi udenfor for-loopet
            int dealerVærdi;// Deklareret dealerVærdi udenfor for-loopet


            BlackJackbord bord = new BlackJackbord(2);// gør mulihed for videreudvikling

            // ud deler start hånd til spiller og dealer
            bord.UddelStartHånd();

            // Beregn værdien af spillerens hånd
            spillerVærdi = bord.BeregnVærdi(bord.hånd);

            // Udskriver spillerens hånd og  håndens værdi
            Console.WriteLine("Spillerens hånd:");
            foreach (var kort in bord.hånd)
            {
                kort.VisKortSæt();
            }

            Console.WriteLine($"Samlet værdi af spillerens hånd: {spillerVærdi}");

            // her skal der vises Dealerens ene kort
            Console.WriteLine();
            Console.Write("Dealerens synlige hånd:");
            bord.Dealer[0].VisKortSæt();
            Console.WriteLine();



            // Beregn værdien af dealerens hånd
            dealerVærdi = bord.BeregnVærdi(bord.Dealer);


            while (spillerVærdi < 21) // Så længe spillerens værdi er under 21 skal spilleren kunne hit
            {
                Console.WriteLine("tast h for at hit, s for at stå");
                response = Console.ReadLine();

                if (response == "H" || response == "h")
                {

                    bord.Hit(); 

                    spillerVærdi = bord.BeregnVærdi(bord.hånd); // Opdater spillerens værdi efter et nyt kort er trukket

                    Console.WriteLine("Spillerens hånd:");
                    foreach (var kort in bord.hånd)
                    {
                        kort.VisKortSæt();
                    }

                    Console.WriteLine($"Samlet værdi af spillerens hånd: {spillerVærdi}");
                }
                else if (response == "S" || response == "s")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("du tastede forkert, prøv igen");
                }
            }

            Console.WriteLine();

            // så længe dealeren har under 17 og spiller ikke har busted skal dealer tage et nyt kort.
            if (dealerVærdi > 17 && spillerVærdi < 22) 
            {
                Console.WriteLine("Dealerens hånd:");
                foreach (var kort in bord.Dealer)
                {
                    kort.VisKortSæt();
                }
                dealerVærdi = bord.BeregnVærdi(bord.Dealer);
                Console.WriteLine($"Samlet værdi af Dealerens hånd: {dealerVærdi}");
            }

            while (dealerVærdi < 17 && spillerVærdi < 22) // Så længe dealer værdi er 16 eller under og spilleren ikke er bust
            {
                bord.HitDealer();

                //Opdater dealerens værdi efter et nyt kort er trukket
                Console.WriteLine("Dealerens hånd:");
                foreach (var kort in bord.Dealer)
                {
                    kort.VisKortSæt();
                }

                dealerVærdi = bord.BeregnVærdi(bord.Dealer);
                Console.WriteLine($"Samlet værdi af Dealerens hånd: {dealerVærdi}");
            }
                       
            dealerVærdi = bord.BeregnVærdi(bord.Dealer);
            Console.WriteLine($"Endelige værdi af Dealerens hånd: {dealerVærdi}");

            if (spillerVærdi >= 22)
            {
                Console.WriteLine("Spiller har bust");
            }
            else if (dealerVærdi >= 22)
            {
                Console.WriteLine("Dealer har bust spiller har vundet");
            }
            else if (dealerVærdi < spillerVærdi)
            {
                Console.WriteLine("Spiller har vundet");
            }
            else if (dealerVærdi > spillerVærdi)
            {
                Console.WriteLine("Spiller har Tabt");
            }
            else if (spillerVærdi == dealerVærdi)
            {
                Console.WriteLine("Spillet er draw");
            }           
        }
    }
}
