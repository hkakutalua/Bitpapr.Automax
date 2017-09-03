using Syncfusion.Windows.Reports;
using Syncfusion.Windows.Reports.Viewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bitpapr.Automax.AttachedProperties
{
    /// <summary>
    /// Custom properties for Syncfusion <see cref="ReportViewer"/>
    /// </summary>
    public class ReportViewerProperties
    {
        #region ReportSource Property

        public static readonly DependencyProperty ReportSourceProperty =
            DependencyProperty.RegisterAttached("ReportSource",
                typeof(string),
                typeof(ReportViewerProperties),
                new UIPropertyMetadata(OnReportSourceChanged));

        public static string GetReportSource(ReportViewer element) =>
            (string)element.GetValue(ReportSourceProperty);

        public static void SetReportSource(ReportViewer element, string value) =>
            element.SetValue(ReportSourceProperty, value);

        #endregion

        #region DataSourceName Property

        public static readonly DependencyProperty DataSourceNameProperty =
            DependencyProperty.RegisterAttached("DataSourceName",
                typeof(string),
                typeof(ReportViewerProperties));

        public static string GetDataSourceName(ReportViewer element) =>
            (string)element.GetValue(DataSourceNameProperty);

        public static void SetDataSourceName(ReportViewer element, string value) =>
            element.SetValue(DataSourceNameProperty, value);

        #endregion

        #region DataSourceValue Property

        public static readonly DependencyProperty DataSourceValueProperty =
            DependencyProperty.RegisterAttached("DataSourceValue",
                typeof(object),
                typeof(ReportViewerProperties),
                new UIPropertyMetadata(OnDataSourceValueChanged));

        public static object GetDataSourceValue(ReportViewer element) =>
            element.GetValue(DataSourceValueProperty);

        public static void SetDataSourceValue(ReportViewer element, object value) =>
            element.SetValue(DataSourceValueProperty, value);

        #endregion

        #region ReportParameters Property

        public static readonly DependencyProperty ReportParametersProperty =
            DependencyProperty.RegisterAttached("ReportParameters",
                typeof(Dictionary<string, object>),
                typeof(ReportViewerProperties),
                new UIPropertyMetadata(OnReportParametersChanged));

        public static object GetReportParameters(ReportViewer element) =>
            element.GetValue(ReportParametersProperty);

        public static void SetReportParameters(ReportViewer element, Dictionary<string, object> value) =>
            element.SetValue(ReportParametersProperty, value);

        #endregion

        private static void OnReportSourceChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            if (element is ReportViewer viewer)
            {
                Uri reportUri = new Uri((string)e.NewValue, UriKind.Relative);
                var streamInfo = Application.GetResourceStream(reportUri);

                viewer.LoadReport(streamInfo.Stream);
                viewer.RefreshReport();
            }
        }

        private static void OnDataSourceValueChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            if (element is ReportViewer viewer)
            {
                string sourceName = (string)viewer.GetValue(DataSourceNameProperty);
                object sourceValue = e.NewValue;
                var dataSource = new ReportDataSource(sourceName, sourceValue);

                viewer.DataSources.Add(dataSource);
            }
        }

        private static void OnReportParametersChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            if (element is ReportViewer viewer)
            {
                var reportParameters = e.NewValue as Dictionary<string, object>;
                viewer.SetParameters(
                    reportParameters.Select(kvPair =>
                    new ReportParameter
                    {
                        Name = kvPair.Key,
                        Values = new List<string> { kvPair.Value.ToString() }
                    })
                );
            }
        }
    }
}
