using CarMix.Client.CarMixViajeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMix.Client.Menus
{
    static class MenuViajes
    {
        public static void Menu()
        {
            WebService_ViajeSoapClient service = new WebService_ViajeSoapClient();
            Console.WriteLine("");
            Console.WriteLine("0-Salir");
            Console.WriteLine("1-Listar viajes");
            Console.WriteLine("2-Detalles de un viaje");
            Console.WriteLine("3-Añadir un viaje");
            Console.WriteLine("4-Eliminar un viaje");
            Console.WriteLine("5-Editar un viaje");
            Console.WriteLine("Selecciona una opción:");
            string seleccion = Console.ReadLine();
            Console.WriteLine("");

            try
            {
                switch (seleccion) {
                    case "0":
                        MenuInicio.Menu();
                        break;
                    case "1":
                        Viaje[] viajes = service.Viajes(new CarMixViajeService.Security
                        {

                            Password = "Password",
                            UserName = "admin"

                        });
                        Console.WriteLine("Viajes:");
                        foreach (Viaje v in viajes)
                        {
                            Console.WriteLine(v.Id + " " + v.Origen + " " + v.Destino + " " +v.Precio + " " +v.Plazas + " " +v.Descripcion);
                        }
                        Menu();
                        break;
                    case "2":
                        Console.WriteLine("Introduzca el identificador del viaje (puede verlo en la lista de viajes)");
                        int idViaje = int.Parse(Console.ReadLine());
                        Console.WriteLine("");
                        Viaje viaje = service.FindViaje(new CarMixViajeService.Security
                        {

                            Password = "Password",
                            UserName = "admin"

                        }, idViaje);
                        Console.WriteLine("Viajes:");
                        Console.WriteLine(viaje.Id + " " + viaje.Origen + " " + viaje.Destino + " " + viaje.Precio + " " + viaje.Plazas + " " + viaje.Descripcion);
                        Console.WriteLine("");
                        Console.WriteLine("Creador:");
                        Console.WriteLine(viaje.Creador.Name);
                        Console.WriteLine("");
                        Console.WriteLine("Invitados:");
                        foreach (User u in viaje.Invitados)
                            Console.WriteLine(u.Name);
                        Menu();
                        break;
                    case "3":
                        Console.WriteLine("Origen:");
                        string origen = Console.ReadLine();
                        Console.WriteLine("Destino:");
                        string destino = Console.ReadLine();
                        Console.WriteLine("Plazas:");
                        int plazas = int.Parse(Console.ReadLine());
                        Console.WriteLine("Precio:");
                        decimal precio =decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Descripción:");
                        string descripcion = Console.ReadLine();
                        service.AddViaje(new CarMixViajeService.Security
                        {

                            Password = "Password",
                            UserName = "admin"

                        },origen,destino,plazas,precio,descripcion);
                        Console.WriteLine("");
                        Console.WriteLine("Viaje añadido correctamente");
                        Menu();
                        break;
                    case "4":
                        Console.WriteLine("Introduce el identificador del viaje");
                        int idViajeDelete = int.Parse(Console.ReadLine());
                        service.DeleteViaje(new CarMixViajeService.Security
                        {

                            Password = "Password",
                            UserName = "admin"

                        }, idViajeDelete);
                        Console.WriteLine("Viaje eliminado correctamente");
                        Menu();
                        break;
                    case "5":
                        Console.WriteLine("Identificador del viaje:");
                        int idViajeUpdate = int.Parse(Console.ReadLine());
                        Console.WriteLine("Origen:");
                        string nOrigen = Console.ReadLine();
                        Console.WriteLine("Destino:");
                        string nDestino = Console.ReadLine();
                        Console.WriteLine("Plazas:");
                        int nPlazas = int.Parse(Console.ReadLine());
                        Console.WriteLine("Precio:");
                        decimal nPrecio = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Descripción:");
                        string nDescripcion = Console.ReadLine();
                        service.UpdateViaje(new CarMixViajeService.Security
                        {

                            Password = "Password",
                            UserName = "admin"

                        }, idViajeUpdate,nOrigen,nDestino,nPlazas,nPrecio,nDescripcion);
                        Console.WriteLine("Viaje actualizado");
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
                MenuInicio.Menu();
            }
        }
    }
}
