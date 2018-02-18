using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMix.Client.Menus
{
    static class Credenciales
    {
        public static void CargarMenu() {
            Console.WriteLine("Introduce las credenciales de administrador");
            Console.WriteLine("Usuario:");
            string user = Console.ReadLine();
            Console.WriteLine("Contraseña:");
            string contrasena = Console.ReadLine();
            if (user.Equals("admin") && contrasena.Equals("admin"))
                MenuInicio.Menu();
            else
                CargarMenu();
        }
    }
}
