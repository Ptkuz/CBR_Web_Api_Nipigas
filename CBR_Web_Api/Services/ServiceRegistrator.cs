using Microsoft.Extensions.DependencyInjection;
using CBR_Web_Api.Services.Intrerfaces;

namespace CBR_Web_Api.Services
{
    internal static class ServiceRegistrator
    {

        // Регистрация сервисов
        public static IServiceCollection AddServices(this IServiceCollection services)
            => services
            .AddTransient<IGetXML, GetXML>()
            .AddTransient<IUserDialog, UserDialog>()
            ;
    }
}
