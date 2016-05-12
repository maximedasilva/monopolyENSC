
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public abstract class Cases {

    public abstract void action(Joueur j);
    public Cases(string _nom)
    {
        nom = _nom;
    }
    public int num { get; set; }
    public string nom { get; set;  }
    
    public override string ToString()
    {
        return num+" "+nom;
    }
}