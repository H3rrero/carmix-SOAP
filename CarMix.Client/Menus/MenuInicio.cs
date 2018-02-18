using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMix.Client.Menus
{
    static class MenuInicio
    {
        public static void Menu()
        {
            Console.WriteLine("");
            Console.WriteLine("0-Salir");
            Console.WriteLine("1-Gestionar usuarios");
            Console.WriteLine("2-Gestionar viajes");
            Console.WriteLine("3-Ver estadisticas");
            Console.WriteLine("Selecciona una opción:");
            string seleccion = Console.ReadLine();
            Console.WriteLine("");
            switch (seleccion)
            {
                case "0":

                    break;
                case "1":
                    MenuUsuarios.Menu();
                    break;
                case "2":
                    MenuViajes.Menu();
                    break;
                case "3":
                    MenuEstadisticas.Menu();
                    break;
                default:
                    Menu();
                    break;
            }
        }
    }
}
