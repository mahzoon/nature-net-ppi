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
                string error = "--- START ON: " + DateTime.Now.ToString() + " ---\r\nMessage: " + e.Message +
                    "\r\n" + e.StackTrace + "--- END (" + DateTime.Now.ToString() + ") ---\r\n" + whole;
                writer.WriteLine(error);
                writer.Close();
            }
            catch (Exception) { }
        }
    }
}
