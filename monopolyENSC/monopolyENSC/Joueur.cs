
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Joueur {


    public Joueur(string _nom) {
        nom = _nom;
        num = cpt++;
        cartesEnPossession = new List<Cartes>();
        sous = 10000;
        etatCourant = Etat.vivant;
        position = 0;
    }

    public enum Etat { vivant, mort, enPrison };

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

    }

}