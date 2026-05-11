using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    internal static class Program
    {

        public static class MusicPlayer
        {
            public static WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();


            public static bool IsPlaying = false;

            public static void Toggle(string path)
            {
                if (IsPlaying)
                {
                    player.controls.stop();
                    IsPlaying = false;
                }
                else
                {
                    player.URL = path;
                    player.settings.setMode("loop", true); 
                    player.controls.play();
                    IsPlaying = true;
                }
            }
            public static void Stop()
            {
                if (IsPlaying)
                {
                    player.controls.stop();
                    IsPlaying = false;
                }
            }



        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WelcomeForm());
        }
    }
}
