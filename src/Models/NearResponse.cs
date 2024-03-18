using System.Text.Json.Serialization;
public class NearResponse
{
    public NearResponse(
        string jsonrpc, NearResponseResult result
    ){
        Jsonrpc = jsonrpc;
        Result = result;
    }
    [JsonPropertyName("jsonrpc")] public string Jsonrpc {get; set;} 

    [JsonPropertyName("result")] public NearResponseResult Result {get; set;}
}

public class NearResponseResult
{
    public NearResponseResult(string blockhash, int blockheight, List<string> logs, List<int> result, string id){
        BlockHash = blockhash;
        BlockHeight = blockheight;
        Logs = logs;
        Result = result;
        Id = id;
    }
    [JsonPropertyName("block_hash")] public string BlockHash {get; set;}

    [JsonPropertyName("block_height")] public int BlockHeight {get; set;}

    [JsonPropertyName("logs")] public List<string> Logs {get; set;}

    [JsonPropertyName("result")] public List<int> Result {get; set;}

    [JsonPropertyName("id")] public string Id {get; set;}

}
