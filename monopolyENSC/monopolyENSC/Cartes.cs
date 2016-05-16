
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public abstract class Cartes {

    public enum TypeC { chance, communaute }//Connaitre dans quel tas va �tre rajout�e la carte
    public TypeC _typeCarte { get;  }//Champ pour connaitre le type de carte
    public string fonction//fonction de la carte
    {
        get; set;
    }
    private static int  cpt=0; //Cpt pour num�roter les cartes
    public int num { get; } //Num�ro de la carte
    public Cartes(TypeC typeCarte, string fct) //Constructeur
    {
        num = cpt++;
        _typeCarte = typeCarte;
        fonction = fct;
    }
    public abstract void actionCarte(Joueur j); //M�thode abstratie d'action
}