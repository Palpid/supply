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
        public static List<Role> RolesList = new List<Role>();
        public static List<User> UsersList = new List<User>();
        public static List<Functionality> FunctionalitiesList = new List<Functionality>();
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
                UsersList.Clear();
                FunctionalitiesList.Clear();

                InitRolesList();
                InitUsuariosList();
                InitFuncionalidadesList();
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
                RolesList.Add(new Role 
                {
                    RoleId = "ADMIN",
                    Description = "Administrador aplicación",
                    Enabled = true,
                    Remarks = null
                });

                RolesList.Add(new Role 
                {
                    RoleId = "OPERATOR",
                    Description = "Operador",
                    Enabled = true,
                    Remarks = null
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
                var adminRol = RolesList.FirstOrDefault(r => r.RoleId.Equals("ADMIN"));
                var operatorRol = RolesList.FirstOrDefault(r => r.RoleId.Equals("OPERATOR"));

                UsersList.Add(new User 
                {
                    UserLogin = "admin",
                    Name = "Administrador",
                    Password = "adminpwd",
                    UserRol = adminRol,
                    Enabled = true,
                    LastLogout = null,
                    Remarks = null
                });

                UsersList.Add(new User
                {
                    UserLogin = "m.ruz",
                    Name = "Mario Ruz Martínez",
                    Password = "mariopwd",
                    UserRol = adminRol,
                    Enabled = true,
                    LastLogout = null,
                    Remarks = null
                });

                UsersList.Add(new User
                {
                    UserLogin = "operador1",
                    Name = "Operador 1",
                    Password = "op1pwd",
                    UserRol = adminRol,
                    Enabled = true,
                    LastLogout = null,
                    Remarks = null
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static void InitFuncionalidadesList()
        {
            try
            {
                var adminRol = RolesList.FirstOrDefault(r => r.RoleId.Equals("ADMIN"));
                var operatorRol = RolesList.FirstOrDefault(r => r.RoleId.Equals("OPERATOR"));

                FunctionalitiesList.Add(new Functionality 
                {
                    FunctionalityName = "MantenimientoUsuarios",
                    //TODO
                    //Read = true,
                    //Modify = true,
                    //Role = adminRol
                });

                FunctionalitiesList.Add(new Functionality
                {
                    FunctionalityName = "MantenimientoRoles",
                    //Read = true,
                    //Modify = true,
                    //Role = adminRol
                });

                FunctionalitiesList.Add(new Functionality
                {
                    FunctionalityName = "MantenimientoFuncionalidades",
                    //Read = true,
                    //Modify = true,
                    //Role = adminRol
                });

                FunctionalitiesList.Add(new Functionality
                {
                    FunctionalityName = "MantenimientoArticulos",
                    //Read = true,
                    //Modify = true,
                    //Role = adminRol
                });

                FunctionalitiesList.Add(new Functionality
                {
                    FunctionalityName = "MantenimientoArticulos",
                    //Read = true,
                    //Modify = false,
                    //Role = operatorRol
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
