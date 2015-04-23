using BusinessManagementSystem.Data;
using BusinessManagementSystem.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystem.ViewModels.Reports
{
    class UserReportViewModel : ObservableObject, IPageViewModel
    {
        #region Properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return "User Report";
            }
        }

        /// <summary>
        /// Gets the type of the page.
        /// </summary>
        /// <value>
        /// The type of the page.
        /// </value>
        public Enums.PageType PageType
        {
            get
            {
                return Enums.PageType.Report;
            }
        }

        #endregion
    }
}
