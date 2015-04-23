using BusinessManagementSystem.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystem.Models
{
    public class UserModel : ObservableObject, IDataErrorInfo
    {
        #region Fields

        private long _id { get; set; }
        private string _initial { get; set; }
        private string _firstName { get; set; }
        private string _middleName { get; set; }
        private string _lastName { get; set; }
        private DateTime _birthDate { get; set; }
        private string _emailAddress { get; set; }
        private string _gender { get; set; }
        private string _userName { get; set; }
        private string _password { get; set; }
        private string _phone { get; set; }
        private int _userType { get; set; }

        //This dictionary contains a list of our validation errors for each field
        private Dictionary<string, string> validationErrors = new Dictionary<string, string>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        /// <summary>
        /// Gets or sets the initial.
        /// </summary>
        /// <value>
        /// The initial.
        /// </value>
        public string Initial
        {
            get
            {
                return _initial;
            }
            set
            {
                if (_initial != value)
                {
                    _initial = value;
                    OnPropertyChanged("Initial");
                }
            }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>
        /// The name of the middle.
        /// </value>
        public string MiddleName
        {
            get
            {
                return _middleName;
            }
            set
            {
                if (_middleName != value)
                {
                    _middleName = value;
                    OnPropertyChanged("MiddleName");
                }
            }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged("LastName");
                }
            }
        }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                if (_birthDate != value)
                {
                    _birthDate = value;
                    OnPropertyChanged("BirthDate");
                }
            }
        }

        public String StringBirthDate
        {
            get
            {
                return _birthDate.ToShortDateString();
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _birthDate = DateTime.Parse(value);
                    OnPropertyChanged("StringBirthDate");
                }
            }
        }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                if (_gender != value)
                {
                    _gender = value;
                    OnPropertyChanged("Gender");
                }
            }
        }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged("Phone");
                }
            }
        }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        public string EmailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                if (_emailAddress != value)
                {
                    _emailAddress = value;
                    OnPropertyChanged("EmailAddress");
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
            get
            {
                return _userName;
            }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged("EmailAddress");
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>
        /// The type of the user.
        /// </value>
        public int UserType
        {
            get
            {
                return _userType;
            }
            set
            {
                if (_userType != value)
                {
                    _userType = value;
                    OnPropertyChanged("UserType");
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has errors.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has errors; otherwise, <c>false</c>.
        /// </value>
        public virtual bool HasErrors
        {
            get { return (validationErrors.Count > 0); }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;

                switch (columnName)
                {
                    case "FirstName":
                        if (string.IsNullOrEmpty(FirstName))
                        {
                            result = "First Name is required!";
                            AddError(columnName, result);
                        }
                        else
                        {
                            RemoveError(columnName);
                        }
                        break;
                    case "LastName":
                        if (string.IsNullOrEmpty(LastName))
                        {
                            result = "Last Name is required!";
                            AddError(columnName, result);
                        }
                        else
                        {
                            RemoveError(columnName);
                        }
                        break;
                    case "Gender":
                        if (string.IsNullOrEmpty(Gender))
                        {
                            result = "Gender is required!";
                            AddError(columnName, result);
                        }
                        else
                        {
                            RemoveError(columnName);
                        }
                        break;
                    case "StringBirthDate":
                        if (string.IsNullOrWhiteSpace(StringBirthDate))
                        {
                            result = "Date Of Birth is required!";
                            AddError(columnName, result);
                        }
                        else
                        {
                            RemoveError(columnName);
                        }
                        break;
                };

                return result;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="msg">The MSG.</param>
        private void AddError(string columnName, string msg)
        {
            if (!validationErrors.ContainsKey(columnName))
            {
                validationErrors.Add(columnName, msg);
            }
        }

        /// <summary>
        /// Removes the error.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        private void RemoveError(string columnName)
        {
            if (validationErrors.ContainsKey(columnName))
            {
                validationErrors.Remove(columnName);
            }
        }

        #endregion
    }
}
