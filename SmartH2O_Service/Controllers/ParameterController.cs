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
    public class ParameterController : ApiController
    {
        private XmlDocument parameter_logger;
        private XmlDocument temp_data;
        public static String PARAM_LOG_FILE_PATH = AppDomain.CurrentDomain.BaseDirectory.ToString() + "param-data.xml";

        public ParameterController()
        {
            initXmlFile();
            temp_data = new XmlDocument();
        }

        private void initXmlFile()
        {
            if (!File.Exists(PARAM_LOG_FILE_PATH))
            {
                createBasicParamXMLFile();
            }
            else
            {
                parameter_logger = new XmlDocument();
                parameter_logger.Load(PARAM_LOG_FILE_PATH);
            }
        }

        private void createBasicParamXMLFile()
        {
            parameter_logger = new XmlDocument();
            XmlDeclaration declaration = parameter_logger.CreateXmlDeclaration("1.0", "utf-8", null);
            parameter_logger.AppendChild(declaration);
            XmlElement parent_paramenter = parameter_logger.CreateElement("parameters");
            parent_paramenter.AppendChild(parameter_logger.CreateElement("pH"));
            parent_paramenter.AppendChild(parameter_logger.CreateElement("Cl2"));
            parent_paramenter.AppendChild(parameter_logger.CreateElement("NH3"));
            parameter_logger.AppendChild(parent_paramenter);
            parameter_logger.Save(PARAM_LOG_FILE_PATH);
        }


        // Routing 
        [Route("parameter/postEntry")]
        [HttpPost]
        public IHttpActionResult postParameter()
        {
            temp_data.Load(Request.Content.ReadAsStreamAsync().Result);
            String str_sensorType = temp_data.SelectSingleNode("/sensor").Attributes["type"].Value;
            
            switch (str_sensorType)
            {
                case "PH":
                    {
                        XmlElement list = (XmlElement)parameter_logger.SelectSingleNode("/parameters/pH");
                        list.AppendChild(parameter_logger.ImportNode(temp_data.SelectSingleNode("/sensor/entry"), false));
                    }
                    break;
                case "NH3":
                    {
                        XmlElement list = (XmlElement)parameter_logger.SelectSingleNode("/parameters/NH3");
                        list.AppendChild(parameter_logger.ImportNode(temp_data.SelectSingleNode("/sensor/entry"), false));
                    }
                    break;
                case "CI2":
                    {
                        XmlElement list = (XmlElement)parameter_logger.SelectSingleNode("/parameters/Cl2");
                        list.AppendChild(parameter_logger.ImportNode(temp_data.SelectSingleNode("/sensor/entry"), false));
                    }
                    break;
            }
            parameter_logger.Save(PARAM_LOG_FILE_PATH);
            
            return Ok("test");
        }

        [Route("parameter/{type}/{day}/{month}/{year}")]
        public IHttpActionResult getHourSummarizedParameterInfo(string type, int day, int month, int year) {
            return Ok("test");
        }

        [Route("parameter/{type}/{day1}/{month1}/{year1}/{day2}/{month2}/{year2}")]
        public IHttpActionResult getDailySummarizedParameterInfoBetweenDates(string type, int day, int month, int year ,int day2, int month2, int year2)
        {
            return Ok("test");
        }

        [Route("parameter/{type}/{week}")]
        public IHttpActionResult getWeeklySummarizedParameterInfo(string type, int week)
        {
            return Ok("test");
        }
    }
}
