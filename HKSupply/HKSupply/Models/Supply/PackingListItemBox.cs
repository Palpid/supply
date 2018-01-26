using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSupply.Models.Supply
{
    [Table("PACKING_LIST_ITEM_BOX")]
    public class PackingListItemBox
    {
        [Column("ID_DOC", TypeName = "NVARCHAR", Order = 0), Key, StringLength(50)]
        public string IdDoc { get; set; }

        [Column("ID_DOC_RELATED", TypeName = "NVARCHAR", Order = 1), Key, StringLength(50)]
        public string IdDocRelated { get; set; }

        [Column("ID_ITEM_BCN", TypeName = "NVARCHAR", Order = 2), Key, StringLength(50)]
        public string IdItemBcn { get; set; }

        /// <summary>
        /// Debido a que el item  puede ser de diferentes clases, lo defino como object y desde la aplicación ya controlaremos el casteo al tipo 
        /// correspondiente según su item group
        /// </summary>
        [NotMapped]
        public object Item { get; set; }

        [Column("ID_ITEM_GROUP", TypeName = "NVARCHAR", Order = 3), Key, StringLength(100)]
        public string IdItemGroup { get; set; }

        [Column("BOX_NUMBER", Order = 4), Key]
        public int BoxNumber { get; set; }

        [Column("PC_QUANTITY", TypeName = "NUMERIC")]
        public decimal PcQuantity { get; set; }


        [Column("NET_WEIGHT", TypeName = "NUMERIC"), Required]
        public decimal NetWeight { get; set; }

        [Column("GROSS_WEIGHT", TypeName = "NUMERIC"), Required]
        public decimal GrossWeight { get; set; }

        #region Foreign Keys

        [ForeignKey("IdItemGroup")]
        public ItemGroup ItemGroup { get; set; }

        #endregion

        #region Equals

        public override bool Equals(object obj)
        {
            try
            {
                if (obj == null || obj == DBNull.Value)
                    return false;

                PackingListItemBox packingListItemBox = (PackingListItemBox)obj;

                bool res = (
                    IdDoc == packingListItemBox.IdDoc &&
                    IdDocRelated == packingListItemBox.IdDocRelated &&
                    IdItemBcn == packingListItemBox.IdItemBcn &&
                    IdItemGroup == packingListItemBox.IdItemGroup &&
                    BoxNumber == packingListItemBox.BoxNumber &&
                    PcQuantity == packingListItemBox.PcQuantity &&
                    NetWeight == packingListItemBox.NetWeight &&
                    GrossWeight == packingListItemBox.GrossWeight
                    );

                return res;

            }
            catch
            {
                throw;
            }
        }

        public override int GetHashCode()
        {
            try
            {
                int hashCode =
                    (IdDoc == null ? 0 : IdDoc.GetHashCode()) +
                    (IdDocRelated == null ? 0 : IdDocRelated.GetHashCode()) +
                    (IdItemBcn == null ? 0 : IdItemBcn.GetHashCode()) +
                    (IdItemGroup == null ? 0 : IdItemGroup.GetHashCode()) +
                    BoxNumber.GetHashCode() +
                    PcQuantity.GetHashCode() +
                    NetWeight.GetHashCode() +
                    GrossWeight.GetHashCode();

                return hashCode;
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
