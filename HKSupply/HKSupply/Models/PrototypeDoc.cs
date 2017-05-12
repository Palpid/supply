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
    [Table("PROTOTYPES_DOCS")]
    public class PrototypeDoc
    {
        [Column("ID_DOC"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDoc { get; set; }

        [Column("ID_PROTOTYPE", TypeName = "NVARCHAR"), StringLength(50)]
        public string IdPrototype { get; set; }

        [Column("ID_DOC_TYPE", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdDocType { get; set; }
        [ForeignKey("IdDocType")]
        public DocType DocType { get; set; }

        [Column("FILE_NAME", TypeName = "NVARCHAR"), StringLength(100)]
        public string FileName { get; set; }
        [Column("FILE_PATH", TypeName = "NVARCHAR"), StringLength(500)]
        public string FilePath { get; set; }

        [Column("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        #region Equals

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            PrototypeDoc prototypeDoc = (PrototypeDoc)obj;

            bool res = (
               IdDoc == prototypeDoc.IdDoc &&
               IdPrototype == prototypeDoc.IdPrototype &&
               IdDocType == prototypeDoc.IdDocType &&
               FileName == prototypeDoc.FileName &&
               FilePath == prototypeDoc.FilePath &&
               CreateDate == prototypeDoc.CreateDate);

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode = (
               IdDoc.GetHashCode() +
               (IdPrototype == null ? 0 : IdPrototype.GetHashCode()) +
               (IdDocType == null ? 0 : IdDocType.GetHashCode()) +
               (FileName == null ? 0 : FileName.GetHashCode()) +
               (FilePath == null ? 0 : FilePath.GetHashCode()) +
               CreateDate.GetHashCode());

            return hashCode;
        }

        #endregion
    }
}
