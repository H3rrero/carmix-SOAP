using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarMix.data;
using CarMix.model;
using MySql.Data.MySqlClient;

namespace CarMix.persistence.impl
{
    public class UserPersistence : IUserPersistence
    {
        IUserViajePersistence db = new UserViajePersistence();

        public List<UserActivity> ActivityUsers()
        {
            MySqlConnection conn = DBConect.Conect();
            List<UserActivity> users = new List<UserActivity>();
            try
            {

                conn.Open();

                string sql = "SELECT u.user, COUNT(FK_user_id) FROM user_viaje,user AS u WHERE u.id=FK_user_id GROUP BY FK_user_id ORDER BY COUNT(FK_user_id) DESC ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    UserActivity user = new UserActivity();
                    user.Name = (string)rdr[0];
                    user.apariciones = (long)rdr[1];
                    users.Add(user);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return users;
        }

        public string AddUser(User user)
        {
            MySqlConnection conn = DBConect.Conect();
            try
            {

                conn.Open();

                string sql = "INSERT INTO user (user, password, generomusical) VALUES ('"+user.Name+ "', '" + user.Password + "','" + user.GeneroMusical + "')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            conn.Close();
            return "usuario añadido con exito";
        }

        public string ChangePassword(int id,string newPassword)
        {
          MySqlConnection conn = DBConect.Conect();
            try
            {

                conn.Open();

                string sql = "UPDATE user SET password= '"+newPassword+"' WHERE id ="+id;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            conn.Close();
            return "contraseña actualizada con exito";
        }

        public string DeleteUser(int id)
        {
            MySqlConnection conn = DBConect.Conect();
            try
            {

                conn.Open();

                string sql = "DELETE FROM `user` WHERE id ="+id;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            conn.Close();
            return "usuario eliminado con exito";
        }

        public string EditUser(int id,string name, string password, string gustosMusicales)
        {
            MySqlConnection conn = DBConect.Conect();
            try
            {

                conn.Open();

                string sql = "UPDATE user SET user='"+name+"',generomusical='"+gustosMusicales+"', password='" + password + "' WHERE id =" + id;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            conn.Close();
            return "usuario actualizado con exito";
        }

        public User User(int id)
        {
            MySqlConnection conn = DBConect.Conect();
            User salida = new User();
            try
            {

                conn.Open();

                string sql = "SELECT * from user WHERE id="+ id;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User user = new User();
                    user.Name = (string)rdr[1];
                    user.Password = (string)rdr[2];
                    user.GeneroMusical = (string)rdr[3];
                    user.ViajesCreados = db.ViajesCreados((long)rdr[0]);
                    user.ViajesSuscrito = db.ViajesSuscrito((long)rdr[0]);
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

        public List<User> Users()
        {
            MySqlConnection conn = DBConect.Conect();
            List<User> users = new List<User>();
            try
            {

                conn.Open();

                string sql = "SELECT * from user";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User user = new User();
                    user.Name = (string)rdr[1];
                    user.Password = (string)rdr[2];
                    user.GeneroMusical = (string)rdr[3];
                    user.ViajesCreados = db.ViajesCreados((long)rdr[0]);
                    user.ViajesSuscrito = db.ViajesSuscrito((long)rdr[0]);
                    users.Add(user);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return users;
        }
    }
}