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
    public class UserPersistence : IUserPersistence
    {
        IUserViajePersistence db = new UserViajePersistence();
        IViajePersistence dbViaje = new ViajePersistence();

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
                    user.Apariciones = (long)rdr[1];
                    users.Add(user);
                }
                rdr.Close();
            }
            catch (Exception)
            {
                throw new GenericException();
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
            catch (Exception)
            {
                throw new GenericException();
            }

            conn.Close();
            return "usuario añadido con exito";
        }

        public string ChangePassword(long id,string newPassword)
        {
          MySqlConnection conn = DBConect.Conect();
            try
            {

                conn.Open();
                User(id);
                string sql = "UPDATE user SET password= '"+newPassword+"' WHERE id ="+id;
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
            return "contraseña actualizada con exito";
        }

        public string DeleteInvitado(long id, long idViaje)
        {
            MySqlConnection conn = DBConect.Conect();
            try
            {

                conn.Open();
                User(id);
                string sql = "DELETE FROM `user_viaje` WHERE role='invitado' AND FK_user_id = " + id + " AND FK_viaje_id = "+idViaje;
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
            return "Invitado eliminado con exito";
        }

        public string DeleteUser(long id)
        {
            MySqlConnection conn = DBConect.Conect();
            try
            {
                User user = User(id);
                conn.Open();
                User(id);
                string sql = "DELETE FROM `user` WHERE id ="+id;
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                if (db.DeleteUserViajeByUser(id))
                {
                    user.ViajesCreados.ForEach(x => dbViaje.DeleteViaje(x.Id));
                    cmd.ExecuteNonQuery();
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
            return "usuario eliminado con exito";
        }
       
        public string DeleteUserViajes(long id)
        {

            MySqlConnection conn = DBConect.Conect();
            try
            {

                conn.Open();
                User(id);
                string sql = "DELETE FROM user_viaje WHERE FK_user_id =" + id;
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
            return "usuario eliminado con exito";
        }

        public string EditUser(long id,string name, string password, string gustosMusicales)
        {
            MySqlConnection conn = DBConect.Conect();
            try
            {

                conn.Open();
                User(id);
                string sql = "UPDATE user SET user='"+name+"',generomusical='"+gustosMusicales+"', password='" + password + "' WHERE id =" + id;
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
            return "usuario actualizado con exito";
        }

        public User User(long id)
        {
            MySqlConnection conn = DBConect.Conect();
            User salida = null;
            try
            {

                conn.Open();

                string sql = "SELECT * from user WHERE id="+ id;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User user = new User();
                    user.Id = (long)rdr[0];
                    user.Name = (string)rdr[1];
                    user.Password = (string)rdr[2];
                    user.GeneroMusical = (string)rdr[3];
                    user.ViajesCreados = db.ViajesCreados((long)rdr[0]);
                    user.ViajesSuscrito = db.ViajesSuscrito((long)rdr[0]);
                    salida = user;
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
                    user.Id = (long)rdr[0];
                    user.Name = (string)rdr[1];
                    user.Password = (string)rdr[2];
                    user.GeneroMusical = (string)rdr[3];
                    user.ViajesCreados = db.ViajesCreados((long)rdr[0]);
                    user.ViajesSuscrito = db.ViajesSuscrito((long)rdr[0]);
                    users.Add(user);
                }
                rdr.Close();
            }
            catch (Exception)
            {
                throw new GenericException();
            }

            conn.Close();
            return users;
        }
    }
}