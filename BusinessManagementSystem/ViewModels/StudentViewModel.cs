using BusinessManagementSystem.Data;
using BusinessManagementSystem.HelperClasses;
using BusinessManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementSystem.ViewModels
{
    class StudentViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private ICommand _addStudentPopupCommand;
        private ICommand _editStudentPopupCommand;
        private ICommand _closeStudentPopupCommand;
        private ICommand _saveStudentCommand;

        private bool _isStudentPopupOpen;
        private StudentModel _currentStudent;
        private List<StudentModel> _studentCollection;

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
                return "Students";
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

        /// <summary>
        /// Gets or sets a value indicating whether this instance is student popup open.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is student popup open; otherwise, <c>false</c>.
        /// </value>
        public bool IsStudentPopupOpen
        {
            get
            {
                return _isStudentPopupOpen;
            }
            set
            {
                if (_isStudentPopupOpen != value)
                {
                    _isStudentPopupOpen = value;
                    OnPropertyChanged("IsStudentPopupOpen");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [current student].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [current student]; otherwise, <c>false</c>.
        /// </value>
        public StudentModel CurrentStudent
        {
            get
            {
                return _currentStudent;
            }
            set
            {
                if (_currentStudent != value)
                {
                    _currentStudent = value;
                    OnPropertyChanged("CurrentStudent");
                }
            }
        }

        /// <summary>
        /// Gets or sets the student collection.
        /// </summary>
        /// <value>
        /// The student collection.
        /// </value>
        public List<StudentModel> StudentCollection
        {
            get
            {
                return _studentCollection;
            }
            set
            {
                if (_studentCollection != value)
                {
                    _studentCollection = value;
                    OnPropertyChanged("StudentCollection");
                }
            }
        }

        #endregion

        #region Constructor

        public StudentViewModel()
        {
            StudentCollection = GetStudents();
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the add student popup command.
        /// </summary>
        /// <value>
        /// The add student popup command.
        /// </value>
        public ICommand AddStudentPopupCommand
        {
            get
            {
                if (_addStudentPopupCommand == null)
                {
                    _addStudentPopupCommand = new RelayCommand(
                        p => AddStudentPopup());
                }

                return _addStudentPopupCommand;
            }
        }

        /// <summary>
        /// Gets the edit student popup command.
        /// </summary>
        /// <value>
        /// The edit student popup command.
        /// </value>
        public ICommand EditStudentPopupCommand
        {
            get
            {
                if (_editStudentPopupCommand == null)
                {
                    _editStudentPopupCommand = new RelayCommand(
                        p => EditStudentPopup((StudentModel)p),
                        p => p is StudentModel);
                }

                return _editStudentPopupCommand;
            }
        }

        /// <summary>
        /// Gets the close student popup command.
        /// </summary>
        /// <value>
        /// The close student popup command.
        /// </value>
        public ICommand CloseStudentPopupCommand
        {
            get
            {
                if (_closeStudentPopupCommand == null)
                {
                    _closeStudentPopupCommand = new RelayCommand(
                        p => CloseStudentPopup());
                }

                return _closeStudentPopupCommand;
            }
        }

        public ICommand SaveStudentCommand
        {
            get
            {
                if (_saveStudentCommand == null)
                {
                    _saveStudentCommand = new RelayCommand(
                        p => SaveStudent());
                }

                return _saveStudentCommand;
            }
        }

        #endregion

        /// <summary>
        /// Adds the student popup.
        /// </summary>
        private void AddStudentPopup()
        {
            CurrentStudent = new StudentModel()
            {
                BirthDate = DateTime.Now
            };

            OpenStudentPopup();
        }

        /// <summary>
        /// Edits the student popup.
        /// </summary>
        /// <param name="student">The student.</param>
        private void EditStudentPopup(StudentModel student)
        {
            CurrentStudent = new StudentModel()
            {
                Id=student.Id,
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                LastName = student.LastName,
                Phone = student.Phone,
                BirthDate = student.BirthDate,
                Gender = student.Gender
            };

            OpenStudentPopup();
        }

        /// <summary>
        /// Saves the student.
        /// </summary>
        private void SaveStudent()
        {
            if (!CurrentStudent.HasErrors)
            {
                using (BusinessManagementSystemEntities entities = new BusinessManagementSystemEntities())
                {
                    Student student;

                    if (CurrentStudent.Id == 0)
                    {
                        student = new Student();
                    }
                    else
                    {
                        student = entities.Students.SingleOrDefault(s => s.Id == CurrentStudent.Id);
                    }

                    if (student != null)
                    {
                        student.FirstName = CurrentStudent.FirstName;
                        student.MiddleName = CurrentStudent.MiddleName;
                        student.LastName = CurrentStudent.LastName;
                        student.Gender = CurrentStudent.Gender;
                        student.BirthDate = CurrentStudent.BirthDate;
                        student.Phone = CurrentStudent.Phone;

                        if (CurrentStudent.Id == 0)
                        {
                            entities.Students.Add(student);
                        }

                        entities.SaveChanges();

                        CloseStudentPopup();

                        if (CurrentStudent.Id == 0)
                        {
                            CurrentStudent.Id = student.Id;
                            StudentCollection.Add(CurrentStudent);
                            StudentCollection = StudentCollection.ToList();
                        }
                        else
                        {
                            StudentCollection.Where(w => w.Id == CurrentStudent.Id).ToList().ForEach(s =>
                            {
                                s.FirstName = CurrentStudent.FirstName;
                                s.MiddleName = CurrentStudent.MiddleName;
                                s.LastName = CurrentStudent.LastName;
                                s.Phone = CurrentStudent.Phone;
                                s.BirthDate = CurrentStudent.BirthDate;
                                s.Gender = CurrentStudent.Gender;
                            });
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the students.
        /// </summary>
        /// <returns></returns>
        private List<StudentModel> GetStudents()
        {
            using (BusinessManagementSystemEntities entities = new BusinessManagementSystemEntities())
            {
                return (from student in entities.Students
                        select new StudentModel
                        {
                            Id = student.Id,
                            FirstName = student.FirstName,
                            MiddleName = student.MiddleName,
                            LastName = student.LastName,
                            BirthDate = student.BirthDate,
                            Gender = student.Gender,
                            Phone = student.Phone
                        }).ToList();
            }
        }
        
        /// <summary>
        /// Opens the student popup.
        /// </summary>
        private void OpenStudentPopup()
        {
            IsStudentPopupOpen = true;
        }

        /// <summary>
        /// Closes the student popup.
        /// </summary>
        private void CloseStudentPopup()
        {
            IsStudentPopupOpen = false;
        }
    }
}
