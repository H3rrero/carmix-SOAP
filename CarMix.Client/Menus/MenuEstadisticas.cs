﻿using CarMix.Client.CarMixViajeService;
using CarMix.Client.CarMixWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMix.Client.Menus
{
    class MenuEstadisticas
    {

        public static void Menu()
        {
            WebService_ViajeSoapClient viajesService = new WebService_ViajeSoapClient();
            WebService_UserSoapClient userService = new WebService_UserSoapClient();

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
                        UserActivity[] actividades = userService.UsersActivity(new CarMixWebService.Security
                        {

                            Password = "Password",
                            UserName = "admin"

                        });
                        Console.WriteLine("Usuarios junto al numero de veces que han usado nuestros servicios:");
                        foreach (UserActivity v in actividades)
                        {
                            Console.WriteLine(v.Name + " " + v.Apariciones);
                        }
                        Menu();
                        break;
                    case "2":
                        LugaresPopulares[] origenes = viajesService.OrigenesPopulares(new CarMixViajeService.Security
                        {

                            Password = "Password",
                            UserName = "admin"

                        });
                        Console.WriteLine("Origenes junto al numero de veces que han sido elegidos por los usuarios:");
                        foreach (LugaresPopulares o in origenes)
                        {
                            Console.WriteLine(o.Name+" "+o.Apariciones);
                        }
                        Menu();
                        break;
                    case "3":
                        LugaresPopulares[] destinos = viajesService.DestinosPopulares(new CarMixViajeService.Security
                        {

                            Password = "Password",
                            UserName = "admin"

                        });
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
                MenuInicio.Menu();
            }
        }
        }
    }