using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Alarms
{
    public partial class Form1 : Form
    {
        public static String DU_NODE_NAME = "SENSOR_NODE";
        public static String ALARM_NODE_NAME = "ALARM_NODE";
        private static string[] m_strSensorInfo = { DU_NODE_NAME };
        private static byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };
        private static MqttClient m_cClient = new MqttClient("127.0.0.1");
        private XmlDocument temp_doc;

        public Form1()
        {
            InitializeComponent();
        }

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            temp_doc.LoadXml(Encoding.UTF8.GetString(e.Message));
            SetText(Encoding.UTF8.GetString(e.Message));

            String str_sensorType = temp_doc.SelectSingleNode("/sensor").Attributes["type"].Value;
            String str_value = temp_doc.SelectSingleNode("/sensor/entry").Attributes["value"].Value.Replace(".", ",");

            float sensor_value = float.Parse(str_value);

            //Load Rules 
            string xml_triggerRules = AppDomain.CurrentDomain.BaseDirectory.ToString() + "trigger-rules.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(xml_triggerRules);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodeList = root.ChildNodes;



            //Check for conditions
            foreach (XmlNode l in nodeList)
            {

                if (l.Attributes["tipo"].Value == str_sensorType)
                {
                    XmlNodeList constrains = l.ChildNodes;

                    foreach (XmlNode c in constrains)
                    {
                        XmlNodeList condition = c.ChildNodes;

                        foreach (XmlNode con in condition)
                        {
                            if (con.Attributes["ativo"].Value == "true")
                            {
                                float ref_value = float.Parse(con.Attributes["valorReferencia"].Value);
                                string operador = con.Attributes["operator"].Value;

                                if (operador == "equals")
                                {
                                    if (sensor_value == ref_value)
                                    {
                                        String[] conditionTriggered = { operador, "" + ref_value, "" };
                                        publishAlarms(str_sensorType, temp_doc.OuterXml, conditionTriggered);
                                        Console.WriteLine("Alarm Triggered:" + conditionTriggered[0] + " - " + conditionTriggered[1] + " - " + conditionTriggered[2]);
                                    }
                                }
                                else if (operador == "less")
                                {
                                    if (sensor_value < ref_value)
                                    {
                                        String[] conditionTriggered = { operador, "" + ref_value, "" };
                                        publishAlarms(str_sensorType, temp_doc.OuterXml, conditionTriggered);
                                        Console.WriteLine("Alarm Triggered:" + conditionTriggered[0] + " - " + conditionTriggered[1] + " - " + conditionTriggered[2]);
                                    }
                                }
                                else if (operador == "greater")
                                {
                                    if (sensor_value > ref_value)
                                    {
                                        String[] conditionTriggered = { operador, "" + ref_value, "" };
                                        publishAlarms(str_sensorType, temp_doc.OuterXml, conditionTriggered);
                                        Console.WriteLine("Alarm Triggered:" + conditionTriggered[0] + " - " + conditionTriggered[1] + " - " + conditionTriggered[2]);
                                    }
                                }
                                else if (operador == "between")
                                {
                                    int ref_value2 = int.Parse(con.Attributes["valorReferencia2"].Value);
                                    if (sensor_value > ref_value && sensor_value < ref_value2)
                                    {
                                        String[] conditionTriggered = { operador, "" + ref_value, "" + ref_value2 };
                                        publishAlarms(str_sensorType, temp_doc.OuterXml, conditionTriggered);
                                        Console.WriteLine("Alarm Triggered:" + conditionTriggered[0] + " - " + conditionTriggered[1] + " - " + conditionTriggered[2]);
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

        public static void publishAlarms(String sensorType, String xmlMessage, String[] conditionTriggered)
        {
            XmlDocument message = new XmlDocument();
            message.LoadXml(xmlMessage);

            XmlDocument publishDoc = new XmlDocument();
            XmlNode declaration = publishDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement alarm = publishDoc.CreateElement("alarm");
            alarm.SetAttribute("type", sensorType);
            XmlElement entry = (XmlElement)publishDoc.ImportNode(message.SelectSingleNode("/sensor/entry"), false);
            alarm.AppendChild(entry);

            XmlElement condition = publishDoc.CreateElement("condition");
            condition.SetAttribute("condition", conditionTriggered[0]);
            condition.SetAttribute("trigger_value", conditionTriggered[1]);
            if (!(conditionTriggered[2].Length == 0))
                condition.SetAttribute("trigger_value2", conditionTriggered[2]);

            alarm.AppendChild(condition);

            publishDoc.AppendChild(declaration);
            publishDoc.AppendChild(alarm);

            if (!m_cClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }
            Console.WriteLine(publishDoc.OuterXml);
            m_cClient.Publish(ALARM_NODE_NAME, Encoding.UTF8.GetBytes(publishDoc.OuterXml));

        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text = text;
            }
        }

        private void btnStartAlarm_Click(object sender, EventArgs e)
        {
            temp_doc = new XmlDocument();
            m_cClient.Connect(Guid.NewGuid().ToString());

            if (!m_cClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }

            m_cClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            //Subscribe to topics
            //QoS
            m_cClient.Subscribe(m_strSensorInfo, qosLevels);

            System.Threading.Thread.Sleep(10);

        }

        private void btnStopAlarm_Click(object sender, EventArgs e)
        {
            if (m_cClient.IsConnected)
            {
                m_cClient.Unsubscribe(m_strSensorInfo); //Put this in a button to see notif!
                m_cClient.Disconnect(); //Free process and process's resources
            }
        }
    }
}
