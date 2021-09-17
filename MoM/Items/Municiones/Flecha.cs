using MoM.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoM
{
    public class Flecha : Item, IMunicion
    {

        int danioBase;
        int cantFlechas;
        int cantFlechasRestantes;

        public Flecha(String nombre, int danioBase, int cantFlechas) : base(nombre)
        {
            this.danioBase = danioBase;
            this.cantFlechas = cantFlechas;
            this.cantFlechasRestantes = cantFlechas;
        }


        public int getDanioBase()
        {
            return this.danioBase;
        }

        public int getCantFlechas()
        {
            return this.cantFlechas;
        }

        public int getCantFlechasRestantes()
        {
            return this.cantFlechasRestantes;
        }

        public void restarFlechas()
        {
            this.cantFlechasRestantes--;
        }


        public bool hayFlechas()
        {
            return this.cantFlechasRestantes > 0;
        }

        public override void equipar(Personaje p)
        {
            try
            {
                if (p.tengoArmaEquipada())
                {
                    p.getArma().equiparMunicion(this, p);
                }
                else
                {
                    Console.WriteLine("No hay arma a la cual se pueda equipar: " + this.descripcion());
                }

            }catch(YaTieneMunicionEquipadaException ytmee)
            {
                Console.WriteLine(ytmee.Message);
            }
            catch (EstaArmaNoPuedeColocarMunicionesException eanpcme)
            {
                Console.WriteLine(eanpcme.Message);
            }
        }

        public override String descripcion()
        {
            return base.descripcion() + $"({this.cantFlechasRestantes}/{this.cantFlechas})";
        }



        public override object Clone()
        {
            Item cItem = new Flecha(base.getNombre(), danioBase, cantFlechas);

            return cItem;
        }
    }
}
