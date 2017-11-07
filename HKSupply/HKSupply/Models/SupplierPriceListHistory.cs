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
    [Table("SUPPLIERS_PRICE_LIST_HISTORY")]
    public class SupplierPriceListHistory
    {
        [Column("ID_VER", Order = 0), Key]
        public int IdVer { get; set; }

        [Column("ID_SUBVER", Order = 1), Key]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP", Order = 2), Key]
        public DateTime Timestamp { get; set; }

        [Column("ID_ITEM_BCN", TypeName = "NVARCHAR", Order = 3), Key, StringLength(50)]
        public string IdItemBcn { get; set; }

        [Column("ID_SUPPLIER", TypeName = "NVARCHAR", Order = 4), Key, StringLength(100)]
        public string IdSupplier { get; set; }

        [Column("PRICE", TypeName = "NUMERIC")]
        public Decimal Price { get; set; }

        [Column("COMMENTS", TypeName = "NVARCHAR"), StringLength(2500)]
        public string Comments { get; set; }

        [Column("UNIT_CODE", TypeName = "NVARCHAR"), StringLength(2)]
        public string UnitCode { get; set; }

        [Column("ID_CURRENCY", TypeName = "NVARCHAR"), StringLength(4)]
        public string IdCurrency { get; set; }

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

        [Column("USER"), StringLength(20)]
        public string User { get; set; }

        #region Equal

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            SupplierPriceListHistory supplierPriceListHistory = (SupplierPriceListHistory)obj;

            bool res;

            res = (
                IdVer == supplierPriceListHistory.IdVer &&
                IdSubVer == supplierPriceListHistory.IdSubVer &&
                Timestamp == supplierPriceListHistory.Timestamp &&
                IdItemBcn == supplierPriceListHistory.IdItemBcn &&
                IdSupplier == supplierPriceListHistory.IdSupplier &&
                Price == supplierPriceListHistory.Price &&
                Comments == supplierPriceListHistory.Comments &&
                UnitCode == supplierPriceListHistory.UnitCode &&
                IdCurrency == supplierPriceListHistory.IdCurrency &&
                PriceBaseCurrency == supplierPriceListHistory.PriceBaseCurrency &&
                ExchangeRateUsed == supplierPriceListHistory.ExchangeRateUsed &&
                MinLot == supplierPriceListHistory.MinLot &&
                IncrLot == supplierPriceListHistory.IncrLot &&
                LeadTime == supplierPriceListHistory.LeadTime &&
                User == supplierPriceListHistory.User
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
                LeadTime.GetHashCode() +
                (User == null ? 0 : User.GetHashCode())
                );

            return hashCode;
        }

        #endregion

        public static implicit operator SupplierPriceListHistory(SupplierPriceList spl)
        {
            SupplierPriceListHistory splh = new SupplierPriceListHistory();

            splh.IdVer = spl.IdVer;
            splh.IdSubVer = spl.IdSubVer;
            splh.Timestamp = spl.Timestamp;
            splh.IdItemBcn = spl.IdItemBcn;
            splh.IdSupplier = spl.IdSupplier;
            splh.Price = spl.Price;
            splh.Comments = spl.Comments;
            splh.UnitCode = spl.UnitCode;
            splh.IdCurrency = spl.IdCurrency;
            splh.PriceBaseCurrency = spl.PriceBaseCurrency;
            splh.ExchangeRateUsed = spl.ExchangeRateUsed;
            splh.MinLot = spl.MinLot;
            splh.IncrLot = spl.IncrLot;
            splh.LeadTime = spl.LeadTime;

            return splh;
        }

    }
}
