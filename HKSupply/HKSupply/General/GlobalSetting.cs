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
    /// Controlador para acceso a datos para Item
    /// Controlador para acceso a datos para Status Prod
    /// Controlador para acceso a datos para Currency
    /// Controlador para acceso a datos para Echange Rates
    /// Controlador para acceso a datos para Payment Terms
    /// Controlador para acceso a datos para Incoterm
    /// Controlador para acceso a datos para User Attr Description
    /// Controlador para acceso a datos para Item Barcelona
    /// Controlador para acceso a datos para Supplier Price List
    /// Controlador para acceso a datos para Customer Price List
    /// Controlador para acceso a datos para Materials (L1, L2, L3)
    /// Controlador para acceso a datos para Mat Type (L1, L2, L3)
    /// Controlador para acceso a datos para Hw Type (L1, L2, L3)
    /// Controlador para acceso a datos para Family HK
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
        static EFItem _itemEF = new EFItem();
        static EFStatusProd _statusProdEF = new EFStatusProd();
        static EFCurrency _currencyEF = new EFCurrency();
        static EFEchangeRate _echangeRateEF = new EFEchangeRate();
        static EFPaymentTerms _paymentTermsEF = new EFPaymentTerms();
        static EFIncoterm _incotermEF = new EFIncoterm();
        static EFUserAttrDescription _userAttrDescriptionEF = new EFUserAttrDescription();
        static EFItemBcn _itemBcnEF = new EFItemBcn();
        static EFSupplierPriceList _supplierPriceListEF = new EFSupplierPriceList();
        static EFCustomerPriceList _customerPriceListEF = new EFCustomerPriceList();
        static EFMaterial _materialEF = new EFMaterial();
        static EFMatType _matTypeEF = new EFMatType();
        static EFHwType _hwTypeEF = new EFHwType();
        static EFFamilyHK _familyHK = new EFFamilyHK();

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

        public static IItem ItemService 
        {
            get 
            {
                if (_itemEF == null)
                    return new EFItem();
                else
                    return _itemEF;
            
            }
        }

        public static IStatusProd StatusProdService 
        {
            get 
            {
                if (_statusProdEF == null)
                    return new EFStatusProd();
                else
                    return _statusProdEF;
            }
        }

        public static ICurrency CurrencyService 
        {
            get 
            {
                if (_currencyEF == null)
                    return new EFCurrency();
                else
                    return _currencyEF;
            }
        }

        public static IPaymentTerms PaymentTermsService
        {
            get 
            {
                if (_paymentTermsEF == null)
                    return new EFPaymentTerms();
                else
                    return _paymentTermsEF;
            }
        }

        public static IIncoterm IncotermService
        {
            get 
            {
                if (_incotermEF == null)
                    return new EFIncoterm();
                else
                    return _incotermEF;
            }
        }

        public static IUserAttrDescription UserAttrDescriptionService
        {
            get
            {
                if (_userAttrDescriptionEF == null)
                    return new EFUserAttrDescription();
                else
                    return _userAttrDescriptionEF;
            }
        }

        public static IItemBcn ItemBcnService  
        {
            get 
            {
                if (_itemBcnEF == null)
                    return new EFItemBcn();
                else
                    return _itemBcnEF;
            }
        }

        public static ISupplierPriceList SupplierPriceListService 
        {
            get 
            {
                if (_supplierPriceListEF == null)
                    return new EFSupplierPriceList();
                else
                    return _supplierPriceListEF;
            }
        }

        public static ICustomerPriceList CustomerPriceListService
        {
            get 
            {
                if (_customerPriceListEF == null)
                    return new EFCustomerPriceList();
                else
                    return _customerPriceListEF;
            }
        }

        public static IMaterial MaterialService 
        {
            get 
            {
                if (_materialEF == null)
                    return new EFMaterial();
                else
                    return _materialEF;
            }
        }

        public static IMatType MatTypeService 
        {
            get 
            {
                if (_matTypeEF == null)
                    return new EFMatType();
                else
                    return _matTypeEF;
            }
        }

        public static IHwType HwTypeService
        {
            get
            {
                if (_hwTypeEF == null)
                    return new EFHwType();
                else
                    return _hwTypeEF;
            }
        }

        public static IFamilyHK FamilyHKService 
        {
            get 
            {
                if (_familyHK == null)
                    return new EFFamilyHK();
                else
                    return _familyHK;
            }
        }

        public static IEchangeRate EchangeRateService
        {
            get 
            {
                if (_echangeRateEF == null)
                    return new EFEchangeRate();
                else
                    return _echangeRateEF;
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
