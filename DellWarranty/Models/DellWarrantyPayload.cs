using System.Text.Json.Serialization;

namespace DellWarranty.Models;
public partial class DellWarrantyPayload
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("serviceTag")]
    public string ServiceTag { get; set; }

    [JsonPropertyName("orderBuid")]
    public long OrderBuid { get; set; }

    [JsonPropertyName("shipDate")]
    public DateTimeOffset ShipDate { get; set; }

    [JsonPropertyName("productCode")]
    public string ProductCode { get; set; }

    [JsonPropertyName("localChannel")]
    public string LocalChannel { get; set; }

    [JsonPropertyName("productId")]
    public string ProductId { get; set; }

    [JsonPropertyName("productLineDescription")]
    public string ProductLineDescription { get; set; }

    [JsonPropertyName("productFamily")]
    public string ProductFamily { get; set; }

    [JsonPropertyName("systemDescription")]
    public string SystemDescription { get; set; }

    [JsonPropertyName("productLobDescription")]
    public string ProductLobDescription { get; set; }

    [JsonPropertyName("countryCode")]
    public string CountryCode { get; set; }

    [JsonPropertyName("duplicated")]
    public bool Duplicated { get; set; }

    [JsonPropertyName("invalid")]
    public bool Invalid { get; set; }

    [JsonPropertyName("entitlements")]
    public List<Entitlement> Entitlements { get; set; }
}

public partial class Entitlement
{
    [JsonPropertyName("itemNumber")]
    public string ItemNumber { get; set; }

    [JsonPropertyName("startDate")]
    public DateTimeOffset StartDate { get; set; }

    [JsonPropertyName("endDate")]
    public DateTimeOffset EndDate { get; set; }

    [JsonPropertyName("entitlementType")]
    public string EntitlementType { get; set; }

    [JsonPropertyName("serviceLevelCode")]
    public string ServiceLevelCode { get; set; }

    [JsonPropertyName("serviceLevelDescription")]
    public string ServiceLevelDescription { get; set; }

    [JsonPropertyName("serviceLevelGroup")]
    public long ServiceLevelGroup { get; set; }
}

