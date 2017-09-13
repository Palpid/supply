using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Models
{
    [Table("MY_COMPANY")]
    public class MyCompany
    {
        [Column("ID_MY_COMPANY", TypeName = "NVARCHAR"), Key, StringLength(100)]
        public string IdMyCompany { get; set; }

        [Column("NAME", TypeName = "NVARCHAR"), StringLength(500)]
        public string Name { get; set; }

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

        [Column("ID_DEFAULT_CURRENCY", TypeName = "NVARCHAR"), StringLength(4)]
        public string IdDefaultCurrency { get; set; }

        #region Foreign Keys
        [ForeignKey("IdDefaultCurrency")]
        public Currency DefaultCurrency { get; set; }
        #endregion

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            MyCompany myCompany = (MyCompany)obj;

            bool res = (
                IdMyCompany == myCompany.IdMyCompany &&
                Name == myCompany.Name &&
                VATNum == myCompany.VATNum &&
                ShippingAddress == myCompany.ShippingAddress &&
                ShippingAddressZh == myCompany.ShippingAddressZh &&
                BillingAddress == myCompany.BillingAddress &&
                BillingAddressZh == myCompany.BillingAddressZh &&
                ContactName == myCompany.ContactName &&
                ContactNameZh == myCompany.ContactNameZh &&
                ContactPhone == myCompany.ContactPhone &&
                Comments == myCompany.Comments &&
                IdDefaultCurrency == myCompany.IdDefaultCurrency
                );

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode =
                (IdMyCompany == null ? 0 : IdMyCompany.GetHashCode()) +
                (Name == null ? 0 : Name.GetHashCode()) +
                (VATNum == null ? 0 : VATNum.GetHashCode()) +
                (ShippingAddress == null ? 0 : ShippingAddress.GetHashCode()) +
                (ShippingAddressZh == null ? 0 : ShippingAddressZh.GetHashCode()) +
                (BillingAddress == null ? 0 : BillingAddress.GetHashCode()) +
                (BillingAddressZh == null ? 0 : BillingAddressZh.GetHashCode()) +
                (ContactName == null ? 0 : ContactName.GetHashCode()) +
                (ContactNameZh == null ? 0 : ContactNameZh.GetHashCode()) +
                (ContactPhone == null ? 0 : ContactPhone.GetHashCode()) +
                (Comments == null ? 0 : Comments.GetHashCode()) +
                (IdDefaultCurrency == null ? 0 : IdDefaultCurrency.GetHashCode());

            return hashCode;
        }
        #endregion
    }
}
