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

using System.Security;



namespace EtherFACE1
{
    public partial class cformEtherFace : Form
    {

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
            int no_of_available_ports = 0;


/*
            pORTToolStripMenuItem.
            // reset the port combo
            metroComboBoxPortList.Text = "";
            metroComboBoxPortList.Items.Clear();


            //getting avialble ports
            no_of_available_ports = SerialPort.GetPortNames().Length;


            //
            if (no_of_available_ports == 0)
            {
                metroComboBoxPortList.Text = "No Ports Online, Sorry";
            }
            else// having more than zero ports
            {
                //selecting port
                metroComboBoxPortList.Enabled = true;


                //selecting baud rate
                metroComboBoxBaudRate.Enabled = true;
                metroButtonBaudRateSelection.Enabled = true;

                //writing functionality
                metroButtonWrite.Enabled = true;
                metroTextBoxWrite.Enabled = true;


                if (no_of_available_ports == 1)
                {
                    metroComboBoxPortList.Text = "One Port Online! ";
                    metroComboBoxPortList.Items.AddRange(SerialPort.GetPortNames());
                }
                else
                {
                    metroComboBoxPortList.Text = String.Format("{0} Ports Online", no_of_available_ports.ToString());
                    metroComboBoxPortList.Items.AddRange(SerialPort.GetPortNames());
                }

    
            }  */
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

        private void buttonXmlStore_Click(object sender, EventArgs e)
        {

        }

        private void communicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }
    }


}


    

