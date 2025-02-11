using MindFusion.Charting;
using Opc.Ua.Client;
using Opc.UaFx;
using Opc.UaFx.Client;
using OpcLabs.EasyOpc.UA.Toolkit.ClientServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace winform
{
    public partial class Form1 : Form
    {
        Timer myTimer = new Timer();

        Timer myTimer2 = new Timer();
        double thickness;

        MyDateTimeSeries series1;
        public Form1()
        {
            InitializeComponent();
            series1 = new MyDateTimeSeries(DateTime.Now, DateTime.Now, DateTime.Now.AddMinutes(1));
            series1.DateTimeFormat = DateTimeFormat.LongTime;
            series1.LabelInterval = 10;
            series1.MinValue = -1000;
            series1.MaxValue = 1000;
            series1.Title = "Date Test";
            series1.SupportedLabels = LabelKinds.XAxisLabel;
            lineChart1.Series.Add(series1);

            lineChart1.ShowLegend = false;
            lineChart1.ShowLegendTitle = false;
            lineChart1.ShowXCoordinates = false;
            lineChart1.XAxis.Title = "";

            myTimer.Tick += MyTimer_Tick;
            myTimer.Interval = 500;
            myTimer.Start();

            myTimer2.Tick += pictureBox1_BackgroundImageChanged;
            myTimer2.Interval = 500;
            myTimer2.Start();
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            if (series1.Size > 1)
            {
                double span = series1.GetValue(series1.Size - 1, 0);
                lineChart1.XAxis.MinValue += span;
                lineChart1.XAxis.MaxValue += span;
            }
            lineChart1.ChartPanel.InvalidateLayout();
        }

        private void ClientUrl_lbl_Click(object sender, EventArgs e)
        {

        }
        public void HandleDataChanged(object sender, OpcDataChangeReceivedEventArgs e)
        {

            OpcMonitoredItem item = (OpcMonitoredItem)sender;
            ReadDta_txt.Text = $"{item.NodeId}: {e.Item.Value}";
            double val = double.Parse(e.Item.Value.ToString());
            thickness = val;
            series1.addValue(val);
            SqlInsert(val);
        }

        private void Connect_btn_Click_1(object sender, EventArgs e)
        {
            //Declaration 
            //Server URL
            string OPC_URL = "opc.tcp://192.168.0.10:4840";

            //NodeID    
            var NODE_ID = "ns=4;s=DataTest";

            var APP_CLIENT = new OpcClient(OPC_URL);

            //Connect
            APP_CLIENT.Connect();

            //Show SERVER URL
            ServerUrl_lbl.Text = OPC_URL.ToString();

            //Client subcription
            OpcSubscription DataTest_SUBDATA = APP_CLIENT.SubscribeDataChange(NODE_ID, HandleDataChanged);
            DataTest_SUBDATA.PublishingInterval = 2000;
            DataTest_SUBDATA.ApplyChanges();
        }


        private void pictureBox1_BackgroundImageChanged(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "C:\\KEYENCE\\KVN Tech - ONE TEAM - 3. Tyson\\2H2024\\Application\\winform\\FTP\\00000.jpeg";
            pictureBox1.Refresh();
            Application.DoEvents();
        }


        private void SqlInsert(double val)
        {
            string connstring = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(@connstring);

            string query = "INSERT INTO GT_Thickness VALUES(@Thickness, @DateTime)";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Thickness", val);
            cmd.Parameters.AddWithValue("@DateTime", myTimer.ToString());

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show($"Value:  {val.ToString()}");
            }
            catch (SqlException i)
            {
                Console.WriteLine("Error Generated. Details: " + i.ToString());
            }
            finally
            {
                conn.Close();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.GT_Thickness' table. You can move, or remove it, as needed.
            this.gT_ThicknessTableAdapter.Fill(this.database1DataSet.GT_Thickness);

        }
    }
}
