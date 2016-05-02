
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public abstract class Propriete : Cases {

    
    public string _nom {get; }
    public double _prixAchat {get; }
    public double _prixLoyer { get; }
    public double _prixHypotheque { get; }



    public Propriete(string nom, double prixAchat, double prixLoyer, double prixHypotheque)
    {
        _nom = nom;
        _prixAchat = prixAchat;
        _prixLoyer = prixLoyer;
        _prixHypotheque = prixHypotheque;
    }

    
    public virtual double calculLoyer()
    {
        return 0;
    }

}