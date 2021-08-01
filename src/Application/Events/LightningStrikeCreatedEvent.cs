using System;
using Application.Events.Base;
using Application.Models;

namespace Application.Events
{
    public class LightningStrikeCreatedEvent : Event<LightningStrikeObjectModel>
    {
        public LightningStrikeCreatedEvent(LightningStrikeObjectModel model, DateTimeOffset requestedAt) : base(model, requestedAt)
        {
        }
    }
}