
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Communaute : Cases {

    
    public Communaute():base("case caisse de communaute") {
    }

    public override void action(Joueur j)
    {
        j.piocher(j.p.cartesCaisseCommunaute);
    }
}