using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartH2O_Service.Models
{
    public class Alarm
    {
        public string type { get; set; }
        public int value { get; set; }
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public int second { get; set; }
    }
}
