using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Taxe : Cases {

    
    double prix { get; set; }
    public Taxe(string nom,double _prix) :base(nom)
    {
        prix = _prix;
    }

    
 
    public override void action(Joueur j)
    {
       if(!j.payer(prix, null))
        { 
          
                j.mettreHypotheque();

                if (!j.payer(prix, null))
                {
                    j.etatCourant = Joueur.Etat.mort;
                    Console.WriteLine("Le joueur {0} est mort", j.nom);
                }
            else
            {
                Console.WriteLine("la mise en hypothèque de vos biens vous a sauvé !");
            }


        }
            Console.WriteLine(this);
    }
    public override string ToString()
    {
        return  nom+" vous payez "+prix;
    }
}