
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public abstract class Cartes {

    public enum Type { chance, communaute}
    public Type _typeCarte { get; set;  }
    public string fonction; 

    public Cartes(Type typeCarte)
    {
        _typeCarte = typeCarte;
    }
    
}