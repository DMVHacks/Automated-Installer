using System;
using System.Net;
using System.Reflection;
using System.Windows;

namespace CodefyAutoInstaller
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //Change network security protocols

            using (Downloader downloader = new Downloader(Constants.VERSION_URL))
            {
                string newestVersion = string.Empty;
                try 
                { 
                    newestVersion = downloader.DownloadString(); 
                }
                catch (WebException) 
                { 
                    MessageBox.Show("Codefy Installer was not able to establish a connection to Codefy's servers. Please ensure that this computer has a stable internet connection and try again!\n\nThis program will now shut down.", "Network Error");
                    Environment.Exit(-1);
                }

                if (!newestVersion.Equals(Constants.APP_VERSION))
                {
                    MessageBox.Show(string.Format("This version of Codefy Installer ({0}) is outdated. For upgraded features & stability, please download the newest version on our website!", Constants.APP_VERSION), "Version Notification");
                }
            }

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            var application = new App();
            application.InitializeComponent();
            application.Run();
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) //Copy & Pasted from denhamcoder.net. This code imports Newtonsoft.Json.dll which is bundled inside the .exe
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CodefyAutoInstaller.Resources.Newtonsoft.Json.dll"))
            {
                var assemblyData = new byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);
                return Assembly.Load(assemblyData);
            }
        }
    }
}
