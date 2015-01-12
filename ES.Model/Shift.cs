using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.Model
{
    /*
     * Class holds the Shift specific data.
     * Each shift is specific to the time of day and 
     * day of the week.  Each shift requires parameters
     * which will be used in the CSP as constraints that 
     * must be satisfied for each corresponding scheduled
     * employee.
     * 
     */
    public class Shift
    {
        // Database primary key.
        public int ShiftID { get; set; }

        // Foreign key to the WorkPeriod.
        public int WorkPeriodID { get; set; }

        public int Day { get; set; }
        public string DayName { get; set; }
        public int ShiftNumber { get; set; }
        public string ShiftName { get; set; }
        public int Vacancies { get; set; }
        public int EmployeeTotal { get; set; }
        public int ShiftsPerDay { get; set; }
        public int Offset { get; set; }
        public double ExpAvg { get; set; }
        public double ExpMin { get; set; }
        public double ExpMax { get; set; }
        public double WageAvg { get; set; }

        // All of the employees that are potentially able to be scheduled for a shift.
        // In other words, all of the employees that say that they can work that day.
        public List<Employee> EmployeeDomain { get; set; }

        public List<Employee> ScheduledEmployees { get; set; }

        /*Main constructor that takes in each specific parameter from the user
         * this is the primary constructor that will be used to create all
         * shifts for the CSP
       */
        //day, shiftnum, emp total, exp avg, exp min, exp max, wage avg, shift per day
        public Shift(int day, int shiftNumber, int empTotal, double expAvg,
             double expMin, double expMax, double wageAvg, int shiftsPerDay)
        {
            Day = day;
            ShiftNumber = shiftNumber;
            EmployeeTotal = empTotal;
            ExpAvg = expAvg;
            ExpMin = expMin;
            ExpMax = expMax;
            WageAvg = wageAvg;
            ShiftsPerDay = shiftsPerDay;
            Vacancies = empTotal;
            ScheduledEmployees = new List<Employee>();
            EmployeeDomain = new List<Employee>();
            Offset = 0;
        }
        //shift val within day, num per day, day of the week 
        public Shift(int shiftNumInDay, int totalShiftsPerDay, int dayNum)
        {
            Day = dayNum;
            ShiftNumber = shiftNumInDay;
            ShiftsPerDay = totalShiftsPerDay;
            EmployeeTotal = 0;
            ExpAvg = 0;
            ExpMin = 0;
            ExpMax = 0;
            WageAvg = 0;
            Vacancies = 0;
            ScheduledEmployees = new List<Employee>();
            EmployeeDomain = new List<Employee>();
            Offset = 0;
        }
        public Shift()
        {
            Day = 0;
            ShiftNumber = 0;
            EmployeeTotal = 0;
            ExpAvg = 0;
            ExpMin = 0;
            ExpMax = 0;
            WageAvg = 0;
            Vacancies = 0;
            ScheduledEmployees = new List<Employee>();
            EmployeeDomain = new List<Employee>();
            Offset = 0;
        }
    }
}