using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.Model
{
    // This is a specific Employee that has been assigned to a specific Shift.
    public class ShiftEmployee 
    {
        public int EmployeeID { get; set; }

        public int ShiftID { get; set; }

        bool Assigned { get; set; }
    }
}
