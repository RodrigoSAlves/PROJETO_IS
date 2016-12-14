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
        public static String ALARM_LOG_FILE_NAME = "alarms-data.xml";
        public static String PARAM_LOG_FILE_NAME = "param-data.xml";
        public static MqttClient m_cClient = new MqttClient(IPAddress.Parse("127.0.0.1"));
        private static string[] m_strTopicsInfo = { DU_NODE_NAME, ALARM_NODE_NAME };
        private static byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };
        private XmlDocument params_data;
        private XmlDocument alarms_data;
        private XmlDocument temp_data;


        static void Main(string[] args)
        {
            DLog_Program program = new DLog_Program();
            program.init();            
        }

        private void init()
        {
            initLogXMLFiles();
            temp_data = new XmlDocument();
            m_cClient.Connect(Guid.NewGuid().ToString());
            if (!m_cClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }

            m_cClient.Subscribe(m_strTopicsInfo, qosLevels);

            m_cClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            
           
        }

        private void initLogXMLFiles()
        {
            if (!File.Exists(ALARM_LOG_FILE_NAME))
            {
                createBasicAlarmXMLFile();

            }
            else {
                alarms_data = new XmlDocument();
                alarms_data.Load(ALARM_LOG_FILE_NAME);
            }


            if (!File.Exists(PARAM_LOG_FILE_NAME))
            {
                createBasicParamXMLFile();
            }
            else {
                params_data = new XmlDocument();
                params_data.Load(PARAM_LOG_FILE_NAME);                
            }
        }

        private void createBasicAlarmXMLFile()
        {   
            alarms_data = new XmlDocument();
            XmlDeclaration declaration = alarms_data.CreateXmlDeclaration("1.0", "utf-8", null);
            alarms_data.AppendChild(declaration);
            XmlElement parent_alarms = alarms_data.CreateElement("alarms");
            alarms_data.AppendChild(parent_alarms);
            alarms_data.Save(ALARM_LOG_FILE_NAME);
        }

        private void createBasicParamXMLFile()
        {
            params_data = new XmlDocument();
            XmlDeclaration declaration = params_data.CreateXmlDeclaration("1.0", "utf-8", null);
            params_data.AppendChild(declaration);
            XmlElement parent_paramenter = params_data.CreateElement("parameters");
            parent_paramenter.AppendChild(params_data.CreateElement("pH"));
            parent_paramenter.AppendChild(params_data.CreateElement("Cl2"));
            parent_paramenter.AppendChild(params_data.CreateElement("NH3"));
            
            params_data.AppendChild(parent_paramenter);
            params_data.Save(PARAM_LOG_FILE_NAME);
        }

        public void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //RECIEVED A MESSAGE
            Console.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic + "\n");

            if (e.Topic == DU_NODE_NAME)
            {
                recievedDUMessage(e.Message);
            }

            if (e.Topic == ALARM_NODE_NAME)
            {
                recievedAlarmMessage(e.Message);
            }
        }

        private void recievedDUMessage(byte[] message)
        {
            temp_data.LoadXml(Encoding.UTF8.GetString(message));
            String param_type = temp_data.SelectSingleNode("/sensor").Attributes["type"].InnerText;

            switch (param_type)
            {
                case "PH":
                    {
                        XmlElement list_pH = (XmlElement)params_data.SelectSingleNode("/parameters/pH");
                        list_pH.AppendChild(params_data.ImportNode(temp_data.SelectSingleNode("/sensor/entry"), false));
                    }
                    break;
                case "NH3":
                    {
                        XmlElement list_pH = (XmlElement)params_data.SelectSingleNode("/parameters/NH3");
                        list_pH.AppendChild(params_data.ImportNode(temp_data.SelectSingleNode("/sensor/entry"), false));
                    }
                    break;
                case "CI2":
                    {
                        XmlElement list_pH = (XmlElement)params_data.SelectSingleNode("/parameters/Cl2");
                        list_pH.AppendChild(params_data.ImportNode(temp_data.SelectSingleNode("/sensor/entry"), false));
                    }
                    break;
            }
            params_data.Save(PARAM_LOG_FILE_NAME);
        }

        private void recievedAlarmMessage(byte[] message)
        {

        }

        

        

        
    }
}
