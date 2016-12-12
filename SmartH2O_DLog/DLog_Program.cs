using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SmartH2O_DLog
{
    class DLog_Program
    {
        public static String DU_NODE_NAME = "SENSOR_NODE";
        public static String ALARM_NODE_NAME = "ALARM_NODE";
        public static MqttClient m_cClient = new MqttClient(IPAddress.Parse("127.0.0.1"));
        private static string[] m_strTopicsInfo = { DU_NODE_NAME, ALARM_NODE_NAME };
        private static byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };

        static void Main(string[] args)
        {
            m_cClient.Connect(Guid.NewGuid().ToString());
            if (!m_cClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }

            m_cClient.Subscribe(m_strTopicsInfo, qosLevels);

            m_cClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        }

        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {

            //RECIEVED A MESSAGE
            Console.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic + "\n");

            if (e.Topic == DU_NODE_NAME)
            {
                recievedDUMessage(e.Message);
            }
            else {
                recievedAlarmMessage(e.Message);
            }
        }

        private static void recievedDUMessage(byte[] message)
        {
            Console.Write("SENSOR MESSAGE:" + Encoding.UTF8.GetString(message) + "\n\n");
        }

        private static void recievedAlarmMessage(byte[] message) {
            Console.Write("ALARM MESSAGE:" + Encoding.UTF8.GetString(message) + "\n\n");
        }
    }
}
