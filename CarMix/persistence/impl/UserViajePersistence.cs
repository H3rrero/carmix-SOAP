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
    public class UserViajePersistence : IUserViajePersistence
    {
        
        public bool AddUserViaje(long idUser, long idViaje, string rol)
        {
            MySqlConnection conn = DBConect.Conect();
            try
            {

                conn.Open();
                string sql = "INSERT INTO user_viaje(FK_user_id, FK_viaje_id, role) VALUES ('" + idUser + "', '" + idViaje + "','" + rol + "')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (FindException)
            {
                throw new FindException();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new GenericException();
            }

            conn.Close();
            return true;
        }

        public User CreadorViaje(long idViaje)
        {
            MySqlConnection conn = DBConect.Conect();
            User salida = new User();
            try
            {

                conn.Open();

                string sql = "SELECT * FROM user WHERE id = (SELECT FK_user_id FROM user_viaje WHERE FK_viaje_id="+idViaje+" AND role='CREADOR')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User user = new User();
                    user.Id = (long)rdr[0];
                    user.Name = (string)rdr[1];
                    user.Password = (string)rdr[2];
                    user.GeneroMusical = (string)rdr[3];
                    salida = user;
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

        public bool DeleteUserViaje(long idViaje)
        {
            MySqlConnection conn = DBConect.Conect();
            try
            {

                conn.Open();

                string sql = "DELETE FROM user_viaje WHERE FK_viaje_id=" + idViaje;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new GenericException();
            }

            conn.Close();
            return true;
        }

        public bool DeleteUserViajeByUser(long idUser)
        {
            MySqlConnection conn = DBConect.Conect();
            try
            {

                conn.Open();

                string sql = "DELETE FROM user_viaje WHERE FK_user_id =" + idUser;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new GenericException();
            }

            conn.Close();
            return true;
        }

        public List<User> InvitadosViaje(long idViaje)
        {
            MySqlConnection conn = DBConect.Conect();
            List<User> salida = new List<User>();
            try
            {

                conn.Open();

                string sql = "SELECT * FROM user WHERE id IN (SELECT FK_user_id FROM user_viaje WHERE FK_viaje_id=" + idViaje + " AND role='INVITADO')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User user = new User();
                    user.Id = (long)rdr[0];
                    user.Name = (string)rdr[1];
                    user.Password = (string)rdr[2];
                    user.GeneroMusical = (string)rdr[3];
                    salida.Add(user);
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
  
        public List<Viaje> ViajesCreados(long idUser)
        {
            MySqlConnection conn = DBConect.Conect();
            List<Viaje> salida = new List<Viaje>();
            try
            {

                conn.Open();

                string sql = "SELECT * FROM viaje WHERE id IN(SELECT FK_viaje_id FROM user_viaje WHERE FK_user_id="+idUser+ " AND role='CREADOR')";
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
                    salida.Add(viaje);
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

        public List<Viaje> ViajesSuscrito(long idUser)
        {
            MySqlConnection conn = DBConect.Conect();
            List<Viaje> salida = new List<Viaje>();
            try
            {

                conn.Open();

                string sql = "SELECT * FROM viaje WHERE id IN(SELECT FK_viaje_id FROM user_viaje WHERE FK_user_id=" + idUser + " AND role='INVITADO')";
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
                    salida.Add(viaje);
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
    }
}