using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bitpapr.Automax
{
    /// <summary>
    /// A base abstract class for classes that contain an attached property
    /// </summary>
    /// <typeparam name="TParent">The type of the owner class</typeparam>
    /// <typeparam name="TProperty">The type of the attached property</typeparam>
    public abstract class BaseAttachedProperty<TParent, TProperty>
        where TParent : BaseAttachedProperty<TParent, TProperty>
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
            "Value",
            typeof(TProperty),
            typeof(TParent),
            new UIPropertyMetadata(
                default(TProperty),
                null
                ));

        /// <summary>
        /// Get's the attached property
        /// </summary>
        /// <param name="d">The object to get the property from</param>
        /// <returns></returns>
        public static TProperty GetValue(DependencyObject d) => (TProperty)d.GetValue(ValueProperty);

        /// <summary>
        /// Set's the attached property
        /// </summary>
        /// <param name="d">The object to set the property to</param>
        /// <param name="value">The value of the property</param>
        public static void SetValue(DependencyObject d, TProperty value) => d.SetValue(ValueProperty, value);
    }
}
