using System;
using System.Windows.Forms;
using System.Drawing;
using Svt.Network;

namespace CasparCG_Client_Example
{
    public partial class Form1 : Form
    {
        CasparCGConnector ccgc = new CasparCGConnector();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            ccgc.ConnectionStatus += new CasparCGConnector.ConnectionStatusDelegate(DisplayConnectionChange);
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            ccgc.IPaddress = textBox1.Text.Trim();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            button1.Enabled = false;
            ccgc.ConnectToCGServer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ccgc.SendString(textBox2.Text.Trim());
        }

        #region CasparCG connection event handling
        public void DisplayConnectionChange(ConnectionEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CasparCGConnector.ConnectionStatusDelegate(DisplayConnectionChange), new Object[] { e });
            }
            else
            {
                UpdateFormOnConnectionChange();
            }
        }
        #endregion

        #region Form controls adjustment
        private void UpdateFormOnConnectionChange()
        {
            for (int i = 0; i < 1; i++)
            {
                button1.Enabled = true;
                if (!ccgc.IsConnected)
                {
                    button1.Text = "Connect";
                    button1.BackColor = Color.LightCoral;
                    disableControls();
                }
                else
                {
                    button1.Text = "Disconnect";
                    button1.Tag = "Initialized";
                    button1.BackColor = Color.LightGreen;
                    enableControls();
                }
            }
        }

        private void enableControls()
        {
            groupBox2.Enabled = true;
        }

        private void disableControls()
        {
            groupBox2.Enabled = false;
        }
        #endregion

    }
}
