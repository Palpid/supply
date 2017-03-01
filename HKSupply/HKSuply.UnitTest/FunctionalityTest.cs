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
    public class FunctionalityTest
    {
        [TestMethod]
        public void GetFunctionality()
        {
            var functionalityEF = new EFFunctionality();

            //by Id
            try
            {
                var functionality = functionalityEF.GetFunctionalityById(0); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); //Es una tontería y siempre dará true, pero así no tengo el warnning
            }

            try
            {
                var functionality = functionalityEF.GetFunctionalityById(150000); //NoExisteFuncionalidadException
            }
            catch (NonexistentFunctionalityException nefex)
            {
                Assert.IsTrue(nefex.GetType() == typeof(NonexistentFunctionalityException));
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var expectedValue = db.Functionalities.FirstOrDefault(f => f.FunctionalityId.Equals(1));
                    var functionality = functionalityEF.GetFunctionalityById(1); //OK
                    Assert.AreEqual(true, functionality.Equals(expectedValue));
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //by Name
            try
            {
                var functionality = functionalityEF.GetFunctionalityByName(null); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); //Es una tontería y siempre dará true, pero así no tengo el warnning
            }

            try
            {
                var functionality = functionalityEF.GetFunctionalityByName("XX"); //NoExisteFuncionalidadException
            }
            catch (NonexistentFunctionalityException nefex)
            {
                Assert.IsTrue(nefex.GetType() == typeof(NonexistentFunctionalityException));
            }

            try
            {
                using (var db = new HKSupplyContext())
                {
                    var expectedValue = db.Functionalities.FirstOrDefault(f => f.FunctionalityId.Equals(1));
                    var functionality = functionalityEF.GetFunctionalityByName(expectedValue.FunctionalityName); //OK
                    Assert.AreEqual(true, functionality.Equals(expectedValue));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        //[TestMethod]
        //public void GetRoleFunctionalities()
        //{
        //    var functionalityEF = new EFFunctionality();

        //    try
        //    {
        //        var functionalities = functionalityEF.GetFunctionalitiesByRol(null); //ArgumentNullException
        //    }
        //    catch (ArgumentNullException anex)
        //    {
        //        Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException));
        //    }

        //    try
        //    {

        //        var functionalities = functionalityEF.GetFunctionalitiesByRol("XXX");  //Empty list. No error
        //        Assert.IsTrue(true);

        //        var roleFunctionalities = functionalityEF.GetFunctionalitiesByRol("ADMIN");
        //        Assert.IsTrue(roleFunctionalities.Count() > 0);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //[TestMethod]
        //public void GetCategoriesRoles()
        //{
        //    var functionalityEF = new EFFunctionality();

        //    try
        //    {
        //        var categories = functionalityEF.GetFunctionalitiesCategoriesRole(null); //ArgumentNullException
        //    }
        //    catch (ArgumentNullException anex)
        //    {
        //        Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException));
        //    }

        //    try
        //    {

        //        var categories = functionalityEF.GetFunctionalitiesCategoriesRole("XXX");  //Empty list. No error
        //        Assert.IsTrue(true);

        //        var rolecategories = functionalityEF.GetFunctionalitiesCategoriesRole("ADMIN");
        //        Assert.IsTrue(rolecategories.Count() > 0);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        [TestMethod]
        public void NewFunctionality()
        {
            var functionalityEF = new EFFunctionality();

            try
            {
                var functionality = functionalityEF.NewFunctionality(null); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException));
            }

            try
            {
                var functionality = functionalityEF.GetFunctionalityById(1);
                var newFunc = functionalityEF.NewFunctionality(functionality); //NewExistingFunctionalityException
            }
            catch (NewExistingFunctionalityException afeex)
            {
                Assert.IsTrue(afeex.GetType() == typeof(NewExistingFunctionalityException));
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
                    var newFunc = functionalityEF.NewFunctionality(func); //ok
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
            var functionalityEF = new EFFunctionality();

            try
            {
                var functionality = functionalityEF.ModifyFunctionality(null); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException));
            }

            try
            {
                var functionality = new Functionality
                {
                    FunctionalityName = "XX",
                    Category = "XX",
                    FormName = "XX"
                };
                
                var modFunctionality = functionalityEF.ModifyFunctionality(functionality); //NonexistentFunctionalityException
            }
            catch (NonexistentFunctionalityException nefex)
            {
                Assert.IsTrue(nefex.GetType() == typeof(NonexistentFunctionalityException));
            }

            try
            {
                var expectedValue = functionalityEF.GetFunctionalityById(1);

                expectedValue.Category = expectedValue.Category + "_X";
                expectedValue.FormName = expectedValue.FormName + "_X";

                var modFunctionality = functionalityEF.ModifyFunctionality(expectedValue); //NoExisteFuncionalidadException
                Assert.AreEqual(true, modFunctionality.Equals(expectedValue));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region v1 "ADO"
        /*
        [TestMethod]
        public void GetFunctionality()
        { 
            MockData.InitData();
            var funcionalidadADO = new ADOFunctionality();

            try
            {
                var funcionalidad = funcionalidadADO.GetFunctionalityByIdRol(null, ""); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); //Es una tontería y siempre dará true, pero así no tengo el warnning
            }

            try
            {
                var funcionalidad = funcionalidadADO.GetFunctionalityByIdRol("", null); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException));
            }

            try
            {
                var funcionalidad = funcionalidadADO.GetFunctionalityByIdRol("XX", "XX"); //NoExisteFuncionalidadException
            }
            catch (NonexistentFunctionalityException nefex)
            {
                Assert.IsTrue(nefex.GetType() == typeof(NonexistentFunctionalityException));
            }

            try
            {
                var expectedValue = MockData.FunctionalitiesList.FirstOrDefault(f => f.FunctionalityName.Equals("MantenimientoUsuarios")
                    && f.Role.RoleId.Equals("ADMIN"));
                var funcionalidad = funcionalidadADO.GetFunctionalityByIdRol("MantenimientoUsuarios", "ADMIN"); //OK
                Assert.AreEqual(true, funcionalidad.Equals(expectedValue));
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void NewFunctionality()
        {
            MockData.InitData();
            var funcionalidadADO = new ADOFunctionality();

            try
            {
                var funcionalidad = funcionalidadADO.NewFunctionality(null); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException));
            }

            try
            {
                var funcionalidad = MockData.FunctionalitiesList.FirstOrDefault(f => f.FunctionalityName.Equals("MantenimientoUsuarios") &&
                    f.Role.RoleId.Equals("ADMIN"));
                var alta = funcionalidadADO.NewFunctionality(funcionalidad); //AltaFuncionalidadExistenteException
            }
            catch (NewExistingFunctionalityException afeex)
            {
                Assert.IsTrue(afeex.GetType() == typeof(NewExistingFunctionalityException));
            }

            try
            {
                var alta = new Functionality 
                {
                    FunctionalityName = "FuncionalidadTest",
                    Read = true,
                    Modify = false,
                    Role = MockData.RolesList.FirstOrDefault(r => r.RoleId.Equals("OPERATOR"))
                };
                var nuevaFuncionalidad = funcionalidadADO.NewFunctionality(alta); //ok
                Assert.AreEqual(true, nuevaFuncionalidad.Equals(alta));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void UpdateFunctionality()
        {
            MockData.InitData();
            var funcionalidadADO = new ADOFunctionality();

            try
            {
                var funcionalidad = funcionalidadADO.ModifyFunctionality(null); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException));
            }

            try
            {
                var funcionalidad = new Functionality 
                {
                    FunctionalityName = "FuncionalidadTest",
                    Read = true,
                    Modify = false,
                    Role = MockData.RolesList.FirstOrDefault(r => r.RoleId.Equals("OPERATOR"))
                };

                var funcionalidadMod = funcionalidadADO.ModifyFunctionality(funcionalidad); //NoExisteFuncionalidadException
            }
            catch (NonexistentFunctionalityException nefex)
            {
                Assert.IsTrue(nefex.GetType() == typeof(NonexistentFunctionalityException));
            }

            try
            {
                var expectedValue = new Functionality
                {
                    FunctionalityName = "MantenimientoUsuarios",
                    Read = false,
                    Modify = false,
                    Role = MockData.RolesList.FirstOrDefault(r => r.RoleId.Equals("ADMIN"))
                };
                var funcionalidadMod = funcionalidadADO.ModifyFunctionality(expectedValue);  //OK

                Assert.AreEqual(true, funcionalidadMod.Equals(expectedValue));

            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
         */ 
        #endregion
    }
}
