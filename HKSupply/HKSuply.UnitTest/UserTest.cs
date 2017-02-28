using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HKSupply.Services.Implementations;
using HKSupply.Helpers.Mocking;
using HKSupply.Exceptions;
using HKSupply.Models;
using HKSupply.DB;
using HKSupply.Helpers;

namespace HKSuply.UnitTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void GetUser()
        {
            var userEF = new EFUser();

            try
            {
                var user = userEF.GetUserByLoginPassword(null, "xx"); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); //Es una tontería y siempre dará true, pero así no tengo el warnning
            }

            try
            {
                var user = userEF.GetUserByLoginPassword("XX", null); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException));
            }


            try
            {
                var user = userEF.GetUserByLoginPassword("XX", "XX"); 
            }
            catch (NonexistentUserException neuex)
            {
                Assert.IsTrue(neuex.GetType() == typeof(NonexistentUserException));
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var expectedValue = db.Users.FirstOrDefault(u => u.UserLogin.Equals("admin"));
                    var usuario = userEF.GetUserByLoginPassword("admin", "adminpwd"); //OK
                    Assert.AreEqual(true, usuario.Equals(expectedValue));
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [TestMethod]
        public void NewUser()
        {
            var userEF = new EFUser();

            try
            {
                userEF.NewUser(null);
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); //Es una tontería y siempre dará true, pero así no tengo el warnning
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.UserLogin.Equals("admin"));
                    userEF.NewUser(user); //NewExistingUserException
                }
                
            }
            catch (NewExistingUserException auxex)
            {
                Assert.IsTrue(auxex.GetType() == typeof(NewExistingUserException));
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

                var user = userEF.NewUser(newuser);
                Assert.AreEqual(true, user.Equals(newuser));
                               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void DisableUser()
        {
            var userEF = new EFUser();

            bool res = false;

            try
            {
                res = userEF.DisableUser(null, null);
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException));
            }

            try
            {
                res = userEF.DisableUser("xx", "xx");
            }
            catch (NonexistentUserException neuex)
            {
                Assert.IsTrue(neuex.GetType() == typeof(NonexistentUserException));
            }

            try
            {
                res = userEF.DisableUser("operator1", "Disable opertor 1");
                Assert.AreEqual(true, res);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #region v1 "ADO"
        /*
        [TestMethod]
        public void GetUser()
        {
            MockData.InitData();
            var usuarioADO = new ADOUser();

            try
            {
                var usuario = usuarioADO.GetUserByIdPassword(null, "xx"); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); //Es una tontería y siempre dará true, pero así no tengo el warnning
            }

            try
            {
                var usuario = usuarioADO.GetUserByIdPassword("XX", null); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); 
            }


            try
            {
                var usuario = usuarioADO.GetUserByIdPassword("XX", "XX"); //NoExisteUsuarioException
            }
            catch (NonexistentUserException neuex)
            {
                Assert.IsTrue(neuex.GetType() == typeof(NonexistentUserException)); 
            }

            try
            {

                var expectedValue = MockData.UsersList.FirstOrDefault(u => u.UserLogin.Equals("admin"));
                var usuario = usuarioADO.GetUserByIdPassword("admin", "adminpwd"); //OK
                Assert.AreEqual(true, usuario.Equals(expectedValue));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }

        [TestMethod]
        public void NewUser()
        {
            MockData.InitData();
            var usuarioADO = new ADOUser();

            try
            {
                usuarioADO.NewUser(null);
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); //Es una tontería y siempre dará true, pero así no tengo el warnning
            }

            try
            {
                var usuario = MockData.UsersList.FirstOrDefault(u => u.UserLogin.Equals("admin"));
                usuarioADO.NewUser(usuario); //AltaUsuarioExistenteException
            }
            catch (NewExistingUserException auxex)
            {
                Assert.IsTrue(auxex.GetType() == typeof(NewExistingUserException));
            }

            try
            {
                var rol = MockData.RolesList.FirstOrDefault(r => r.RoleId.Equals("OPERATOR"));
                var nuevoUsuario = new User
                {
                    UserLogin = "usuario.test",
                    Name = "Usuario de Test",
                    Password = "usutestpwd",
                    UserRol = rol,
                    Enabled = true,
                    LastLogout = null,
                    Remarks = "Observaciones usuario de test"
                };

                var usuario = usuarioADO.NewUser(nuevoUsuario);
                Assert.AreEqual(true, usuario.Equals(nuevoUsuario));
            }
            catch (Exception ex) 
            {
                throw ex; 
            }
        }
        
        [TestMethod]
        public void DisableUser()
        {
            MockData.InitData();
            var usuarioADO = new ADOUser();
            bool res = false;

            try
            {
                res = usuarioADO.DesactivarUsuario(null, null);
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException));
            }

            try
            {
                res = usuarioADO.DesactivarUsuario("xx", "xx");
            }
            catch (NonexistentUserException neuex)
            {
                Assert.IsTrue(neuex.GetType() == typeof(NonexistentUserException));
            }

            try
            {
                res = usuarioADO.DesactivarUsuario("operador1", "Desactivamos operador 1");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
         */
        #endregion
    }
}
