using Microsoft.Extensions.Configuration;

namespace MyCore.Common.ConfigHelper
{
    public static class ConfigurationHelper
    {
        private static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
        public static T GetConfig<T>(string sectionName) where T : class
        {
            var settings = GetConfig();
            if (settings.GetSection(sectionName).Exists())
                return settings.GetSection(sectionName).Get<T>();
            return null;
        }
        public static List<T> GetConfigList<T>(string sectionName) where T : class
        {
            var settings = GetConfig();
            if (settings.GetSection(sectionName).Exists())
                return settings.GetSection(sectionName).Get<List<T>>();
            return null;
        }

        public static string GetValue(string keyName)
        {
            var settings = GetConfig();
            return settings.GetValue<string>(keyName) == null
                ? ""
                : settings.GetValue<string>(keyName);
        }
    }
}
