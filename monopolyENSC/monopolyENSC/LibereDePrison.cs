
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class LibereDePrison : Cartes {

    
    public LibereDePrison(TypeC typecarte, string fct):base(typecarte, fct)//Constructeur
    {
          
    }

    public override void actionCarte(Joueur j)//Override de la m�thode d'action
    {
        
        if (j.etatCourant ==Joueur.Etat.enPrison)
        {
            j.etatCourant = Joueur.Etat.vivant;
            Console.WriteLine("Vous �tes lib�r�(e) de prison");
            

        }
        else
        {
            Console.WriteLine("Vous n'�tes pas en prison");
        }
        
    }
}