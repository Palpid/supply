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
    [Table("ITEMS_HISTORY")]
    public class ItemHistory
    {
        [Column("ID_VER", Order = 0), Key]
        public int IdVer { get; set; }

        [Column("ID_SUBVER", Order = 1), Key]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP", Order = 2), Key]
        public DateTime Timestamp { get; set; }

        [Column("ID_ITEM_GROUP", Order = 3, TypeName = "NVARCHAR"), Key, StringLength(100)]
        public string IdItemGroup { get; set; }

        [Column("ID_PROTOTYPE", Order = 4, TypeName = "NVARCHAR"), Key, StringLength(50)]
        public string IdPrototype { get; set; }
        [Column("PROTOTYPE_NAME", TypeName = "NVARCHAR"), StringLength(100)]
        public string PrototypeName { get; set; }
        [Column("PROTOTYPE_DESCRIPTION", TypeName = "NVARCHAR"), StringLength(100)]
        public string PrototypeDescription { get; set; }
        [Column("PROTOTYPE_STATUS")]
        public int? PrototypeStatus { get; set; }

        [Column("ID_ITEM_BCN", Order = 5, TypeName = "NVARCHAR"), Key, StringLength(20)]
        public string IdItemBcn { get; set; }

        [Column("ID_MATERIAL_L1", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdMaterialL1 { get; set; }
        [Column("ID_MATERIAL_L2", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdMaterialL2 { get; set; }
        [Column("ID_MATERIAL_L3", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdMaterialL3 { get; set; }

        [Column("ID_MAT_TYPE_L1", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdMatTypeL1 { get; set; }
        [Column("ID_MAT_TYPE_L2", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdMatTypeL2 { get; set; }
        [Column("ID_MAT_TYPE_L3", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdMatTypeL3 { get; set; }

        [Column("ID_HW_TYPE_L1", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdHwTypeL1 { get; set; }
        [Column("ID_HW_TYPE_L2", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdHwTypeL2 { get; set; }
        [Column("ID_HW_TYPE_L3", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdHwTypeL3 { get; set; }

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

        [Column("ID_ITEM_HK", TypeName = "NVARCHAR"), StringLength(20)]
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

        [Column("DOCS_LINK", TypeName = "NVARCHAR"), StringLength(512)]
        public string DocsLink { get; set; }

        [Column("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        [Column("USER"), StringLength(20)]
        public string User { get; set; }


        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            ItemHistory itemHistory = (ItemHistory)obj;

            bool res = (
                IdVer == itemHistory.IdVer &&
                IdSubVer == itemHistory.IdSubVer &&
                Timestamp == itemHistory.Timestamp &&
                IdItemGroup == itemHistory.IdItemGroup &&
                IdPrototype == itemHistory.IdPrototype &&
                PrototypeName == itemHistory.PrototypeName &&
                PrototypeDescription == itemHistory.PrototypeDescription &&
                PrototypeStatus == itemHistory.PrototypeStatus &&
                IdItemBcn == itemHistory.IdItemBcn &&
                IdMaterialL1 == itemHistory.IdMaterialL1 &&
                IdMaterialL2 == itemHistory.IdMaterialL2 &&
                IdMaterialL3 == itemHistory.IdMaterialL3 &&
                IdMatTypeL1 == itemHistory.IdMatTypeL1 &&
                IdMatTypeL2 == itemHistory.IdMatTypeL2 &&
                IdMatTypeL3 == itemHistory.IdMatTypeL3 &&
                IdHwTypeL1 == itemHistory.IdHwTypeL1 &&
                IdHwTypeL2 == itemHistory.IdHwTypeL2 &&
                IdHwTypeL3 == itemHistory.IdHwTypeL3 &&
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
                DocsLink == itemHistory.DocsLink &&
                CreateDate == itemHistory.CreateDate &&
                User == itemHistory.User
                );

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode =
                 IdVer.GetHashCode() +
                IdSubVer.GetHashCode() +
                Timestamp.GetHashCode() +
                (IdItemGroup == null ? 0 : IdItemGroup.GetHashCode()) +
                (IdPrototype == null ? 0 : IdPrototype.GetHashCode()) +
                (PrototypeName == null ? 0 : PrototypeName.GetHashCode()) +
                (PrototypeDescription == null ? 0 : PrototypeDescription.GetHashCode()) +
                (PrototypeStatus == null ? 0 : PrototypeStatus.GetHashCode()) +
                (IdItemBcn == null ? 0 : IdItemBcn.GetHashCode()) +
                (IdMaterialL1 == null ? 0 : IdMaterialL1.GetHashCode()) +
                (IdMaterialL2 == null ? 0 : IdMaterialL2.GetHashCode()) +
                (IdMaterialL3 == null ? 0 : IdMaterialL3.GetHashCode()) +
                (IdMatTypeL1 == null ? 0 : IdMatTypeL1.GetHashCode()) +
                (IdMatTypeL2 == null ? 0 : IdMatTypeL2.GetHashCode()) +
                (IdMatTypeL3 == null ? 0 : IdMatTypeL3.GetHashCode()) +
                (IdHwTypeL1 == null ? 0 : IdHwTypeL1.GetHashCode()) +
                (IdHwTypeL2 == null ? 0 : IdHwTypeL2.GetHashCode()) +
                (IdHwTypeL3 == null ? 0 : IdHwTypeL3.GetHashCode()) +
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
                User.GetHashCode();

            return hashCode;
        }
        #endregion

        public static implicit operator ItemHistory(Item i)
        {
            ItemHistory ih = new ItemHistory();
            ih.IdVer = i.IdVer;
            ih.IdSubVer = i.IdSubVer;
            ih.Timestamp = i.Timestamp;
            ih.IdItemGroup = i.IdItemGroup;
            ih.IdPrototype = i.IdPrototype;
            ih.PrototypeName = i.PrototypeName;
            ih.PrototypeDescription = i.PrototypeDescription;
            ih.PrototypeStatus = i.PrototypeStatus;
            ih.IdItemBcn = i.IdItemBcn;
            ih.IdMaterialL1 = i.IdMaterialL1;
            ih.IdMaterialL2 = i.IdMaterialL2;
            ih.IdMaterialL3 = i.IdMaterialL3;
            ih.IdMatTypeL1 = i.IdMatTypeL1;
            ih.IdMatTypeL2 = i.IdMatTypeL2;
            ih.IdMatTypeL3 = i.IdMatTypeL3;
            ih.IdHwTypeL1 = i.IdHwTypeL1;
            ih.IdHwTypeL2 = i.IdHwTypeL2;
            ih.IdHwTypeL3 = i.IdHwTypeL3;
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
            ih.DocsLink = i.DocsLink;
            ih.CreateDate = i.CreateDate;

            return ih;
        }
    }
}
