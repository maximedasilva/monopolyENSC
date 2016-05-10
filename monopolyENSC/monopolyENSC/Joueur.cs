
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
        //elle retroune le nb de propri�t�s de couleur que le joueur a qui sont de la m�me couleur qu'une propri�t� donn�e
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
        if(etatCourant==Etat.vivant)
        {
            Random des = new Random();
            int De1 = des.Next(1, 7);
            int de2 = des.Next(1, 7);
            valeurDernierDeplacement = De1 + de2;
            position += valeurDernierDeplacement;

            if(position>=p.cases.Length)
            {
                position = position % p.cases.Length;
                sous += 100;
            }
            Console.WriteLine(this.ToString());
            Console.WriteLine(p.cases[position]);
            p.cases[position].action(this);
        }
    }

    public Boolean MemeNbMaisons(ProprieteDeCouleur prop) // on v�rifie ici que toutes les propri�t�s de la m�me couleur que prop ont le m�meme nombre de maisons 
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