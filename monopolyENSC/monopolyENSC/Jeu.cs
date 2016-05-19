
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
    public Plateau plateau
    {
        get;
        
    }//le plateau de jeu

    public string fichierSave { get; private set; }//Pour connaitre le nom du fichier courant, pour ne pas a avoir a remettre un nom de fichier a chaque fois

    public void loadAnXML(string filename)//M�thode de chargement d'une partie sauvegard�e
    {
        XDocument doc = XDocument.Load("..//..//data//" + filename+".xml");//On charge le bon fichier xml
        var jeu = doc.Descendants("jeu").First();//On prend la premi�re balise du document s'appelant "jeu"
        var joueurs = jeu.Descendants("joueur");//on prend toutes les balises joueurs descendants de jeu
        /*
        <jeu>
        <Joueur/>
        <joueur/>
        </jeu
                  */
        foreach(var j in joueurs)//pour chaque joueur
        {
            Joueur tmp = new Joueur((string)j.Attribute("nom"), plateau);//on cr�e un nouveau joueur a partir des informations dans le xml
            tmp.num = (int)j.Attribute("num");//On lui modifie les attributs pour qu'ils correspondent � son �tat avant la sauvegarde
            tmp.position = (int)j.Attribute("position");
            tmp.etatCourant = (Joueur.Etat)Enum.Parse(typeof(Joueur.Etat),(string)j.Attribute("etatCourant"));
            tmp.valeurDernierDeplacement = (int)j.Attribute("dernierDeplacement");
            tmp.sous = (int)j.Attribute("sous");
            plateau.addJoueur(tmp);//On l'ajoute au plateau
        }
        var couleur = jeu.Descendants("Couleur");
        foreach(var proprieteCouleur in couleur)//pour cahque propri�t� de couleur
        {
            var tmp = plateau.cases[(int)proprieteCouleur.Attribute("num")] as ProprieteDeCouleur;//on consid�re la case en tant que propri�t� de couelur
            tmp._nbBatimentsConstruits = (int)proprieteCouleur.Attribute("nbBatiment");//on lui r�affecte tous ses attributs originels
            tmp.etat = (Propriete.EtatPropriete)Enum.Parse(typeof(ProprieteDeCouleur.EtatPropriete),(string)proprieteCouleur.Attribute("etat"));
            if ((int)proprieteCouleur.Attribute("proprietaire") != -1)
            {
                var joueur = plateau.Joueurs.Find(current => current.num == (int)proprieteCouleur.Attribute("proprietaire"));
                tmp.proprietaire = joueur;
                joueur.proprieteEnPossession.Add(tmp);
            }
            plateau.cases[(int)proprieteCouleur.Attribute("num")] = tmp;

        }
        var gares = jeu.Descendants("Gare");
        foreach(var gare in gares)
        {
            var tmp = plateau.cases[(int)gare.Attribute("num")] as Gare;//on consid�re la case en tant que gare
            tmp.etat = (Propriete.EtatPropriete)Enum.Parse(typeof(ProprieteDeCouleur.EtatPropriete), (string)gare.Attribute("etat"));
            if ((int)gare.Attribute("proprietaire") != -1)
            {
                var joueur = plateau.Joueurs.Find(current => current.num == (int)gare.Attribute("proprietaire"));
                tmp.proprietaire = joueur;

                joueur.proprieteEnPossession.Add(tmp);
            }
            plateau.cases[(int)gare.Attribute("num")] = tmp;
        }
        var compagnies = jeu.Descendants("Compagnie");
        foreach(var compagnie in compagnies)
        {
            var tmp = plateau.cases[(int)compagnie.Attribute("num")] as Compagnies;//on consid�re la case en tant que compagnie
            tmp.etat = (Propriete.EtatPropriete)Enum.Parse(typeof(ProprieteDeCouleur.EtatPropriete), (string)compagnie.Attribute("etat"));
            if ((int)compagnie.Attribute("proprietaire") != -1)
            {
                var joueur = plateau.Joueurs.Find(current => current.num == (int)compagnie.Attribute("proprietaire"));
                tmp.proprietaire = joueur;

                joueur.proprieteEnPossession.Add(tmp);
            }
            plateau.cases[(int)compagnie.Attribute("num")] = tmp;
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
                DirectoryInfo d = new DirectoryInfo("..//..//data");//Si il veut charger, on lui fait choisir un fichier
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
                    else if(selectFile.Key==ConsoleKey.Escape)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
  
                        initialiser();
                    }

                } while (selectFile.Key != ConsoleKey.Enter);
                loadAnXML(Path.GetFileNameWithoutExtension(files.ElementAt(j).Name));//On r�cup�re juste le nom du fichier
                     
            }
            if (c.KeyChar == '1')//Sinon il fait une nouvelle partie
            {
                Console.Clear();
                string name = null;
                int cpt = 0;
                do
                {
                    Console.WriteLine("Nouveau joueur (rentrez . pour quitter) 2 joueurs minimum");

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
        if(fichierSave=="")//Si fichierSave est null i.e cette partie n'a jamais �t� sauvegard�e
        {
            fichierSave = filename;//on r�cup�re le nom du fichier �crit par l'utilisateur et on l'affecte.
        }
        XDocument xmldoc = new XDocument();//On cr�e un nouveau fichier Xml
        XElement jeu = new XElement("jeu");//ON cr�e une balise g�n�rale jeu
        xmldoc.Add(jeu);//On l'ajoute au fichier
        XElement joueurs = new XElement("joueurs");//On ajoute une balise joueur
        jeu.Add(joueurs);
        foreach(Joueur j in plateau.Joueurs)//Pour chaque joueur dans mon jeu
        {
            XElement joueur = new XElement("joueur");//On cr�e une nouvelle balise joueur
            joueur.SetAttributeValue("nom", j.nom);//A laquelle on ajoute toutes les informations voulues(nom num sous etat...)
            joueur.SetAttributeValue("num", j.num);
            joueur.SetAttributeValue("sous", j.sous);
            joueur.SetAttributeValue("etatCourant", j.etatCourant.ToString());
            joueur.SetAttributeValue("position", j.position);
            joueur.SetAttributeValue("dernierDeplacement", j.valeurDernierDeplacement);
            joueurs.Add(joueur);//On ajoute cet �l�ment a notre xml
        }
        XElement propriete = new XElement("proprietes");
        int i = 0;
        foreach (Cases c in plateau.cases)//Pour chaque cases
        {
            
            if(c is ProprieteDeCouleur)//Si c'est une propriete
            {
      
                ProprieteDeCouleur tmp = c as ProprieteDeCouleur;
                if (tmp.etat == Propriete.EtatPropriete.achete || tmp.etat == Propriete.EtatPropriete.hypotheque)//si elle a un interet a etre sauvegard�e (elle a �t� achet�e)
                {
                    XElement terrain = new XElement("Couleur");//On cr�e une nouvelle balise pour stocker la propri�t� de couleur
                    terrain.SetAttributeValue("num", i);//et on rajoute des attributs pour chaque element int�ressant 
                    if(tmp.etat == Propriete.EtatPropriete.achete)//Sii elle est achet�e (et pas en hypoth�que)
                    terrain.SetAttributeValue("proprietaire", tmp.proprietaire.num);
                    else
                        terrain.SetAttributeValue("proprietaire", -1);
                    terrain.SetAttributeValue("etat", tmp.etat.ToString());
                    terrain.SetAttributeValue("nbBatiment", tmp._nbBatimentsConstruits);

                    propriete.Add(terrain);//on ajoute le terrain aux propri�t�s
                }
            }
            if (c is Gare)//on fait de m�me pour les gares
            {
                Gare tmp = c as Gare;
                if (tmp.etat == Propriete.EtatPropriete.achete || tmp.etat == Propriete.EtatPropriete.hypotheque)
                {
                    XElement gare = new XElement("Gare");
                    gare.SetAttributeValue("num", i);
                    if (tmp.etat == Propriete.EtatPropriete.achete)
                        gare.SetAttributeValue("proprietaire", tmp.proprietaire.num);
                    else
                        gare.SetAttributeValue("proprietaire", -1);
                    gare.SetAttributeValue("etat", tmp.etat.ToString());
                    propriete.Add(gare);
                }
            }
            if(c is Compagnies)//On fait la m�me pour les compagnies
            {
                Compagnies tmp = c as Compagnies;
                if (tmp.etat == Propriete.EtatPropriete.achete || tmp.etat == Propriete.EtatPropriete.hypotheque)
                {
                    XElement compagnie = new XElement("Compagnie");
                    compagnie.SetAttributeValue("num", i);
                    if (tmp.etat == Propriete.EtatPropriete.achete)
                        compagnie.SetAttributeValue("proprietaire", tmp.proprietaire.num);
                    else
                        compagnie.SetAttributeValue("proprietaire", -1);
                    compagnie.SetAttributeValue("etat", tmp.etat.ToString());
                    propriete.Add(compagnie);
                }
            }
            i++;
        }
        jeu.Add(propriete);//On ajoute les propri�t�s au XML

        
    
      xmldoc.Save("..//..//data//"+fichierSave+".xml");//On sauvegarde notre partie

       

    }
    

    
    public void simulation()//Simulation globale du jeu
    {
       
        while (plateau.joueursEnlice())//S'il reste plus d'un joueur
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

    
   
   
}