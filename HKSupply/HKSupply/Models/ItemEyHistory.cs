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
    [Table("ITEMS_EY_HISTORY")]
    public class ItemEyHistory
    {
        [Column("ID_VER", Order = 0), Key]
        public int IdVer { get; set; }

        [Column("ID_SUBVER", Order = 1), Key]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP", Order = 2), Key]
        public DateTime Timestamp { get; set; }

        [Column("ID_ITEM_BCN", Order = 3, TypeName = "NVARCHAR"), Key, StringLength(50)]
        public string IdItemBcn { get; set; }

        [Column("ID_PROTOTYPE", TypeName = "NVARCHAR"), StringLength(50)]
        public string IdPrototype { get; set; }

        [Column("ID_MATERIAL_L1", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdMaterialL1 { get; set; }
        [Column("ID_MATERIAL_L2", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdMaterialL2 { get; set; }
        [Column("ID_MATERIAL_L3", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdMaterialL3 { get; set; }


        [Column("ID_DEFAULT_SUPPLIER", TypeName = "NVARCHAR"), StringLength(100)]
        public String IdDefaultSupplier { get; set; }

        [Column("ID_MODEL", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdModel { get; set; }

        [Column("ID_FAMILY_HK", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdFamilyHK { get; set; }

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

        [Column("ID_STATUS_PROD")]
        public int IdStatusProd { get; set; }

        [Column("ID_USER_ATTRI_1", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdUserAttri1 { get; set; }
        [Column("ID_USER_ATTRI_2", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdUserAttri2 { get; set; }
        [Column("ID_USER_ATTRI_3", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdUserAttri3 { get; set; }

        [Column("UNIT", TypeName = "NVARCHAR"), StringLength(2)]
        public string Unit { get; set; }

        [Column("UNIT_SUPPLY", TypeName = "NVARCHAR"), StringLength(2)]
        public string UnitSupply { get; set; }

        [Column("DOCS_LINK", TypeName = "NVARCHAR"), StringLength(512)]
        public string DocsLink { get; set; }

        [Column("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        [Column("USER"), StringLength(20)]
        public string User { get; set; }

        [Column("PHOTO_URL", TypeName = "NVARCHAR"), StringLength(2500)]
        public string PhotoUrl { get; set; }


        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            ItemEyHistory itemHistory = (ItemEyHistory)obj;

            bool res = (
                IdVer == itemHistory.IdVer &&
                IdSubVer == itemHistory.IdSubVer &&
                Timestamp == itemHistory.Timestamp &&
                IdPrototype == itemHistory.IdPrototype &&
                IdItemBcn == itemHistory.IdItemBcn &&
                IdMaterialL1 == itemHistory.IdMaterialL1 &&
                IdMaterialL2 == itemHistory.IdMaterialL2 &&
                IdMaterialL3 == itemHistory.IdMaterialL3 &&
                IdDefaultSupplier == itemHistory.IdDefaultSupplier &&
                IdModel == itemHistory.IdModel &&
                IdFamilyHK == itemHistory.IdFamilyHK &&
                Caliber == itemHistory.Caliber &&
                IdColor1 == itemHistory.IdColor1 &&
                IdColor2 == itemHistory.IdColor2 &&
                IdItemHK == itemHistory.IdItemHK &&
                ItemDescription == itemHistory.ItemDescription &&
                Comments == itemHistory.Comments &&
                Segment == itemHistory.Segment &&
                Category == itemHistory.Category &&
                Age == itemHistory.Age &&
                LaunchDate == itemHistory.LaunchDate &&
                RemovalDate == itemHistory.RemovalDate &&
                IdStatusCial == itemHistory.IdStatusCial &&
                IdStatusProd == itemHistory.IdStatusProd &&
                IdUserAttri1 == itemHistory.IdUserAttri1 &&
                IdUserAttri2 == itemHistory.IdUserAttri2 &&
                IdUserAttri3 == itemHistory.IdUserAttri3 &&
                Unit == itemHistory.Unit &&
                UnitSupply == itemHistory.UnitSupply &&
                DocsLink == itemHistory.DocsLink &&
                CreateDate == itemHistory.CreateDate &&
                User == itemHistory.User &&
                PhotoUrl == itemHistory.PhotoUrl
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
                (UnitSupply == null ? 0 : UnitSupply.GetHashCode()) +
                (DocsLink == null ? 0 : DocsLink.GetHashCode()) +
                CreateDate.GetHashCode() +
                User.GetHashCode() + 
                (PhotoUrl == null ? 0 : PhotoUrl.GetHashCode());

            return hashCode;
        }
        #endregion

        public static implicit operator ItemEyHistory(ItemEy i)
        {
            ItemEyHistory ih = new ItemEyHistory();
            ih.IdVer = i.IdVer;
            ih.IdSubVer = i.IdSubVer;
            ih.Timestamp = i.Timestamp;
            ih.IdPrototype = i.IdPrototype;
            ih.IdItemBcn = i.IdItemBcn;
            ih.IdMaterialL1 = i.IdMaterialL1;
            ih.IdMaterialL2 = i.IdMaterialL2;
            ih.IdMaterialL3 = i.IdMaterialL3;
            ih.IdDefaultSupplier = i.IdDefaultSupplier;
            ih.IdModel = i.IdModel;
            ih.IdFamilyHK = i.IdFamilyHK;
            ih.Caliber = i.Caliber;
            ih.IdColor1 = i.IdColor1;
            ih.IdColor2 = i.IdColor2;
            ih.IdItemHK = i.IdItemHK;
            ih.ItemDescription = i.ItemDescription;
            ih.Comments = i.Comments;
            ih.Segment = i.Segment;
            ih.Category = i.Category;
            ih.Age = i.Age;
            ih.LaunchDate = i.LaunchDate;
            ih.RemovalDate = i.RemovalDate;
            ih.IdStatusCial = i.IdStatusCial;
            ih.IdStatusProd = i.IdStatusProd;
            ih.IdUserAttri1 = i.IdUserAttri1;
            ih.IdUserAttri2 = i.IdUserAttri2;
            ih.IdUserAttri3 = i.IdUserAttri3;
            ih.Unit = i.Unit;
            ih.UnitSupply = i.UnitSupply;
            ih.DocsLink = i.DocsLink;
            ih.CreateDate = i.CreateDate;
            ih.PhotoUrl = i.PhotoUrl;

            return ih;
        }
    }
}
