
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Text;
using System.Xml;
using System.Xml.Linq;

public class Jeu {


    public Jeu() {//Constructeur de jeu
        
        plateau = new Plateau(this);
        fichierSave = "";
        
    }
    public string fichierSave { get; set; }//Pour connaitre le nom du fichier courant, pour ne pas a avoir a remettre un nom de fichier a chaque fois
    public void loadAnXML(string filename)//Méthode de chargement d'une partie sauvegardée
    {
        XDocument doc = XDocument.Load("..//..//data//" + filename+".xml");//On charge le bon fichier xml
        var jeu = doc.Descendants("jeu").First();//On prend la première balise du document s'appelant "jeu"
        var joueurs = jeu.Descendants("joueur");//on prend toutes les balises joueurs descendants de jeu
        /*
        <jeu>
        <Joueur/>
        <joueur/>
        </jeu
                  */
        foreach(var j in joueurs)//pour chaque joueur
        {
            Joueur tmp = new Joueur((string)j.Attribute("nom"), plateau);//on crée un nouveau joueur a partir des informations dans le xml
            tmp.num = (int)j.Attribute("num");//On lui modifie les attributs pour qu'ils correspondent à son état avant la sauvegarde
            tmp.position = (int)j.Attribute("position");
            tmp.etatCourant = (Joueur.Etat)Enum.Parse(typeof(Joueur.Etat),(string)j.Attribute("etatCourant"));
            tmp.valeurDernierDeplacement = (int)j.Attribute("dernierDeplacement");
            tmp.sous = (int)j.Attribute("sous");
            plateau.addJoueur(tmp);//On l'ajoute au plateau
        }
        var couleur = jeu.Descendants("Couleur");
        foreach(var j in couleur)//pour cahque propriété de couleur
        {
            var tmp = plateau.cases[(int)j.Attribute("num")] as ProprieteDeCouleur;//on cosidère la case en tant que propriété de couelur
            tmp._nbBatimentsConstruits = (int)j.Attribute("nbBatiment");//on lui réaffecte tous ses attributs originels
            tmp.etat = (Propriete.EtatPropriete)Enum.Parse(typeof(ProprieteDeCouleur.EtatPropriete),(string)j.Attribute("etat"));
            var joueur=plateau.Joueurs.Find(current => current.num == (int)j.Attribute("proprietaire"));
            tmp.proprietaire = joueur;
            plateau.cases[(int)j.Attribute("num")] = tmp;

        }
        fichierSave = filename;//On modifie le nom du fichier sur lequel sauvegarder (pour automatiser les prochaine sauvegarde de cette partie

    }
    public void initialiser()//Initialisation du plateau de jeu
    {
        ConsoleKeyInfo c;
        plateau.generate();
        do
        {
            Console.WriteLine("1)Nouvelle partie 2) Charger une partie 3)Quitter");//On laisse a l'utilisateur le choix
            c = Console.ReadKey();
            if (c.KeyChar == '2')
            {
                Console.Clear();
                DirectoryInfo d = new DirectoryInfo("..//..//data");//Si il eut charger, on lui fait choisir un fichier
                FileInfo[] files = d.GetFiles("*.xml");
                int j = 0;
                int i = 0;
                ConsoleKeyInfo selectFile;
                do
                {
                    i = 0;
                    Console.Clear();
                    
                    foreach (var file in files)
                    {
                        if (i == j)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(file.Name);
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor =  ConsoleColor.White;
                            Console.WriteLine(file.Name);
                        }
                        i++;
                    }
                    selectFile = Console.ReadKey();
                    if(selectFile.Key==ConsoleKey.DownArrow&& j<files.Count()-1)
                    {
                        j++;
                    }
                    else if(selectFile.Key == ConsoleKey.UpArrow && j > 0)
                    {
                        j--;
                    }

                } while (selectFile.Key != ConsoleKey.Enter);
                loadAnXML(Path.GetFileNameWithoutExtension(files.ElementAt(j).Name));//On récupère juste le nom du fichier
                     
            }
            if (c.KeyChar == '1')//Sinon il fait une nouvelle partie
            {
                Console.Clear();
                string name = null;
                int cpt = 0;
                do
                {
                    Console.WriteLine("Entrez un nom d'un nouveau joueur (rentrez . pour quitter) 2 joueurs minimum");

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
    public void saveAsXML(string filename)//Sauvegarde en XML
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
            if(c is Gare)
            {
                Gare tmp = c as Gare;
                if (tmp.etat == Propriete.EtatPropriete.achete || tmp.etat == Propriete.EtatPropriete.hypotheque)
                {
                    XElement gare = new XElement("Gare");
                    gare.SetAttributeValue("num", i);
                    gare.SetAttributeValue("proprietaire", tmp.proprietaire.num);
                    gare.SetAttributeValue("etat", tmp.etat.ToString());
                    
                }
            i++;
        }
        jeu.Add(propriete);

        //TODO rajouter les gare et compagnies
        


    
      xmldoc.Save("..//..//data//"+fichierSave+".xml");//On sauvegarde notre partie

       

    }
    

    public Plateau plateau{get; set;}//le plateau de jeu
    
    public void simulation()//Simulation globale du jeu
    {
       
        while (joueursEnlice())//S'il reste plus d'un joueur
        {
            simulerUnTour();//On simule un nouveau tour
        }
    }

    private void simulerUnTour()//Pour chaque tour
    {
        foreach (Joueur j in plateau.Joueurs)//Pour chaque joueur
        {
         
                Console.Clear();//On efface la console et on le fait jouer
                j.jouer();
            
            
        }
       
    }

    public bool joueursEnlice()//Permet de savoir s'il reste plus d'un joueur qui n'est pas mort
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