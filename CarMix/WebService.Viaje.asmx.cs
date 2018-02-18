using CarMix.data;
using CarMix.excepciones;
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
            if (Security != null && Security.UserName.Equals("admin") && Security.Password.Equals("admin"))
            {
                return true;
            }
            else
            {
                throw new UnCheckException();
            }
        }

        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public List<Viaje> Viajes()
        {
            List<Viaje> viajes = new List<Viaje>();
            try
            {
                if (Check())
                {

                    viajes = db.Viajes();
                    return viajes;
                }
            }
            catch (UnCheckException ex)
            {
                throw ex;
            }
            catch (GenericException ex)
            {
                throw ex;
            }
            return viajes;
        }
        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public string AddViaje(long idCreador,string origen, string destino, int plazas, decimal precio, string descripcion)
        {
            try { 
            if (Check())
            {

                Viaje viaje = new Viaje
                {
                    Origen = origen,
                    Destino = destino,
                    Plazas = plazas,
                    Precio = precio,
                    Descripcion = descripcion,
                    Creador = new model.User { Id=idCreador, Name="user", Password="prueba", GeneroMusical="prueba"}
                };
                    
                return db.AddViaje(viaje);
            }
            }
            catch (UnCheckException ex)
            {
                throw ex;
            }
            catch (GenericException ex)
            {
                throw ex;
            }
            return "No tienes permisos de administrador";
        }

        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public string DeleteViaje(long id)
        {
            try { 
            if (Check())
            {


                return db.DeleteViaje(id);
            }
            }
            catch (UnCheckException ex)
            {
                throw ex;
            }
            catch (FindException)
            {
                throw new FindException();
            }
            catch (GenericException ex)
            {
                throw ex;
            }
            return "No tienes permisos de administrador";
        }

       

        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public string UpdateViaje(int id,string origen, string destino, int plazas, decimal precio, string descripcion)
        {
            try { 
            if (Check())
            {


                return db.EditViaje(id,origen, destino, plazas, precio, descripcion);
            }
            }
            catch (UnCheckException ex)
            {
                throw ex;
            }
            catch (FindException)
            {
                throw new FindException();
            }
            catch (GenericException ex)
            {
                throw ex;
            }
            return "No tienes permisos de administrador";
        }


        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public Viaje FindViaje(int id)
        {
            try { 
            if (Check())
            {


                return db.FindViaje(id);
            }
            }
            catch (UnCheckException ex)
            {
                throw ex;
            }
            catch (FindException)
            {
                throw new FindException();
            }
            catch (GenericException ex)
            {
                throw ex;
            }
            return new Viaje();
        }

        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public List<LugaresPopulares> DestinosPopulares()
        {
            List<LugaresPopulares> salida = new List<LugaresPopulares>();
            try { 
            if (Check())
            {

                salida = db.DestinosPopulares();
                return salida;
            }
            }
            catch (UnCheckException ex)
            {
                throw ex;
            }
            catch (GenericException ex)
            {
                throw ex;
            }
            return salida;
        }


        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public List<LugaresPopulares> OrigenesPopulares()
        {
            List<LugaresPopulares> salida = new List<LugaresPopulares>();
            try { 
            if (Check())
            {

                salida = db.OrigenesPopulares();
                return salida;
            }
            }
            catch (UnCheckException ex)
            {
                throw ex;
            }
            catch (GenericException ex)
            {
                throw ex;
            }
            return salida;
        }
    }

}
