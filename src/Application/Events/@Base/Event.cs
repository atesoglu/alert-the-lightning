using System;
using System.Text.Json;
using Application.Models.Base;

namespace Application.Events.@Base
{
    public abstract class Event<T> : EventBase
        where T : ObjectModelBase
    {
        public T Model { get; }
        public DateTimeOffset RequestedAt { get; }

        protected Event(T model, DateTimeOffset requestedAt)
        {
            Model = model;
            RequestedAt = requestedAt;
        }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}