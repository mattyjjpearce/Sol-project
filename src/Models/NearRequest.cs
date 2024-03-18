using System.Text.Json.Serialization;

public class NearRequest
{
 
        public NearRequest(string jsonrpc, string id, string method)
        {
            Jsonrpc = jsonrpc;
            Id = id;
            Method = method;
        }

        [JsonPropertyName("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("method")]
        public string Method { get; set; }

        [JsonPropertyName("params")]
        public NearRequestParams Params { get; set; }
    
}

public class NearRequestParams
{
    public NearRequestParams(string requestType, string methodName, string finality, string accountId, string argsBase64)
    {
        RequestType = requestType;
        MethodName = methodName;
        Finality = finality;
        AccountId = accountId;
        ArgsBase64 = argsBase64;
    }
    [JsonPropertyName("request_type")] public string RequestType { get; set; }

    [JsonPropertyName("method_name")]
    public string MethodName { get; set; }

    [JsonPropertyName("finality")]
    public string Finality { get; set; }

    [JsonPropertyName("account_id")]
    public string AccountId { get; set; }

    [JsonPropertyName("args_base64")]
    public string ArgsBase64 { get; set; }
}

