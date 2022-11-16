using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;

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
            if (SongList.SelectedIndex > SongList.Items.Count || SongList.SelectedIndex < 0)
            {
                SongList.SelectedIndex = 0;
            }

            if (File.Exists(paths[SongList.SelectedIndex]))
            {
                MediaPlayer.URL = paths[SongList.SelectedIndex];
                //MessageBox.Show($@"Song: {files[SongList.SelectedIndex]} | Path: {paths[SongList.SelectedIndex]}");
            }
        }

        private void MediaPlayer_Enter(object sender, EventArgs e)
        {
            while (MediaPlayer.playState != WMPLib.WMPPlayState.wmppsPlaying)
            {
                if (MediaPlayer.playState == WMPLib.WMPPlayState.wmppsStopped)
                {
                    if (SongList.SelectedIndex < paths.Count - 1)
                    {
                        MediaPlayer.URL = paths[SongList.SelectedIndex + 1];
                    }
                    else
                    {
                        MediaPlayer.URL = paths[0];
                    }
                }
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            NameFilter filter = new NameFilter();
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
            int flength = files.Count;
            db.Delete(SongList, out SongList, files, paths, out files, out paths);
            if (flength == files.Count)
            {
                files.RemoveAt(0);
            }
        }
    }
    class NameFilter
    {
        public void Remover(string[] files, out string[] output) // This method breaks deletion-part etc.
        {
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Contains('(') && files[i].Contains(')'))
                {
                    int start = files[i].LastIndexOf('(');
                    int end = files[i].LastIndexOf(')');
                    files[i] = files[i].Remove(start, end - start + 1);

                    if (files[i].Contains('(') && files[i].Contains(')'))
                    {
                        start = files[i].LastIndexOf('(');
                        end = files[i].LastIndexOf(')');
                        files[i] = files[i].Remove(start, end - start + 1);

                        if (files[i].Contains('(') && files[i].Contains(')'))
                        {
                            start = files[i].LastIndexOf('(');
                            end = files[i].LastIndexOf(')');
                            files[i] = files[i].Remove(start, end - start + 1);
                        }
                    }
                }

                if (files[i].Contains('[') && files[i].Contains(']'))
                {
                    int startbracket = files[i].LastIndexOf('[');
                    int endbracket = files[i].LastIndexOf(']');
                    files[i] = files[i].Remove(startbracket, endbracket - startbracket + 1);
                }
                /* Might use later
                if (files[i].Contains(".mp3"))
                {
                    int start = files[i].IndexOf(".mp3");
                    int end = files[i].LastIndexOf("mp3");
                    files[i] = files[i].Remove(start, end - start + 3);
                }
                */
            }
            output = files;
        }
    }
    class Deleter
    {
        public void ArrDel(string[] files, int i, out string[] output) // Not relevant anymore since it's moved to an list
        {
            string file = "";

            for (int index = 0; index < files.Length; index++)
            {
                file += files[index] + ";";
            }

            int rem = file.IndexOf(files[i]);
            file = file.Remove(rem, file.IndexOf(';', rem));
            file = file.Remove(file.Length - 1);

            files = file.Split(';');
            output = files;
        }
    }
}