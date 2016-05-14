
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
    public double _prixAchat { get; }
    public double _prixHypotheque { get; }
    public Joueur proprietaire { get; set; }

    public Propriete(string nom, double prixAchat, double prixHypotheque):base(nom)
    {
        etat = EtatPropriete.libre;
        _prixAchat = prixAchat;
        _prixHypotheque = prixHypotheque;
        proprietaire = null;
    }
    public override string ToString()
    {
      string rep=  base.ToString() + "\n elle est " + etat.ToString() + " \n a l'achat elle vaut " + _prixAchat + "e \n en hypothèque elle en vaut " + _prixHypotheque + " ";
        if(proprietaire!=null)
        {
            rep += "\n" + proprietaire.ToString();
        }
        return rep+"\n";
    }
    public override void action(Joueur j)
    {
        if (this.etat == EtatPropriete.libre)
        {
            ConsoleKeyInfo c;
            Console.WriteLine("voulez vous acheter {0} pour {1} (y) (n)", this.nom, this._prixAchat);
            do
            {
                c = Console.ReadKey();
            }
            while (c.KeyChar != 'y' && c.KeyChar != 'n');
            if (c.KeyChar == 'y' && j.payer(_prixAchat, null))
            {
                Console.WriteLine("Vous avez acheté {0}", this.nom);
                this.proprietaire = j;
                etat = EtatPropriete.achete;
                j.proprieteEnPossession.Add(this);
            }
            else
            {
                etat = EtatPropriete.hypotheque;
            }
        } 
        else if(etat==EtatPropriete.hypotheque)
        {
            ConsoleKeyInfo rep;
            do
            {
                Console.WriteLine("cette propriete est en enchère, voulez vous l'acheter pour {0}e (y) (n)??", _prixHypotheque);
                rep = Console.ReadKey();
            }
            while (rep.KeyChar != 'y' && rep.KeyChar != 'n');
        }
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