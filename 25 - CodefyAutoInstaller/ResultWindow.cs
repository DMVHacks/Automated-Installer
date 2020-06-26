using System.Windows.Forms;

namespace CodefyAutoInstaller
{
    public partial class ResultWindow : Form
    {
        public ResultWindow()
        {
            InitializeComponent();
        }

        public void AppendMessage(string message)
        {
            ErrorBox.AppendText(message + '\n');
        }

        private void ReturnButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
