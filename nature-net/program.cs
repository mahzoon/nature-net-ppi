using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace nature_net
{
    public class program : System.Windows.Application
    {
        [STAThread]
        public static void Main()
        {
            //try
            //{
                program p = new program();
                p.StartupUri = new System.Uri("main_window.xaml", System.UriKind.Relative);
                p.Run();
            //}
            //catch (Exception ex)
            //{
            //    StreamReader reader = new StreamReader(configurations.GetAbsoluteLogFilePath());
            //    string whole = reader.ReadToEnd();
            //    reader.Close();

            //    StreamWriter writer = new StreamWriter(configurations.GetAbsoluteLogFilePath());
            //    writer.WriteLine("---START---");
            //    writer.WriteLine(ex.Message);
            //    writer.WriteLine(whole);
            //    writer.WriteLine(ex.StackTrace);
            //    writer.WriteLine("---END---");
            //    writer.Close();
            //}
        }
    }
}
