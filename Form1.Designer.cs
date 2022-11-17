namespace MusicPlayer
{
    partial class Form
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.SongList = new System.Windows.Forms.ListBox();
            this.ImportSong = new System.Windows.Forms.Button();
            this.MediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.RemoveSong = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // SongList
            // 
            this.SongList.BackColor = System.Drawing.Color.DodgerBlue;
            this.SongList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SongList.FormattingEnabled = true;
            this.SongList.Location = new System.Drawing.Point(614, 13);
            this.SongList.Name = "SongList";
            this.SongList.Size = new System.Drawing.Size(186, 390);
            this.SongList.TabIndex = 5;
            this.SongList.SelectedIndexChanged += new System.EventHandler(this.SongList_SelectedIndexChanged);
            // 
            // ImportSong
            // 
            this.ImportSong.BackColor = System.Drawing.Color.DodgerBlue;
            this.ImportSong.FlatAppearance.BorderSize = 0;
            this.ImportSong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImportSong.Location = new System.Drawing.Point(717, 415);
            this.ImportSong.Name = "ImportSong";
            this.ImportSong.Size = new System.Drawing.Size(83, 33);
            this.ImportSong.TabIndex = 6;
            this.ImportSong.Text = "Import song(s)";
            this.ImportSong.UseVisualStyleBackColor = false;
            this.ImportSong.Click += new System.EventHandler(this.button1_Click);
            // 
            // MediaPlayer
            // 
            this.MediaPlayer.Enabled = true;
            this.MediaPlayer.Location = new System.Drawing.Point(12, 12);
            this.MediaPlayer.Name = "MediaPlayer";
            this.MediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MediaPlayer.OcxState")));
            this.MediaPlayer.Size = new System.Drawing.Size(554, 391);
            this.MediaPlayer.TabIndex = 7;
            this.MediaPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.MediaPlayerStateChangeEvent);
            this.MediaPlayer.Enter += new System.EventHandler(this.MediaPlayer_Enter);
            // 
            // RemoveSong
            // 
            this.RemoveSong.BackColor = System.Drawing.Color.DodgerBlue;
            this.RemoveSong.FlatAppearance.BorderSize = 0;
            this.RemoveSong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveSong.Location = new System.Drawing.Point(614, 415);
            this.RemoveSong.Name = "RemoveSong";
            this.RemoveSong.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RemoveSong.Size = new System.Drawing.Size(97, 33);
            this.RemoveSong.TabIndex = 8;
            this.RemoveSong.Text = "Remove song(s)";
            this.RemoveSong.UseVisualStyleBackColor = false;
            this.RemoveSong.Click += new System.EventHandler(this.RemoveSong_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RemoveSong);
            this.Controls.Add(this.MediaPlayer);
            this.Controls.Add(this.ImportSong);
            this.Controls.Add(this.SongList);
            this.Name = "Form";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox SongList;
        private System.Windows.Forms.Button ImportSong;
        private AxWMPLib.AxWindowsMediaPlayer MediaPlayer;
        private System.Windows.Forms.Button RemoveSong;
        private System.Windows.Forms.Timer timer1;
    }
}

