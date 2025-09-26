using Domain.services;

namespace Domain;

public class Zone
{
    private string _name;

    public Zone(string name)
    {
        _name = name;
    }

    public string Name
    {
        get => _name;
        set => _name = GenericSetter.SetString(value, 100, "Zone name");
    }
}