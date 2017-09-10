using Bitpapr.Automax.Core.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.ValueConverters
{
    public class RoleToStringConverter : BaseValueConverter<RoleToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EmployeeRole role)
            {
                switch (role)
                {
                    case EmployeeRole.Regular:
                        return "Funcionário Normal";
                    case EmployeeRole.Manager:
                        return "Gerente";
                    case EmployeeRole.Administrator:
                        return "Administrador";
                }
            }

            return string.Empty;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
