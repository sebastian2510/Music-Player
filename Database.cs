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
    internal class Database
    {

    }

    class DBHandler
    {
         /* - Database creation
         CREATE DATABASE Music;
         CREATE TABLE Songs(
         id int PRIMARY KEY IDENTITY(1,1),
         path VARCHAR(100),
         song VARCHAR(100),
         Dato DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
         );
         */

          public void Insert(List<string> paths, List<string> files, OpenFileDialog ofd)
          {
             NameFilter filter = new NameFilter();
             string cstring = "server = 192.168.16.178; uid = Sebastian; pwd = 123Abcd123; DATABASE = Music;";
             int rows = 0;

              SqlCommand cmd = new SqlCommand();
             SqlConnection conn = new SqlConnection(cstring);
             conn.Open();

              for (int i = files.Count - ofd.SafeFileNames.Length; i < files.Count; i++)
             {
                 //filter.Remover(files, out files);
                 cmd = new SqlCommand($"INSERT INTO Songs (path, song) VALUES ('{paths[i]}', '{files[i]}')", conn);
                 rows += cmd.ExecuteNonQuery();
                 cmd.Cancel();
             }

              MessageBox.Show($"Imported song(s): {rows}");
             conn.Close();
          }

          public void Load(ListBox SongList, List<string> files, List<string> paths, out List<string> filesoutput, out List<string> pathsoutput)
          {
             string cstring = "server = 192.168.16.178; uid = Sebastian; pwd = 123Abcd123; DATABASE = Music;";

              SqlConnection conn = new SqlConnection(cstring);
             conn.Open();

              SqlCommand cmd = new SqlCommand("SELECT * FROM Songs", conn);
             SqlDataReader dr = cmd.ExecuteReader();

              while (dr.Read())
              {
                  //MessageBox.Show($"Songs: {Convert.ToString(dr.GetValue(2))}");
                  paths.Add(Convert.ToString(@dr.GetValue(1)));
                  //MessageBox.Show($@"I: {index} | {paths[index]}");
                  files.Add(Convert.ToString(dr.GetValue(2)));
              }
              
              for (int i = 0; i < files.Count; i++)
              {
                  if (File.Exists(paths[i]))
                  {
                      SongList.Items.Add(files[i]);
                  }
                  /* - Can't close the reader since i can't open it again while being in the loop
                  else
                  {
                      dr.Close(); // Does not work
                      cmd.Cancel(); // Does not work
              
                       cmd = new SqlCommand($"DELETE FROM Songs WHERE path = '{paths[i]}'", conn);
                      int pathdel = cmd.ExecuteNonQuery();
                      MessageBox.Show($"Deleted song: {files[i]}");
                  }
                  */
              }

              dr.Close();
              cmd.Cancel();
              conn.Close();
              pathsoutput = paths;
              filesoutput = files;
          }

          public void Delete(ListBox SongList, out ListBox Songs, List<string> files, List<string> paths, out List<string> filesoutput, out List<string> pathsoutput)
          {
             string cstring = "server = 192.168.16.178; uid = Sebastian; pwd = 123Abcd123; DATABASE = Music;";

              SqlCommand cmd = new SqlCommand();
             SqlConnection conn = new SqlConnection(cstring);
             conn.Open();

              cmd = new SqlCommand($"DELETE FROM Songs WHERE song = '{files[SongList.SelectedIndex]}'", conn);
             int rows = cmd.ExecuteNonQuery();
             cmd.Cancel();

            files.RemoveAt(SongList.SelectedIndex);
            paths.RemoveAt(SongList.SelectedIndex);
            SongList.Items.RemoveAt(SongList.SelectedIndex);


            for (int i = 0; i < SongList.Items.Count; i++)
              {
                 if (SongList.Items == null)
                 {
                     SongList.Items.RemoveAt(i);
                     files.RemoveAt(i);
                     paths.RemoveAt(i);
                 }
              }
             Songs = SongList;
             MessageBox.Show($"Delete song(s): {rows}");
             conn.Close();
             pathsoutput = paths;
             filesoutput = files;
          }
    }
}
