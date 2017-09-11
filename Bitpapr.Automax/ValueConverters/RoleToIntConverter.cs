using Bitpapr.Automax.Core.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.ValueConverters
{
    public class RoleToIntConverter : BaseValueConverter<RoleToIntConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EmployeeRole role)
            {
                switch (role)
                {
                    case EmployeeRole.Regular:
                        return 0;
                    case EmployeeRole.Manager:
                        return 1;
                }
            }

            return 0;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int roleNumber)
            {
                switch (roleNumber)
                {
                    case 0:
                        return EmployeeRole.Regular;
                    case 1:
                        return EmployeeRole.Manager;
                }
            }

            return 0;
        }
    }
}
