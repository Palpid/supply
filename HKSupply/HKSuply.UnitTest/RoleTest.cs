using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HKSupply.Services.Implementations;
using HKSupply.Exceptions;
using HKSupply.Helpers.Mocking;
using HKSupply.Models;
using HKSupply.DB;
using HKSupply.General;


namespace HKSuply.UnitTest
{
    [TestClass]
    public class RoleTest
    {
        [TestMethod]
        public void GetAllRoles()
        {
            try
            {
                var roles = GlobalSetting.RoleService.GetRoles();
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void GetRole()
        {
            try
            {
                var rol = GlobalSetting.RoleService.GetRoleById(null); //ArgumentNullException
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
                var rol = GlobalSetting.RoleService.GetRoleById("XXXXXXXX"); //NonexistentRoleException
                Assert.Fail("Expected exception");
            }
            catch (NonexistentRoleException)
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
                    var expectedValue = db.Roles.FirstOrDefault(r => r.RoleId.Equals("ADMIN"));  
                    var rol = GlobalSetting.RoleService.GetRoleById("ADMIN");
                    Assert.AreEqual(true, rol.Equals(expectedValue));
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void NewRole()
        {
            try
            {
                var newRole = GlobalSetting.RoleService.NewRole(null);
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
                    var role = db.Roles.FirstOrDefault(r => r.RoleId.Equals("ADMIN"));
                    var newRole = GlobalSetting.RoleService.NewRole(role);
                    Assert.Fail("Expected exception");
                }
                
            }
            catch (NewExistingRoleException)
            {
                Assert.IsTrue(true);
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    Role newRole = new Role
                    {
                        RoleId = "ROLTEST",
                        Description = null,
                        Enabled = true,
                        Remarks = "test role for unit test"
                    };

                    var role = GlobalSetting.RoleService.NewRole(newRole);
                }

            }
            catch (Exception e)
            {
                //No se puede capturar el DbEntityValidationException desde aquí al no haber contexto de EF
                if (e.GetType().Name.Equals("DbEntityValidationException"))
                    Assert.IsTrue(true);
                else
                    throw e;
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

                    var role = GlobalSetting.RoleService.NewRole(newRole); //OK
                    Assert.AreEqual(true, role.Equals(newRole));
                }

            }
            catch (Exception)
            {
                throw;
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
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }

            try
            {
                res = roleEF.DisableRole("XXXXXXX", "");
                Assert.Fail("Expected exception");
            }
            catch (NonexistentRoleException)
            {
                Assert.IsTrue(true);
            }

            try
            {
                res = roleEF.DisableRole("ROLTEST", ""); //OK
                Assert.AreEqual(true, res);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [TestMethod]
        public void UpdateRoles()
        {
            bool res = false;

            try
            {
                res = GlobalSetting.RoleService.UpdateRoles(null); //ArgumentNullException
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
                var roles = GlobalSetting.RoleService.GetRoles();
                roles = roles.Where(r => r.RoleId.Equals("XX")).ToList();
                res = GlobalSetting.RoleService.UpdateRoles(roles); //OK
                Assert.AreEqual(true, res);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
