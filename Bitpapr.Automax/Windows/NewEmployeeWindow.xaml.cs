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
using System.Security;

namespace Bitpapr.Automax
{
    /// <summary>
    /// Interaction logic for NewEmployeeWindow.xaml
    /// </summary>
    public partial class NewEmployeeWindow : BaseWindow<NewEmployeeViewModel>, IHavePassword
    {
        public NewEmployeeWindow()
        {
            InitializeComponent();
        }

        public SecureString SecurePassword => PasswordBox.SecurePassword;
    }
}
