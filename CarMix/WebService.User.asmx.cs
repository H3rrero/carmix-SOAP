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
     [WebService(Namespace = "http://carmix.admin/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
     public class WebService_User : System.Web.Services.WebService
    {

        public Security Security { set; get; }


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
            if (Security != null && Security.UserName.Equals("admin"))
            {
                UserPersistence db = new UserPersistence();
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
                UserPersistence db = new UserPersistence();
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
                UserPersistence db = new UserPersistence();

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
                UserPersistence db = new UserPersistence();

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
                UserPersistence db = new UserPersistence();

                return db.EditUser(id,name, password, gustosMusicales);
            }
            return "No tienes permisos de administrador";
        }

    }
}
