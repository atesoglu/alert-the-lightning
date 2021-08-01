using System.Text.Json;
using Application.Models;
using Application.Request;
using Domain.Models;

namespace Application.Flows.LightningStrikes.Commands
{
    public class ProcessLightningStrikeCommand : Request<LightningStrikeObjectModel>
    {
        public FlashTypes FlashType { get; set; }
        public long StrikeTime { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int PeakAmps { get; set; }
        public string Reserved { get; set; }
        public int IcHeight { get; set; }
        public long ReceivedTime { get; set; }
        public int NumberOfSensors { get; set; }
        public int Multiplicity { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}