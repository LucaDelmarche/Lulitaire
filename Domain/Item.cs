using Domain.services;

namespace Domain;

public class Item
{
    private string _name;

    public Item(string name, double quantity, string expirationDate)
    {
        _name = name;
        _quantity = quantity;
        _expirationDate = expirationDate;
    }

    public string Name
    {
        get => _name;
        set => _name = GenericSetter.SetString(value, 100, "Item name");
    }

    private double _quantity;

    public double Quantity
    {
        get => _quantity;
        set => _quantity = GenericSetter.SetDouble(value,"Item quantity");
    }

    private string _expirationDate;

    public string ExpirationDate
    {
        get => _expirationDate;
        set => GenericSetter.SetString(value, 10, "Expiration Data");
    }
}