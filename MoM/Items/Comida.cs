using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoM
{
    public class Comida : Item
    {

        int cantVidaDeCura;
        int cantVecesQueSePuedeUsar;
        int cantDisponibles;


        public Comida(String nombre, int cantVidaDeCura, int cantVecesQueSePuedeUsar): base(nombre)
        {
            this.cantVidaDeCura = cantVidaDeCura;
            this.cantVecesQueSePuedeUsar = cantVecesQueSePuedeUsar;
            this.cantDisponibles = cantVecesQueSePuedeUsar;
        }


        public int getCantVidaDeCura()
        {
            return this.cantVidaDeCura;
        }

        public int getcantVecesQueSePuedeUsar()
        {
            return this.cantVecesQueSePuedeUsar;
        }

        public int getCantDisponibles()
        {
            return this.cantDisponibles;
        }

        public override void equipar(Personaje p)
        {
            if(cantDisponibles > 0)
            {
                Console.WriteLine("Se acaba de equipar la comida: " + this.getNombre());
                p.curarse(cantVidaDeCura);
                this.cantDisponibles--;
                Console.WriteLine($"Le quedan ({ cantDisponibles }) usos");
            }
            else
            {
                Console.WriteLine("Ya no tiene");
            }

        }

        public override string descripcion()
        {
            return $"{this.getNombre()}({this.cantDisponibles}/{this.cantVecesQueSePuedeUsar})";
        }


        public override object Clone()
        {
            Item cItem = new Comida(base.getNombre(), cantVidaDeCura, cantVecesQueSePuedeUsar);

            return cItem;
        }

    }
}
