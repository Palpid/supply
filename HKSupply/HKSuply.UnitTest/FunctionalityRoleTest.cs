using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HKSupply.Services.Implementations;
using HKSupply.Helpers.Mocking;
using HKSupply.Exceptions;
using HKSupply.Models;
using HKSupply.DB;
using HKSupply.General;

namespace HKSuply.UnitTest
{
    [TestClass]
    public class FunctionalityRoleTest
    {
        [TestMethod]
        public void GetAllFunctionalitiesRole()
        {
            try
            {
                var functionalitiesRole = GlobalSetting.FunctionalityRoleService.GetAllFunctionalitiesRole();
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void GetFunctionalityRole()
        {
            try
            {
                var functionalityRole = GlobalSetting.FunctionalityRoleService.GetFunctionalityRole(0, "XX"); //ArgumentNullException
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }

            try
            {
                var functionalityRole = GlobalSetting.FunctionalityRoleService.GetFunctionalityRole(1, null); //ArgumentNullException
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }

            try
            {
                var functionalityRole = GlobalSetting.FunctionalityRoleService.GetFunctionalityRole(1, "XX"); //NonexistentFunctionalityRoleException
                Assert.Fail("Expected exception");
            }
            catch (NonexistentFunctionalityRoleException)
            {
                Assert.IsTrue(true);
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var expectedValue = db.FunctionalitiesRole.FirstOrDefault(f => f.FunctionalityId.Equals(1) && f.RoleId.Equals("ADMIN"));
                    var functionalityRole = GlobalSetting.FunctionalityRoleService.GetFunctionalityRole(expectedValue.FunctionalityId, expectedValue.RoleId); //OK
                    Assert.AreEqual(true, functionalityRole.Equals(expectedValue));
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        [TestMethod]
        public void GetFunctionalitiesRole()
        {
            try
            {
                var functionalities = GlobalSetting.FunctionalityRoleService.GetFunctionalitiesRole(null); //ArgumentNullException
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var expectedValue = db.FunctionalitiesRole.Where(f => f.RoleId.Equals("ADMIN")).ToList();
                    var functionalities = GlobalSetting.FunctionalityRoleService.GetFunctionalitiesRole("ADMIN"); //ok

                    Assert.IsTrue(expectedValue.Count() == functionalities.Count());
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void GetFunctionalitiesCategoriesRole()
        {
            try
            {
                var functionalities = GlobalSetting.FunctionalityRoleService.GetFunctionalitiesCategoriesRole(null); //ArgumentNullException
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var functionalities = GlobalSetting.FunctionalityRoleService.GetFunctionalitiesCategoriesRole("ADMIN"); //ok
                    Assert.IsTrue(true);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        [TestMethod]
        public void NewFunctionalityRole()
        {
            try
            {
                var functionalityRole = GlobalSetting.FunctionalityRoleService.NewFunctionalityRole(null); //ArgumentNullException
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true); 
            }

            try
            {
                var existing = GlobalSetting.FunctionalityRoleService.GetFunctionalityRole(1, "ADMIN");
                var functionalityRole = GlobalSetting.FunctionalityRoleService.NewFunctionalityRole(existing); //NewExistingFunctionalityRoleException
                Assert.Fail("Expected exception");
            }
            catch(NewExistingFunctionalityRoleException)
            {
                 Assert.IsTrue(true); 
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var opRole = db.Roles.FirstOrDefault(r => r.RoleId.Equals("OPERATOR"));
                    var funcUserManagement = db.Functionalities.FirstOrDefault(f => f.FunctionalityName.Equals("UserManagement"));

                    var fr = new FunctionalityRole
                    {
                        RoleId = opRole.RoleId,
                        FunctionalityId = funcUserManagement.FunctionalityId,
                        Read = false,
                        New = false,
                        Modify = false,
                    };

                    var functionalityRole = GlobalSetting.FunctionalityRoleService.NewFunctionalityRole(fr);

                    Assert.AreEqual(true, functionalityRole.Equals(fr));
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void ModifyFunctionalityRole()
        {
            try
            {
                var func = GlobalSetting.FunctionalityRoleService.ModifyFunctionalityRole(null);
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true); 
            }

            try
            {
                var fr = new FunctionalityRole 
                {
                    RoleId = "XXXXXX",
                    FunctionalityId = 1,
                    Read = false,
                    New = false,
                    Modify = false,
                };

                var func = GlobalSetting.FunctionalityRoleService.ModifyFunctionalityRole(fr);
                Assert.Fail("Expected exception");
            }
            catch (NonexistentFunctionalityRoleException)
            {
                Assert.IsTrue(true);
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var funcMatManagement = GlobalSetting.FunctionalityService.GetFunctionalityByName("Materials Management");

                    var fr = GlobalSetting.FunctionalityRoleService.GetFunctionalityRole(funcMatManagement.FunctionalityId, "OPERATOR");
                    fr.Read = true;
                    fr.New = false;
                    fr.Modify = true;
                    var func = GlobalSetting.FunctionalityRoleService.ModifyFunctionalityRole(fr); //OK

                    Assert.AreEqual(true, func.Equals(fr));
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [TestMethod]
        public void UpdateFunctionalitiesRoles()
        {
            bool res = false;
            try
            {
                res = GlobalSetting.FunctionalityRoleService.UpdateFunctionalitiesRoles(null);  //ArgumentNullException
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }

            try
            {

                var funcMatManagement = GlobalSetting.FunctionalityService.GetFunctionalityByName("Materials Management");

                var all = GlobalSetting.FunctionalityRoleService.GetAllFunctionalitiesRole();
                var frmo = all.Where(f => f.FunctionalityId.Equals(funcMatManagement.FunctionalityId) && f.RoleId.Equals("OPERATOR")).ToList();
                frmo.FirstOrDefault().Modify = false;

                res = GlobalSetting.FunctionalityRoleService.UpdateFunctionalitiesRoles(frmo);
                Assert.AreEqual(true, res);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
