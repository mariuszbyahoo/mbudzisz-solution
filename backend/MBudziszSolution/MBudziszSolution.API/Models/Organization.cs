using System.Text.Json.Serialization;

namespace MBudziszSolution.Models;

public class Organization
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public required string Industry { get; set; }
    public required string Tier { get; set; }
    public required string ContactEmail { get; set; }
    public required string CreatedAt { get; set; }
    public required string Description { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Notes { get; set; }

    public required OrganizationSettings Settings { get; set; }
    public required OrganizationMetadata Metadata { get; set; }
}

public class OrganizationSettings
{
    public required string Timezone { get; set; }
    public required string Currency { get; set; }
    public required bool AllowOvertime { get; set; }
    public required string DefaultLocale { get; set; }
}

public class OrganizationMetadata
{
    public required string Source { get; set; }
    public int? LegacyId { get; set; }
    public string? MigratedAt { get; set; }
}
