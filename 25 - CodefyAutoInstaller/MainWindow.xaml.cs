using System.Windows;

namespace CodefyAutoInstaller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            using (AboutWindow aboutWindow = new AboutWindow())
            {
                aboutWindow.ShowDialog();
            }
        }

        //==========================================================================

        private void InstallScratchBasic_Click(object sender, RoutedEventArgs e)
        {
            new DownloadWorker(Constants.FileList.SCRATCH_BASIC).Run();
        }

        private void InstallJavaBasic_Click(object sender, RoutedEventArgs e)
        {
            new DownloadWorker(Constants.FileList.JAVA_BASIC).Run();
        }

        private void InstallJavaAdvanced_Click(object sender, RoutedEventArgs e)
        {
            new DownloadWorker(Constants.FileList.JAVA_ADVANCED).Run();
        }

        private void InstallPythonBasic_Click(object sender, RoutedEventArgs e)
        {
            new DownloadWorker(Constants.FileList.PYTHON_BASIC).Run();
        }

        private void InstallPythonAdvanced_Click(object sender, RoutedEventArgs e)
        {
            new DownloadWorker(Constants.FileList.PYTHON_ADVANCED).Run();
        }

        private void InstallWebDevelopment_Click(object sender, RoutedEventArgs e)
        {
            new DownloadWorker(Constants.FileList.WEB_DEVELOPMENT).Run();
        }

        private void InstallWebApplications_Click(object sender, RoutedEventArgs e)
        {
            new DownloadWorker(Constants.FileList.WEB_APPLICATIONS).Run();
        }
    }
}
