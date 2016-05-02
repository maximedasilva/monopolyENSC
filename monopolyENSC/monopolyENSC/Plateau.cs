
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
    public List<Cartes> cartesChance { get; set; }
    public List<Cartes> cartesCaisseCommunaute { get; set; }

    public Plateau()
    {
        cases = new Cases[40];

        generate();

        cartesChance = new List<Cartes>();
        cartesCaisseCommunaute = new List<Cartes>();
$
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