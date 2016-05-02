
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ProprieteDeCouleur : Propriete {


    public ProprieteDeCouleur(double prixM, double prixH, string nom, double prixAchat, double prixLoyer, double prixHypotheque, couleur _Couleur) :base(nom, prixAchat, prixLoyer, prixHypotheque)
    {
        _prixMaison = prixM;
        _prixHotel = prixH;
        _nbHotelsConstruits = 0;
        _nbMaisonConstruites = 0;
        Couleur = _Couleur;
    }

    public enum couleur {bleu, cyan, rose, marron, orange, rouge, jaune, vert};

    public couleur Couleur { get; set; }
    public double _prixMaison { get; }

    public double _prixHotel { get;}


    public int _nbMaisonConstruites { get; set; }

    public int _nbHotelsConstruits { get; set; }

    //le calcul de loyer ne peut pas se faire sans XML
    public override double calculLoyer() {
        double loyer= _prixLoyer* (_nbMaisonConstruites+1);
        return loyer;
    }

}