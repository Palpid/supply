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
        [Column("ID_VER", Order = 1), Key]
        public int IdVer { get; set; }

        [Column("ID_SUBVER", Order = 2), Key]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP")]
        public DateTime Timestamp { get; set; }

        [Column("ID_SUPPLIER", Order = 0), Key, StringLength(3)]
        public string IdSupplier { get; set; }

        [Column("SUPPLIER_NAME")]
        public string SupplierName { get; set; }

        [Column("ACTIVE")]
        public bool Active { get; set; }

        [Column("VAT_NUM")]
        public string VATNum { get; set; }

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

        #region Equal
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            Supplier customer = (Supplier)obj;

            bool res = (
                IdVer == customer.IdVer &&
                IdSubVer == customer.IdSubVer &&
                Timestamp == customer.Timestamp &&
                IdSupplier == customer.IdSupplier &&
                SupplierName == customer.SupplierName &&
                Active == customer.Active &&
                VATNum == customer.VATNum &&
                ShippingAddress == customer.ShippingAddress &&
                BillingAddress == customer.BillingAddress &&
                ContactName == customer.ContactName &&
                ContactPhone == customer.ContactPhone &&
                IdIncoterm == customer.IdIncoterm &&
                IdPaymentTerms == customer.IdPaymentTerms &&
                Currency == customer.Currency);

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
                (BillingAddress == null ? 0 : BillingAddress.GetHashCode()) +
                (ContactName == null ? 0 : ContactName.GetHashCode()) +
                (ContactPhone == null ? 0 : ContactPhone.GetHashCode()) +
                IdIncoterm.GetHashCode() +
                IdPaymentTerms.GetHashCode() +
                (Currency == null ? 0 : Currency.GetHashCode());

            return hashCode;
        }
        #endregion
    }
}
