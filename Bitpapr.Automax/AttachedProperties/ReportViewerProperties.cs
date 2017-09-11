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
        /// <summary>
        /// ReportSource attached property for defining the uri of the report
        /// used by the <see cref="ReportViewer"/>
        /// </summary>
        public static readonly DependencyProperty ReportSourceProperty =
            DependencyProperty.RegisterAttached("ReportSource",
                typeof(string),
                typeof(ReportViewerProperties),
                new UIPropertyMetadata(OnReportSourceChanged));

        public static string GetReportSource(ReportViewer element) =>
            (string)element.GetValue(ReportSourceProperty);

        public static void SetReportSource(ReportViewer element, string value) =>
            element.SetValue(ReportSourceProperty, value);

        /// <summary>
        /// DataSourceName attached property to define the name of the data source
        /// used by the <see cref="ReportViewer"/>
        /// </summary>
        public static readonly DependencyProperty DataSourceNameProperty =
            DependencyProperty.RegisterAttached("DataSourceName",
                typeof(string),
                typeof(ReportViewerProperties));

        public static string GetDataSourceName(ReportViewer element) =>
            (string)element.GetValue(DataSourceNameProperty);

        public static void SetDataSourceName(ReportViewer element, string value) =>
            element.SetValue(DataSourceNameProperty, value);
        
        /// <summary>
        /// Attached property holding the data source value
        /// </summary>
        public static readonly DependencyProperty DataSourceValueProperty =
            DependencyProperty.RegisterAttached("DataSourceValue",
                typeof(object),
                typeof(ReportViewerProperties),
                new UIPropertyMetadata(OnDataSourceValueChanged));

        public static object GetDataSourceValue(ReportViewer element) =>
            element.GetValue(DataSourceValueProperty);

        public static void SetDataSourceValue(ReportViewer element, object value) =>
            element.SetValue(DataSourceValueProperty, value);

        /// <summary>
        /// Attached property containg the parameters passed to the report
        /// </summary>
        public static readonly DependencyProperty ReportParametersProperty =
            DependencyProperty.RegisterAttached("ReportParameters",
                typeof(Dictionary<string, object>),
                typeof(ReportViewerProperties),
                new UIPropertyMetadata(OnReportParametersChanged));

        public static object GetReportParameters(ReportViewer element) =>
            element.GetValue(ReportParametersProperty);

        public static void SetReportParameters(ReportViewer element, Dictionary<string, object> value) =>
            element.SetValue(ReportParametersProperty, value);

        /// <summary>
        /// Called when report source file is changed, the new report is loaded
        /// as an embedded resource
        /// </summary>
        /// <param name="element">The <see cref="ReportViewer"/></param>
        /// <param name="e">Event arguments</param>
        private static void OnReportSourceChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            if (element is ReportViewer viewer)
            {
                // Load an report stored as an embedded into a stream
                Uri reportUri = new Uri((string)e.NewValue, UriKind.Relative);
                var streamInfo = Application.GetResourceStream(reportUri);

                // Load the viewer with the report
                viewer.LoadReport(streamInfo.Stream);
                viewer.RefreshReport();
            }
        }

        /// <summary>
        /// Called when the report data source is changed
        /// </summary>
        /// <param name="element">The <see cref="ReportViewer"/></param>
        /// <param name="e">Event arguments</param>
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

        /// <summary>
        /// Called when the report parameters are changed.
        /// The parameters are contained in a Dictionary, then this method
        /// create a collection of <see cref="ReportParameter"/>s mapped from
        /// the dictionary and load the report with them.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="e"></param>
        private static void OnReportParametersChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            if (element is ReportViewer viewer)
            {
                var reportParameters = e.NewValue as Dictionary<string, object>;
                
                // Convert parameters dictionary collection into a List
                // of ReportViewer report parameters and load the viewer with them
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
