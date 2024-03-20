using System.Text;
using System.Text.Json;


public class NearService
{
    private readonly HttpClient _httpClient;

    public NearService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

  public async Task<string> GetFtMetadataAsync()
    {
        var rpcPayload = new NearRequest(){
            @Params = new NearRequestParams(
                requestType: "call_function", methodName: "ft_metadata",
                finality: "final",
                argsBase64: "e30="
            )
        };
        
        var json = JsonSerializer.Serialize(rpcPayload); 
        var content = new StringContent(json, Encoding.UTF8, "application/json");  

        
        try
        {
            var response = await _httpClient.PostAsync("https://rpc.testnet.near.org", content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            return Formatter.FormatResponseData(responseBody);          
        } 
        catch(Exception e)
        {
            return e.Message;
        }
       
    }

    public async Task<string> GetFtBalanceAsync(string id)
    {
        string base64ConvertedString = Encoder.convertToBase64(id);
       
        var rpcPayload = new NearRequest()
        {
            @Params = new NearRequestParams(
                requestType: "call_function",
                methodName: "ft_balance_of",
                finality: "final", 
                argsBase64: base64ConvertedString              
            )
        };

        var json = JsonSerializer.Serialize(rpcPayload);  
        var content = new StringContent(json, Encoding.UTF8, "application/json");    
        
        try
        {
            var response = await _httpClient.PostAsync("https://rpc.testnet.near.org", content);
            response.EnsureSuccessStatusCode(); 

            var responseBody = await response.Content.ReadAsStringAsync();
            return Formatter.FormatResponseData(responseBody); 

        }
        catch(Exception e)
        {
            return e.Message;
        }
   
    }

}