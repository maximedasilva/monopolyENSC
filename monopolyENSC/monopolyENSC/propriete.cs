
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public abstract class Propriete : Cases {

   public enum EtatPropriete { libre,achete,hypotheque}
    protected EtatPropriete etat { get; set; }
    public string _nom {get; }
    public double _prixAchat {get; }
    public double _prixLoyer { get; }
    public double _prixHypotheque { get; }
    public Joueur proprietaire { get; set; }




    public Propriete(string nom, double prixAchat, double prixLoyer, double prixHypotheque)
    {
        etat = EtatPropriete.libre;
        _nom = nom;
        _prixAchat = prixAchat;
        _prixLoyer = prixLoyer;
        _prixHypotheque = prixHypotheque;
        proprietaire = null;
    }

    
    public virtual double calculLoyer()
    {
        return 0;
    }

}