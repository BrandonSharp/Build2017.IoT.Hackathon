using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Build2017.IoT.Hackathon.Models;

namespace Build2017.IoT.Hackathon.Controllers
{
    [Route("api/[controller]")]
    public class ShipmentsController : Controller
    {
        static string iotHubConnectionstring = "HostName=bs-iot-hackathon.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=CruDa1E8fFYEuYFu2DHrzFW3rdA4IW8+jiRwD63vh6E=";
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=hackathon2017;AccountKey=5LktSt6p93x66DZQQm9yMi6CgZn/8uuI+X5Hk5biSa+sNqQfmHzQHH1drT0amWCAIsP+92yiR251GGa3FE7prQ==;EndpointSuffix=core.windows.net");
        public ShipmentsController() {

        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> GetShipmentIds() {
            return new string[] { "S001", "S002", "S003", "S004", "S005" };
        }

        [Route("devices")]
        [HttpGet]
        public IEnumerable<string> GetDeviceIds() {
            var iotClient = Microsoft.Azure.Devices.ServiceClient.CreateFromConnectionString(iotHubConnectionstring);
            
            return null;
        }

        [HttpPost]
        public async Task<bool> CreateShipment(ShipmentInformation shipInfo) {
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Shipments");

            shipInfo.RowKey = shipInfo.Name;

            TableOperation insertOp = TableOperation.Insert(shipInfo);
            await table.ExecuteAsync(insertOp);
            return true;
        }

        [Route("{shipmentName}/devicedata")]
        [HttpGet]
        public async Task<IEnumerable<DeviceData>> GetDeviceData(string shipmentName) {
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Shipments");

            TableOperation getOp = TableOperation.Retrieve<ShipmentInformation>(shipmentName, shipmentName);
            var shipmentResult = await table.ExecuteAsync(getOp);

            if(shipmentResult.Result != null) {
                ShipmentInformation shippingInfo = ((ShipmentInformation)shipmentResult.Result);

                CloudTable dataTable = tableClient.GetTableReference("DeviceData");
                TableQuery<DeviceData> dataquery = new TableQuery<DeviceData>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, shippingInfo.TrackerId));
                var resultingData = dataTable.ExecuteQuery(dataquery);
                return resultingData.ToList().OrderBy(x => x.MessageId);
            } else {
                return null;
            }

        }
    }
}
