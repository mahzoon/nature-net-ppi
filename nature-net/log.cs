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

        public static void WriteInteractionLog(int type, string details, System.Windows.Input.TouchDevice t)
        {
            if (configurations.write_interaction_log)
            {
                Interaction_Log i = new Interaction_Log();
                i.date = DateTime.Now; i.details = details; i.type = type;
                if (t != null)
                {
                    i.touch_id = t.Id;
                    i.touch_x = t.GetTouchPoint(null).Position.X;
                    i.touch_y = t.GetTouchPoint(null).Position.Y;
                }
                else
                {
                    i.touch_id = -1;
                    i.touch_x = -1;
                    i.touch_y = -1;
                }
                database_manager.InsertInteraction(i);
            }
        }

        public static void WriteInteractionLog(int type, string details, double x, double y)
        {
            if (configurations.write_interaction_log)
            {
                Interaction_Log i = new Interaction_Log();
                i.date = DateTime.Now; i.details = details; i.type = type;
                i.touch_id = -1; i.touch_x = x; i.touch_y = y;
                database_manager.InsertInteraction(i);
            }
        }
    }
}
