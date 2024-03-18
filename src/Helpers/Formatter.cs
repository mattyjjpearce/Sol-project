using System.Text;
using System.Text.Json;

public class Formatter
{
    public static string FormatResponseData(string json)
    {
        
        var nearResponse = JsonSerializer.Deserialize<NearResponse>(json);

        //Results array comes as a list of integers which represent ASCII values. Below converts these to relevant characters and then to a string. 
        var resultArray = nearResponse?.Result?.Result?.ToArray();

        var resultString = resultArray != null ? Encoding.ASCII.GetString(resultArray.Select(Convert.ToByte).ToArray()) : string.Empty;

        return resultString;
    }
}