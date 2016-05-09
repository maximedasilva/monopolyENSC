
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Cartes {

    public enum TypeC { chance, communaute }
    public TypeC _typeCarte { get; set;  }
    public string fonction;
    private static int  cpt=0;
    public int num { get; set; }
    public Cartes(TypeC typeCarte, string fct)
    {
        num = cpt++;
        _typeCarte = typeCarte;
        fonction = fct;
    }
    
}