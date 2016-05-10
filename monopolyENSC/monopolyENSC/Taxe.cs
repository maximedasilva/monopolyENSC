using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Taxe : Cases {

    
    double prix;
    public Taxe(string nom,double _prix) :base(nom)
    {
        prix = _prix;
    }

    
    public float valeurTaxe;
    public override void action(Joueur j)
    {
        j.payer(prix, null);
    }

}