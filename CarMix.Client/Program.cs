using CarMix.Client.CarMixWebService;
using CarMix.excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMix.Client
{
    class Program
    {

        static void Main(string[] args)
        {
            Inicio();

        }

        public static void Inicio()
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
                    MenuUsuarios();
                    break;
                default:
                    break;
            }
        }

        public static void MenuUsuarios()
        {
            WebService_UserSoapClient service = new WebService_UserSoapClient();
            Console.WriteLine("");
            Console.WriteLine("0-Salir");
            Console.WriteLine("1-Listar usuarios");
            string seleccion = Console.ReadLine();
            Console.WriteLine("");
            try
            {
                switch (seleccion)
                {
                    case "0":
                        Inicio();
                        break;
                    case "1":
                        User[] users = service.Users(new CarMixWebService.Security
                        {

                            Password = "Password",
                            UserName = "fff"

                        });
                        foreach (User user in users)
                        {
                            Console.WriteLine(user.Name);
                        }
                        MenuUsuarios();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Se necesitan permisos"))
                {
                    Console.WriteLine("ERROR: Se necesita ser administrador");
                }
                Inicio();
            }
        }
    }
}