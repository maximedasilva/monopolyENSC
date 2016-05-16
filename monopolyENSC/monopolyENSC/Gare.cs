
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Gare : Propriete {
    public Gare(string nom, double prixAchat, double prixHypotheque): base(nom,prixAchat, prixHypotheque)//constructeur
    {
     
    }

    
    public override double calculLoyer(int valeur,int nbgares)//Override de la émthode permettant de calculer le loyer
    {
        double loyer = valeur * nbgares;
        return loyer;
    }
    public override void action(Joueur j)//Override de la méthode permettant de faire l'action de la case
    {
        if (this.etat == EtatPropriete.achete && proprietaire != j)
        {
            if(j.payer(calculLoyer(25,proprietaire.getNbGares()),proprietaire))
            { 
                Console.WriteLine("vous venez de payer le loyer de "+ calculLoyer(25, proprietaire.getNbGares())+" euros à "+ proprietaire.nom);
            }
            else
            {
                j.etatCourant = Joueur.Etat.mort;
                j.mettreHypotheque();
                Console.WriteLine("Le joueur {0} est mort", j.nom);
            }

        }
        base.action(j);//ON appelle la méthode action de propriété pour factoriser le code
        
    }

}