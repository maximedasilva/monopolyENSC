
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class LibereDePrison : Cartes {

    
    public LibereDePrison(TypeC typecarte, string fct):base(typecarte, fct)
    {
          
    }

    public override void actionCarte(Joueur j)
    {
        
        if (j.etatCourant ==Joueur.Etat.enPrison)
        {
            j.etatCourant = Joueur.Etat.vivant;
            Console.WriteLine("Vous êtes libéré(e) de prison");
            

        }
        else
        {
            Console.WriteLine("Vous n'êtes pas en prison");
        }
        
    }
}