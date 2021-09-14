﻿using System.Text.Json.Serialization;

namespace DellWarranty.Models;
public class DellOAuthPayload
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    [JsonPropertyName("expires_in")]
    public long ExpiresIn { get; set; }

    [JsonPropertyName("scope")]
    public string Scope { get; set; }
}
