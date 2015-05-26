using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace EventHubSender
{
  class Program
  {
    static string eventHubName = "{YOUR_EVENT_HUB_NAME}";
    static string connectionString = "{YOUR_SENDER_CONNECTION_STRING}";

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

      var random = new Random();

      while (true)
      {
        var guid = Guid.NewGuid().ToString();
        var time = DateTime.Now.ToString();
        var thermal = random.Next(195, 265) / 10.0 ;
        var humidity = random.Next(58, 82);
        var message = "{\"id\":\""+guid+"\", \"thermal\":"+thermal+", \"humidity\":"+humidity+", \"time\":\""+time+"\"}";

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
