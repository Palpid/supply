using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Models
{
    [Table("SUPPLIERS_HISTORY")]
    public class SupplierHistory
    {
        [Column("ID_VER", Order = 1), Key]
        public int IdVer { get; set; }

        [Column("ID_SUBVER", Order = 2), Key]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP", Order = 3), Key]
        public DateTime Timestamp { get; set; }

        [Column("ID_SUPPLIER", TypeName = "NVARCHAR", Order = 0), Key, StringLength(100)]
        public string IdSupplier { get; set; }

        [Column("SUPPLIER_NAME", TypeName = "NVARCHAR"), StringLength(500)]
        public string SupplierName { get; set; }

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

        [Column("USER"), StringLength(20)]
        public string User { get; set; }



        #region Equal
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            SupplierHistory supplierHistory = (SupplierHistory)obj;

            bool res = (
                IdVer == supplierHistory.IdVer &&
                IdSubVer == supplierHistory.IdSubVer &&
                Timestamp == supplierHistory.Timestamp &&
                IdSupplier == supplierHistory.IdSupplier &&
                SupplierName == supplierHistory.SupplierName &&
                Active == supplierHistory.Active &&
                VATNum == supplierHistory.VATNum &&
                ShippingAddress == supplierHistory.ShippingAddress &&
                ShippingAddress2 == supplierHistory.ShippingAddress2 &&
                ShippingAddressZh == supplierHistory.ShippingAddressZh &&
                ShippingAddressZh2 == supplierHistory.ShippingAddressZh2 &&
                BillingAddress == supplierHistory.BillingAddress &&
                BillingAddress2 == supplierHistory.BillingAddress2 &&
                BillingAddressZh == supplierHistory.BillingAddressZh &&
                BillingAddressZh2 == supplierHistory.BillingAddressZh2 &&
                ContactName == supplierHistory.ContactName &&
                ContactNameZh == supplierHistory.ContactNameZh &&
                ContactPhone == supplierHistory.ContactPhone &&
                Comments == supplierHistory.Comments &&
                IdIncoterm == supplierHistory.IdIncoterm &&
                IdPaymentTerms == supplierHistory.IdPaymentTerms &&
                IdDefaultCurrency == supplierHistory.IdDefaultCurrency &&
                User == supplierHistory.User
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
                (User == null ? 0: User.GetHashCode());

            return hashCode;
        }
        #endregion

        public static implicit operator SupplierHistory(Supplier s)
        {
            SupplierHistory sh = new SupplierHistory();

            sh.IdVer =  s.IdVer;
            sh.IdSubVer = s.IdSubVer;
            sh.Timestamp = s.Timestamp;
            sh.IdSupplier = s.IdSupplier;
            sh.SupplierName = s.SupplierName;
            sh.Active = s.Active;
            sh.VATNum = s.VATNum;
            sh.ShippingAddress = s.ShippingAddress;
            sh.ShippingAddress2 = s.ShippingAddress2;
            sh.ShippingAddressZh = s.ShippingAddressZh;
            sh.ShippingAddressZh2 = s.ShippingAddressZh2;
            sh.BillingAddress = s.BillingAddress;
            sh.BillingAddress2 = s.BillingAddress2;
            sh.BillingAddressZh = s.BillingAddressZh;
            sh.BillingAddressZh2 = s.BillingAddressZh2;
            sh.ContactName = s.ContactName;
            sh.ContactNameZh = s.ContactNameZh;
            sh.ContactPhone = s.ContactPhone;
            sh.Comments = s.Comments;
            sh.IdIncoterm = s.IdIncoterm;
            sh.IdPaymentTerms = s.IdPaymentTerms;
            sh.IdDefaultCurrency = s.IdDefaultCurrency;

            return sh;
        }
    }
}
