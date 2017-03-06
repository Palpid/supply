using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKSupply.Models;

namespace HKSupply.Services.Interfaces
{
    public interface IUser
    {
        IEnumerable<User> GetAllUsers();
        User GetUserByLoginPassword(string UserLogin, string Password);
        User NewUser(User newUser);
        bool DisableUser(string userId, string remarks);
        bool UpdateUsers(IEnumerable<User> usersToUpdate);
    }
}
