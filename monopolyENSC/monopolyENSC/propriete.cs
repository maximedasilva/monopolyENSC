
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Propriete : Cases {

    /**
     * 
     */
    public Propriete() {
    }

    /**
     * 
     */
    public string nom;

    /**
     * 
     */
    public float prixAchat;

    /**
     * 
     */
    public float prixLoyer;

    /**
     * 
     */
    public float prixHypotheque;


    /**
     * 
     */
    public virtual double calculLoyer() {
        return 0;
    }

}