using CarMix.data;
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

        public Security Security { set; get; }
        UserPersistence db = new UserPersistence();

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
        public List<User> Users()
        {
            List<User> users = new List<User>();
            if (Check())
            {
               
                 users = db.Users();
                return users;
            }
            return users;
        }

        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public string AddUser(string name, string password, string generoMusical)
        {
           
            if (Check())
            {
              
                User user = new User();
                user.Name = name;
                user.Password = password;
                user.GeneroMusical = generoMusical;
                return db.AddUser(user);
            }
            return "No tienes permisos de administrador";
        }

        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public string DeleteUser(int id)
        {

            if (Check())
            {
                

                return db.DeleteUser(id);
            }
            return "No tienes permisos de administrador";
        }

        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public string ChangePassword(int id, string password)
        {

            if (Check())
            {
                

                return db.ChangePassword(id,password);
            }
            return "No tienes permisos de administrador";
        }

        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public string UpdateUser(int id, string name, string password, string gustosMusicales)
        {

            if (Check())
            {
               

                return db.EditUser(id,name, password, gustosMusicales);
            }
            return "No tienes permisos de administrador";
        }


        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public User FindUser(int id)
        {

            if (Check())
            {
               

                return db.User(id);
            }
            return new User();
        }

        [WebMethod]
        [SoapHeader("Security", Direction = SoapHeaderDirection.In)]
        public List<UserActivity> UsersActivity()
        {
            List<UserActivity> users = new List<UserActivity>();
            if (Check())
            {

                users = db.ActivityUsers();
                return users;
            }
            return users;
        }

    }
}
