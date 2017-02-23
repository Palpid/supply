using HKSupply.Exceptions;
using HKSupply.Helpers.Mocking;
using HKSupply.Models;
using HKSupply.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HKSupply.Services.Implementations
{
    public class ADOUsuario : IUsuario
    {
        public Usuario GetUsuarioByIdPassword(string UsuarioId, string Password)
        {
            try
            {
                if (UsuarioId == null)
                    throw new ArgumentNullException("UsuarioId");
                if (Password == null)
                    throw new ArgumentNullException("Password");

                var usuario = MockData.UsuariosList.FirstOrDefault(u => u.UsuarioId.Equals(UsuarioId) && u.Password.Equals(Password));

                if (usuario == null)
                    throw new NoExisteUsuarioException();

                return usuario;
            }
            catch (ArgumentNullException anex)
            {
                throw anex;
            }
            catch (NoExisteUsuarioException neuex)
            {
                throw neuex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario AltaUsuario(Usuario nuevoUsuario)
        {
            try
            {
                if (nuevoUsuario == null)
                    throw new ArgumentNullException("nuevoUsuario");

                var usuario = MockData.UsuariosList.FirstOrDefault(u => u.UsuarioId.Equals(nuevoUsuario.UsuarioId));

                if (usuario != null)
                    throw new AltaUsuarioExistenteException();

                MockData.UsuariosList.Add(nuevoUsuario);

                return GetUsuarioByIdPassword(nuevoUsuario.UsuarioId, nuevoUsuario.Password);

            }
            catch (ArgumentNullException anex)
            {
                throw anex;
            }
            catch (AltaUsuarioExistenteException aueex)
            {
                throw aueex;
            }
            catch (NoExisteUsuarioException neuex)
            {
                throw neuex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DesactivarUsuario(string usuarioId, string observaciones)
        {
            try
            {
                if (usuarioId == null)
                    throw new ArgumentNullException("UsuarioId");

                var usuario = MockData.UsuariosList.FirstOrDefault(u => u.UsuarioId.Equals(usuarioId));

                if (usuario == null)
                    throw new NoExisteUsuarioException();

                usuario.Activo = false;
                usuario.Observaciones = observaciones;

                return true;
            }
            catch (ArgumentNullException anex)
            {
                throw anex;
            }
            catch (NoExisteUsuarioException neuex)
            {
                throw neuex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
