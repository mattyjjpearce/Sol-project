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
        var rpcPayload = new
        {
            jsonrpc = "2.0",
            id = "dontcare",
            method = "query",
            @params = new
            {
                request_type = "call_function",
                method_name = "ft_metadata",           
                finality = "final", 
                account_id = "absurd-pet.testnet",
                args_base64 = "e30="
                
            }
        };

        //Convert to JSON
        var json = JsonConvert.SerializeObject(rpcPayload); 
        var content = new StringContent(json, Encoding.UTF8, "application/json");    
        
        //Sent request to NEAR
        var response = await _httpClient.PostAsync("https://rpc.testnet.near.org", content);
        response.EnsureSuccessStatusCode(); 

        var responseBody = await response.Content.ReadAsStringAsync();

        //Convert response from array of ASCII characters into a string

        var jsonResult = JObject.Parse(responseBody);
        var resultArray = jsonResult["result"]["result"].ToObject<byte[]>();
        var resultString = Encoding.ASCII.GetString(resultArray);
        
        return resultString;
    }

    public async Task<string> GetFtBalanceAsync(string id)
    {

        string base64ConvertedString = Encoder.convertToBase64(id);
        Console.WriteLine(base64ConvertedString);
         var rpcPayload = new
        {
            jsonrpc = "2.0",
            id = "dontcare",
            method = "query",
            @params = new
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
        Console.WriteLine(response);
        response.EnsureSuccessStatusCode(); 

        var responseBody = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseBody);


        var jsonResult = JObject.Parse(responseBody);
        var resultArray = jsonResult["result"]["result"].ToObject<byte[]>();
        var resultString = Encoding.ASCII.GetString(resultArray);
        
        return resultString;
    }



}