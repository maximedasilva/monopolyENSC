using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monopolyENSC
{
    class Program
    {
        static void Main(string[] args)
        {
            var j = new Joueur("anthony");
            Console.Write(j);
            var t = new Jeu();
        }
    }
}
