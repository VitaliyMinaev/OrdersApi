using Newtonsoft.Json;

namespace OrdersApi.Common.Logging;

public record LogEntry
{
    public string Class { get; init; } = string.Empty;
    public string Method { get; init; } = string.Empty;
    public string Comment { get; init; } = string.Empty;
    public string? Operation { get; init; }
    public string Parameters { get; init; } = string.Empty;
    
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}