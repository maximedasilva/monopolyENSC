
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Xml;
using System.Xml.Linq;

public class Jeu {


    public Jeu() {
        
        plateau = new Plateau(this);
        
    }
    public void loadAnXML(string filename)
    {
        XDocument doc = XDocument.Load("..//..//data//" + filename + ".xml");
        var jeu = doc.Descendants("jeu").First();
        var joueurs = jeu.Descendants("joueur");
        foreach(var j in joueurs)
        {
            Joueur tmp = new Joueur((string)j.Attribute("nom"), plateau);
            tmp.num = (int)j.Attribute("num");
            tmp.position = (int)j.Attribute("position");
            tmp.etatCourant = (Joueur.Etat)Enum.Parse(typeof(Joueur.Etat),(string)j.Attribute("etatCourant"));
            tmp.valeurDernierDeplacement = (int)j.Attribute("dernierDeplacement");
            tmp.sous = (int)j.Attribute("sous");
            plateau.addJoueur(tmp);
        }
        var couleur = jeu.Descendants("couleur");
        foreach(var j in couleur)
        {

        }


    }
    public void initialiser()
    {
        loadAnXML("testset");
    /*    Console.Clear();
        string name = null;
        int cpt = 0;
        do
        {
            Console.WriteLine("Entrez un nom d'un nouveau joueur joueur (rentrez . pour quitter) 2 joueurs minimum");

            name = Console.ReadLine();
            if (name != ".")
            {
                plateau.Joueurs.Add(new Joueur(name, plateau));
                cpt++;
            }
        }
        while (cpt < 2 || name != ".");*/
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
        int i = 0;
        foreach (Cases c in plateau.cases)
        {
            
            if(c is ProprieteDeCouleur)
            {
      
                ProprieteDeCouleur tmp = c as ProprieteDeCouleur;
                if (tmp.etat == Propriete.EtatPropriete.achete || tmp.etat == Propriete.EtatPropriete.hypotheque)
                {
                    XElement terrain = new XElement("Couleur");
                    terrain.SetAttributeValue("num", i);
                    terrain.SetAttributeValue("proprietaire", tmp.proprietaire.nom);
                    terrain.SetAttributeValue("etat", tmp.etat.ToString());
                    terrain.SetAttributeValue("nbBatiment", tmp._nbBatimentsConstruits);

                    propriete.Add(terrain);
                }
            }
            i++;
        }
        jeu.Add(propriete);
        


    
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