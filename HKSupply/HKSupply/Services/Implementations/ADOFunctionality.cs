using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKSupply.Services.Interfaces;
using HKSupply.Helpers.Mocking;
using HKSupply.Exceptions;
using HKSupply.Models;

namespace HKSupply.Services.Implementations
{
    public class ADOFunctionality : IFunctionality
    {
        public Functionality GetFunctionalityByIdRol(string functionalityId, string roleId)
        {
            try
            {
                if (functionalityId == null)
                    throw new ArgumentNullException("functionalityId");

                if (roleId == null)
                    throw new ArgumentNullException("roleId");

                var functionality = MockData.FunctionalitiesList.FirstOrDefault(f => f.FunctionalityName.Equals(functionalityId) &&
                    f.Role.RoleId.Equals(roleId));

                if (functionality == null)
                    throw new NonexistentFunctionalityException();

                return functionality;

            }
            catch (ArgumentNullException anex)
            {
                throw anex;
            }
            catch (NonexistentFunctionalityException nefex)
            {
                throw nefex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Functionality NewFunctionality(Functionality newFunctionality)
        {
            try
            {
                if (newFunctionality == null)
                    throw new ArgumentNullException();

                var funcionalidad = MockData.FunctionalitiesList.FirstOrDefault(f => f.FunctionalityName.Equals(newFunctionality.FunctionalityName)
                    && f.Role.RoleId.Equals(newFunctionality.Role.RoleId));

                if (funcionalidad != null)
                    throw new NewExistingFunctionalityException();
                
                MockData.FunctionalitiesList.Add(newFunctionality);

                return GetFunctionalityByIdRol(newFunctionality.FunctionalityName, newFunctionality.Role.RoleId);
            }
            catch (ArgumentNullException anex)
            {
                throw anex;
            }
            catch (NewExistingFunctionalityException afeex)
            {
                throw afeex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public Functionality ModifyFunctionality(Functionality modFunctionality)
        {
            try
            {
                if (modFunctionality == null)
                    throw new ArgumentNullException();

                var funcionalidad = MockData.FunctionalitiesList.FirstOrDefault(f => f.FunctionalityName.Equals(modFunctionality.FunctionalityName)
                    && f.Role.RoleId.Equals(modFunctionality.Role.RoleId));

                if (funcionalidad == null)
                    throw new NonexistentFunctionalityException();

                funcionalidad.Read = modFunctionality.Read;
                funcionalidad.Modify = modFunctionality.Modify;

                return funcionalidad;

            }
            catch (ArgumentNullException anex)
            {
                throw anex;
            }
            catch (NonexistentFunctionalityException nefex)
            {
                throw nefex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
