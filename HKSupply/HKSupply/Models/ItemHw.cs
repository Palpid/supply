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
    [Table("ITEMS_HW")]
    public class ItemHw
    {
        [Column("ID_VER"), Required]
        public int IdVer { get; set; }

        [Column("ID_SUBVER"), Required]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP"), Required]
        public DateTime Timestamp { get; set; }

        [Column("ID_ITEM_BCN",TypeName = "NVARCHAR"), Key, StringLength(20)]
        public string IdItemBcn { get; set; }


        [Column("ID_HW_TYPE_L1", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdHwTypeL1 { get; set; }
        [Column("ID_HW_TYPE_L2", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdHwTypeL2 { get; set; }
        [Column("ID_HW_TYPE_L3", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdHwTypeL3 { get; set; }

        [ForeignKey("IdHwTypeL1")]
        public HwTypeL1 HwTypeL1 { get; set; }
        [ForeignKey("IdHwTypeL2")]
        public HwTypeL2 HwTypeL2 { get; set; }
        [ForeignKey("IdHwTypeL3")]
        public HwTypeL3 HwTypeL3 { get; set; }

        [Column("ID_DEFAULT_SUPPLIER", TypeName = "NVARCHAR"), StringLength(100)]
        public String IdDefaultSupplier { get; set; }
        [ForeignKey("IdDefaultSupplier")]
        public Supplier DefaultSupplier { get; set; }

        [Column("ID_PROTOTYPE", TypeName = "NVARCHAR"),  StringLength(50)]
        public string IdPrototype { get; set; }
        [ForeignKey("IdPrototype")]
        public Prototype Prototype { get; set; }

        [Column("ID_FAMILY_HK", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdFamilyHK { get; set; }
        [ForeignKey("IdFamilyHK")]
        public FamilyHK FamilyHK { get; set; }

        [Column("ID_COLOR_1", TypeName = "NVARCHAR"), StringLength(30)]
        public string IdColor1 { get; set; }
        [ForeignKey("IdColor1")]
        public EtnColor Color1 { get; set; }
        [Column("ID_COLOR_2", TypeName = "NVARCHAR"), StringLength(30)]
        public string IdColor2 { get; set; }
        [ForeignKey("IdColor2")]
        public EtnColor Color2 { get; set; }
        
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

            ItemHw itemHw = (ItemHw)obj;

            bool res = (
                IdVer == itemHw.IdVer &&
                IdSubVer == itemHw.IdSubVer &&
                Timestamp == itemHw.Timestamp &&
                IdPrototype == itemHw.IdPrototype &&
                IdItemBcn == itemHw.IdItemBcn &&
                IdHwTypeL1 == itemHw.IdHwTypeL1 &&
                IdHwTypeL2 == itemHw.IdHwTypeL2 &&
                IdHwTypeL3 == itemHw.IdHwTypeL3 &&
                IdDefaultSupplier == itemHw.IdDefaultSupplier &&
                IdFamilyHK == itemHw.IdFamilyHK &&
                IdColor1 == itemHw.IdColor1 &&
                IdColor2 == itemHw.IdColor2 &&
                IdItemHK == itemHw.IdItemHK &&
                ItemDescription == itemHw.ItemDescription &&
                Comments == itemHw.Comments &&
                LaunchDate == itemHw.LaunchDate &&
                RemovalDate == itemHw.RemovalDate &&
                IdStatusCial == itemHw.IdStatusCial &&
                IdStatusProd == itemHw.IdStatusProd &&
                IdUserAttri1 == itemHw.IdUserAttri1 &&
                IdUserAttri2 == itemHw.IdUserAttri2 &&
                IdUserAttri3 == itemHw.IdUserAttri3 &&
                Unit == itemHw.Unit &&
                DocsLink == itemHw.DocsLink &&
                CreateDate == itemHw.CreateDate &&
                PhotoUrl == itemHw.PhotoUrl
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
                (PhotoUrl == null ? 0 : PhotoUrl.GetHashCode());

            return hashCode;
        }

        #endregion
    }
}
