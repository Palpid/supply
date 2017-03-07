using HKSupply.DB;
using HKSupply.Exceptions;
using HKSupply.Helpers;
using HKSupply.Models;
using HKSupply.Services.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Services.Implementations
{
    public class EFUser : IUser
    {

        ILog _log = LogManager.GetLogger(typeof(EFUser));

        
        #region Public Methods
        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                using (var db = new HKSupplyContext())
                {
                    return db.Users.Include(r => r.UserRole).ToList();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                if (userId == 0)
                    throw new ArgumentNullException("userId");

                using (var db = new HKSupplyContext())
                {
                    var user = db.Users
                        .Include(r => r.UserRole)
                        .Where(u => u.Id.Equals(userId))
                        .FirstOrDefault();

                    if (user == null)
                        throw new NonexistentUserException();

                    return user;
                }
            }
            catch (ArgumentNullException anex)
            {
                _log.Error(anex.Message, anex);
                throw anex;
            }
            catch (NonexistentUserException neuex)
            {
                _log.Info(neuex.Message, neuex);
                throw neuex;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public User GetUserByLoginPassword(string UserLogin, string Password)
        {
            
            try
            {
                if (UserLogin == null)
                    throw new ArgumentNullException("UserId");
                if (Password == null)
                    throw new ArgumentNullException("Password");

                using (var db = new HKSupplyContext())
                {
                    var user = db.Users
                        .Include(r => r.UserRole)
                        .Where(u => u.UserLogin.Equals(UserLogin) && u.Enabled.Equals(true))
                        .FirstOrDefault();

                    if (user == null)
                        throw new NonexistentUserException();

                    if (PasswordHelper.ValidatePass(Password, user.Password) == false)
                        throw new InvalidPasswordException();

                    return user;
                }
                
            }
            catch (ArgumentNullException anex)
            {
                _log.Error(anex.Message, anex);
                throw anex;
            }
            catch (NonexistentUserException neuex)
            {
                _log.Info(neuex.Message, neuex);
                throw neuex;
            }
            catch(InvalidPasswordException ipex)
            {
                _log.Info(ipex.Message, ipex);
                throw ipex;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public User NewUser(User newUser)
        {
            try
            {
                if (newUser == null)
                    throw new ArgumentNullException("newUser");

                using (var db = new HKSupplyContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.UserLogin.Equals(newUser.UserLogin));

                    if (user != null)
                        throw new NewExistingUserException();

                    db.Users.Add(newUser);
                    db.SaveChanges();

                    //return GetUserByIdPassword(newUser.UserLogin, newUser.Password);
                    return newUser;
                }
                

            }
            catch (ArgumentNullException anex)
            {
                _log.Error(anex.Message, anex);
                throw anex;
            }
            catch (NewExistingUserException aueex)
            {
                _log.Error(aueex.Message, aueex);
                throw aueex;
            }
            catch (NonexistentUserException neuex)
            {
                _log.Error(neuex.Message, neuex);
                throw neuex;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    _log.Error("Entity of type \"" + eve.Entry.Entity.GetType().Name +"\" in state \"" + eve.Entry.State + "\" has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        _log.Error("- Property: \"" + ve.PropertyName + "\", Error: \"" + ve.ErrorMessage + "\"");
                    }
                }
                throw e;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public bool DisableUser(string userId, string remarks)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException("userId");

                using (var db = new HKSupplyContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.UserLogin.Equals(userId));

                    if (user== null)
                        throw new NonexistentUserException();

                    user.Enabled = false;
                    user.Remarks = remarks;
                    db.SaveChanges();

                    return true;
                }
                
            }
            catch (ArgumentNullException anex)
            {
                _log.Error(anex.Message, anex);
                throw anex;
            }
            catch (NonexistentUserException neuex)
            {
                _log.Error(neuex.Message, neuex);
                throw neuex;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }
        #endregion

        public bool ChangePassword(int userId, string password)
        {
            try
            {
                if (userId == 0)
                    throw new ArgumentNullException("userId");

                using (var db = new HKSupplyContext())
                {
                    var user = db.Users
                        .Include(r => r.UserRole)
                        .Where(u => u.Id.Equals(userId))
                        .FirstOrDefault();

                    if (user == null)
                        throw new NonexistentUserException();

                    user.Password = password;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (ArgumentNullException anex)
            {
                _log.Error(anex.Message, anex);
                throw anex;
            }
        }


        public bool UpdateUsers(IEnumerable<User> usersToUpdate)
        {
            try
            {
                if (usersToUpdate == null)
                    throw new ArgumentException("usersToUpdate");

                using (var db = new HKSupplyContext())
                {
                    foreach (var user in usersToUpdate)
                    {
                        var userToUpdate = db.Users.FirstOrDefault(u => u.Id.Equals(user.Id));
                        if (userToUpdate != null)
                        {
                            //userToUpdate.Password = role.Description;
                            userToUpdate.Name = user.Name;
                            userToUpdate.RoleId = user.RoleId;
                            userToUpdate.Enabled = user.Enabled;
                            userToUpdate.Remarks = user.Remarks;
                        }
                    }

                    db.SaveChanges();
                    return true;
                }

            }
            catch (ArgumentNullException nrex)
            {
                _log.Error(nrex.Message, nrex);
                throw nrex;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
        }

    }
}
