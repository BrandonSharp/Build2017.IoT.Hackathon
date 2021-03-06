﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Build2017.IoT.Hackathon.Models;
using System.Text;
using Microsoft.Azure.Devices;

namespace Build2017.IoT.Hackathon.Controllers
{
    [Route("api/[controller]")]
    public class ShipmentsController : Controller
    {
        static string iotHubConnectionstring = "HostName=<iot-hub>.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=<sas-key>";
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=hackathon2017;AccountKey=<access-key>;EndpointSuffix=core.windows.net");
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
            shipInfo.TrackerId = "0080E1B97233";
            shipInfo.TrackerName = "tracker1";

            TableOperation insertOp = TableOperation.Insert(shipInfo);
            await table.ExecuteAsync(insertOp);

            var iotClient = Microsoft.Azure.Devices.ServiceClient.CreateFromConnectionString(iotHubConnectionstring);

            var methodInvocation = new CloudToDeviceMethod("StartTracking") { ResponseTimeout = TimeSpan.FromSeconds(30) };
            methodInvocation.SetPayloadJson("{}");
            await iotClient.InvokeDeviceMethodAsync(shipInfo.TrackerName, methodInvocation);
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
                return resultingData.ToList().OrderBy(x => x.Timestamp);
            } else {
                return null;
            }

        }

        [Route("{shipmentName}/details")]
        [HttpGet]
        public async Task<ShipmentDetails> GetShipmentDetails(string shipmentName) {

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Shipments");

            TableOperation getOp = TableOperation.Retrieve<ShipmentInformation>(shipmentName, shipmentName);
            var shipmentResult = await table.ExecuteAsync(getOp);

            if (shipmentResult.Result != null) {
                ShipmentInformation shippingInfo = ((ShipmentInformation)shipmentResult.Result);

                CloudTable dataTable = tableClient.GetTableReference("DeviceData");
                TableQuery<DeviceData> dataquery = new TableQuery<DeviceData>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, shippingInfo.TrackerId));
                var resultingData = dataTable.ExecuteQuery(dataquery);
                var results = resultingData.ToList().OrderBy(x => x.Timestamp);

                var details = new ShipmentDetails() {
                    Name = shippingInfo.Name,
                    Description = shippingInfo.Description,
                    IsFreefallAllowed = shippingInfo.IsFreefallAllowed,
                    IsTippingAllowed = shippingInfo.IsTippingAllowed,
                    MaximumHumidity = shippingInfo.MaximumHumidity,
                    MinimumHumidity = shippingInfo.MinimumHumidity,
                    MaximumTemperature = shippingInfo.MaximumTemperature,
                };
            } else {
                return null;
            }
            return new ShipmentDetails();
        }
    }
}
