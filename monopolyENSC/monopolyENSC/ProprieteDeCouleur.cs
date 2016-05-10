
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ProprieteDeCouleur : Propriete {


    public ProprieteDeCouleur(double prixM, string nom, double prixAchat, double prixHypotheque, couleur _Couleur, double t0, double t1, double t2, double t3, double t4, double t5) :base(nom, prixAchat, prixHypotheque)
    {
        _prixMaison = prixM;
        
        _nbBatimentsConstruits = 0;
        Couleur = _Couleur;

        

         prixLoyer =new double[] { t0,t1,t2,t3,t4,t5};

    }

    public enum couleur {bleu, cyan, rose, marron, orange, rouge, jaune, vert};
    public double[] prixLoyer;
    public couleur Couleur { get; set; }
    public double _prixMaison { get; }

    public double _prixHotel { get;}


    public int _nbBatimentsConstruits { get; set; }

   

    
    public override double calculLoyer() {
        double loyer = prixLoyer[_nbBatimentsConstruits];
        return loyer;
    }
    public override void action(Joueur j)
    {
        if (this.etat == EtatPropriete.achete && proprietaire != j)
        {
            if (j.sous > calculLoyer())
            {
                j.sous -= this.calculLoyer();
                Console.Write("vous venez de payer le loyer");
            }
            else
            {
                j.etatCourant = Joueur.Etat.mort;
                j.mettreHypotheque();
                Console.Write("Le joueur {0} est mort", j.nom);
            }

        }
        else if (this.etat == EtatPropriete.libre)
        {
            ConsoleKeyInfo c;
            Console.Write("voulez vous acheter {0} pour {1} (y) (n)", this.nom, this._prixAchat);
            do
            {
                c = Console.ReadKey();
            }
            while (c.KeyChar != 'y' && c.KeyChar != 'n');
            if (c.KeyChar == 'y' && j.payer(_prixAchat,null))
            {
                Console.WriteLine("Vous avez acheté {0}", this.nom);
                this.proprietaire = j;
                etat = EtatPropriete.achete;
                j.proprieteEnPossession.Add(this);
            }
            else
            {
                etat = EtatPropriete.hypotheque;
            }
        }
    }
    
}