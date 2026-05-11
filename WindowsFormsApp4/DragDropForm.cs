using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp4;
//using static System.Net.Mime.MediaTypeNames;
using static WindowsFormsApp4.Program;
using WindowsFormsApp4.Properties;
using System.Resources;

namespace CuteCompressorGUI
{
  

    public partial class DragDropForm : Form
    {
        private string Mode;
        private List<(string path, string key)> droppedFiles = new List<(string path, string key)>();
        private HashSet<string> processingFiles = new HashSet<string>();
        private Dictionary<string, (ProgressBar, Label)> fileProgressControls = new Dictionary<string, (ProgressBar, Label)>();
        private int currentX = 20;
        private int currentY = 10;


        public DragDropForm(string mode)
        {
            InitializeComponent();
            Mode = mode;
           
            panelDropArea.AllowDrop = true;
            panelDropArea.DragEnter += Panel_DragEnter;
            panelDropArea.DragDrop += Panel_DragDrop;
            panelDropArea.BackColor = Color.FromArgb(0, 0, 0, 0);

        }

        private void Panel_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Panel_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                int iconSize = 30;
                int horizontalSpacing = 2;
                int panelWidth = panelDropArea.Width;

                foreach (string file in files)
                {
                    // Check if the file is already in the list
                    bool alreadyExists = droppedFiles.Exists(f => f.path == file);
                    if (alreadyExists)
                    {
                        MessageBox.Show($"A task is already in progress for this file:\n{Path.GetFileName(file)}", "Duplicate File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue; 
                    }

                   
                    string uniqueKey = file + "_" + Guid.NewGuid().ToString();

                    
                    droppedFiles.Add((file, uniqueKey));

                    string fileName = Path.GetFileName(file);

                  
                    Label nameLabel = new Label
                    {
                        Text = fileName,
                        AutoSize = true,
                        ForeColor = Color.Black,
                        Font = new Font("Arial", 10, FontStyle.Bold)
                    };

                    
                    ColoredProgressBar progressBar = new ColoredProgressBar
                    {
                        Width = 150,
                        Height = 10,
                        Minimum = 0,
                        Maximum = 100,
                    };

             
                    Label percentLabel = new Label
                    {
                        Text = "0%",
                        AutoSize = true,
                        ForeColor = Color.Black
                    };

                   
                    FlowLayoutPanel filePanel = new FlowLayoutPanel
                    {
                        FlowDirection = FlowDirection.LeftToRight,
                        AutoSize = true
                    };

                    filePanel.Controls.Add(nameLabel);
                    filePanel.Controls.Add(progressBar);
                    filePanel.Controls.Add(percentLabel);

                    panelLeft.Controls.Add(filePanel);



                    fileProgressControls[uniqueKey] = (progressBar, percentLabel);

                    PictureBox fileIcon = new PictureBox
                    {
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Size = new Size(iconSize, iconSize),
                        Location = new Point(currentX, currentY),
                        Cursor = Cursors.Hand
                    };

                    string imagePath = Path.Combine(Application.StartupPath, "Resources", "Darwin_Logo.png");
                    fileIcon.Image = Image.FromFile(imagePath);

                    ToolTip tooltip = new ToolTip();
                    tooltip.SetToolTip(fileIcon, Path.GetFileName(file));

                    panelDropArea.Controls.Add(fileIcon);
                    currentX += iconSize + horizontalSpacing;

                    if (currentX + iconSize > panelWidth)
                    {
                        currentX = 20;
                        currentY += 25;
                    }
                }
            }
        }



        private void RunHuffmanProcess(string inputPath, string progressKey)

