// <copyright file="Speler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.Abdul.Game_OX
{
    using System.Collections.Generic;

    /// <summary>
    /// Hier staat een abstract class met Gets en sets prop.
    /// </summary>
    public abstract class Speler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Speler"/> class.</summary>
        /// <param name="naam">De naam van de speler.</param>
        public Speler(string naam)
        {
            this.Naam = naam;
        }

        /// <summary>
        /// Gets or sets de naam van de spelers.
        /// </summary>
        public string Naam { get; set; }

        /// <summary>
        /// Gets de punten.
        /// </summary>
        public int Punten { get; private set; }

        /// <summary>
        /// Gets or sets teken van de spelers.
        /// </summary>
        public Teken TeGebruikenTeken { get; set; }

        /// <summary>
        /// Zet een teken op het bord.
        /// </summary>
        /// <param name="bord">Bord.</param>
        /// <param name="spelers">Spelers.</param>
        public void Zet(Bord bord, IList<Speler> spelers)
        {
            short indexOpHetBord = 0;

            // is de zet valid?
            do
            {
                indexOpHetBord = this.BepaalZet(bord, spelers);
            }
            while (!bord.IsValidZet(indexOpHetBord));

            bord.DoeZet(this, indexOpHetBord);
        }

        /// <summary>
        /// functie om te bepalen welke set de gebruiker wil doen.
        /// </summary>
        /// <param name="bord">het bord waarmee gespeeld wordt. </param>
        /// <param name="spelers">Spelers.</param>
        /// <returns>de index van het vakje op het bord. </returns>
        public abstract short BepaalZet(Bord bord, IList<Speler> spelers);

        /// <summary>
        /// Dit method geef een punt aan de winnaar.
        /// </summary>
        /// <param name="bord">Het boord.</param>
        public void BeeindigBord(Bord bord)
        {
            // ben ik winnaar?
            // ja geef me dan een punt
            this.Punten++;
        }

        /// <summary>
        /// Vraagt of de speler is gewonen of nog niet . Als ja stuur een bericht dat hij is gewinnen.
        /// </summary>
        public virtual void UHeeftGewonnen()
        {
        }

        /// <summary>
        /// Wie heeft verloren.
        /// </summary>
        /// <param name="tegenspeler">Tegenspeler.</param>
        public virtual void UHeeftVerloren(Speler tegenspeler)
        {
        }

        /// <summary>
        /// add een punt aan de winnaar.
        /// </summary>
        /// <param name="speler">hudige speler.</param>
        public virtual void UkrijgtEenPunt(Speler speler)
        {
        }

        /// <summary>
        /// Als Het boord vol is, Krijgt je dan deze berecht.
        /// </summary>
        public virtual void HetBoordIsVol()
        {
        }

        /// <summary>
        /// Hier bepaal wie gaat starten.
        /// </summary>
        /// <param name="wieStart">De speler die gat starten.</param>
        public virtual void WieStart(ref int wieStart)
        {
        }

        /// <summary>
        /// De winner.
        /// </summary>
        /// <param name="huidigeSpeler">Huidige speler.</param>
        /// <param name="spelers">Spelers.</param>
        public virtual void DeWinner(Speler huidigeSpeler, IList<Speler> spelers)
        {
        }
    }
}
