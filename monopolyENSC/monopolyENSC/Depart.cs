using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monopolyENSC
{
    class Depart : Cases
    {
        public Depart() : base("case Depart") { }//Constructeur
        public override void action(Joueur j)//Méthode action, elle ne fait rien puisque la case ne "sert a rien"
        { }
    }
    
}
