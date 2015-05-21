using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

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
            //    log.WriteErrorLog(ex);
            //    MessageBox.Show("Sorry! The program needs to be restarted.");
            //}
        }
    }
}
