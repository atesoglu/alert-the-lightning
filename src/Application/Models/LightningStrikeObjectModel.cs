using System.Text.Json;
using Application.Models.Base;
using Domain.Models;

namespace Application.Models
{
    public class LightningStrikeObjectModel : ObjectModelBase<LightningStrikeModel>
    {
        public int LightningStrikeId { get; set; }
        public FlashTypes FlashType { get; set; }
        public string FlashTypeDescription { get; set; }
        public long StrikeTime { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int PeakAmps { get; set; }
        public string Reserved { get; set; }
        public int IcHeight { get; set; }
        public long ReceivedTime { get; set; }
        public int NumberOfSensors { get; set; }
        public int Multiplicity { get; set; }

        public LightningStrikeObjectModel()
        {
        }

        public LightningStrikeObjectModel(LightningStrikeModel entity)
        {
            AssignFrom(entity);
        }

        public sealed override void AssignFrom(LightningStrikeModel entity)
        {
            LightningStrikeId = entity.LightningStrikeId;
            FlashType = entity.FlashType;
            FlashTypeDescription = entity.FlashType == FlashTypes.ToGround ? "Cloud to ground" : "Cloud to cloud";
            StrikeTime = entity.StrikedAt.ToUnixTimeMilliseconds();
            Latitude = entity.Latitude;
            Longitude = entity.Longitude;
            PeakAmps = entity.PeakAmps;
            Reserved = entity.Reserved;
            IcHeight = entity.IcHeight;
            ReceivedTime = entity.ReceivedAt.ToUnixTimeMilliseconds();
            NumberOfSensors = entity.NumberOfSensors;
            Multiplicity = entity.Multiplicity;
        }
        
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}