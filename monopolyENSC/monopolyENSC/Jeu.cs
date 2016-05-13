
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Text;
using System.Xml;
using System.Xml.Linq;

public class Jeu {


    public Jeu() {
        
        plateau = new Plateau(this);
        fichierSave = "";
        
    }
    public string fichierSave { get; set; }
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
        var couleur = jeu.Descendants("Couleur");
        foreach(var j in couleur)
        {
            var tmp = plateau.cases[(int)j.Attribute("num")] as ProprieteDeCouleur;
            tmp._nbBatimentsConstruits = (int)j.Attribute("nbBatiment");
            tmp.etat = (Propriete.EtatPropriete)Enum.Parse(typeof(ProprieteDeCouleur.EtatPropriete),(string)j.Attribute("etat"));
            var joueur=plateau.Joueurs.Find(current => current.num == (int)j.Attribute("proprietaire"));
            tmp.proprietaire = joueur;
            plateau.cases[(int)j.Attribute("num")] = tmp;

        }


    }
    public void initialiser()
    {
        ConsoleKeyInfo c;
        do
        {
            Console.WriteLine("1)Nouvelle partie 2) Charger une partie 3)Quitter");
            c = Console.ReadKey();
            if (c.KeyChar == '2')
            {
                Console.Clear();
                DirectoryInfo d = new DirectoryInfo("..//..//data");
                FileInfo[] files = d.GetFiles("*.xml");
                int i = 1;
                foreach(var file in files)
                {
                    Console.WriteLine(i+") "+file.Name);
                    i++;
                }
            }
            if (c.KeyChar == '1')
            {
                Console.Clear();
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
                while (cpt < 2 || name != ".");
            }
            if(c.KeyChar=='3')
            {
                System.Environment.Exit(1);
            }
        }
        while (c.KeyChar != '1' && c.KeyChar != '2');
    }
    public void saveAsXML(string filename)
    {
        if(fichierSave=="")
        {
            fichierSave = filename;
        }
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
                    terrain.SetAttributeValue("proprietaire", tmp.proprietaire.num);
                    terrain.SetAttributeValue("etat", tmp.etat.ToString());
                    terrain.SetAttributeValue("nbBatiment", tmp._nbBatimentsConstruits);

                    propriete.Add(terrain);
                }
            }
            i++;
        }
        jeu.Add(propriete);
        


    
      xmldoc.Save("..//..//data//"+fichierSave+".xml");

       

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