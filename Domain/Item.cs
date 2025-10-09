using Domain.services;

namespace Domain;

public class Item
{
    private string _name;

    public Item(string name, double quantity, string unit,string expirationDate, string location)
    {
        Unit = unit;
        Location = location;
        Name = name;
        Quantity = quantity;
        ExpirationDate = expirationDate;
    }
    public Item() { }

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
        set => _expirationDate = GenericSetter.SetString(value, 10, "Expiration Data");
    }
    private string _unit;
    public string Unit
    {
        get => _unit;
        set => _unit = GenericSetter.SetString(value, 20, "Item unit");
    }
    private string _location;

    public string Location
    {
        get => _location;
        set => _location = GenericSetter.SetString(value, 100, "Item location");
    }
}