using System;
using System.Text.Json;
using Domain.Models.Base;

namespace Domain.Models
{
    public class LightningStrikeModel : ModelBase
    {
        public int LightningStrikeId { get; set; }
        public FlashTypes FlashType { get; set; }
        public DateTimeOffset StrikedAt { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int PeakAmps { get; set; }
        public string Reserved { get; set; }
        public int IcHeight { get; set; }
        public DateTimeOffset ReceivedAt { get; set; }
        public int NumberOfSensors { get; set; }
        public int Multiplicity { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}