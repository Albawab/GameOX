// <copyright file="ComputerSpeler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HenE.Abdul.Game_OX
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// speler die geen in put heeft en zelf zaken kan bedenken.
    /// </summary>
    public class ComputerSpeler : Speler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComputerSpeler"/> class.
        /// </summary>
        /// <param name="name">name.</param>
        public ComputerSpeler(string name)
            : base(name)
        {
        }

        /// <summary>
        /// bepaal een vrij vak en kies dat.
        /// Kun je winnen? bepalen.
        /// </summary>
        /// <param name="bord">het bord waarp gespeeld wordt. </param>
        /// <param name="spelers">De spelers.</param>
        /// <returns>index van het veld op het bord. </returns>
        public override short BepaalZet(Bord bord, IList<Speler> spelers)
        {
            // todo
            // welke vakjes zijn nog leeg?
            // vragen aan bord om aan te geven wat leeg is
            // loop door de lege velden.
            List<short> vrijeVelden = bord.VrijVelden();

            foreach (short index in vrijeVelden)
            {
                bord.DoeZet(this, index);

                // als ik mijn teken daar invul, win ik dan.
                // of wint de tegenstander dan?
                if (bord.HeeftTekenGewonnen(this.TeGebruikenTeken))
                {
                    bord.ResetVeld(index);
                    return index;
                }
                else
                {
                    Spel spel = new Spel();

                    // ik heb niet gewonnen, reset het veld en laat de tegespeler de zet doen
                    Speler tegenSpeler = bord.TegenSpeler(this, spelers);

                    bord.DoeZet(tegenSpeler, index);
                    if (bord.HeeftTekenGewonnen(tegenSpeler.TeGebruikenTeken))
                    {
                        // tegenstander heeft gewonnen
                        bord.ResetVeld(index);
                        return index;
                    }
                }

                // altijd weer het bord resetten
                bord.ResetVeld(index);
            }

            // als ik hier kom,
            return vrijeVelden[0];
        }

        /// <inheritdoc/>
        public override void UHeeftGewonnen()
        {
        }

        /// <inheritdoc/>
        public override void UkrijgtEenPunt(Speler speler)
        {
        }

        /// <inheritdoc/>
        public override void HetBoordIsVol()
        {
        }
    }
}
