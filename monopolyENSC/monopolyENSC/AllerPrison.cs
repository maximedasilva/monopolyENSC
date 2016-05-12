using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monopolyENSC
{
    public class AllerPrison : Cartes
    {
        public AllerPrison(TypeC typecarte, string fct):base(typecarte, fct)
        {
        }

        public override void actionCarte(Joueur j)
        {
            j.etatCourant = Joueur.Etat.enPrison;
            j.position = 10;
        }
    }
}
