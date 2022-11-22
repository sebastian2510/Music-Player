using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class Form : System.Windows.Forms.Form
    {
        public List<string> paths = new List<string>();
        public List<string> files = new List<string>();

        public Form()
        {
            InitializeComponent();
        }

        private void SongList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show($"PRE: {SongList.SelectedIndex}");
            
            if (File.Exists(paths[SongList.SelectedIndex]))
            {
                PlaySong(paths[SongList.SelectedIndex]);
                //MessageBox.Show($@"Song: {files[SongList.SelectedIndex]} | Path: {paths[SongList.SelectedIndex]}");
            }
        }

        private void MediaPlayer_Enter(object sender, EventArgs e)
        {
        }

        private void Form_Load(object sender, EventArgs e)
        {
            DBHandler db = new DBHandler();
            if (SongList.Items.Count > 0)
            {
                SongList.Items.Clear();
            }

            db.Load(SongList, files, paths, out files, out paths);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBHandler db = new DBHandler();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (files != null)
                {
                    files.AddRange(ofd.SafeFileNames);
                    paths.AddRange(ofd.FileNames);

                    for (int i = files.Count - ofd.SafeFileNames.Length; i < files.Count; i++)
                    {
                        SongList.Items.Add(files[i]);
                    }
                }
                else
                {
                    files.AddRange(ofd.SafeFileNames);
                    paths.AddRange(ofd.FileNames);
                    SongList.Items.Add(files);
                }

                db.Insert(paths, files, ofd);
                //MessageBox.Show($"Files: {Convert.ToString(files[0])}  | Paths: {Convert.ToString(paths[0])}");
            }
        }

        private void RemoveSong_Click(object sender, EventArgs e)
        {
            DBHandler db = new DBHandler();
            db.Delete(SongList, out SongList, files, paths, out files, out paths);
        }
        private void MediaPlayerStateChangeEvent(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 0)
            {
                // undefined loaded
            }
            else if (e.newState == 1)
            {
                // if the file is stopped 
            }
            else if (e.newState == 3)
            {
                // if the file is playing 
            }
            else if (e.newState == 8)
            {
                if (SongList.SelectedIndex < (paths.Count - 1))
                {
                    SongList.SelectedIndex++;
                }
                else
                {
                    SongList.SelectedIndex = 0;
                }
            }
            else if (e.newState == 9)
            {
                // if the media player is loading new video
            }
            else if (e.newState == 10)
            {
                // media is ready to play again
                timer1.Start();
            }
        }

        public void PlaySong(string path)
        {
            MediaPlayer.URL = path;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MediaPlayer.Ctlcontrols.play();
            timer1.Stop();
        }
    }
}