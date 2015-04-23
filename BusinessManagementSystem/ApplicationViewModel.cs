using BusinessManagementSystem.Data;
using BusinessManagementSystem.HelperClasses;
using BusinessManagementSystem.ViewModels;
using BusinessManagementSystem.ViewModels.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace BusinessManagementSystem
{
    public class ApplicationViewModel : ObservableObject
    {
        #region Fields

        private ICommand _changePageCommand;
        private ICommand _openLoginUserCommand;
        private ICommand _closeLoginUserCommand;
        private ICommand _loginUserCommand;
        private ICommand _logoutCommand;
        private ICommand _cancelLoginCommand;
        private ICommand _redirectToHomePageCommand;
        private ICommand _openRegistrationnUserCommand;
        private ICommand _closeRegistrationnUserCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;
        private List<IPageViewModel> _generalViewModels;
        private List<IPageViewModel> _administratorViewModels;
        private List<IPageViewModel> _reportViewModels;
        private bool _isLoginPopupOpen;
        private User _currentUser;
        private string _userName;
        private string _userDisplayName;
        private bool _isAuntheticated;
        private bool _isAdmin;
        private bool _isUser;
        private bool _loginError;
        private bool _isRegistrationPopupOpen;

        public PasswordBox MyPasswordBox;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationViewModel"/> class.
        /// </summary>
        public ApplicationViewModel()
        {
            //Add Page View Models
            PageViewModels.Add(new UserViewModel());
            //PageViewModels.Add(new StudentViewModel());
            PageViewModels.Add(new UserReportViewModel());

            RedirectToHomePage();

            OpenLoginUserPopup();
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the change page command.
        /// </summary>
        /// <value>
        /// The change page command.
        /// </value>
        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }

        /// <summary>
        /// Gets the open login user command.
        /// </summary>
        /// <value>
        /// The open login user command.
        /// </value>
        public ICommand OpenLoginUserCommand
        {
            get
            {
                if (_openLoginUserCommand == null)
                {
                    _openLoginUserCommand = new RelayCommand(
                        p => OpenLoginUserPopup());
                }
                return _openLoginUserCommand;
            }
        }

        /// <summary>
        /// Gets the close login user command.
        /// </summary>
        /// <value>
        /// The close login user command.
        /// </value>
        public ICommand CloseLoginUserCommand
        {
            get
            {
                if (_closeLoginUserCommand == null)
                {
                    _closeLoginUserCommand = new RelayCommand(
                        p => CloseLoginUserPopup());
                }
                return _closeLoginUserCommand;
            }
        }

        /// <summary>
        /// Gets the login user command.
        /// </summary>
        /// <value>
        /// The login user command.
        /// </value>
        public ICommand LoginUserCommand
        {
            get
            {
                if (_loginUserCommand == null)
                {
                    _loginUserCommand = new RelayCommand(
                        p => LoginUser((Object)p),
                            p => p is Object);
                }

                return _loginUserCommand;
            }
        }

        /// <summary>
        /// Gets the login user command.
        /// </summary>
        /// <value>
        /// The login user command.
        /// </value>
        public ICommand CancelLoginCommand
        {
            get
            {
                if (_cancelLoginCommand == null)
                {
                    _cancelLoginCommand = new RelayCommand(
                        p => CancelLogin((Object)p),
                            p => p is Object);
                }

                return _cancelLoginCommand;
            }
        }

        /// <summary>
        /// Gets the logout command.
        /// </summary>
        /// <value>
        /// The logout command.
        /// </value>
        public ICommand LogoutCommand
        {
            get
            {
                if (_logoutCommand == null)
                {
                    _logoutCommand = new RelayCommand(
                        p => Logout());
                }

                return _logoutCommand;
            }
        }

        /// <summary>
        /// Gets the redirect to home page command.
        /// </summary>
        /// <value>
        /// The redirect to home page command.
        /// </value>
        public ICommand RedirectToHomePageCommand
        {
            get
            {
                if (_redirectToHomePageCommand == null)
                {
                    _redirectToHomePageCommand = new RelayCommand(
                        p => RedirectToHomePage());
                }

                return _redirectToHomePageCommand;
            }
        }

        public ICommand OpenRegistrationnUserCommand
        {
            get
            {
                if (_openRegistrationnUserCommand == null)
                {
                    _openRegistrationnUserCommand = new RelayCommand(
                        p => OpenRegistrationUserPopup());
                }
                return _openRegistrationnUserCommand;
            }
        }

        public ICommand CloseRegistrationnUserCommand
        {
            get
            {
                if (_closeRegistrationnUserCommand == null)
                {
                    _closeRegistrationnUserCommand = new RelayCommand(
                        p => CloseRegistrationUserPopup());
                }
                return _closeRegistrationnUserCommand;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the page view models.
        /// </summary>
        /// <value>
        /// The page view models.
        /// </value>
        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        /// <summary>
        /// Gets the administration view models.
        /// </summary>
        /// <value>
        /// The administration view models.
        /// </value>
        public List<IPageViewModel> AdministratiorViewModels
        {
            get
            {
                return _administratorViewModels;
            }
            set
            {
                if (_administratorViewModels != value)
                {
                    _administratorViewModels = value;
                    OnPropertyChanged("AdministratiorViewModels");
                }
            }
        }

        public List<IPageViewModel> ReportViewModels
        {
            get
            {
                return _reportViewModels;
            }
            set
            {
                if (_reportViewModels != value)
                {
                    _reportViewModels = value;
                    OnPropertyChanged("ReportViewModels");
                }
            }
        }

        /// <summary>
        /// Gets or sets the current view models.
        /// </summary>
        /// <value>
        /// The current view models.
        /// </value>
        public List<IPageViewModel> GeneralViewModels
        {
            get
            {
                return _generalViewModels;
            }
            set
            {
                if (_generalViewModels != value)
                {
                    _generalViewModels = value;
                    OnPropertyChanged("GeneralViewModels");
                }
            }
        }

        /// <summary>
        /// Gets or sets the current page view model.
        /// </summary>
        /// <value>
        /// The current page view model.
        /// </value>
        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is login popup open.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is login popup open; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoginPopupOpen
        {
            get
            {
                return _isLoginPopupOpen;
            }
            set
            {
                if (_isLoginPopupOpen != value)
                {
                    _isLoginPopupOpen = value;
                    OnPropertyChanged("IsLoginPopupOpen");
                }
            }
        }

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                if (value != _currentUser)
                {
                    _currentUser = value;
                    OnPropertyChanged("CurrentUser");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged("UserName");
            }
        }

        /// <summary>
        /// Gets or sets the display name of the user.
        /// </summary>
        /// <value>
        /// The display name of the user.
        /// </value>
        public string UserDisplayName
        {
            get { return _userDisplayName; }
            set
            {
                _userDisplayName = value;
                RaisePropertyChanged("UserDisplayName");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is auntheticated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is auntheticated; otherwise, <c>false</c>.
        /// </value>
        public bool IsAuntheticated
        {
            get { return _isAuntheticated; }
            set
            {
                _isAuntheticated = value;
                RaisePropertyChanged("IsAuntheticated");
            }
        }

        /// <summary>
        /// Gets or sets the login error message.
        /// </summary>
        /// <value>
        /// The login error message.
        /// </value>
        public bool LoginError
        {
            get
            {
                return _loginError;
            }
            set
            {
                if (_loginError != value)
                {
                    _loginError = value;
                    OnPropertyChanged("LoginError");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is admin.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is admin; otherwise, <c>false</c>.
        /// </value>
        public bool IsAdmin
        {
            get
            {
                return _isAdmin;
            }
            set
            {
                if (_isAdmin != value)
                {
                    _isAdmin = value;
                    OnPropertyChanged("IsAdmin");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is user.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is user; otherwise, <c>false</c>.
        /// </value>
        public bool IsUser
        {
            get
            {
                return _isUser;
            }
            set
            {
                if (_isUser != value)
                {
                    _isUser = value;
                    OnPropertyChanged("IsUser");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is registration popup open.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is registration popup open; otherwise, <c>false</c>.
        /// </value>
        public bool IsRegistrationPopupOpen
        {
            get
            {
                return _isRegistrationPopupOpen;
            }
            set
            {
                if (_isRegistrationPopupOpen != value)
                {
                    _isRegistrationPopupOpen = value;
                    OnPropertyChanged("IsRegistrationPopupOpen");
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Changes the view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        /// <summary>
        /// Opens the login user popup.
        /// </summary>
        private void OpenLoginUserPopup()
        {
            LoginError = false;
            UserName = string.Empty;

            if (MyPasswordBox != null)
            {
                MyPasswordBox.Password = string.Empty;
            }

            IsLoginPopupOpen = true;
        }

        /// <summary>
        /// Closes the login user popup.
        /// </summary>
        private void CloseLoginUserPopup()
        {
            IsLoginPopupOpen = false;
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="objPasswordBox">The object password box.</param>
        private void LoginUser(Object objPasswordBox)
        {
            MyPasswordBox = objPasswordBox as PasswordBox;

            using (BusinessManagementSystemEntities entities = new BusinessManagementSystemEntities())
            {
                CurrentUser = (from user in entities.Users
                               where
                               (
                               user.UserName == UserName &&
                               user.Password == MyPasswordBox.Password
                               )
                               select user).SingleOrDefault();
            }

            if (CurrentUser != null)
            {
                IsAuntheticated = true;
                IsAdmin = CurrentUser.UserType == (int)Enums.UserType.Admin;
                IsUser = CurrentUser.UserType == (int)Enums.UserType.User;

                UserDisplayName = (!String.IsNullOrWhiteSpace(CurrentUser.Initial) ? CurrentUser.Initial + " " : string.Empty) +
                                    CurrentUser.FirstName + " " + (!String.IsNullOrWhiteSpace(CurrentUser.MiddleName) ? CurrentUser.MiddleName + " " : string.Empty) +
                                    " " + CurrentUser.LastName;
                CloseLoginUserPopup();

                GeneralViewModels = PageViewModels.Where(w => w.PageType == Enums.PageType.General).ToList();
                AdministratiorViewModels = PageViewModels.Where(w => w.PageType == Enums.PageType.Admin).ToList();
                ReportViewModels = PageViewModels.Where(w => w.PageType == Enums.PageType.Report).ToList();

                // Set starting page
                RedirectToHomePage();
            }
            else
            {
                LoginError = true;
            }
        }

        /// <summary>
        /// Cancels the login.
        /// </summary>
        /// <param name="objPasswordBox">The object password box.</param>
        private void CancelLogin(Object objPasswordBox)
        {
            MyPasswordBox = objPasswordBox as PasswordBox;
            CloseLoginUserPopup();
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        private void Logout()
        {
            CurrentUser = null;
            IsAuntheticated = false;
            IsAdmin = false;
            IsUser = false;
            UserDisplayName = string.Empty;

            //Clear Curent View Models
            GeneralViewModels.Clear();
            AdministratiorViewModels.Clear();

            // Set starting page
            RedirectToHomePage();

            OpenLoginUserPopup();
        }

        /// <summary>
        /// Redirects to home page.
        /// </summary>
        private void RedirectToHomePage()
        {
            CurrentPageViewModel = new HomeViewModel();
        }

        /// <summary>
        /// Opens the registration user popup.
        /// </summary>
        private void OpenRegistrationUserPopup()
        {
            IsRegistrationPopupOpen = true;
        }

        /// <summary>
        /// Closes the registration user popup.
        /// </summary>
        private void CloseRegistrationUserPopup()
        {
            IsRegistrationPopupOpen = false;
        }

        #endregion
    }
}
