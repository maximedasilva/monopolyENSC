
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public abstract class Cartes {

    public enum TypeC { chance, communaute }
    public TypeC _typeCarte { get;  }
    public string fonction
    {
        get; set;
    }
    private static int  cpt=0;
    public int num { get; }
    public Cartes(TypeC typeCarte, string fct)
    {
        num = cpt++;
        _typeCarte = typeCarte;
        fonction = fct;
    }
    public abstract void actionCarte(Joueur j);
}