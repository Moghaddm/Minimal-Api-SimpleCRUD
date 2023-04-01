namespace Settings;

public class HotelDatabaseSettings
{
    public string ConnectionString { get; set; } = default!;
    public string DatabaseName { get; set; } = default!;
    public string CustomerCollectionName { get; set; } = default!;
}
