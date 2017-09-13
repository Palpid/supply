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
    [Table("ITEMS_HW_HISTORY")]
    public class ItemHwHistory
    {
        [Column("ID_VER", Order = 0), Key]
        public int IdVer { get; set; }

        [Column("ID_SUBVER", Order = 1), Key]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP", Order = 2), Key]
        public DateTime Timestamp { get; set; }

        [Column("ID_ITEM_BCN", Order = 3, TypeName = "NVARCHAR"), Key, StringLength(50)]
        public string IdItemBcn { get; set; }


        [Column("ID_HW_TYPE_L1", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdHwTypeL1 { get; set; }
        [Column("ID_HW_TYPE_L2", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdHwTypeL2 { get; set; }
        [Column("ID_HW_TYPE_L3", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdHwTypeL3 { get; set; }

        [ForeignKey("IdHwTypeL1")]
        public HwTypeL1 HwTypeL1 { get; set; }
        [ForeignKey("IdHwTypeL2, IdHwTypeL1")]
        public HwTypeL2 HwTypeL2 { get; set; }
        [ForeignKey("IdHwTypeL3, IdHwTypeL2, IdHwTypeL1")]
        public HwTypeL3 HwTypeL3 { get; set; }

        [Column("ID_DEFAULT_SUPPLIER", TypeName = "NVARCHAR"), StringLength(100)]
        public String IdDefaultSupplier { get; set; }
        [ForeignKey("IdDefaultSupplier")]
        public Supplier DefaultSupplier { get; set; }

        [Column("ID_PROTOTYPE", TypeName = "NVARCHAR"), StringLength(50)]
        public string IdPrototype { get; set; }
        [ForeignKey("IdPrototype")]
        public Prototype Prototype { get; set; }

        [Column("ID_MODEL", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdModel { get; set; }
        [ForeignKey("IdModel")]
        public Model Model { get; set; }

        [Column("ID_FAMILY_HK", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdFamilyHK { get; set; }
        [ForeignKey("IdFamilyHK")]
        public FamilyHK FamilyHK { get; set; }

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

        [Column("ID_GROUP_TYPE", TypeName = "NVARCHAR"), StringLength(50)]
        public string IdGroupType { get; set; }

        [Column("USER"), StringLength(20)]
        public string User { get; set; }


        #region Equal

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            ItemHwHistory itemHwHistory = (ItemHwHistory)obj;

            bool res = (
                IdVer == itemHwHistory.IdVer &&
                IdSubVer == itemHwHistory.IdSubVer &&
                Timestamp == itemHwHistory.Timestamp &&
                IdPrototype == itemHwHistory.IdPrototype &&
                IdItemBcn == itemHwHistory.IdItemBcn &&
                IdHwTypeL1 == itemHwHistory.IdHwTypeL1 &&
                IdHwTypeL2 == itemHwHistory.IdHwTypeL2 &&
                IdHwTypeL3 == itemHwHistory.IdHwTypeL3 &&
                IdDefaultSupplier == itemHwHistory.IdDefaultSupplier &&
                IdModel == itemHwHistory.IdModel &&
                IdFamilyHK == itemHwHistory.IdFamilyHK &&
                IdColor1 == itemHwHistory.IdColor1 &&
                IdColor2 == itemHwHistory.IdColor2 &&
                IdItemHK == itemHwHistory.IdItemHK &&
                ItemDescription == itemHwHistory.ItemDescription &&
                Comments == itemHwHistory.Comments &&
                LaunchDate == itemHwHistory.LaunchDate &&
                RemovalDate == itemHwHistory.RemovalDate &&
                IdStatusCial == itemHwHistory.IdStatusCial &&
                IdStatusProd == itemHwHistory.IdStatusProd &&
                IdUserAttri1 == itemHwHistory.IdUserAttri1 &&
                IdUserAttri2 == itemHwHistory.IdUserAttri2 &&
                IdUserAttri3 == itemHwHistory.IdUserAttri3 &&
                Unit == itemHwHistory.Unit &&
                DocsLink == itemHwHistory.DocsLink &&
                CreateDate == itemHwHistory.CreateDate &&
                PhotoUrl == itemHwHistory.PhotoUrl &&
                IdGroupType == itemHwHistory.IdGroupType &&
                User == itemHwHistory.User
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
                (IdHwTypeL1 == null ? 0 : IdHwTypeL1.GetHashCode()) +
                (IdHwTypeL2 == null ? 0 : IdHwTypeL2.GetHashCode()) +
                (IdHwTypeL3 == null ? 0 : IdHwTypeL3.GetHashCode()) +
                (IdDefaultSupplier == null ? 0 : IdDefaultSupplier.GetHashCode()) +
                (IdFamilyHK == null ? 0 : IdFamilyHK.GetHashCode()) +
                (IdModel == null ? 0 : IdModel.GetHashCode()) +
                (IdColor1 == null ? 0 : IdColor1.GetHashCode()) +
                (IdColor2 == null ? 0 : IdColor2.GetHashCode()) +
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
                (IdGroupType == null ? 0 : IdGroupType.GetHashCode()) +
                (User == null ? 0 : User.GetHashCode());

            return hashCode;
        }

        #endregion

        public static implicit operator ItemHwHistory(ItemHw i)
        {
            ItemHwHistory ihwh = new ItemHwHistory();

             ihwh.IdVer = i.IdVer;
                ihwh.IdSubVer = i.IdSubVer;
                ihwh.Timestamp = i.Timestamp;
                ihwh.IdPrototype = i.IdPrototype;
                ihwh.IdItemBcn = i.IdItemBcn;
                ihwh.IdHwTypeL1 = i.IdHwTypeL1;
                ihwh.IdHwTypeL2 = i.IdHwTypeL2;
                ihwh.IdHwTypeL3 = i.IdHwTypeL3;
                ihwh.IdDefaultSupplier = i.IdDefaultSupplier;
                ihwh.IdModel = i.IdModel;
                ihwh.IdFamilyHK = i.IdFamilyHK;
                ihwh.IdColor1 = i.IdColor1;
                ihwh.IdColor2 = i.IdColor2;
                ihwh.IdItemHK = i.IdItemHK;
                ihwh.ItemDescription = i.ItemDescription;
                ihwh.Comments = i.Comments;
                ihwh.LaunchDate = i.LaunchDate;
                ihwh.RemovalDate = i.RemovalDate;
                ihwh.IdStatusCial = i.IdStatusCial;
                ihwh.IdStatusProd = i.IdStatusProd;
                ihwh.IdUserAttri1 = i.IdUserAttri1;
                ihwh.IdUserAttri2 = i.IdUserAttri2;
                ihwh.IdUserAttri3 = i.IdUserAttri3;
                ihwh.Unit = i.Unit;
                ihwh.DocsLink = i.DocsLink;
                ihwh.CreateDate = i.CreateDate;
                ihwh.PhotoUrl = i.PhotoUrl;
                ihwh.IdGroupType = i.IdGroupType;

            return ihwh;
        }
    }
}
