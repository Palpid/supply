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
    [Table("ITEMS")]
    public class Item
    {
        [Column("ID_VER", Order = 0), Key]
        public int IdVer { get; set; }

        [Column("ID_SUBVER", Order = 1), Key]
        public int IdSubVer { get; set; }

        [Column("TIMESTAMP"), Required]
        public DateTime Timestamp { get; set; }

        [Column("ID_PROTOTYPE", Order = 2, TypeName = "NVARCHAR"), Key, StringLength(50)]
        public string IdPrototype { get; set; }
        [Column("PROTOTYPE_NAME", TypeName = "NVARCHAR"), StringLength(100)]
        public string PrototypeName { get; set; }
        [Column("PROTOTYPE_DESCRIPTION", TypeName = "NVARCHAR"), StringLength(100)]
        public string PrototypeDescription { get; set; }
        [Column("PROTOTYPE_STATUS")]
        public int? PrototypeStatus { get; set; }

        [Column("ID_ITEM_GROUP", Order = 4, TypeName = "NVARCHAR"), Key, StringLength(30)]
        public string IdItemGroup { get; set; }
        [ForeignKey("IdItemGroup")]
        public ItemGroup ItemGroup { get; set; }

        [Column("ID_EY1", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdEy1 { get; set; }
        [Column("ID_EY2", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdEy2 { get; set; }
        [Column("ID_EY3", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdEy3 { get; set; }

        [Column("ID_MAT1", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdMat1 { get; set; }
        [Column("ID_MAT2", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdMat2 { get; set; }
        [Column("ID_MAT3", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdMat3 { get; set; }

        [Column("ID_HW1", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdHw1 { get; set; }
        [Column("ID_HW2", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdHw2 { get; set; }
        [Column("ID_HW3", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdHw3 { get; set; }

        [Column("ID_DEFAULT_SUPPLIER", TypeName = "NVARCHAR"), StringLength(3)]
        public String IdDefaultSupplier { get; set; }
        //[ForeignKey("IdDefaultSupplier")] //Nota: Supplier tiene una PK combinada entre IdVer, IdSubver e IdSupplir, no puedo arrastrar la FK aqui compuesta de 3
        [NotMapped]
        public Supplier DefaultSupplier { get; set; }

        [Column("ID_MODEL", TypeName = "NVARCHAR"), StringLength(30)]
        public string IdModel { get; set; }
        [ForeignKey("IdModel")]
        public Model Model { get; set; }

        [Column("ID_FAMILY_HK", TypeName = "NVARCHAR"), StringLength(30)]
        public string IdFamilyHK { get; set; }
        [ForeignKey("IdFamilyHK")]
        public FamilyHK FamilyHK { get; set; }

        [Column("CALIBER", TypeName = "NUMERIC")]
        public decimal Caliber { get; set; }

        [Column("ID_COLOR_1", TypeName = "NVARCHAR"), StringLength(30)]
        public string IdColor1 { get; set; }
        [ForeignKey("IdColor1")]
        public EtnColor Color1 { get; set; }
        [Column("ID_COLOR_2", TypeName = "NVARCHAR"), StringLength(30)]
        public string IdColor2 { get; set; }
        [ForeignKey("IdColor2")]
        public EtnColor Color2 { get; set; }

        [Column("ID_ITEM_BCN", Order = 3, TypeName = "NVARCHAR"), Key, StringLength(20)]
        public string IdItemBcn { get; set; }


        [Column("ID_ITEM_HK", TypeName = "NVARCHAR"), StringLength(20)]
        public string IdItemHK { get; set; }

        [Column("ITEM_DESCRIPTION", TypeName = "NVARCHAR"), StringLength(100)]
        public string ItemDescription { get; set; }

        [Column("COMMENTS", TypeName = "NVARCHAR"), StringLength(2500)]
        public string Comments{ get; set; }

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

        [Column("ID_STATUS_CIAL", TypeName = "NVARCHAR"), StringLength(30)]
        public string IdStatusCial { get; set; }
        [ForeignKey("IdStatusCial")]
        public StatusCial StatusCial { get; set; }

        [Column("ID_STATUS_PROD", TypeName = "NVARCHAR"), StringLength(30)]
        public string IdStatusProd { get; set; }
        [ForeignKey("IdStatusProd")]
        public StatusProd StatusProd { get; set; }

        [Column("ID_USER_ATTRI_1", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdUserAttri1 { get; set; }
        [Column("ID_USER_ATTRI_2", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdUserAttri2 { get; set; }
        [Column("ID_USER_ATTRI_3", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdUserAttri3 { get; set; }

        [Column("UNIT", TypeName = "NVARCHAR"), StringLength(2)]
        public string Unit { get; set; }

        [Column("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        #region Equal

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            Item item = (Item)obj;

            bool res = (
                IdVer == item.IdVer &&
                IdSubVer == item.IdSubVer &&
                Timestamp == item.Timestamp &&
                IdPrototype == item.IdPrototype &&
                PrototypeName == item.PrototypeName &&
                PrototypeDescription == item.PrototypeDescription &&
                PrototypeStatus == item.PrototypeStatus &&
                IdItemGroup == item.IdItemGroup &&
                IdEy1 == item.IdEy1 &&
                IdEy2 == item.IdEy2 &&
                IdEy3 == item.IdEy3 &&
                IdMat1 == item.IdMat1 &&
                IdMat2 == item.IdMat2 &&
                IdMat3 == item.IdMat3 &&
                IdHw1 == item.IdHw1 &&
                IdHw2 == item.IdHw2 &&
                IdHw3 == item.IdHw3 &&
                IdDefaultSupplier == item.IdDefaultSupplier &&
                IdModel == item.IdModel &&
                IdFamilyHK == item.IdFamilyHK &&
                Caliber == item.Caliber &&
                IdColor1 == item.IdColor1 &&
                IdColor2 == item.IdColor2 &&
                IdItemBcn == item.IdItemBcn &&
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
                CreateDate == item.CreateDate
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
                (PrototypeName == null ? 0 : PrototypeName.GetHashCode()) +
                (PrototypeDescription == null ? 0 : PrototypeDescription.GetHashCode()) +
                PrototypeStatus.GetHashCode() +
                (IdItemGroup == null ? 0 : IdItemGroup.GetHashCode()) +
                (IdEy1 == null ? 0 : IdEy1.GetHashCode()) +
                (IdEy2 == null ? 0 : IdEy2.GetHashCode()) +
                (IdEy3 == null ? 0 : IdEy3.GetHashCode()) +
                (IdMat1 == null ? 0 : IdMat1.GetHashCode()) +
                (IdMat2 == null ? 0 : IdMat2.GetHashCode()) +
                (IdMat3 == null ? 0 : IdMat3.GetHashCode()) +
                (IdHw1 == null ? 0 : IdHw1.GetHashCode()) +
                (IdHw2 == null ? 0 : IdHw2.GetHashCode()) +
                (IdHw3 == null ? 0 : IdHw3.GetHashCode()) +
                (IdDefaultSupplier == null ? 0 : IdDefaultSupplier.GetHashCode()) +
                (IdModel == null ? 0 : IdModel.GetHashCode()) +
                (IdFamilyHK == null ? 0 : IdFamilyHK.GetHashCode()) +
                Caliber.GetHashCode() +
                (IdColor1 == null ? 0 : IdColor1.GetHashCode()) +
                (IdColor2 == null ? 0 : IdColor2.GetHashCode()) +
                (IdItemBcn == null ? 0 : IdItemBcn.GetHashCode()) +
                (IdItemHK == null ? 0 : IdItemHK.GetHashCode()) +
                (ItemDescription == null ? 0 : ItemDescription.GetHashCode()) +
                (Comments == null ? 0 : Comments.GetHashCode()) +
                (Segment == null ? 0 : Segment.GetHashCode()) +
                (Category == null ? 0 : Category.GetHashCode()) +
                (Age == null ? 0 : Age.GetHashCode()) +
                (LaunchDate == null ? 0 : LaunchDate.GetHashCode()) +
                (RemovalDate == null ? 0 : RemovalDate.GetHashCode()) +
                (IdStatusCial == null ? 0 : IdStatusCial.GetHashCode()) +
                (IdStatusProd == null ? 0 : IdStatusProd.GetHashCode()) +
                (IdUserAttri1 == null ? 0 : IdUserAttri1.GetHashCode()) +
                (IdUserAttri2 == null ? 0 : IdUserAttri2.GetHashCode()) +
                (IdUserAttri3 == null ? 0 : IdUserAttri3.GetHashCode()) +
                (Unit == null ? 0 : Unit.GetHashCode()) +
                (CreateDate == null ? 0 : CreateDate.GetHashCode());

            return hashCode;
        }

        #endregion
    }
}
