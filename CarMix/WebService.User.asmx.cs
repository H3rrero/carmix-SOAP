using CarMix.data;
using CarMix.excepciones;
using CarMix.model;
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
     [WebService(Namespace = "http://carmix.user.admin/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
     public class WebService_User : System.Web.Services.WebService
    {
        public  Security Security { set; get; }

        UserPersistence db = new UserPersistence();
        public  bool Check()
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
        public List<User> Users()
        {
            List<User> users = new List<User>();
            try
            {
                if (Check())
                {

                    users = db.Users();
                    return users;
                }
            }
            catch (UnCheckException ex) {
                throw ex;
            }
            catch (GenericException ex) {
                throw ex;
            }
            return users;
        }

        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public string AddUser(string name, string password, string generoMusical)
        {
            try
            {
                List<User> users = Users();
                users.ForEach(x=> { if (x.Name.Equals(name)) { throw new FindException("Ya existe un usuario con ese nombre"); } });
                if (Check())
                {
                    

                    User user = new User();
                    user.Name = name;
                    user.Password = password;
                    user.GeneroMusical = generoMusical;
                    return db.AddUser(user);
                }
            }
            catch (FindException ex)
            {
                throw ex;
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
        public string DeleteUser(int id)
        {
            try
            {
                if (Check())
                {


                    return db.DeleteUser(id);
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
        public string ChangePassword(int id, string password)
        {
            try
            {
                if (Check())
                {


                    return db.ChangePassword(id, password);
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
        public string UpdateUser(int id, string name, string password, string gustosMusicales)
        {
            try
            {
                List<User> users = Users();
                users.ForEach(x => { if (x.Name.Equals(name)) { throw new FindException("Ya existe un usuario con ese nombre"); } });
                if (Check())
                {


                    return db.EditUser(id, name, password, gustosMusicales);
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
        public User FindUser(int id)
        {
            try
            {
                if (Check())
                {


                    return db.User(id);
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
            return new User();
        }

        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public List<UserActivity> UsersActivity()
        {
            List<UserActivity> users = new List<UserActivity>();
            try
            {
                if (Check())
                {

                    users = db.ActivityUsers();
                    return users;
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
            return users;
        }

        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public string DeleteInvitado(long id, long idViaje)
        {
            try
            {
                if (Check())
                {
                    return db.DeleteInvitado(id, idViaje);
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
        public string DeleteViajesUser(int id)
        {
            try
            {
                if (Check())
                {
                    return db.DeleteUserViajes(id);
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
    }
}
