using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Helpers.Mocking
{
    public static class MockData
    {
        #region Public Properties
        //public List<Rol> RolesList = new List<Rol>();
        //public List<Usuario> UsuariosList = new List<Usuario>();
        //public List<Funcionalidad> FuncionalidadesList = new List<Funcionalidad>();
        public static List<Rol> RolesList = new List<Rol>();
        public static List<Usuario> UsuariosList = new List<Usuario>();
        public static List<Funcionalidad> FuncionalidadesList = new List<Funcionalidad>();
        #endregion

        #region Constructor
        //public MockData()
        //{
        //    InitRolesList();
        //    InitUsuariosList();
        //}
        #endregion

        #region Public Methods
        public static void InitData()
        {
            try
            {
                RolesList.Clear();
                UsuariosList.Clear();
                FuncionalidadesList.Clear();

                InitRolesList();
                InitUsuariosList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Private Methods

        private static void InitRolesList()
        {
            try
            {
                RolesList.Add(new Rol 
                {
                    RolId = "ADMIN",
                    Descripcion = "Administrador aplicación",
                    Activo = true,
                    Observaciones = null
                });

                RolesList.Add(new Rol 
                {
                    RolId = "OPERATOR",
                    Descripcion = "Operador",
                    Activo = true,
                    Observaciones = null
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void InitUsuariosList()
        {
            try
            {
                var adminRol = RolesList.FirstOrDefault(r => r.RolId.Equals("ADMIN"));
                var operatorRol = RolesList.FirstOrDefault(r => r.RolId.Equals("OPERATOR"));

                UsuariosList.Add(new Usuario 
                {
                    UsuarioId = "admin",
                    Nombre = "Administrador",
                    Password = "adminpwd",
                    UserRol = adminRol,
                    Activo = true,
                    LastLogout = null,
                    Observaciones = null
                });

                UsuariosList.Add(new Usuario
                {
                    UsuarioId = "m.ruz",
                    Nombre = "Mario Ruz Martínez",
                    Password = "mariopwd",
                    UserRol = adminRol,
                    Activo = true,
                    LastLogout = null,
                    Observaciones = null
                });

                UsuariosList.Add(new Usuario
                {
                    UsuarioId = "operador1",
                    Nombre = "Operador 1",
                    Password = "op1pwd",
                    UserRol = adminRol,
                    Activo = true,
                    LastLogout = null,
                    Observaciones = null
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

    }
}
