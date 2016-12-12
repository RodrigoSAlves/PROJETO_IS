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
            XmlNode sensorNode = doc.CreateElement("SensorNode");
            doc.AppendChild(sensorNode);

            //numero de indice         //moleculas            //ph              //data(para as estatisticas)
            XmlElement number = doc.CreateElement("id");
            number.InnerText = messages[0];

            XmlElement molecule = doc.CreateElement("molecule");
            molecule.InnerText = messages[1];

            XmlElement ph = doc.CreateElement("value");
            ph.InnerText = messages[2];

            XmlElement data = doc.CreateElement("Data");
            data.InnerText = DateTime.Now.ToString("dd/mm/yyyy hh:mm:ss");

            sensorNode.AppendChild(number);
            sensorNode.AppendChild(molecule);
            sensorNode.AppendChild(ph);
            sensorNode.AppendChild(data);

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
