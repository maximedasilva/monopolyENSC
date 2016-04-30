
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class ProprieteDeCouleur : Propriete {


    public ProprieteDeCouleur() {
    }


    public float prixMaison { get; set; }

    public float prixHotel { get; set; }


    public int nbMaisonConstruites { get; set; }

    public int nbHotelsConstruits { get; set; }


    public double calculLoyer() {
        if (nbHotelsConstruits == 0)
        {
            return nbMaisonConstruites * prixMaison;
        }
        else
            return prixHotel;
    }

}