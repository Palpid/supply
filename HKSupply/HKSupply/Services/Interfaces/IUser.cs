using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKSupply.Models;

namespace HKSupply.Services.Interfaces
{
    /// <summary>
    /// Interface para el service de User
    /// </summary>
    public interface IUser
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int userId);
        User GetUserByLoginPassword(string UserLogin, string Password);
        User NewUser(User newUser);
        bool DisableUser(string userId, string remarks);
        bool ChangePassword(int userId, string password);
        bool UpdateUsers(IEnumerable<User> usersToUpdate);
    }
}
