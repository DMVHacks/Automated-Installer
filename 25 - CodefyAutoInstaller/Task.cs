using System;
using System.Diagnostics;

namespace CodefyAutoInstaller
{
    class Task : IDisposable
    {
        private readonly Process process;

        public Task(string fileName, ProcessWindowStyle windowStyle)
        {
            process = new Process();
            process.StartInfo.WindowStyle = windowStyle;
            process.StartInfo.FileName = fileName;
        }

        ~Task()
        {
            Dispose();
        }

        public void Dispose()
        {
            process.Dispose();
        }


        public int Run()
        {
            process.Start();
            process.WaitForExit();

            return process.ExitCode;
        }
    }
}
