using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandler
{
    public class FileHandler
    {
        // used to ensure only one process writes to a textfile at a time
        private object threadLock = new object();

        public FileHandler()
        {

        }

        public bool OverwriteData(string FilePath, List<string> data)
        {
            lock (threadLock)
            {
                try
                {
                    return WriteData(new StreamWriter(new FileStream(FilePath, FileMode.Create, FileAccess.Write)), data);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool AppendData(string FilePath, List<string> data)
        {
            lock (threadLock)
            {
                try
                {
                    // If the file to be appended to does not exist
                    if (!File.Exists(FilePath))
                    {
                        Directory.CreateDirectory(Directory.GetDirectoryRoot(FilePath));
                        // Create the file
                        File.Create(FilePath);
                    }

                    // Create a stream to the file and attempt to append the information
                    return WriteData(new StreamWriter(new FileStream(FilePath, FileMode.Append, FileAccess.Write)), data);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        private bool WriteData(StreamWriter writer, List<string> data)
        {
            try
            {
                foreach (string line in data)
                {
                    writer.WriteLine(line);
                }
                writer.Flush();
                writer.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<string> ReadData(string FilePath)
        {
            lock (threadLock)
            {
                try
                {
                    StreamReader reader = new StreamReader(new FileStream(FilePath, FileMode.Open, FileAccess.Read));

                    List<string> temp = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        temp.Add(reader.ReadLine());
                    }

                    reader.Close();
                    return temp;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
