
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Jeu {


    public Jeu() {
        
        plateau = new Plateau();
        plateau.addJoueur();
    }
    
    

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
        foreach (Joueur j in plateau.Joueurs)
        {
            if (j.etatCourant != Joueur.Etat.enPrison)
            {
                Console.Clear();
                j.jouer();
            }
            
        }
    }

    public bool joueursEnlice()
    {
        int cpt = 0;
        foreach(Joueur j in plateau.Joueurs)
        {
            if (j.etatCourant != Joueur.Etat.mort)
            {
                cpt++;
            }
        }
        if (cpt > 1)
            return true;
        else return false;

    }
   
   
}