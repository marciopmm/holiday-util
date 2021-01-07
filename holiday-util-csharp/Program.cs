using System;
using System.IO;
using System.Text.RegularExpressions;

namespace holiday_util_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Holiday Util ***");
            Console.Write("Type the day you want to check (yyyy-mm-dd): ");
            string strDate = Console.ReadLine();

            Regex rgxDate = new Regex(@"(\d{4})\/(\d{2})\/(\d{2})");
            Match matchIn = rgxDate.Match(strDate); 
            if (matchIn.Success)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("--- ERROR --- Date format is not recognized.");
                return;
            }

            DateTime date = new DateTime(int.Parse(matchIn.Groups[1].Value),
                                         int.Parse(matchIn.Groups[2].Value),
                                         int.Parse(matchIn.Groups[1].Value));

            string holidayList = null;
            using (StreamReader sr = new StreamReader("HolidayList.txt"))
            {
                holidayList = sr.ReadToEnd();
            }

            HolidayService service = new HolidayService();
            bool isHoliday = service.IsHoliday(date, holidayList);
            DateTime workingDayBefore = service.GetWorkingDayBefore(date, holidayList);
            DateTime workingDayAfter = service.GetWorkingDayAfter(date, holidayList);

            Console.WriteLine();
            Console.WriteLine($"Check for {date}:");
            if (isHoliday)
                Console.WriteLine(" - It is a holiday!");
            else
                Console.WriteLine(" - It is not a holiday ... :(");

            Console.WriteLine($" - Working day before is {workingDayBefore}");
            Console.WriteLine($" - Working day after is {workingDayAfter}");
            Console.WriteLine();
            Console.WriteLine("Thanks for using it!");
        }
    }
}
