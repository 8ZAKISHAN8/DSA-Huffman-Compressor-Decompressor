using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WMPLib;
using static WindowsFormsApp4.Program;
namespace WindowsFormsApp4
{
  
  
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
            this.Shown += WelcomeForm_Shown;

        }
        private void WelcomeForm_Shown(object sender, EventArgs e)
        {
            
            MusicPlayer.Toggle(" the-amazing-world-of-gumball-theme.wav");  
        }




        private void button1_Click(object sender, EventArgs e)
        {
            ModeForm mode = new ModeForm();
            mode.StartPosition = FormStartPosition.CenterScreen;
        
            mode.Show();
            this.Hide(); 
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MusicPlayer.Toggle(" the-amazing-world-of-gumball-theme.wav");
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MusicPlayer.Stop();
        }

        private void WelcomeForm_Load(object sender, EventArgs e)
        {

        }

        private void WelcomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MusicPlayer.Stop();
        }
    }
}
