namespace Application.Request
{
    public interface IRequest : IRequestBase
    {
    }

    public interface IRequest<out TResponse> : IRequest
    {
    }
}