        {
            try
            {
                string exePath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Resources", "Huffy.exe");

                string modeFlag = (Mode == "-c") ? "-c" : "-d";
                string bufferMode = "-b";
                int bufferSize = ModeForm.SelectedBufferSize > 0 ? ModeForm.SelectedBufferSize : 65000;

                string arguments = $"\"{modeFlag}\" \"{inputPath}\" \"{inputPath}\" {bufferMode} \"{bufferSize}\"";

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = exePath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8 
                };

                using (Process process = new Process())
                {
                    process.StartInfo = psi;
                    process.EnableRaisingEvents = true;


                    if (!fileProgressControls.TryGetValue(progressKey, out var controls))
                    {
                        MessageBox.Show($"No progress controls found for file: {inputPath}");
                        return;
                    }

                    process.OutputDataReceived += (s, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data) && e.Data.StartsWith("Progress:"))
                        {
                            try
                            {
                                string line = e.Data.Replace("Progress:", "").Trim(); //  "43% | ETA: 2.3 seconds"
                                string percentPart = line.Split('%')[0].Trim();       // "43"
                                if (double.TryParse(percentPart, out double percent))
                                {
                                    int progressValue = (int)Math.Min(Math.Max(percent, 0), 100);

                                    if (panelLeft.InvokeRequired)
                                    {
                                        panelLeft.Invoke(new Action(() =>
                                        {
                                            controls.Item1.Value = progressValue;
                                            controls.Item2.Text = $"{progressValue}%";
                                            controls.Item1.Refresh();
                                            controls.Item2.Refresh();
                                        }));
                                    }
                                    else
                                    {
                                        controls.Item1.Value = progressValue;
                                        controls.Item2.Text = $"{progressValue}%";
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Parsing error: " + ex.Message);
                            }
                        }
                    };


                    process.ErrorDataReceived += (s, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            this.Invoke(new Action(() =>
                            {
                                Console.WriteLine("STDERR: " + e.Data);
                                MessageBox.Show("Error Output: " + e.Data);
                            }));

                        }
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    process.WaitForExit();

                    this.Invoke(new Action(() =>
                    {
                        Label resultLabel = new Label
                        {
                            Text = $"✔️ Done processing {Path.GetFileName(inputPath)}",
                            ForeColor = Color.FromArgb(255, 111, 0),  //  Darwin Orange
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            AutoSize = true,
                            Margin = new Padding(5)
                        };

                        panelRight.Controls.Add(resultLabel);
                    }));

                }
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Exception: " + ex.Message);
                }));
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            var Mode = new ModeForm();
            Mode.StartPosition = FormStartPosition.CenterScreen;
       
            Mode.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MusicPlayer.Toggle(" the-amazing-world-of-gumball-theme.wav");
        }
       

        private void Process_Click(object sender, EventArgs e)
        {
            if (droppedFiles.Count == 0)
            {
                MessageBox.Show("Please drop at least one file first.");
                return;
            }

            var filesToProcess = new List<(string path, string key)>(droppedFiles);

            foreach (var (path, key) in filesToProcess)
            {
                lock (processingFiles)
                {
                    if (processingFiles.Contains(path))
                    {
                        MessageBox.Show($"A task is already in progress for: {Path.GetFileName(path)}");
                        continue;
                    }

                    processingFiles.Add(path);
                }

                Task.Run(() =>
                {
                    RunHuffmanProcess(path, key);

                    this.Invoke(new Action(() =>
                    {
                        lock (processingFiles)
                        {
                            processingFiles.Remove(path);
                        }

                        // Optional: remove from UI when finished
                        droppedFiles.RemoveAll(f => f.path == path);
                        if (droppedFiles.Count == 0)
                        {
                            panelDropArea.Controls.Clear();
                            currentX = 20;
                            currentY = 10;
                        }
                    }));
                });
            }
        }


        private void DragDrop_Load(object sender, EventArgs e)
        {

        }

        private void DragDropForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MusicPlayer.Stop();
        }
    }


    public class ColoredProgressBar : ProgressBar
    {
        private static readonly Color GumballBlue = Color.FromArgb(76, 191, 232);

        public ColoredProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = e.ClipRectangle;
            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;

            // Draw base bar background
            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);

            // Draw custom progress bar
            rec.Height = rec.Height - 4;

            using (SolidBrush brush = new SolidBrush(GumballBlue))
            {
                e.Graphics.FillRectangle(brush, 2, 2, rec.Width, rec.Height);
            }
        }
    }

}
