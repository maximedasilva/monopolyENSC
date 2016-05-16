
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Communaute : Cases {

    
    public Communaute():base("case caisse de communaute") {//Constructeur
    }

    public override void action(Joueur j)//Override de la méthdoe action de cases
    {
        j.piocher(j.p.cartesCaisseCommunaute);//On pioche une carte caisse communauté et on fait l'action.
    }
}