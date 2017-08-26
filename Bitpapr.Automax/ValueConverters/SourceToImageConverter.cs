﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Bitpapr.Automax.ValueConverters
{
    /// <summary>
    /// Value converter that converts a image source string to
    /// a BitmapImage that can be used in Image associated controls
    /// </summary>
    public class SourceToImageConverter : MarkupExtension, IValueConverter
    {
        private static SourceToImageConverter instance;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new BitmapImage();

            var sourceUri = new Uri((string)value, UriKind.Relative);
            return new BitmapImage(sourceUri);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (instance == null)
                instance = new SourceToImageConverter();
            return instance;
        }
    }
}
