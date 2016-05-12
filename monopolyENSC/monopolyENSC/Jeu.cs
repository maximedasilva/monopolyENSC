
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Xml;
using System.Xml.Linq;

public class Jeu {


    public Jeu() {
        
        plateau = new Plateau(this);
        plateau.addJoueur();
    }
    public void saveAsXML(string filename)
    {
        XDocument xmldoc = new XDocument();
        XElement jeu = new XElement("jeu");
        xmldoc.Add(jeu);
        XElement joueurs = new XElement("joueurs");
        jeu.Add(joueurs);
        foreach(Joueur j in plateau.Joueurs)
        {
            XElement joueur = new XElement("joueur");
            joueur.SetAttributeValue("nom", j.nom);
            joueur.SetAttributeValue("num", j.num);
            joueur.SetAttributeValue("sous", j.sous);
            joueur.SetAttributeValue("etatCourant", j.etatCourant.ToString());
            joueur.SetAttributeValue("position", j.position);
            joueur.SetAttributeValue("dernierDeplacement", j.valeurDernierDeplacement);
            joueurs.Add(joueur);
        }
        XElement propriete = new XElement("proprietes");

        foreach(Cases c in plateau.cases)
        {
            if(c is ProprieteDeCouleur)
            {
      
                ProprieteDeCouleur tmp = c as ProprieteDeCouleur;
                if (tmp.etat == Propriete.EtatPropriete.achete || tmp.etat == Propriete.EtatPropriete.hypotheque)
                {
                    XElement couleur = new XElement("Couleur");
                    couleur.SetAttributeValue("num", tmp.num);
                    couleur.SetAttributeValue("proprietaire", tmp.proprietaire);
                    couleur.SetAttributeValue("etat", tmp.etat.ToString());
                    couleur.SetAttributeValue("nbBatiment", tmp._nbBatimentsConstruits);
                }
            }
        }



    
      xmldoc.Save("..//..//data//"+filename+".xml");

       

    }
    

    public Plateau plateau{get; set;}
    
    public void simulation()
    {
       
        while (joueursEnlice())
        {
            simulerUnTour();
        }
    }

    private void simulerUnTour()
    {
        foreach (Joueur j in plateau.Joueurs)
        {
            if (j.etatCourant != Joueur.Etat.enPrison)
            {
                Console.Clear();
                j.jouer();
            }
            
        }
       
    }

    public bool joueursEnlice()
    {
        int cpt = 0;
        foreach(Joueur j in plateau.Joueurs)
        {
            if (j.etatCourant != Joueur.Etat.mort)
            {
                cpt++;
            }
        }
        if (cpt > 1)
            return true;
        else return false;

    }
   
   
}