using System;

namespace Application.Events.@Base
{
    public abstract class EventBase
    {
        public bool IsPublished { get; set; }
        public DateTimeOffset OccurredAt { get; protected set; }

        protected EventBase()
        {
            OccurredAt = DateTimeOffset.Now;
        }
    }
}