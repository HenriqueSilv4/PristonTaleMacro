using System;
using System.Windows.Forms;

namespace HenMacro
{
    static class Program
    {
        private static Form1 Programa = new Form1();
        
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Programa);
        }
    }
}