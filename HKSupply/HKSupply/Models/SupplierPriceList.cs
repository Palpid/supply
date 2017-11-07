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
    [Table("SUPPLIERS_PRICE_LIST")]
    public class SupplierPriceList
    {
        [Column("ID_VER"), Required]
        public int IdVer { get; set; }

        [Column("ID_SUBVER"), Required]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP"), Required]
        public DateTime Timestamp { get; set; }

        [Column("ID_ITEM_BCN", TypeName = "NVARCHAR", Order = 0), Key, StringLength(50)]
        public string IdItemBcn { get; set; }
        [ForeignKey("IdItemBcn")]
        public ItemBcn ItemBcn { get; set; }

        [Column("ID_SUPPLIER", TypeName = "NVARCHAR", Order = 1), Key, StringLength(100)]
        public string IdSupplier { get; set; }
        [ForeignKey("IdSupplier")]
        public Supplier Supplier { get; set; }

        [Column("PRICE", TypeName = "NUMERIC")]
        public Decimal Price { get; set; }

        [Column("COMMENTS", TypeName = "NVARCHAR"), StringLength(2500)]
        public string Comments { get; set; }

        [Column("UNIT_CODE", TypeName = "NVARCHAR"), StringLength(2)]
        public string UnitCode { get; set; }
        [ForeignKey("UnitCode")]
        public Unit Unit { get; set; }

        [Column("ID_CURRENCY", TypeName = "NVARCHAR"), StringLength(4)]
        public string IdCurrency { get; set; }
        [ForeignKey("IdCurrency")]
        public Currency Currency { get; set; }

        [Column("PRICE_BASE_CURRENCY", TypeName = "NUMERIC")]
        public Decimal PriceBaseCurrency { get; set; }

        [Column("EXCHANGE_RATE_USED", TypeName = "NUMERIC")]
        public Decimal ExchangeRateUsed { get; set; }

        [Column("MIN_LOT")]
        public int MinLot { get; set; }

        [Column("INCR_LOT")]
        public int IncrLot { get; set; }

        [Column("LEAD_TIME", TypeName = "REAL")]
        public float LeadTime { get; set; }

        #region equal
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            SupplierPriceList supplierPriceList = (SupplierPriceList)obj;

            bool res;

            res = (
                IdVer == supplierPriceList.IdVer &&
                IdSubVer == supplierPriceList.IdSubVer &&
                Timestamp == supplierPriceList.Timestamp &&
                IdItemBcn == supplierPriceList.IdItemBcn &&
                IdSupplier == supplierPriceList.IdSupplier &&
                Price == supplierPriceList.Price &&
                Comments == supplierPriceList.Comments &&
                UnitCode == supplierPriceList.UnitCode &&
                IdCurrency == supplierPriceList.IdCurrency &&
                PriceBaseCurrency == supplierPriceList.PriceBaseCurrency &&
                ExchangeRateUsed == supplierPriceList.ExchangeRateUsed &&
                MinLot == supplierPriceList.MinLot &&
                IncrLot == supplierPriceList.IncrLot &&
                LeadTime == supplierPriceList.LeadTime 
                );

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode;

            hashCode = (
                IdVer.GetHashCode() + 
                IdSubVer.GetHashCode() + 
                Timestamp.GetHashCode() + 
                (IdItemBcn == null ? 0 : IdItemBcn.GetHashCode()) + 
                (IdSupplier == null ? 0 : IdSupplier.GetHashCode()) + 
                Price.GetHashCode() + 
                (Comments == null ? 0 : Comments.GetHashCode()) +
                (UnitCode == null ? 0 : UnitCode.GetHashCode()) +
                (IdCurrency == null ? 0 : IdCurrency.GetHashCode()) + 
                PriceBaseCurrency.GetHashCode() + 
                ExchangeRateUsed.GetHashCode() + 
                MinLot.GetHashCode() +
                IncrLot.GetHashCode() + 
                LeadTime.GetHashCode() 
                );

            return hashCode;
        }
        #endregion

        public static implicit operator SupplierPriceList(SupplierPriceListHistory splh)
        {
            SupplierPriceList spl = new SupplierPriceList();

            spl.IdVer = splh.IdVer;
            spl.IdSubVer = splh.IdSubVer;
            spl.Timestamp = splh.Timestamp;
            spl.IdItemBcn = splh.IdItemBcn;
            spl.IdSupplier = splh.IdSupplier;
            spl.Price = splh.Price;
            spl.Comments = splh.Comments;
            spl.IdCurrency = splh.IdCurrency;
            spl.UnitCode = splh.UnitCode;
            spl.PriceBaseCurrency = splh.PriceBaseCurrency;
            spl.ExchangeRateUsed = splh.ExchangeRateUsed;
            spl.MinLot = splh.MinLot;
            spl.IncrLot = splh.IncrLot;
            spl.LeadTime = splh.LeadTime;

            return spl;
        }

    }
}
