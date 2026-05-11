using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CuteCompressorGUI;
using WMPLib;
using static WindowsFormsApp4.Program;

namespace WindowsFormsApp4
{
    public partial class ModeForm : Form
    {
        public static string mode = "";
        public static int SelectedBufferSize = -1; 

        public ModeForm()
        {
            InitializeComponent();
            
          
        }

        private void btncompress_Click(object sender, EventArgs e)
        {
            mode = "-c";
            if (int.TryParse(BufferSize.Text.Trim(), out int bufSize) && bufSize > 0)
                SelectedBufferSize = bufSize;
            else
                SelectedBufferSize = -1; 
            var dragForm = new DragDropForm(mode);
            dragForm.Show();
            this.Hide();
        }

        private void btndecompress_Click(object sender, EventArgs e)
        {
            mode = "-d";
            if (int.TryParse(BufferSize.Text.Trim(), out int bufSize) && bufSize > 0)
                SelectedBufferSize = bufSize;
            else
                SelectedBufferSize = -1;
            var dragForm = new DragDropForm(mode);
            
         
         
            dragForm.Show();
            this.Hide();
        }

   

        private void button2_Click(object sender, EventArgs e)
        {
            MusicPlayer.Toggle(" the-amazing-world-of-gumball-theme.wav");
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MusicPlayer.Stop();
            
        }
        private void ModeForm_Load(object sender, EventArgs e)
        {

        }

        private void ModeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MusicPlayer.Stop();
        }
    }
}
