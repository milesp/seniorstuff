using ES.Controller;
using ES.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeScheduler
{
    public partial class ScheduleInput : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                Repeater1.Visible = false;
            }
        }

        /*method on click of the button to get the 2 inputs, and instatiate
         * the instance of workPeriod.  This instance will initially create
         * days * shifts number of shifts into a 2d array of shifts.  These
         * shifts will then be bound to a repeater and posted to screen per corresponding
         * shift.
         * 
         * */
        protected void Button1_Click(object sender, EventArgs e)
        {

            int days = Convert.ToInt32(shiftsPerDay.Text.ToString());
            int shifts = Convert.ToInt32(numOfDays.Text.ToString());
            WorkPeriod myWorkPeriod = (new WorkPeriod(days, shifts));
            //Repeater for each individual shift found in each day, and each day in a week

            Repeater1.DataSource = myWorkPeriod.WorkDays;
            Repeater1.DataBind();
            Repeater1.Visible = true;
            Button3.Visible = true;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //Employee newEmp = new Employee(Convert.ToInt32(numOfDays.Text.ToString())
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

        }

        // This is when the user generates the schedule.  The 'go' button.
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile == true)
            {
                ExcelInputInitialization eii = new ExcelInputInitialization();
                WorkPeriod MyExcelSchedule = eii.ExcelInputValidation(FileUpload1.FileName);

                // Run the algorithm
                BacktrackingSolver bs = new BacktrackingSolver();
                //if the algorithm did not run successfully, this needs to be caught here
                bool success = bs.BacktrackingAlgorithm(ref MyExcelSchedule);
                if( success)
                {
                    MyExcelSchedule.EmployeeList = MyExcelSchedule.EmployeeList.OrderBy(emp => emp.Name).ToList<Employee>();
                }
                // Save the results into this Session variable, so that it can be used on another aspx page.
                Session["SchedResults"] = MyExcelSchedule;

                // Go to this webpage.
                Response.Redirect("ScheduleResults.aspx", false);
            }
        }
    }
}