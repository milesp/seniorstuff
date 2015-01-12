using ES.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.Controller
{
    public class BacktrackingSolver
    {
        public Boolean BacktrackingAlgorithm(ref WorkPeriod MyWorkPeriod)
        {
            Boolean isSolved = false;
            Stack<DayShiftPair> crtVariableList = new Stack<DayShiftPair>();

            while (!isSolved)
            {
                //if there exists a shift that has not fully scheduled
                //all positions aka it has a vacancy
                //
                DayShiftPair vacantShift = HasVacancies(MyWorkPeriod);
                if (vacantShift.Day == -1)
                {
                    isSolved = true;
                    break;
                }
                //if(!nextInstantiation(crt)
                //if the shifts domain still has potential to fill the open positions
                //get the next available emp and assign them to the shift

                else if (applyConstraints(vacantShift, ref MyWorkPeriod))
                {

                    //MyWorkPeriod.workWeek[vacantShift.Day].ShiftsInDay[vacantShift.ShiftNumber].EmployeeDomain.Sort();
                    //incriment the number of shifts the employee currently is assigned to
                    MyWorkPeriod.WorkDays[vacantShift.Day].ShiftsInDay[vacantShift.ShiftNumber].EmployeeDomain.ElementAt
                         (MyWorkPeriod.WorkDays[vacantShift.Day].ShiftsInDay[vacantShift.ShiftNumber].Offset).AssignedShifts++;
                    //add the shift pair to the employees stack of scheduled pairs
                    MyWorkPeriod.WorkDays[vacantShift.Day].ShiftsInDay[vacantShift.ShiftNumber].EmployeeDomain.ElementAt
                         (MyWorkPeriod.WorkDays[vacantShift.Day].ShiftsInDay[vacantShift.ShiftNumber].Offset).AssignedShiftsStack.Push(vacantShift);

                    //add the current employee, determined by the offset value within the domain, to the scheduled employees list
                    MyWorkPeriod.WorkDays[vacantShift.Day].ShiftsInDay[vacantShift.ShiftNumber].ScheduledEmployees.Add
                          (MyWorkPeriod.WorkDays[vacantShift.Day].ShiftsInDay[vacantShift.ShiftNumber].EmployeeDomain.ElementAt
                          (MyWorkPeriod.WorkDays[vacantShift.Day].ShiftsInDay[vacantShift.ShiftNumber].Offset));
                    //remove the current employee from the current shifts domain as it is now part of the solution
                    MyWorkPeriod.WorkDays[vacantShift.Day].ShiftsInDay[vacantShift.ShiftNumber].EmployeeDomain.RemoveAt(
                          MyWorkPeriod.WorkDays[vacantShift.Day].ShiftsInDay[vacantShift.ShiftNumber].Offset);
                    MyWorkPeriod.WorkDays[vacantShift.Day].ShiftsInDay[vacantShift.ShiftNumber].Vacancies--;
                    crtVariableList.Push(vacantShift);

                }
                else
                {
                    //current shift has no more potential choices for the availability
                    //aka the domain is completely exhausted either from over constraint(s)
                    //or previously assigned domains.  backtracking required.


                    //reset the current shifts offset back to 0 to ensure the selection starts at 0
                    //continue to remove previously added shifts until current shift is empty
                    //pop 1 (aka most recently added) variable from the most recently filled shift
                    //incriment that shifts offset by 1.
                    MyWorkPeriod.WorkDays[vacantShift.Day].ShiftsInDay[vacantShift.ShiftNumber].Offset = 0;
                    DayShiftPair mostRescentShift = null;
                    while (MyWorkPeriod.WorkDays[vacantShift.Day].ShiftsInDay[vacantShift.ShiftNumber].ScheduledEmployees.Count() != 0)
                    {
                        mostRescentShift = crtVariableList.Pop();
                        //decriment the current employees number of assigned shifts                             
                        MyWorkPeriod.WorkDays[mostRescentShift.Day].ShiftsInDay[mostRescentShift.ShiftNumber].ScheduledEmployees.LastOrDefault().AssignedShifts--;
                        //pop the most recently assigned pair off the assigned shift stack
                        MyWorkPeriod.WorkDays[mostRescentShift.Day].ShiftsInDay[mostRescentShift.ShiftNumber].ScheduledEmployees.LastOrDefault().AssignedShiftsStack.Pop();
                        MyWorkPeriod.WorkDays[mostRescentShift.Day].ShiftsInDay[mostRescentShift.ShiftNumber].EmployeeDomain.Add(
                             MyWorkPeriod.WorkDays[mostRescentShift.Day].ShiftsInDay[mostRescentShift.ShiftNumber].ScheduledEmployees.LastOrDefault());
                        MyWorkPeriod.WorkDays[vacantShift.Day].ShiftsInDay[vacantShift.ShiftNumber].ScheduledEmployees.RemoveAt(
                             MyWorkPeriod.WorkDays[mostRescentShift.Day].ShiftsInDay[mostRescentShift.ShiftNumber].ScheduledEmployees.Count - 1);


                        MyWorkPeriod.WorkDays[mostRescentShift.Day].ShiftsInDay[mostRescentShift.ShiftNumber].Vacancies++;
                        MyWorkPeriod.WorkDays[mostRescentShift.Day].ShiftsInDay[mostRescentShift.ShiftNumber].EmployeeDomain.Sort();
                        MyWorkPeriod.WorkDays[mostRescentShift.Day].ShiftsInDay[mostRescentShift.ShiftNumber].ScheduledEmployees.Sort();

                    }
                    //MyWorkPeriod.workWeek[mostRescentShift.Day].ShiftsInDay[mostRescentShift.ShiftNumber].Offset++;
                }
            }

            //MyWorkPeriod.printSchedule();

            foreach (WorkDay day in MyWorkPeriod.WorkDays)
            {
                foreach (Shift shift in day.ShiftsInDay)
                {
                    // Sort the list by Name.
                    shift.ScheduledEmployees = shift.ScheduledEmployees.OrderBy(emp => emp.Name).ToList<Employee>();
                }
            }

            return isSolved;
        }

        /// <summary>
        /// Method recieves a day/shift pair as input, and returns whether or not
        /// there is a potential solution to the variable that passes all constraints.
        /// If there the whole domain is traversed and the domain that satisfies the 
        /// constraints is smaller then the vacancies, false is returned
        /// </summary>
        /// <param name="curShiftPair">Day/Shift pair of the current shift within the work period</param>
        /// <returns>returns true if a applicable subset of the domain passes all constraints,
        /// false if the subset is smaller then the vacancies</returns>
        private Boolean applyConstraints(DayShiftPair curShiftPair, ref WorkPeriod MyWorkPeriod)
        {
            bool fulfilsConstraints = false;
            while (!fulfilsConstraints &&
                 (MyWorkPeriod.WorkDays[curShiftPair.Day].ShiftsInDay[curShiftPair.ShiftNumber].EmployeeDomain.Count
                 - MyWorkPeriod.WorkDays[curShiftPair.Day].ShiftsInDay[curShiftPair.ShiftNumber].Offset >=
                     MyWorkPeriod.WorkDays[curShiftPair.Day].ShiftsInDay[curShiftPair.ShiftNumber].Vacancies))
            {


                //stands for the current shifts employee at the offset position
                Employee curEmp = MyWorkPeriod.WorkDays[curShiftPair.Day].ShiftsInDay[curShiftPair.ShiftNumber].EmployeeDomain.ElementAt(
                     MyWorkPeriod.WorkDays[curShiftPair.Day].ShiftsInDay[curShiftPair.ShiftNumber].Offset);
                //count how many times the employee is already scheduled per current Day
                int shiftsScheduledInDay = 0;
                foreach (DayShiftPair scheduledPair in curEmp.AssignedShiftsStack)
                {
                    if (scheduledPair.Day == curShiftPair.Day)
                    {
                        shiftsScheduledInDay++;
                    }
                }

                //this if statement applies All Employee constraints.
                //in the future, custom constraints may be required, new implementation
                //will be required via list or some sort of string processing
                if ((curEmp.AssignedShifts < curEmp.MaximumShifts) &&
                     (curEmp.Experience >= MyWorkPeriod.WorkDays[curShiftPair.Day].ShiftsInDay[curShiftPair.ShiftNumber].ExpMin) &&
                     (shiftsScheduledInDay < curEmp.ShiftsAllowedPerDay))
                {
                    fulfilsConstraints = true;
                }
                else
                {
                    MyWorkPeriod.WorkDays[curShiftPair.Day].ShiftsInDay[curShiftPair.ShiftNumber].Offset++;
                }
            }
            return fulfilsConstraints;
        }
        
        /// <summary>
        /// Method checks to see whether or not a shift exists within the
        /// work period (domain of all shifts) that has vacancies that can
        /// be fulfilled.  If such a shift does not exist, the default Day/shift
        /// pair of (-1,-1) is returned.  If such a shift exists, the most 
        /// constrained shift is returned.
        /// </summary>
        /// <returns>Day/Shift pair of the most constrained shift, or (-1,-1) if it does not exist</returns>
        private DayShiftPair HasVacancies(WorkPeriod MyWorkPeriod)
        {
            DayShiftPair vacantShift = new DayShiftPair(-1, -1);
            double curHighestCoefficient = 0;
            foreach (WorkDay workDay in MyWorkPeriod.WorkDays)
            {
                foreach (Shift curShift in workDay.ShiftsInDay)
                {
                    updatePartialDomain(curShift);
                    //if there exists a shift that has yet to be fully populated
                    //with employees
                    int shiftVacancyTotal = curShift.EmployeeTotal - curShift.ScheduledEmployees.Count();
                    double curShiftCoeficient = curShift.Vacancies / (double)((curShift.EmployeeDomain.Count() - curShift.Offset));
                    if ((shiftVacancyTotal != 0) && (curHighestCoefficient < curShiftCoeficient))
                    {
                        vacantShift = new DayShiftPair(curShift.Day, curShift.ShiftNumber);
                        curHighestCoefficient = curShiftCoeficient;
                    }
                }
            }
            return vacantShift;
        }
        
        /// <summary>
        /// Method recieves a Day/shift pair as input, and updates the coefficients
        /// of the employees of the shift.  The employee domain per shift is only
        /// updated from the offset onwards.
        /// </summary>
        /// <param name="curShift">Day/shift pair for the employee domain to be updated </param>
        private void updatePartialDomain(Shift curShift)
        {
            List<Employee> partialDomain = curShift.EmployeeDomain.GetRange(curShift.Offset, curShift.EmployeeDomain.Count - curShift.Offset);

            curShift.EmployeeDomain.RemoveRange(curShift.Offset, curShift.EmployeeDomain.Count - curShift.Offset);

            foreach (Employee e in partialDomain)
            {
                e.UpdateCoeficient();
            }
            partialDomain.Sort();
            curShift.EmployeeDomain.AddRange(partialDomain);
        }
    
    }
}
