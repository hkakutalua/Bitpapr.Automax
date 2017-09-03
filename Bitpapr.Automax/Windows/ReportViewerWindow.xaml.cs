using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.ViewModels;
using Syncfusion.Windows.Reports;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bitpapr.Automax
{
    /// <summary>
    /// Interaction logic for ReportViewerWindow.xaml
    /// </summary>
    public partial class ReportViewerWindow : BaseWindow<ReportViewerViewModel>
    {
        public ReportViewerWindow()
        {
            InitializeComponent();
        }
    }
}
