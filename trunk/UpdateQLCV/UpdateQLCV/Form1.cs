using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace UpdateWIN
{
    public partial class Form1 : Form
    {
        string ftpServerIP;
        string ftpUserID;
        string ftpPassword;
        string ftpFileName;
        string ftpFileCreate;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ftpServerIP = "leadsofts.net/httpdocs";
            ftpUserID = "erpleadsofts";
            ftpPassword = "123pass";
            ftpFileName = "Qlcv.zip";
            ftpFileCreate = DateTime.Now.ToString("yyyyMMddhhmmss");
        }
        private void Upload(string filename)
        {
            FileInfo fileInf = new FileInfo(filename);
            string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;
            FtpWebRequest reqFTP;

            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileInf.Name));

            // Provide the WebPermission Credintials
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

            // By default KeepAlive is true, where the control connection is not closed
            // after a command is executed.
            reqFTP.KeepAlive = false;

            // Specify the command to be executed.
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Specify the data transfer type.
            reqFTP.UseBinary = true;

            // Notify the server about the size of the uploaded file
            reqFTP.ContentLength = fileInf.Length;

            // The buffer size is set to 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;

            // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
            FileStream fs = fileInf.OpenRead();

            try
            {
                // Stream to which the file to be upload is written
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                contentLen = fs.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Upload Error");
            }
        }

        public void DeleteFTP(string fileName)
        {
            try
            {
                string uri = "ftp://" + ftpServerIP + "/" + fileName;
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileName));

                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FTP 2.0 Delete");
            }
        }

        private string[] GetFilesDetailList()
        {
            string[] downloadFiles;
            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/"));
                ftp.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }

                result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
                //MessageBox.Show(result.ToString().Split('\n'));
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                downloadFiles = null;
                return downloadFiles;
            }
        }

        public string[] GetFileList()
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/"));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                //MessageBox.Show(reader.ReadToEnd());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                //MessageBox.Show(response.StatusDescription);
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                downloadFiles = null;
                return downloadFiles;
            }
        }
        private void Download(string filePath, string fileName,string fileDown)
        {
            FtpWebRequest reqFTP;
            lblStatus.Text = "Downloading...";
            lblStatus.Refresh();
            progressBar.Value = 0;
            try
            {
                //filePath = <<The full path where the file is to be created.>>, 
                //fileName = <<Name of the file to be created(Need not be the name of the file on FTP server).>>
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.OpenOrCreate);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileDown));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                
                int bufferSize = 2048;
                int readCount;
                long fileSize = GetFileSize(fileDown);
                long count = fileSize / 2048;
                byte[] buffer = new byte[bufferSize];
                progressBar.Maximum = (int)count + (int)count/3;
                progressBar.Value = 0;
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                int i = 0;
                while (readCount > 0)
                {
                    i++;
                    if (progressBar.Value < progressBar.Maximum)
                    {
                        progressBar.Value = progressBar.Value + 1;

                        progressBar.Refresh();
                    }
                    else
                    {
                    }
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                

                ftpStream.Close();
                outputStream.Close();
                response.Close();
                lblStatus.Text = "Updating...";
                lblStatus.Refresh();
                progressBar.Value = progressBar.Maximum;

                progressBar.Refresh();
               
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private long GetFileSize(string filename)
        {
            FtpWebRequest reqFTP;
            long fileSize = 0;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + filename));
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                fileSize = response.ContentLength;

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return fileSize;
        }

        private void Rename(string currentFilename, string newFilename)
        {
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + currentFilename));
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newFilename;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MakeDir(string dirName)
        {
            FtpWebRequest reqFTP;
            try
            {
                // dirName = name of the directory to create.
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + dirName));
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RunScript()
        {
            string path = Application.StartupPath + "\\" + "Script";
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                if (files.Length > 0)
                {
                    if (Provider.Connect())
                    {
                        foreach (string file in files)
                        {
                            try
                            {
                                StreamReader fs = new StreamReader(file);
                                string script = fs.ReadLine();
                                fs.Close();
                                Provider.ExecuteNonQuery(script);
                            }
                            catch { }

                        }
                        Provider.Close();
                    }
                }
                Directory.Delete(path, true);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                 ftpFileCreate = DateTime.Now.ToString("yyyyMMddhhmmss")+"";
                 Download(Application.StartupPath, ftpFileCreate, ftpFileName);
                 Unzip(Application.StartupPath, ftpFileCreate);
                 if (File.Exists("newversion.dll"))
                 {
                     StreamReader fsn = new StreamReader("newversion.dll");
                     string newver=fsn.ReadLine();
                     fsn.Close();
                     if (File.Exists("version.dll"))
                     {
                         StreamReader fr = new StreamReader("version.dll");
                         string ver = fr.ReadLine();
                         fr.Close();
                         if (newver != ver)
                         {
                             FileStream fs = new FileStream("version.dll", FileMode.OpenOrCreate);
                             StreamWriter fw = new StreamWriter(fs);
                             fw.WriteLine(newver);
                             fw.Close();
                             fs.Close();
                             try
                             {
                                 RunScript();
                                 
                             }
                             catch
                             {
                             }

                         }

                     }
                     else
                     {
                         FileStream fs = new FileStream("version.dll", FileMode.OpenOrCreate);
                         StreamWriter fw = new StreamWriter(fs);
                         fw.WriteLine(newver);
                         fw.Close();
                         fs.Close();
                         try
                         {
                             RunScript();
                         }
                         catch
                         {
                         }
                     }
                     File.Delete("newversion.dll");
                 }


                 MessageBox.Show("Update successful.","Infomation",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                 lblStatus.Text = "Finish";
                 //this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Update unsuccessful .Please try again." + ex.ToString(), "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Unzip(string zipfilePath, string zipfileName)
        {

            try
            {
                string destination = zipfilePath +"\\"+ zipfileName;
                using (ZipInputStream s = new ZipInputStream(File.OpenRead(destination)))
                {

                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {

                        Console.WriteLine(theEntry.Name);

                        string directoryName = Path.GetDirectoryName(theEntry.Name);
                        string fileName = Path.GetFileName(theEntry.Name);

                        // create directory
                        if (directoryName.Length > 0)
                        {
                            Directory.CreateDirectory(directoryName);
                        }

                        if (fileName != String.Empty)
                        {
                            using (FileStream streamWriter = File.Create(theEntry.Name))
                            {

                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }

                    }
                }
                File.Delete(destination);
                
            }
            catch (Exception ex)
            {
                throw (ex);
               //s MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
