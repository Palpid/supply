using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HKSupply.Models.Supply
{
    [Table("BOXES")]
    public class Box
    {
        [Column("ID_BOX", TypeName = "NVARCHAR"), Key, StringLength(50)]
        public string IdBox { get; set; }

        [Column("DESCRIPTION", TypeName = "NVARCHAR"), StringLength(250), Required]
        public string Description { get; set; }

        [Column("LENGTH"), Required]
        public int Length { get; set; }

        [Column("WIDTH"), Required]
        public int Width { get; set; }

        [Column("HEIGHT"), Required]
        public int Height { get; set; }

        [NotMapped]
        public string Name
        {
            get
            {
                return $"{Length.ToString()} X {Width.ToString()} X {Height.ToString()}";
            }
        }

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;

            Box box = (Box)obj;
            bool res = (
                IdBox == box.IdBox &&
                Description == box.Description &&
                Length == box.Length &&
                Width == box.Width &&
                Height == box.Height
                );

            return res;
        }

        public override int GetHashCode()
        {
            int hashCode =
              (IdBox == null ? 0 : IdBox.GetHashCode()) +
              (Description == null ? 0 : Description.GetHashCode()) +
              Length.GetHashCode() +
              Width.GetHashCode() +
              Height.GetHashCode();

            return hashCode;
        }
        #endregion

    }
}
