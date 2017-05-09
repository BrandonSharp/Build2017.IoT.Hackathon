using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Build2017.IoT.Hackathon.Models
{
    public class ShipmentInformation : TableEntity
    {
        public string TrackerId { get; set; }
        public string TrackerName { get; set; }


        public string Name {
            get { return PartitionKey; }
            set {
                PartitionKey = value;
                RowKey = value;
            }
        }
        public string Description { get; set; }

        public bool IsFreefallAllowed { get; set; }
        public bool IsTippingAllowed { get; set; }

        public int MinimumTemperature { get; set; }
        public int MaximumTemperature { get; set; }

        public int MinimumHumidity { get; set; }
        public int MaximumHumidity { get; set; }
    }
}
