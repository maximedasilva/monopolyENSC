
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Deplacement : Cartes {

    public int _deplacementRelatif; //avancer de 3 cases par exemple
    public int _deplacementAbsolu;  // aller à la rue de la Paix par exemple, c'est le numero de la case d'arrivée


    public Deplacement(TypeC typecarte, string fct,int depRel, int depAbs):base (typecarte, fct)
    {
        _deplacementAbsolu = depAbs;
        _deplacementRelatif = depRel;
    }

    public override void actionCarte (Joueur j)
    {
        if (this._deplacementRelatif==0)
        {
            int anciennePosition = j.position;
            j.position = _deplacementAbsolu;

            Console.WriteLine(this.fonction);
            if (j.position < anciennePosition)
            {
                j.sous += 100;
                Console.WriteLine("Vous gagnez 100 euros en passant par la case départ.");
            }
            Console.WriteLine(j.ToString());
            Console.WriteLine(j.p.cases[j.position].nom);
            
            j.p.cases[j.position].action(j);

            
        }
        else
        {
            Console.WriteLine(this.fonction);
            j.position += _deplacementRelatif;
            Console.WriteLine(j.p.cases[j.position].nom);
            
            j.p.cases[j.position].action(j);

            if (j.position >= j.p.cases.Length)
            {
                j.position = j.position % j.p.cases.Length;
                j.sous += 100;
                Console.WriteLine("Vous gagnez 100 euros en passant par la case départ.");
            }
        }
    }
}