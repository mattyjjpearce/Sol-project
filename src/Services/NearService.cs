using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class NearService
{
    private readonly HttpClient _httpClient;

    public NearService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


  public async Task<string> GetFtMetadataAsync()
    {
        var rpcPayload = new NearRequest
        {
            jsonrpc = "2.0",
            id = "dontcare",
            method = "query",
            @params = new NearRequestParams
            {
                request_type = "call_function",
                method_name = "ft_metadata",           
                finality = "final", 
                account_id = "absurd-pet.testnet",
                args_base64 = "e30="                
            }
        };

        //Convert to JSON using NewtonSoft JSON lib
        var json = JsonConvert.SerializeObject(rpcPayload); 


        //Format content for sending
        var content = new StringContent(json, Encoding.UTF8, "application/json");    
        
        //Send request to NEAR
        var response = await _httpClient.PostAsync("https://rpc.testnet.near.org", content);
        response.EnsureSuccessStatusCode(); 


        var responseBody = await response.Content.ReadAsStringAsync();

        // Convert response from array of ASCII characters into a string
        var jsonResult = JObject.Parse(responseBody);       
        var resultArray = jsonResult?["result"]?["result"]?.ToObject<byte[]>();
        var resultString = resultArray != null ? Encoding.ASCII.GetString(resultArray) : string.Empty;
        
        return resultString;
    }

    public async Task<string> GetFtBalanceAsync(string id)
    {

        //Basically all of this logic can be stripped away into a helper function which can be called here and in method above. Leaving for now as just in prototyping phase. 

        string base64ConvertedString = Encoder.convertToBase64(id);
       
         var rpcPayload = new NearRequest
        {
            jsonrpc = "2.0",
            id = "dontcare",
            method = "query",
            @params = new NearRequestParams
            {
                request_type = "call_function",
                method_name = "ft_balance_of",           
                finality = "final", 
                account_id = "absurd-pet.testnet",
                args_base64 = base64ConvertedString                
            }
        };

        var json = JsonConvert.SerializeObject(rpcPayload); 
        var content = new StringContent(json, Encoding.UTF8, "application/json");    
        
        //Sent request to NEAR
        var response = await _httpClient.PostAsync("https://rpc.testnet.near.org", content);
  
        response.EnsureSuccessStatusCode(); 

        var responseBody = await response.Content.ReadAsStringAsync();

        var jsonResult = JObject.Parse(responseBody);
         var resultArray = jsonResult?["result"]?["result"]?.ToObject<byte[]>();
        var resultString = resultArray != null ? Encoding.ASCII.GetString(resultArray) : string.Empty;
        
        return resultString;
    }



}