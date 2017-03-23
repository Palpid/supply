﻿using System;
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

            Customer customer = (Customer)obj;

            bool res = (
                IdVer == customer.IdVer &&
                IdSubVer == customer.IdSubVer &&
                Timestamp == customer.Timestamp &&
                idCustomer == customer.IdCustomer &&
                CustName == customer.CustName &&
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
                idCustomer.GetHashCode() +
                CustName.GetHashCode() +
                Active.GetHashCode() +
                VATNum.GetHashCode() +
                ShippingAddress.GetHashCode() +
                BillingAddress.GetHashCode() +
                ContactName.GetHashCode() +
                ContactPhone.GetHashCode() +
                IdIncoterm.GetHashCode() +
                IdPaymentTerms.GetHashCode() +
                Currency.GetHashCode();

            return hashCode;
        }
        #endregion

        public static implicit operator CustomerHistory(Customer c)
        {
            CustomerHistory ch = new CustomerHistory();

            ch.IdVer = c.IdVer;
            ch.IdSubVer = c.IdSubVer;
            ch.Timestamp = c.Timestamp;
            ch.idCustomer = c.IdCustomer;
            ch.CustName = c.CustName;
            ch.Active = c.Active;
            ch.VATNum = c.VATNum;
            ch.ShippingAddress = c.ShippingAddress;
            ch.BillingAddress = c.BillingAddress;
            ch.ContactName = c.ContactName;
            ch.ContactPhone = c.ContactPhone;
            ch.IdIncoterm = c.IdIncoterm;
            ch.IdPaymentTerms = c.IdPaymentTerms;
            ch.Currency = c.Currency;

            return ch;
        }
    }
}