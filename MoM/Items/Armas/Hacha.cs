using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoM
{
    public class Hacha : Arma
    {

        float porcentajeCritico;


        public Hacha(String nombre, int danioBase, float porcentajeCritico):base(nombre,danioBase)
        {
            this.porcentajeCritico = porcentajeCritico;
        }


        public bool golpeCritico()
        {
            return true;
        }

        override
        public int getDanioTotal()
        {
            return golpeCritico() ? (getDanioBase() * 2) : getDanioBase();
        }

       

        public override object Clone()
        {
            Item cItem = new Hacha(base.getNombre(), base.getDanioBase(), porcentajeCritico);

            return cItem;
        }
    }
}
