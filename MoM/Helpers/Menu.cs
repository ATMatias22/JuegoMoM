using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoM.Helpers
{
    public sealed class Menu
    {
    
        public static void mostrarMenuDeOpciones(String opciones)
        {
            Console.WriteLine(opciones);
        }


        public static void lineaConMensaje(String mensaje)
        {
            Console.WriteLine($"-----------------------------------------{mensaje}--------------------------------------------");
        }

        public static void lineaSinMensaje()
        {
            Console.WriteLine($"----------------------------------------------------------------------------------------------");
        }


        //VERIFICA SI LA RESPUESTA SEGUN EL MENU QUE SE LE PASO, ES UN NUMERO
        public static bool esUnNumeroLaOpcionElegida(out int opcionNumero, bool laOpcionTrabajaSobreUnArray = false)
        {
            bool opcionValida;
            opcionValida = int.TryParse(Console.ReadLine(), out opcionNumero);
            return opcionValida;
        }

        //ESTE METODO TRABAJA CON OPCIONES QUE NO VAN DIRIGIDAS A UN ARRAY 
        private static bool estoyEntreLosNumerosDeLasOpciones(int opcionNumero, int  numeroMin, int numeroMax)
        {
            return (opcionNumero) >= numeroMin && opcionNumero <= numeroMax;
        }

        //ESTE METODO TRABAJA CON OPCIONES QUE NO VAN DIRIGIDAS A UN ARRAY 
        public static bool esUnNumeroYestoyEntreLosNumerosDeLasOpciones(out int opcionNumero, int numeroMin, int numeroMax)
        {
            bool esUnNumero = esUnNumeroLaOpcionElegida(out opcionNumero);
            bool estoyEntreLasOpciones = estoyEntreLosNumerosDeLasOpciones(opcionNumero,numeroMin, numeroMax);
            return esUnNumero && estoyEntreLasOpciones;
        }



        //ESTE METODO ME AYUDA A NO ACCEDER A UN INDEX NULL
        private static bool estoyEntreLosNumerosDeLasOpciones(int opcionNumero, int numeroMax)
        {
            return (opcionNumero) >= 0 && opcionNumero < numeroMax;
        }

        //ESTE METODO TRABAJA CON OPCIONES QUE ESTAN DIRIGDAS A UNA POSICION DE UNA RRAY
        public static bool esUnNumeroYestoyEntreLosNumerosDeLasOpciones(out int opcionNumero, int numeroMax)
        {
            bool esUnNumero = esUnNumeroLaOpcionElegida(out opcionNumero);
            opcionNumero--;
            bool estoyEntreLasOpciones = estoyEntreLosNumerosDeLasOpciones(opcionNumero, numeroMax);

            return esUnNumero && estoyEntreLasOpciones;
        }


    }
}
