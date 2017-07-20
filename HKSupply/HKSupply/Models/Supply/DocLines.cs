using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKSupply.Models.Supply
{
    [Table("DOC_LINES")]
    public class DocLine
    {
        [Column("ID_DOC", TypeName = "NVARCHAR", Order = 0), Key, StringLength(50)]
        public string IdDoc { get; set; }

        public int NumLin { get; set; } //TODO: identity!

        [Column("ID_ITEM_BCN", TypeName = "NVARCHAR"), StringLength(50), Required]
        public string IdItemBcn { get; set; }

        /// <summary>
        /// Debido a que el item  puede ser de diferentes clases, lo defino como object y desde la aplicación ya controlaremos el casteo al tipo 
        /// correspondiente según su item group
        /// </summary>
        [NotMapped]
        public object Item { get; set; }

        [Column("ID_ITEM_GROUP", TypeName = "NVARCHAR"), StringLength(100), Required]
        public string IdItemGroup { get; set; }

        [Column("ID_SUPPLY_STATUS", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdSupplyStatus { get; set; }

        [Column("BATCH", TypeName = "NVARCHAR"), StringLength(50)]
        public string Batch { get; set; }

        [Column("QUANTITY", TypeName = "REAL")]
        public short Quantity { get; set; }

        [Column("QUANTITY_ORIGINAL", TypeName = "REAL")]
        public short QuantityOriginal { get; set; }

        [Column("DELIVERED_QUANTITY", TypeName = "REAL")]
        public short DeliveredQuantity { get; set; }

        [Column("REMARKS", TypeName = "NVARCHAR"), StringLength(5000)]
        public string Remarks { get; set; }

        [Column("UNIT_PRICE", TypeName = "REAL")]
        public short UnitPrice { get; set; }

        [Column("UNIT_PRICE_BASE_CURRENCY", TypeName = "REAL")]
        public short UnitPriceBaseCurrency { get; set; }

        #region Foreign Keys

        [ForeignKey("IdItemGroup")]
        public ItemGroup ItemGroup { get; set; }

        [ForeignKey("IdSupplyStatus")]
        public SupplyStatus SupplyStatus { get; set; }

        #endregion

        #region #Equals
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            DocLine docLine = (DocLine)obj;

            bool res = (
                IdDoc == docLine.IdDoc &&
                NumLin == docLine.NumLin &&
                IdItemBcn == docLine.IdItemBcn && 
                IdItemGroup == docLine.IdItemGroup &&
                IdSupplyStatus == docLine.IdSupplyStatus &&
                Batch == docLine.Batch &&
                Quantity == docLine.Quantity &&
                QuantityOriginal == docLine.QuantityOriginal  &&
                DeliveredQuantity == docLine.DeliveredQuantity &&
                Remarks == docLine.Remarks &&
                UnitPrice == docLine.UnitPrice &&
                UnitPriceBaseCurrency == docLine.UnitPriceBaseCurrency
               );

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode = (
                (IdDoc == null ? 0 : IdDoc.GetHashCode()) + 
                (NumLin.GetHashCode()) + 
                (IdItemBcn == null ? 0 : IdItemBcn.GetHashCode()) +
                (IdItemGroup == null ? 0 : IdItemGroup.GetHashCode()) + 
                (IdSupplyStatus == null ? 0 : IdSupplyStatus.GetHashCode()) + 
                (Batch == null ? 0 : Batch.GetHashCode()) +
                (Quantity.GetHashCode()) + 
                (QuantityOriginal.GetHashCode()) +
                (DeliveredQuantity.GetHashCode()) + 
                (Remarks == null ? 0 : Remarks.GetHashCode()) +
                (UnitPrice.GetHashCode()) + 
                (UnitPriceBaseCurrency.GetHashCode())
            );

            return hashCode;
        }
        #endregion

    }
}
