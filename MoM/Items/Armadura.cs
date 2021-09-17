using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoM
{
    public class Armadura : Item, IEquipable
    {

        int defensa;

        public Armadura(String nombre,int defensa) : base(nombre)
        {
            this.defensa = defensa;
        }

        public int getDefensa()
        {
            return this.defensa;
        }

        public int amortiguarDanio(int danio)
        {
            int danioTotal = this.defensa - danio;

            return danioTotal >= 0 ? 0 : Math.Abs(danioTotal);

        }

        public override void equipar(Personaje p)
        {
            Console.WriteLine("Se acaba de equipar la armadura: " + this.descripcion());
            if (!p.tengoArmaduraEquipada())
            {
                p.equiparArmadura(this);
                p.eliminarItemDelInventario(this);
            }
            else
            {
                p.agregarItemAlInventario(p.getArmadura());
                p.equiparArmadura(this);
                p.eliminarItemDelInventario(this);
            }
        }

        public override object Clone()
        {
            Item cItem = new Armadura(base.getNombre(), defensa);

            return cItem;
        }

        public bool sePudoAutoEquipar(Personaje p)
        {
            if (!p.tengoArmaduraEquipada()) { 
                p.equiparArmadura(this);
                return true;
            }

            return false;
        }

        public void desequiparse(Personaje p)
        {
                Console.WriteLine($"Se desequipo la armadura: {this.descripcion()} y se coloco en el inventario");
                p.agregarItemAlInventario(this);
                p.equiparArmadura(null);
        }
    }
}
