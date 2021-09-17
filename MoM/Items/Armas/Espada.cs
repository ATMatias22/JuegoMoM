using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoM
{
    public class Espada : Arma
    {
        public Espada(string nombre, int danioBase) : base(nombre, danioBase) { }

        override
        public int getDanioTotal()
        {
            return getDanioBase();
        }

        public override object Clone()
        {
            Item cItem = new Espada(base.getNombre(), base.getDanioBase());

            return cItem;
        }
    }
}
