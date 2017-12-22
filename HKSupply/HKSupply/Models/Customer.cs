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
    [Table("CUSTOMERS")]
    public class Customer
    {
        [Column("ID_VER"), Required]
        public int IdVer { get; set; }

        [Column("ID_SUBVER"), Required]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP"), Required]
        public DateTime Timestamp { get; set; }

        [Column("ID_CUSTOMER", TypeName = "NVARCHAR"), Key, StringLength(100)]
        public string IdCustomer { get; set; }

        [Column("CUSTOMER_NAME", TypeName = "NVARCHAR"), StringLength(500)]
        public string CustomerName { get; set; }

        [Column("ACTIVE")]
        public bool Active { get; set; }

        [Column("VAT_NUM", TypeName = "NVARCHAR"), StringLength(100)]
        public string VATNum { get; set; }

        [Column("SHIPING_ADDRESS", TypeName = "NVARCHAR"), StringLength(2500)]
        public string ShippingAddress { get; set; }

        [Column("SHIPING_ADDRESS_2", TypeName = "NVARCHAR"), StringLength(2500)]
        public string ShippingAddress2 { get; set; }

        [Column("SHIPING_ADDRESS_ZH", TypeName = "NVARCHAR"), StringLength(2500)]
        public string ShippingAddressZh { get; set; }

        [Column("SHIPING_ADDRESS_ZH_2", TypeName = "NVARCHAR"), StringLength(2500)]
        public string ShippingAddressZh2 { get; set; }

        [Column("BILLING_ADDRESS", TypeName = "NVARCHAR"), StringLength(2500)]
        public string BillingAddress { get; set; }

        [Column("BILLING_ADDRESS_2", TypeName = "NVARCHAR"), StringLength(2500)]
        public string BillingAddress2 { get; set; }

        [Column("BILLING_ADDRESS_ZH", TypeName = "NVARCHAR"), StringLength(2500)]
        public string BillingAddressZh { get; set; }

        [Column("BILLING_ADDRESS_ZH_2", TypeName = "NVARCHAR"), StringLength(2500)]
        public string BillingAddressZh2 { get; set; }

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

        [Column("FACTORY")]
        public bool Factory { get; set; }

        [ForeignKey("IdIncoterm")]
        public Incoterm Incoterm { get; set; }
        [ForeignKey("IdPaymentTerms")]
        public PaymentTerms PaymentTerms { get; set; }
        [ForeignKey("IdDefaultCurrency")]
        public Currency DefaultCurrency { get; set; }

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
                ShippingAddress2 == customer.ShippingAddress2 &&
                ShippingAddressZh == customer.ShippingAddressZh &&
                ShippingAddressZh2 == customer.ShippingAddressZh2 &&
                BillingAddress == customer.BillingAddress &&
                BillingAddress2 == customer.BillingAddress2 &&
                BillingAddressZh == customer.BillingAddressZh &&
                BillingAddressZh2 == customer.BillingAddressZh2 &&
                ContactName == customer.ContactName &&
                ContactNameZh == customer.ContactNameZh &&
                ContactPhone == customer.ContactPhone &&
                Comments == customer.Comments &&
                IdIncoterm == customer.IdIncoterm &&
                IdPaymentTerms == customer.IdPaymentTerms &&
                IdDefaultCurrency == customer.IdDefaultCurrency) &&
                Factory == customer.Factory;

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
                (ShippingAddress2 == null ? 0 : ShippingAddress2.GetHashCode()) +
                (ShippingAddressZh == null ? 0 : ShippingAddressZh.GetHashCode()) +
                (ShippingAddressZh2 == null ? 0 : ShippingAddressZh2.GetHashCode()) +
                (BillingAddress == null ? 0 : BillingAddress.GetHashCode()) +
                (BillingAddress2 == null ? 0 : BillingAddress2.GetHashCode()) +
                (BillingAddressZh == null ? 0 : BillingAddressZh.GetHashCode()) +
                (BillingAddressZh2 == null ? 0 : BillingAddressZh2.GetHashCode()) +
                (ContactName == null ? 0 : ContactName.GetHashCode()) + 
                (ContactNameZh == null ? 0 : ContactNameZh.GetHashCode()) + 
                (ContactPhone == null ? 0 : ContactPhone.GetHashCode()) + 
                (Comments == null ? 0 : Comments.GetHashCode()) + 
                (IdIncoterm == null ? 0 : IdIncoterm.GetHashCode()) + 
                (IdPaymentTerms == null ? 0 : IdPaymentTerms.GetHashCode()) + 
                (IdDefaultCurrency == null ? 0 : IdDefaultCurrency.GetHashCode()) +
                Factory.GetHashCode();

            return hashCode;
        }
        #endregion

        public static implicit operator Customer(CustomerHistory ch)
        {
            Customer c = new Customer();

            c.IdVer = ch.IdVer;
            c.IdSubVer = ch.IdSubVer;
            c.Timestamp = ch.Timestamp;
            c.IdCustomer = ch.IdCustomer;
            c.CustomerName = ch.CustomerName;
            c.Active = ch.Active;
            c.VATNum = ch.VATNum;
            c.ShippingAddress = ch.ShippingAddress;
            c.ShippingAddress2 = ch.ShippingAddress2;
            c.ShippingAddressZh = ch.ShippingAddressZh;
            c.ShippingAddressZh2 = ch.ShippingAddressZh2;
            c.BillingAddress = ch.BillingAddress;
            c.BillingAddress2 = ch.BillingAddress2;
            c.BillingAddressZh = ch.BillingAddressZh;
            c.BillingAddressZh2 = ch.BillingAddressZh2;
            c.ContactName = ch.ContactName;
            c.ContactNameZh = ch.ContactNameZh;
            c.ContactPhone = ch.ContactPhone;
            c.Comments = ch.Comments;
            c.IdIncoterm = ch.IdIncoterm;
            c.IdPaymentTerms = ch.IdPaymentTerms;
            c.IdDefaultCurrency = ch.IdDefaultCurrency;
            c.Factory = ch.Factory;

            return c;
        }
    }
}
