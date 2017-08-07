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
    [Table("ITEMS_DOCS")]
    public class ItemDoc
    {
        [Column("ID_DOC"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDoc { get; set; }

        [Column("ID_ITEM_BCN", TypeName = "NVARCHAR"), StringLength(20)]
        public string IdItemBcn { get; set; }

        [Column("ID_VER_ITEM"), Required]
        public int IdVerItem { get; set; }

        [Column("ID_SUBVER_ITEM"), Required]
        public int IdSubVerItem { get; set; }

        [Column("ID_ITEM_GROUP", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdItemGroup { get; set; }
        [ForeignKey("IdItemGroup")]
        public ItemGroup ItemGroup { get; set; }

        [Column("ID_SUPPLIER", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdSupplier { get; set; }
        [ForeignKey("IdSupplier")]
        public Supplier Supplier { get; set; }
        
        [Column("ID_DOC_TYPE", TypeName = "NVARCHAR"), StringLength(100)]
        public string IdDocType { get; set; }
        [ForeignKey("IdDocType")]
        public DocType DocType { get; set; }

        [Column("FILE_NAME", TypeName = "NVARCHAR"), StringLength(100)]
        public string FileName { get; set; }
        [Column("FILE_PATH", TypeName = "NVARCHAR"), StringLength(500)]
        public string FilePath { get; set; }

        [Column("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        #region Equals

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            ItemDoc itemDoc = (ItemDoc)obj;

            bool res = (
                IdDoc == itemDoc.IdDoc &&
                IdItemBcn == itemDoc.IdItemBcn &&
                IdVerItem == itemDoc.IdVerItem &&
                IdSubVerItem == itemDoc.IdSubVerItem &&
                IdItemGroup == itemDoc.IdItemGroup &&
                IdDocType == itemDoc.IdDocType &&
                FileName == itemDoc.FileName &&
                FilePath == itemDoc.FilePath &&
                CreateDate == itemDoc.CreateDate
                );

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode = (
                IdDoc.GetHashCode() + 
                (IdItemBcn == null ? 0 : IdItemBcn.GetHashCode()) + 
                IdVerItem.GetHashCode() + 
                IdSubVerItem.GetHashCode() + 
                (IdItemGroup == null ? 0 : IdItemGroup.GetHashCode()) + 
                (IdDocType == null ? 0 : IdDocType.GetHashCode()) + 
                (FileName == null ? 0 : FileName.GetHashCode()) + 
                (FilePath == null ? 0 : FilePath.GetHashCode()) + 
                CreateDate.GetHashCode());

            return hashCode;
        }

        #endregion

    }
}
