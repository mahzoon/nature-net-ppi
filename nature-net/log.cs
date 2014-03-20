using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace nature_net
{
    class log
    {
        public static void WriteErrorLog(Exception e)
        {
            try
            {
                StreamReader reader = new StreamReader(configurations.GetAbsoluteLogFilePath());
                string whole = reader.ReadToEnd();
                reader.Close();

                StreamWriter writer = new StreamWriter(configurations.GetAbsoluteLogFilePath());
                writer.WriteLine("--START--");
                writer.WriteLine(e.Message);
                writer.WriteLine(whole);
                writer.WriteLine(e.StackTrace);
                writer.WriteLine("--END--");
                writer.Close();
            }
            catch (Exception) { }
        }
    }
}
