
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Chance : Cases {

    
    public Chance():base("Case chance") { 
        
    }

    public override void action(Joueur j)
    {
        j.piocher(j.p.cartesChance);
    }

}