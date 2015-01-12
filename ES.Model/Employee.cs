using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.Model
{
    public class Employee : IComparable<Employee>
    {
        // Database primary key.
        public int EmployeeID { get; set; }

        // Foreign key to the WorkPeriod.
        public int WorkPeriodID { get; set; }

        public string Name { get; set; }

        public string EmployeeNumber { get; set; }
        //public string SSN { get; set; }
        public double Experience { get; set; }
        public double Wage { get; set; }
        public List<DayShiftPair> Availability { get; set; }
        public int MinimumShifts { get; set; }
        public int MaximumShifts { get; set; }
        public int AssignedShifts { get; set; }
        public Stack<DayShiftPair> AssignedShiftsStack { get; set; }
        public double AvailabilityCoefficient { get; set; }
        public int ShiftsAllowedPerDay { get; set; }
        //name, ssn, exp, availability, wage, min, max
        public Employee(string name, string ssn, double exp, List<DayShiftPair> availability, double wage, int min, int max)
        {//bool[,] availability
            Name = name;
            EmployeeNumber = ssn;
            Experience = exp;
            Wage = wage;
            Availability = availability;
            MinimumShifts = min;
            MaximumShifts = max;
            AssignedShifts = 0;
            AvailabilityCoefficient = (double)min / (double)availability.Count;
            AssignedShiftsStack = new Stack<DayShiftPair>();
            ShiftsAllowedPerDay = 1;
            //Employee y = new Employee("miles", "124432", 1.0, 20.0, 1, 1);
        }
        public Employee()
        {
            Name = "0";
            EmployeeNumber = "0";
            Experience = 0;
            Wage = 0;
            //DayAvailability = availability;
            Availability = new List<DayShiftPair>();
            MinimumShifts = 0;
            MaximumShifts = 0;
            AssignedShifts = 0;
            AssignedShiftsStack = new Stack<DayShiftPair>();
            ShiftsAllowedPerDay = 1;
        }
        public int CompareTo(Employee b)
        {
            return b.AvailabilityCoefficient.CompareTo(this.AvailabilityCoefficient);
        }
        public void UpdateCoeficient()
        {
            if (AssignedShifts < MinimumShifts)
            {
                AvailabilityCoefficient = (double)(MinimumShifts - AssignedShifts) / (double)(Availability.Count - AssignedShiftsStack.Count);
            }
            else
            {
                AvailabilityCoefficient = (AssignedShifts / (double)MaximumShifts) / (double)(Availability.Count - AssignedShiftsStack.Count);
            }
        }
    }
}
