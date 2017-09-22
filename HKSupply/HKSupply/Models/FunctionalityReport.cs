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
    [Table("FUNCTIONALITY_REPORTS")]
    public class FunctionalityReport
    {
        [Column("FUNCTIONALITY_REPORT_ID")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FunctionalityReportId { get; set; }

        [Column("FUNCTIONALITY_ID")]
        public int FunctionalityId { get; set; }

        [Column("REPORT_NAME_EN", TypeName = "NVARCHAR"), StringLength(250), Required]
        public string ReportNameEn { get; set; }

        [Column("REPORT_FILE", TypeName = "NVARCHAR"), StringLength(1000), Required]
        public string ReportFile { get; set; }

        [Column("CODE", TypeName = "NVARCHAR"), StringLength(50)]
        public string Code { get; set; }

        [ForeignKey("FunctionalityId")]
        public Functionality Functionality { get; set; }

        #region Equals

        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            FunctionalityReport functionalityReport = (FunctionalityReport)obj;

            return (FunctionalityReportId == functionalityReport.FunctionalityReportId);
        }

        public override int GetHashCode()
        {
            return FunctionalityReportId.GetHashCode();
        }

        #endregion
    }
}
