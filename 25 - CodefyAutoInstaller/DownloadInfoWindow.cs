using System.Windows.Forms;

namespace CodefyAutoInstaller
{
    public partial class DownloadInfoWindow : Form
    {
        public DownloadInfoWindow()
        {
            InitializeComponent();
        }

        public void SetInfo(string className, string[] downloadList)
        {
            TitleBox.Text = string.Format("You are about to download & install software for: {0}", className);

            foreach (string download in downloadList)
            {
                DownloadListBox.AppendText(download + '\n');
            }
        }

        private void ReturnButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }

        private void InstallButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
