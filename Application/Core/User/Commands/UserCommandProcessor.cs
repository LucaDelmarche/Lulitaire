using Application.user.commands.create;
using Application.user.commands.PatchUsername;
using Application.user.commands.put;
using Application.utils;

namespace Application.user.commands;

public class UserCommandProcessor
{
    private readonly ICommandHandler<UserCreateCommand,UserCreateOutput.UserCreateDto> _userCommandProcessor;
    private readonly IEmptyOutputCommandHandler<int> _userEmptyCommandProcessorDelete;
    private readonly IEmptyOutputCommandHandler<UserPatchUsernameCommand> _userEmptyCommandProcessorPatchUsername;
    private readonly IEmptyOutputCommandHandler<UserPatchEmailAddressCommand> _userEmptyCommandProcessorPatchEmail;
    private readonly IEmptyOutputCommandHandler<UserPatchRoleCommand> _userEmptyCommandProcessorPatchRole;
    private readonly IEmptyOutputCommandHandler<UserPutCommand> _userEmptyCommandProcessorPut;


    public UserCommandProcessor(ICommandHandler<UserCreateCommand, UserCreateOutput.UserCreateDto> userCommandProcessor, IEmptyOutputCommandHandler<int> userEmptyCommandProcessorDelete, IEmptyOutputCommandHandler<UserPatchUsernameCommand> userEmptyCommandProcessorPatchUsername, IEmptyOutputCommandHandler<UserPatchEmailAddressCommand> userEmptyCommandProcessorPatchEmail, IEmptyOutputCommandHandler<UserPatchRoleCommand> userEmptyCommandProcessorPatchRole, IEmptyOutputCommandHandler<UserPutCommand> userEmptyCommandProcessorPut)
    {
        _userCommandProcessor = userCommandProcessor;
        _userEmptyCommandProcessorDelete = userEmptyCommandProcessorDelete;
        _userEmptyCommandProcessorPatchUsername = userEmptyCommandProcessorPatchUsername;
        _userEmptyCommandProcessorPatchEmail = userEmptyCommandProcessorPatchEmail;
        _userEmptyCommandProcessorPatchRole = userEmptyCommandProcessorPatchRole;
        _userEmptyCommandProcessorPut = userEmptyCommandProcessorPut;
    }

    public UserCreateOutput.UserCreateDto Create(UserCreateCommand command) {
        return _userCommandProcessor.Handle(command);
    }

    public void Delete(int id)
    {
        _userEmptyCommandProcessorDelete.Handle(id);
    }
    
    public void Put(UserPutCommand command)
    {
        _userEmptyCommandProcessorPut.Handle(command);
    }
    public void PatchRole(UserPatchRoleCommand command)
    {
        _userEmptyCommandProcessorPatchRole.Handle(command);
    }

    public void PatchUsername(UserPatchUsernameCommand command)
    {
        _userEmptyCommandProcessorPatchUsername.Handle(command);
    }
    public void PatchEmail(UserPatchEmailAddressCommand command)
    {
        _userEmptyCommandProcessorPatchEmail.Handle(command);
    }
}