using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.Model
{
    /// <summary>
    /// This is a class that holds two values that correspond to the DayNumber and the ShiftNumber
    /// within a work week.
    /// </summary>
     public class DayShiftPair : System.Object
     {
          public int Day { get; set; }
          public int ShiftNumber { get; set; }
          public DayShiftPair(int day, int shiftNumber)
          {
               Day = day;
               ShiftNumber = shiftNumber;
          }
          public override bool Equals(System.Object obj)
          {
               // If parameter is null return false.
               if (obj == null)
               {
                    return false;
               }
               if (obj == null || GetType() != obj.GetType())
                    return false;

               // If parameter cannot be cast to Point return false.
               DayShiftPair p = obj as DayShiftPair;
               if ((System.Object)p == null)
               {
                    return false;
               }
               return (Day == p.Day) && (ShiftNumber == p.ShiftNumber);
          }

     }
 
}
