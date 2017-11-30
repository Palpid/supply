using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSupply.Models.Supply
{
    [Table("PACKING_LIST_ITEM_BATCH")]
    public class PackingListItemBatch
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

        [Column("BATCH", TypeName = "NVARCHAR", Order = 4), Key, StringLength(50)]
        public string Batch { get; set; }

        [Column("QUANTITY", TypeName = "NUMERIC")]
        public decimal Quantity { get; set; }

        #region Foreign Keys

        [ForeignKey("IdItemGroup")]
        public ItemGroup ItemGroup { get; set; }

        #endregion

        #region Equals

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            PackingListItemBatch packingListItemBatch = (PackingListItemBatch)obj;
            bool res = (
                IdDoc == packingListItemBatch.IdDoc &&
                IdDocRelated == packingListItemBatch.IdDocRelated &&
                IdItemBcn == packingListItemBatch.IdItemBcn &&
                IdItemGroup == packingListItemBatch.IdItemGroup &&
                Batch == packingListItemBatch.Batch &&
                Quantity == Quantity
                );

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode =
            (IdDoc == null ? 0 : IdDoc.GetHashCode()) +
            (IdDocRelated == null ? 0 : IdDocRelated.GetHashCode()) +
            (IdItemBcn == null ? 0 : IdItemBcn.GetHashCode()) +
            (IdItemGroup == null ? 0 : IdItemGroup.GetHashCode()) +
            (Batch == null ? 0 : Batch.GetHashCode()) +
            Quantity.GetHashCode();

            return hashCode;
        }

        #endregion
    }
}
