using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Helpers
{
    /// <summary>
    /// Helpers para Entity Framework
    /// </summary>
    public class EntityFrameworkHelper
    {
        /// <summary>
        /// Devuelve el objeto POCO de un proxy generado por EF
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="proxyObject"></param>
        /// <returns></returns>
        /// <remarks>
        /// Cuando se hace una consulta de EF, realmente no se devuelve el objeto "model", sino uno heredado de este
        /// que aparte incluye todas las funcionalidades de EF (tracking de los cambios del objeto por si queremos modificarlo
        /// en la db, lazy loading, etc.
        /// </remarks>
        public static T UnProxy<T>(DbContext context, T proxyObject) where T : class
        {
            var proxyCreationEnabled = context.Configuration.ProxyCreationEnabled;
            try
            {
                context.Configuration.ProxyCreationEnabled = false;
                T poco = context.Entry(proxyObject).CurrentValues.ToObject() as T;
                return poco;
            }
            finally
            {
                context.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
            }
        }
    }
}
