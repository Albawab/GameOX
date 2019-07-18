// <copyright file="HumanSpeler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace HenE.Abdul.Game_OX
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// De human Speler.
    /// </summary>
    public class HumanSpeler : Speler
    {
        private readonly Spel spel = new Spel();

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanSpeler"/> class.
        /// </summary>
        /// <param name="naam">Naam van de speler.</param>
        public HumanSpeler(string naam)
            : base(naam)
        {
        }

        /// <summary>
        /// vraag aan de speler welke zet hij wil doen.
        /// </summary>
        /// <param name="bord">het bord waarp gespeeld wordt.</param>
        /// <param name="spelers">De speler.</param>
        /// <returns>index van het veld op het bord.</returns>
        public override short BepaalZet(Bord bord, IList<Speler> spelers)
        {
            // vraag aan de gebruiker wat hij/zij wil doen?
            Console.WriteLine("Welke zet wil je doen ?");
            short spelerKeuze = 0;
            List<short> vrijeVelden = bord.VrijVelden();

            do
            {
                Console.Write("Kies een nummer ");

                foreach (short vrijVeld in vrijeVelden)
                {
                    Console.Write(vrijVeld.ToString() + ", ");
                }

                Console.WriteLine();
                string splelerKiest = Console.ReadLine();
                if (short.TryParse(splelerKiest, out spelerKeuze))
                {
                    spelerKeuze = short.Parse(splelerKiest);
                }
                else
                {
                    while (!short.TryParse(splelerKiest, out spelerKeuze))
                    {
                        Console.WriteLine("U hebt geen nummer ingevoerd");
                        splelerKiest = Console.ReadLine();
                        short.TryParse(splelerKiest, out spelerKeuze);
                    }
                }
            }
            while (vrijeVelden.IndexOf(spelerKeuze) < 0);
            int gebruikerNummer = spelerKeuze;
            return (short)gebruikerNummer;
        }

        /// <inheritdoc/>
        public override void UHeeftGewonnen()
        {
            // als dit een remote speler is, zorg dan dat er een event uit gaat naar de tcpClient
            Console.WriteLine();
            Console.WriteLine(this.Naam + " : Hoeraaaa " + this.Naam + " je bent gewonnen !!!!");
            Console.WriteLine();
        }

        /// <inheritdoc/>
        public override void UHeeftVerloren(Speler tegenspeler)
        {
            // als dit een remote speler is, zorg dan dat er een event uit gaat naar de tcpClient
            Console.WriteLine();
            Console.WriteLine("U Heeft verloren, {0} heeft gewonnen.", tegenspeler.Naam);
            Console.WriteLine();
        }

        /// <inheritdoc/>
        public override void UkrijgtEenPunt(Speler speler)
        {
            base.UkrijgtEenPunt(speler);

            Console.WriteLine(speler.Naam + " Je hebt : " + speler.Punten + " Punt !!");
        }

        /// <inheritdoc/>
        public override void HetBoordIsVol()
        {
            Console.WriteLine("Het boord is vol !!!");
        }

        /// <inheritdoc/>
        public override void WieStart(ref int wieStart)
        {
            Console.WriteLine("  Wil je starten , J of N ?");
            string deEersteSpeler = Console.ReadLine().ToLower();
            while ((deEersteSpeler != "j") && (deEersteSpeler != "n"))
            {
                Console.WriteLine("Geef J of N !");
                deEersteSpeler = Console.ReadLine();
            }

            if (deEersteSpeler == "n")
            {
                wieStart = 1;
            }
        }

        /// <summary>
        /// Vraag de speler of hij een ander rondje wil doen.
        /// </summary>
        /// <param name="stopDeSpel">true als ja . false als nee.</param>
        /// <param name="huidigeBord">huidigeBord.</param>
        /// <param name="vraagEenRondje">True of false. Wil je nog een rondje.</param>
        /// <param name="speler">De speler.</param>
        /// <param name="spelers">De spelers.</param>
        public void VraagNieuwRond(ref bool stopDeSpel, Bord huidigeBord, ref bool vraagEenRondje, Speler speler, IList<Speler> spelers)
        {
            Console.WriteLine("Wil je nog een rondje , J of N?");
            string nieuwRondjes = Console.ReadLine();
            nieuwRondjes.ToLower();
            while ((nieuwRondjes != "j") && (nieuwRondjes != "n"))
            {
                Console.WriteLine("Geef J of N !");
                nieuwRondjes = Console.ReadLine();
            }

            if (nieuwRondjes == "j")
            {
                stopDeSpel = false;
                huidigeBord.ResetBord();
                this.spel.NeiuwRondje(huidigeBord, spelers);
            }
            else
            {
                stopDeSpel = true;
                vraagEenRondje = false;
                this.DeWinner(speler, spelers);
            }
        }

        /// <inheritdoc/>
        public override void DeWinner(Speler huidigeSpeler, IList<Speler> spelers)
        {
            if (huidigeSpeler.Punten > this.spel.TegenSpeler(huidigeSpeler, spelers).Punten)
            {
                Console.WriteLine(huidigeSpeler.Naam + " heeft gewonnen ....");
            }
            else
            {
                Console.WriteLine("Niemand heeft gewonnen !");
            }
        }
    }
}
