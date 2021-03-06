﻿
using CarMix.Client.UserHttps;
using CarMix.Client.ViajeHttps;
using System;
using System.Net;
using System.ServiceModel;

namespace CarMix.Client.Menus
{
    static class MenuViajes
    {
        public static void Menu()
        {
            ServicePointManager.ServerCertificateValidationCallback +=
               (sender, certificate, chain, sslPolicyErrors) => true;
            var myBinding = new BasicHttpsBinding();
            var myEndpointAddress = new EndpointAddress("https://156.35.98.41:8443/CarMix/WebService.Viaje.asmx?WSDL");
            var myEndpointAddressUser = new EndpointAddress("https://156.35.98.41:8443/CarMix/WebService.User.asmx?WSDL");
            WebService_ViajeSoapClient service = new WebService_ViajeSoapClient(myBinding, myEndpointAddress);
            WebService_UserSoapClient userService = new WebService_UserSoapClient(myBinding, myEndpointAddressUser);
            CarMix.Client.ViajeHttps.Security securityViaje = new CarMix.Client.ViajeHttps.Security
            {

                Password = "admin",
                UserName = "admin"

            };
            CarMix.Client.UserHttps.Security securityUser = new CarMix.Client.UserHttps.Security
            {

                Password = "admin",
                UserName = "admin"

            };
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
                        CarMix.Client.ViajeHttps.Viaje[] viajes = service.Viajes(securityViaje);
                        Console.WriteLine("Viajes:");
                        Console.WriteLine("ID-Origen-Destino-Precio-Plazas-Descripción-Lista de spotify");
                        foreach (CarMix.Client.ViajeHttps.Viaje v in viajes)
                        {
                            Console.WriteLine(v.Id + " " + v.Origen + " " + v.Destino + " " +v.Precio + " " +v.Plazas + " " +v.Descripcion+" "+v.Lista);
                        }
                        Menu();
                        break;
                    case "2":
                        Console.WriteLine("Introduzca el identificador del viaje (puede verlo en la lista de viajes)");
                        int idViaje = int.Parse(Console.ReadLine());
                        Console.WriteLine("");
                        CarMix.Client.ViajeHttps.Viaje viaje = service.FindViaje(securityViaje, idViaje);
                        Console.WriteLine("Viaje:");
                        Console.WriteLine("ID-Origen-Destino-Precio-Plazas-Descripción-Lista de spotify");
                        Console.WriteLine(viaje.Id + " " + viaje.Origen + " " + viaje.Destino + " " + viaje.Precio + " " + viaje.Plazas + " " + viaje.Descripcion);
                        Console.WriteLine("");
                        Console.WriteLine("Creador:");
                        Console.WriteLine(viaje.Creador.Name);
                        Console.WriteLine("");
                        Console.WriteLine("Invitados:");
                        foreach (CarMix.Client.ViajeHttps.User u in viaje.Invitados)
                            Console.WriteLine(u.Name);
                        Menu();
                        break;
                    case "3":
                        Console.WriteLine("identificador del usuario creador del viaje:");
                        int idUserC = int.Parse(Console.ReadLine());
                        userService.FindUser(securityUser,idUserC);
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
                        service.AddViaje(securityViaje, idUserC, origen,destino,plazas,precio,descripcion);
                        Console.WriteLine("");
                        Console.WriteLine("Viaje añadido correctamente");
                        Menu();
                        break;
                    case "4":
                        Console.WriteLine("Introduce el identificador del viaje");
                        int idViajeDelete = int.Parse(Console.ReadLine());
                        service.DeleteViaje(securityViaje, idViajeDelete);
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
                        service.UpdateViaje(securityViaje, idViajeUpdate,nOrigen,nDestino,nPlazas,nPrecio,nDescripcion);
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
