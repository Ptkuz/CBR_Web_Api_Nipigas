using Microsoft.Extensions.DependencyInjection;

namespace CBR_Web_Api.ViewModels
{
    internal static class ViewModelRegistrator
    {
        // Регистрируем ViewModel через внедрение зависимостей
        internal static IServiceCollection AddViewModels(this IServiceCollection services)
            => services
            .AddTransient<MainWindowViewMiodel>()
            ;
    }
}
