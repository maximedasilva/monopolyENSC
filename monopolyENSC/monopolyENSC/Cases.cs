
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public abstract class Cases {
    

    public Cases()
    {

    }
    public int num { get; set; }
    public virtual void action(Joueur j) { }

}