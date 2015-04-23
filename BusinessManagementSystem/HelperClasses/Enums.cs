using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystem.HelperClasses
{
    public class Enums
    {
        public enum UserType
        {
            Admin = 0,
            User = 1
        }

        public enum PageType
        {
            Admin = 0,
            General = 1,
            Report = 2
        }
    }
}
