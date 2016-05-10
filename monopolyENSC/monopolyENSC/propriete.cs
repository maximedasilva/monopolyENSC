
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public abstract class Propriete : Cases {

   public enum EtatPropriete { libre,achete,hypotheque}
    public EtatPropriete etat { get; set; }
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

    public override void action(Joueur j)
    {
        if (this.etat == EtatPropriete.achete && proprietaire != j)
        {
            if (j.sous > calculLoyer())
            {
                j.payer(calculLoyer(), proprietaire);
                Console.Write("vous venez de payer le loyer");
            }
            else
            {
                j.etatCourant = Joueur.Etat.mort;
                j.mettreHypotheque();
                Console.Write("Le joueur {0} est mort", this._nom);
            }

        }
        else if (this.etat == EtatPropriete.libre)
        {
            ConsoleKeyInfo c;
            Console.Write("voulez vous acheter {0} pour {1} (y) (n)", this._nom, this._prixAchat);
            do
            {
                c = Console.ReadKey();
            }
            while (c.KeyChar != 'y' && c.KeyChar != 'n');
            if (c.KeyChar == 'y' && j.payer(_prixAchat, null))
            {
                Console.WriteLine("Vous avez acheté {0}", this._nom);
                this.proprietaire = j;
                etat = EtatPropriete.achete;
                j.proprieteEnPossession.Add(this);
            }
            else
            {
                etat = EtatPropriete.hypotheque;
            }
        }
    }
    public virtual double calculLoyer()
    {
        return 0;
    }

}