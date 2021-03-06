﻿using HKSupply.Models;
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
    /// Controlador para acceso a datos para Item EY
    /// Controlador para acceso a datos para Item HW
    /// Controlador para acceso a datos para Item MT
    /// Controlador para acceso a datos para Status Prod
    /// Controlador para acceso a datos para Status Cial
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
    /// Controlador para acceso a datos para Doc Type
    /// Controlador para acceso a datos para Item Doc
    /// Controlador para acceso a datos para Prototypes
    /// Controlador para acceso a datos para Prototypes Docs
    /// </remarks>
    public sealed class GlobalSetting
    {
        #region Private Members
        static string _dbEnvironment = Constants.SQL_EXPRESS_CONN; //SQL Express local
        //static string _dbEnvironment = Constants.SQL_DEV_SERVER_CONN; //SQL Server (Desarrollo)
        //static string _dbEnvironment = Constants.SQL_PROD_SERVER_CONN; //SQL Server (Producción)

        static EFRole _roleEF = new EFRole();
        static EFUser _userEF = new EFUser();
        static EFFunctionality _functionalityEF = new EFFunctionality();
        static EFFunctionalityRole _functionalityRoleEF = new EFFunctionalityRole();
        static EFCustomer _customerEF = new EFCustomer();
        static EFSupplier _supplierEF = new EFSupplier();
        static EFStore _storeEF = new EFStore();
        static EFItemEy _itemEyEF = new EFItemEy();
        static EFItemHw _itemHwEF = new EFItemHw();
        static EFItemMt _itemMtEF = new EFItemMt();
        static EFStatusProd _statusProdEF = new EFStatusProd();
        static EFStatusCial _statusCialEF = new EFStatusCial();
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
        static EFFamilyHK _familyHKEF = new EFFamilyHK();
        static EFDocType _docTypeEF = new EFDocType();
        static EFItemDoc _itemDocEF = new EFItemDoc();
        static EFPrototype _prototypeEF = new EFPrototype();
        static EFPrototypeDoc _prototypeDocEF = new EFPrototypeDoc();

        static User _loggedUser;
        static IEnumerable<FunctionalityRole> _functionalitiesRoles;

        static ResourceManager _resManager = new ResourceManager(typeof(HKSupply.Resources.HKSupplyRes));
        #endregion

        #region Public Properties
        public static string DbEnvironment
        {
            get { return _dbEnvironment; }
        }

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

        public static IItemEy ItemEyService 
        {
            get 
            {
                if (_itemEyEF == null)
                    return new EFItemEy();
                else
                    return _itemEyEF;
            
            }
        }

        public static IItemHw ItemHwService
        {
            get 
            {
                if (_itemHwEF == null)
                    return new EFItemHw();
                else
                    return _itemHwEF;
            }
        }

        public static IItemMt ItemMtService
        {
            get
            {
                if (_itemMtEF == null)
                    return new EFItemMt();
                else
                    return _itemMtEF;
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

        public static IStatusCial StatusCialService
        {
            get
            {
                if (_statusCialEF == null)
                    return new EFStatusCial();
                else
                    return _statusCialEF;
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
                if (_familyHKEF == null)
                    return new EFFamilyHK();
                else
                    return _familyHKEF;
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

        public static IDocType DocTypeService
        {
            get 
            {
                if (_docTypeEF == null)
                    return new EFDocType();
                else
                    return _docTypeEF;
            }
        }

        public static IItemDoc ItemDocService
        {
            get
            {
                if (_itemDocEF == null)
                    return new EFItemDoc();
                else
                    return _itemDocEF;
            }
        }

        public static IPrototype PrototypeService
        {
            get
            {
                if (_prototypeEF == null)
                    return new EFPrototype();
                else
                    return _prototypeEF;
            }
        }

        public static IPrototypeDoc PrototypeDocService
        {
            get
            {
                if (_prototypeDocEF == null)
                    return new EFPrototypeDoc();
                else
                    return _prototypeDocEF;
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
