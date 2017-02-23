using HKSupply.Exceptions;
using HKSupply.Helpers.Mocking;
using HKSupply.Models;
using HKSupply.Services.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MockData.InitData();

            //Roles
            var rolADO = new ADORol();
            var nuevoRol = new Rol 
            {
                RolId = "ROLTEST",
                Descripcion = "Rol de Test",
                Activo = true,
                Observaciones = null
            };

            var createdRol = rolADO.AltaRol(nuevoRol);
            bool res = rolADO.DesactivarRol("ROLTEST", "Desactivamos rol");

            //Usuario
            var usuarioADO = new ADOUsuario();
            var nuevoUsuario = new Usuario 
            {
                UsuarioId = "usuario.test",
                Nombre = "Usuario Test",
                Password = "usutestpwd",
                UserRol = MockData.RolesList.FirstOrDefault(r => r.RolId.Equals("OPERATOR")),
                Activo = true,
                LastLogout = null,
                Observaciones = "Observaciones usuario test"
            };

            var createdUser = usuarioADO.AltaUsuario(nuevoUsuario);
            bool resDesUsu = usuarioADO.DesactivarUsuario(nuevoUsuario.UsuarioId, "Desactivamos usuario test");


        }
    }
}
