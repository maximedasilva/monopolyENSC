
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
    public List<Cartes> cartes { get; set; }//TODO modififier cette liste pour qu'il y ait deux pile

    public void addCartes(Cartes c)
    {
        cartes.Add(c);
    }
}