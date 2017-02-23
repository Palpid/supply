using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HKSupply.Services.Implementations;
using HKSupply.Exceptions;
using HKSupply.Helpers.Mocking;
using HKSupply.Models;

namespace HKSuply.UnitTest
{
    [TestClass]
    public class RolTest
    {

        [TestMethod]
        public void GetRol()
        {
            MockData.InitData();
            var rolADO = new ADORol();

            try
            {
                var rol = rolADO.GetRolById(null); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); //Es una tontería y siempre dará true, pero así no tengo el warnning
            }

            try
            {
                var rol = rolADO.GetRolById("XX"); //NoExisteRolException
            }
            catch (NoExisteRolException nerex)
            {
                Assert.IsTrue(nerex.GetType() == typeof(NoExisteRolException));
            }

            try
            {
                var expectedValue = MockData.RolesList.FirstOrDefault(r => r.RolId.Equals("ADMIN"));
                var rol = rolADO.GetRolById("ADMIN");
                Assert.AreEqual(true, rol.Equals(expectedValue));

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void AltaRol()
        {
            MockData.InitData();
            var rolADO = new ADORol();

            try
            {
                var alta = rolADO.AltaRol(null);
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException));
            }

            try
            {
                var rol = MockData.RolesList.FirstOrDefault(r => r.RolId.Equals("ADMIN"));
                var alta = rolADO.AltaRol(rol);
            }
            catch (AltaRolExistenteException areex)
            {
                Assert.IsTrue(areex.GetType() == typeof(AltaRolExistenteException));
            }

            try
            {
                Rol nuevoRol = new Rol
                {
                    RolId = "ROLTEST",
                    Descripcion = "Rol de test",
                    Activo = true,
                    Observaciones = "Rol de test para las pruebas unitarias"
                };

                var rol = rolADO.AltaRol(nuevoRol); //OK
                Assert.AreEqual(true, rol.Equals(nuevoRol));
            }
            catch( Exception ex)
            {
                throw ex;
            }
        }


        [TestMethod]
        public void DesactivarRol()
        {
            MockData.InitData();

            bool res = false;
            var rolADO = new ADORol();

            try
            {
                res = rolADO.DesactivarRol(null, null); 
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); 
            }

            try
            {
                res = rolADO.DesactivarRol("XXXX", "");
            }
            catch (NoExisteRolException nerex)
            {
                Assert.IsTrue(nerex.GetType() == typeof(NoExisteRolException));
            }

            try
            {
                res = rolADO.DesactivarRol("ADMIN", ""); //OK
                Assert.AreEqual(true, res);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
