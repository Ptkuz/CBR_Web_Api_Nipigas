using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CBR_Web_Api.Services.Intrerfaces;

namespace CBR_Web_Api.Services
{
    // Реализация диалоговых окон
    internal class UserDialog : IUserDialog
    {
        public void ConfirmInformation(string information, string caption) =>
           MessageBox.Show(information, caption, MessageBoxButton.OK, MessageBoxImage.Information);

        public void ConfirmWarning(string information, string caption) =>
            MessageBox.Show(information, caption, MessageBoxButton.OK, MessageBoxImage.Warning);

        public void ConfirmError(string information, string caption) =>
            MessageBox.Show(information, caption, MessageBoxButton.OK, MessageBoxImage.Error);
    }

}
