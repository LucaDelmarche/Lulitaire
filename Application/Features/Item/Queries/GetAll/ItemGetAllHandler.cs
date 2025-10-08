using Application.Features.Zone.Queries.GetAll;
using Application.user.queries.getAll;
using Application.utils;
using AutoMapper;
using Infrastructure.Entities;
using Infrastructure.Repositories.Generic;

namespace Application.Features.Item.Queries.GetAll;

public class ItemGetAllHandler : IQueryHandler<ItemGetAllQuery,ItemGetallOutput>
{
    private readonly IGenericRepository<DbItem> _repository;
    private readonly IMapper _iMapper;

    public ItemGetAllHandler(IGenericRepository<DbItem> repository, IMapper iMapper)
    {
        _repository = repository;
        _iMapper = iMapper;
    }
    
    public ItemGetallOutput Handle(ItemGetAllQuery input)
    {
        IEnumerable<DbItem?> items = _repository.GetAll();
        ItemGetallOutput output = new ItemGetallOutput
        {
            Items = new List<ItemGetallOutput.ItemDto>()
        };
        foreach (DbItem? item in items)
        {
            ItemGetallOutput.ItemDto itemDto = new ItemGetallOutput.ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Id_user = item.UserId,
                Quantity = item.Quantity,
                Unit = item.Unit,
                ExpirationDate = item.ExpritationData,
                ZoneId = item.ZoneId
            };
            
            if (itemDto.Id_user == int.Parse(input.id_user) && itemDto.ZoneId == input.id_zone)
            {
                output.Items.Add(itemDto);
            }
        }

        return output;
    }
    
}