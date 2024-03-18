using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipMpClient
{
    public partial class Form2_PreparatoryScreen : Form
    {
        public Form2_PreparatoryScreen()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        #region ****************************************************** 2D Drawing ******************************************************
        private Point selectionStart;
        private Point selectionEnd;
        private Rectangle selection;
        private bool mouseDown;



        protected override void OnMouseDown(MouseEventArgs e)
        {
            selectionStart = PointToClient(MousePosition);
            mouseDown = true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            mouseDown = false;

            SetSelectionRect();
            Invalidate();

            GetSelectedButtons();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!mouseDown)
            {
                return;
            }

            selectionEnd = PointToClient(MousePosition);
            SetSelectionRect();

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (mouseDown)
            {
                using (Pen pen = new Pen(Color.Black, 1F))
                {
                    pen.DashStyle = DashStyle.Dash;
                    e.Graphics.DrawRectangle(pen, selection);
                }
            }
        }

        private void SetSelectionRect()
        {
            int x, y;
            int width, height;

            x = selectionStart.X > selectionEnd.X ? selectionEnd.X : selectionStart.X;
            y = selectionStart.Y > selectionEnd.Y ? selectionEnd.Y : selectionStart.Y;

            width = selectionStart.X > selectionEnd.X ? selectionStart.X - selectionEnd.X : selectionEnd.X - selectionStart.X;
            height = selectionStart.Y > selectionEnd.Y ? selectionStart.Y - selectionEnd.Y : selectionEnd.Y - selectionStart.Y;

            selection = new Rectangle(x, y, width, height);
        }

        #endregion

        public static Dictionary<string, int> shipsCount = new Dictionary<string, int>()
        {
            {"Portaerei", 1 },{"Cannoniera", 2},{"Fregata", 3},{"Sottomarino", 4}
        };

        public static List<Ship> shipList = new List<Ship>();

        List<string> AllSelectedButtonList = new List<string>();

        bool isPanelActive = true;

        private void Form2_Load(object sender, EventArgs e)
        {
            shipList = null;
            CreateShipList();
            RemainingShips();
            timer1.Start();
        }

        private void CreateShipList()
        {
            if (shipList == null)
            {
                shipList = new List<Ship>();
            }
            foreach (var item in shipsCount)
            {
                Ship _ship = new Ship();
                _ship.shipName = item.Key;
                _ship.remShips = item.Value;

                shipList.Add(_ship);
            }
        }

        private void GetSelectedButtons()
        {
            List<Button> selected = new List<Button>();

            foreach (Control c in Controls)
            {
                if (c is Button)
                {
                    if (selection.IntersectsWith(c.Bounds))
                    {
                        selected.Add((Button)c);
                    }
                }
            }

            List<string> buttonsNames = new List<string>();

            foreach (Control c in selected)
            {
                buttonsNames.Add(c.Name);
            }

            buttonsNames.Sort();

            List<int> totalAsciiList = new List<int>();
            foreach (var item in buttonsNames)
            {
                char ch1 = char.Parse(item.Substring(0, 1));
                char ch2 = char.Parse(item.Substring(item.Length - 1));
                totalAsciiList.Add((int)ch1 + (int)ch2);
            }

            if (totalAsciiList.Count > 1)
            {
                for (int i = 0; i < totalAsciiList.Count - 1; i++)
                {
                    if (totalAsciiList[i] + 1 != totalAsciiList[i + 1])
                    {
                        MessageBox.Show("L'area in cui desideri posizionare la nave deve essere costituita da riquadri consecutivi.");
                        return;
                    }
                }
            }

            foreach (var item in selected)
            {
                if (item.BackColor == Color.DarkGray)
                {
                    DeleteShip(selected);
                    return;
                }
            }

            if (selected.Count > 0 && selected.Contains(buttonStart) == false)
            {
                using (var frm3 = new Form3_ShipSelectScreen(buttonsNames))
                {
                    var res = frm3.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        string val = frm3.returnValue;
                        foreach (var item in shipList)
                        {
                            if (item.shipName == val)
                            {
                                item.remShips--;

                                if (item.shipPerButton == null)
                                    item.shipPerButton = new List<ShipButtons>();

                                ShipButtons sb = new ShipButtons() { buttonNames = new List<string>() };
                                foreach (var item2 in selected)
                                {
                                    item2.BackColor = Color.DarkGray;
                                    //AllSelectedButtonList.Add(item2.Name);
                                    sb.buttonNames.Add(item2.Name);
                                }
                                item.shipPerButton.Add(sb);

                                //AllSelectedButtonList.Sort();
                                RemainingShips();
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void DeleteShip(List<Button> selected)
        {
            DialogResult dres = MessageBox.Show("Sei sicuro di voler eliminare la nave?", "Eliminazione nave!", MessageBoxButtons.YesNo);
            if (dres == DialogResult.Yes)
            {
                foreach (var item1 in shipList)
                {
                    if (item1.shipPerButton != null)
                    {
                        foreach (var item2 in item1.shipPerButton)
                        {
                            foreach (var item3 in selected)
                            {
                                if (item2.buttonNames.FirstOrDefault(x => x.Equals(item3.Name)) != null)
                                {
                                    item1.remShips++;
                                    foreach (var item4 in item2.buttonNames)
                                    {
                                        this.Controls.Find(item4, true)[0].BackColor = Color.Transparent;
                                    }
                                    item1.shipPerButton.Remove(item2);
                                    RemainingShips();
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }


        private void yardımToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1- Tieni premuto il tasto SINISTRO del mouse e seleziona le caselle in cui desideri posizionare le navi.\n\n" +
                "2- \r\nSeleziona la nave che desideri posizionare.\n\n" +
                "3- Fai clic su 'PRONTO' dopo aver posizionato tutte le navi.\n\n" +
                "ATTENZIONE: Il gioco inizierà dopo che entrambi i giocatori avranno premuto 'CONTINUA'.");
        }

        void RemainingShips()
        {
            lblBattleship.Text = lblBattleship.Text.Substring(0, lblBattleship.Text.Length - 1);
            lblBattleship.Text += shipList.FirstOrDefault(x => x.shipName == "Portaerei").remShips.ToString();

            lblCruiser.Text = lblCruiser.Text.Substring(0, lblCruiser.Text.Length - 1);
            lblCruiser.Text += shipList.FirstOrDefault(x => x.shipName == "Cannoniera").remShips.ToString();

            lblDestroyer.Text = lblDestroyer.Text.Substring(0, lblDestroyer.Text.Length - 1);
            lblDestroyer.Text += shipList.FirstOrDefault(x => x.shipName == "Fregata").remShips.ToString();

            lblSubmarine.Text = lblSubmarine.Text.Substring(0, lblSubmarine.Text.Length - 1);
            lblSubmarine.Text += shipList.FirstOrDefault(x => x.shipName == "Sottomarino").remShips.ToString();

            foreach (var item in shipList)
            {
                if (item.remShips == 0)
                {
                    isPanelActive = true;
                }
                else
                {
                    isPanelActive = false;
                    break;
                }
            }
            if (isPanelActive == true)
            {
                buttonStart.Enabled = true;
                buttonStart.Text = "PRONTO.\nStart";
            }
            else
            {
                buttonStart.Enabled = false;
                buttonStart.Text = "Preparazione";
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Form4_GameScreen frm4 = new Form4_GameScreen(FillAllButtonList());
            this.Visible = false;
            frm4.Show();
        }

        private List<string> FillAllButtonList()
        {
            foreach (var item1 in shipList)
            {
                foreach (var item2 in item1.shipPerButton)
                {
                    foreach (var item3 in item2.buttonNames)
                    {
                        AllSelectedButtonList.Add(item3);
                    }
                }
            }
            return AllSelectedButtonList;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Client.client.Connected == false)
            {
                MessageBox.Show("ERRORE! Connessione fallita.");
                Client.client.Dispose();
                Client.client = null;
                timer1.Stop();
                this.Close();
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            Form1_ClientScreen frm1 = new Form1_ClientScreen();
            frm1.Visible = true;
        }
    }
}
