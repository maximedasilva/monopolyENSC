
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public abstract class Propriete : Cases
{

    public enum EtatPropriete { libre, achete, hypotheque }
    public EtatPropriete etat { get; set; }
    public string _nom { get; }
    public double _prixAchat { get; }
    public double _prixHypotheque { get; }
    public Joueur proprietaire { get; set; }

    public Propriete(string nom, double prixAchat, double prixHypotheque)
    {
        etat = EtatPropriete.libre;
        _nom = nom;
        _prixAchat = prixAchat;
        _prixHypotheque = prixHypotheque;
        proprietaire = null;
    }

    public override void action(Joueur j)
    {

    }
    public virtual double calculLoyer()
    {
        return 0;
    }
    public virtual double calculLoyer(int valeur, int nbPossede)
    {
        return 0;
    }
}