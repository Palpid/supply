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
    [Table("CUSTOMERS_HISTORY")]
    public class CustomerHistory 
    {
        [Column("ID_VER", Order = 1), Key]
        public int IdVer { get; set; }

        [Column("ID_SUBVER", Order = 2), Key]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP", Order = 3), Key]
        public DateTime Timestamp { get; set; }

        [Column("ID_CUSTOMER", TypeName = "NVARCHAR", Order = 0), Key, StringLength(100)]
        public string IdCustomer { get; set; }

        [Column("CUSTOMER_NAME", TypeName = "NVARCHAR"), StringLength(500)]
        public string CustomerName { get; set; }

        [Column("ACTIVE")]
        public bool Active { get; set; }

        [Column("VAT_NUM", TypeName = "NVARCHAR"), StringLength(100)]
        public string VATNum { get; set; }

        [Column("SHIPING_ADDRESS", TypeName = "NVARCHAR"), StringLength(2500)]
        public string ShippingAddress { get; set; }

        [Column("SHIPING_ADDRESS_ZH", TypeName = "NVARCHAR"), StringLength(2500)]
        public string ShippingAddressZh { get; set; }

        [Column("BILLING_ADDRESS", TypeName = "NVARCHAR"), StringLength(2500)]
        public string BillingAddress { get; set; }

        [Column("BILLING_ADDRESS_ZH", TypeName = "NVARCHAR"), StringLength(2500)]
        public string BillingAddressZh { get; set; }

        [Column("CONTACT_NAME", TypeName = "NVARCHAR"), StringLength(100)]
        public string ContactName { get; set; }

        [Column("CONTACT_NAME_ZH", TypeName = "NVARCHAR"), StringLength(100)]
        public string ContactNameZh { get; set; }

        [Column("CONTACT_PHONE", TypeName = "NVARCHAR"), StringLength(100)]
        public string ContactPhone { get; set; }

        [Column("COMMENTS", TypeName = "NVARCHAR"), StringLength(2500)]
        public string Comments { get; set; }

        [Column("ID_INCOTERM", TypeName = "NVARCHAR"), StringLength(8)]
        public string IdIncoterm { get; set; }

        [Column("ID_PAYMENT_TERMS", TypeName = "NVARCHAR"), StringLength(4)]
        public string IdPaymentTerms { get; set; }

        [Column("ID_DEFAULT_CURRENCY", TypeName = "NVARCHAR"), StringLength(4)]
        public string IdDefaultCurrency { get; set; }

/*
        [Column("SHIPING_ADDRESS")]
        public string ShippingAddress { get; set; }

        [Column("BILLING_ADDRESS")]
        public string BillingAddress { get; set; }

        [Column("CONTACT_NAME")]
        public string ContactName { get; set; }

        [Column("CONTACT_PHONE")]
        public string ContactPhone { get; set; }

        [Column("ID_INCOTERM")]
        public int IdIncoterm { get; set; }

        [Column("IDPAYMENTTERMS")]
        public int IdPaymentTerms { get; set; }

        [Column("CURRENCY")]
        public string Currency { get; set; }
*/
        #region Equal
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            Customer customer = (Customer)obj;

            bool res = (
                IdVer == customer.IdVer &&
                IdSubVer == customer.IdSubVer &&
                Timestamp == customer.Timestamp &&
                IdCustomer == customer.IdCustomer &&
                CustomerName == customer.CustomerName &&
                Active == customer.Active &&
                VATNum == customer.VATNum &&
                ShippingAddress == customer.ShippingAddress &&
                ShippingAddressZh == customer.ShippingAddressZh &&
                BillingAddress == customer.BillingAddress &&
                BillingAddressZh == customer.BillingAddressZh &&
                ContactName == customer.ContactName &&
                ContactNameZh == customer.ContactNameZh &&
                ContactPhone == customer.ContactPhone &&
                Comments == customer.Comments &&
                IdIncoterm == customer.IdIncoterm &&
                IdPaymentTerms == customer.IdPaymentTerms &&
                IdDefaultCurrency == customer.IdDefaultCurrency);

            return res;

        }

        public override int GetHashCode()
        {
            int hashCode =
                IdVer.GetHashCode() +
                IdSubVer.GetHashCode() +
                Timestamp.GetHashCode() +
                (IdCustomer == null ? 0 : IdCustomer.GetHashCode()) +
                (CustomerName == null ? 0 : CustomerName.GetHashCode()) +
                Active.GetHashCode() +
                (VATNum == null ? 0 : VATNum.GetHashCode()) +
                (ShippingAddress == null ? 0 : ShippingAddress.GetHashCode()) +
                (ShippingAddressZh == null ? 0 : ShippingAddressZh.GetHashCode()) +
                (BillingAddress == null ? 0 : BillingAddress.GetHashCode()) +
                (BillingAddressZh == null ? 0 : BillingAddressZh.GetHashCode()) +
                (ContactName == null ? 0 : ContactName.GetHashCode()) +
                (ContactNameZh == null ? 0 : ContactNameZh.GetHashCode()) +
                (ContactPhone == null ? 0 : ContactPhone.GetHashCode()) +
                (Comments == null ? 0 : Comments.GetHashCode()) +
                (IdIncoterm == null ? 0 : IdIncoterm.GetHashCode()) +
                (IdPaymentTerms == null ? 0 : IdPaymentTerms.GetHashCode()) +
                (IdDefaultCurrency == null ? 0 : IdDefaultCurrency.GetHashCode());

            return hashCode;
        }
        #endregion

        public static implicit operator CustomerHistory(Customer c)
        {
            CustomerHistory ch = new CustomerHistory();

            ch.IdVer = c.IdVer;
            ch.IdSubVer = c.IdSubVer;
            ch.Timestamp = c.Timestamp;
            ch.IdCustomer = c.IdCustomer;
            ch.CustomerName = c.CustomerName;
            ch.Active = c.Active;
            ch.VATNum = c.VATNum;
            ch.ShippingAddress = c.ShippingAddress;
            ch.ShippingAddressZh = c.ShippingAddressZh;
            ch.BillingAddress = c.BillingAddress;
            ch.BillingAddressZh = c.BillingAddressZh;
            ch.ContactName = c.ContactName;
            ch.ContactNameZh = c.ContactNameZh;
            ch.ContactPhone = c.ContactPhone;
            ch.Comments = c.Comments;
            ch.IdIncoterm = c.IdIncoterm;
            ch.IdPaymentTerms = c.IdPaymentTerms;
            ch.IdDefaultCurrency = c.IdDefaultCurrency;

            return ch;
        }
    }
}
