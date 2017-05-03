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
    [Table("CUSTOMER_PRICE_LIST_HISTORY")]
    public class CustomerPriceListHistory
    {
        [Column("ID_VER", Order = 0), Key]
        public int IdVer { get; set; }

        [Column("ID_SUBVER", Order = 1), Key]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP", Order = 2), Key]
        public DateTime Timestamp { get; set; }

        [Column("ID_ITEM_BCN", TypeName = "NVARCHAR", Order = 3), Key, StringLength(50)]
        public string IdItemBcn { get; set; }

        [Column("ID_CUSTOMER", TypeName = "NVARCHAR", Order = 4), Key, StringLength(100)]
        public string IdCustomer { get; set; }

        [Column("PRICE", TypeName = "NUMERIC")]
        public Decimal Price { get; set; }

        [Column("COMMENTS", TypeName = "NVARCHAR"), StringLength(2500)]
        public string Comments { get; set; }

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

            CustomerPriceListHistory customerPriceListHistory = (CustomerPriceListHistory)obj;

            bool res;

            res = (
                IdVer == customerPriceListHistory.IdVer &&
                IdSubVer == customerPriceListHistory.IdSubVer &&
                Timestamp == customerPriceListHistory.Timestamp &&
                IdItemBcn == customerPriceListHistory.IdItemBcn &&
                IdCustomer == customerPriceListHistory.IdCustomer &&
                Price == customerPriceListHistory.Price &&
                Comments == customerPriceListHistory.Comments &&
                IdCurrency == customerPriceListHistory.IdCurrency &&
                PriceBaseCurrency == customerPriceListHistory.PriceBaseCurrency &&
                ExchangeRateUsed == customerPriceListHistory.ExchangeRateUsed &&
                MinLot == customerPriceListHistory.MinLot &&
                IncrLot == customerPriceListHistory.IncrLot &&
                LeadTime == customerPriceListHistory.LeadTime &&
                User == customerPriceListHistory.User
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
                (IdCustomer == null ? 0 : IdCustomer.GetHashCode()) +
                Price.GetHashCode() +
                (Comments == null ? 0 : Comments.GetHashCode()) +
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

        public static implicit operator CustomerPriceListHistory(CustomerPriceList cpl)
        {
            CustomerPriceListHistory cplh = new CustomerPriceListHistory();

            cplh.IdVer = cpl.IdVer;
            cplh.IdSubVer = cpl.IdSubVer;
            cplh.Timestamp = cpl.Timestamp;
            cplh.IdItemBcn = cpl.IdItemBcn;
            cplh.IdCustomer = cpl.IdCustomer;
            cplh.Price = cpl.Price;
            cplh.Comments = cpl.Comments;
            cplh.IdCurrency = cpl.IdCurrency;
            cplh.PriceBaseCurrency = cpl.PriceBaseCurrency;
            cplh.ExchangeRateUsed = cpl.ExchangeRateUsed;
            cplh.MinLot = cpl.MinLot;
            cplh.IncrLot = cpl.IncrLot;
            cplh.LeadTime = cpl.LeadTime;

            return cplh;
        }
    }
}
