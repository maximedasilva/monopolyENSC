
using monopolyENSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

/**
 * 
 */
public class Plateau {
    public Cases[] cases{ get; private set; }
    public List<Cartes> cartesChance { get; set; }
    public List<Cartes> cartesCaisseCommunaute { get; set; }
    public List<Joueur> Joueurs { get;  }
    public Jeu jeu { get; }

    public Plateau(Jeu j)
    {
        jeu = j;
        Joueurs = new List<Joueur>();
        cases = new Cases[40];
        cartesChance = new List<Cartes>();
        cartesCaisseCommunaute = new List<Cartes>();

    }
 

    public void addCartesChance(Cartes c)
    {
        cartesChance.Add(c);
    }
    public void addCartesCaisseCommunaute(Cartes c)
    {
        cartesCaisseCommunaute.Add(c);
        
    }
    public void generate()
    {
        XDocument document = XDocument.Load("..//..//Plateau.xml");
        var jeu = document.Descendants("jeu").First();
        var plateau = document.Descendants("plateau").First();
        var groupe = jeu.Descendants("groupe");
        var gares = jeu.Descendants("gare").First();
        var compagnie = jeu.Descendants("compagnie").First();
        var cartes = document.Descendants("cartes").First();
        


        foreach(var g in groupe)
        {
            var terrain = g.Descendants("terrain");
            foreach(var t in terrain)
            {
                cases[(int)t.Attribute("id")] = new ProprieteDeCouleur((double)g.Attribute("maison"), (string)t.Attribute("nom"), (double)t.Attribute("prix"), (double)t.Attribute("hyp"), (ProprieteDeCouleur.couleur)Enum.Parse(typeof(ProprieteDeCouleur.couleur),(string)g.Attribute("couleur")), (double)t.Attribute("t0"), (double)t.Attribute("t1"), (double)t.Attribute("t2"), (double)t.Attribute("t3"), (double)t.Attribute("t4"), (double)t.Attribute("t5"));
            }
        }

        var gare = plateau.Descendants("gare");
        foreach(var ga in gare)
        {
            cases[(int)ga.Attribute("id")] = new Gare((string)ga.Attribute("nom"), (double)gares.Attribute("prix"),  (double)gares.Attribute("hyp"));
        }
        var taxe = plateau.Descendants("taxe");
        foreach(var t in taxe)
        {
            cases[(int)t.Attribute("id")] = new Taxe((string)t.Attribute("nom"), (double)t.Attribute("prix"));
        }
        var compagnies = plateau.Descendants("compagnie");
        foreach(var c in compagnies)
        {
            cases[(int)c.Attribute("id")] = new Compagnies((string)c.Attribute("nom"),(double) compagnie.Attribute("prix"), (double)compagnie.Attribute("mul1"), (double)compagnie.Attribute("hyp"));
        }
        cases[20] = new ParcGratuit();
        cases[30] = new police();
        cases[10] = new Prison();
        cases[0] = new Depart();

        var caseCarte = plateau.Descendants("carte");
        foreach(var c in caseCarte)
        {
            if ((int)c.Attribute("type")==1)
            {
                cases[(int)c.Attribute("id")] = new Communaute();
            }
            else
            {

                cases[(int)c.Attribute("id")] = new Chance();
            }
        }
        

        var caisseCom = cartes.Descendants("paquet");
        

        foreach (var p in caisseCom)
        {
            var carte = p.Descendants("carte");

            if ((int)p.Attribute("type")==1) // creation des cartes communaute
            {
                
                foreach (var c in carte)
                {
                    if ((string)c.Attribute("type") == "argent")
                    {
                        Transaction nvCarte = new Transaction(Cartes.TypeC.communaute, (string)c.Attribute("lib"), (double)c.Attribute("valeur"), false);
                        addCartesCaisseCommunaute(nvCarte);
                    }
                    
                    else if ((string)c.Attribute("type") == "aller_a")
                    {
                        Deplacement nvCarte = new Deplacement(Cartes.TypeC.communaute, (string)c.Attribute("lib"), (int)c.Attribute("dep"), (int)c.Attribute("id"));
                        addCartesCaisseCommunaute(nvCarte);
                    }
                    else if ((string)c.Attribute("type") == "aller_prison")
                    {
                        AllerPrison nvCarte = new AllerPrison(Cartes.TypeC.communaute, (string)c.Attribute("lib"));
                        addCartesCaisseCommunaute(nvCarte);
                    }
                    else if ((string)c.Attribute("type") == "libere")
                    {
                        LibereDePrison nvCarte = new LibereDePrison(Cartes.TypeC.communaute, (string)c.Attribute("lib"));
                        addCartesCaisseCommunaute(nvCarte);
                    }
                }
                
            }
            else if ((int)p.Attribute("type") == 0) //creation des cartes chance
            {
                foreach (var c in carte)
                {
                    if ((string)c.Attribute("type") == "argent")
                    {
                        Transaction nvCarte = new Transaction(Cartes.TypeC.communaute, (string)c.Attribute("lib"), (double)c.Attribute("valeur"), false);
                        addCartesChance(nvCarte);
                    }
                    else if ((string)c.Attribute("type") == "reparation")
                    {
                        Transaction nvCarte = new Transaction(Cartes.TypeC.communaute, (string)c.Attribute("lib"), 0, true);
                        addCartesChance(nvCarte);
                    }
                    else if ((string)c.Attribute("type") == "aller_prison")
                    {
                        AllerPrison nvCarte = new AllerPrison(Cartes.TypeC.chance, (string)c.Attribute("lib"));
                        addCartesChance(nvCarte);
                    }
                    else if ((string)c.Attribute("type") == "libere")
                    {
                        LibereDePrison nvCarte = new LibereDePrison(Cartes.TypeC.chance, (string)c.Attribute("lib"));
                        addCartesChance(nvCarte);
                    }
                    else if ((string)c.Attribute("type") == "aller_a")
                    {
                        Deplacement nvCarte = new Deplacement(Cartes.TypeC.communaute, (string)c.Attribute("lib"), (int)c.Attribute("dep"), (int)c.Attribute("id"));
                        addCartesChance(nvCarte);
                    }
                }
            }
        }
        
    }

    internal string playerInfo()
    {
        string rep="";
        foreach(Joueur j in Joueurs)
        {
            rep += "\n" + j.ToString();
        }
        return rep;
    }
    public void addJoueur(Joueur j)
    {
        Joueurs.Add(j);
    }

    public int calculePropCouleur (ProprieteDeCouleur prop) // calcule le nombre de prop de la couleur de la prop qu'on fournit 
    {
        int nb = 0;
        foreach (Cases c in cases)
        {
            if (c is ProprieteDeCouleur)
            {
                ProprieteDeCouleur tmp = c as ProprieteDeCouleur;
                if (tmp.Couleur == prop.Couleur)
                {
                    nb = nb + 1;
                }

            }
        }
        return nb;
    }
}