using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartH2O_SeeApp
{
    public partial class SeeAppForm1 : Form
    {
        private WebClient webClient;
        private static string serviceReference = "http://localhost:55959/";
        public SeeAppForm1()
        {
            InitializeComponent();
            webClient = new WebClient();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void buttonAlarmsBetweenDates_Click(object sender, EventArgs e)
        {

        }

        private void btnDailyStats_Click(object sender, EventArgs e)
        {

            webClient.Headers["Content-type"] = "application/xml";
            byte[] result = webClient.DownloadData(serviceReference + "parameter/pH/1/12/2016");
            string str_result = Encoding.UTF8.GetString(result);

        }

        private void checkBoxPH_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxNH3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxCI2_CheckedChanged(object sender, EventArgs e)
        {

        }
        
    }
}
