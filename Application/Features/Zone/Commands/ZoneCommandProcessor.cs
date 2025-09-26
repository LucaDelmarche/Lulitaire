using Application.Features.Zone.Commands.Create;
using Application.Features.Zone.Commands.Delete;
using Application.Features.Zone.Commands.PatchUsername;
using Application.user.commands.create;
using Application.user.commands.PatchUsername;
using Application.user.commands.put;
using Application.utils;

namespace Application.Features.Zone.Commands;

public class ZoneCommandProcessor
{
    private readonly ICommandHandler<ZoneCreateCommand,ZoneCreateOutput.ZoneCreateDto> _zoneCommandProcessor;
    private readonly IEmptyOutputCommandHandler<ZoneDeleteCommand> _zoneEmptyCommandProcessorDelete;
    private readonly IEmptyOutputCommandHandler<ZonePatchCommand> _zoneEmptyCommandProcessorPatch;


    public ZoneCommandProcessor(ICommandHandler<ZoneCreateCommand, ZoneCreateOutput.ZoneCreateDto> zoneCommandProcessor, IEmptyOutputCommandHandler<ZoneDeleteCommand> zoneEmptyCommandProcessorDelete, IEmptyOutputCommandHandler<ZonePatchCommand> zoneEmptyCommandProcessorPatch)
    {
        _zoneCommandProcessor = zoneCommandProcessor;
        _zoneEmptyCommandProcessorDelete = zoneEmptyCommandProcessorDelete;
        _zoneEmptyCommandProcessorPatch = zoneEmptyCommandProcessorPatch;
    }

    public ZoneCreateOutput.ZoneCreateDto Create(ZoneCreateCommand command) {
        return _zoneCommandProcessor.Handle(command);
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