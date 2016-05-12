
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class LibereDePrison : Cartes {

    
    public LibereDePrison(TypeC typecarte, string fct):base(typecarte, fct)
    {
          
    }

    public Boolean liberation (Joueur j)
    {
        Boolean lib;
        if (j.etatCourant ==Joueur.Etat.enPrison)
        {
            j.etatCourant = Joueur.Etat.vivant;
            lib = true;
            j.position = 10; //10 étant la position de la case prison

        }
        else
        {
            lib = false;
        }
        return lib;
    }
}