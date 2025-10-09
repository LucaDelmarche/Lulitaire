namespace Application.Features.Item.Commands.Create;

public class ItemCreateOutput
{
    public ItemCreateDto itemdto;
    public class ItemCreateDto
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public string ExpirationDate { get; set; }
        public string Location { get; set; }
    }
}