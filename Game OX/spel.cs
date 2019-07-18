// <copyright file="spel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace HenE.Abdul.Game_OX
{
    using System.Collections.Generic;

    /// <summary>
    /// De spel.
    /// </summary>
    public class Spel
    {
        /// <summary>
        /// De spelers.
        /// </summary>
        private IList<Speler> spelers = new List<Speler>();
        private Bord huidigeBord;
        private bool stopDeSpel = false;
        private bool vraagEenRondje = true;
        private int wieStart = 0;
        private Speler speler;

        /// <summary>
        /// Bepaalt wie wil starten.
        /// </summary>
        public void WieStart()
        {
           this.speler = this.WieIsSpeler(this.spelers[this.wieStart]);
           this.speler.WieStart(ref this.wieStart);
        }

        /// <summary>
        /// Bepaal wie is de speler op dit moment.
        /// </summary>
        /// <param name="speler">Huidige Speler.</param>
        /// <returns>De spelr.</returns>
        public Speler WieIsSpeler(Speler speler) => speler;

        /// <summary>
        /// Dit start het spel.
        /// </summary>
        /// <param name="dimension">Het formaat van het bord.</param>
        /// <returns>Het getekende bord.</returns>
        public Bord Start(short dimension)
        {
            // maak een bord, met de jusite dimension
            // hoe bepaal je wie mag beginnen?
            this.huidigeBord = new Bord(dimension, this);

            // wie begint?
            this.huidigeBord.TekenBord();
            Speler huidigeSpeler = this.spelers[this.wieStart];
            this.WieIsSpeler(huidigeSpeler);
            while (!this.stopDeSpel || this.huidigeBord.HeeftTekenGewonnen(huidigeSpeler.TeGebruikenTeken))
            {
                List<short> vrijVelden = this.huidigeBord.VrijVelden();

                // teken het bord
                // vraag aan speler 1 wat hij wil doen
                huidigeSpeler.Zet(this.huidigeBord, this.spelers);
                this.huidigeBord.TekenBord();
                if (this.huidigeBord.HeeftTekenGewonnen(huidigeSpeler.TeGebruikenTeken))
                {
                    huidigeSpeler.UHeeftGewonnen();

                    // todo, vertel de andere speler dat hij heeft verloren.
                    this.TegenSpeler(huidigeSpeler, this.spelers).UHeeftVerloren(huidigeSpeler);

                    huidigeSpeler.BeeindigBord(this.huidigeBord);
                    huidigeSpeler.UkrijgtEenPunt(huidigeSpeler);

                    if (!this.huidigeBord.IsBordFinished())
                    {
                        this.VraagNieuwRondje(huidigeSpeler);
                    }
                }

                if (this.huidigeBord.IsBordFinished())
                {
                    this.stopDeSpel = true;
                    huidigeSpeler.HetBoordIsVol();
                    this.VraagNieuwRondje(huidigeSpeler);
                }

                huidigeSpeler = this.TegenSpeler(huidigeSpeler, this.spelers);
            }

            if (this.vraagEenRondje)
            {
                this.VraagNieuwRondje(huidigeSpeler);
            }

            return this.huidigeBord;
        }

        /// <summary>
        /// Vraag de speler voor niuew rondje.
        /// </summary>
        /// <param name="huidigeBord">huidigeBord.</param>
        /// <param name="spelers">De spelers.</param>
        public void NeiuwRondje(Bord huidigeBord, IList<Speler> spelers)
        {
            this.spelers = spelers;
            this.Start(huidigeBord.Dimension);
        }

        /// <summary>
        /// Bepaalt wie gaat tegen de huidige speler spelen.
        /// </summary>
        /// <param name="huidigeSpeler">huidigeSpeler.</param>
        /// <param name="spelers">De spelers.</param>
        /// <returns>De speler.</returns>
        public Speler TegenSpeler(Speler huidigeSpeler, IList<Speler> spelers)
        {
            this.spelers = spelers;
            foreach (Speler speler in this.spelers)
            {
                if (speler != huidigeSpeler)
                {
                    return speler;
                }
            }

            return null;
        }

        /// <summary>
        /// Deze method geef een nieuwe speler als de speler niet al bestaat.
        /// </summary>
        /// <param name="naam">De naam van de human speler.</param>
        /// <param name="teken">welek teken gaat de spelr gebruiken.</param>
        /// <returns>Deze method geeft een neuwie speler terug.</returns>
        public Speler AddHumanSpeler(string naam, Teken teken)
        {
            // bestaat deze speler al?
            if (this.FindSpelerByNaam(naam) != null)
            {
                throw new System.ArgumentException("Speler bestaat al");
            }

            Speler speler = new HumanSpeler(naam)
            {
                TeGebruikenTeken = teken,
            };
            this.spelers.Add(speler);

            return speler;
        }

        /// <summary>
        /// Add Een speler als computer speler.
        /// </summary>
        /// <param name="naam">Naam.</param>
        /// <param name="teken">Teken.</param>
        /// <returns>De computer speler.</returns>
        public Speler AddComputerSpeler(string naam, Teken teken)
        {
            Speler computerSpeler = new ComputerSpeler(naam)
            {
                TeGebruikenTeken = teken,
            };
            this.spelers.Add(computerSpeler);

            return computerSpeler;
        }

        /// <summary>
        /// Deze method zoekt of de naam van de niuwe speler al bastaat.
        /// </summary>
        /// <param name="naam">De naam van de speler.</param>
        /// <returns>De niuwe speler.</returns>
        public Speler FindSpelerByNaam(string naam)
        {
            foreach (Speler speler in this.spelers)
            {
                if (speler.Naam == naam)
                {
                    return speler;
                }
            }

            return null;
        }

        /// <summary>
        /// Vraag om niuwe spel te doen .
        /// </summary>
        /// <param name="huidigeSpeler">huidigeSpeler.</param>
        private void VraagNieuwRondje(Speler huidigeSpeler)
        {
            HumanSpeler speler = new HumanSpeler(huidigeSpeler.Naam);
            speler.VraagNieuwRond(ref this.stopDeSpel, this.huidigeBord, ref this.vraagEenRondje, huidigeSpeler, this.spelers);
        }
    }
}