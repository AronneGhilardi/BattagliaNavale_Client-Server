using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace BattleshipMp
{
    public partial class Form4_GameScreen : Form
    {
        public StreamReader STR;
        public StreamWriter STW;
        public string recieve;
        public string TextToSend;
        List<Button> gameBoardButtons;
        List<Button> myBoardButtons;
        bool areEnabledButtons = true;
        List<string> AllSelectedButtonList;
        bool myExit = false;

        public Form4_GameScreen(List<string> list)
        {
            InitializeComponent();
            this.AllSelectedButtonList = list;
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        /*/
        private void button_mousehover(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(new Bitmap(Application.StartupPath + @"\Images\target.png"), 20, 20);
            ((Button)sender).Cursor = new Cursor(bm.GetHicon());
        }
        private void button_mouseleave(object sender, EventArgs e)
        {
            ((Button)sender).Cursor = Cursors.Default;
        }
        /*/

        private void Form4_GameScreen_Load(object sender, EventArgs e)
        {
            gameBoardButtons = groupBox2.Controls.OfType<Button>().ToList();
            myBoardButtons = groupBox1.Controls.OfType<Button>().ToList();

            foreach (var item in AllSelectedButtonList)
            {
                groupBox1.Controls.Find(item, true)[0].BackColor = Color.DarkGray;
            }

            try
            {
                STR = new StreamReader(Server.client.GetStream());
                STW = new StreamWriter(Server.client.GetStream());
                STW.AutoFlush = true;
                backgroundWorker1.RunWorkerAsync();
                backgroundWorker2.WorkerSupportsCancellation = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            Random random = new Random();
            int rnd = random.Next(0, 2);
            if (rnd == 0)
            {
                areEnabledButtons = true;
                SwitchGameButtonsEnabled();
            }
            else
            {
                areEnabledButtons = false;
                SwitchGameButtonsEnabled();
            }
            AttackToEnemy(rnd.ToString());

            timer1.Start();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (Server.client.Connected)
            {
                try
                {
                    recieve = STR.ReadLine();

                    if (recieve != "")
                    {
                        AttackFromEnemy(recieve);
                    }
                    recieve = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void AttackFromEnemy(string recieve)
        {
            if (recieve == "0")
            {
                areEnabledButtons = true;
                SwitchGameButtonsEnabled();
                return;
            }
            else if (recieve == "1")
            {
                areEnabledButtons = false;
                SwitchGameButtonsEnabled();
                return;
            }

            else if (recieve.Contains("Acqua:"))
            {
                string result = recieve.Substring(recieve.Length - 2, 2);
                result = result + result.Substring(result.Length - 1);
                gameBoardButtons.FirstOrDefault(x => x.Name == result).BackgroundImage = Image.FromFile(Application.StartupPath + @".\o.png");
                richTextBox1.AppendText("Acqua!\n");
                return;
            }
            else if (recieve.Contains("Colpito:"))
            {
                string result = recieve.Substring(recieve.Length - 2, 2);
                result = result + result.Substring(result.Length - 1);
                gameBoardButtons.FirstOrDefault(x => x.Name == result).BackgroundImage = Image.FromFile(Application.StartupPath + @".\x.png");
                richTextBox1.AppendText("Colpito!\n");
                return;
            }

            else if (recieve.Contains("youwin"))
            {
                DialogResult res = MessageBox.Show("VITTORIA! Desideri tornare alla schermata di preparazione?", "Server - Game Result", MessageBoxButtons.YesNo);
                {
                    if (res == DialogResult.Yes)
                    {
                        myExit = true;
                        this.Close();
                    }
                    else
                        Environment.Exit(1);
                }
                return;
            }
            else if (recieve.Contains("exit"))
            {
                MessageBox.Show("\r\nL'avversario ha lasciato il gioco. Verrai indirizzato alla fase di preparazione.");
                this.Close();
                return;
            }

            bool isShot = false;
            string shotButtonName = "";
            string shottedShip = "";
            ShipButtons deletingButton = null;

            if (Form2_PreparatoryScreen.shipList[0].shipPerButton == null)
            {
                return;
            }
            foreach (var item1 in Form2_PreparatoryScreen.shipList)
            {
                foreach (var item2 in item1.shipPerButton)
                {
                    foreach (string item3 in item2.buttonNames)
                    {
                        if (item3 == recieve.Substring(0, recieve.Length - 1))
                        {

                            isShot = true;
                            shotButtonName = item3;
                            shottedShip = item1.shipName;
                            deletingButton = item2;
                            break;
                        }
                        else
                        {
                            string xxx = recieve.Substring(0, recieve.Length - 1);
                            shotButtonName = xxx;
                        }
                    }
                }
            }

            if (isShot)
            {
                myBoardButtons.FirstOrDefault(x => x.Name == shotButtonName).BackgroundImage = Image.FromFile(Application.StartupPath + @".\x.png");

                AttackToEnemy("Colpito:" + shotButtonName);

                deletingButton.buttonNames.Remove(shotButtonName);

                if (shottedShip == "Portaerei")
                {
                    foreach (var item in Form2_PreparatoryScreen.shipList.FirstOrDefault(x => x.shipName == "Portaerei").shipPerButton)
                    {
                        if (item.buttonNames.Count > 0)
                        {
                            return;
                        }
                        AttackToEnemy("youwin");
                        DialogResult res = MessageBox.Show("HAI PERSO! Vuoi tornare alla schermata di preparazione?", "Server - Game Result", MessageBoxButtons.YesNo);
                        {
                            if (res == DialogResult.Yes)
                            {
                                myExit = true;
                                this.Close();
                            }
                            else
                                Environment.Exit(1);
                        }
                        return;
                    }
                }
                return;
            }
            else
            {
                myBoardButtons.FirstOrDefault(x => x.Name == shotButtonName).BackgroundImage = Image.FromFile(Application.StartupPath + @".\o.png");
                AttackToEnemy("Acqua:" + shotButtonName);
                return;
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Server.client.Connected)
            {
                STW.WriteLine(TextToSend);
            }
            else
            {
                MessageBox.Show("ERRORE nell'invio del messaggio!");
            }

            backgroundWorker2.CancelAsync();
        }

        private void button_click(object sender, EventArgs e)
        {
            AttackToEnemy(((Button)sender).Name);
        }

        private void AttackToEnemy(string buttonName)
        {
            if (buttonName == null)
            {
                return;
            }

            else if (buttonName.Length == 3)
            {
                TextToSend = buttonName.Substring(0, 2);
            }

            TextToSend = buttonName;
            backgroundWorker2.RunWorkerAsync();

            SwitchGameButtonsEnabled();
        }

        private void SwitchGameButtonsEnabled()
        {
            if (areEnabledButtons == true)
            {
                foreach (var item in gameBoardButtons)
                {
                    item.Enabled = false;
                }
                labelAttackTurn.Text = "ATTENDERE...";
                areEnabledButtons = false;
            }
            else
            {
                foreach (var item in gameBoardButtons)
                {
                    item.Enabled = true;
                }
                labelAttackTurn.Text = "ATTACCA!";
                areEnabledButtons = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Server.listener != null && (Server.client == null && !Server.client.Connected))
            {
                MessageBox.Show("ERRORE! Connessione client non riuscita.");
            }
        }

        private void Form4_GameScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!myExit)
            {
                AttackToEnemy("EXIT");
            }
            Form2_PreparatoryScreen frm2 = new Form2_PreparatoryScreen();
            frm2.Show();
        }
    }
}
