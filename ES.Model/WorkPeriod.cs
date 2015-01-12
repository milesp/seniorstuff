using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.Model
{
     public class WorkPeriod
     {
         // Database primary key.
         public int WorkPeriodID { get; set; }

         // ForeignKey to the WebsiteUser.  This is the user that created this record.
         public int WebsiteUserID_CreatedBy { get; set; }
          private int NumOfShiftsPerDay { get; set; }

          public string[] ShiftNames { get; set; }

         // A comma-separated list of the ShiftNames.
          public string ShiftNamesList { get; set; }

         private int WorkDaysPerPeriod { get; set; }
         public string[] WorkDayNames { get; set; }
         public string WorkDayNamesList { get; set; }
         
         // This is the list of WorkDay's in the workperiod
         public List<WorkDay> WorkDays { get; set; }

          public List<Employee> EmployeeList = new List<Employee>();
          public WorkPeriod(int perDays, int daysInWeek)
          {
               NumOfShiftsPerDay = perDays;
               WorkDaysPerPeriod = daysInWeek;
               bool[] availability = new bool[WorkDaysPerPeriod];
               List<WorkDay> workDayArray = new List<WorkDay>();
               for (int i = 0; i < WorkDaysPerPeriod; i++)
               {
                   //num per day, day of the week
                    WorkDay blankDay = new WorkDay(perDays, i);
                    workDayArray.Add(blankDay);
                   
               }
               WorkDays = workDayArray;
          }
          public WorkPeriod()
          {
               NumOfShiftsPerDay = 1;
               WorkDaysPerPeriod = 1;

               List<WorkDay> workDayArray = new List<WorkDay>();
               for (int i = 0; i < WorkDaysPerPeriod; i++)
               {
                    WorkDay blankDay = new WorkDay(0, 0);
                    workDayArray.Add(blankDay);

               }
               WorkDays = workDayArray;
               
          }
          public void initializeShiftDomains()
          {

              foreach (Employee emp in this.EmployeeList)
              {
                  foreach (DayShiftPair shiftPair in emp.Availability)
                  {
                      this.WorkDays[shiftPair.Day].ShiftsInDay[shiftPair.ShiftNumber].EmployeeDomain.Add(emp);
                  }
              }
              foreach (WorkDay workDay in this.WorkDays)
              {
                  foreach (Shift curShift in workDay.ShiftsInDay)
                  {
                      curShift.EmployeeDomain.Sort();
                  }
              }

          }
          public string printSchedule()
          {
              string output = "";
              foreach (WorkDay curDay in this.WorkDays)
              {
                  foreach (Shift curShift in curDay.ShiftsInDay)
                  {
                      output = output + curShift.Day + curShift.ShiftNumber + "@";
                      output = output + curShift.EmployeeTotal + "@";
                      output = output + curShift.ScheduledEmployees.Count() + "@";
                      output = output + "     ";
                  }
              } // System.Environment.NewLine
              output = output.Replace("@", " ");
              //scheduleOutput.Text = output;
              return output;
          }
     }

}
