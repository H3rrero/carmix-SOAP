using CarMix.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMix.persistence
{
    interface IUserPersistence
    {
        List<User> Users();

        string AddUser(User user);

        string DeleteUser(int id);

        string EditUser(int id,string name, string password, string gustosMusicales);

        string ChangePassword(int id,string newPassword);
    }
}
