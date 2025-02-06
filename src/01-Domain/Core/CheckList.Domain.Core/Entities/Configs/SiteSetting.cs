namespace CheckList.Domain.Core.Entities.Configs;

public class SiteSetting
{
    public ConnectionString ConnectionString { get; set; } = null!;
    public int Limitation { get; set; }
}
