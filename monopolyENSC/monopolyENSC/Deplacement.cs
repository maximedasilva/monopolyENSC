
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

}