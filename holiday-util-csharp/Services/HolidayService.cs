using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace holiday_util_csharp
{
    public class HolidayService
    {
        public bool IsHoliday(DateTime date, string holidayList)
        {
            var orderedList = ExtractOrderedList(holidayList);
            return orderedList.Contains(date);
        }

        public DateTime GetWorkingDayBefore(DateTime date, string holidayList)
        {
            
        }

        private IOrderedEnumerable<DateTime> ExtractOrderedList(string holidayList)
        {
            var list = new List<DateTime>();
            Regex rgxDate = new Regex(@"(\d{4})-(\d{1,2})-(\d{1,2})");
            MatchCollection matches = rgxDate.Matches(holidayList);
            foreach (Match match in matches)
            {
                list.Add(new DateTime(int.Parse(match.Groups[1].Value),
                                      int.Parse(match.Groups[2].Value),
                                      int.Parse(match.Groups[3].Value));
            }

            return list.OrderByDescending(x => x);
        }
    }
}