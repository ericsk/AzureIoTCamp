using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace EventHubSender
{
  class Program
  {
    static string eventHubName = "iotlab";
    static string connectionString = "Endpoint=sb://azureiotlab-ns.servicebus.windows.net/;SharedAccessKeyName=SendPolicy;SharedAccessKey=NHUpR3ZMakCWj7rQMPwEWZGycU4k2TSkfENNTTA83hU=";

    static void Main(string[] args)
    {
      Console.WriteLine("按下 Ctrl-C 來停止傳送訊息");
      Console.WriteLine("按下 Enter 鍵後開始傳送訊息");
      Console.ReadLine();
      SendingRandomMessages().Wait();
    }

    static async Task SendingRandomMessages()
    {
      var eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);
      while (true)
      {
        var guid = Guid.NewGuid().ToString();
        var time = DateTime.Now.ToString();
        var message = "{\"id\":\""+guid+"\", \"time\":\""+time+"\"}";

        try
        {
          Console.WriteLine("{0} > 傳送訊息:{1}", time, message);
          await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
        }
        catch (Exception exception)
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine("{0} > 例外狀況:{1}", time, exception.Message);
          Console.ResetColor();
        }

        await Task.Delay(200);
      }
    }
  }
}
