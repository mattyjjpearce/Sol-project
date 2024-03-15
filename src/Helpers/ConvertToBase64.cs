using System;
using System.Text;

public class Encoder
{
    public static string convertToBase64(string id)
    {
        byte[] byteArray = Encoding.UTF8.GetBytes(id);

        string base64String = Convert.ToBase64String(byteArray);

        return base64String;
    }
}