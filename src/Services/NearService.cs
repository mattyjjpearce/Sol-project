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

        //Convert to JSON using NewtonSoft JSON lib
        var json = JsonSerializer.Serialize(rpcPayload); 

        //Format content for sending
        var content = new StringContent(json, Encoding.UTF8, "application/json");    
        
        //Send request to NEAR
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

        //Basically all of this logic can be stripped away into a helper function which can be called here and in method above. Leaving for now as just in prototyping phase. 

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