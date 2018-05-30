using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM.Helpers
{
    public static class CustomExtensions
    {
        public static decimal ObjToDecimal(this object obj)
        {
            try
            {
                decimal dec = 0;

                if (obj == null || obj == DBNull.Value)
                    dec = 0;

                if (decimal.TryParse(obj.ToString(), out dec) == false)
                    dec = 0;

                return dec;

            }
            catch
            {
                throw;
            }
        }

        public static int ObjToInt(this object obj)
        {
            try
            {
                int num = 0;

                if (obj == null || obj == DBNull.Value)
                    num = 0;

                if (int.TryParse(obj.ToString(), out num) == false)
                    num = 0;

                return num;

            }
            catch
            {
                throw;
            }
        }
    }
}
