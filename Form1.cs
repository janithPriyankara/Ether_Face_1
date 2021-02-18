using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//metroFrameWork
using MetroFramework.Forms;

//Serial Port
using System.IO.Ports;
using System.Threading;


//XML
using System.Xml;
using System.IO;


//network
using System.Timers;

using System.Security;



namespace EtherFACE1
{
    public partial class cformEtherFace : Form
    {

        public Thread ReadSerialDataThread;
        public string readSerialValue;
        public string readSerialBytes;

        public string xmlread;


        //SerialPort Object
        public static System.IO.Ports.SerialPort port_considered;


        private byte[] Data = new byte[5];
        private string rx_data = String.Empty;
        private string port_name = String.Empty;
        private int baud_rate = 0;

        public string sentHistory = "";
        OpenFileDialog openFileDialog1 = new OpenFileDialog();


        bool timer1State = true;


        public cformEtherFace()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void windowToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripSecondary_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void panelNetwork_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //visibility of Network Port
        private void networkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (networkToolStripMenuItem.Checked)
            {
                panelNetwork.Visible = true;
            }
            else
            {
                panelNetwork.Visible = false;
            }

        }

        private void pORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            


        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void xMLToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (xMLToolStripMenuItem3.Checked)
            {
                panelXml.Visible = true;
            }
            else
            {
                panelXml.Visible = false;
            }

        }

        private void newXMLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBoxXmlEditor.Text = null;
            labelActiveFileName.Text = "newFile.XMl";


        }

        private void openXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //XML Relating
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            int size = -1;
            string fileName;

            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                fileName = openFileDialog1.FileName;
                try
                {
                    String xmlread = File.ReadAllText(fileName);
                    size = xmlread.Length;

                    MessageBox.Show(String.Format("File opened Successfully ! \r\n\nLocation : {0} \r\n\n", fileName.ToString()));
                    richTextBoxXmlEditor.Text = xmlread;

                    labelActiveFileName.Text = Path.GetFileName(fileName);

                }
                catch (IOException)
                {
                    MessageBox.Show(String.Format("File opening Fail ! \r\n\nLocation : {0} \r\n\n", fileName.ToString()));

                }



            }
        }


        private void panelXml_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.FilterIndex = 2;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, richTextBoxXmlEditor.Text);
                    labelActiveFileName.Text = Path.GetFileName(sfd.FileName);

                }
            }
        }







        public void writeData(byte[] dataIn)
        {
            if (radioButtonOn.Enabled) { 
            try
            {
                    //if the port is closed--> turn on the port
                    if (port_considered.IsOpen == false)
                    {
                        port_considered.Open();
                    }

                    if (port_considered.IsOpen == true)
                    {
                        MessageBox.Show(dataIn.Length.ToString());

                        while (true)
                        {
                            port_considered.Write(dataIn, 0, dataIn.Length);
                            if (port_considered.BytesToWrite == 0)
                            {
                                break;
                            }
                        }
                        //metroTextBoxSent.AppendText(Environment.NewLine + dataIn);
                    }

                    else
                    {
                        MessageBox.Show("Storing Failure..!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    




        private void buttonXmlStore_Click(object sender, EventArgs e)
        {
            writeData(FramGenerator("xml", "store", metroTextRead.Text + xmlread, true));
        }



        private void communicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //clear the  drop down items
            LoadSequence();

            int no_of_available_ports = 0;

            //getting avialble ports
            no_of_available_ports = SerialPort.GetPortNames().Length;

            //Zero Ports
            if (no_of_available_ports == 0)
            {
                pORTcomboboxmenuitem.Items.Add("No Ports Online, Sorry");
            }
            else// having more than zero ports
            {
                //writing functionality
                foreach (var item in SerialPort.GetPortNames()) {
                    pORTcomboboxmenuitem.Items.Add(item);
                }
            }
        }


        


        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }


        private void pictureBox9_Click_1(object sender, EventArgs e)
        {

        }



        public static byte[] FramGenerator(string headersMain, string task, string data, bool endstate)
        {

            byte _frameHeader;

            int extend = 0;

            byte preheader1 = 0xAA;
            byte preheader2 = 0xAB;

            var headersMainList = new Dictionary<string, byte>() { };
            var taskHeadersXML = new Dictionary<string, byte>() { };


            //Main Headers
            headersMainList.Add("parallel", 0x01);
            headersMainList.Add("sequential", 0x02);
            headersMainList.Add("master", 0x03);
            headersMainList.Add("xml", 0x04);
            headersMainList.Add("control cmd", 0x06);
            headersMainList.Add("emergency", 0x07);

            //XML tasks
            taskHeadersXML.Add("store", 0x01);
            taskHeadersXML.Add("running", 0x02);
            taskHeadersXML.Add("read", 0x03);
            taskHeadersXML.Add("storage", 0x04);
            taskHeadersXML.Add("activate", 0x05);
            taskHeadersXML.Add("delete", 0x06);

            //header+task bytes
            _frameHeader = Convert.ToByte((headersMainList[headersMain] << 4) | (taskHeadersXML[task]));


            //end state flag
            if (endstate)
            {
                extend = 1;
            }
            else
            {
                extend = 0;
            }


            //taking bytes array of input data string
            byte[] bytes = Encoding.ASCII.GetBytes(data);
            int lenghtOfBytearray = bytes.Length + 3 + extend;

            // making new frame
            var Frame = new byte[lenghtOfBytearray];

            Frame[0] = preheader1;
            Frame[1] = preheader2;
            Frame[2] = _frameHeader;


            //assigning bytes to output Frame
            if (endstate)
            {
                Frame[lenghtOfBytearray - 1] = 0xff;

                for (int i = 3; i < lenghtOfBytearray - 1; i++)
                {
                    Frame[i] = bytes[i - 3];
                }
            }
            else
            {
                for (int i = 3; i < lenghtOfBytearray; i++)
                {
                    Frame[i] = bytes[i - 3];
                }
            }
            return Frame;
        }

        private void label9_Click_1(object sender, EventArgs e)
        {

        }

        private void noPortsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        public void LoadSequence() {
            pORTcomboboxmenuitem.Items.Clear();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        //Recieve thread operations
        private void readSerialData()
        {
            try
            {
                ReadSerialDataThread = new Thread(ReadSerial);
                ReadSerialDataThread.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ReadSerial()
        {
            while (port_considered.IsOpen)
            {
                if (port_considered.BytesToRead > 0)
                {
                    readSerialValue = port_considered.ReadExisting();
                    ShowSerialData(readSerialValue);
                }

            }
        }


        public delegate void ShowSerialDatadelegate(string r);

        private void ShowSerialData(string s)
        {
            if (richTextBoxXmlEditor.InvokeRequired)
            {
                ShowSerialDatadelegate SSDD = ShowSerialData;
                Invoke(SSDD, s);
            }
            else
            {
                richTextBoxXmlEditor.AppendText(s);
                //MessageBox.Show(s);

            }
        }

        private void noPortsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void pORTComboBox_selectedIndexChanged(object sender, EventArgs e)
        {
            // making new port using selected port and baudRate
            port_name = pORTcomboboxmenuitem.SelectedItem.ToString();
            baud_rate = Convert.ToInt32(4800);

            port_considered = new SerialPort(port_name, baud_rate);

        }

        private void radioButton_click(object sender, EventArgs e)
        {
            if (port_considered == null)
            {
                MessageBox.Show("Please Follow the correct Guide-Line !");
            }
            else
            {

                try
                {
                    if (port_considered.IsOpen == false)
                    {
                        port_considered.Open();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (port_considered.IsOpen)
                {
                    readSerialData();
                }
                timer1.Enabled = true;
                timer1.Interval = 1000;
                timer1.Start();
            }
        }


        private void radioButtonOff_clicked(object sender, EventArgs e)
        {
            if (port_considered!= null) {
                if (port_considered.IsOpen)
                {
                    port_considered.Close();
                    pORTcomboboxmenuitem.Items.Clear();
                }
                else
                {
                    MessageBox.Show("No port in Action !");
                }
                pORTcomboboxmenuitem.SelectedText = "";
                pORTcomboboxmenuitem.Items.Clear();
            }

            timer1.Enabled = false;
            this.pictureBox12.BackColor = Color.Blue;

        }


        private void timer_tick(object sender, EventArgs e)
        {
            if (timer1State)
            {

                this.pictureBox12.BackColor = Color.Crimson;
            }
            else
            {
                this.pictureBox12.BackColor = Color.Blue;
            }

            timer1State = !timer1State;
        }
    }
}




    

