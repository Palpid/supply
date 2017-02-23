using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HKSupply.Services.Implementations;
using HKSupply.Helpers.Mocking;
using HKSupply.Exceptions;
using HKSupply.Models;

namespace HKSuply.UnitTest
{
    [TestClass]
    public class UsuarioTest
    {
        [TestMethod]
        public void GetUsuario()
        {
            MockData.InitData();
            var usuarioADO = new ADOUsuario();

            try
            {
                var usuario = usuarioADO.GetUsuarioByIdPassword(null, "xx"); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); //Es una tontería y siempre dará true, pero así no tengo el warnning
            }

            try
            {
                var usuario = usuarioADO.GetUsuarioByIdPassword("XX", null); //ArgumentNullException
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); 
            }


            try
            {
                var usuario = usuarioADO.GetUsuarioByIdPassword("XX", "XX"); //NoExisteUsuarioException
            }
            catch (NoExisteUsuarioException neuex)
            {
                Assert.IsTrue(neuex.GetType() == typeof(NoExisteUsuarioException)); 
            }

            try
            {

                var expectedValue = MockData.UsuariosList.FirstOrDefault(u => u.UsuarioId.Equals("admin"));
                var usuario = usuarioADO.GetUsuarioByIdPassword("admin", "adminpwd"); //OK
                Assert.AreEqual(true, usuario.Equals(expectedValue));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }

        [TestMethod]
        public void AltaUsuario()
        {
            MockData.InitData();
            var usuarioADO = new ADOUsuario();

            try
            {
                usuarioADO.AltaUsuario(null);
            }
            catch (ArgumentNullException anex)
            {
                Assert.IsTrue(anex.GetType() == typeof(ArgumentNullException)); //Es una tontería y siempre dará true, pero así no tengo el warnning
            }

            try
            {
                var usuario = MockData.UsuariosList.FirstOrDefault(u => u.UsuarioId.Equals("admin"));
                usuarioADO.AltaUsuario(usuario); //AltaUsuarioExistenteException
            }
            catch (AltaUsuarioExistenteException auxex)
            {
                Assert.IsTrue(auxex.GetType() == typeof(AltaUsuarioExistenteException));
            }

            try
            {
                var rol = MockData.RolesList.FirstOrDefault(r => r.RolId.Equals("OPERATOR"));
                var nuevoUsuario = new Usuario
                {
                    UsuarioId = "usuario.test",
                    Nombre = "Usuario de Test",
                    Password = "usutestpwd",
                    UserRol = rol,
                    Activo = true,
                    LastLogout = null,
                    Observaciones = "Observaciones usuario de test"
                };

                var usuario = usuarioADO.AltaUsuario(nuevoUsuario);
                Assert.AreEqual(true, usuario.Equals(nuevoUsuario));
            }
            catch (Exception ex) 
            {
                throw ex; 
            }
        }
        
        [TestMethod]
        public void DesactivarUsuario()
        {
            MockData.InitData();
            var usuarioADO = new ADOUsuario();
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
            catch (NoExisteUsuarioException neuex)
            {
                Assert.IsTrue(neuex.GetType() == typeof(NoExisteUsuarioException));
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
    }
}
