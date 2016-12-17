using DAL;

namespace Services
{
    public static class Config
    {
        public static DefaultsConfig Defaults { get; set; }
        public static DatabaseConfig Database { get; set; }

        static Config()
        {
            Defaults = new DefaultsConfig();
            Database = new DatabaseConfig();
        }
    }

    public class DefaultsConfig
    {
        public string CountryCode { get; set; }
        public string TimeZoneName { get; set; }
        public string TimeZoneCode { get; set; }
    }
    public class DatabaseConfig
    {
        public string ConnectionString
        {
            get { return DB.DatabaseConnectionString; }
            set { DB.DatabaseConnectionString = value; }
        }
    }
}