using CarMix.Client.CarMixWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMix.Client.Menus
{
    static class MenuUsuarios
    {
        public static void Menu()
        {
            WebService_UserSoapClient service = new WebService_UserSoapClient();
            CarMixWebService.Security securityUser = new CarMixWebService.Security
            {

                Password = "admin",
                UserName = "admin"

            };
            Console.WriteLine("");
            Console.WriteLine("0-Salir");
            Console.WriteLine("1-Listar usuarios");
            Console.WriteLine("2-Detalles de un usuario");
            Console.WriteLine("3-Añadir un usuario");
            Console.WriteLine("4-Eliminar un usuario");
            Console.WriteLine("5-Editar un usuario");
            Console.WriteLine("6-Cambiar contraseña");
            Console.WriteLine("7-Eliminar invitado");
            Console.WriteLine("Selecciona una opción:");
            string seleccion = Console.ReadLine();
            Console.WriteLine("");
            try
            {
                switch (seleccion)
                {
                    case "0":
                        MenuInicio.Menu();
                        break;
                    case "1":
                        User[] users = service.Users(securityUser);
                        Console.WriteLine("ID-Nombre-Contraseña");
                        foreach (User u in users)
                        {
                            Console.WriteLine(u.Id + " " + u.Name + " " + u.Password + " ");
                        }
                        Menu();
                        break;
                    case "2":
                        Console.WriteLine("Introduzca el identificador del usuario (puede verlo en la lista de usuarios)");
                        int idUser = int.Parse(Console.ReadLine());
                        Console.WriteLine("");
                        User user = service.FindUser(securityUser, idUser);
                        Console.WriteLine("ID-Nombre-Contraseña");
                        Console.WriteLine(user.Id + " " + user.Name + " " + user.Password + " ");
                        Console.WriteLine("");
                        Console.WriteLine("Viajes creados:");
                        Console.WriteLine("ID-Origen-Destino-Precio-Plazas");
                        foreach (Viaje v in user.ViajesCreados)
                            Console.WriteLine(v.Id + " " + v.Origen + " " + v.Destino + " " + v.Precio + " " + v.Plazas);
                        Console.WriteLine("");
                        Console.WriteLine("Viajes como invitado:");
                        Console.WriteLine("ID-Origen-Destino-Precio-Plazas");
                        foreach (Viaje v in user.ViajesSuscrito)
                            Console.WriteLine(v.Id + " " + v.Origen + " " + v.Destino + " " + v.Precio + " " + v.Plazas);
                        Menu();
                        break;
                    case "3":
                        Console.WriteLine("Nombre:");
                        string nombre = Console.ReadLine();
                        Console.WriteLine("Password:");
                        string password = Console.ReadLine();
                        Console.WriteLine("Genero Músical:");
                        string generoMusical = Console.ReadLine();
                        service.AddUser(securityUser, nombre, password, generoMusical);
                        Console.WriteLine("");
                        Console.WriteLine("Usuario añadido correctamente");
                        Menu();
                        break;
                    case "4":
                        Console.WriteLine("Introduce el identificador del usuario (ten en cuenta que todos los viajes creados por este usuario seran eliminados)");
                        int idUserDelete = int.Parse(Console.ReadLine());
                        service.DeleteUser(securityUser, idUserDelete);
                        Console.WriteLine("Usuario eliminado correctamente");
                        Menu();
                        break;
                    case "5":
                        Console.WriteLine("Identificador del usuario:");
                        int idUserUpdate = int.Parse(Console.ReadLine());
                        Console.WriteLine("Nombre:");
                        string nNombre = Console.ReadLine();
                        Console.WriteLine("Password:");
                        string nPassword = Console.ReadLine();
                        Console.WriteLine("Genero Músical:");
                        string nGeneroMusical = Console.ReadLine();
                        service.UpdateUser(securityUser, idUserUpdate, nNombre, nPassword, nGeneroMusical);
                        Console.WriteLine("Usuario actualizado");
                        Menu();
                        break;
                    case "6":
                        Console.WriteLine("Identificador del usuario:");
                        int idUserPass = int.Parse(Console.ReadLine());
                        Console.WriteLine("Nueva contraseña:");
                        string nPass = Console.ReadLine();
                        service.ChangePassword(securityUser, idUserPass, nPass);
                        Console.WriteLine("Contraseña cambiada correctamente");
                        Menu();
                        break;
                    case "7":
                        Console.WriteLine("Eliminaras a un invitado suscrito a un viaje");
                        Console.WriteLine("Identificador del usuario:");
                        int idUserInvitado = int.Parse(Console.ReadLine());
                        Console.WriteLine("Identificador del viaje del que se quiere eliminar al invitado:");
                        int idViajeInvitado = int.Parse(Console.ReadLine());
                        service.DeleteInvitado(securityUser, idUserInvitado, idViajeInvitado);
                        Console.WriteLine("Usuario eliminado correctamente");
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
                else if (ex.Message.Contains("Ya existe un usuario con ese nombre"))
                {
                    Console.WriteLine("Ese nombre de usuario no esta disponible");
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
