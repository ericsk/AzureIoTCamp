using System;
using Microsoft.ServiceBus.Messaging;

namespace EventHubReceiver
{
  class Program
  {
    static void Main(string[] args)
    {
      string eventHubConnectionString = "讀取原則的連接字串";
	  string eventHubName = "事件中樞名稱";
	  string storageAccountName = "儲存體帳戶名稱";
	  string storageAccountKey = "儲存體存取金鑰";
      string storageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}",
                  storageAccountName, storageAccountKey);

      string eventProcessorHostName = Guid.NewGuid().ToString();
      EventProcessorHost eventProcessorHost = new EventProcessorHost(eventProcessorHostName, eventHubName, EventHubConsumerGroup.DefaultGroupName, eventHubConnectionString, storageConnectionString);
      eventProcessorHost.RegisterEventProcessorAsync<ReceiveProcessor>().Wait();

      Console.WriteLine("Receiving.Press enter key to stop worker.");
      Console.ReadLine();
    }
  }
}
