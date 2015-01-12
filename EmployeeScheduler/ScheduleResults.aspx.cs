using ES.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeScheduler
{
    public partial class ScheduleResults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                // Hide repeaters by default
                rInputData.Visible = false;
                rEmployerOutput.Visible = false;


                // we check to see if this value is in the query string.
                if (string.IsNullOrEmpty(Request.QueryString["ddlDisplayOptions"]) == false)
                {
                    // if we have passed in the query string value, then use that value.
                    // http://localhost:16100/ScheduleResults.aspx?ddlDisplayOptions=InputData

                    // this is the value that is selected in the dropdownlist
                    ddlDisplayOptions.SelectedValue = Request.QueryString["ddlDisplayOptions"];
                }

                WorkPeriod MyExcelSchedule = new WorkPeriod();

                // Retrieve the data from Session and store it in this local variable.
                MyExcelSchedule = (WorkPeriod)Session["SchedResults"];

                // Clear the data from Session.
                //Session["SchedResults"] = null;

                if (ddlDisplayOptions.SelectedValue == "Version1" )
                {
                    rEmployerOutput.DataSource = MyExcelSchedule.WorkDays;
                    rEmployerOutput.DataBind();
                    rEmployerOutput.Visible = true;
                }
                else if (ddlDisplayOptions.SelectedValue == "InputData")
                {

                    rInputData.DataSource = MyExcelSchedule.WorkDays;
                    rInputData.DataBind();
                    rInputData.Visible = true;
                }
                else if (ddlDisplayOptions.SelectedValue == "Version2")
                {
                    rEmployeeOutput.DataSource = MyExcelSchedule.EmployeeList;
                    rEmployeeOutput.DataBind();
                    rEmployeeOutput.Visible = true;
                }
                else if (ddlDisplayOptions.SelectedValue == "AllData")
                {

                }
            }
        }

        // Change the repeater that is displayed, based on the selected value in the dropdownlist.
        protected void bChangeView_Click(object sender, EventArgs e)
        {
            // Go to this webpage.
            Response.Redirect("ScheduleResults.aspx?ddlDisplayOptions=" + ddlDisplayOptions.SelectedValue, false);
        }
    }
}