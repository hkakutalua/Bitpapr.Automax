using Bitpapr.Automax.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for NewInvoiceWindow.xaml
    /// </summary>
    public partial class NewInvoiceWindow : BaseWindow<NewInvoiceViewModel>
    {
        public NewInvoiceWindow()
        {
            InitializeComponent();

            SizeToContent = SizeToContent.WidthAndHeight;
            this.SizeChanged += (s, e) =>
            {
                MinHeight = e.NewSize.Height;
                MaxHeight = e.NewSize.Height;
            };
        }
    }
}
