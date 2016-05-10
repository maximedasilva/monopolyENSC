
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Gare : Propriete {
    public Gare(string nom, double prixAchat, double prixHypotheque): base(nom,prixAchat, prixHypotheque)
    {
     
    }

    
    public override double calculLoyer(int valeur,int nbgares)
    {
        double loyer = valeur * nbgares;
        return loyer;
    }
    public override void action(Joueur j)
    {
        if (this.etat == EtatPropriete.achete && proprietaire != j)
        {
            if(j.payer(calculLoyer(25,proprietaire.getNbGares()),proprietaire))
            { 
                Console.Write("vous venez de payer le loyer");
            }
            else
            {
                j.etatCourant = Joueur.Etat.mort;
                j.mettreHypotheque();
                Console.Write("Le joueur {0} est mort", j.nom);
            }

        }
        else if (this.etat == EtatPropriete.libre)
        {
            ConsoleKeyInfo c;
            Console.Write("voulez vous acheter {0} pour {1} (y) (n)", this.nom, this._prixAchat);
            do
            {
                c = Console.ReadKey();
            }
            while (c.KeyChar != 'y' && c.KeyChar != 'n');
            if (c.KeyChar == 'y' && j.payer(_prixAchat,null))
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
    }

}