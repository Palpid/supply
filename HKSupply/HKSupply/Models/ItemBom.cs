using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HKSupply.Models
{
    public class ItemBom
    {
        [Column("ID_BOM"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBom { get; set; }

        [Column("ID_VER"), Required]
        public int IdVer { get; set; }

        [Column("ID_SUBVER"), Required]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP"), Required]
        public DateTime Timestamp { get; set; }

        [Column("ID_ITEM_BCN", TypeName = "NVARCHAR"), StringLength(50)]
        public string IdItemBcn { get; set; }

        /// <summary>
        /// Debido a que el item del bom puede ser de diferentes clases, lo defino como object y desde la aplicación ya controlaremos el casteo al tipo 
        /// correspondiente según su item group
        /// </summary>
        [NotMapped]
        public object Item { get; set; }

        [Column("ID_ITEM_GROUP", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdItemGroup { get; set; }
        [ForeignKey("IdItemGroup")]
        public ItemGroup ItemGroup { get; set; }

        [Column("CREATE_DATE", TypeName = "DATETIME")]
        public DateTime CreateDate { get; set; }

        public List<DetailBomMt> Materials { get; set; }

        public List<DetailBomHw> Hardwares { get; set; }

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            ItemBom itemBom = (ItemBom)obj;

            bool res = (
                IdBom == itemBom.IdBom &&
                IdVer == itemBom.IdVer &&
                IdSubVer == itemBom.IdSubVer &&
                Timestamp == itemBom.Timestamp &&
                IdItemBcn == itemBom.IdItemBcn &&
                IdItemGroup == itemBom.IdItemGroup &&
                CreateDate == itemBom.CreateDate
                );

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode = (
                IdBom.GetHashCode() +
                IdVer.GetHashCode() +
                IdSubVer.GetHashCode() +
                Timestamp.GetHashCode() +
                (IdItemBcn == null ? 0 : IdItemBcn.GetHashCode()) +
                (IdItemGroup == null ? 0 : IdItemGroup.GetHashCode()) +
                CreateDate.GetHashCode()
                );

            return hashCode;
        }
        #endregion
    }
}
