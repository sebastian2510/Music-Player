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
        string[] paths;
        string[] files;
        
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

            MediaPlayer.URL = paths[SongList.SelectedIndex];
            MessageBox.Show($@"Song: {files[SongList.SelectedIndex]} | Path: {paths[SongList.SelectedIndex]}");
        }

        private void MediaPlayer_Enter(object sender, EventArgs e)
        {

        }

        private void Form_Load(object sender, EventArgs e)
        {
            NameFilter filter = new NameFilter();

            if (SongList.Items.Count > 0)
            {
                SongList.Items.Clear();
            }

            string cstring = "server = 192.168.16.178; uid = Sebastian; pwd = 123Abcd123; DATABASE = Music;";

            SqlConnection conn = new SqlConnection(cstring);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Songs", conn);
            SqlDataReader dr = cmd.ExecuteReader();

            string path = "";
            string file = "";

            while (dr.Read())
            {
                //MessageBox.Show($"Songs: {Convert.ToString(dr.GetValue(2))}");
                path += Convert.ToString(@dr.GetValue(1)) + ";";
                //MessageBox.Show($@"I: {index} | {paths[index]}");
                file += Convert.ToString(dr.GetValue(2)) + ";";
            }
            path = path.Remove(path.Length - 1);
            file = file.Remove(file.Length - 1);
            paths = path.Split(';');
            files = file.Split(';');
            //filter.Remover(files, out files);
            for (int i = 0; i < files.Length; i++)
            {
                SongList.Items.Add(files[i]);
            }

            //MessageBox.Show($@"{paths[6]}");
            dr.Close();
            cmd.Cancel();
            conn.Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            NameFilter filter = new NameFilter();
            DBHandler db = new DBHandler();
            OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = ".mp3";
            ofd.Multiselect = true;
            
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (files != null)
                {
                    Array.Resize(ref files, (files.Length + ofd.SafeFileNames.Length));
                    Array.Resize(ref paths, (paths.Length + ofd.FileNames.Length));

                    for (int i = files.Length - ofd.SafeFileNames.Length; i < files.Length; i++)
                    {
                        files[i] = ofd.SafeFileNames[i - 1];
                        paths[i] = ofd.FileNames[i - 1];

                        SongList.Items.Add(files[i]);
                    }
                }
                else
                {
                    Array.Resize(ref files, ofd.SafeFileNames.Length);
                    Array.Resize(ref paths, ofd.FileNames.Length);

                    for (int i = 0; i < files.Length; i++)
                    {
                        files[i] = ofd.SafeFileNames[i];
                        paths[i] = ofd.FileNames[i];

                        SongList.Items.Add(files[i]);
                    }
                }



                db.Insert(paths, files, ofd);


                //MessageBox.Show($"Files: {Convert.ToString(files[0])}  | Paths: {Convert.ToString(paths[0])}");

            }
        }

        private void RemoveSong_Click(object sender, EventArgs e)
        {
            string cstring = "server = 192.168.16.178; uid = Sebastian; pwd = 123Abcd123; DATABASE = Music;";

            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(cstring);
            conn.Open();

            cmd = new SqlCommand($"DELETE FROM Songs WHERE song = '{files[SongList.SelectedIndex]}'", conn);
            
            int rows = cmd.ExecuteNonQuery();
            SongList.Items.Remove(files[SongList.SelectedIndex]);
            cmd.Cancel();

            for (int i = 0; i < SongList.Items.Count; i++)
            {
                if (SongList.Items == null)
                {
                    SongList.Items.RemoveAt(i);
                    
                }
            }

            MessageBox.Show($"Delete song(s): {rows}");
            conn.Close();
        }
    }
    class DBHandler
    {
        /*
        CREATE DATABASE Music;
        CREATE TABLE Songs(
        id int PRIMARY KEY IDENTITY(1,1),
        path VARCHAR(100),
        song VARCHAR(100),
        Dato DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
        );
        */

        public void Insert(string[] paths, string[] files, OpenFileDialog ofd)
        {
            NameFilter filter = new NameFilter();
            string cstring = "server = 192.168.16.178; uid = Sebastian; pwd = 123Abcd123; DATABASE = Music;";

            SqlCommand cmd = new SqlCommand();
            int rows = 0;
            SqlConnection conn = new SqlConnection(cstring);
            conn.Open();

            for (int i = files.Length - ofd.SafeFileNames.Length; i < files.Length; i++)
            {
                //filter.Remover(files, out files);
                cmd = new SqlCommand($"INSERT INTO Songs (path, song) VALUES ('{paths[i]}', '{files[i]}')", conn);
                rows += cmd.ExecuteNonQuery();
                cmd.Cancel();
            }

            MessageBox.Show($"Imported song(s): {rows}");
            conn.Close();
        }
    }
    class NameFilter
    {
        public void Remover(string[] files, out string[] output)
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
                /*
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
}
