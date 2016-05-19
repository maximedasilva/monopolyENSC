
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Une compagnie est une sorte de Propriete.
public class Compagnies : Propriete {


    public Compagnies(string nom,double prixAchat,double prixLoyer,double prixHypotheque):base(nom,prixAchat,prixHypotheque)//Constructeur
    {
    }


    public override double calculLoyer(int valeurDe, int nbCompagnies)//On calcule le loyer en fonction de la valeur des dés et du nombre de compag,ies
    {
        if (nbCompagnies == 1)
        {
            return (double)4 * valeurDe;
        }
        else
            return (double)10 * valeurDe;
    }
    public override void action(Joueur j)//Override de la méthode action 
    {
        if (this.etat == EtatPropriete.achete && proprietaire != j)
        {
            if (j.payer(calculLoyer(j.valeurDernierDeplacement, proprietaire.getNbCompagnies()), proprietaire))
            {
                Console.WriteLine("vous venez de payer le loyer de "+ calculLoyer(j.valeurDernierDeplacement, proprietaire.getNbCompagnies())+" euros à "+ proprietaire.nom);
            }
            else
            {
                j.etatCourant = Joueur.Etat.mort;
                j.mettreHypotheque();
                if (j.sous < 0)
                {
                    Console.WriteLine("Le joueur {0} est mort", j.nom);
                }
            }
        }
        base.action(j);//On appelle la méthode case de propriété (pour avoir une factorisation du code)
    }

}