using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
namespace MSMQ
{
    class Program 
    {
        static void Main(string[] args)
        {
            //createMQ();
            bool flag = true;
            while (flag)
            {
                createMQ();
                Console.WriteLine("Y/N");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.KeyChar == 'n')
                {
                    flag = false;
                }
            }
        }

        static void createMQ()
        {
            MessageQueue messageQueue = null;
            if (MessageQueue.Exists(@".\Private$\MyQueues"))
            {
                messageQueue = new MessageQueue(@".\Private$\MyQueues");
                messageQueue.Label = "Testing Queue";
            }
            else
            {
                messageQueue = MessageQueue.Create(@".\Private$\MyQueues");
                messageQueue.Label = "Newly Created Queue";
            }
            messageQueue.Formatter = new XmlMessageFormatter(new string[] { "System.String" });
            for (int i = 1; i <= 20; i++)
            {
                messageQueue.Send("日志体" + i.ToString(), "日志标题" + i.ToString());
            }
        }

    }
}
