using HKSupply.Exceptions;
using HKSupply.Helpers.Mocking;
using HKSupply.Models;
using HKSupply.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HKSupply.Services.Implementations
{
    public class ADOUser : IUser
    {
        public User GetUserByLoginPassword(string UserId, string Password)
        {
            try
            {
                if (UserId == null)
                    throw new ArgumentNullException("UsuarioId");
                if (Password == null)
                    throw new ArgumentNullException("Password");

                var usuario = MockData.UsersList.FirstOrDefault(u => u.UserLogin.Equals(UserId) && u.Password.Equals(Password));

                if (usuario == null)
                    throw new NonexistentUserException();

                return usuario;
            }
            catch (ArgumentNullException anex)
            {
                throw anex;
            }
            catch (NonexistentUserException neuex)
            {
                throw neuex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User NewUser(User nuevoUsuario)
        {
            try
            {
                if (nuevoUsuario == null)
                    throw new ArgumentNullException("nuevoUsuario");

                var usuario = MockData.UsersList.FirstOrDefault(u => u.UserLogin.Equals(nuevoUsuario.UserLogin));

                if (usuario != null)
                    throw new NewExistingUserException();

                MockData.UsersList.Add(nuevoUsuario);

                return GetUserByLoginPassword(nuevoUsuario.UserLogin, nuevoUsuario.Password);

            }
            catch (ArgumentNullException anex)
            {
                throw anex;
            }
            catch (NewExistingUserException aueex)
            {
                throw aueex;
            }
            catch (NonexistentUserException neuex)
            {
                throw neuex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DisableUser(string userId, string remarks)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException("userId");

                var usuario = MockData.UsersList.FirstOrDefault(u => u.UserLogin.Equals(userId));

                if (usuario == null)
                    throw new NonexistentUserException();

                usuario.Enabled = false;
                usuario.Remarks = remarks;

                return true;
            }
            catch (ArgumentNullException anex)
            {
                throw anex;
            }
            catch (NonexistentUserException neuex)
            {
                throw neuex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
