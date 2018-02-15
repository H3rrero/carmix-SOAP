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

        User User(long id);

        string AddUser(User user);

        string DeleteUser(long id);

        string EditUser(long id,string name, string password, string gustosMusicales);

        string ChangePassword(long id,string newPassword);

        List<UserActivity> ActivityUsers();

        string DeleteInvitado(long id);

        string DeleteUserViajes(long id);
    }
}
