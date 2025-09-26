namespace Application.Services;

public class RandomStringGenerator
{
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var result = new char[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = chars[random.Next(chars.Length)];
        }
        return new string(result);
    }

    public static void Main()
    {
        string randomStr = RandomString(10);
        Console.WriteLine(randomStr); // exemple: "aZ3xQ9kP2L"
    }
}