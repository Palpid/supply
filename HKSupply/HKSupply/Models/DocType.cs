using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Models
{
    [Table("DOCS_TYPES")]
    public class DocType
    {
        [Column("ID_DOC_TYPE", TypeName = "NVARCHAR"), Key, StringLength(100)]
        public string IdDocType { get; set; }
        [Column("Description", TypeName = "NVARCHAR"), StringLength(500)]
        public string Description { get; set; }

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            DocType docType = (DocType)obj;
            bool res = (
                IdDocType == docType.IdDocType &&
                Description == docType.Description);
            
            return res;
        }

        public override int GetHashCode()
        {
            int hashCode =
                (IdDocType == null ? 0 : IdDocType.GetHashCode()) +
                (Description == null ? 0 : Description.GetHashCode());

            return hashCode;
        }
        #endregion
    }
}
