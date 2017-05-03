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
    [Table("CUSTOMERS_PRICE_LIST")]
    public class CustomerPriceList
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

        [Column("ID_CUSTOMER", TypeName = "NVARCHAR", Order = 1), Key, StringLength(100)]
        public string IdCustomer { get; set; }
        [ForeignKey("IdCustomer")]
        public Customer Customer { get; set; }

        [Column("PRICE", TypeName = "NUMERIC")]
        public Decimal Price { get; set; }

        [Column("COMMENTS", TypeName = "NVARCHAR"), StringLength(2500)]
        public string Comments { get; set; }

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

            CustomerPriceList customerPriceList = (CustomerPriceList)obj;

            bool res;

            res = (
                IdVer == customerPriceList.IdVer &&
                IdSubVer == customerPriceList.IdSubVer &&
                Timestamp == customerPriceList.Timestamp &&
                IdItemBcn == customerPriceList.IdItemBcn &&
                IdCustomer == customerPriceList.IdCustomer &&
                Price == customerPriceList.Price &&
                Comments == customerPriceList.Comments &&
                IdCurrency == customerPriceList.IdCurrency &&
                PriceBaseCurrency == customerPriceList.PriceBaseCurrency &&
                ExchangeRateUsed == customerPriceList.ExchangeRateUsed &&
                MinLot == customerPriceList.MinLot &&
                IncrLot == customerPriceList.IncrLot &&
                LeadTime == customerPriceList.LeadTime
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
                LeadTime.GetHashCode()
                );

            return hashCode;
        }
        #endregion

        public static implicit operator CustomerPriceList(CustomerPriceListHistory cplh)
        {
            CustomerPriceList cpl = new CustomerPriceList();

            cpl.IdVer = cplh.IdVer;
            cpl.IdSubVer = cplh.IdSubVer;
            cpl.Timestamp = cplh.Timestamp;
            cpl.IdItemBcn = cplh.IdItemBcn;
            cpl.IdCustomer = cplh.IdCustomer;
            cpl.Price = cplh.Price;
            cpl.Comments = cplh.Comments;
            cpl.IdCurrency = cplh.IdCurrency;
            cpl.PriceBaseCurrency = cplh.PriceBaseCurrency;
            cpl.ExchangeRateUsed = cplh.ExchangeRateUsed;
            cpl.MinLot = cplh.MinLot;
            cpl.IncrLot = cplh.IncrLot;
            cpl.LeadTime = cplh.LeadTime;

            return cpl;
        }

    }
}
