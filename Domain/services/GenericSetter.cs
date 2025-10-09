namespace Domain.services;

public class GenericSetter
{
    public static string SetString(string value, int maxLength, string fieldName)
    {
        if (value.Length > maxLength)
            throw new ArgumentException($"{fieldName} cannot exceed {maxLength} characters.");
        return value;
    }
    public static double SetDouble(double value, string fieldName)
    {
        if (value < 0)
        {
            throw new ArgumentException($"{fieldName} cannot be negative.");
        }

        return value;
    }
}