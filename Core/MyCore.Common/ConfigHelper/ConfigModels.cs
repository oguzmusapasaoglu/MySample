namespace MyCore.Common.ConfigHelper
{
    public class MySampleSettingsConfigModel
    {
        public const string SectionName = "MySampleSettings";
        public string ConnectionString { get; set; }
        public string LogDbConnectionString { get; set; }
        public string ServicesURL { get; set; }
        public string ServicesURLHTTPS { get; set; }
    }
    public class MySampleSettingsConfigModelHelper
    {
        public static MySampleSettingsConfigModel GetSettings()
        {
            var settings = ConfigurationHelper.GetConfig<MySampleSettingsConfigModel>(MySampleSettingsConfigModel.SectionName);
            return settings;
        }
        public static string GetConnection()
        {
            var settings = GetSettings();
            return settings.ConnectionString;
        }
        public static string GetLogDbConnection()
        {
            var settings = GetSettings();
            return settings.LogDbConnectionString;
        }
        public static string GetServicesURL(bool isHTTP = true)
        {
            var settings = GetSettings();
            return (isHTTP) ? settings.ServicesURL : settings.ServicesURLHTTPS;
        }
    }
}
