using BusinessManagementSystem.HelperClasses;

namespace BusinessManagementSystem.ViewModels
{
    public class HomeViewModel : ObservableObject, IPageViewModel
    {
        #region Properties

        public string Name
        {
            get
            {
                return "Home";
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
                return Enums.PageType.General;
            }
        }

        #endregion
    }
}
