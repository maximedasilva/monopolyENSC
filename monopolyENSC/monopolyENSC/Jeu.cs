
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Jeu {

    /**
     * 
     */
    public Jeu() {
    }

    public List<Joueur> Joueurs { get; set; }

    public Plateau plateau{get; set;}
    
    public void simulation()
    {
        while(joueursEnlice())
        {
            simulerUnTour();
        }
    }

    private void simulerUnTour()
    {
        foreach (Joueur j in Joueurs)
        {
            if (j.etatCourant != Joueur.Etat.enPrison)
            {
                j.jouer();
            }
        }
    }

    public bool joueursEnlice()
    {
        int cpt = 0;
        foreach(Joueur j in Joueurs)
        {
            if (j.etatCourant != Joueur.Etat.enPrison)
            {
                cpt++;
            }
        }
        if (cpt > 1)
            return true;
        else return false;

    }
    public void addJoueur(Joueur j)
    {
        Joueurs.Add(j);
    }
   
}