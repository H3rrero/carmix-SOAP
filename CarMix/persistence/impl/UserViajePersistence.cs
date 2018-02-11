using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarMix.data;
using CarMix.model;
using MySql.Data.MySqlClient;

namespace CarMix.persistence.impl
{
    public class UserViajePersistence : IUserViajePersistence
    {
        public User CreadorViaje(long idViaje)
        {
            MySqlConnection conn = DBConect.Conect();
            User salida = new User();
            try
            {

                conn.Open();

                string sql = "SELECT * FROM user WHERE id = (SELECT FK_user_id FROM user_viaje WHERE FK_viaje_id="+idViaje+" AND role='creador')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User user = new User();
                    user.Name = (string)rdr[1];
                    user.Password = (string)rdr[2];
                    user.GeneroMusical = (string)rdr[3];
                    salida = user;
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return salida;
        }

        public bool DeleteUserViaje(int idViaje)
        {
            MySqlConnection conn = DBConect.Conect();
            try
            {

                conn.Open();

                string sql = "DELETE FROM user_viaje WHERE FK_viaje_id=" + idViaje;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
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

                string sql = "SELECT * FROM user WHERE id = (SELECT FK_user_id FROM user_viaje WHERE FK_viaje_id=" + idViaje + " AND role='invitado')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User user = new User();
                    user.Name = (string)rdr[1];
                    user.Password = (string)rdr[2];
                    user.GeneroMusical = (string)rdr[3];
                    salida.Add(user);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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

                string sql = "SELECT * FROM viaje WHERE id IN(SELECT FK_viaje_id FROM user_viaje WHERE FK_user_id="+idUser+ " AND role='creador')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Viaje viaje = new Viaje();
                    viaje.Origen = (string)rdr[1];
                    viaje.Destino = (string)rdr[2];
                    viaje.Plazas = (int)rdr[3];
                    viaje.Precio = (decimal)rdr[4];
                    viaje.Descripcion = (string)rdr[5];
                    salida.Add(viaje);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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

                string sql = "SELECT * FROM viaje WHERE id IN(SELECT FK_viaje_id FROM user_viaje WHERE FK_user_id=" + idUser + " AND role='invitado')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Viaje viaje = new Viaje();
                    viaje.Origen = (string)rdr[1];
                    viaje.Destino = (string)rdr[2];
                    viaje.Plazas = (int)rdr[3];
                    viaje.Precio = (decimal)rdr[4];
                    viaje.Descripcion = (string)rdr[5];
                    salida.Add(viaje);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return salida;
        }
    }
}