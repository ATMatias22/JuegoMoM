using MoM.Excepciones;
using MoM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoM
{
    public class Arco : Arma
    {

        Flecha flechaEquipada;

        public Arco(String nombre, int danioBase) : base(nombre, danioBase)
        {
        }

        override
        public int getDanioTotal()
        {

            int danio = 0;
            if (hayFlecha())
            {
                if (flechaEquipada.hayFlechas())
                {
                    flechaEquipada.restarFlechas();

                    danio = (getDanioBase() + flechaEquipada.getDanioBase());
                }
            }
            return danio;
        }

        private bool hayFlecha()
        {
            return this.flechaEquipada != null;
        }

        public override void equipar(Personaje p)
        {
            int opcion;
            if (!hayFlecha())
            {
                Menu.lineaConMensaje("Desea Equipar una flecha en su arco?");
                Menu.mostrarMenuDeOpciones("1-Si\n2-No");
                Menu.lineaConMensaje("Elija opcion");
                while (!Menu.esUnNumeroYestoyEntreLosNumerosDeLasOpciones(out opcion, 1, 2))
                {
                    Console.WriteLine("Debe colocar alguna de las opciones");
                    Menu.lineaConMensaje("Desea Equipar una flecha en su arco?");
                    Menu.mostrarMenuDeOpciones("1-Si\n2-No");
                    Menu.lineaConMensaje("Elija opcion");
                }
                try
                {
                    if (opcion == 1)
                    {
                        this.equiparMunicion(seleccionDeFlecha(p),p);
                    }
                    else if (opcion == 2)
                    {
                        Console.WriteLine("No se equipo ninguna flecha");
                    }
                }
                catch (NoHayEstaMunicionEnElInventarioException nhemeeie)
                {
                    Console.WriteLine(nhemeeie.Message);
                }
                catch (YaTieneMunicionEquipadaException ytmee)
                {
                    Console.WriteLine(ytmee.Message);
                }
            }
            base.equipar(p);
        }




        private List<Flecha> buscarFlechasEnElInvetario(Personaje p)
        {
            List<Flecha> flechas = new List<Flecha>();
            List<Item> items = p.getInventario();

            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                if (item is Flecha)
                {
                    flechas.Add((Flecha)item);
                }
            }

            if (flechas.Count <= 0)
            {
                throw new NoHayEstaMunicionEnElInventarioException("No hay flechas en el inventario");
            }
            return flechas;
        }

        private void mostrarFlechas(List<Flecha> flechas)
        {

            for (int i = 0; i < flechas.Count; i++)
            {
                Console.WriteLine($"{(i + 1)}- {flechas[i].descripcion()}");
            }
        }


        public Flecha seleccionDeFlecha(Personaje p)
        {
            int opcionNumero;
            Flecha flechaElegida = null;
            try
            {
                List<Flecha> flechas = buscarFlechasEnElInvetario(p);
                Menu.lineaConMensaje("Cual flecha elije?, coloque el numero?");
                mostrarFlechas(flechas);
                Menu.lineaConMensaje("Elija opcion");
                while (!Menu.esUnNumeroYestoyEntreLosNumerosDeLasOpciones(out opcionNumero, flechas.Count))
                {
                    Console.WriteLine("Debe colocar alguna de las opciones");
                    Menu.lineaConMensaje("Cual flecha elije?, coloque el numero?");
                    mostrarFlechas(flechas);
                    Menu.lineaConMensaje("Elija opcion");
                }
                Menu.lineaConMensaje("Respuesta de la opcion elegida");
                Console.WriteLine(flechas[opcionNumero].getNombre());
                flechaElegida = flechas[opcionNumero];
            }
            catch (Exception ex)
            {
                throw new NoHayEstaMunicionEnElInventarioException(ex.Message);
            }

            return flechaElegida;
        }



        public override void equiparMunicion(IMunicion m, Personaje p)
        {
            if (hayFlecha())
            {
                throw new YaTieneMunicionEquipadaException("Este arco ya tiene flecha asignada");
            }

            this.flechaEquipada = (Flecha)m;
            p.eliminarItemDelInventario(this.flechaEquipada);
            Console.WriteLine($"Se acaba de equipar {this.flechaEquipada.getCantFlechas()} flechas: " + this.flechaEquipada.descripcion() + " al arco");
        }




        public override String descripcion()
        {
            return hayFlecha() ? $"{this.getNombre()} con flechas: {this.flechaEquipada.descripcion()}" : $"{this.getNombre()} sin flechas";
        }

        public override object Clone()
        {
            Item cItem = new Arco(base.getNombre(), base.getDanioBase());

            return cItem;
        }
    }


}
