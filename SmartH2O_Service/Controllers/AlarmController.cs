using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;

namespace SmartH2O_Service.Controllers
{
    public class AlarmController : ApiController
    {
        public static String ALARM_LOG_FILE_PATH = AppDomain.CurrentDomain.BaseDirectory.ToString() +  "alarms-data.xml";
        private XmlDocument alarm_logger;
        private XmlDocument temp_data;

        public AlarmController()
        {
            initLogFile();
            temp_data = new XmlDocument();
        }

        private void initLogFile()
        {
            if (!File.Exists(ALARM_LOG_FILE_PATH))
            {
                createBasicAlarmXMLFile();
            }
            else {
                alarm_logger = new XmlDocument();
                alarm_logger.Load(ALARM_LOG_FILE_PATH);
            }

        }

        private void createBasicAlarmXMLFile()
        {
            alarm_logger = new XmlDocument();
            XmlDeclaration declaration = alarm_logger.CreateXmlDeclaration("1.0", "utf-8", null);
            alarm_logger.AppendChild(declaration);
            XmlElement parent_alarms = alarm_logger.CreateElement("alarms");
            parent_alarms.AppendChild(alarm_logger.CreateElement("pH"));
            parent_alarms.AppendChild(alarm_logger.CreateElement("Cl2"));
            parent_alarms.AppendChild(alarm_logger.CreateElement("NH3"));
            alarm_logger.AppendChild(parent_alarms);
            alarm_logger.Save(ALARM_LOG_FILE_PATH);
        }


        [Route("alarm/postAlarm")]
        [HttpPost]
        public IHttpActionResult postAlarm()
        {
            temp_data.Load(Request.Content.ReadAsStreamAsync().Result);

            string str_sensorType = temp_data.SelectSingleNode("/alarm").Attributes["type"].Value;
            
            switch (str_sensorType)
            {
                case "PH":
                    {
                        XmlElement list = (XmlElement)alarm_logger.SelectSingleNode("/alarms/pH");
                        list.AppendChild(alarm_logger.ImportNode(temp_data.SelectSingleNode("/alarm"), true));
                    }
                    break;
                case "NH3":
                    {
                        XmlElement list = (XmlElement)alarm_logger.SelectSingleNode("/alarms/NH3");
                        list.AppendChild(alarm_logger.ImportNode(temp_data.SelectSingleNode("/alarm"), true));

                    }
                    break;
                case "CI2":
                    {
                        XmlElement list = (XmlElement)alarm_logger.SelectSingleNode("/alarms/Cl2");
                        list.AppendChild(alarm_logger.ImportNode(temp_data.SelectSingleNode("/alarm"), true));

                    }
                    break;
            }
            alarm_logger.Save(ALARM_LOG_FILE_PATH);
            
            return Ok();
        }

    }
}
