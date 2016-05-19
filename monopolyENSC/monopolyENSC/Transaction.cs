
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
        reparation = rep;
    }

    public override void actionCarte (Joueur j)
    {
        if (reparation)
        {
            double ValeurRep = 25 * (j.getNbMaison()) + 100 * j.getNbHotel();
            if (j.payer(ValeurRep, null))
                Console.WriteLine(fonction);
            else {
                j.mettreHypotheque();
                if (j.sous < 0)
                {
                    j.etatCourant = Joueur.Etat.mort;
                    Console.WriteLine("Le joueur {0} est mort", j.nom);
                }

            }
        }
        else
        {

            if (j.payer(_valeur, null))
                Console.WriteLine(fonction);
            else {
                j.mettreHypotheque();
                if (j.sous < 0)
                {
                    j.etatCourant = Joueur.Etat.mort;
                    Console.WriteLine("Le joueur {0} est mort", j.nom);
                }
            }

        }
    }
}