using HKSupply.Models;
using HKSupply.Services.Implementations;
using HKSupply.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.General
{
    /// <summary>
    /// Objeto para guardar valores globales accesibles desde toda la aplicación. 
    /// </summary>
    /// <remarks>
    /// Objetos que incluye:
    /// - Usuario logado
    /// Controlador para acceso a datos para Role
    /// Controlador para acceso a datos para User
    /// Controlador para acceso a datos para Functionality
    /// Controlador para acceso a datos para Functionality Role
    /// Controlador para acceso a datos para Customer
    /// Controlador para acceso a datos para Supplier
    /// Controlador para acceso a datos para Store
    /// </remarks>
    public sealed class GlobalSetting
    {
        #region Private Members
        static EFRole _roleEF = new EFRole();
        static EFUser _userEF = new EFUser();
        static EFFunctionality _functionalityEF = new EFFunctionality();
        static EFFunctionalityRole _functionalityRoleEF = new EFFunctionalityRole();
        static EFCustomer _customerEF = new EFCustomer();
        static EFSupplier _supplierEF = new EFSupplier();
        static EFStore _storeEF = new EFStore();

        static User _loggedUser;
        static IEnumerable<FunctionalityRole> _functionalitiesRoles;

        static ResourceManager _resManager = new ResourceManager(typeof(HKSupply.Resources.HKSupplyRes));
        #endregion

        #region Public Properties
        public static IRole RoleService 
        {
            get 
            {
                if (_roleEF == null)
                    return new EFRole();
                else
                    return _roleEF;
            }

        }
        
        public static IUser UserService 
        {
            get 
            {
                if (_userEF == null)
                    return new EFUser();
                else
                    return _userEF;

            }
        }
        
        public static IFunctionality FunctionalityService 
        {
            get 
            {
                if (_functionalityEF == null)
                    return new EFFunctionality();
                else
                    return _functionalityEF;
            }
        }
        
        public static IFunctionalityRole FunctionalityRoleService 
        {
            get 
            {
                if (_functionalityRoleEF == null)
                    return new EFFunctionalityRole();
                else
                    return _functionalityRoleEF;
            }
        }

        public static ICustomer CustomerService
        {
            get
            {
                if (_customerEF == null)
                    return new EFCustomer();
                else
                    return _customerEF;
            }
        }

        public static ISupplier SupplierService
        {
            get 
            {
                if (_supplierEF == null)
                    return new EFSupplier();
                else
                    return _supplierEF;
            }
        }

        public static IStore StoreService
        {
            get 
            {
                if (_storeEF == null)
                    return new EFStore();
                else
                    return _storeEF;
            }
        }

        public static User LoggedUser 
        {
            get { return _loggedUser; }
            set { _loggedUser = value; }
        }

        public static IEnumerable<FunctionalityRole> FunctionalitiesRoles 
        {
            get { return _functionalitiesRoles; }
            set { _functionalitiesRoles = value; }
        }

        public static ResourceManager ResManager 
        {
            get { return _resManager; }
        }
        #endregion
        
        private static readonly Lazy<GlobalSetting> lazy =
        new Lazy<GlobalSetting>(() => new GlobalSetting());

        public static GlobalSetting Instance { get { return lazy.Value; } }

        public GlobalSetting()
        {

        }
    }
}
