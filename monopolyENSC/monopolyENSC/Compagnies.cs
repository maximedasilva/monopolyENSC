
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
    public Compagnies(string nom,double prixAchat,double prixLoyer,double prixHypotheque):base(nom,prixAchat,prixHypotheque)
    {
    }

    /**
     * 
     */
    public override double calculLoyer(int valeurDe, int nbCompagnies)
    {
        if (nbCompagnies == 1)
        {
            return (double)4 * valeurDe;
        }
        else
            return (double)10 * valeurDe;
    }
    public override void action(Joueur j)
    {
        if (this.etat == EtatPropriete.achete && proprietaire != j)
        {
            if (j.payer(calculLoyer(j.valeurDernierDeplacement, proprietaire.getNbCompagnies()), proprietaire))
            {
                Console.WriteLine("vous venez de payer le loyer de "+ calculLoyer(j.valeurDernierDeplacement, proprietaire.getNbCompagnies())+" euros");
            }
            else
            {
                j.etatCourant = Joueur.Etat.mort;
                j.mettreHypotheque();
                Console.WriteLine("Le joueur {0} est mort", j.nom);
            }
        }
        base.action(j);
    }

}