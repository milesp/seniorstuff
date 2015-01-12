using ES.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ES.Controller
{
    public class ExcelInputInitialization
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public WorkPeriod ExcelInputValidation(string filename)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            int rCnt = 0;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(filename, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;

            double days = (range.Cells[1, 2] as Excel.Range).Value2;
            double shifts = (range.Cells[1, 1] as Excel.Range).Value2;
            WorkPeriod myWorkPeriod = new WorkPeriod((int)shifts, (int)days);
            string[] dayNames = new string[(int)days];
            string[] shiftNames = new string[(int)shifts];
            for (int i = 1; i <= days + shifts; i++)
            {
                if (i <= shifts)
                {
                    shiftNames[i - 1] = (string)(range.Cells[2, i] as Excel.Range).Value2;
                }
                else
                {
                    dayNames[i - (int)shifts - 1] = (string)(range.Cells[2, i] as Excel.Range).Value2;
                }
            }
            myWorkPeriod.WorkDayNames = (dayNames);
            myWorkPeriod.ShiftNames = shiftNames;
            //need to move workday and tempday outside of for loops to keep the data and be able to 
            //assign it to the workperiod
            List<Shift> shiftArray = new List<Shift>();
            WorkDay workDay = null;
            int shiftPositionInDay = 0;
            for (rCnt = 3; rCnt <= (days * shifts) + 2; rCnt++)
            {
                if ((rCnt - 3) % shifts == 0)
                {
                    shiftArray = new List<Shift>();
                    int curDay = (int)((rCnt - 3) / (int)shifts);
                    workDay = new WorkDay((int)shifts, (int)((rCnt - 3) / (int)shifts));
                    shiftPositionInDay = 0;
                }


                //day, shiftnum, emp total, exp avg, exp min, exp max, wage avg, shift per day
                int tempTotal;
                double tempExpMin, tempExpAvg, tempExpMax, tempWageavg;
                tempTotal = (int)(range.Cells[rCnt, 1] as Excel.Range).Value2;
                tempExpAvg = (range.Cells[rCnt, 2] as Excel.Range).Value2;
                tempExpMin = (range.Cells[rCnt, 3] as Excel.Range).Value2;
                tempExpMax = (range.Cells[rCnt, 4] as Excel.Range).Value2;
                tempWageavg = (range.Cells[rCnt, 5] as Excel.Range).Value2;

                //day, shiftnum, emp total, exp avg, exp min, exp max, wage avg, shift per day
                Shift newShift = new Shift(((rCnt - 3) / (int)shifts), shiftPositionInDay,
                     tempTotal, tempExpAvg, tempExpMin, tempExpMax, tempWageavg, (int)shifts);
                newShift.ShiftName = myWorkPeriod.ShiftNames[shiftPositionInDay];
                newShift.DayName = myWorkPeriod.WorkDayNames[(rCnt - 3) / (int)shifts];
                shiftArray.Add(newShift);


                if (shiftPositionInDay == shifts - 1)
                {
                    workDay.ShiftsInDay = shiftArray;
                    myWorkPeriod.WorkDays[(int)((rCnt - 3) / shifts)] = workDay;
                }
                shiftPositionInDay++;

            }
            myWorkPeriod = initializeEmployeesFromFile(xlApp, xlWorkBook, myWorkPeriod);
            //input is complete
            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            
            //populate all shifts with employee availability
            myWorkPeriod.initializeShiftDomains();

            //BacktrackingAlgorithm();

            return myWorkPeriod;
        }

        /// <summary>
        /// Method reads Excel file for specific input, and formats the
        /// WorkPeriod data structure based upon the input given
        /// </summary>
        /// <param name="xlApp"></param>
        /// <param name="xlWorkBook"></param>
        /// <param name="myWorkPeriod"></param>
        /// <returns>Finalized WorkPeriod instance with completed input</returns>
        private WorkPeriod initializeEmployeesFromFile(Excel.Application xlApp, Excel.Workbook xlWorkBook, WorkPeriod myWorkPeriod)
        {
            Excel.Worksheet xlWorkSheet;

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(2);
            Excel.Range range = xlWorkSheet.UsedRange;

            for (int rCnt = 1; rCnt <= range.Rows.Count; rCnt++)
            {
                Employee newEmp = new Employee();
                //string empName;
                // int shiftMin, shiftMax;
                for (int cCnt = 1; cCnt <= range.Columns.Count; cCnt++)
                {
                    if (cCnt == 1)
                    {
                        newEmp.Name = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                    }
                    else if (cCnt == 2)
                    {
                        newEmp.MinimumShifts = (int)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                    }
                    else if (cCnt == 3)
                    {
                        newEmp.MaximumShifts = (int)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                    }
                    else if (cCnt == 4)
                    {
                        newEmp.Experience = (int)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                    }
                    else if ((string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2 != "false")
                    {
                        var cellValue = (string)(xlWorkSheet.Cells[rCnt, cCnt] as Excel.Range).Value;

                        string pairValue = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value;
                        string[] splitPair = pairValue.Split(',');
                        DayShiftPair newAvailabilityPair = new DayShiftPair(Convert.ToInt32(splitPair[1]), Convert.ToInt32(splitPair[0]));
                        newEmp.Availability.Add(newAvailabilityPair);
                    }
                }
                newEmp.AvailabilityCoefficient = (double)newEmp.MinimumShifts / (double)newEmp.Availability.Count;
                myWorkPeriod.EmployeeList.Add(newEmp);
            }
            return myWorkPeriod;
        }
        
        /// <summary>
        /// Method releases the Excel objects after they are done being used
        /// </summary>
        /// <param name="obj"></param>
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
