using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
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
            var appSettingRoot = $"{Directory.GetCurrentDirectory()}/../HeroesCup/";
            var configurationBuilder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(commandLineArgs)
                .SetBasePath(appSettingRoot)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return configurationBuilder.Build();
        }
    }
}
