
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Jeu {


    public Jeu() {
        Joueurs = new List<Joueur>();
        plateau = new Plateau();
        addJoueur();
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
                Console.WriteLine(j.position + " " + j.nom);
              //  System.Threading.Thread.Sleep(5000);
            }
        }
    }

    public bool joueursEnlice()
    {
        int cpt = 0;
        foreach(Joueur j in Joueurs)
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
    public void addJoueur()
    {
        Console.Clear();
        string name = null;
        int cpt = 0;
        do
        {
            Console.WriteLine("Entrez un nom d'un nouveau joueur joueur (rentrez . pour quitter) 2 joueurs minimum");

            name = Console.ReadLine();
            if (name != ".")
            {
                Joueurs.Add(new Joueur(name, this.plateau));
                cpt++;
            }
        }
        while (cpt < 2 || name != ".");

    }
   
}