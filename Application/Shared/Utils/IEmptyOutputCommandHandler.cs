
namespace Application.utils;

public interface IEmptyOutputCommandHandler<in TInput>
{
    void Handle(TInput input);
}