using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    internal class FileModify
    {
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
