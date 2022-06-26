using System;
using System.Windows;
using CBR_Web_Api.ViewModels;
using CBR_Web_Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace CBR_Web_Api
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    // Настрока сервисов и переопределение методов запуска и остановки
    public partial class App : Application
    {
        public static bool IsDesignTrue { get; private set; } = true;

        private static IHost? host;
        public static IHost Host => host
            ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services = Host.Services;

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
            => services
            .AddServices()
            .AddViewModels()
            ;

        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignTrue = true;
            var host = Host;
            base.OnStartup(e);
            await host.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using var host = Host;
            base.OnExit(e);
            await Host.StopAsync();
        }
    }
}
