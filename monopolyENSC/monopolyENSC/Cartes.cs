
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public abstract class Cartes {

    public enum Type { chance, communaute}
    public Type _typeCarte { get; set;  }
    public string fonction;
    private static int  cpt=0;
    public int num { get; set; }
    public Cartes(Type typeCarte)
    {
        num = cpt++;
        _typeCarte = typeCarte;
    }
    
}