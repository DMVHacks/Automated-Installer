using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Collections.Generic;

using Newtonsoft.Json; 

namespace CodefyAutoInstaller
{
    class DownloadWorker
    {
        private readonly Constants.FileList list;

        public DownloadWorker(Constants.FileList listIn)
        {
            list = listIn;
        }

        public void Run()
        {
            string rawJson;

            using (Downloader downloader = new Downloader(Constants.SERVER_BASE_URL + list.GetDescription()))
            {
                try
                {
                    rawJson = downloader.DownloadString();
                }
                catch (WebException)
                {
                    MessageBox.Show("Codefy Installer was not able to download a list of necessary programs for that class. Please verify that this computer has a stable internet connection and try again.", "Download Error");
                    return;
                }
            }

            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(rawJson);

            DownloadList downloadList = new DownloadList(json["class"], json["files"].Split(','), json["file_names"].Split(','), json["file_links"].Split(','));

            DownloadInfoWindow infoWindow = new DownloadInfoWindow();
            infoWindow.SetInfo(downloadList.ClassName, downloadList.Files);
            DialogResult choice = infoWindow.ShowDialog();

            if (choice == DialogResult.OK)
            {
                DownloadProgressWindow progressWindow = new DownloadProgressWindow();
                progressWindow.Show();

                List<string> errors = new List<string>();

                progressWindow.SetClassName(downloadList.ClassName);
                progressWindow.UpdateProgress("Starting download & installation process for: " + downloadList.ClassName);

                for (byte i = 0; i < downloadList.Files.Length; i++)
                {
                    string file = downloadList.Files[i];
                    string fileName = Constants.TEMP_DIRECTORY + downloadList.FileNames[i];
                    string fileLink = downloadList.FileLinks[i];

                    try
                    {
                        progressWindow.UpdateProgress("Downloading: " + fileName);
                        using (Downloader downloader = new Downloader(fileLink))
                        {
                            downloader.DownloadFile(fileName);
                        }

                        int returnCode;

                        progressWindow.UpdateProgress("Executing: " + fileName);
                        using (Task program = new Task(fileName, System.Diagnostics.ProcessWindowStyle.Normal))
                        {
                            returnCode = program.Run();
                        }

                        if (returnCode != 0)
                        {
                            errors.Add(string.Format("{0} returned exit code {1}. This means that the user may have canceled the installation process, or an unknown error occured.", fileName, returnCode));
                        }
                    }
                    catch (Exception e)
                    {
                        progressWindow.UpdateProgress("An error was thrown while attempting to download or execute {0}. This installer will be skipped.");
                        errors.Add(string.Format("While attempting to install {0}, this exception was raised: {1}", file, e.ToString()));
                        continue;
                    }
                    finally
                    {
                        progressWindow.UpdateProgress("Cleaning up: " + fileName);

                        if (File.Exists(fileName))
                            File.Delete(fileName);
                    }
                }

                progressWindow.UpdateProgress("Process complete!");

                System.Threading.Thread.Sleep(1000);

                progressWindow.Close();

                //=======================

                ResultWindow resultWindow = new ResultWindow();

                if (errors.Count == 0)
                {
                    resultWindow.AppendMessage("The installation process finished successfully.");
                }
                else
                {
                    resultWindow.AppendMessage("The installation process finished with errors. Some errors may signify that certain pieces of software were not installed on this computer. If you need assistance, please contact our support team!");

                    foreach (string error in errors)
                    {
                        resultWindow.AppendMessage(error);
                    }
                }

                resultWindow.ShowDialog();

                infoWindow.Dispose();
                progressWindow.Dispose();
                resultWindow.Dispose();
            }
        }
    }

    class DownloadList
    {
        public readonly string ClassName;
        public readonly string[] Files;
        public readonly string[] FileNames;
        public readonly string[] FileLinks;

        public DownloadList(string classNameIn, string[] filesIn, string[] fileNamesIn, string[] fileLinksIn)
        {
            ClassName = classNameIn;
            Files = filesIn;
            FileNames = fileNamesIn;
            FileLinks = fileLinksIn;
        }
    }
}
