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
    [Table("ITEMS_MT_HISTORY")]
    public class ItemMtHistory
    {
        [Column("ID_VER", Order = 0), Key]
        public int IdVer { get; set; }

        [Column("ID_SUBVER", Order = 1), Key]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP", Order = 2), Key]
        public DateTime Timestamp { get; set; }

        [Column("ID_ITEM_BCN", Order = 3, TypeName = "NVARCHAR"), Key, StringLength(20)]
        public string IdItemBcn { get; set; }


        [Column("ID_MAT_TYPE_L1", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdMatTypeL1 { get; set; }
        [Column("ID_MAT_TYPE_L2", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdMatTypeL2 { get; set; }
        [Column("ID_MAT_TYPE_L3", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdMatTypeL3 { get; set; }

        [ForeignKey("IdMatTypeL1")]
        public MatTypeL1 MatTypeL1 { get; set; }
        [ForeignKey("IdMatTypeL2")]
        public MatTypeL2 MatTypeL2 { get; set; }
        [ForeignKey("IdMatTypeL3")]
        public MatTypeL3 MatTypeL3 { get; set; }

        [Column("ID_DEFAULT_SUPPLIER", TypeName = "NVARCHAR"), StringLength(100)]
        public String IdDefaultSupplier { get; set; }
        [ForeignKey("IdDefaultSupplier")]
        public Supplier DefaultSupplier { get; set; }

        [Column("ID_PROTOTYPE", TypeName = "NVARCHAR"), StringLength(50)]
        public string IdPrototype { get; set; }
        [ForeignKey("IdPrototype")]
        public Prototype Prototype { get; set; }

        [Column("ID_FAMILY_HK", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdFamilyHK { get; set; }
        [ForeignKey("IdFamilyHK")]
        public FamilyHK FamilyHK { get; set; }


        [Column("ID_ITEM_HK", TypeName = "NVARCHAR"), StringLength(20)]
        public string IdItemHK { get; set; }

        [Column("ITEM_DESCRIPTION", TypeName = "NVARCHAR"), StringLength(100)]
        public string ItemDescription { get; set; }

        [Column("COMMENTS", TypeName = "NVARCHAR"), StringLength(2500)]
        public string Comments { get; set; }


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

        [Column("USER"), StringLength(20)]
        public string User { get; set; }

        #region Equal

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            ItemMtHistory itemMtHistory = (ItemMtHistory)obj;

            bool res = (
                IdVer == itemMtHistory.IdVer &&
                IdSubVer == itemMtHistory.IdSubVer &&
                Timestamp == itemMtHistory.Timestamp &&
                IdPrototype == itemMtHistory.IdPrototype &&
                IdItemBcn == itemMtHistory.IdItemBcn &&
                IdMatTypeL1 == itemMtHistory.IdMatTypeL1 &&
                IdMatTypeL2 == itemMtHistory.IdMatTypeL2 &&
                IdMatTypeL3 == itemMtHistory.IdMatTypeL3 &&
                IdDefaultSupplier == itemMtHistory.IdDefaultSupplier &&
                IdFamilyHK == itemMtHistory.IdFamilyHK &&
                IdItemHK == itemMtHistory.IdItemHK &&
                ItemDescription == itemMtHistory.ItemDescription &&
                Comments == itemMtHistory.Comments &&
                LaunchDate == itemMtHistory.LaunchDate &&
                RemovalDate == itemMtHistory.RemovalDate &&
                IdStatusCial == itemMtHistory.IdStatusCial &&
                IdStatusProd == itemMtHistory.IdStatusProd &&
                IdUserAttri1 == itemMtHistory.IdUserAttri1 &&
                IdUserAttri2 == itemMtHistory.IdUserAttri2 &&
                IdUserAttri3 == itemMtHistory.IdUserAttri3 &&
                Unit == itemMtHistory.Unit &&
                DocsLink == itemMtHistory.DocsLink &&
                CreateDate == itemMtHistory.CreateDate &&
                PhotoUrl == itemMtHistory.PhotoUrl &&
                User == itemMtHistory.User
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
                (IdMatTypeL1 == null ? 0 : IdMatTypeL1.GetHashCode()) +
                (IdMatTypeL2 == null ? 0 : IdMatTypeL2.GetHashCode()) +
                (IdMatTypeL3 == null ? 0 : IdMatTypeL3.GetHashCode()) +
                (IdDefaultSupplier == null ? 0 : IdDefaultSupplier.GetHashCode()) +
                (IdFamilyHK == null ? 0 : IdFamilyHK.GetHashCode()) +
                (IdItemHK == null ? 0 : IdItemHK.GetHashCode()) +
                (ItemDescription == null ? 0 : ItemDescription.GetHashCode()) +
                (Comments == null ? 0 : Comments.GetHashCode()) +
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
                (PhotoUrl == null ? 0 : PhotoUrl.GetHashCode()) +
                (User == null ? 0 : User.GetHashCode());

            return hashCode;
        }

        #endregion

        public static implicit operator ItemMtHistory(ItemMt i)
        {
            ItemMtHistory imth = new ItemMtHistory();

            imth.IdVer = i.IdVer;
            imth.IdSubVer = i.IdSubVer;
            imth.Timestamp = i.Timestamp;
            imth.IdPrototype = i.IdPrototype;
            imth.IdItemBcn = i.IdItemBcn;
            imth.IdMatTypeL1 = i.IdMatTypeL1;
            imth.IdMatTypeL2 = i.IdMatTypeL2;
            imth.IdMatTypeL3 = i.IdMatTypeL3;
            imth.IdDefaultSupplier = i.IdDefaultSupplier;
            imth.IdFamilyHK = i.IdFamilyHK;
            imth.IdItemHK = i.IdItemHK;
            imth.ItemDescription = i.ItemDescription;
            imth.Comments = i.Comments;
            imth.LaunchDate = i.LaunchDate;
            imth.RemovalDate = i.RemovalDate;
            imth.IdStatusCial = i.IdStatusCial;
            imth.IdStatusProd = i.IdStatusProd;
            imth.IdUserAttri1 = i.IdUserAttri1;
            imth.IdUserAttri2 = i.IdUserAttri2;
            imth.IdUserAttri3 = i.IdUserAttri3;
            imth.Unit = i.Unit;
            imth.DocsLink = i.DocsLink;
            imth.CreateDate = i.CreateDate;
            imth.PhotoUrl = i.PhotoUrl;

            return imth;

        }
    }
}
