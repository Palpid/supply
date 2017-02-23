using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKSupply.Models;

namespace HKSupply.Services.Interfaces
{
    interface IUsuario
    {
        Usuario GetUsuarioByIdPassword(string UsuarioId, string Password);
        Usuario AltaUsuario(Usuario nuevoUsuario);
        bool DesactivarUsuario(string usuarioId, string observaciones);
    }
}
