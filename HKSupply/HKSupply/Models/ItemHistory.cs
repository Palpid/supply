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
    [Table("ITEMS_HISTORY")]
    public class ItemHistory
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

        [Column("ID_FAMILY_HK", TypeName = "NVARCHAR"), StringLength(100)]
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
                IdPrototype == itemHistory.IdPrototype &&
                PrototypeName == itemHistory.PrototypeName &&
                PrototypeDescription == itemHistory.PrototypeDescription &&
                PrototypeStatus == itemHistory.PrototypeStatus &&
                IdItemGroup == itemHistory.IdItemGroup &&
                IdEy1 == itemHistory.IdEy1 &&
                IdEy2 == itemHistory.IdEy2 &&
                IdEy3 == itemHistory.IdEy3 &&
                IdMat1 == itemHistory.IdMat1 &&
                IdMat2 == itemHistory.IdMat2 &&
                IdMat3 == itemHistory.IdMat3 &&
                IdHw1 == itemHistory.IdHw1 &&
                IdHw2 == itemHistory.IdHw2 &&
                IdHw3 == itemHistory.IdHw3 &&
                IdDefaultSupplier == itemHistory.IdDefaultSupplier &&
                IdModel == itemHistory.IdModel &&
                IdFamilyHK == itemHistory.IdFamilyHK &&
                Caliber == itemHistory.Caliber &&
                IdColor1 == itemHistory.IdColor1 &&
                IdColor2 == itemHistory.IdColor2 &&
                IdItemBcn == itemHistory.IdItemBcn &&
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
                (IdPrototype == null? 0 : IdPrototype.GetHashCode()) +
                (PrototypeName == null ? 0 : IdPrototype.GetHashCode()) +
                (PrototypeDescription  == null ? 0 : PrototypeDescription.GetHashCode()) +
                PrototypeStatus.GetHashCode() +
                (IdItemGroup  == null ? 0 : IdItemGroup.GetHashCode()) +
                (IdEy1  == null ? 0 : IdEy1.GetHashCode()) +
                (IdEy2  == null ? 0 : IdEy2.GetHashCode()) +
                (IdEy3  == null ? 0 : IdEy3.GetHashCode()) +
                (IdMat1  == null ? 0 : IdMat1.GetHashCode()) +
                (IdMat2  == null ? 0 : IdMat3.GetHashCode()) +
                (IdMat3  == null ? 0 : IdMat3.GetHashCode()) +
                (IdHw1  == null ? 0 : IdHw1.GetHashCode()) +
                (IdHw2  == null ? 0 : IdHw2.GetHashCode()) +
                (IdHw3  == null ? 0 : IdHw3.GetHashCode()) +
                (IdDefaultSupplier  == null ? 0 : IdDefaultSupplier.GetHashCode()) +
                (IdModel  == null ? 0 : IdModel.GetHashCode()) +
                (IdFamilyHK  == null ? 0 : IdFamilyHK.GetHashCode()) +
                Caliber.GetHashCode() +
                (IdColor1  == null ? 0 : IdColor1.GetHashCode()) +
                (IdColor2  == null ? 0 : IdColor2.GetHashCode()) +
                (IdItemBcn  == null ? 0 : IdItemBcn.GetHashCode()) +
                (IdItemHK  == null ? 0 : IdItemHK.GetHashCode()) +
                (ItemDescription  == null? 0 : ItemDescription.GetHashCode()) +
                (Comments  == null ? 0 : Comments.GetHashCode()) +
                (Segment  == null ? 0 : Segment.GetHashCode()) +
                (Category  == null ? 0 : Category.GetHashCode()) +
                (Age  == null ? 0 : Age.GetHashCode()) +
                (LaunchDate  == null ? 0 : LaunchDate.GetHashCode()) +
                (RemovalDate == null ? 0 : RemovalDate.GetHashCode()) +
                IdStatusCial.GetHashCode() +
                IdStatusProd.GetHashCode() +
                (IdUserAttri1 == null ? 0 : IdUserAttri1.GetHashCode()) +
                (IdUserAttri2 == null ? 0 : IdUserAttri2.GetHashCode()) +
                (IdUserAttri3 == null ? 0 : IdUserAttri3.GetHashCode()) +
                (Unit == null ? 0 : Unit.GetHashCode()) +
                (CreateDate == null ? 0 : CreateDate.GetHashCode()) +
                (User == null ? 0 : User.GetHashCode());

            return hashCode;
        }
        #endregion

        public static implicit operator ItemHistory(Item i)
        {
            ItemHistory ih = new ItemHistory();
            ih.IdVer = i.IdVer;
            ih.IdSubVer = i.IdSubVer;
            ih.Timestamp = i.Timestamp;
            ih.IdPrototype = i.IdPrototype;
            ih.PrototypeName = i.PrototypeName;
            ih.PrototypeDescription = i.PrototypeDescription;
            ih.PrototypeStatus = i.PrototypeStatus;
            ih.IdItemGroup = i.IdItemGroup;
            ih.IdEy1 = i.IdEy1;
            ih.IdEy2 = i.IdEy2;
            ih.IdEy3 = i.IdEy3;
            ih.IdMat1 = i.IdMat1;
            ih.IdMat2 = i.IdMat2;
            ih.IdMat3 = i.IdMat3;
            ih.IdHw1 = i.IdHw1;
            ih.IdHw2 = i.IdHw2;
            ih.IdHw3 = i.IdHw3;
            ih.IdDefaultSupplier = i.IdDefaultSupplier;
            ih.IdModel = i.IdModel;
            ih.IdFamilyHK = i.IdFamilyHK;
            ih.Caliber = i.Caliber;
            ih.IdColor1 = i.IdColor1;
            ih.IdColor2 = i.IdColor2;
            ih.IdItemBcn = i.IdItemBcn;
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
            ih.CreateDate = i.CreateDate;

            return ih;
        }
    }
}
