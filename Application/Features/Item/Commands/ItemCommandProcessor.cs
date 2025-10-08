using Application.Features.Item.Commands.Create;
using Application.Features.Zone.Commands.Create;
using Application.Features.Zone.Commands.Delete;
using Application.Features.Zone.Commands.PatchUsername;
using Application.user.commands.create;
using Application.utils;

namespace Application.Features.Item.Commands;

public class ItemCommandProcessor
{
    private readonly ICommandHandler<ItemCreateCommand,ItemCreateOutput.ItemCreateDto> _itemCommandProcessor;
    private readonly IEmptyOutputCommandHandler<ZoneDeleteCommand> _zoneEmptyCommandProcessorDelete;
    private readonly IEmptyOutputCommandHandler<ZonePatchCommand> _zoneEmptyCommandProcessorPatch;


    public ItemCommandProcessor(ICommandHandler<ItemCreateCommand, ItemCreateOutput.ItemCreateDto> itemCommandProcessor, IEmptyOutputCommandHandler<ZoneDeleteCommand> zoneEmptyCommandProcessorDelete, IEmptyOutputCommandHandler<ZonePatchCommand> zoneEmptyCommandProcessorPatch)
    {
        _itemCommandProcessor = itemCommandProcessor;
        _zoneEmptyCommandProcessorDelete = zoneEmptyCommandProcessorDelete;
        _zoneEmptyCommandProcessorPatch = zoneEmptyCommandProcessorPatch;
    }

    public ItemCreateOutput.ItemCreateDto Create(ItemCreateCommand command) {
        return _itemCommandProcessor.Handle(command);
    }

    public void Delete(ZoneDeleteCommand command)
    {
        
        _zoneEmptyCommandProcessorDelete.Handle(command);
    }
    public void Patch(ZonePatchCommand command)
    {
        _zoneEmptyCommandProcessorPatch.Handle(command);
    }
}