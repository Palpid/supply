using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HKSupply.Services.Implementations;
using HKSupply.Helpers.Mocking;
using HKSupply.Exceptions;
using HKSupply.Models;
using HKSupply.DB;
using HKSupply.Helpers;
using HKSupply.General;

namespace HKSuply.UnitTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void GetAllUser()
        {
            try
            {
                var users = GlobalSetting.UserService.GetAllUsers();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void GetUser()
        {
            try
            {
                var user = GlobalSetting.UserService.GetUserByLoginPassword(null, "xx"); //ArgumentNullException
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                var user = GlobalSetting.UserService.GetUserByLoginPassword("XX", null); //ArgumentNullException
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                var user = GlobalSetting.UserService.GetUserByLoginPassword("XXXXXXX", "XX"); //NonexistentUserException
                Assert.Fail("Expected exception");
            }
            catch (NonexistentUserException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                var user = GlobalSetting.UserService.GetUserByLoginPassword("admin", "xx"); //InvalidPasswordException
                Assert.Fail("Expected exception");
            }
            catch (InvalidPasswordException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var expectedValue = db.Users.FirstOrDefault(u => u.UserLogin.Equals("admin"));
                    var usuario = GlobalSetting.UserService.GetUserByLoginPassword("admin", "adminpwd"); //OK
                    Assert.AreEqual(true, usuario.Equals(expectedValue));
                }
                
            }
            catch (Exception)
            {
                throw;
            }


        }

        [TestMethod]
        public void NewUser()
        {
            try
            {
                GlobalSetting.UserService.NewUser(null);
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true); 
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.UserLogin.Equals("admin"));
                    GlobalSetting.UserService.NewUser(user); //NewExistingUserException
                    Assert.Fail("Expected exception");
                }
                
            }
            catch (NewExistingUserException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                var newuser = new User
                {
                    UserLogin = "user.test",
                    Name = "Test User",
                    Password = PasswordHelper.GetHash("usutestpwd"),
                    RoleId = "OPERATOR",
                    Enabled = true,
                    LastLogout = null,
                    Remarks = "Test user remarks"
                };

                var user = GlobalSetting.UserService.NewUser(newuser);
                Assert.AreEqual(true, user.Equals(newuser));
                               
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void DisableUser()
        {
            bool res = false;

            try
            {
                res = GlobalSetting.UserService.DisableUser(null, null);
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                res = GlobalSetting.UserService.DisableUser("XXXXXXXXX", "remarks xx");
                Assert.Fail("Expected exception");
            }
            catch (NonexistentUserException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                res = GlobalSetting.UserService.DisableUser("operator1", "Disable opertor 1");
                Assert.AreEqual(true, res);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [TestMethod]
        public void ChangePassword()
        { 
            var userEF = new EFUser();
            bool res = false;

            try
            {
                res = userEF.ChangePassword(0, "XX"); //ArgumentNullException
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                res = userEF.ChangePassword(1, null); //ArgumentNullException
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                res = userEF.ChangePassword(-1, "xx"); //NonexistentUserException
                Assert.Fail("Expected exception");
            }
            catch (NonexistentUserException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                res = userEF.ChangePassword(1, PasswordHelper.GetHash("adminpwd")); //ok
                Assert.AreEqual(true, res);
            }
            catch (Exception)
            {
                throw;
            }
        
        }

        [TestMethod]
        public void UpdateUsers()
        {
            bool res = false;

            try
            {
                res = GlobalSetting.UserService.UpdateUsers(null); //ArgumentNullException
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                var users = GlobalSetting.UserService.GetAllUsers();
                users = users.Where(u => u.UserLogin.Contains("operator")).ToList();
                res = GlobalSetting.UserService.UpdateUsers(users); //OK
                Assert.AreEqual(true, res);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
