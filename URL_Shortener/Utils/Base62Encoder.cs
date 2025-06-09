using System.Text;

namespace URL_Shortener.Utils;

public class Base62Encoder
{
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    public static string Encode(long num)
    {
        var Enc = new StringBuilder();
        while (num > 0)
        {
            Enc.Insert(0, Alphabet[(int)(num % 62)]);
            num /= 62;
        }
        
        return Enc.ToString();
    }
}