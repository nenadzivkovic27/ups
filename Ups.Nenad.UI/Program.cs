using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ups.Nenad.Services;

namespace Ups.Nenad.UI
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);


            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var empForm = serviceProvider.GetRequiredService<MainForm>();
                Application.Run(empForm);
            }

        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging();

            services.AddScoped<IExceptionService, ExceptionService>();
            services.AddScoped<IEmployeeDataService, EmployeeRestService>();
           
            services.AddScoped<MainForm>();

        }
    }
}
