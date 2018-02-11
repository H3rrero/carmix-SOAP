using CarMix.model;
using CarMix.persistence;
using CarMix.persistence.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace CarMix
{
     [WebService(Namespace = "http://carmix.viaje.admin/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebService_Viaje : System.Web.Services.WebService
    {
        public Security Security { set; get; }

        IViajePersistence db = new ViajePersistence();

        public bool Check()
        {
            if (Security != null && Security.UserName.Equals("admin"))
            {
                return true;
            }
            return false;
        }

        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public List<Viaje> Viajes()
        {
            List<Viaje> viajes = new List<Viaje>();
            if (Check())
            {

                viajes = db.Viajes();
                return viajes;
            }
            return viajes;
        }
        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public string AddViaje(string origen, string destino, int plazas, decimal precio, string descripcion)
        {

            if (Check())
            {

                Viaje viaje = new Viaje
                {
                    Origen = origen,
                    Destino = destino,
                    Plazas = plazas,
                    Precio = precio,
                    Descripcion = descripcion
                };
                return db.AddViaje(viaje);
            }
            return "No tienes permisos de administrador";
        }

        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public string DeleteViaje(int id)
        {

            if (Check())
            {


                return db.DeleteViaje(id);
            }
            return "No tienes permisos de administrador";
        }

       

        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public string UpdateViaje(int id,string origen, string destino, int plazas, decimal precio, string descripcion)
        {

            if (Check())
            {


                return db.EditViaje(id,origen, destino, plazas, precio, descripcion);
            }
            return "No tienes permisos de administrador";
        }


        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public Viaje FindViaje(int id)
        {

            if (Check())
            {


                return db.FindViaje(id);
            }
            return new Viaje();
        }

    }

}
