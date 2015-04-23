using System;
using System.Windows;
using System.Windows.Data;

namespace BusinessManagementSystem.HelperClasses
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        #region Constructors

        /// <summary>
        /// The default constructor
        /// </summary>
        public BooleanToVisibilityConverter() { }

        #endregion

        #region Properties

        public bool Collapse { get; set; }
        public bool Reverse { get; set; }

        #endregion

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool bValue = (bool)value;

            if (bValue != Reverse)
            {
                return Visibility.Visible;
            }
            else
            {
                if (Collapse)
                    return Visibility.Collapsed;
                else
                    return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;

            if (visibility == Visibility.Visible)
                return !Reverse;
            else
                return Reverse;
        }

        #endregion
    }
}
