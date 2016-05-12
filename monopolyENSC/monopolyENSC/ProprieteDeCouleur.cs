
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ProprieteDeCouleur : Propriete {


    public ProprieteDeCouleur(double prixM, string nom, double prixAchat, double prixHypotheque, couleur _Couleur, double t0, double t1, double t2, double t3, double t4, double t5) :base(nom, prixAchat, prixHypotheque)
    {
        _prixConstruction = prixM;
        
        _nbBatimentsConstruits = 0;
        Couleur = _Couleur;

        

         prixLoyer =new double[] { t0,t1,t2,t3,t4,t5};

    }

    public enum couleur {bleu, cyan, rose, marron, orange, rouge, jaune, vert};
    public double[] prixLoyer;
    public couleur Couleur { get; set; }
    public double _prixConstruction { get; }

    


    public int _nbBatimentsConstruits { get; set; }

   

    
    public override double calculLoyer() {
        double loyer = prixLoyer[_nbBatimentsConstruits];
        return loyer;
    }
    public override void action(Joueur j)
    {
        if (this.etat == EtatPropriete.achete )
        {
            if (proprietaire != j)
            {
                if (j.sous > calculLoyer())
                {
                    
                    j.payer(this.calculLoyer(), proprietaire);
                    Console.WriteLine("vous venez de payer le loyer de "+calculLoyer()+" euros");
                }
                else
                {
                    j.etatCourant = Joueur.Etat.mort;
                    j.mettreHypotheque();
                    Console.WriteLine("Le joueur {0} est mort", j.nom);
                }
            }
            else
            {
                Console.WriteLine("ce terrain vous appartient voulez faire une nouvelle construction dessus?");
                ConsoleKeyInfo c;
                do
                {
                    c = Console.ReadKey();
                }
                while (c.KeyChar != 'y' && c.KeyChar != 'n');
                if (c.KeyChar == 'y')
                {
                    if (construire(j))
                    {
                        Console.WriteLine("vous avez construit un nouveau terrain");
                    }
                    else
                    {
                        Console.WriteLine("vous ne pouvez pas construire");
                    }
                }
            }
        }
        base.action(j);
    }
    public Boolean construire(Joueur j)
    {
        // on vérifie que le joueur a bien toutes les propriétés de la couleur 
        // et aussi qu'il y a bien autant de maisons dans chaque propriété de la couleur 
        if (j.compteProprieteCouleurJoueur(this) == j.p.calculePropCouleur(this) && this._nbBatimentsConstruits<=5) 
        {
            if (j.MemeNbMaisons(this) == true)
            {
                if (j.payer(_prixConstruction, null))
                {
                    _nbBatimentsConstruits = _nbBatimentsConstruits + 1;
                    return true;
                }
                else return false;
            }
            else return false;
        }
        else return false;
    }
}