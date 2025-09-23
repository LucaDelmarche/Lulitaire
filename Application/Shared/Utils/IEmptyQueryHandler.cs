namespace Application.utils;

public interface IEmptyQueryHandler<out TOutput>
{
    TOutput Handle();
}