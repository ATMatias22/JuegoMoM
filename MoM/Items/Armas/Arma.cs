using MoM.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoM
{
    public abstract class Arma : Item, IEquipable
    {
        int danioBase;
        public Arma(String nombre, int danioBase) : base(nombre)
        {
            this.danioBase = danioBase;
        }

        public int getDanioBase()
        {
            return this.danioBase;
        }

        public abstract int getDanioTotal();


        public override void equipar(Personaje p)
        {
            Console.WriteLine("Se acaba de equipar el Arma: " + this.descripcion());
            if (p.tengoArmaEquipada())
            {
                p.agregarItemAlInventario(p.getArma());
            }
            p.equiparArma(this);
            p.eliminarItemDelInventario(this);
        }

        public bool sePudoAutoEquipar(Personaje p)
        {
            if (!p.tengoArmaEquipada())
            {
                p.equiparArma(this);
                return true;
            }
            return false;
        }

        public void desequiparse(Personaje p)
        {
            Console.WriteLine($"Se desequipo el arma: {this.descripcion()} y se coloco en el inventario");
            p.agregarItemAlInventario(this);
            p.equiparArma(null);
        }

        public virtual void equiparMunicion(IMunicion m, Personaje p)
        {
            throw new EstaArmaNoPuedeColocarMunicionesException("Esta arma no se le puede colocar municion");
        }

    }
}
