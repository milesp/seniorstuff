using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.Model
{
     
     public class WorkDay
     {
          private int NumOfShiftsPerDay { get; set; }
          public List<Shift> ShiftsInDay { get; set; }
          private int DayOfWeek { get; set; }

          /*
           * Class representing each day of the work week
           * class holds an array of shifts per day, as well
           * as an int representing its position within the week
           * ie. dayOfWeek = 3 is the third work day of the
           * work week
           * */
          public WorkDay(int shiftsPerDay, int dayOfWeek)
          {

               ShiftsInDay = new List<Shift>();
               NumOfShiftsPerDay = shiftsPerDay;
               DayOfWeek = dayOfWeek;
               for (int i = 0; i < shiftsPerDay; i++)
               {
                   //shift val within day, num per day, day of the week
                    Shift emptyShift = new Shift(i, shiftsPerDay, dayOfWeek);
                    ShiftsInDay.Add(emptyShift);
               }

          }
          public WorkDay(int shiftsPerDay, int dayOfWeek, List<Shift> shifts)
          {
               ShiftsInDay = shifts;
               NumOfShiftsPerDay = shiftsPerDay;
               DayOfWeek = dayOfWeek;
          }

     }
}
