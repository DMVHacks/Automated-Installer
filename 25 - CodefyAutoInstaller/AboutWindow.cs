using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace CodefyAutoInstaller
{
    public partial class AboutWindow : Form
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void AboutWindow_Load(object sender, EventArgs e)
        {
            VersionLabel.Text = "Version v" + Constants.APP_VERSION;
        }

        private void InstallCertificateButton_Click(object sender, EventArgs e)
        {
            string certName = Constants.TEMP_DIRECTORY + Constants.CERTIFICATE_NAME;

            X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
            try
            {
                store.Open(OpenFlags.ReadWrite);
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                MessageBox.Show("Codefy Installer was not able to access this computer's certificate store. Please run this application with administrator privileges!", "Permission Error");
                return;
            }

            using (Downloader downloader = new Downloader(Constants.CERTIFICATE_URL))
            {
                try
                {
                    downloader.DownloadFile(certName);
                }
                catch (WebException)
                {
                    MessageBox.Show("Codefy Installer was not able to download Codefy's root certificate. Please check that this computer has a stable internet connection and try again!", "Download Error");
                    return;
                }
            }

            store.Add(new X509Certificate2(certName));
            store.Close();

            File.Delete(certName);

            MessageBox.Show("Codefy's root certificate has been successfully installed on this computer!", "Installation Success");
        }
    }
}
