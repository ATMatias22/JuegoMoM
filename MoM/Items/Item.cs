using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoM
{
    public abstract class Item : ICloneable
    {
        static int contadorItems = 0;
        int ID;
        String nombre;

        public Item(String nombre)
        {
            this.nombre = nombre;
            contadorItems++;
            this.ID = contadorItems;
        }


        public int getID()
        {
            return this.ID;
        }
        public String getNombre()
        {
            return this.nombre;
        }

        public virtual String descripcion()
        {
            return this.nombre;
        }

        //
        public void setNombre(String nombre)
        {
            this.nombre = nombre;
        }

        public abstract void equipar(Personaje p);

        public abstract object Clone();
        
    }
}
