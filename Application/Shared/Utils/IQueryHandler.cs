
namespace Application.utils;

public interface IQueryHandler<in TInput, out TOutput>
{
   TOutput Handle(TInput input);
}