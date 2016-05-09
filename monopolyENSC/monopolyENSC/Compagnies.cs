
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Compagnies : Propriete {

    /**
     * 
     */
    public Compagnies(string nom,double prixAchat,double prixLoyer,double prixHypotheque):base(nom,prixAchat,prixLoyer,prixHypotheque)
    {
    }

    /**
     * 
     */
    public override double calculLoyer() {
        // TODO implement here
        return 0;
    }

}