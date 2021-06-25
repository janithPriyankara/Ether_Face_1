using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


//metroFrameWork

//Serial Port
using System.Threading;
using System.IO.Ports;


//XML
using System.IO;


//network


namespace EtherFACE1
{
    public partial class cformEtherFace : Form
    {

        public Thread ReadSerialDataThread;
        public string readSerialValue;
        public byte[] readSerialBytes;

        public string xmlread;


        //SerialPort Object
        public static System.IO.Ports.SerialPort port_considered;


        private byte[] Data = new byte[5];
        private string rx_data = String.Empty;
        private string port_name = String.Empty;
        private int baud_rate = 4800;

        public string sentHistory = "";
        OpenFileDialog openFileDialog1 = new OpenFileDialog();


        bool timer1State = true;


        public cformEtherFace()
        {
            InitializeComponent();
        }

        private void cformEtherFace_Load(object sender, EventArgs e)
        {
            tabClose();
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

        //XML file operations
        #region
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
        #endregion   




        private void label9_Click(object sender, EventArgs e)
        {

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
                        MessageBox.Show("Writing Failure..!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }



        //Port Selection  And Configureration
        #region

        public void LoadSequence()
        {
            pORTcomboboxmenuitem.Items.Clear();
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
        } //Load port list

        private void pORTComboBox_selectedIndexChanged(object sender, EventArgs e) //make selected port
        {
            // making new port using selected port and baudRate
            port_name = pORTcomboboxmenuitem.SelectedItem.ToString();
            //baud_rate = Convert.ToInt32(baud_rate);

            port_considered = new SerialPort(port_name, baud_rate);

            port_considered.ReadBufferSize = 8192;
            port_considered.WriteBufferSize = 8192;

        }

        private void radioButtonOn_click(object sender, EventArgs e) // turning On the selected Port
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


        private void timer_tick(object sender, EventArgs e)
        {
            if (timer1State)
            {

                this.radioButtonOn.BackColor = Color.LimeGreen;
            }
            else
            {
                this.radioButtonOn.BackColor = Color.SkyBlue;
            }

            timer1State = !timer1State;
        } // Background Timer for 

        #endregion



        public static byte[] FramGenerator(string headersMain, string task, string data, bool endstate)
        {

            byte _frameHeader;

            int lenghtOfBytearray;
            int extend = 0;

            byte[] bytes;


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
            if (data == null)
            {
                lenghtOfBytearray =  3 + extend;
                var Frame = new byte[lenghtOfBytearray];

                Frame[0] = preheader1;
                Frame[1] = preheader2;
                Frame[2] = _frameHeader;

                if (endstate)
                {
                    Frame[lenghtOfBytearray - 1] = 0xff;
                }

                return Frame;

            }
            else {
                bytes = Encoding.ASCII.GetBytes(data);
                lenghtOfBytearray = bytes.Length + 3 + extend;

                var Frame = new byte[lenghtOfBytearray];

                Frame[0] = preheader1;
                Frame[1] = preheader2;
                Frame[2] = _frameHeader;

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

            // making new frame
        }


        public static byte[] FramGenerator(byte[] bytes)
        {

            int lenghtOfBytearray;

            byte preheader1 = 0xAA;
            byte preheader2 = 0xAB;

          
            lenghtOfBytearray = bytes.Length + 2;

            var Frame = new byte[lenghtOfBytearray];

            Frame[0] = preheader1;
            Frame[1] = preheader2;

            for (int i = 2; i < lenghtOfBytearray; i++)
               {
                   Frame[i] = bytes[i - 2];
               }
                
                return Frame;
            
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



        // Reading as Characters
        #region

        /*
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
 * 
 * 
 *       public delegate void ShowSerialDatadelegate(string r);
 *
 *
 *
            private void ShowSerialData(string s)
            {
            if (richTextBoxXmlEditor.InvokeRequired)
            {
                ShowSerialDatadelegate SSDD = ShowSerialData;
                Invoke(SSDD, s);
            }
            else
            {
                richTextBoxReciever.AppendText(Environment.NewLine+s);
                //MessageBox.Show(s);

            }
        }

 * 
 * 
 *             */
        #endregion

        // Reading byte-wise


        public int bytestoreadValue;

        private void ReadSerial() //Serial Reading iSP
        {
            while (port_considered.IsOpen)
            {
                bytestoreadValue = port_considered.BytesToRead;
                readSerialBytes = new byte[bytestoreadValue];

                if (bytestoreadValue > 0)
                {
                    port_considered .Read(readSerialBytes, 0, bytestoreadValue);  

                    ShowSerialData(readSerialBytes);
                }

            }
        }


        // Read event generation
        public delegate void ShowSerialDatadelegate(byte[] r);

        private void ShowSerialData(byte[] s)
        {
            if (richTextBoxXmlEditor.InvokeRequired)
            {
                ShowSerialDatadelegate SSDD = ShowSerialData;
                Invoke(SSDD, s);
            }
            else
            {
                //FrameDecoder(s.ToList());

                //                foreach (var item in s) {
                //                  richTextBoxReciever.AppendText(Environment.NewLine + item);
                //
                //             }


                string income = "";

                foreach (var item in s) {
                    income += item;

                }

                MessageBox.Show(income);


            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="InFrame"></param>
        public static void FrameDecoder(List<byte> InFrame) {

            int preHeader1Index;
            int preHeader2Index;

            byte header;//main categories
            byte task;//task of selected category

            var payload = new List<int>();

            preHeader1Index = InFrame.IndexOf(0xAA);
            preHeader2Index = InFrame.IndexOf(0xAB);

            if ( (preHeader1Index >= -1) & (preHeader2Index >= -1) ) {

                //Not corrupted frame
                if ((preHeader2Index-preHeader1Index) ==1) {
                    if ((InFrame[preHeader2Index + 1] == 0x10) && (InFrame[preHeader2Index + 1] == 0x06)) {



                    }


                }
                else {
                    MessageBox.Show("Data is corrupted, couldn't recieve the fgrame header!");
                }


                //Sending intger values of bytes because no need of bytes to process
                for (int i = 3;i<InFrame.Count;i++) {
                    payload[i-3] = InFrame[i];
                }
                
                
                /*
                header = Convert.ToByte(InFrame[preHeader2Index + 1] >> 4);
                task = Convert.ToByte(header & 0x0F);

                switch (header) {

                    case 0x02: //Register read local memory space
                        break;

                    case 0x0A: //Register read user memory space
                        break;

                    case 0x03: //Read triggered time of digital input
                        break;

                    case 0x04: //Digital input read
                        break;

                    case 0x06: //Analog input read
                        break;

                    case 0x08: //I2C read
                        XMLOperation(task,payload.ToArray());
                        break;

                    case 0x0C: //XML
                        break;

                    case 0x0E: //Control Commands
                        break;

                    case 0x0F: //Emergency
                        break;


                    default:  //Reserved
                        break;


                }*/
            }


        }

        public static void XMLOperation(byte task, int[] xmlData)
        {
            switch (task)
            {
                case 0x01:  //Read(send the requested XML file data)
                    break;

                case 0x02:  //Load(Send the name of the active XML file)
                    break;

                case 0x04:  //Read Available XML names (send all XML file names in the master)
                    break;

                case 0x06:  //Delete success (delete this file success)  
                    break;

                case 0x0E:  //Delete unsuccess (delete this file unsuccess)
                    break;

                case 0x05:  //Set success (successfully set this file as the active XML)
                    break;

                case 0x0D:  //Set unsuccess (unsuccessfully set this file as the active XML)
                    break;

                default:    // Others
                    break;
                    
            }
        }










        private void buttonRunning_Click(object sender, EventArgs e)
        {
            writeData(FramGenerator("xml", "running", null, false));
            labelMessenger.Text = "RUNNING XML IS FETCHED ";
        }

        private void buttonStorage_Click(object sender, EventArgs e)
        {
            writeData(FramGenerator("xml", "storage", richTextBoxFileName.Text, false));
            labelMessenger.Text = "STORAGE HAS BEEN LOADED";
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            if (richTextBoxFileName.TextLength!=10) {
                MessageBox.Show("please ennter a Valid XML file Name !");

            }
            else {
                writeData(FramGenerator("xml", "read", richTextBoxFileName.Text, false));
                labelMessenger.Text = "DONE READING";
            }
        }
            
        private void buttonActivate_Click(object sender, EventArgs e)
        {
            if (richTextBoxFileName.TextLength != 10)
            {
                MessageBox.Show("please ennter a Valid XML file Name !");
            }
            else
            {
                writeData(FramGenerator("xml", "activate", richTextBoxFileName.Text, false));
                labelMessenger.Text = "DONE ACTIVATING";
            }

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (richTextBoxFileName.TextLength != 10)
            {
                MessageBox.Show("please ennter a Valid XML file Name !");
            }
            else
            {
                writeData(FramGenerator("xml", "delete", richTextBoxFileName.Text, false));
                labelMessenger.Text = "DONE DELETING";
            }
        }

        private void buttonXmlStore_Click(object sender, EventArgs e)
        {
            if (richTextBoxFileName.TextLength != 10)
            {
                MessageBox.Show("please ennter a Valid XML file Name !");
            }
            else
            {
                writeData(FramGenerator("xml", "store", richTextBoxFileName.Text + richTextBoxXmlEditor.Text, true));
                labelMessenger.Text = "DONE STORING";

            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {



        }



        //Tab Opearions...................................................................................

        #region
        //This will only show the selected tabs
        public void tabShow(Panel showOnly)
        {

            Panel[] panelList = { panelNetwork, panelXml, panelBasicFeatures, panelGPIOOperation, panelMotorOp, panelGCodeImp };

            foreach (var item in panelList)
            {
                item.Visible = false;
            }

            showOnly.Visible = true;
            showOnly.Location = new Point(305, 43);
        }

        //This will Close the selected tabs
        public void tabClose()
        {

            Panel[] panelList = { panelNetwork, panelXml, panelBasicFeatures, panelGPIOOperation, panelMotorOp, panelGCodeImp };

            foreach (var item in panelList)
            {
                item.Visible = false;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            tabShow(panelGPIOOperation );
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabShow(panelXml);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            tabShow(panelGCodeImp);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabShow(panelNetwork);
            
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            
            tabShow(panelBasicFeatures);

        }

        private void button6_Click(object sender, EventArgs e)
        {

            tabShow(panelMotorOp);
        }

        private void pictureBoxBasicfclosed_Click(object sender, EventArgs e)
        {
            panelBasicFeatures.Visible = false;
        }

        private void pictureBoxGPIOclose_Click(object sender, EventArgs e)
        {
            panelGPIOOperation.Visible = false;
        }

        private void pictureBoxGcodeclose_Click(object sender, EventArgs e)
        {
            panelGCodeImp.Visible = false;
        }

        private void pictureBoxMotoropclose_Click(object sender, EventArgs e)
        {
            panelMotorOp.Visible = false;
        }

        private void pictureBoxXMLclose_Click(object sender, EventArgs e)
        {
            panelXml.Visible = false;
        }

        private void pictureBoxNetmonClose_Click(object sender, EventArgs e)
        {
            panelNetwork.Visible = false;
        }

        #endregion







        private void radioButtonOff_CheckedChanged(object sender, EventArgs e)
        {
            if (port_considered != null)
            {
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

            this.radioButtonOn.BackColor = Color.SkyBlue;
        }




        // Basic features..................................................................................
        #region

        private void buttonBootOn_Click(object sender, EventArgs e)
        {
            writeData(FramGenerator(new byte[] {0x64,0x00}));
        }

        private void buttonSyncOn_Click(object sender, EventArgs e)
        {
            writeData(FramGenerator(new byte[] { 0x65, 0x00 }));
        }

        private void buttonSyncOff_Click(object sender, EventArgs e)
        {
            writeData(FramGenerator(new byte[] { 0x66, 0x00 }));
        }



        #endregion



        //GPIO Operations..................................................................................
        #region

        // this will  update the  information of slave address textboxes with the data obtained fromthe combobox 
        private void comboBoxSlaveAddr_SelectedIndexChanged(object sender, EventArgs e)
        {
            int slaveaddr = Convert.ToInt32(comboBoxSlaveAddr.Text);
            MessageBox.Show(slaveaddr.ToString());
            AddressUpdater(slaveaddr);


        }

        private void AddressUpdater(int indexAddr)
        {
            var AddressList = new List<TextBox>() { textBoxAddr0, textBoxAddr1, textBoxAddr2, textBoxAddr3, textBoxAddr4, textBoxAddr5, textBoxAddr6, textBoxAddr7 };
            foreach (var item in AddressList)
            {
                item.Text = "0";
            }
            AddressList[indexAddr - 0].Text = "1";
        }


        // This will convert text inputs to address
        private byte AddressCalculator()
        {
            try
            {
                int intaddr = (Convert.ToInt32(textBoxAddr7.Text)) * 128 +
                             (Convert.ToInt32(textBoxAddr6.Text)) * 64 +
                             (Convert.ToInt32(textBoxAddr5.Text)) * 32 +
                             (Convert.ToInt32(textBoxAddr4.Text)) * 16 +
                             (Convert.ToInt32(textBoxAddr3.Text)) * 8 +
                             (Convert.ToInt32(textBoxAddr2.Text)) * 4 +
                             (Convert.ToInt32(textBoxAddr1.Text)) * 2 +
                             (Convert.ToInt32(textBoxAddr0.Text)) * 1;
                return Convert.ToByte(intaddr);
            }
            catch (System.FormatException)
            {

                MessageBox.Show("Please re run the application and Enter a binary address! \nDo not leave the Slave address empty! ");
                return 0x00;
            }

        }

        // GP write operations
        #region
        static byte outputByte = 0x00;

        // will flip a selected bit from a selectyed byte
        private byte bitFlipper(int index, byte whichByte)
        {
            byte res = Convert.ToByte((whichByte & ~(1 << index)) | ((~whichByte) & (1 << index)));
            //Console.WriteLine(res);
            WriteLights(res);
            return res;
        }


 

        //This will handle the radiobuttons for writing purpose
        private void WriteLights(byte light) {

            radioButtonWr0.Checked = false;
            radioButtonWr1.Checked = false;
            radioButtonWr2.Checked = false;
            radioButtonWr3.Checked = false;
            radioButtonWr4.Checked = false;
            radioButtonWr5.Checked = false;
            radioButtonWr6.Checked = false;
            radioButtonWr7.Checked = false;

            if (((light & 0x01) >> 0) == 1)
            {

                radioButtonWr0.Checked = true;
            }
            if (((light & 0x02) >> 1) == 1)
            {

                radioButtonWr1.Checked = true;
            }
            if (((light & 0x04) >> 2) == 1)
            {

                radioButtonWr2.Checked = true;
            }
            if (((light & 0x08) >> 3) == 1)
            {

                radioButtonWr3.Checked = true;
            }
            if (((light & 0x10) >> 4) == 1)
            {

                radioButtonWr4.Checked = true;
            }
            if (((light & 0x20) >> 5) == 1)
            {

                radioButtonWr5.Checked = true;
            }
            if (((light & 0x40) >> 6) == 1)
            {

                radioButtonWr6.Checked = true;
            }
            if (((light & 0x80) >> 7) == 1)
            {

                radioButtonWr7.Checked = true;
            }
        }

        //Set of functions to make the GP Output byte
        #region 
        private void buttonGPO0_Click(object sender, EventArgs e)
        {
            outputByte = bitFlipper(0, outputByte);
        }

        private void buttonGPO1_Click(object sender, EventArgs e)
        {
            outputByte = bitFlipper(1, outputByte);
        }

        private void buttonGPO2_Click(object sender, EventArgs e)
        {
            outputByte = bitFlipper(2, outputByte);
        }

        private void buttonGPO3_Click(object sender, EventArgs e)
        {
            outputByte = bitFlipper(3, outputByte);
        }

        private void buttonGPO4_Click(object sender, EventArgs e)
        {
            outputByte = bitFlipper(4, outputByte);
        }

        private void buttonGPO5_Click(object sender, EventArgs e)
        {
            outputByte = bitFlipper(5, outputByte);
        }

        private void buttonGPO6_Click(object sender, EventArgs e)
        {
            outputByte = bitFlipper(6, outputByte);
        }

        private void buttonGPO7_Click(object sender, EventArgs e)
        {
            outputByte = bitFlipper(7, outputByte);
        }

        #endregion

        //GP output write operation
        private void buttonGPOWrite_Click(object sender, EventArgs e)
        {            
            writeData(FramGenerator(new byte[] { 0x10, 0x0A, AddressCalculator(), 0xff, outputByte, 0xc0 }));
            labelGPIO.Text = "Writing succesful...";
        }
        #endregion

        
        



        private void buttonGPIRead_Click(object sender, EventArgs e)
        {
            writeData(FramGenerator(new byte[] { 0x10, 0x02, AddressCalculator(), 0xff, 0x55, 0xc0 }));

        }







        #endregion

    
        private void pictureBoxBasicfminimize_Click(object sender, EventArgs e)
        {
            
        }





        //Motor Operations..................................................................................
        private void buttonMotorWrite_Click(object sender, EventArgs e)
        {

            int pulsemotor1 = Convert.ToInt32(textBoxMotor1.Text);
            byte firstByte1 = Convert.ToByte((pulsemotor1 & 0x0f00) >> 16);
            byte secondByte1 = Convert.ToByte((pulsemotor1 & 0x00f0) >> 8);
            byte thirdByte1 = Convert.ToByte((pulsemotor1 & 0x000f));


            int pulsemotor2 = Convert.ToInt32(textBoxMotor2.Text);
            byte firstByte2 = Convert.ToByte((pulsemotor2 & 0x0f00) >> 16);
            byte secondByte2 = Convert.ToByte((pulsemotor2 & 0x00f0) >> 8);
            byte thirdByte2 = Convert.ToByte((pulsemotor2 & 0x000f));

            if (pulsemotor1 < 0) {
                writeData(FramGenerator(new byte[] { 0x10, 0x0B, 0x13, 0x08, 0x01, firstByte1, secondByte1, thirdByte1, 0x04, firstByte2, secondByte2, thirdByte2, 0x00 }));
            }
            else
            {
                writeData(FramGenerator(new byte[] { 0x10, 0x0B, 0x13, 0x08, 0x01, firstByte1, secondByte1, thirdByte1, 0x04, firstByte2, secondByte2, thirdByte2, 0x00 }));
            }
            if (pulsemotor2 < 0) {
                writeData(FramGenerator(new byte[] { 0x10, 0x0B, 0x13, 0x08, 0x01, firstByte1, secondByte1, thirdByte1, 0x04, firstByte2, secondByte2, thirdByte2, 0x00 }));
            }
            else
            {
                writeData(FramGenerator(new byte[] { 0x10, 0x0B, 0x13, 0x08, 0x01, firstByte1, secondByte1, thirdByte1, 0x04, firstByte2, secondByte2, thirdByte2, 0x00 }));
            }

        }



    }
}




    

