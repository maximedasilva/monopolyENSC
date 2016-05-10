using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Taxe : Cases {

    string nom;
    double prix;
    public Taxe(string _nom,double _prix) :base()
    {
        nom = _nom;
        prix = _prix;
    }

    
    public float valeurTaxe;
    public override void action(Joueur j)
    {
        j.payer(prix, null);
    }

}