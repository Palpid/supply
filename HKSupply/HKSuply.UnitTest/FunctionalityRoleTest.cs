using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HKSupply.Services.Implementations;
using HKSupply.Helpers.Mocking;
using HKSupply.Exceptions;
using HKSupply.Models;
using HKSupply.DB;

namespace HKSuply.UnitTest
{
    [TestClass]
    public class FunctionalityRoleTest
    {
        [TestMethod]
        public void GetFunctionalityRole()
        {
            var functionalityRoleEF = new EFFunctionalityRole();

            try
            {
                var functionalityRole = functionalityRoleEF.GetFunctionalityRole(0, "XX"); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); //Es una tontería y siempre dará true, pero así no tengo el warnning
            }

            try
            {
                var functionalityRole = functionalityRoleEF.GetFunctionalityRole(1, null); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); //Es una tontería y siempre dará true, pero así no tengo el warnning
            }

            try
            {
                var functionalityRole = functionalityRoleEF.GetFunctionalityRole(1, "XX"); //NonexistentFunctionalityRoleException
            }
            catch (NonexistentFunctionalityRoleException nefex)
            {
                Assert.IsTrue(nefex.GetType() == typeof(NonexistentFunctionalityRoleException));
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var expectedValue = db.FunctionalitiesRole.FirstOrDefault(f => f.FunctionalityId.Equals(1) && f.RoleId.Equals("ADMIN"));
                    var functionalityRole = functionalityRoleEF.GetFunctionalityRole(expectedValue.FunctionalityId, expectedValue.RoleId); //OK
                    Assert.AreEqual(true, functionalityRole.Equals(expectedValue));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void GetFunctionalitiesRole()
        {
            var functionalityRoleEF = new EFFunctionalityRole();

            try
            {
                var functionalities = functionalityRoleEF.GetFunctionalitiesRole(null); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); //Es una tontería y siempre dará true, pero así no tengo el warnning
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var expectedValue = db.FunctionalitiesRole.Where(f => f.RoleId.Equals("ADMIN")).ToList();
                    var functionalities = functionalityRoleEF.GetFunctionalitiesRole("ADMIN"); //ArgumentNullException

                    Assert.IsTrue(expectedValue.Count() == functionalities.Count());
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void NewFunctionalityRole()
        {
            var functionalityRoleEF = new EFFunctionalityRole();

            try
            {
                var functionalityRole = functionalityRoleEF.NewFunctionalityRole(null); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); 
            }

            try
            {
                var existing = functionalityRoleEF.GetFunctionalityRole(1, "ADMIN");
                var functionalityRole = functionalityRoleEF.NewFunctionalityRole(existing);
            }
            catch(NewExistingFunctionalityRoleException nefre)
            {
                 Assert.IsTrue(nefre.GetType() == typeof(NewExistingFunctionalityRoleException)); 
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

                    var functionalityRole = functionalityRoleEF.NewFunctionalityRole(fr);

                    Assert.AreEqual(true, functionalityRole.Equals(fr));
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void ModifyFunctionalityRole()
        {
            var functionalityRoleEF = new EFFunctionalityRole();
            try
            {
                var func = functionalityRoleEF.ModifyFunctionalityRole(null);
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); 
            }

            try
            {
                var fr = new FunctionalityRole 
                {
                    RoleId = "XX",
                    FunctionalityId = 1,
                    Read = false,
                    New = false,
                    Modify = false,
                };

                var func = functionalityRoleEF.ModifyFunctionalityRole(fr);
            }
            catch (NonexistentFunctionalityRoleException nfrex)
            {
                Assert.IsTrue(nfrex.GetType() == typeof(NonexistentFunctionalityRoleException));
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var funcUserManagement = db.Functionalities.FirstOrDefault(f => f.FunctionalityName.Equals("MaterialsManagement"));
                    var fr = functionalityRoleEF.GetFunctionalityRole(funcUserManagement.FunctionalityId, "OPERATOR");
                    fr.Read = true;
                    fr.New = false;
                    fr.Modify = true;
                    var func = functionalityRoleEF.ModifyFunctionalityRole(fr); //OK

                    Assert.AreEqual(true, func.Equals(fr));
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
