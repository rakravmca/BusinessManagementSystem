using BusinessManagementSystem.Data;
using BusinessManagementSystem.HelperClasses;
using BusinessManagementSystem.Models;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System;

namespace BusinessManagementSystem.ViewModels
{
    public class UserViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private ICommand _addUserPopupCommand;
        private ICommand _editUserPopupCommand;
        private ICommand _closeUserPopupCommand;
        private ICommand _saveUserCommand;

        private bool _isUserPopupOpen;
        private UserModel _currentUser;
        private List<UserModel> _userCollection;

        #endregion

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

        /// <summary>
        /// Gets or sets a value indicating whether this instance is User popup open.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is User popup open; otherwise, <c>false</c>.
        /// </value>
        public bool IsUserPopupOpen
        {
            get
            {
                return _isUserPopupOpen;
            }
            set
            {
                if (_isUserPopupOpen != value)
                {
                    _isUserPopupOpen = value;
                    OnPropertyChanged("IsUserPopupOpen");
                }
            }
        }

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public UserModel CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                    OnPropertyChanged("CurrentUser");
                }
            }
        }

        /// <summary>
        /// Gets or sets the User collection.
        /// </summary>
        /// <value>
        /// The User collection.
        /// </value>
        public List<UserModel> UserCollection
        {
            get
            {
                return _userCollection;
            }
            set
            {
                if (_userCollection != value)
                {
                    _userCollection = value;
                    OnPropertyChanged("UserCollection");
                }
            }
        }

        #endregion

        #region Constructor

        public UserViewModel()
        {
            UserCollection = GetUsers();
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the add User popup command.
        /// </summary>
        /// <value>
        /// The add User popup command.
        /// </value>
        public ICommand AddUserPopupCommand
        {
            get
            {
                if (_addUserPopupCommand == null)
                {
                    _addUserPopupCommand = new RelayCommand(
                        p => AddUserPopup());
                }

                return _addUserPopupCommand;
            }
        }

        /// <summary>
        /// Gets the edit User popup command.
        /// </summary>
        /// <value>
        /// The edit User popup command.
        /// </value>
        public ICommand EditUserPopupCommand
        {
            get
            {
                if (_editUserPopupCommand == null)
                {
                    _editUserPopupCommand = new RelayCommand(
                        p => EditUserPopup((UserModel)p),
                        p => p is UserModel);
                }

                return _editUserPopupCommand;
            }
        }

        /// <summary>
        /// Gets the close User popup command.
        /// </summary>
        /// <value>
        /// The close User popup command.
        /// </value>
        public ICommand CloseUserPopupCommand
        {
            get
            {
                if (_closeUserPopupCommand == null)
                {
                    _closeUserPopupCommand = new RelayCommand(
                        p => CloseUserPopup());
                }

                return _closeUserPopupCommand;
            }
        }

        public ICommand SaveUserCommand
        {
            get
            {
                if (_saveUserCommand == null)
                {
                    _saveUserCommand = new RelayCommand(
                        p => SaveUser());
                }

                return _saveUserCommand;
            }
        }

        #endregion

        /// <summary>
        /// Adds the User popup.
        /// </summary>
        private void AddUserPopup()
        {
            CurrentUser = new UserModel()
            {
                BirthDate = DateTime.Now
            };

            OpenUserPopup();
        }

        /// <summary>
        /// Edits the User popup.
        /// </summary>
        /// <param name="User">The User.</param>
        private void EditUserPopup(UserModel User)
        {
            CurrentUser = new UserModel()
            {
                Id = User.Id,
                Initial = User.Initial,
                FirstName = User.FirstName,
                MiddleName = User.MiddleName,
                LastName = User.LastName,
                Phone = User.Phone,
                BirthDate = User.BirthDate,
                Gender = User.Gender,
                UserType = User.UserType,
                UserName = User.UserName
            };

            OpenUserPopup();
        }

        /// <summary>
        /// Saves the User.
        /// </summary>
        private void SaveUser()
        {
            if (!CurrentUser.HasErrors)
            {
                using (BusinessManagementSystemEntities entities = new BusinessManagementSystemEntities())
                {
                    User user;

                    if (CurrentUser.Id == 0)
                    {
                        user = new User();
                    }
                    else
                    {
                        user = entities.Users.SingleOrDefault(s => s.Id == CurrentUser.Id);
                    }

                    if (user != null)
                    {
                        user.FirstName = CurrentUser.FirstName;
                        user.MiddleName = CurrentUser.MiddleName;
                        user.LastName = CurrentUser.LastName;
                        user.Gender = CurrentUser.Gender;
                        user.BirthDate = CurrentUser.BirthDate;
                        user.Phone = CurrentUser.Phone;

                        if (CurrentUser.Id == 0)
                        {
                            entities.Users.Add(user);
                        }

                        entities.SaveChanges();

                        CloseUserPopup();

                        if (CurrentUser.Id == 0)
                        {
                            CurrentUser.Id = user.Id;
                            UserCollection.Add(CurrentUser);
                            UserCollection = UserCollection.ToList();
                        }
                        else
                        {
                            UserCollection.Where(w => w.Id == CurrentUser.Id).ToList().ForEach(s =>
                            {
                                s.FirstName = CurrentUser.FirstName;
                                s.MiddleName = CurrentUser.MiddleName;
                                s.LastName = CurrentUser.LastName;
                                s.Phone = CurrentUser.Phone;
                                s.BirthDate = CurrentUser.BirthDate;
                                s.Gender = CurrentUser.Gender;
                            });
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the Users.
        /// </summary>
        /// <returns></returns>
        private List<UserModel> GetUsers()
        {
            using (BusinessManagementSystemEntities entities = new BusinessManagementSystemEntities())
            {
                return (from user in entities.Users
                        select new UserModel
                        {
                            Id = user.Id,
                            Initial=user.Initial,
                            FirstName = user.FirstName,
                            MiddleName = user.MiddleName,
                            LastName = user.LastName,
                            BirthDate = user.BirthDate,
                            Gender = user.Gender,
                            Phone = user.Phone,
                            EmailAddress=user.EmailAddress
                        }).ToList();
            }
        }

        /// <summary>
        /// Opens the User popup.
        /// </summary>
        private void OpenUserPopup()
        {
            IsUserPopupOpen = true;
        }

        /// <summary>
        /// Closes the User popup.
        /// </summary>
        private void CloseUserPopup()
        {
            IsUserPopupOpen = false;
        }
    }
}
