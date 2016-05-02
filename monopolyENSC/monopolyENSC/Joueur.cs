
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
    }

    public enum Etat { vivant, mort, enPrison };
    public Plateau p { get; set; }

    public int num  { get; set;}

    private static int cpt = 0;

    public List<Cartes> cartesEnPossession;

    public string nom { get; set; }

    public double sous { get; set; }

    public Etat etatCourant { get; set; }

    public int position { get; set; }

    public void mouvementArgent(double valeur)
    {
        sous += valeur;
    }
    public void ajoutCarte(Cartes c)
    {
        cartesEnPossession.Add(c);
    }
    public void retraitCartes(Cartes c)
    {
        if (cartesEnPossession.Count>0)
        {
            cartesEnPossession.Remove(cartesEnPossession.Find(current=>current.id==c.id));
        }
    }
    public override string ToString()
    {
        string rep = String.Format("{0}, il lui rest {1} euros et il est en position {2} ", nom,sous, position);
        return rep;
    }
    public void jouer()
    {
        if(etatCourant==Etat.vivant)
        {
            Random des = new Random();
            int De1 = des.Next(1, 7);
            int de2 = des.Next(1, 7);

            position += position + De1 + de2;
            if(position>=p.cases.Length)
            {
                position = position % p.cases.Length;
                sous += 2000;
            }
        }
    }

}