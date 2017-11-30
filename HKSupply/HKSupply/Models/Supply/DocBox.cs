using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSupply.Models.Supply
{
    [Table("DOC_BOXES")]
    public class DocBox
    {
        [Column("ID_DOC", TypeName = "NVARCHAR", Order = 0), Key, StringLength(50)]
        public string IdDoc { get; set; }

        [Column("BOX_NUMBER", Order = 1), Key]
        public int BoxNumber { get; set; }

        [Column("ID_BOX", TypeName = "NVARCHAR"), StringLength(50), Required]
        public string IdBox { get; set; }

        [Column("NET_WEIGHT", TypeName = "NUMERIC"), Required]
        public decimal NetWeight { get; set; }

        [Column("GROSS_WEIGHT", TypeName = "NUMERIC"), Required]
        public decimal GrossWeight { get; set; }

        #region Foreign keys

        [ForeignKey("IdBox")]
        public Box Box { get; set; }

        #endregion

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            DocBox docBox = (DocBox)obj;
            bool res = (
                IdDoc == docBox.IdDoc &&
                BoxNumber == docBox.BoxNumber &&
                IdBox == docBox.IdBox &&
                NetWeight == docBox.NetWeight &&
                GrossWeight == docBox.GrossWeight
                );

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode =
              (IdDoc == null ? 0 : IdDoc.GetHashCode()) +
              BoxNumber.GetHashCode() +
              (IdBox == null ? 0 : IdBox.GetHashCode()) +
              NetWeight.GetHashCode() +
              GrossWeight.GetHashCode();

            return hashCode;
        }
        #endregion
    }
}
