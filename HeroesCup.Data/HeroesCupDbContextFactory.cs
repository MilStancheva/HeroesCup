using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.IO;

namespace HeroesCup.Data
{
    public class HeroesCupDbContextFactory : IDesignTimeDbContextFactory<HeroesCupDbContext>
    {
        public HeroesCupDbContext CreateDbContext(string[] args)
        {
            var heroesCupConfiguration = BuildConfiguration(args);
            var connectionString = heroesCupConfiguration.GetSection("HEROESCUP_CONNECTIONSTRING").Value;
            var optionBuilder = new DbContextOptionsBuilder<HeroesCupDbContext>();
            optionBuilder.UseMySql(connectionString);

            return new HeroesCupDbContext(optionBuilder.Options);
        }

        protected virtual IConfigurationRoot BuildConfiguration(string[] commandLineArgs)
        {
#if DEBUG
            var appSettingRoot = $"{Directory.GetCurrentDirectory()}/../HeroesCup.Web/";
#else
            var appSettingRoot = $"{Directory.GetCurrentDirectory()}";
#endif

            var configurationBuilder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(commandLineArgs)
                .SetBasePath(appSettingRoot)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return configurationBuilder.Build();
        }
    }
}