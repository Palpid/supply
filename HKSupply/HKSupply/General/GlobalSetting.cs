using HKSupply.Models;
using HKSupply.Services.Implementations;
using HKSupply.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.General
{
    public sealed class GlobalSetting
    {
        #region Private Members
        static EFRole _roleEF ;
        static EFUser _userEF;
        static EFFunctionality _functionalityEF;
        static EFFunctionalityRole _functionalityRoleEF;

        static User _loggedUser;

        #endregion

        #region Public Properties
        public static IRole RoleCont 
        {
            get 
            {
                if (_roleEF == null)
                    return new EFRole();
                else
                    return _roleEF;
            }

        }
        
        public static IUser UserCont 
        {
            get 
            {
                if (_userEF == null)
                    return new EFUser();
                else
                    return _userEF;

            }
        }
        
        public static IFunctionality FunctionalityCont 
        {
            get 
            {
                if (_functionalityEF == null)
                    return new EFFunctionality();
                else
                    return _functionalityEF;
            }
        }
        
        public static IFunctionalityRole FunctionalityRoleCont 
        {
            get 
            {
                if (_functionalityRoleEF == null)
                    return new EFFunctionalityRole();
                else
                    return _functionalityRoleEF;
            }
        }

        public static User LoggedUser 
        {
            get { return _loggedUser; }
            set { _loggedUser = value; }
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
