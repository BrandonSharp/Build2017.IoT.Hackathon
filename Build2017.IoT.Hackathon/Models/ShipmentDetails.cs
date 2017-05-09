using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Build2017.IoT.Hackathon.Models
{
    [DataContract]
    public class ShipmentDetails
    {
        [DataMember]
        public bool WasDropped { get; set; }
        [DataMember]
        public bool WasTipped { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool IsFreefallAllowed { get; set; }
        [DataMember]
        public bool IsTippingAllowed { get; set; }

        [DataMember]
        public int MinimumTemperature { get; set; }
        [DataMember]
        public int MaximumTemperature { get; set; }

        [DataMember]
        public int MinimumHumidity { get; set; }
        [DataMember]
        public int MaximumHumidity { get; set; }
    }
}
