
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Joueur {


    public Joueur(string _nom,Plateau _p) {
        nom = _nom;
        num = cpt++;
        cartesEnPossession = new List<Cartes>();
        sous = 2000;
        etatCourant = Etat.vivant;
        position = 0;
        p = _p;
        proprieteEnPossession = new List<Propriete>();
        valeurDernierDeplacement = 0;
    }


    public int compteProprieteCouleurJoueur(ProprieteDeCouleur prop) 
    {
        //elle retroune le nb de propriétés de couleur que le joueur a qui sont de la même couleur qu'une propriété donnée
        int nb = 0;
        foreach (ProprieteDeCouleur p in proprieteEnPossession)
        {
            if (p.Couleur == prop.Couleur)
            {
                nb = nb + 1;
            }
        }
        return nb;
    }
    public bool payer(double prix, Joueur J2)
    {
        if(this.sous>prix)
        {
            sous -= prix;
            if (J2 != null)
            {
                J2.sous += prix;
            }
            return true;
        }
        else return false;
    }

    internal int getNbGares()
    {
        int cpt = 0;
        foreach (Propriete p in proprieteEnPossession)
        {
            if (p is Gare)
            {
                cpt++;
            }
        }

        return cpt;
    }
    public int getNbCompagnies()
    {
        int cpt = 0;
        foreach (Propriete p in proprieteEnPossession)
        {
            if (p is Compagnies)
            {
                cpt++;
            }
        }

        return cpt;
    }

    public enum Etat { vivant, mort, enPrison };
    public Plateau p { get; set; }

    public List<Propriete> proprieteEnPossession { get; set; }

    public int num  { get; set;}

    private static int cpt = 0;

    public List<Cartes> cartesEnPossession;
    private int nbTourPrison {
        get; set;
    }

    public string nom { get; set; }

    public int valeurDernierDeplacement { get; set; }

    public double sous { get; set; }

    public Etat etatCourant { get; set; }

    public int position { get; set; }

    
    public void ajoutCarte(Cartes c)
    {
        cartesEnPossession.Add(c);
    }
    
    public void retraitCartes(Cartes c)
    {
        if (cartesEnPossession.Count>0)
        {
            //permet de supprimer une carte de la main du joueur
            cartesEnPossession.Remove(cartesEnPossession.Find(current=>current.num==c.num));
        }
    }
    public void mettreHypotheque()
    {
        if(proprieteEnPossession.Count()>0)
        {
            foreach(Propriete p in proprieteEnPossession)
            {
                p.proprietaire = null;
                p.etat = Propriete.EtatPropriete.hypotheque;
            }
        }
    }
   
    public override string ToString()
    {
        string rep = String.Format("{0}, il vous reste {1} euros et vous etes en position {2} ", nom,sous, position);
        return rep;
    }

    public void jouer()
    {
        if (etatCourant == Etat.vivant)
        {
            Random des = new Random();
            int De1 = des.Next(1, 7);
            int de2 = des.Next(1, 7);
            valeurDernierDeplacement = De1 + de2;
            position += valeurDernierDeplacement;

            if (position >= p.cases.Length)
            {
                position = position % p.cases.Length;
                sous += 100;
            }
            Console.WriteLine(this.ToString());
            Console.WriteLine(p.cases[position].nom);
            p.cases[position].action(this);
            ConsoleKeyInfo c = new ConsoleKeyInfo();
            do
            {
               string affiche ="\n Menu: \n 1) Voir tous les joueurs \n 2) Information sur la case. \n sinon passer son tour";
                if(this.num==0)
                {
                    affiche += "\n vous pouvez enregistrer la partie en appuyant sur 's'";
                }
                Console.WriteLine(affiche);
                c = Console.ReadKey();

                if (c.KeyChar == '1')
                {
                    Console.WriteLine(p.playerInfo());

                }
                else if (c.KeyChar == '2')
                {
                    Console.WriteLine(p.cases[position].ToString());
                }
                else if (c.KeyChar == 's' && num == 0)
                {
                    Console.WriteLine("ecrivez le nom de fichier et appuyez sur entree");
                    p.jeu.saveAsXML(Console.ReadLine());
                }

                

            }
            while (c.KeyChar == '1' || c.KeyChar == '2');
        }


        else if (etatCourant == Etat.enPrison)
        {
            Random des = new Random();
            int De1 = des.Next(1, 7);
            int de2 = des.Next(1, 7);
            if (de2 == De1 || nbTourPrison == 3)
            {
                etatCourant = Etat.vivant;
                valeurDernierDeplacement = De1 + de2;
                position += valeurDernierDeplacement;
            }
            else
                this.nbTourPrison++;
        }
    }

    public Boolean MemeNbMaisons(ProprieteDeCouleur prop) // on vérifie ici que toutes les propriétés ont un nombre de maisons adapté à la construction  
    {
        Boolean res = true;
        int i = 0;
        while (res== true && i<proprieteEnPossession.Count)
        {
            ProprieteDeCouleur tmp = proprieteEnPossession.ElementAt(i) as ProprieteDeCouleur;
            if (tmp.Couleur == prop.Couleur)
            {
                if (tmp._nbBatimentsConstruits != prop._nbBatimentsConstruits && prop._nbBatimentsConstruits != tmp._nbBatimentsConstruits - 1)
                {
                    res = false;
                }
            }
            i = i + 1;
        }
        return res;
    }
}