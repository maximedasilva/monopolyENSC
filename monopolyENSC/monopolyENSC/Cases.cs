
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public abstract class Cases {

    public abstract void action(Joueur j); //M�thode abstraite d'action
    public Cases(string _nom) //Constructeur
    {
        nom = _nom;
    }
    public string nom { get; set;  }//Nom de la carte
    
    public override string ToString()//M�thode d'affichage
    {
        return nom;
    }
}