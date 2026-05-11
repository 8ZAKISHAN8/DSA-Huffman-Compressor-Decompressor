namespace WindowsFormsApp4
{
    partial class ModeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
           
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModeForm));
            this.btncompress = new System.Windows.Forms.Button();
            this.btndecompress = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.BufferSize = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // btncompress
            // 
            this.btncompress.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btncompress.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btncompress.BackgroundImage")));
            this.btncompress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btncompress.Font = new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncompress.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btncompress.Location = new System.Drawing.Point(464, 251);
            this.btncompress.Name = "btncompress";
            this.btncompress.Size = new System.Drawing.Size(182, 52);
            this.btncompress.TabIndex = 0;
            this.btncompress.UseVisualStyleBackColor = false;
            this.btncompress.Click += new System.EventHandler(this.btncompress_Click);
            // 
            // btndecompress
            // 
            this.btndecompress.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btndecompress.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btndecompress.BackgroundImage")));
            this.btndecompress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btndecompress.Font = new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndecompress.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btndecompress.Location = new System.Drawing.Point(464, 345);
            this.btndecompress.Name = "btndecompress";
            this.btndecompress.Size = new System.Drawing.Size(182, 48);
            this.btndecompress.TabIndex = 2;
            this.btndecompress.UseVisualStyleBackColor = false;
            this.btndecompress.Click += new System.EventHandler(this.btndecompress_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(391, 153);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(314, 50);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(377, 554);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(279, 30);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Goldenrod;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Location = new System.Drawing.Point(987, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 45);
            this.button2.TabIndex = 8;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // BufferSize
            // 
            this.BufferSize.BackColor = System.Drawing.Color.Orange;
            this.BufferSize.Location = new System.Drawing.Point(303, 55);
            this.BufferSize.Multiline = true;
            this.BufferSize.Name = "BufferSize";
            this.BufferSize.Size = new System.Drawing.Size(100, 34);
            this.BufferSize.TabIndex = 9;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox3.Location = new System.Drawing.Point(12, 55);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(285, 34);
            this.pictureBox3.TabIndex = 10;
            this.pictureBox3.TabStop = false;
            // 
            // ModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1054, 603);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.BufferSize);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btndecompress);
            this.Controls.Add(this.btncompress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ModeForm";
            this.Text = "Mode";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModeForm_FormClosing);
            this.Load += new System.EventHandler(this.ModeForm_Load);
            this.Leave += new System.EventHandler(this.button2_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btncompress;
        private System.Windows.Forms.Button btndecompress;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox BufferSize;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}