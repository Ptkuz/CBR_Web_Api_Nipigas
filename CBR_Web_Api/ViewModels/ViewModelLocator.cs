using Microsoft.Extensions.DependencyInjection;
using CBR_Web_Api.Services.Intrerfaces;

namespace CBR_Web_Api.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewMiodel MainWindowModel =>
            App.Services.GetRequiredService<MainWindowViewMiodel>();
            
    }
}
