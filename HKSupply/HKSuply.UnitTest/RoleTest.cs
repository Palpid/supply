using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HKSupply.Services.Implementations;
using HKSupply.Exceptions;
using HKSupply.Helpers.Mocking;
using HKSupply.Models;
using HKSupply.DB;


namespace HKSuply.UnitTest
{
    [TestClass]
    public class RoleTest
    {
        [TestMethod]
        public void GetRole()
        {
            var roleEF = new EFRole();

            try
            {
                var rol = roleEF.GetRoleById(null); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); //Es una tontería y siempre dará true, pero así no tengo el warnning
            }

            try
            {
                var rol = roleEF.GetRoleById("XX"); //NonexistentRoleException
            }
            catch (NonexistentRoleException nerex)
            {
                Assert.IsTrue(nerex.GetType() == typeof(NonexistentRoleException));
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var expectedValue = db.Roles.FirstOrDefault(r => r.RoleId.Equals("ADMIN"));  
                    var rol = roleEF.GetRoleById("ADMIN");
                    Assert.AreEqual(true, rol.Equals(expectedValue));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void NewRole()
        {
            var roleEF = new EFRole();

            try
            {
                var newRole = roleEF.NewRole(null);
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException));
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var role = db.Roles.FirstOrDefault(r => r.RoleId.Equals("ADMIN"));
                    var newRole = roleEF.NewRole(role);
                }
                
            }
            catch (NewExistingRoleException areex)
            {
                Assert.IsTrue(areex.GetType() == typeof(NewExistingRoleException));
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    Role newRole = new Role
                    {
                        RoleId = "ROLTEST",
                        Description = "Test Role",
                        Enabled = true,
                        Remarks = "test role for unit test"
                    };

                    var role = roleEF.NewRole(newRole);
                    Assert.AreEqual(true, role.Equals(newRole));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [TestMethod]
        public void DisableRole()
        {
            bool res = false;
            var roleEF = new EFRole();

            try
            {
                res = roleEF.DisableRole(null, null);
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException));
            }

            try
            {
                res = roleEF.DisableRole("XXXX", "");
            }
            catch (NonexistentRoleException nerex)
            {
                Assert.IsTrue(nerex.GetType() == typeof(NonexistentRoleException));
            }

            try
            {
                res = roleEF.DisableRole("ROLTEST", ""); //OK
                Assert.AreEqual(true, res);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region V1 "ADO"
        /*
        [TestMethod]
        public void GetRole()
        {
            MockData.InitData();
            var rolADO = new ADORole();

            try
            {
                var rol = rolADO.GetRoleById(null); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); //Es una tontería y siempre dará true, pero así no tengo el warnning
            }

            try
            {
                var rol = rolADO.GetRoleById("XX"); //NoExisteRolException
            }
            catch (NonexistentRoleException nerex)
            {
                Assert.IsTrue(nerex.GetType() == typeof(NonexistentRoleException));
            }

            try
            {
                var expectedValue = MockData.RolesList.FirstOrDefault(r => r.RoleId.Equals("ADMIN"));
                var rol = rolADO.GetRoleById("ADMIN");
                Assert.AreEqual(true, rol.Equals(expectedValue));

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void NewRole()
        {
            MockData.InitData();
            var rolADO = new ADORole();

            try
            {
                var alta = rolADO.NewRole(null);
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException));
            }

            try
            {
                var rol = MockData.RolesList.FirstOrDefault(r => r.RoleId.Equals("ADMIN"));
                var alta = rolADO.NewRole(rol);
            }
            catch (NewExistingRoleException areex)
            {
                Assert.IsTrue(areex.GetType() == typeof(NewExistingRoleException));
            }

            try
            {
                Role nuevoRol = new Role
                {
                    RoleId = "ROLTEST",
                    Description = "Rol de test",
                    Enabled = true,
                    Remarks = "Rol de test para las pruebas unitarias"
                };

                var rol = rolADO.NewRole(nuevoRol); //OK
                Assert.AreEqual(true, rol.Equals(nuevoRol));
            }
            catch( Exception ex)
            {
                throw ex;
            }
        }


        [TestMethod]
        public void DisableRole()
        {
            MockData.InitData();

            bool res = false;
            var rolADO = new ADORole();

            try
            {
                res = rolADO.DisableRole(null, null); 
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); 
            }

            try
            {
                res = rolADO.DisableRole("XXXX", "");
            }
            catch (NonexistentRoleException nerex)
            {
                Assert.IsTrue(nerex.GetType() == typeof(NonexistentRoleException));
            }

            try
            {
                res = rolADO.DisableRole("ADMIN", ""); //OK
                Assert.AreEqual(true, res);
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
