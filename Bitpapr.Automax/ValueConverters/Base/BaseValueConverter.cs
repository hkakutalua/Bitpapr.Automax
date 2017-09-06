using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Bitpapr.Automax.ValueConverters
{
    /// <summary>
    /// Base class for all value converters that can be used from XAML
    /// </summary>
    /// <typeparam name="T">The value converter type</typeparam>
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T : BaseValueConverter<T>, new()
    {
        private static T instance;

        /// <summary>
        /// Called to convert from one value to another
        /// </summary>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Called to convert back from an converted value
        /// </summary>
        /// <returns></returns>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Provide this value converter instance to XAML
        /// </summary>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (instance == null)
                instance = new T();
            return instance;
        }
    }
}
