using System;

namespace Application.Request
{
    public abstract class Request : IRequest
    {
        public DateTimeOffset RequestedAt { get; set; }

        protected Request()
        {
            RequestedAt = DateTimeOffset.Now;
        }
    }

    public abstract class Request<TResponse> : Request, IRequest<TResponse>
    {
    }
}