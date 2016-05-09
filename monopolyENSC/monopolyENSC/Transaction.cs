
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Transaction : Cartes {

    double _valeur;
    Boolean reparation;  // permet d'intégrer les cartes de reparations sans créer une nouvelle classe

    public Transaction(TypeC typeCarte, string fct, double valeur, Boolean rep):base(typeCarte, fct)
    {
        _valeur = valeur;
    }

}