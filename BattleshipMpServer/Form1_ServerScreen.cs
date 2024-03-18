using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace BattleshipMp
{
    public partial class Form1_ServerScreen : Form
    {

        public Form1_ServerScreen()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;

            textBoxIpAddress.Text = FillIpTextBox();
        }

        string FillIpTextBox()
        {
            string ipadr = "127.0.0.1";
            return ipadr;
        }

        private void textBoxIpAddress_Leave(object sender, EventArgs e)
        {
            //if (textBoxIpAddress.Text != "127.0.0.1" && textBoxIpAddress.Text != FillIpTextBox())
            //{
            //    textBoxIpAddress.Text = FillIpTextBox();
            //}
        }

        private void buttonServerStart_Click(object sender, EventArgs e)
        {
            Server.ServerStart(textBoxIpAddress.Text, textBoxPort.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Server.client != null && Server.client.Connected)
            {
                labelServerState.Text = "Giocatore connesso.";
                buttonGoToBoard.Enabled = true;
            }
            else if (Server.listener != null)
            {
                labelServerState.Text = "SERVER AVVIATO! Il giocatore sta aspettando...";
            }
            else
            {
                labelServerState.Text = "Attendendo il SERVER...";
            }
        }

        private void buttonGoToBoard_Click(object sender, EventArgs e)
        {

            timer1.Stop();

            Form2_PreparatoryScreen frm2 = new Form2_PreparatoryScreen();
            frm2.Show();
            this.Visible = false;
        }

        private void Form1_ServerScreen_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Form1_ServerScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Server.client.Close();
            Server.client.Dispose();
            Server.client = null;

            Server.listener.Stop();
            Server.listener = null;

            Environment.Exit(1);
        }
    }
}
