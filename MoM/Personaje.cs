using MoM.Excepciones;
using MoM.Helpers;
using System;
using System.Collections.Generic;

namespace MoM
{
    public class Personaje
    {

        String nombre;
        int vida;
        int vidaRestante;
        EstadoDeSalud estadoSalud;
        Arma arma;
        Armadura armadura;
        List<Item> inventario;
        List<Personaje> enemigos;
        const int VIDA_INICIAL_DEL_JUGADOR = 100;
        const int VIDA_TOTAL_DEL_JUGADOR = 100;

        public Personaje(String nombre, List<Item> itemsParaElInventario)
        {
            this.estadoSalud = EstadoDeSalud.VIVO;
            this.nombre = nombre;
            this.vida = VIDA_TOTAL_DEL_JUGADOR;
            this.vidaRestante = VIDA_INICIAL_DEL_JUGADOR;
            this.inventario = itemsParaElInventario;
            autoInsertarseItemsDelInventario();
            actualizarEstadoDeSalud(this);
        }

        public String getNombre()
        {
            return this.nombre;
        }

        public int getVida()
        {
            return this.vida;
        }

        public int getVidaRestante()
        {
            return this.vidaRestante;
        }

        public EstadoDeSalud getEstadoSalud()
        {
            return this.estadoSalud;
        }
        public void equiparArma(Arma arma)
        {
            this.arma = arma;
        }

        public void equiparArmadura(Armadura armadura)
        {
            this.armadura = armadura;
        }

        public Arma getArma()
        {
            return this.arma;
        }

        public Armadura getArmadura()
        {
            return this.armadura;
        }

        public bool tengoArmaEquipada()
        {
            return this.arma != null;
        }

        public bool tengoArmaduraEquipada()
        {
            return this.armadura != null;
        }



     


        //FUNCIONES QUE TIENEN Q VER CON EL INVENTARIO

        private int cantidadDeItemsEnInventario()
        {
            return this.inventario.Count;
        }

        public void agregarItemAlInventario(Item i)
        {
            inventario.Add(i);
        }

        public void eliminarItemDelInventario(Item i)
        {
            this.inventario.Remove(i);
        }


        public void equiparItemDelInventario(int posicionDelItemEnElInventario)
        {
            this.inventario[posicionDelItemEnElInventario].equipar(this);
        }

        public List<Item> getInventario()
        {
            return this.inventario;
        }

        //----------------------------------------------------------------------------





        // FUNCIONES QUE SE UTILIZAN UNA VES
        public void saludoYDescripcionDePersonaje()
        {
            Console.WriteLine($"Gracias por elegirme soy {this.nombre}, tengo {this.vida} de vida en total");
        }



        public void autoInsertarseItemsDelInventario()
        {
            List<Item> equipables = new List<Item>();

            int contador = 0;
            while ((!tengoArmaEquipada() || !tengoArmaduraEquipada()) && (contador < cantidadDeItemsEnInventario()))
            {
                Item item = this.inventario[contador];
                if (item is IEquipable)
                {
                    if (((IEquipable)item).sePudoAutoEquipar(this))
                    {
                        equipables.Add(item);
                    }
                }
                contador++;
            }
            eliminarUnaListaDeItems(equipables);
        }

