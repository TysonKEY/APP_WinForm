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
using System.Data.SqlTypes;
using System.Management;
using OpcLabs.BaseLib.Extensions.Internal;
using System.IO;
using System.Reflection;
using winform.Properties;
using Org.BouncyCastle.Utilities;
using System.Threading;
using Timer = System.Windows.Forms.Timer;

namespace winform
{
    public partial class Form1 : Form
    {
        Timer myTimer = new Timer();
        double gtdata;
        decimal maxValue;
        decimal minValue;
        MyDateTimeSeries series1;
        DataSet set = new DataSet();

        public static string OPC_URL = "opc.tcp://192.168.0.10:4840";
        string NODE_ID              = "ns=4;s=DataTest";
        string Bank0Limit_tag       = "ns=4;s=SetLimit";
        string Bank0HiSetting_tag   = "ns=4;s=Bank0HiSetting";
        string Bank0LoSetting_tag   = "ns=4;s=Bank0LoSetting";
        string TriggerCnt_tag       = "ns=4;s=TrigCnt";
        string IVStatus_tag         = "ns=4;s=IVStatus";
        string IVDate_tag           = "ns=4;s=IVDate";
        string sampling_rate        = "ns=4;s=SamplingRate";
        string second               = "ns=4;s=Second";

        OpcClient APP_CLIENT = new OpcClient(OPC_URL);
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

