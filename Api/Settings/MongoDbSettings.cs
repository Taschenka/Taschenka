namespace Taschenka.Settings;

public class MongoDbSettings
{
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string User { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConnectionString
    {
        get
        {
            return $"mongodb://{User}:{Password}@{Host}:{Port}";
        }
    }
}
