using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Helpers
{
    public class DatetimeHelper
    {
        public static DateTime FirtDateOfWeek(int year, int weekOfYear, System.Globalization.CultureInfo ci = null)
        {
            try
            {
                if (ci == null)
                {
                    ci = System.Globalization.CultureInfo.InvariantCulture;
                }

                DateTime jan1 = new DateTime(year, 1, 1);
                int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
                DateTime firstWeekDay = jan1.AddDays(daysOffset);
                int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
                if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
                {
                    weekOfYear -= 1;
                }
                return firstWeekDay.AddDays(weekOfYear * 7);


            }
            catch
            {
                throw;
            }
        }
    }
}
