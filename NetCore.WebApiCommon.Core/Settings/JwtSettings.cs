namespace NetCore.WebApiCommon.Core.Settings;

public class JwtSettings
{
    public string Issuer { get; init; } = default!;
    public string Audience { get; init; } = default!;
    public string SigningKey { get; init; } = default!;
    public bool ValidateAudience { get; init; }
    public bool ValidateIssuer { get; init; }
    public bool ValidateIssuerSigningKey { get; init; }
    public bool ValidateLifetime { get; init; }
}