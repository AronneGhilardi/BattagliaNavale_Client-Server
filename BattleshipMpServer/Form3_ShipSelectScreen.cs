using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipMp
{
    public partial class Form3_ShipSelectScreen : Form
    {
        List<string> buttonsNames;

        public string returnValue { get; set; }

        Dictionary<string, int> squarePerShips = new Dictionary<string, int>()
        {
            {"Portaerei", 4}, {"Cannoniera", 3}, {"Fregata", 2}, {"Sottomarino", 1}
        };
        public Form3_ShipSelectScreen(List<string> buttonsNames)
        {
            this.buttonsNames = buttonsNames;
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            foreach (var item1 in Form2_PreparatoryScreen.shipList)
            {
                foreach (var item2 in squarePerShips)
                {
                    if (item1.shipName == item2.Key)
                    {
                        if (item2.Value == buttonsNames.Count)
                        {
                            if (item1.remShips <= 0)
                            {
                                MessageBox.Show("Non ci sono navi da posizionare nell'area selezionata.");
                                this.Close();
                            }
                            listBox1.Items.Add(item1.shipName);
                        }
                    }
                }
                
            }

            if (listBox1.Items.Count != 0)
            {
                listBox1.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                this.returnValue = listBox1.SelectedItem.ToString();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
