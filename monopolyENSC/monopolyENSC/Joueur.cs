
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
        sous = 10000;
        etatCourant = Etat.vivant;
        position = 0;
        p = _p;
        proprieteEnPossession = new List<Propriete>();
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

    public enum Etat { vivant, mort, enPrison };
    public Plateau p { get; set; }

    public List<Propriete> proprieteEnPossession { get; set; }

    public int num  { get; set;}

    private static int cpt = 0;

    public List<Cartes> cartesEnPossession;

    public string nom { get; set; }

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
        string rep = String.Format("{0}, il lui reste {1} euros et il est en position {2} ", nom,sous, position);
        return rep;
    }
    public void jouer()
    {
        if(etatCourant==Etat.vivant)
        {
            Random des = new Random();
            int De1 = des.Next(1, 7);
            int de2 = des.Next(1, 7);

            position += De1 + de2;
            if(position>=p.cases.Length)
            {
                position = position % p.cases.Length;
                sous += 2000;
            }
            p.cases[position].action(this);
        }
    }

}