namespace Versteigerungs_App.Models;

public interface IDatabaseSettings
{
    /// <summary>Connection string to database.</summary>
    string? ConnectionString { get; set; }

    /// <summary>Name of the database.</summary>
    string? DatabaseName { get; set; }
    string? CollectionName { get; set; }

}

public class DatabaseSettings : IDatabaseSettings
{
    public string? ConnectionString { get; set; }

    public string? DatabaseName { get; set; }
    public string? CollectionName { get; set; }
}