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
    [Table("ITEMS_EY")]
    public class ItemEy
    {
        [Column("ID_VER"), Required]
        public int IdVer { get; set; }

        [Column("ID_SUBVER"), Required]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP"), Required]
        public DateTime Timestamp { get; set; }

        [Column("ID_ITEM_BCN", TypeName = "NVARCHAR"), Key, StringLength(50)]
        public string IdItemBcn { get; set; }

        [Column("ID_PROTOTYPE", TypeName = "NVARCHAR"), StringLength(50)]
        public string IdPrototype { get; set; }
        [ForeignKey("IdPrototype")]
        public Prototype Prototype { get; set; }

        [Column("ID_MATERIAL_L1", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdMaterialL1 { get; set; }
        [Column("ID_MATERIAL_L2", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdMaterialL2 { get; set; }
        [Column("ID_MATERIAL_L3", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdMaterialL3 { get; set; }

        [ForeignKey("IdMaterialL1")]
        public MaterialL1 MaterialL1 { get; set; }
        [ForeignKey("IdMaterialL2")]
        public MaterialL2 MaterialL2 { get; set; }
        [ForeignKey("IdMaterialL3")]
        public MaterialL3 MaterialL3 { get; set; }

        [Column("ID_DEFAULT_SUPPLIER", TypeName = "NVARCHAR"), StringLength(100)]
        public String IdDefaultSupplier { get; set; }
        [ForeignKey("IdDefaultSupplier")]
        public Supplier DefaultSupplier { get; set; }

        [Column("ID_MODEL", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdModel { get; set; }
        [ForeignKey("IdModel")]
        public Model Model { get; set; }

        [Column("ID_FAMILY_HK", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdFamilyHK { get; set; }
        [ForeignKey("IdFamilyHK")]
        public FamilyHK FamilyHK { get; set; }

        [Column("CALIBER", TypeName = "NUMERIC")]
        public decimal Caliber { get; set; }

        [Column("ID_COLOR_1", TypeName = "NVARCHAR"), StringLength(30)]
        public string IdColor1 { get; set; }
        [Column("ID_COLOR_2", TypeName = "NVARCHAR"), StringLength(30)]
        public string IdColor2 { get; set; }

        [Column("ID_ITEM_HK", TypeName = "NVARCHAR"), StringLength(50)]
        public string IdItemHK { get; set; }

        [Column("ITEM_DESCRIPTION", TypeName = "NVARCHAR"), StringLength(100)]
        public string ItemDescription { get; set; }

        [Column("COMMENTS", TypeName = "NVARCHAR"), StringLength(2500)]
        public string Comments { get; set; }

        [Column("SEGMENT", TypeName = "NVARCHAR"), StringLength(30)]
        public string Segment { get; set; }

        [Column("CATEGORY", TypeName = "NVARCHAR"), StringLength(100)]
        public string Category { get; set; }

        [Column("AGE", TypeName = "NVARCHAR"), StringLength(100)]
        public string Age { get; set; }

        [Column("LAUNCH_DATE")]
        public DateTime? LaunchDate { get; set; }

        [Column("REMOVAL_DATE")]
        public DateTime? RemovalDate { get; set; }

        [Column("ID_STATUS_CIAL")]
        public int IdStatusCial { get; set; }
        [ForeignKey("IdStatusCial")]
        public StatusCial StatusCial { get; set; }

        [Column("ID_STATUS_PROD")]
        public int IdStatusProd { get; set; }
        [ForeignKey("IdStatusProd")]
        public StatusHK StatusProd { get; set; }

        [Column("ID_USER_ATTRI_1", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdUserAttri1 { get; set; }
        [Column("ID_USER_ATTRI_2", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdUserAttri2 { get; set; }
        [Column("ID_USER_ATTRI_3", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdUserAttri3 { get; set; }

        [Column("UNIT", TypeName = "NVARCHAR"), StringLength(2)]
        public string Unit { get; set; }

        [Column("DOCS_LINK", TypeName = "NVARCHAR"), StringLength(512)]
        public string DocsLink { get; set; }

        [Column("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        [Column("PHOTO_URL", TypeName = "NVARCHAR"), StringLength(2500)]
        public string PhotoUrl { get; set; }

        #region Equal

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            ItemEy item = (ItemEy)obj;

            bool res = (
                IdVer == item.IdVer &&
                IdSubVer == item.IdSubVer &&
                Timestamp == item.Timestamp &&
                IdPrototype == item.IdPrototype &&
                IdItemBcn == item.IdItemBcn &&
                IdMaterialL1 == item.IdMaterialL1 &&
                IdMaterialL2 == item.IdMaterialL2 &&
                IdMaterialL3 == item.IdMaterialL3 &&
                IdDefaultSupplier == item.IdDefaultSupplier &&
                IdModel == item.IdModel &&
                IdFamilyHK == item.IdFamilyHK &&
                Caliber == item.Caliber &&
                IdColor1 == item.IdColor1 &&
                IdColor2 == item.IdColor2 &&
                IdItemHK == item.IdItemHK &&
                ItemDescription == item.ItemDescription &&
                Comments == item.Comments &&
                Segment == item.Segment &&
                Category == item.Category &&
                Age == item.Age &&
                LaunchDate == item.LaunchDate &&
                RemovalDate == item.RemovalDate &&
                IdStatusCial == item.IdStatusCial &&
                IdStatusProd == item.IdStatusProd &&
                IdUserAttri1 == item.IdUserAttri1 &&
                IdUserAttri2 == item.IdUserAttri2 &&
                IdUserAttri3 == item.IdUserAttri3 &&
                Unit == item.Unit &&
                DocsLink == item.DocsLink &&
                CreateDate == item.CreateDate &&
                PhotoUrl == item.PhotoUrl
                );
          
            return res;
        }

        public override int GetHashCode()
        {
            int hashCode =
                IdVer.GetHashCode() + 
                IdSubVer.GetHashCode() + 
                Timestamp.GetHashCode() + 
                (IdPrototype == null ? 0 : IdPrototype.GetHashCode()) + 
                (IdItemBcn == null ? 0 : IdItemBcn.GetHashCode()) + 
                (IdMaterialL1 == null ? 0 : IdMaterialL1.GetHashCode()) + 
                (IdMaterialL2 == null ? 0 : IdMaterialL2.GetHashCode()) + 
                (IdMaterialL3 == null ? 0 : IdMaterialL3.GetHashCode()) + 
                (IdDefaultSupplier == null ? 0 : IdDefaultSupplier.GetHashCode()) + 
                (IdModel == null ? 0 : IdModel.GetHashCode()) + 
                (IdFamilyHK == null ? 0 : IdFamilyHK.GetHashCode()) + 
                Caliber.GetHashCode() + 
                (IdColor1 == null ? 0 : IdColor1.GetHashCode()) + 
                (IdColor2 == null ? 0 : IdColor2.GetHashCode()) + 
                (IdItemHK == null ? 0 : IdItemHK.GetHashCode()) + 
                (ItemDescription == null ? 0 : ItemDescription.GetHashCode()) + 
                (Comments == null ? 0 : Comments.GetHashCode()) + 
                (Segment == null ? 0 : Segment.GetHashCode()) + 
                (Category == null ? 0 : Category.GetHashCode()) + 
                (Age == null ? 0 : Age.GetHashCode()) +  
                (LaunchDate == null ? 0 : LaunchDate.GetHashCode()) + 
                (RemovalDate == null ? 0 : RemovalDate.GetHashCode()) + 
                IdStatusCial.GetHashCode() + 
                IdStatusProd.GetHashCode() + 
                (IdUserAttri1 == null ? 0 : IdUserAttri1.GetHashCode()) + 
                (IdUserAttri2 == null ? 0 : IdUserAttri2.GetHashCode()) + 
                (IdUserAttri3 == null ? 0 : IdUserAttri3.GetHashCode()) + 
                (Unit == null ? 0 : Unit.GetHashCode()) +
                (DocsLink == null ? 0 : DocsLink.GetHashCode()) +
                CreateDate.GetHashCode() + 
                (PhotoUrl == null ? 0 : PhotoUrl.GetHashCode());

            return hashCode;
        }

        #endregion
    }
}
