using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBR_Web_Api.Services.Intrerfaces
{
    internal interface IUserDialog
    {
        void ConfirmInformation(string information, string caption);
        void ConfirmWarning(string information, string caption);
        void ConfirmError(string information, string caption);
    }
}
