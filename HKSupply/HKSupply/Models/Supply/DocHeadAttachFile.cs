using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKSupply.Models.Supply
{
    [Table("DOC_HEAD_ATTACH_FILES")]
    public class DocHeadAttachFile
    {
        [Column("ID_FILE"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFile { get; set; }

        [Column("ID_DOC_HEAD", TypeName = "NVARCHAR"), StringLength(50)]
        public string IdDocHead { get; set; }

        [Column("FILE_NAME", TypeName = "NVARCHAR"), StringLength(250)]
        public string FileName { get; set; }

        [Column("FILE_EXTENSION", TypeName = "NVARCHAR"), StringLength(10)]
        public string FileExtension { get; set; }

        [Column("FILE_PATH", TypeName = "NVARCHAR"), StringLength(500)]
        public string FilePath { get; set; }

        [Column("USER", TypeName = "NVARCHAR"), StringLength(20)]
        public string User { get; set; }

        [Column("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        #region Equals

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            DocHeadAttachFile attachFile = (DocHeadAttachFile)obj;
            bool res = (
                IdFile == attachFile.IdFile &&
                IdDocHead == attachFile.IdDocHead &&
                FileName == attachFile.FileName &&
                FileExtension == attachFile.FileExtension &&
                FilePath == attachFile.FilePath &&
                User == attachFile.User
                );

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode = (
                IdFile.GetHashCode() + 
                (IdDocHead == null? 0 : IdDocHead.GetHashCode()) +
                (FileName == null ? 0 : FileName.GetHashCode()) +
                (FileExtension == null ? 0 : FileExtension.GetHashCode()) +
                (FilePath == null ? 0 : FilePath.GetHashCode()) +
                (User == null ? 0 : User.GetHashCode()) 
                );

            return hashCode;
        }

        #endregion
    }
}
