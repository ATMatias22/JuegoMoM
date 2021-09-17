using MoM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MoM
{
    public class Juego
    {

        List<Personaje> personajes;
        List<Item> itemsEnElJuego;
        Personaje personajeElegido;
        Random rnd;
        const int ITEMS_A_REGALAR =10;
        const int CANTIDAD_PERSONAJES_A_FABRICAR = 3;

        public Juego()
        {
            personajes = new List<Personaje>();
            itemsEnElJuego = new List<Item>();
            rnd = new Random();
        }

        public void iniciar()
        {
            agregarItems();
            agregarPersonajes();
            mostrarItemsDeTodosLosPersonajes();
            seleccionDePersonaje();
            colocarEnemigosParaElPersonaje();
            iniciandoJuegoParaElPersonajeElegido();
        }

        public void agregarPersonajes()
        {
            HashSet<Personaje> hsPersonajes = new HashSet<Personaje>(new PersonajeComparator());

            Console.WriteLine("------------------------Coloque el nombre de los jugadores---------------------------");
            for (int i = 0; i < CANTIDAD_PERSONAJES_A_FABRICAR; i++)
            {
                String nombre = Console.ReadLine();
                if (!hsPersonajes.Add(new Personaje(nombre, llenarInventarioDeUnJugadorConItemsAlAzar())))
                {
                    Console.WriteLine($"No se pudo agregar nombre: {nombre} porque ya existe");
                }
            }
            Console.WriteLine("-------------------------------------------------------------------------------------");
            personajes = hsPersonajes.ToList();
        }

        public void agregarItems()
        {
            itemsEnElJuego.Add(new Hacha("Hacha nivel 1", 30, 50));
            itemsEnElJuego.Add(new Espada("Espada nivel 1", 40));
            itemsEnElJuego.Add(new Arco("Arco nivel 1", 35));
            itemsEnElJuego.Add(new Hacha("Hacha nivel 2", 35, 55));
            itemsEnElJuego.Add(new Espada("Espada nivel 2", 45));
            itemsEnElJuego.Add(new Arco("Arco nivel 2", 40));
            itemsEnElJuego.Add(new Armadura("Armadura nivel 1", 15));
            itemsEnElJuego.Add(new Armadura("Armadura nivel 2", 20));
            itemsEnElJuego.Add(new Comida("Comida nivel 1", 35, 2));
            itemsEnElJuego.Add(new Comida("Comida nivel 2", 40, 2));
            itemsEnElJuego.Add(new Flecha("Flecha nivel 1", 30, 15));
        }

        public void seleccionDePersonaje()
        {
            int numeroElegido;
            Menu.lineaConMensaje("Coloque numero de personaje elegido");
            mostrarPersonajes();
            Menu.lineaConMensaje("Elija opcion");
            while (!Menu.esUnNumeroYestoyEntreLosNumerosDeLasOpciones(out numeroElegido, personajes.Count))
            {
                Console.WriteLine("Debe colocar alguna de las opciones");
                Menu.lineaConMensaje("Coloque numero de personaje elegido");
                mostrarPersonajes();
                Menu.lineaConMensaje("Elija opcion");
            }
            personajeElegido = personajes[numeroElegido];
            Menu.lineaConMensaje("Respuesta de la opcion elegida");
            personajeElegido.saludoYDescripcionDePersonaje();
        }

        private void mostrarPersonajes()
        {
            for (int i = 0; i < personajes.Count; i++)
            {
                Console.WriteLine($"{(i + 1)}- {personajes[i].getNombre()}");
            }
        }

        public void colocarEnemigosParaElPersonaje()
        {
            List<Personaje> enemigos = new List<Personaje>();
            foreach (Personaje personaje in personajes)
            {
                if (!personaje.Equals(personajeElegido))
                {
                    enemigos.Add(personaje);
                }
            }
            personajeElegido.insertarEnemigos(enemigos);
        }



        public void mostrarItemsDeTodosLosPersonajes()
        {
            for (int i = 0; i < personajes.Count; i++)
            {
                Console.WriteLine($"Inventario de : {personajes[i].getNombre()}");
                Console.WriteLine("-------------------------------------------------------------------------------------");
                personajes[i].mostrarInventarioDelPersonaje();
                Console.WriteLine("-------------------------------------------------------------------------------------");
            }
        }

        private List<Item> llenarInventarioDeUnJugadorConItemsAlAzar()
        {
            List<Item> itemsRegaladosPorEmpezar = new List<Item>();
            for (int i = 0; i < ITEMS_A_REGALAR; i++)
            {
                itemsRegaladosPorEmpezar.Add(obtenerUnItemAlAzar());
            }

            return itemsRegaladosPorEmpezar;
        }


        private Item obtenerUnItemAlAzar()
        {
            int numeroObtenido = rnd.Next(0, itemsEnElJuego.Count);
            return (Item)itemsEnElJuego[numeroObtenido].Clone();

        }


        public void iniciandoJuegoParaElPersonajeElegido()
        {
            personajeElegido.mostrarMenuDeOpciones();
        }

   

        //INNER CLASS
        private class PersonajeComparator : IEqualityComparer<Personaje>
        {
            public bool Equals(Personaje x, Personaje y)
            {
                return x.getNombre().Equals(y.getNombre());
            }

            public int GetHashCode(Personaje obj)
            {
                return obj.getNombre().GetHashCode();
            }
        }
    }
}
