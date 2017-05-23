using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HKSupply.Models
{

    [Table("ITEMS_BOM")]
    public class ItemBom
    {
        [Column("ID_BOM"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBom { get; set; }

        [Column("ID_VER"), Required, Index("IX_VER_ITEM", 1, IsUnique = true)]
        public int IdVer { get; set; }

        [Column("ID_SUBVER"), Required, Index("IX_VER_ITEM", 2, IsUnique = true)] 
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP"), Required]
        public DateTime Timestamp { get; set; }

        [Column("ID_ITEM_BCN", TypeName = "NVARCHAR"), StringLength(50), Required, Index("IX_VER_ITEM", 3, IsUnique = true)] 
        public string IdItemBcn { get; set; }

        /// <summary>
        /// Debido a que el item del bom puede ser de diferentes clases, lo defino como object y desde la aplicación ya controlaremos el casteo al tipo 
        /// correspondiente según su item group
        /// </summary>
        [NotMapped]
        public object Item { get; set; }

        [Column("ID_ITEM_GROUP", TypeName = "NVARCHAR"), StringLength(100), Required]
        public string IdItemGroup { get; set; }
        [ForeignKey("IdItemGroup")]
        public ItemGroup ItemGroup { get; set; }

        [Column("CREATE_DATE", TypeName = "DATETIME")]
        public DateTime CreateDate { get; set; }

        //public List<DetailBomMt> Materials { get; set; }
        //public List<DetailBomHw> Hardwares { get; set; }

        //Se supone que Entity-Framework necesita de "virtual icollection" para implentar el lazy load, pero después me está dando problemas con las devexpress 
        //public virtual ICollection<DetailBomMt> Materials { get; set; }
        //public virtual ICollection<DetailBomHw> Hardwares { get; set; }

        public virtual List<DetailBomMt> Materials { get; set; }

        public virtual List<DetailBomHw> Hardwares { get; set; }


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

            if (res == true)
            {
                res = (Hardwares.Count == itemBom.Hardwares.Count);
            }

            if (res == true)
            {
                res = (Materials.Count == itemBom.Materials.Count);
            }

            int i = 0;
            if (res == true)
            {
                foreach(var h in Hardwares)
                {
                    res = res && (
                        h.IdBom == itemBom.Hardwares[i].IdBom &&
                        h.IdItemBcn == itemBom.Hardwares[i].IdItemBcn &&
                        h.Quantity == itemBom.Hardwares[i].Quantity &&
                        h.Waste == itemBom.Hardwares[i].Waste
                        );
                    i++;
                }
            }

            i = 0;
            if (res == true)
            {
                foreach (var m in Materials)
                {
                    res = res && (
                        m.IdBom == itemBom.Materials[i].IdBom &&
                        m.IdItemBcn == itemBom.Materials[i].IdItemBcn &&
                        m.Quantity == itemBom.Materials[i].Quantity &&
                        m.Waste == itemBom.Materials[i].Waste
                        );
                    i++;
                }
            }

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
