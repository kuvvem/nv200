using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITLlib;

namespace eSSP_example
{
    public partial class Form1 : Form
    {
        // Events handling section 

        private void Form1_Load(object sender, EventArgs e)
        {
            // create an instance of the validator info class
            Validator = new CValidator();
            btnHalt.Enabled = false;

            // Position comms window
            Point p = new Point(Location.X, Location.Y);
            p.X += this.Width;
            Validator.CommsLog.Location = p;

            if (Properties.Settings.Default.CommWindow)
            {
                Validator.CommsLog.Show();
                logTickBox.Checked = true;
            }
            else
                logTickBox.Checked = false;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            // hide this and show opening menu
            Hide();
            frmOpenMenu menu = new frmOpenMenu(this);
            menu.Show();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            MainLoop();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formSettings = new frmSettings();
            formSettings.ShowDialog();
            Running = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void btnHalt_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("Poll loop stopped\r\n");
            Running = false;
        }

        private void resetValidatorBtn_Click(object sender, EventArgs e)
        {
            if (Validator != null)
            {
                Validator.Reset(textBox1);
                Validator.SSPComms.CloseComPort();
            }
         
        }

        private void logTickBox_CheckedChanged(object sender, EventArgs e)
        {
            if (logTickBox.Checked)
                Validator.CommsLog.Show();
            else
                Validator.CommsLog.Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Running = false;
            Properties.Settings.Default.CommWindow = logTickBox.Checked;
            Properties.Settings.Default.ComPort = Global.ComPort;
            Properties.Settings.Default.Save();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSettings f = new frmSettings();
            f.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Validator.NumberOfNotesStacked = 0;
        }

        private void reconnectionTimer_Tick(object sender, EventArgs e)
        {
            reconnectionTimer.Enabled = false;
        }
    }
}
