using System;
using System.Net;
using System.Text;
using System.Xml;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Communication_Infrastructure
{
    class CI_Program
    {
        public static String DU_NODE_NAME = "SENSOR_NODE";
        public static String ALARM_NODE_NAME = "ALARM_NODE";
        public static String LOGGER_NODE_NAME = "LOGGER_NODE";

        private static string[] m_strTopicsInfo = {DU_NODE_NAME, ALARM_NODE_NAME};
        private static MqttClient m_cClient = new MqttClient(IPAddress.Parse("127.0.0.1"));
        private static byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE};


        static void Main(string[] args)
        {
            m_cClient.Connect(Guid.NewGuid().ToString());
            if (!m_cClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }
            //SPECIFY THE EVENTS WE ARE INSTERESTED ON
            //NEW MESSAGE RECIEVED
            m_cClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            //SUBSCRITE TO TOPICS
            m_cClient.Subscribe(m_strTopicsInfo, qosLevels);

            //FINISH THE PROGRAM
            Console.ReadKey();
            if (m_cClient.IsConnected)
            {
                m_cClient.Unsubscribe(m_strTopicsInfo); //Put this in a button to see notif!
                m_cClient.Disconnect(); //Free process and process's resources
            }
        }

        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //RECIEVED A MESSAGE
            Console.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic + "\n");

            if (e.Topic == DU_NODE_NAME)
            {
                messageReceived(e.Message);
            }
        }

        private static void messageReceived(byte[] message)
        {
            XmlDocument doc = new XmlDocument();
            String xml = Encoding.UTF8.GetString(message);
            doc.LoadXml(xml);
            XmlNode nome = doc.SelectSingleNode("/sensor/name");
        }
       
    }
}
