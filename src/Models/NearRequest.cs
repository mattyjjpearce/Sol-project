public class NearRequest
{
    public string jsonrpc { get; set; }
    public string id { get; set; }
    public string method { get; set; }
    public NearRequestParams @params { get; set; }
}

public class NearRequestParams
{
    public string request_type { get; set; }
    public string method_name { get; set; }
    public string finality { get; set; }
    public string account_id { get; set; }
    public string args_base64 { get; set; }
}

