
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */

public class Transaction : Cartes {

    double _valeur;
    Boolean reparation;  // permet d'int�grer les cartes de reparations sans cr�er une nouvelle classe

    public Transaction(TypeC typeCarte, string fct, double valeur, Boolean rep):base(typeCarte, fct)
    {
        _valeur = valeur;
    }

    public override void actionCarte (Joueur j)
    {
        if (reparation)
        {
            double ValeurRep = 25 * (j.getNbMaison()) + 100 * j.getNbHotel();
            j.payer(ValeurRep, null);
            Console.WriteLine("Vous avez pay� " + ValeurRep + " euros pour vos r�parations.");
        }
        else
        {
            j.payer(_valeur, null);
            if (_valeur > 0)
            {
                Console.WriteLine("Vous avez pay� " + _valeur + " euros.");
            }
            else
            {
                Console.WriteLine("Vous avez re�u " + Math.Abs(_valeur) + " euros.");
            }
        }
    }
}