
using CarMix.Client.UserHttps;
using CarMix.Client.ViajeHttps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CarMix.Client.Menus
{
    class MenuEstadisticas
    {

        public static void Menu()
        {
            ServicePointManager.ServerCertificateValidationCallback +=
               (sender, certificate, chain, sslPolicyErrors) => true;
            var myBinding = new BasicHttpsBinding();
            var myEndpointAddress = new EndpointAddress("https://156.35.98.41:8443/CarMix/WebService.Viaje.asmx?WSDL");
            var myEndpointAddressUser = new EndpointAddress("https://156.35.98.41:8443/CarMix/WebService.User.asmx?WSDL");
            WebService_ViajeSoapClient viajesService = new WebService_ViajeSoapClient(myBinding, myEndpointAddress);
            WebService_UserSoapClient userService = new WebService_UserSoapClient(myBinding, myEndpointAddressUser);
            CarMix.Client.UserHttps.Security securityUser = new CarMix.Client.UserHttps.Security
            {

                Password = "admin",
                UserName = "admin"

            };
            CarMix.Client.ViajeHttps.Security securityViaje = new CarMix.Client.ViajeHttps.Security
            {

                Password = "admin",
                UserName = "admin"

            };

            Console.WriteLine("");
            Console.WriteLine("0-Salir");
            Console.WriteLine("1-Actividad de los usuarios");
            Console.WriteLine("2-Origenes populares");
            Console.WriteLine("3-Destinos populares");
            Console.WriteLine("Selecciona una opción:");
            string seleccion = Console.ReadLine();
            Console.WriteLine("");

            try {
                switch (seleccion) {
                    case "0":
                        MenuInicio.Menu();
                        break;
                    case "1":
                        UserActivity[] actividades = userService.UsersActivity(securityUser);
                        Console.WriteLine("Usuarios junto al numero de veces que han usado nuestros servicios:");
                        foreach (UserActivity v in actividades)
                        {
                            Console.WriteLine(v.Name + " " + v.Apariciones);
                        }
                        Menu();
                        break;
                    case "2":
                        LugaresPopulares[] origenes = viajesService.OrigenesPopulares(securityViaje);
                        Console.WriteLine("Origenes junto al numero de veces que han sido elegidos por los usuarios:");
                        foreach (LugaresPopulares o in origenes)
                        {
                            Console.WriteLine(o.Name+" "+o.Apariciones);
                        }
                        Menu();
                        break;
                    case "3":
                        LugaresPopulares[] destinos = viajesService.DestinosPopulares(securityViaje);
                        Console.WriteLine("Destinos junto al numero de veces que han sido elegidos por los usuarios:");
                        foreach (LugaresPopulares o in destinos)
                        {
                            Console.WriteLine(o.Name + " " + o.Apariciones);
                        }
                        Menu();
                        break;
                    default:
                        Menu();
                        break;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Se necesitan permisos"))
                {
                    Console.WriteLine("ERROR: Se necesita ser administrador");
                }
                else if (ex.Message.Contains("No se ha encontrado la entidad solicitada"))
                {
                    Console.WriteLine("No existe ninguna entidad con ese identificador introducido");
                }
                else if (ex.Message.Contains("Se produjo un error al procesar su petición"))
                {
                    Console.WriteLine("Se ha producido un error al procesar su peticion");
                }
                Menu();
            }
        }
        }
    }
