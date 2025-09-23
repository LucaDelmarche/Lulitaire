namespace Application.utils;

public interface ICommandHandler<in TInput, out TOutput>
{
    TOutput Handle(TInput input);
}