using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
        private WebClient webClient;


        static void Main(string[] args)
        {
            DLog_Program program = new DLog_Program();
            program.init();            
        }

        private void init()
        {
            m_cClient.Connect(Guid.NewGuid().ToString());
            if (!m_cClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }

            m_cClient.Subscribe(m_strTopicsInfo, qosLevels);

            m_cClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            webClient = new WebClient();

        }

        public void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //RECIEVED A MESSAGE
            Console.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic + "\n");

            if (e.Topic == DU_NODE_NAME)
            {
                postParameterEntry(e.Message);
            }

            if (e.Topic == ALARM_NODE_NAME)
            {
                postAlarmTriggered(e.Message);
            }
        }

        private void postAlarmTriggered(byte[] message) {
            Console.WriteLine("Alarm:\n"+Encoding.UTF8.GetString(message));
            webClient.Headers["Content-type"] = "application/xml";
            byte[] result = webClient.UploadData("http://localhost:55959/alarm/postAlarm", "POST", message);
            Console.WriteLine(Encoding.UTF8.GetString(result));

        }

        private void postParameterEntry(byte[] message)
        {
            Console.WriteLine("Parameter:\n"+Encoding.UTF8.GetString(message));
            webClient.Headers["Content-type"] = "application/xml";
            byte[] result = webClient.UploadData("http://localhost:55959/parameter/postEntry", "POST", message);
            Console.WriteLine(Encoding.UTF8.GetString(result));

        }

    }
}
