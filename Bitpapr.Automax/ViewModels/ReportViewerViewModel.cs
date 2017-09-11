using Bitpapr.Automax.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.ViewModels
{
    public class ReportData
    {
        public string ReportLocation { get; set; }
        public string DataSourceName { get; set; }
        public object DataSourceValue { get; set; }
        public Dictionary<string, object> ReportParameters { get; set; }
    }

    public class ReportViewerViewModel : BaseWindowViewModel, INavigationAware
    {
        public ReportData ReportData { get; set; }

        public void OnNavigatedTo(object parameter)
        {
            if (ValidReportArgument(parameter as ReportData))
                ReportData = (ReportData)parameter;
        }

        private bool ValidReportArgument(ReportData argument)
        {
            return !(argument == null) &&
                !(string.IsNullOrEmpty(argument.ReportLocation)) &&
                !(string.IsNullOrEmpty(argument.DataSourceName)) &&
                !(argument.DataSourceValue == null);
        }
    }
}
