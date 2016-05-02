
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Gare : Propriete {

    
    public Gare(string nom, double prixAchat, double prixLoyer, double prixHypotheque): base(nom,prixAchat, prixLoyer, prixHypotheque)
    {
    }

    
    public double calculLoyer(int nbgares)
    {
        double loyer = 25 * nbgares;
        return loyer;
    }

}