            //Liveview for default combobox
            target_cbbx.SelectedIndex = 0;
            starttime_picker.Value = endtime_picker.Value.AddHours(-1);
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
        public void HandleDataChanged(object sender, OpcDataChangeReceivedEventArgs e)
        {
            OpcMonitoredItem item = (OpcMonitoredItem)sender;
            double var;
            var = double.Parse(APP_CLIENT.ReadNode(NODE_ID, OpcAttribute.Value).ToString());
            series1.addValue(var);
            gtdata = var;
            if (target_cbbx.SelectedIndex == 0)
            {
                SqlConn();
            }
        }
        public void ImgChange(object sender, OpcDataChangeReceivedEventArgs e)
        {
            OpcMonitoredItem item = (OpcMonitoredItem)sender;
            if (target_cbbx.SelectedIndex == 0)
            {
                int delaymillisecond = 200;
                Thread.Sleep(delaymillisecond);
                IVImgUpdate();
            }

        }
        private void Connect_btn_Click_1(object sender, EventArgs e)
        {
            try
            {
                //Connect
                APP_CLIENT.Connect();

                //Show SERVER URL
                ServerUrl_lbl.Text = OPC_URL.ToString();

                //Client subcription
                OpcSubscription DataTest_SUBDATA = APP_CLIENT.SubscribeDataChange(second, HandleDataChanged);
                DataTest_SUBDATA.ApplyChanges();

                OpcSubscription IV_SUBDATA = APP_CLIENT.SubscribeDataChange(TriggerCnt_tag, ImgChange);
                IV_SUBDATA.ApplyChanges();

                samplingrate_updown.Value = (decimal.Parse(APP_CLIENT.ReadNode(sampling_rate, OpcAttribute.Value).ToString()))/10;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }
        private void IVImgUpdate()
        {
            var outPutDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));
            var iconPath = Path.Combine(outPutDirectory, "FTP\\00000.jpeg");
            string icon_path = new Uri(iconPath).LocalPath;
            byte[] imageBytes = File.ReadAllBytes(icon_path);
            pictureBox1.ImageLocation = icon_path.ToString();
            pictureBox1.Refresh();
            Application.DoEvents();
            try
            {
                string IVStatus = APP_CLIENT.ReadNode(IVStatus_tag, OpcAttribute.Value).ToString();
                string IVDate   = APP_CLIENT.ReadNode(IVDate_tag, OpcAttribute.Value).ToString();
                string ConnString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\KEYENCE\\KVN Tech - ONE TEAM - 3. Tyson\\2H2024\\Application\\winform\\DataTable.mdf\";Integrated Security=True";
                SqlConnection conn = new SqlConnection(@ConnString);
                string Insert_Query = "INSERT INTO Img(Date,Time,Img,Status,DateandTime) VALUES(@Date,@Time,@IconByte,@Status,@DateandTime)";
                SqlCommand cmd = new SqlCommand(Insert_Query, conn);

                string Select_Query = "Select TOP 10 * from IMG " +
                    "ORDER BY Id DESC";
                SqlCommand s_cmd = new SqlCommand(Select_Query, conn);

                DateTime currTime = DateTime.Now;
                cmd.Parameters.AddWithValue("@Date", currTime.ToShortDateString());
                cmd.Parameters.AddWithValue("@Time", currTime.ToString("HH:mm:ss"));
                cmd.Parameters.AddWithValue("@IconByte", imageBytes);
                cmd.Parameters.AddWithValue("@Status", IVStatus);
                cmd.Parameters.AddWithValue("@DateandTime", currTime.ToString("M/d/yyyy HH:mm:ss"));

                conn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(s_cmd);
                DataSet set = new DataSet();
                adapter.Fill(set);
                dataGridView2.DataSource = set.Tables[0];
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }
        private void SqlConn()
        {
            string ConnString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\KEYENCE\\KVN Tech - ONE TEAM - 3. Tyson\\2H2024\\Application\\winform\\DataTable.mdf\";Integrated Security=True";
            SqlConnection conn = new SqlConnection(@ConnString);

            string Insert_Query = "INSERT INTO GT(Date,Time,Thickness,Status,DateandTime) Values(@Date,@Time,@Thickness,@Status,@DateTime)";
            SqlCommand cmd = new SqlCommand(Insert_Query, conn);

            DateTime currTime = DateTime.Now;

            cmd.Parameters.AddWithValue("@Date", currTime.ToShortDateString());
            cmd.Parameters.AddWithValue("@Time", currTime.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("@Thickness", gtdata);
            cmd.Parameters.AddWithValue("@DateTime", currTime.ToString("M/d/yyyy HH:mm:ss"));

            decimal data = (decimal)gtdata;
            string status = " ";

            if ((minValue <= data) && (data <= maxValue))
            {
                status = "OK";
            }
            else if ((Math.Abs(minValue) + Math.Abs(maxValue)) == 0)
            {
                status = " ";
            }
            else if ((minValue > data) || (data > maxValue))
            {
                status = "NG";
            }
            cmd.Parameters.AddWithValue("@Status", status);
            conn.Open();
            cmd.ExecuteNonQuery();
            dataGridView1.DataSource = null;
            LiveView();
            conn.Close();
        }
        private void LiveView()
        {
            string ConnString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\KEYENCE\\KVN Tech - ONE TEAM - 3. Tyson\\2H2024\\Application\\winform\\DataTable.mdf\";Integrated Security=True";
            SqlConnection conn = new SqlConnection(@ConnString);
            string Select_Query = "Select TOP 5 * from GT " +
                "ORDER BY Id DESC";
            conn.Open();
            SqlCommand cmd = new SqlCommand(Select_Query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet set = new DataSet();
            adapter.Fill(set);
            FindMaxMin(set);
            dataGridView1.DataSource = set.Tables[0];
            conn.Close();
        }
        public void LoadData()
        {
            string ConnString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\KEYENCE\\KVN Tech - ONE TEAM - 3. Tyson\\2H2024\\Application\\winform\\DataTable.mdf\";Integrated Security=True";
            SqlConnection conn = new SqlConnection(@ConnString);
            string Select_Query = "Select top 1000 * from GT ";
            conn.Open();
            SqlCommand cmd = new SqlCommand(Select_Query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(set);
            FindMaxMin(set);
            dataGridView1.DataSource = set.Tables[0];
            conn.Close();
        }
        public void FindMaxMin(DataSet set)
        {
            DataTable dt = set.Tables[0];
            DataView dv = new DataView(dt);
            dv.RowFilter = "Thickness = Max(Thickness)";
            PrintDataViewMax(dv);
            dv.RowFilter = "Thickness = Min(Thickness)";
            PrintDataViewMin(dv);
            if (target_cbbx.SelectedValue == "DataView")
            {
                mean();
            }
        }
        public void PrintDataViewMax(DataView dv)
        {
            max_lbl.Text = dv[0]["Thickness"].ToString();
        }
        public void PrintDataViewMin(DataView dv)
        {
            min_lbl.Text = dv[0]["Thickness"].ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataTableDataSet.Img' table. You can move, or remove it, as needed.
            this.imgTableAdapter.Fill(this.dataTableDataSet.Img);
            // TODO: This line of code loads data into the 'dataTableDataSet1.GT' table. You can move, or remove it, as needed.
            try
            {
                this.gTTableAdapter.Fill(this.dataTableDataSet1.GT);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return; }

        }
        private void extract_btn_Click(object sender, EventArgs e)
        {
            string ConnString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\KEYENCE\\KVN Tech - ONE TEAM - 3. Tyson\\2H2024\\Application\\winform\\DataTable.mdf\";Integrated Security=True";
            SqlConnection conn = new SqlConnection(@ConnString);
            string GT_Query = "Select Date,Time,Thickness,Status from GT WHERE DateandTime between @start and @end ";

            string IV_Query = "Select Date,Time,Img,Status from Img WHERE DateandTime between @start and @end ";

            SqlCommand GT_cmd = new SqlCommand(GT_Query, conn);
            SqlCommand IV_cmd = new SqlCommand(IV_Query, conn);

            conn.Open();

            DateTime starttime = starttime_picker.Value;
            DateTime endtime = endtime_picker.Value;
            DateTime startdate = start_picker.Value;
            DateTime enddate = end_picker.Value;

            string start = startdate.ToShortDateString() + " " + starttime.ToString("HH:mm:ss");
            string end = enddate.ToShortDateString() + " " + endtime.ToString("HH:mm:ss");

            MessageBox.Show(start);

            GT_cmd.Parameters.AddWithValue("@start", start);
            GT_cmd.Parameters.AddWithValue("@end", end);

            SqlDataAdapter GTadapter = new SqlDataAdapter(GT_cmd);
            DataSet GTset = new DataSet();
            GTadapter.Fill(GTset);
            dataGridView1.DataSource = GTset.Tables[0];

            IV_cmd.Parameters.AddWithValue("@start", start);
            IV_cmd.Parameters.AddWithValue("@end", end);

            SqlDataAdapter IVadapter = new SqlDataAdapter(IV_cmd);
            DataSet IVset = new DataSet();
            IVadapter.Fill(IVset);
            dataGridView2.DataSource = IVset.Tables[0];

            conn.Close();
        }
        private async void apply_btn_Click(object sender, EventArgs e)
        {
            int delayMilliseconds = 500;
            
            if ((design_updown.Value + upper_updown.Value == 0) || (design_updown.Value + lower_updown.Value == 0))
            {
                MessageBox.Show("Please input tolerance!");
            }
            else
            {
                maxValue = (design_updown.Value + upper_updown.Value);
                minValue = (design_updown.Value + lower_updown.Value);
                upper_lbl.Text = maxValue.ToString();
                lower_lbl.Text = minValue.ToString();
                MessageBox.Show("Limit updated!");
            }
            APP_CLIENT.WriteNode(Bank0HiSetting_tag, Convert.ToDouble(maxValue));
            APP_CLIENT.WriteNode(Bank0LoSetting_tag, Convert.ToDouble(minValue));
            Thread.Sleep(delayMilliseconds);
            APP_CLIENT.WriteNode(Bank0Limit_tag, true);
            Thread.Sleep(delayMilliseconds);
            APP_CLIENT.WriteNode(Bank0Limit_tag, false);
        }
        public string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            foreach (char c in str)
            {
                count++;
                if ((c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        private void upper_updown_ValueChanged(object sender, EventArgs e)
        {
            lower_updown.Value = -upper_updown.Value;
        }
        //Calculation
        public void mean()
        {
            string ConnString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\KEYENCE\\KVN Tech - ONE TEAM - 3. Tyson\\2H2024\\Application\\winform\\DataTable.mdf\";Integrated Security=True";
            SqlConnection conn = new SqlConnection(@ConnString);


            string query = "Select SUM(Thickness) from GT";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            var count = (double)num_updown.Value;
            var maincount = 1000.00;

            if (observ_chck.Checked == true)
            {
                maincount = count;
            }

            var sum = Convert.ToDouble(cmd.ExecuteScalar());

            double mean = Math.Round(sum / maincount, 4);

            conn.Close();

            double val, p_variance = 0, variance, tuso = 0;
            for (int i = 1; i <= maincount; i++)
            {
                string ConString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\KEYENCE\\KVN Tech - ONE TEAM - 3. Tyson\\2H2024\\Application\\winform\\DataTable.mdf\";Integrated Security=True";
                SqlConnection con = new SqlConnection(@ConString);

                string query1 = "SELECT Thickness FROM (\r\n  SELECT\r\n    ROW_NUMBER() OVER (ORDER BY Id ASC) AS rownumber,\r\n    Thickness\r\n  FROM GT\r\n) AS foo\r\nWHERE rownumber = @count\r\n\r\n";
                SqlCommand cmd1 = new SqlCommand(query1, con);

                con.Open();
                cmd1.Parameters.AddWithValue("@count", i);
                val = Convert.ToDouble(cmd1.ExecuteScalar());
                p_variance = tuso + Math.Pow(val - mean, 2);
                con.Close();
            }
            variance = Math.Sqrt(p_variance / maincount);
            std_deviation_lbl.Text = variance.ToString();
        }
        private void update_btn_Click(object sender, EventArgs e)
        {
            mean();
        }
        private void target_cbbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            APP_CLIENT.Connected += APP_CLIENT_Conneceted;
            if ((target_cbbx.SelectedIndex == 1))
            {
                APP_CLIENT.Disconnected += APP_CLIENT_Disconnected;
                observ_chck.Enabled = true;
                DialogResult result = DialogResult.Yes;
                result = MessageBox.Show("Stop logging data", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                LoadData();
                extract_btn.Enabled = true;
                sts_lbl.ForeColor = Color.Red;
                sts_lbl.Text = "STOP";
            }
            else if ((target_cbbx.SelectedIndex == 0))
            {
                sts_lbl.ForeColor = Color.LimeGreen;
                sts_lbl.Text = "RUN";
                observ_chck.Enabled = false;
                extract_btn.Enabled = false;
            }
        }
        private void APP_CLIENT_Conneceted(object sender, EventArgs e)
        {
            MessageBox.Show("Connected OK", "Notification", MessageBoxButtons.OK);

        }
        private void APP_CLIENT_Disconnected(object sender, EventArgs e)
        {
            MessageBox.Show("Datalogging stopped. Proceed to data view", "Notification", MessageBoxButtons.OK);
        }
        private void observ_chck_CheckedChanged(object sender, EventArgs e)
        {
            if (observ_chck.Checked == true)
            {
                num_updown.Enabled = true;
            }
        }
        private void fillByToolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.gTTableAdapter.FillBy(this.dataTableDataSet1.GT);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void fillBy1ToolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.gTTableAdapter.FillBy1(this.dataTableDataSet1.GT);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Status")
            {
                if (e.Value != null)
                {
                    // Check for the string "OK" in the cell.
                    e.CellStyle.Font = new Font("Gill Sans", 12);
                    string stringValue = (string)e.Value;
                    stringValue = stringValue.ToLower();
                    if ((stringValue.IndexOf("ok") > -1))
                    {
                        e.CellStyle.ForeColor = Color.LimeGreen;
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                }
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            APP_CLIENT.Disconnect();
        }
        private void fillToolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.imgTableAdapter.Fill(this.dataTableDataSet.Img);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void dataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            IVImg.Location = new Point(65, 20);
            string ConnString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\KEYENCE\\KVN Tech - ONE TEAM - 3. Tyson\\2H2024\\Application\\winform\\DataTable.mdf\";Integrated Security=True";
            SqlConnection conn = new SqlConnection(@ConnString);

            string query = "Select Img from Img where Id = @id";
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                if (dataGridView2.SelectedRows.Count != 0)
                {
                    DataGridViewRow row = dataGridView2.SelectedRows[0];
                    IVImg.Image = System.Drawing.Image.FromStream(new MemoryStream((byte[])row.Cells[2].Value));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void IVImg_Click(object sender, EventArgs e)
        {
            IVImg.Location = new Point(-1000, -1000);
        }
        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name == "Status")
            {
                // Check for the string "OK" in the cell.
                e.CellStyle.Font = new Font("Gill Sans", 12);
                string stringValue = (string)e.Value;
                stringValue = stringValue.ToLower();
                if ((stringValue.IndexOf("ok") > -1))
                {
                    e.CellStyle.ForeColor = Color.LimeGreen;
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
        }

        private void samplingrate_updown_ValueChanged(object sender, EventArgs e)
        {
            decimal rate;
            rate = samplingrate_updown.Value * 10;
            APP_CLIENT.WriteNode(sampling_rate, Convert.ToInt16(rate));
        }
    }
}
