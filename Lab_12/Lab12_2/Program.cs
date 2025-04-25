using System;
using System.Windows.Forms;

namespace Lab12_2 
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles(); // .NET Framework initialization
            Application.SetCompatibleTextRenderingDefault(false); // .NET Framework initialization
            Application.Run(new Form1());
        }
    }
}