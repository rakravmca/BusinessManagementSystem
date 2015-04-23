using BusinessManagementSystem.HelperClasses;

namespace BusinessManagementSystem.ViewModels
{
    public class UserViewModel : ObservableObject, IPageViewModel
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
                return "Users";
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
                return Enums.PageType.Admin;
            }
        }

        #endregion
    }
}
