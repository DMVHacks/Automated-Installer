using System.Windows.Forms;

namespace CodefyAutoInstaller
{
    public partial class DownloadProgressWindow : Form
    {
        public DownloadProgressWindow()
        {
            InitializeComponent();
        }

        public void SetClassName(string className)
        {
            TitleBox.AppendText("Installation Progress for: " + className); //Using .AppendText() so that text is shown early
        }

        public void UpdateProgress(string update)
        {
            ProgressBox.AppendText(update + '\n');
        }
    }
}
