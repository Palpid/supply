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
    public class FunctionalityTest
    {

        [TestMethod]
        public void GetAllFunctionalities()
        {
            try
            {
                var functionalities = GlobalSetting.FunctionalityService.GetAllFunctionalities(); 
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void GetFunctionality()
        {
            //by Id
            try
            {
                var functionality = GlobalSetting.FunctionalityService.GetFunctionalityById(0); //ArgumentNullException
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true); 
            }

            try
            {
                var functionality = GlobalSetting.FunctionalityService.GetFunctionalityById(150000); //NoExisteFuncionalidadException
                Assert.Fail("Expected exception");
            }
            catch (NonexistentFunctionalityException)
            {
                Assert.IsTrue(true);
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var expectedValue = db.Functionalities.FirstOrDefault(f => f.FunctionalityId.Equals(1));
                    var functionality = GlobalSetting.FunctionalityService.GetFunctionalityById(1); //OK
                    Assert.AreEqual(true, functionality.Equals(expectedValue));
                }
                
            }
            catch (Exception)
            {
                throw;
            }

            //by Name
            try
            {
                var functionality = GlobalSetting.FunctionalityService.GetFunctionalityByName(null); //ArgumentNullException
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
                var functionality = GlobalSetting.FunctionalityService.GetFunctionalityByName("XX"); //NoExisteFuncionalidadException
                Assert.Fail("Expected exception");
            }
            catch (NonexistentFunctionalityException)
            {
                Assert.IsTrue(true);
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var expectedValue = db.Functionalities.FirstOrDefault(f => f.FunctionalityId.Equals(1));
                    var functionality = GlobalSetting.FunctionalityService.GetFunctionalityByName(expectedValue.FunctionalityName); //OK
                    Assert.AreEqual(true, functionality.Equals(expectedValue));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [TestMethod]
        public void NewFunctionality()
        {

            try
            {
                var functionality = GlobalSetting.FunctionalityService.NewFunctionality(null); //ArgumentNullException
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }

            try
            {
                var functionality = GlobalSetting.FunctionalityService.GetFunctionalityById(1);
                var newFunc = GlobalSetting.FunctionalityService.NewFunctionality(functionality); //NewExistingFunctionalityException
                Assert.Fail("Expected exception");
            }
            catch (NewExistingFunctionalityException )
            {
                Assert.IsTrue(true);
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var func = new Functionality
                    {
                        FunctionalityName = "TestFunctionality",
                        Category = "TEST",
                        FormName = "frmTest"
                    };
                    var newFunc = GlobalSetting.FunctionalityService.NewFunctionality(func); //ok
                    Assert.AreEqual(true, newFunc.Equals(func));
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void UpdateFunctionality()
        {
            try
            {
                var functionality = GlobalSetting.FunctionalityService.ModifyFunctionality(null); //ArgumentNullException
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }

            try
            {
                var functionality = new Functionality
                {
                    FunctionalityName = "XXXXXX",
                    Category = "XX",
                    FormName = "XX"
                };

                var modFunctionality = GlobalSetting.FunctionalityService.ModifyFunctionality(functionality); //NonexistentFunctionalityException
                Assert.Fail("Expected exception");
            }
            catch (NonexistentFunctionalityException)
            {
                Assert.IsTrue(true);
            }

            try
            {
                //var expectedValue = GlobalSetting.FunctionalityService.GetFunctionalityById(1);
                var expectedValue = GlobalSetting.FunctionalityService.GetFunctionalityByName("XX");

                //expectedValue.Category = expectedValue.Category + "_X";
                expectedValue.FormName = expectedValue.FormName + "_X";

                var modFunctionality = GlobalSetting.FunctionalityService.ModifyFunctionality(expectedValue);  //OK
                Assert.AreEqual(true, modFunctionality.Equals(expectedValue));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void UpdateFunctionalities()
        {
            bool res = false;
            try
            {
                res = GlobalSetting.FunctionalityService.UpdateFunctionalities(null); //ArgumentNullException
                Assert.Fail("Expected exception");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            
            try
            {
                var functionalities = GlobalSetting.FunctionalityService.GetAllFunctionalities();
                functionalities = functionalities.Where(f => f.FunctionalityName.Equals("XX")).ToList();
                functionalities.FirstOrDefault().FormName += "_X";

                res = GlobalSetting.FunctionalityService.UpdateFunctionalities(functionalities); //OK
                Assert.AreEqual(true, res);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
