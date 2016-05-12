
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Deplacement : Cartes {

    public int _deplacementRelatif; //avancer de 3 cases par exemple
    public int _deplacementAbsolu;  // aller � la rue de la Paix par exemple, c'est le numero de la case d'arriv�e


    public Deplacement(TypeC typecarte, string fct,int depRel, int depAbs):base (typecarte, fct)
    {
        _deplacementAbsolu = depAbs;
        _deplacementRelatif = depRel;
    }

    public void bougeJoueur (Joueur j)
    {
        if (this._deplacementRelatif==0)
        {
            int anciennePosition = j.position;
            j.position = _deplacementAbsolu;

            if (j.position < anciennePosition)
            {
                j.sous += 100;
                Console.WriteLine("Vous gagnez 100 euros en passant par la case d�part.");
            }
        }
        else
        {
            j.position += _deplacementRelatif;

            if (j.position >= j.p.cases.Length)
            {
                j.position = j.position % j.p.cases.Length;
                j.sous += 100;
                Console.WriteLine("Vous gagnez 100 euros en passant par la case d�part.");
            }
        }
    }
}