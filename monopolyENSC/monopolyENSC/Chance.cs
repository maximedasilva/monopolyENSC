
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Chance : Cases {

    
    public Chance():base("Case chance") { //Contructeur
        
    }

    public override void action(Joueur j)//Override de la m�thode de cases 
    {
        j.piocher(j.p.cartesChance);//On pioche une carte et on �x�cute son action
    }

}