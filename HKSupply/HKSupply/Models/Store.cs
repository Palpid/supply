using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Models
{
    [Table("STORES")]
    public class Store
    {
        [Column("ID_STORE"), Key]
        public string IdStore { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("ACTIVE")]
        public bool Active { get; set; }

        #region Equal
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            Store store = (Store)obj;

            bool res = (
                IdStore == store.IdStore &&
                Name == store.Name &&
                Active == store.Active);

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode =
                IdStore.GetHashCode() +
                Name.GetHashCode() +
                Active.GetHashCode();

            return hashCode;
        }
        #endregion

    }
}
