using System;
using System.Net;
using System.Text;
using System.Xml;
using uPLibrary.Networking.M2Mqtt;

namespace SmartH2O_DU
{
    class DU_Program
    {   
        public static String DU_NODE_NAME = "SENSOR_NODE";
        public static MqttClient m_cClient = new MqttClient(IPAddress.Parse("127.0.0.1"));
        
        static void Main(string[] args)
        {
            DU_Program du_Program = new DU_Program();
            
            SensorNodeDll.SensorNodeDll dll = new SensorNodeDll.SensorNodeDll();
            dll.Initialize(GeneratedMessage, 1000);
            Console.ReadKey();
        }

        public DU_Program() {
            m_cClient.Connect(Guid.NewGuid().ToString());
        }

        public static void GeneratedMessage(string str)
        {
            sendMessage(createXMLDoc(str));
        }

        public static String createXMLDoc(string message)
        {
            String[] messages = message.Split(';');
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);
            XmlElement parent = doc.CreateElement("sensor");
            parent.SetAttribute("type", messages[1]);

            XmlElement entry = doc.CreateElement("entry");
            entry.SetAttribute("value", messages[2]);
            DateTime date = DateTime.Now;
            entry.SetAttribute("day", "" + date.Day);
            entry.SetAttribute("month", "" + date.Month);
            entry.SetAttribute("year", "" + date.Year);
            entry.SetAttribute("hour", "" + date.Hour);
            entry.SetAttribute("minute", "" + date.Minute);
            entry.SetAttribute("milis", "" +date.Millisecond);

            parent.AppendChild(entry);
            doc.AppendChild(parent);

            return doc.OuterXml;
        }

        public static void sendMessage(String XMLMessage)
        {
            if (!m_cClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }
            Console.Write("Sending Message:" + XMLMessage + "\n");
            m_cClient.Publish(DU_NODE_NAME, Encoding.UTF8.GetBytes(XMLMessage));
        }
    }
}
