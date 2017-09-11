using Bitpapr.Automax.Commands;
using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Navigation;
using Bitpapr.Automax.Reports.ParamsMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bitpapr.Automax.ViewModels
{
    public class VisualizeInvoiceViewModel : BaseWindowViewModel, INavigationAware
    {
        private readonly IInvoiceToReportParamsMapper _reportParamsMapper;
        private readonly INavigationService _navigationService;

        public Invoice Invoice { get; private set; }

        public ICommand ReprintCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        public VisualizeInvoiceViewModel(IInvoiceToReportParamsMapper invoiceToReportParamsMapper,
            INavigationService navigationService)
        {
            _reportParamsMapper = invoiceToReportParamsMapper;
            _navigationService = navigationService;

            ReprintCommand = new RelayCommand(ExecuteReprint);
            ExitCommand = new RelayCommand(ExecuteExit);
        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is Invoice invoice)
                Invoice = invoice;
        }

        private void ExecuteReprint()
        {
            var invoiceReportParams = _reportParamsMapper.Map(Invoice);

            var reportData = new ReportData
            {
                ReportLocation = "/Reports/InvoiceReport.rdlc",
                DataSourceName = "ServicesToProvide",
                DataSourceValue = Invoice.ServicesToProvide,
                ReportParameters = invoiceReportParams
            };

            _navigationService.ShowWindowAsModal(WindowType.ReportViewerWindow, reportData);
        }

        private void ExecuteExit() =>
            base.OnWindowCloseRequested();
    }
}
