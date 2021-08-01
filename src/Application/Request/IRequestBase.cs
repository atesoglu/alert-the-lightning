using System;

namespace Application.Request
{
    public interface IRequestBase
    {
        DateTimeOffset RequestedAt { get; set; }
    }
}