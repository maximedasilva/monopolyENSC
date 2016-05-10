
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public abstract class Cases {
    

    public Cases(string _nom)
    {
        nom = _nom;
    }
    public int num { get; set; }
    public string nom { get; set;  }
    public virtual void action(Joueur j) { }
    public override string ToString()
    {
        return nom;
    }
}