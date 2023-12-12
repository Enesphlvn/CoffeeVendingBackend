namespace Entities.Concrete
{
    public class AppSettingsValue
    {
        public ConnectionString ConnectionStrings { get; set; }
    }

    public class ConnectionString {

        public string PostgreSQL { get; set; }

    }
}
