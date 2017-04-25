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
    [Table("ITEMS_MT")]
    public class ItemMt
    {
        [Column("ID_VER"), Required]
        public int IdVer { get; set; }

        [Column("ID_SUBVER"), Required]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP"), Required]
        public DateTime Timestamp { get; set; }

        [Column("ID_ITEM_BCN", TypeName = "NVARCHAR"), Key, StringLength(20)]
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

        #region Equal

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            ItemMt itemMt = (ItemMt)obj;

            bool res = (
                IdVer == itemMt.IdVer &&
                IdSubVer == itemMt.IdSubVer &&
                Timestamp == itemMt.Timestamp &&
                IdPrototype == itemMt.IdPrototype &&
                IdItemBcn == itemMt.IdItemBcn &&
                IdMatTypeL1 == itemMt.IdMatTypeL1 &&
                IdMatTypeL2 == itemMt.IdMatTypeL2 &&
                IdMatTypeL3 == itemMt.IdMatTypeL3 &&
                IdDefaultSupplier == itemMt.IdDefaultSupplier &&
                IdFamilyHK == itemMt.IdFamilyHK &&
                IdItemHK == itemMt.IdItemHK &&
                ItemDescription == itemMt.ItemDescription &&
                Comments == itemMt.Comments &&
                LaunchDate == itemMt.LaunchDate &&
                RemovalDate == itemMt.RemovalDate &&
                IdStatusCial == itemMt.IdStatusCial &&
                IdStatusProd == itemMt.IdStatusProd &&
                IdUserAttri1 == itemMt.IdUserAttri1 &&
                IdUserAttri2 == itemMt.IdUserAttri2 &&
                IdUserAttri3 == itemMt.IdUserAttri3 &&
                Unit == itemMt.Unit &&
                DocsLink == itemMt.DocsLink &&
                CreateDate == itemMt.CreateDate &&
                PhotoUrl == itemMt.PhotoUrl
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
                (PhotoUrl == null ? 0 : PhotoUrl.GetHashCode());

            return hashCode;
        }

        #endregion
    }
}
