using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.Model
{
    public class WebsiteUser
    {
        // Primary Key
        public int WebsiteUserID { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
