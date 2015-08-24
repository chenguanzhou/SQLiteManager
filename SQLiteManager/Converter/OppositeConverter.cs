using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace SQLiteManager.Converter
{
    class OppositeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return opposite(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return opposite(value);
        }

        private object opposite(object obj)
        {
            if (obj.GetType() == 1.GetType())
                return -(double)obj;

            if (obj.GetType() == 1.0.GetType())
                return -(double)obj;
            
            try
            {
                double val;
                if (Double.TryParse(obj.ToString(),out val) == false)
                    throw new Exception();
                return -val;
            }
            catch (Exception)
            {
                return obj;
            }
        }
    }
}
