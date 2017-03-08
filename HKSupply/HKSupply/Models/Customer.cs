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
        [Column("ID_VER", Order = 1), Key]
        public int idVer { get; set; }

        [Column("ID_SUBVER", Order = 2), Key]
        public int idSubVer { get; set; }

        [Column("TIMESTAMP")]
        public DateTime Timestamp { get; set; }

        [Column("ID_CUSTOMER", Order = 0), Key]
        public string idCustomer { get; set; }

        [Column("CUST_NAME")]
        public string CustName { get; set; }

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
        public int idIncoterm { get; set; }

        [Column("IDPAYMENTTERMS")]
        public int idPaymentTerms { get; set; }

        [Column("CURRENCY")]
        public string Currency { get; set; }

        #region Equal
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            Customer customer = (Customer)obj;

            bool res = (
                idVer == customer.idVer &&
                idSubVer == customer.idVer &&
                Timestamp == customer.Timestamp &&
                idCustomer == customer.idCustomer &&
                CustName == customer.CustName &&
                Active == customer.Active &&
                VATNum == customer.VATNum &&
                ShippingAddress == customer.ShippingAddress &&
                BillingAddress == customer.BillingAddress &&
                ContactName == customer.ContactName &&
                ContactPhone == customer.ContactPhone &&
                idIncoterm == customer.idIncoterm &&
                idPaymentTerms == customer.idPaymentTerms &&
                Currency == customer.Currency);

            return res;

        }

        public override int GetHashCode()
        {
            int hashCode =
                idVer.GetHashCode() +
                idSubVer.GetHashCode() +
                Timestamp.GetHashCode() +
                idCustomer.GetHashCode() +
                CustName.GetHashCode() +
                Active.GetHashCode() +
                VATNum.GetHashCode() +
                ShippingAddress.GetHashCode() +
                BillingAddress.GetHashCode() +
                ContactName.GetHashCode() +
                ContactPhone.GetHashCode() +
                idIncoterm.GetHashCode() +
                idPaymentTerms.GetHashCode() +
                Currency.GetHashCode();

            return hashCode;
        }
        #endregion
    }
}
