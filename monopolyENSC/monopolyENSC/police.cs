using monopolyENSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class police : Cases
{
    public police():base("Poste de Police") { }

    public override void action(Joueur j)
    {
        j.etatCourant = Joueur.Etat.enPrison;
        Console.WriteLine("Vous etes envoyé(e) en prison");
    }
}