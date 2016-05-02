
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
}