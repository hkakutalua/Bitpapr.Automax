using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bitpapr.Automax.Navigation
{
    public class DialogService : IDialogService
    {
        // TODO: use a custom MessageBox that supports detail message
        public void ShowDetailedDialog(string titleMessage,string contentMessage,
            string detailMessage, DialogType dialogType)
        {
            MessageBox.Show(App.Current.MainWindow, contentMessage + "\n\n" + detailMessage,
                titleMessage, MessageBoxButton.OK);
        }
    }
}
