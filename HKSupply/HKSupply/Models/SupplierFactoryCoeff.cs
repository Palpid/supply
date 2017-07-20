using HKSupply.General;
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
    [Table("SUPPLIERS_FACTORIES_COEFF")]
    public class SupplierFactoryCoeff
    {
        [Column("ID_SUPPLIER", TypeName = "NVARCHAR", Order = 0), Key, StringLength(100)]
        public string IdSupplier { get; set; }

        [Column("ID_FACTORY", TypeName = "NVARCHAR", Order = 1), Key, StringLength(100)]
        public string IdFactory { get; set; }

        [Column("ID_ITEM_GROUP", TypeName = "NVARCHAR", Order = 2), Key, StringLength(100)]
        public string IdItemGroup { get; set; }

        [Column("DENSITY", TypeName = "NUMERIC")]
        public decimal? Density { get; set; }

        [Column("COEFFICIENT1", TypeName = "NUMERIC")]
        public decimal Coefficient1 { get; set; }

        [Column("COEFFICIENT2", TypeName = "NUMERIC")]
        public decimal Coefficient2 { get; set; }

        [Column("SCRAP", TypeName = "NUMERIC")]
        public decimal Scrap { get; set; }

        #region Foreign keys
        [ForeignKey("IdItemGroup")]
        public ItemGroup ItemGroup { get; set; }

        [ForeignKey("IdSupplier")]
        public Supplier Supplier { get; set; }

        [ForeignKey("IdFactory")]
        public Supplier Factory { get; set; }
        #endregion

        #region Constructor
        public SupplierFactoryCoeff(string idItemGroup)
        {
            IdItemGroup = idItemGroup;

            if (idItemGroup == Constants.ITEM_GROUP_MT)
            {
                Density = 1.29m;
                Coefficient1 = 1 / 1000m;
                Coefficient2 = 1;
                Scrap = 1.18m;
            }
            else if (idItemGroup == Constants.ITEM_GROUP_HW)
            {
                Density = null;
                Coefficient1 = 1;
                Coefficient2 = 1;
                Scrap = 1;
            }


        }
        #endregion

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            SupplierFactoryCoeff supplierFactoryCoeff = (SupplierFactoryCoeff)obj;

            bool res = (
                IdSupplier == supplierFactoryCoeff.IdSupplier &&
                IdFactory == supplierFactoryCoeff.IdFactory &&
                Coefficient1 == supplierFactoryCoeff.Coefficient1 &&
                Coefficient2 == supplierFactoryCoeff.Coefficient2 &&
                Scrap == supplierFactoryCoeff.Scrap
                );

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode = (
                (IdSupplier == null ? 0 : IdSupplier.GetHashCode()) +
                (IdFactory == null ? 0 : IdFactory.GetHashCode()) +
                Coefficient1.GetHashCode() +
                Coefficient2.GetHashCode() +
                Scrap.GetHashCode()
            );

            return hashCode;
        }
        #endregion
    }
}
