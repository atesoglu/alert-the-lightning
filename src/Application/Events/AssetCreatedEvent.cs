using System;
using Application.Events.Base;
using Application.Models;
using Domain.Models;

namespace Application.Events
{
    public class AssetCreatedEvent : Event<AssetObjectModel>
    {
        public AssetCreatedEvent(AssetObjectModel model, DateTimeOffset requestedAt) : base(model, requestedAt)
        {
        }
    }
}