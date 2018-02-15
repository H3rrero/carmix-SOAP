using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarMix.data;
using CarMix.excepciones;
using CarMix.model;
using MySql.Data.MySqlClient;

namespace CarMix.persistence.impl
{
    public class ViajePersistence : IViajePersistence
    {
        IUserViajePersistence db = new UserViajePersistence();
        public string AddViaje(Viaje viaje)
        {
            MySqlConnection conn = DBConect.Conect();
           
            try
            {

                conn.Open();

                string sql = "INSERT INTO viaje (origen, destino, plazas, precio, descripcion) VALUES ('" + viaje.Origen + "', '" + viaje.Destino + "','" + viaje.Plazas + "','" + viaje.Precio + "','" + viaje.Descripcion + "')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new GenericException();
            }

            conn.Close();
            return "viaje añadido con exito";
        }

        public string DeleteViaje(long id)
        {
            MySqlConnection conn = DBConect.Conect();
            try
            {

                conn.Open();
                FindViaje(id);
                string sql = "DELETE FROM viaje WHERE id =" + id;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                if (db.DeleteUserViaje(id))
                {
                    cmd.ExecuteNonQuery();
                }
                else {
                    return "no se pudo eliminar el viaje";
                }
            }
            catch (FindException)
            {
                throw new FindException();
            }
            catch (Exception)
            {
                throw new GenericException();
            }

            conn.Close();
            return "viaje eliminado con exito";
        }

        public List<string> DestinosPopulares()
        {
            MySqlConnection conn = DBConect.Conect();
            List<string> salida = new List<string>();
            try
            {

                conn.Open();

                string sql = "SELECT destino, COUNT(destino) FROM viaje GROUP BY destino ORDER BY COUNT(destino) DESC";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    salida.Add((string)rdr[0]);
                }
                rdr.Close();
            }
            catch (Exception)
            {
                throw new GenericException();
            }

            conn.Close();
            return salida;
        }

        public string EditViaje(long id, string origen, string destino, int plazas, decimal precio, string descripcion)
        {
            MySqlConnection conn = DBConect.Conect();
            try
            {

                conn.Open();
                FindViaje(id);
                string sql = "UPDATE viaje SET origen='" + origen + "',destino='" + destino + "', plazas='" + plazas + "', precio='" + precio + "', descripcion='" + descripcion + "' WHERE id =" + id;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (FindException)
            {
                throw new FindException();
            }
            catch (Exception)
            {
                throw new GenericException();
            }

            conn.Close();
            return "viaje actualizado con exito";
        }

        public Viaje FindViaje(long id)
        {
            MySqlConnection conn = DBConect.Conect();
            Viaje salida = null;
            try
            {

                conn.Open();

                string sql = "SELECT * from viaje WHERE id=" + id;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Viaje viaje = new Viaje();
                    viaje.Id = (long)rdr[0];
                    viaje.Origen = (string)rdr[1];
                    viaje.Destino = (string)rdr[2];
                    viaje.Plazas = (int)rdr[3];
                    viaje.Precio = (decimal)rdr[4];
                    viaje.Descripcion = (string)rdr[5];
                    viaje.Creador = db.CreadorViaje((long)rdr[0]);
                    viaje.Invitados = db.InvitadosViaje((long)rdr[0]);
                    salida = viaje;
                }
                rdr.Close();
                if (salida == null)
                    throw new FindException();
            }
            catch (FindException)
            {
                throw new FindException();
            }
            catch (Exception)
            {
                throw new GenericException();
            }

            conn.Close();
            return salida;
        }

        public List<string> OrigenesPopulares()
        {
            MySqlConnection conn = DBConect.Conect();
            List<string> salida = new List<string>();
            try
            {

                conn.Open();

                string sql = "SELECT origen, COUNT(origen) FROM viaje GROUP BY origen ORDER BY COUNT(origen) DESC";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                   
                    salida.Add((string)rdr[0]);
                }
                rdr.Close();
            }
            catch (Exception)
            {
                throw new GenericException();
            }

            conn.Close();
            return salida;
        }

        public List<Viaje> Viajes()
        {
            MySqlConnection conn = DBConect.Conect();
            List<Viaje> viajes = new List<Viaje>();
            try
            {

                conn.Open();

                string sql = "SELECT * from viaje";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Viaje viaje = new Viaje();
                    viaje.Id = (long)rdr[0];
                    viaje.Origen = (string)rdr[1];
                    viaje.Destino = (string)rdr[2];
                    viaje.Plazas = (int)rdr[3];
                    viaje.Precio = (decimal)rdr[4];
                    viaje.Descripcion = (string)rdr[5];
                    viaje.Creador = db.CreadorViaje((long)rdr[0]);
                    viaje.Invitados = db.InvitadosViaje((long)rdr[0]);
                    viajes.Add(viaje);
                }
                rdr.Close();
            }
            catch (Exception)
            {
                throw new GenericException();
            }

            conn.Close();
            return viajes;
        }
    }
}