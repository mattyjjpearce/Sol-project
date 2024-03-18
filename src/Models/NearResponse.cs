using System.Text.Json.Serialization;
public class NearResponse
{
    [JsonPropertyName("jsonrpc")] public string jsonrpc {get; set;} 

    [JsonPropertyName("result")] public NearResponseResult result {get; set;}
}

public class NearResponseResult
{
    [JsonPropertyName("block_hash")] public string block_hash {get; set;}

    [JsonPropertyName("block_height")] public string block_height {get; set;}

    [JsonPropertyName("logs")] public List<string> logs {get; set;}

    [JsonPropertyName("result")] public List<int> result {get; set;}

    [JsonPropertyName("id")] public string id {get; set;}

}
