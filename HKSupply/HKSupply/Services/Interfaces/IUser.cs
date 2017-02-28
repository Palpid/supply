using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKSupply.Models;

namespace HKSupply.Services.Interfaces
{
    interface IUser
    {
        User GetUserByLoginPassword(string UserLogin, string Password);
        User NewUser(User newUser);
        bool DisableUser(string userId, string remarks);
    }
}
