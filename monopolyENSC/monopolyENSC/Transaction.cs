
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */

public class Transaction : Cartes {

    double _valeur;
    Boolean reparation;  // permet d'intégrer les cartes de reparations sans créer une nouvelle classe

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
            Console.WriteLine("Vous avez payé " + ValeurRep + " euros pour vos réparations.");
        }
        else
        {
            j.payer(_valeur, null);
            if (_valeur > 0)
            {
                Console.WriteLine("Vous avez payé " + _valeur + " euros.");
            }
            else
            {
                Console.WriteLine("Vous avez reçu " + Math.Abs(_valeur) + " euros.");
            }
        }
    }
}