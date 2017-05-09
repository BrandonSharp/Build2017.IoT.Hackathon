using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Build2017.IoT.Hackathon.Models
{
    public class DeviceData : TableEntity {
        public long MessageId { get; set; }

        public long AccX { get; set; }
        public long AccY { get; set; }
        public long AccZ { get; set; }

        public long GyrX { get; set; }
        public long GyrY { get; set; }
        public long GyrZ { get; set; }


        public long Humidity { get; set; }
        public long Temperature { get; set; }

    }
}