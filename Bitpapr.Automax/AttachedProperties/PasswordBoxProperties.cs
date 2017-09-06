using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Bitpapr.Automax.AttachedProperties
{
    /// <summary>
    /// Attached properties holder for <see cref="PasswordBox"/>
    /// </summary>
    public class PasswordBoxProperties
    {
        /// <summary>
        /// Attached property used to monitor a PasswordChanged event in a
        /// <see cref="PasswordBox"/>
        /// </summary>
        public static readonly DependencyProperty MonitorPasswordProperty =
            DependencyProperty.RegisterAttached("MonitorPassword",
                typeof(bool),
                typeof(PasswordBoxProperties),
                new UIPropertyMetadata(OnMonitorPasswordChanged));

        public static bool GetMonitorPassword(PasswordBox element) =>
            (bool)element.GetValue(MonitorPasswordProperty);

        public static void SetMonitorPassword(PasswordBox element, bool value) =>
            element.SetValue(MonitorPasswordProperty, value);

        /// <summary>
        /// Attached property changed when <see cref="PasswordBox"/> has text or
        /// loses all text
        /// </summary>
        public static readonly DependencyProperty HasTextProperty =
            DependencyProperty.RegisterAttached("HasText",
                typeof(bool),
                typeof(PasswordBoxProperties));

        public static bool GetHasText(PasswordBox element) =>
            (bool)element.GetValue(HasTextProperty);

        public static void SetHasText(PasswordBox element, bool value) =>
            element.SetValue(HasTextProperty, value);

        /// <summary>
        /// Called when <see cref="MonitorPasswordProperty"/> value is changed
        /// </summary>
        /// <param name="d">The PasswordBox containing the property</param>
        /// <param name="e">Event arguments</param>
        private static void OnMonitorPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                // Remove any previous registered handlers
                passwordBox.PasswordChanged -= UpdateHasTextProperty;

                // Starts listening for PasswordBox password value updates
                // if MonitorPassword property was set to true
                if ((bool)e.NewValue)
                {
                    UpdateHasTextProperty(passwordBox, new RoutedEventArgs());
                    passwordBox.PasswordChanged += UpdateHasTextProperty;
                }
            }
        }

        private static void UpdateHasTextProperty(object sender, RoutedEventArgs args)
        {
            if (sender is PasswordBox passwordBox)
            {
                bool hasText = passwordBox.SecurePassword.Length > 0;
                passwordBox.SetValue(HasTextProperty, hasText);
            }
        }
    }
}
