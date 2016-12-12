using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;


namespace SmartH2O_Alarm
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("bsdaofgof");
            MqttClient dataClient = new MqttClient("127.0.0.1");
            string[] m_strSensorInfo = { "ALARM_NODE" };

            dataClient.Connect(Guid.NewGuid().ToString());

            if (!dataClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }

            dataClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            //Subscribe to topics
            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };//QoS
            dataClient.Subscribe(m_strSensorInfo, qosLevels);

            System.Threading.Thread.Sleep(10);

            Console.ReadKey();

            if (dataClient.IsConnected)
            {
                dataClient.Unsubscribe(m_strSensorInfo); //Put this in a button to see notif!
                dataClient.Disconnect(); //Free process and process's resources
            }


        }

        private static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine("fgeougqoeufg");
            String[] sensor = new String[4];
            int i = 0;
            String strTemp = Encoding.UTF8.GetString(e.Message);
            Console.WriteLine(strTemp + "Received");

            XmlDocument docReceived = new XmlDocument();
            docReceived.LoadXml(strTemp);

            XmlNodeList sensorsList = docReceived.GetElementsByTagName("SENSOR_NODE");

            foreach(XmlNode x in sensorsList)
            {
                XmlNodeList dataList = x.ChildNodes;
                    foreach(XmlNode y in dataList)
                {
                    sensor[i] = y.InnerText;
                    Console.WriteLine(sensor[i]);
                    i++;
                }
                
            }
            

            string xml = AppDomain.CurrentDomain.BaseDirectory.ToString() + "trigger-rules.xml";

            XmlDocument doc = new XmlDocument();

            doc.Load(xml);

            XmlElement root = doc.DocumentElement;

            XmlNodeList nodeList = root.ChildNodes;

            String replaced = sensor[2].Replace(',', ',');

            foreach (XmlNode l in nodeList)
            {

                if (l.Attributes["tipo"].Value == sensor[1])
                {
                    XmlNodeList constrains = l.ChildNodes;

                    foreach (XmlNode c in constrains)
                    {
                        XmlNodeList condition = c.ChildNodes;

                        foreach (XmlNode con in condition)
                        {
                            if (con.Attributes["ativo"].Value == "true")
                            {
                                float value = float.Parse(con.Attributes["valorReferencia"].Value);
                                String alarmCondition;
                                float dataValue = float.Parse(replaced);
                                string operador = con.Attributes["operator"].Value;

                                if (operador == "equals")
                                {
                                    if (dataValue == value)
                                    {
                                        alarmCondition = "Value received (" + dataValue.ToString() + ") is equal to reference value (" + value.ToString() + ")";
                                        publishAlarms(strTemp, alarmCondition);
                                        Console.WriteLine("Alarm Triggered" + sensor[1]);
                                    }
                                }
                                else if (operador == "less")
                                {
                                    if (dataValue < value)
                                    {
                                        alarmCondition = "Value received (" + dataValue.ToString() + ") is less than reference value (" + value.ToString() + ")";
                                        publishAlarms(strTemp, alarmCondition);
                                        Console.WriteLine("Alarm Triggered" + sensor[1]);
                                    }
                                }
                                else if (operador == "greater")
                                {
                                    if (dataValue > value)
                                    {
                                        alarmCondition = "Value received (" + dataValue.ToString() + ") is greater than reference value (" + value.ToString() + ")";
                                        publishAlarms(strTemp, alarmCondition);
                                        Console.WriteLine("Alarm Triggered" + sensor[1]);
                                    }
                                }
                                else if (operador == "between")
                                {
                                    int value2 = int.Parse(con.Attributes["valorReferencia2"].Value);
                                    if (dataValue > value && dataValue < value2)
                                    {
                                        alarmCondition = "Value received (" + dataValue.ToString() + ") is between reference value (" + value.ToString() + ") and value (" + value2 + ")";
                                        publishAlarms(strTemp, alarmCondition);
                                        Console.WriteLine("Alarm Triggered" + sensor[1]);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No alarm Triggered");
                                }
                            }
                        }
                    }

                }
            }

        }

        public static void publishAlarms(String stringXml, String alarm)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(stringXml);

            MqttClient dataClient = new MqttClient("127.0.0.1");
            string[] m_strSensorInfo = { "ALARM_NODE" };

            dataClient.Connect(Guid.NewGuid().ToString());

            if (!dataClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }

            XmlElement alarmCondition = doc.CreateElement("alarmCondition");
            alarmCondition.InnerText = alarm;

            doc.AppendChild(alarmCondition);

            
            
            dataClient.Publish("ALARM_NODE", Encoding.UTF8.GetBytes(doc.OuterXml));

            System.Threading.Thread.Sleep(10);


            if (dataClient.IsConnected)
            {
                dataClient.Unsubscribe(m_strSensorInfo); //Put this in a button to see notif!
                dataClient.Disconnect(); //Free process and process's resources
            }
        }




    }
}