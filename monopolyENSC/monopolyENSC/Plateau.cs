
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
    public Cases[] cases{ get; set; }
  //  public List<CaisseCommunaute> {get; set;}
    public List<Cartes> cartes { get; set; }//TODO modififier cette liste pour qu'il y ait deux pile

    public Plateau()
    {
        cases = new Cases[40];
        cartes = new List<Cartes>();
        generate();
    }
    public void addCartes(Cartes c)
    {
        cartes.Add(c);
    }
    public void generate()
    {
        XDocument plateau = XDocument.Load("..//..//Plateau.xml");
        var jeu = plateau.Descendants("jeu").First();
        var groupe = j.Descendants("groupe");
        foreach(var g in groupe)
        {
            var terrain = g.Descendants("Terrain");
            foreach(var t in terrain)
            {
                cases[t.]
            }
        }
        
    }
}