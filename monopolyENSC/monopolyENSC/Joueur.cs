
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Joueur {


    public Joueur(string _nom,Plateau _p) {//Constructeur 
        nom = _nom;
        num = cpt++;
        cartesEnPossession = new List<Cartes>();
        sous = 2000;
        etatCourant = Etat.vivant;
        position = 0;
        p = _p;
        proprieteEnPossession = new List<Propriete>();
        valeurDernierDeplacement = 0;
    }


    public int compteProprieteCouleurJoueur(ProprieteDeCouleur prop) //On compte combien de propriété de la même couleur que celle envoyé en parametres compte le joueur
    {
        //elle retroune le nb de propriétés de couleur que le joueur a qui sont de la même couleur qu'une propriété donnée
        int nb = 0;
        foreach (Propriete p in proprieteEnPossession)
        {
            if (p is ProprieteDeCouleur)
            {
                ProprieteDeCouleur tmp = p as ProprieteDeCouleur; //double vérification on sait jamais
                if (tmp.Couleur == prop.Couleur)
                {
                    nb = nb + 1;
                }
            }
            
        }
        return nb;
    }
    public bool payer(double prix, Joueur J2)//Fonction de paiement
    {
        if (this.sous > prix)
        {
            sous -= prix;
            if (J2 != null)//Si le J2 n'est pas nul, c'est que le paiement s'effectue entre 2 joueurs, sinon c'est qu'il s'effectue a la banque.
            {
                J2.sous += prix;
            }
            return true;
        }
        else
            return false; 
    }

    internal int getNbGares()//Récupérer le nombre de gare possédées
    {
        int cpt = 0;
        foreach (Propriete p in proprieteEnPossession)
        {
            if (p is Gare)
            {
                cpt++;
            }
        }

        return cpt;
    }
    public int getNbCompagnies()//Récupérer le nombr de compagnies possédées
    {
        int cpt = 0;
        foreach (Propriete p in proprieteEnPossession)
        {
            if (p is Compagnies)
            {
                cpt++;
            }
        }

        return cpt;
    }

    public enum Etat { vivant, mort, enPrison };//ENum de l'état du joueur
    public Plateau p { get; }//Plateau dans lequel evolue le jouer

    public List<Propriete> proprieteEnPossession { get;  }//LIste des propriétés en possession

    public int num  { get; set; }//Numéro unique du joueur

    private static int cpt = 0;//compteur permettant d'affecter des numéros uniques

    public List<Cartes> cartesEnPossession//cartes en possession par le joueur
    {
        get;
    }
    private int nbTourPrison {//nombre de tours passés en prison
        get; set;
    }

    public string nom { get; }//Nom du joueur

    public int valeurDernierDeplacement { get; set; }//valeur du dernier déplacement 

    public double sous { get; set; }//sous actuels possédés par le joueur

    public Etat etatCourant { get; set; }//Etat courant du joueur (cf enum)

    public int position { get; set; }//Position du joueur

    
    public void ajoutCarte(Cartes c)//Rajoutuer une carte aux cartes du joueur
    {
        cartesEnPossession.Add(c);
    }
    
    public void retraitCartes(Cartes c)//sortir une carte du joueur
    {
        if (cartesEnPossession.Count>0)
        {
            //permet de supprimer une carte de la main du joueur
            cartesEnPossession.Remove(cartesEnPossession.Find(current=>current.num==c.num));
        }
    }
    public void mettreHypotheque()//Mettre en hypotheques toutes les propriétés, hypotheque obligatoire
    {
        if(proprieteEnPossession.Count()>0)
        {
            foreach(Propriete p in proprieteEnPossession)
            {
                p.proprietaire = null;
                p.etat = Propriete.EtatPropriete.hypotheque;
            }
        }
    }
   
    public override string ToString()
    {
        string rep = String.Format("{0}, il vous reste {1} euros et vous etes en position {2} ", nom,sous, position);
        if (this.etatCourant!= Etat.mort)
        {
            return rep;
        }
        else
        {
            string repmort = String.Format("{0} est mort", nom);
            return repmort;
        }
        
    }

    public void jouer()//jouer un tour
    {
        if (etatCourant == Etat.vivant)//si on est vivant
        {
            Random des = new Random();
            int De1 = des.Next(1, 7);
            int de2 = des.Next(1, 7);
            valeurDernierDeplacement = De1 + de2;//Random des dés + affectation du déplacement
            position += valeurDernierDeplacement;
            Console.WriteLine("Vous avez fait {0} et {1} lors de votre lancer de dés.", De1, de2);

            if (position >= p.cases.Length)//SI on arrive a la fin du plateau, on revient au début
            {
                position = position % p.cases.Length;
                sous += 100;
                Console.WriteLine("Vous avez reçu 100 en passant par la case depart.");
            }
            Console.WriteLine(this.ToString());
            Console.WriteLine(p.cases[position].nom);
            p.cases[position].action(this);//On fait l'action de la case atuelle 
        }



        else if (etatCourant == Etat.enPrison)//Si on est en prison
        {
            Console.WriteLine(nom + " vous êtes actuellement en prison.");
            if (cartesEnPossession.Count > 0)
            {
                ConsoleKeyInfo choix;
                do
                {


                    Console.WriteLine("Voulez vous utiliser une carte libéré de prison (y) (n)?");//on peut utiliser une carte

                    choix = Console.ReadKey();
                    if (choix.KeyChar == 'y')
                    {
                        etatCourant = Etat.vivant;
                        Console.WriteLine("Vous vous êtes libéré(e) de prison.");
                        var tmp = cartesEnPossession.ElementAt(0);
                        cartesEnPossession.RemoveAt(0);
                        if (tmp._typeCarte == Cartes.TypeC.chance)
                        {
                            p.addCartesChance(tmp);
                        }
                        else
                            p.addCartesCaisseCommunaute(tmp);

                    }
                } while (choix.KeyChar != 'y' && choix.KeyChar != 'n');
                }

            if (etatCourant == Etat.enPrison) //il pourrait très bien s'être libéré avec une carte depuis la vérif du premier if 
            {
                Random des = new Random();
                
                int De1 = des.Next(1, 7);
                int de2 = des.Next(1, 7);
                if (de2 == De1 || nbTourPrison == 3)
                {
                    etatCourant = Etat.vivant;
                    if (de2==De1)
                    {
                        Console.WriteLine("Vous êtes libéré(e) de prison après avoir fait un double.");
                    }
                    else
                    {
                        Console.WriteLine("Vous êtes libéré(e) de prison car vous y etes resté 3 tours.");
                    }
                    
                    position = 10; //10 étant la case prison
                    valeurDernierDeplacement = De1 + de2;
                    position += valeurDernierDeplacement;
                    Console.WriteLine(this.ToString());
                    Console.WriteLine(p.cases[position].nom);
                    p.cases[position].action(this);

                }
                else
                {
                    Console.WriteLine("Pour sortir, vous devez faire un double");
                    Console.WriteLine("Vous avez fait " + De1 + " et " + de2 + ", vous restez en prison");
                    this.nbTourPrison++;
                }
            }
            else if (etatCourant == Etat.mort)
            {
                Console.WriteLine(this);
            }
                
                
        }
            ConsoleKeyInfo c = new ConsoleKeyInfo();
            do
            {
          
                string affiche = "\n Menu: \n 1) Voir tous les joueurs \n 2) Information sur la case. \n 3) Mettre une propriété en hypotheque et vendre les biens dessus\n sinon passer son tour";
                if (this.num == 0)
                {
                    affiche += "\n vous pouvez enregistrer la partie en appuyant sur 's'";
                }
                Console.WriteLine(affiche);
                c = Console.ReadKey();
            Console.Clear();
            if (c.KeyChar == '1')
                {
                    Console.WriteLine(p.playerInfo());

                }
                else if (c.KeyChar == '2')
                {
                    Console.WriteLine(p.cases[position].ToString());
                }
                else if (c.KeyChar == '3')
                {
                    Console.Clear();
                    ConsoleKeyInfo select;
                    int actual = 0;
                    do
                    {
                        Console.Clear();
                        int cpt = 0;
                        foreach (Propriete p in this.proprieteEnPossession)
                        {
                            if (cpt == actual)
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;

                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            Console.WriteLine(p.nom);
                            cpt++;
                        }
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Echap pour quitter");
                        select = Console.ReadKey();
                        if (select.Key == ConsoleKey.DownArrow && actual < proprieteEnPossession.Count - 1)
                        {
                            actual++;
                        }
                        if (select.Key == ConsoleKey.UpArrow && actual > 0)
                        {
                            actual--;
                        }
                        if (select.Key == ConsoleKey.Enter)
                        {
                            this.hypothequeVolontaire(proprieteEnPossession.ElementAt(actual));
                        }
                    }
                    while (select.Key != ConsoleKey.Escape);
                }
                else if (c.KeyChar == 's' && num == 0)
                {

                    if (p.jeu.fichierSave == "")
                    {
                        Console.WriteLine("ecrivez le nom de fichier et appuyez sur entree");
                        p.jeu.saveAsXML(Console.ReadLine());
                    }
                    else
                    {
                        p.jeu.saveAsXML("");
                    }
                }



            }
            while (c.KeyChar == '1' || c.KeyChar == '2'|| c.KeyChar == '3'|| c.KeyChar == 's');
        }
    

    private void hypothequeVolontaire(Propriete propriete)
    {
        if(propriete is ProprieteDeCouleur)
        {
            var tmp = propriete as ProprieteDeCouleur;
            this.sous += (tmp._nbBatimentsConstruits * tmp._prixConstruction) / 2;
            this.sous += tmp._prixHypotheque;
            tmp._nbBatimentsConstruits = 0;

        }
        if(propriete is Compagnies)
        {
            var tmp = propriete as Compagnies;
            this.sous += (tmp._prixHypotheque);
        }
        if(propriete is Gare)
        {
            var tmp = propriete as Gare;
            this.sous = tmp._prixHypotheque;
        }
        proprieteEnPossession.Remove(propriete);
        propriete.etat = Propriete.EtatPropriete.hypotheque;
        propriete.proprietaire = null;
    }

    public int getNbMaison() //permet d'avoir le nombre de maisons du joueur
    {
        int nbMaison = 0;
        foreach (Propriete p in proprieteEnPossession)
        {
            if (p is ProprieteDeCouleur)
            {
                ProprieteDeCouleur tmp = p as ProprieteDeCouleur;
                if (tmp._nbBatimentsConstruits < 5) //si ça vaut 5 c'est en fait un hotel
                {
                    nbMaison = nbMaison + tmp._nbBatimentsConstruits;
                }
            }
            
        }
        return nbMaison;
    }

    public int getNbHotel()
    {
        int nbHotel = 0;
        foreach (Propriete p in proprieteEnPossession)
        {
            if (p is ProprieteDeCouleur)
            {
                ProprieteDeCouleur tmp = p as ProprieteDeCouleur;
                if (tmp._nbBatimentsConstruits == 5)
                {
                    nbHotel = nbHotel + 1;
                }
            }
            
        }
        return nbHotel;
    }

    public Boolean MemeNbMaisons(ProprieteDeCouleur prop) // on vérifie ici que toutes les propriétés ont un nombre de maisons adapté à la construction  
    {
        Boolean res = true;
        int i = 0;
        while (res == true && i < proprieteEnPossession.Count)
        {
            if (proprieteEnPossession.ElementAt(i) is ProprieteDeCouleur)
            {
                ProprieteDeCouleur tmp = proprieteEnPossession.ElementAt(i) as ProprieteDeCouleur;
                if (tmp.Couleur == prop.Couleur)
                {
                    if (tmp._nbBatimentsConstruits != prop._nbBatimentsConstruits && prop._nbBatimentsConstruits != tmp._nbBatimentsConstruits - 1)
                    {
                        res = false;
                    } 
                }
            }
    
            i = i + 1;
        }
        return res;
    }

    public void piocher(List<Cartes> l)
    {
        var tmp = l.ElementAt(0);
        l.RemoveAt(0);
        if (tmp is LibereDePrison)
        {
            
            this.ajoutCarte(tmp);
        }
        else {
            l.Add(tmp);
            
            tmp.actionCarte(this);
        }
    }
}