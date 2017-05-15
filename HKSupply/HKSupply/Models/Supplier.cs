using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Models
{
    [Table("SUPPLIERS")]
    public class Supplier
    {
        [Column("ID_VER"), Required]
        public int IdVer { get; set; }

        [Column("ID_SUBVER"), Required]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP"), Required]
        public DateTime Timestamp { get; set; }

        [Column("ID_SUPPLIER", TypeName = "NVARCHAR"), Key, StringLength(100)]
        public string IdSupplier { get; set; }

        [Column("SUPPLIER_NAME", TypeName = "NVARCHAR"), StringLength(500)]
        public string SupplierName { get; set; }

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

            Supplier supplier = (Supplier)obj;

            bool res = (
                IdVer == supplier.IdVer &&
                IdSubVer == supplier.IdSubVer &&
                Timestamp == supplier.Timestamp &&
                IdSupplier == supplier.IdSupplier &&
                SupplierName == supplier.SupplierName &&
                Active == supplier.Active &&
                VATNum == supplier.VATNum &&
                ShippingAddress == supplier.ShippingAddress &&
                ShippingAddressZh == supplier.ShippingAddressZh &&
                BillingAddress == supplier.BillingAddress &&
                BillingAddressZh == supplier.BillingAddressZh &&
                ContactName == supplier.ContactName &&
                ContactNameZh == supplier.ContactNameZh &&
                ContactPhone == supplier.ContactPhone &&
                Comments == supplier.Comments &&
                IdIncoterm == supplier.IdIncoterm &&
                IdPaymentTerms == supplier.IdPaymentTerms &&
                IdDefaultCurrency == supplier.IdDefaultCurrency
                );

            return res;

        }

        public override int GetHashCode()
        {
            int hashCode =
                IdVer.GetHashCode() + 
                IdSubVer.GetHashCode() + 
                Timestamp.GetHashCode() + 
                (IdSupplier == null ? 0 : IdSupplier.GetHashCode()) +
                (SupplierName == null ? 0 : SupplierName.GetHashCode()) + 
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

        public static implicit operator Supplier(SupplierHistory sh)
        {
            Supplier s = new Supplier();

            s.IdVer = sh.IdVer;
            s.IdSubVer = sh.IdSubVer;
            s.Timestamp = sh.Timestamp;
            s.IdSupplier = sh.IdSupplier;
            s.SupplierName = sh.SupplierName;
            s.Active = sh.Active;
            s.VATNum = sh.VATNum;
            s.ShippingAddress = sh.ShippingAddress;
            s.ShippingAddressZh = sh.ShippingAddressZh;
            s.BillingAddress = sh.BillingAddress;
            s.BillingAddressZh = sh.BillingAddressZh;
            s.ContactName = sh.ContactName;
            s.ContactNameZh = sh.ContactNameZh;
            s.ContactPhone = sh.ContactPhone;
            s.Comments = sh.Comments;
            s.IdIncoterm = sh.IdIncoterm;
            s.IdPaymentTerms = sh.IdPaymentTerms;
            s.IdDefaultCurrency = s.IdDefaultCurrency;

            return s;
        }
    }
}