        private void eliminarUnaListaDeItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                eliminarItemDelInventario(item);
            }
        }


        public void insertarEnemigos(List<Personaje> enemigos)
        {
            this.enemigos = enemigos;
        }


        //-----------------------------------------------------------------------





























        //-----------------FUNCIONES QUE TIENEN QUE VER CON BATALLA

        private int amortiguarDanio(int danio)
        {
            return tengoArmaduraEquipada() ? armadura.amortiguarDanio(danio) : danio;
        }


        public void curarse(int cura)
        {
            if (this.vidaRestante + cura > vida)
            {
                this.vidaRestante = vida;
            }
            else
            {
                this.vidaRestante += cura;
            }

            actualizarEstadoDeSalud(this);
        }

        public void atacarEnemigo(Personaje pEnemigo)
        {
            //obtengo el el daño total que recibe el enemigo con el daño actual del personaje - la armadura del enemigo
            int danioBrutoARealizar = this.calcularDanioDePersonajeSegunSuEstado();
            Console.WriteLine($"El danio normal de {this.getNombre()} es de: {danioBrutoARealizar}");
            int danioRealizadoAlEnemigo = pEnemigo.amortiguarDanio(danioBrutoARealizar);
            Console.WriteLine($"El danio total que recibe {pEnemigo.getNombre()} es de: {danioRealizadoAlEnemigo}");


            pEnemigo.cambiarEstadoDeSaludPorDanioRecibido(pEnemigo, danioRealizadoAlEnemigo);
            Console.WriteLine($"La vida actual de {pEnemigo.getNombre()} es de: {pEnemigo.getVidaRestante()}/{pEnemigo.getVida()} - Estado : {pEnemigo.getEstadoSalud()} ");
        }

        private bool sigueVivo()
        {
            return vidaRestante > 0;
        }

        private void cambiarEstadoDeSaludPorDanioRecibido(Personaje pEnemigo, int danioRecibido)
        {
            pEnemigo.vidaRestante = pEnemigo.vidaRestante - danioRecibido;
            if (pEnemigo.vidaRestante < 0)
            {
                pEnemigo.vidaRestante = 0;
            }
            actualizarEstadoDeSalud(pEnemigo);
        }


        private void actualizarEstadoDeSalud(Personaje personaje)
        {
            if (personaje.vidaRestante >= (personaje.vida / 2))
            {
                personaje.estadoSalud = EstadoDeSalud.VIVO;
            }
            else if (personaje.vidaRestante > 0 && personaje.vidaRestante < (personaje.vida / 2))
            {
                personaje.estadoSalud = EstadoDeSalud.HERIDO;
            }
            else if (personaje.vidaRestante <= 0)
            {
                personaje.estadoSalud = EstadoDeSalud.MUERTO;
            }

        }

        private int calcularDanioDePersonajeSegunSuEstado()
        {
            int danioCalculado = 0;
            if (tengoArmaEquipada())
            {
                if (this.estadoSalud.Equals(EstadoDeSalud.VIVO))
                {
                    danioCalculado = this.arma.getDanioTotal();
                }
                else if (this.estadoSalud.Equals(EstadoDeSalud.HERIDO))
                {
                    danioCalculado =(int)this.arma.getDanioTotal() / 2;
                }
            }

            return danioCalculado;
        }


        private void quienGano(Personaje enemigo)
        {

            Personaje ganador = null;

            if (this.sigueVivo())
            {
                Console.WriteLine($"{this.getNombre()} acabas de matar a : {enemigo.getNombre()}");
                ganador = this;
            }
            else
            {
                Console.WriteLine($"{enemigo.getNombre()} acabas de matar a : {this.getNombre()}");
                ganador = enemigo;
            }

            Menu.lineaConMensaje($"El Ganador es: {ganador.getNombre()}");
            

        }

        private void buscarPeleaConEnemigo(int i)
        {
            Personaje enemigo = enemigos[i];
            int contador = 0;
            if (enemigo.sigueVivo())
            {
                if (this.sigueVivo())
                {
                    Menu.lineaConMensaje("Presentacion de jugadores");
                    Menu.lineaConMensaje("Comienza la batalla");

                    while (enemigo.sigueVivo() && this.sigueVivo())
                    {
                        contador++;
                        Menu.lineaConMensaje($"Turno {contador}");
                        this.estadoActualDelPersonaje();
                        enemigo.estadoActualDelPersonaje();
                        Menu.lineaSinMensaje();

                        if (this.sigueVivo())
                        {
                            Console.WriteLine($"Es el turno de: {this.getNombre()}");
                            this.menuDeBatalla(enemigo);
                        }
                      

                        if (enemigo.sigueVivo())
                        {
                            Console.WriteLine($"Es el turno de: {enemigo.getNombre()}");
                            enemigo.menuDeBatalla(this);
                        }

                    }

                    quienGano(enemigo);


                    Console.WriteLine("Pelea terminada");

                }
                else
                {
                    Console.WriteLine("No se puede pelear estas muerto");
                }
            }
            else
            {
                Console.WriteLine("No se puede pelear con este enemigo, esta muerto");
            }

            Menu.lineaSinMensaje();
            this.mostrarMenuDeOpciones();

        }


        //-----------------------------------------------------------------------------



        // FUNCIONES QUE MUESTRAN EL MENU



        public void mostrarMenuDeOpciones()
        {
            int opcionNumero;
            String menuDeOpciones = "Que desea hacer?\n1-Buscar enemigos?\n2-Equipar algun item?\n3-Ver inventario?\n4-Ver tu estado actual?\n5-Desequipar item?\n0-Salir del programa?";
            do
            {
                Menu.lineaConMensaje("Menu principal");
                Menu.mostrarMenuDeOpciones(menuDeOpciones);
                Menu.lineaConMensaje("Elija opcion");

                while (!Menu.esUnNumeroYestoyEntreLosNumerosDeLasOpciones(out opcionNumero, 0, 5))
                {
                    Console.WriteLine("Debe colocar alguna de las opciones");
                    Menu.lineaConMensaje("Menu principal");
                    Menu.mostrarMenuDeOpciones(menuDeOpciones);
                    Menu.lineaConMensaje("Elija opcion");
                }
                opcionDeMenuElegida(opcionNumero);
            } while (opcionNumero != 0);
        }


        private void opcionDeMenuElegida(int opcion)
        {

            switch (opcion)
            {
                case 0:
                    break;
                case 1:
                    Menu.lineaConMensaje("Enemigos");
                    mostrarEnemigosParaPelear();
                    break;
                case 2:
                    Menu.lineaConMensaje("Que item desea equipar");
                    equiparItem();
                    break;
                case 3:
                    Menu.lineaConMensaje("Vista del inventario");
                    mostrarInventarioDelPersonaje();
                    break;
                case 4:
                    Menu.lineaConMensaje("Estado actual del personaje");
                    estadoActualDelPersonaje();
                    break;
                case 5:
                    Menu.lineaConMensaje("Desequipando");
                    desequiparItemsColocados();
                    break;
            }
        }


        //OPCION 1
        public void mostrarEnemigosParaPelear()
        {
            int opcionElegida;

            int cantidadDeEnemigos = this.enemigos.Count;
            if (cantidadDeEnemigos > 0)
            {
                Menu.lineaConMensaje("Con cual desea pelear?");
                mostrarEnemigos();
                Menu.lineaConMensaje("Elija opcion");
                while (!Menu.esUnNumeroYestoyEntreLosNumerosDeLasOpciones(out opcionElegida, cantidadDeEnemigos))
                {
                    Console.WriteLine("Debe colocar alguna de las opciones");
                    Menu.lineaConMensaje("Con cual desea pelear?");
                    mostrarEnemigos();
                    Menu.lineaConMensaje("Elija opcion");
                }

                buscarPeleaConEnemigo(opcionElegida);

            }
            else
            {
                Console.WriteLine("No hay enemigos");
            }

        }

        private void mostrarEnemigos()
        {
            for (int i = 0; i < this.enemigos.Count; i++)
            {
                Console.WriteLine($"{ (i + 1)}- {this.enemigos[i].getNombre()} ({this.enemigos[i].getVidaRestante()}/{this.enemigos[i].getVida()} - Estado : {this.enemigos[i].getEstadoSalud()})");
            }

        }

      
        private void menuDeBatalla(Personaje enemigo)
        {

            int opcionElegida;
            Menu.lineaConMensaje($"Que desea hacer {this.getNombre()}?");
            Console.WriteLine("1-Atacar\n2-Equipar");
            Menu.lineaConMensaje("Elija opcion");

            while (!Menu.esUnNumeroYestoyEntreLosNumerosDeLasOpciones(out opcionElegida, 1, 2))
            {
                Console.WriteLine("Debe colocar alguna de las opciones");
                Menu.lineaConMensaje($"Que desea hacer {this.getNombre()}?");
                Console.WriteLine("1-Atacar\n2-Equipar");
                Menu.lineaConMensaje("Elija opcion");
            }
            Menu.lineaSinMensaje();

            opcionesDeBatalla(opcionElegida, enemigo);

        }
        //-------------------------------------------------------------------------------
        private void opcionesDeBatalla(int opcion, Personaje enemigo)
        {
            switch (opcion)
            {
                case 1:
                    this.atacarEnemigo(enemigo);
                    break;
                case 2:
                    this.equiparItem();
                    break;
            }
        }


        //OPCION 2
        public void equiparItem()
        {
            int opcion;
            bool seCumple;
            do
            {
                Console.WriteLine("Que item desea equipar al personaje, elija el numero?");
                mostrarInventarioDelPersonaje();
                Console.WriteLine("Cualquier tecla que no este en las opciones para volver al menu principal");
                Menu.lineaConMensaje("Elija opcion");
                seCumple = Menu.esUnNumeroYestoyEntreLosNumerosDeLasOpciones(out opcion, this.inventario.Count);
                if (seCumple)
                {
                    this.equiparItemDelInventario(opcion);
                    Menu.lineaConMensaje("Que item desea equipar");
                }
            } while (seCumple);
        }


        //OPCION 3
        public void mostrarInventarioDelPersonaje()
        {
            if (cantidadDeItemsEnInventario() > 0)
            {
                for (int i = 0; i < inventario.Count; i++)
                {
                    Console.WriteLine($"{(i + 1)}- {inventario[i].descripcion()}");
                }
            }
            else
            {
                Console.WriteLine("No hay items en el inventario");
            }
        }


        //OPCION 4
        public void estadoActualDelPersonaje()
        {
            Console.WriteLine($"Soy {this.getNombre()} tengo ({this.vidaRestante}/{this.vida}) de vida, por lo tanto mi estado de vida es: {this.estadoSalud}" +
                            "\nArma = " + (tengoArmaEquipada() ? this.arma.descripcion() : "No hay nada equipado como arma") +
                            "\nArmadura = " + (tengoArmaduraEquipada() ? this.armadura.descripcion() : "No hay nada equipado como Armadura"));
        }


        // OPCION 5
        public void desequiparItemsColocados()
        {
            if (tengoArmaEquipada())
            {
                this.arma.desequiparse(this);
            }
            else
            {
                Console.WriteLine("No hay equipado ningun arma");
            }

            if (tengoArmaduraEquipada())
            {
                this.armadura.desequiparse(this);
            }
            else
            {
                Console.WriteLine("No hay equipado ninguna armadura");
            }
        }


        //--------------------------------------------------------------------------------------------------------------

        public enum EstadoDeSalud
        {
            HERIDO,
            VIVO,
            MUERTO
        }
    }
}
