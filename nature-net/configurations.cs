using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace nature_net
{
    public class configurations
    {
        static string config_file = "config.ini";
        public static string line_break = "\r\n";
        static string log_file = "log.txt";
        public static string contribution_comment_date = "Taken by: ";
        public static string contribution_comment_tag = "Tags: ";
        public static string contribution_comment_location = "Location ";
        public static string designidea_date_desc = "Last Update: ";
        public static string designidea_num_desc = "replies";
        public static string users_date_desc = "Last Update: ";
        public static string users_num_desc = "Contributions";
        public static string users_no_date = "Just Created"; 
        public static string activities_date_desc = "Last Update: ";
        public static string activities_num_desc = "Contributions";

        public static bool high_contrast = false;
        public static bool top_most = false;
        public static bool show_update_label = false;
        public static bool response_to_mouse_clicks = true;

        public static int max_num_content_update = 12;

        public static int max_signup_frame = 5;
        public static int max_collection_frame = 10;
        public static int max_image_display_frame = 10;
        public static int max_design_ideas_frame = 10;
        public static int max_activity_frame = 10;
        public static int max_activity_frame_title_chars = 10;
        //public static int thumbnail_pixel_width = 100;
        public static int thumbnail_pixel_height = 100;
        public static TimeSpan thumbnail_video_span = new TimeSpan(0, 0, 2);
        public static bool use_existing_thumbnails = true;
        public static double drag_dy_dx_factor = 2.1;
        //public static double drag_dx_dy_factor = 1.0;

        public static double drag_collection_theta = 5;
        public static double scroll_scale_factor = 5;
        public static int min_touch_points = 2;
        public static int max_consecutive_drag_points = 5;
        public static double tap_error = 1;
        public static int tab_width_percentage = 17;
        //public static int tab_header_width = 65;
        public static bool manual_scroll = false;
        public static bool manual_tap = false;
        public static bool right_panel_drag = true;
        public static bool whole_item_drag = false;
        
        public static bool use_avatar_drag = false;

        public static List<Point> locations = new List<Point>();
        public static int location_dot_diameter = 55;
        public static Brush location_dot_color = Brushes.Crimson;
		public static Brush location_dot_outline_color = Brushes.Crimson;
        public static Brush location_dot_font_color = Brushes.White;

        //static string current_directory = System.IO.Directory.GetCurrentDirectory() + "\\";
        //static string image_directory = System.IO.Directory.GetCurrentDirectory() + "\\images\\";
        //static string avatar_directory = System.IO.Directory.GetCurrentDirectory() + "\\images\\avatars\\";
        //static string thumbnails_directory = System.IO.Directory.GetCurrentDirectory() + "\\images\\thumbnails\\";
        //static string contributions_directory = System.IO.Directory.GetCurrentDirectory() + "\\images\\contributions\\";

        public static string googledrive_directory_id = "0B9mU-w_CpbztUUxtaXVIeE9SbWM";
        public static string googledrive_client_id = "333780750675-ag76kpq3supbbqi3v92vn3ejil8ght23.apps.googleusercontent.com";
        public static string googledrive_client_secret = "bCYIAfrAC0i-qIfl0cLRnhwn";
        public static string googledrive_storage = "gdrive_uploader";
        public static string googledrive_key = "z},drdzf11x9;87";
        public static string googledrive_refresh_token = "1/jpJHu8br2TnnM5hwCqgFe-yagf6zixlDZrlUvdXZ9s8";
        public static string googledrive_lastchange = "1";
		public static string googledrive_userfilename = "Users.txt";
        public static string googledrive_userfiletitle = "Users";
        public static string googledrive_ideafilename = "Ideas.txt";
        public static string googledrive_ideafiletitle = "Ideas";
        public static int googledrive_update_min = 2;
        public static int googledrive_update_sec = 30;

        // 10KB = 10 * 1024
        public static int download_buffer_size = 10240;

        static string image_path = ".\\images\\";
        static string avatar_path = ".\\images\\avatars\\";
        static string thumbnails_path = ".\\images\\thumbnails\\";
        static string contributions_path = ".\\images\\contributions\\";

        static string background_pic = "background.png";
        static string drop_avatar_pic = "drop_avatar.png";
        static string loading_image_pic = "loading_image.png";
        static string empty_image_pic = "empty_image.png";
        static string not_found_image_pic = "not_found_image.png";
        static string sound_image_pic = "sound_image.png";
        static string video_image_pic = "film.png";
        static string keyboard_pic = "keyboard.png";
        static string close_icon = "close.png";
        static string change_view_list_icon = "change_view_list.png";
        static string change_view_stack_icon = "change_view_stack.png";
        static string collection_window_icon = "collection_window_icon.png";
        static string signup_icon = "signup.png";
        static string signup_window_icon = "signup_window_icon.png";
        static string submit_idea_icon = "submit_idea.png";
        static string thumbs_up_icon = "tu.jpg";
        static string thumbs_down_icon = "td.jpg";
        static string drag_icon = "drag.png";

        public static string keyboard_click_wav = "click.wav";

        public static ImageSource img_background_pic;
        public static ImageSource img_drop_avatar_pic;
        public static ImageSource img_loading_image_pic;
        public static ImageSource img_empty_image_pic;
        public static ImageSource img_not_found_image_pic;
        public static ImageSource img_sound_image_pic;
        public static ImageSource img_video_image_pic;
        public static ImageSource img_keyboard_pic;
        public static ImageSource img_close_icon;
        public static ImageSource img_change_view_list_icon;
        public static ImageSource img_change_view_stack_icon;
        public static ImageSource img_collection_window_icon;
        public static ImageSource img_signup_window_icon;
        public static ImageSource img_signup_icon;
        public static ImageSource img_submit_idea_icon;
        public static ImageSource img_thumbs_up_icon;
        public static ImageSource img_thumbs_down_icon;
        public static ImageSource img_drag_icon;

        public static int frame_width = 300;
        public static int frame_title_bar_height = 40;
        public static int frame_icon_width = 40;
        public static int collection_listbox_height = 170;

        public static int toolbar_item_width = 30;
        public static double title_font_size = 20;
        public static string title_font_name = "Calibri";
        public static Brush right_panel_background = Brushes.LightGray;
        public static Brush right_panel_border_color = Brushes.Gray;
        public static double right_panel_width = 80;
        public static double user_item_avatar_width = 60;
        public static double design_idea_item_title_font_size = 17;
        public static double design_idea_item_user_info_font_size = 10;
        public static double design_idea_item_avatar_width = 30;
        public static double design_idea_right_panel_width = 45;

        public static Random RAND = new Random();
        public static int SEED()
        {
            return RAND.Next();
        }

        public static int RANDOM(int min, int max)
        {
            return RAND.Next(min, max);
        }

        public static byte[] GetBytes(string content)
        {
            byte[] b = new byte[content.Length];
            for (int counter = 0; counter < content.Length; counter++)
                b[counter] = Convert.ToByte(content[counter]);
            return b;
        }

        public static string GetString(byte[] content)
        {
            string s = "";
            for (int counter = 0; counter < content.Length; counter++)
                s = s + Convert.ToChar(content[counter]);

            return s;
        }

        public static string GetAbsolutePath()
        {
            return (Path.GetFullPath(Assembly.GetExecutingAssembly().CodeBase.Substring(10))).Substring(0, Path.GetFullPath(Assembly.GetExecutingAssembly().CodeBase.Substring(10)).Length - 14);
        }

        public static string GetAbsoluteImagePath()
        {
            return configurations.GetAbsolutePath() + configurations.image_path.Substring(2);
        }

        public static string GetAbsoluteAvatarPath()
        {
            return configurations.GetAbsolutePath() + configurations.avatar_path.Substring(2);
        }

        public static string GetAbsoluteThumbnailPath()
        {
            return configurations.GetAbsolutePath() + configurations.thumbnails_path.Substring(2);
        }

        public static string GetAbsoluteContributionPath()
        {
            return configurations.GetAbsolutePath() + configurations.contributions_path.Substring(2);
        }
		
        public static string GetAbsoluteConfigFilePath()
        {
            return configurations.GetAbsolutePath() + configurations.config_file;
        }

        public static string GetAbsoluteLogFilePath()
        {
            return configurations.GetAbsolutePath() + configurations.log_file;
        }
		
        public static void LoadIconImages()
        {
            img_background_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + background_pic));
            img_drop_avatar_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + drop_avatar_pic));
            img_loading_image_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + loading_image_pic));
            img_empty_image_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + empty_image_pic));
            img_not_found_image_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + not_found_image_pic));
            img_sound_image_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + sound_image_pic));
            img_video_image_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + video_image_pic));
            img_keyboard_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + keyboard_pic));
            img_close_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + close_icon));
            img_collection_window_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + collection_window_icon));
            img_signup_window_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + signup_window_icon));
            img_change_view_list_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + change_view_list_icon));
            img_change_view_stack_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + change_view_stack_icon));
            img_signup_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + signup_icon));
            img_submit_idea_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + submit_idea_icon));
            img_thumbs_up_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + thumbs_up_icon));
            img_thumbs_down_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + thumbs_down_icon));
            img_thumbs_down_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + thumbs_down_icon));
            img_drag_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + drag_icon));
        }

        public static string GetDate_Formatted(DateTime dt)
        {
            string r = dt.Day.ToString() + " " + GetMonthName(dt.Month) + " " + dt.Year.ToString();
            return r;
        }

        public static DateTime? GetDate_FromFormatted(string formatted)
        {
            string[] parts = formatted.Split(new Char[] { ' ' });
            if (parts.Count() != 3) return null;
            DateTime dt = new DateTime(Convert.ToInt32(parts[2]), GetMonth_FromName(parts[1]), Convert.ToInt32(parts[0]));
            return dt;
        }

        public static string GetCurrentDate_Formatted()
        {
            DateTime dt = DateTime.Now;
            string r = dt.Day.ToString() + " " + GetMonthName(dt.Month) + " " + dt.Year.ToString();
            return r;
        }

        public static String GetMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "Jan";
                case 2:
                    return "Feb";
                case 3:
                    return "Mar";
                case 4:
                    return "Apr";
                case 5:
                    return "May";
                case 6:
                    return "Jun";
                case 7:
                    return "Jul";
                case 8:
                    return "Aug";
                case 9:
                    return "Sep";
                case 10:
                    return "Oct";
                case 11:
                    return "Nov";
                case 12:
                    return "Dec";
                default:
                    return "Unknown";
            }
        }

        public static int GetMonth_FromName(string month)
        {
            if (month == "Jan") return 1;
            if (month == "Feb") return 2;
            if (month == "Mar") return 3;
            if (month == "Apr") return 4;
            if (month == "May") return 5;
            if (month == "Jun") return 6;
            if (month == "Jul") return 7;
            if (month == "Aug") return 8;
            if (month == "Sep") return 9;
            if (month == "Oct") return 10;
            if (month == "Nov") return 11;
            if (month == "Dec") return 12;
            return -1;
        }

        public static ImageSource GetThumbnailFromImage(string filename, int height)
        {
            BitmapImage bi = new BitmapImage();
            try
            {
                // create the thumbnail
                bi.BeginInit();
                bi.DecodePixelHeight = height;
                //bi.DecodePixelWidth = width;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.UriSource = new Uri(configurations.GetAbsoluteContributionPath() + filename);
                bi.EndInit();
                bi.Freeze();
            }
            catch (Exception)
            {
                // could not create thumbnail -- reason: filenotfound or currupt download or ...
                // write log
                return null;
            }
            return bi;
        }

        public static ImageSource GetThumbnailFromVideo(string filename, TimeSpan interval, int height)
        {
            //configurations.the_flag++;
            MediaPlayer _mediaPlayer = new MediaPlayer();
            _mediaPlayer.MediaOpened += new EventHandler(configurations._mediaPlayer_MediaOpened);
            _mediaPlayer.BufferingEnded += new EventHandler(configurations._mediaPlayer_MediaOpened);
            _mediaPlayer.ScrubbingEnabled = true;
            _mediaPlayer.Open(new Uri(configurations.GetAbsoluteContributionPath() + filename));
            _mediaPlayer.Pause();
            //_mediaPlayer.Position = interval;
            System.Threading.Thread.Sleep(5 * 1000);

            //while (the_flag != 0) { System.Threading.Thread.Sleep(1000); }
            _mediaPlayer.Position = interval;
            ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + video_image_pic));
            src.Freeze();
            //uint[] framePixels = new uint[width * height];
            // Render the current frame into a bitmap
            var drawingVisual = new DrawingVisual();
            try
            {
                int width = height;
                if (_mediaPlayer.NaturalVideoWidth * _mediaPlayer.NaturalVideoHeight != 0)
                    width = height * _mediaPlayer.NaturalVideoWidth / _mediaPlayer.NaturalVideoHeight;
                using (var drawingContext = drawingVisual.RenderOpen())
                {
                    drawingContext.DrawVideo(_mediaPlayer, new System.Windows.Rect(0, 0, width, height));
                    drawingContext.DrawImage(src, new System.Windows.Rect(0, 0, width, height));
                    //drawingContext.DrawVideo(_mediaPlayer, new System.Windows.Rect(0, 0, height, height));
                    //drawingContext.DrawImage(src, new System.Windows.Rect(0, 0, height, height));
                }
                var renderTargetBitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Default);
                //var renderTargetBitmap = new RenderTargetBitmap(height, height, 96, 96, PixelFormats.Default);
                renderTargetBitmap.Render(drawingVisual);
                return (ImageSource)renderTargetBitmap;
            }
            catch (Exception exc) { log.WriteErrorLog(exc); return null; }
        }

        public static void _mediaPlayer_MediaOpened(object sender, EventArgs e)
        {
            //configurations.the_flag--;
        }

        public static void SaveThumbnail(BitmapSource src, string filename)
        {
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(src));
            using (var fs = new FileStream(configurations.GetAbsoluteThumbnailPath() + filename + ".jpg", FileMode.Create))
            {
                encoder.Save(fs);
            }
        }

        public static Visual GetDescendantByType(Visual element, Type type)
        {
            if (element == null)
            {
                return null;
            }
            if (element.GetType() == type)
            {
                return element;
            }
            Visual foundElement = null;
            if (element is FrameworkElement)
            {
                (element as FrameworkElement).ApplyTemplate();
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByType(visual, type);
                if (foundElement != null)
                {
                    break;
                }
            }
            return foundElement;
        }

        public static string GetItemFromJSON(string whole, string key)
        {
            if (whole.Length < 3) return "";
            string data = whole.Substring(1, whole.Length - 2);
            string[] items = data.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in items)
            {
                string[] values = item.Split(new Char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Count() < 2) continue;
                if (values[0].Length < 3) continue;
                string pkey = values[0].Substring(1, values[0].Length - 2);
                if (pkey == key)
                {
                    string value = "";
                    if (!values[1].Contains('"'))
                    {
                        value = values[1].Trim();
                        return value;
                    }
                    if (values[1].Length < 3) return "";
                    value = values[1].Substring(1, values[1].Length - 2);
                    return value;
                }
            }
            return "";
        }

        public static List<string> GetUserNameList_GDText(string text_file)
        {
            List<string> usernames = new List<string>();
            string[] lines = text_file.Split(new Char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] parts = line.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Count() < 2) { usernames.Add(""); continue; }
                string[] values = parts[1].Split(new Char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Count() < 2) { usernames.Add(""); continue; }
                usernames.Add(values[1].Trim());
            }
            return usernames;
        }

        public static List<string> GetAvatarList_GDText(string text_file)
        {
            List<string> avatars = new List<string>();
            string[] lines = text_file.Split(new Char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] parts = line.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Count() < 3) { avatars.Add(""); continue; }
                string[] values = parts[2].Split(new Char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Count() < 2) { avatars.Add(""); continue; }
                string value = values[1].Trim();
                value = value.Substring(0, value.Length - 1);
                if (value.Substring(value.Length - 4, 4) != ".png")
                    value = value + ".png";
                avatars.Add(value);
            }
            return avatars;
        }

        public static string GetItem_GDText(string text_record, int col)
        {
            text_record = text_record.Substring(1, text_record.Length - 2);
            //List<string> names = new List<string>();
            //string[] lines = text_file.Split(new Char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string name = "";
            //foreach (string line in lines)
            //{
            string[] parts = text_record.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Count() < col + 1) { name = ""; }
            string[] values = parts[col].Split(new Char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            if (values.Count() < 2) { name = ""; }
            name = values[1].Trim();
            //}
            return name;
        }

        public static int get_or_create_collection(naturenet_dataclassDataContext db, int user_id, int activity_id, DateTime dt)
        {
            var r = from c in db.Collections
                    where ((c.user_id == user_id) && c.activity_id == activity_id)
                    orderby c.date descending
                    select c;
            if (r.Count() != 0)
            {
                foreach (Collection col in r)
                {
                    if (configurations.GetDate_Formatted(col.date) == configurations.GetDate_Formatted(dt))
                        return col.id;
                }
            }

            // create new collection
            Collection cl = new Collection();
            cl.activity_id = activity_id;
            cl.date = dt;
            cl.name = configurations.GetDate_Formatted(dt);
            cl.user_id = user_id;
            db.Collections.InsertOnSubmit(cl);
            db.SubmitChanges();
            return cl.id;
        }

        public static int get_or_create_collection(naturenet_dataclassDataContext db, string user_name, string avatar, int activity_id, DateTime dt)
        {
            int user_id =0;
            var ru = from u in db.Users
                     where u.name == user_name
                     select u;
            if (ru.Count() == 0)
            {
                User u0 = new User();
                u0.name = user_name; u0.avatar = avatar;
                u0.password = ""; u0.email = "";
                db.Users.InsertOnSubmit(u0);
                db.SubmitChanges();
                user_id = u0.id;
            }
            else
            {
                user_id = ru.First<User>().id;
            }

            var r = from c in db.Collections
                    where ((c.user_id == user_id) && c.activity_id == activity_id)
                    orderby c.date descending
                    select c;
            if (r.Count() != 0)
            {
                foreach (Collection col in r)
                {
                    if (configurations.GetDate_Formatted(col.date) == configurations.GetDate_Formatted(dt))
                        return col.id;
                }
            }

            // create new collection
            Collection cl = new Collection();
            cl.activity_id = activity_id;
            cl.date = dt;
            cl.name = configurations.GetDate_Formatted(dt);
            cl.user_id = user_id;
            db.Collections.InsertOnSubmit(cl);
            db.SubmitChanges();
            return cl.id;
        }

        public static void SetSettingsFromConfig(iniparser parser)
        {
            // locations in MJ's laptop
            //Point p1 = new Point(120, 382); locations.Add(p1);
            //Point p2 = new Point(140, 420); locations.Add(p2);
            //Point p3 = new Point(290, 230); locations.Add(p3);
            //Point p4 = new Point(400, 300); locations.Add(p4);
            //Point p5 = new Point(405, 480); locations.Add(p5);
            //Point p6 = new Point(480, 595); locations.Add(p6);
            //Point p7 = new Point(515, 690); locations.Add(p7);
            //Point p8 = new Point(465, 755); locations.Add(p8);
            //Point p9 = new Point(415, 755); locations.Add(p9);
            //Point p10 = new Point(180, 545); locations.Add(p10);
            //Point p11 = new Point(150, 570); locations.Add(p11);

            // Locations (in tabletop)
            Point p1 = new Point(parser.GetValue("Locations", "P1X", 569 - 240), parser.GetValue("Locations", "P1Y", 699 - 20)); locations.Add(p1);
            Point p2 = new Point(parser.GetValue("Locations", "P2X", 615 - 240), parser.GetValue("Locations", "P2Y", 783 - 20)); locations.Add(p2);
            Point p3 = new Point(parser.GetValue("Locations", "P3X", 989 - 240), parser.GetValue("Locations", "P3Y", 439 - 20)); locations.Add(p3);
            Point p4 = new Point(parser.GetValue("Locations", "P4X", 1264 - 230), parser.GetValue("Locations", "P4Y", 558 - 15)); locations.Add(p4);
            Point p5 = new Point(parser.GetValue("Locations", "P5X", 1275 - 240), parser.GetValue("Locations", "P5Y", 888 - 20)); locations.Add(p5);
            Point p6 = new Point(parser.GetValue("Locations", "P6X", 1476 - 230), parser.GetValue("Locations", "P6Y", 1098 - 20)); locations.Add(p6);
            Point p7 = new Point(parser.GetValue("Locations", "P7X", 1554 - 240), parser.GetValue("Locations", "P7Y", 1253 - 10)); locations.Add(p7);
            Point p8 = new Point(parser.GetValue("Locations", "P8X", 1438 - 230), parser.GetValue("Locations", "P8Y", 1375 - 10)); locations.Add(p8);
            Point p9 = new Point(parser.GetValue("Locations", "P9X", 1310 - 230), parser.GetValue("Locations", "P9Y", 1375 - 10)); locations.Add(p9);
            Point p10 = new Point(parser.GetValue("Locations", "P10X", 724 - 240), parser.GetValue("Locations", "P10Y", 985 - 20)); locations.Add(p10);
            Point p11 = new Point(parser.GetValue("Locations", "P11X", 630 - 240), parser.GetValue("Locations", "P11Y", 1036 - 20)); locations.Add(p11);
            location_dot_diameter = parser.GetValue("Locations", "location_dot_diameter", 55);
            location_dot_color = parser.GetValue("Locations", "location_dot_color", Brushes.Crimson);
            location_dot_outline_color = parser.GetValue("Locations", "location_dot_outline_color", Brushes.Crimson);
            location_dot_font_color = parser.GetValue("Locations", "location_dot_font_color", Brushes.White);

            // General variables
            //line_break = parser.GetValue("General", "line_break", "\r\n");
            log_file = parser.GetValue("General", "log_file", "log.txt");
            contribution_comment_date = parser.GetValue("General", "contribution_comment_date", "Taken by: ");
            contribution_comment_tag = parser.GetValue("General", "contribution_comment_tag", "Tags: ");
            contribution_comment_location = parser.GetValue("General", "contribution_comment_location", "Location ");
            designidea_date_desc = parser.GetValue("General", "designidea_date_desc", "Last Update: ");
            designidea_num_desc = parser.GetValue("General", "designidea_num_desc", "Comments");
            users_date_desc = parser.GetValue("General", "users_date_desc", "Last Update: ");
            users_num_desc = parser.GetValue("General", "users_num_desc", "Contributions");
            users_no_date = parser.GetValue("General", "users_no_date", "Just Created");
            activities_date_desc = parser.GetValue("General", "activities_date_desc", "Last Update: ");
            activities_num_desc = parser.GetValue("General", "activities_num_desc", "Contributions");

            // Parameters
            high_contrast = parser.GetValue("Parameters", "high_contrast", false);
            top_most = parser.GetValue("Parameters", "top_most", false);
            show_update_label = parser.GetValue("Parameters", "show_update_label", false);
            response_to_mouse_clicks = parser.GetValue("Parameters", "response_to_mouse_clicks", true);
            max_num_content_update = parser.GetValue("Parameters", "max_num_content_update", 12);
            max_signup_frame = parser.GetValue("Parameters", "max_signup_frame", 5);
            max_collection_frame = parser.GetValue("Parameters", "max_collection_frame", 10);
            max_image_display_frame = parser.GetValue("Parameters", "max_image_display_frame", 10);
            max_design_ideas_frame = parser.GetValue("Parameters", "max_design_ideas_frame", 10);
            max_activity_frame = parser.GetValue("Parameters", "max_activity_frame", 10);
            max_activity_frame_title_chars = parser.GetValue("Parameters", "max_activity_frame_title_chars", 10);
            //thumbnail_pixel_width = parser.GetValue("Parameters", "thumbnail_pixel_width",100);
            thumbnail_pixel_height = parser.GetValue("Parameters", "thumbnail_pixel_height", 100);
            thumbnail_video_span = new TimeSpan(0, 0, parser.GetValue("Parameters", "thumbnail_video_span_seconds", 2));
            use_existing_thumbnails = parser.GetValue("Parameters", "use_existing_thumbnails", true);
            drag_dy_dx_factor = parser.GetValue("Parameters", "drag_dy_dx_factor", 2.1);
            //drag_dx_dy_factor = parser.GetValue("Parameters", "drag_dx_dy_factor",1.0);
            drag_collection_theta = parser.GetValue("Parameters", "drag_collection_theta", 5);
            scroll_scale_factor = parser.GetValue("Parameters", "scroll_scale_factor", 5);
            min_touch_points = parser.GetValue("Parameters", "min_touch_points", 2);
            max_consecutive_drag_points = parser.GetValue("Parameters", "max_consecutive_drag_points", 5);
            tap_error = parser.GetValue("Parameters", "tap_error", 5);
            use_avatar_drag = parser.GetValue("Parameters", "use_avatar_drag", false);
            tab_width_percentage = parser.GetValue("Parameters", "tab_width_percentage", 17);
            manual_scroll = parser.GetValue("Parameters", "manual_scroll", false);
            manual_tap = parser.GetValue("Parameters", "manual_tap", false);
            right_panel_drag = parser.GetValue("Parameters", "right_panel_drag", true);
            whole_item_drag = parser.GetValue("Parameters", "whole_item_drag", false);

            // Google Drive
            googledrive_directory_id = parser.GetValue("GoogleDrive", "googledrive_directory_id", "0B9mU-w_CpbztUUxtaXVIeE9SbWM");
            googledrive_client_id = parser.GetValue("GoogleDrive", "googledrive_client_id", "333780750675-ag76kpq3supbbqi3v92vn3ejil8ght23.apps.googleusercontent.com");
            googledrive_client_secret = parser.GetValue("GoogleDrive", "googledrive_client_secret", "bCYIAfrAC0i-qIfl0cLRnhwn");
            googledrive_storage = parser.GetValue("GoogleDrive", "googledrive_storage", "gdrive_uploader");
            googledrive_key = parser.GetValue("GoogleDrive", "googledrive_key", "z},drdzf11x9;87");
            googledrive_refresh_token = parser.GetValue("GoogleDrive", "googledrive_refresh_token", "1/jpJHu8br2TnnM5hwCqgFe-yagf6zixlDZrlUvdXZ9s8");
            googledrive_lastchange = parser.GetValue("GoogleDrive", "googledrive_lastchange", configurations.googledrive_lastchange);
            googledrive_userfilename = parser.GetValue("GoogleDrive", "googledrive_userfilename", "Users.txt");
            googledrive_userfiletitle = parser.GetValue("GoogleDrive", "googledrive_userfiletitle", "Users");
            googledrive_ideafilename = parser.GetValue("GoogleDrive", "googledrive_ideafilename", "Ideas.txt");
            googledrive_ideafiletitle = parser.GetValue("GoogleDrive", "googledrive_ideafiletitle", "Ideas");
            googledrive_update_min = parser.GetValue("GoogleDrive", "googledrive_update_min", 2);
            googledrive_update_sec = parser.GetValue("GoogleDrive", "googledrive_update_sec", 30);
            download_buffer_size = parser.GetValue("GoogleDrive", "download_buffer_size", 10240); // 10KB = 10 * 1024

            // Paths
            image_path = parser.GetValue("Paths", "image_path", ".\\images\\");
            avatar_path = parser.GetValue("Paths", "avatar_path", ".\\images\\avatars\\");
            thumbnails_path = parser.GetValue("Paths", "thumbnails_path", ".\\images\\thumbnails\\");
            contributions_path = parser.GetValue("Paths", "contributions_path", ".\\images\\contributions\\");

            // Files
            background_pic = parser.GetValue("Files", "background_pic", "background.png");
            drop_avatar_pic = parser.GetValue("Files", "drop_avatar_pic", "drop_avatar.png");
            loading_image_pic = parser.GetValue("Files", "loading_image_pic", "loading_image.png");
            empty_image_pic = parser.GetValue("Files", "empty_image_pic", "empty_image.png");
            not_found_image_pic = parser.GetValue("Files", "not_found_image_pic", "not_found_image.png");
            sound_image_pic = parser.GetValue("Files", "sound_image_pic", "sound_image.png");
            video_image_pic = parser.GetValue("Files", "video_image_pic", "film.png");
            keyboard_pic = parser.GetValue("Files", "keyboard_pic", "keyboard.png");
            close_icon = parser.GetValue("Files", "close_icon", "close.png");
            change_view_list_icon = parser.GetValue("Files", "change_view_list_icon", "change_view_list.png");
            change_view_stack_icon = parser.GetValue("Files", "change_view_stack_icon", "change_view_stack.png");
            collection_window_icon = parser.GetValue("Files", "collection_window_icon", "collection_window_icon.png");
            signup_icon = parser.GetValue("Files", "signup_icon", "signup.png");
            signup_window_icon = parser.GetValue("Files", "signup_window_icon", "signup_window_icon.png");
            submit_idea_icon = parser.GetValue("Files", "submit_idea_icon", "submit_idea.png");
            thumbs_up_icon = parser.GetValue("Files", "thumbs_up_icon", "tu.jpg");
            thumbs_down_icon = parser.GetValue("Files", "thumbs_down_icon", "td.jpg");
            drag_icon = parser.GetValue("Files", "drag_icon", "drag.png");
            keyboard_click_wav = parser.GetValue("Files", "keyboard_click_wav", "click.wav");

            // Frame
            frame_width = parser.GetValue("Frame", "frame_width", 300);
            frame_title_bar_height = parser.GetValue("Frame", "frame_title_bar_height", 40);
            frame_icon_width = parser.GetValue("Frame", "frame_icon_width", 40);

            // Item
            toolbar_item_width = parser.GetValue("Item", "toolbar_item_width", 30);
        }

        public static void SaveConfigurations(iniparser parser)
        {
            parser.SetValue("Locations", "location_dot_diameter", location_dot_diameter);
            parser.SetValue("Locations", "location_dot_color", location_dot_color);
            parser.SetValue("Locations", "location_dot_outline_color", location_dot_outline_color);
            parser.SetValue("Locations", "location_dot_font_color", location_dot_font_color);

            // General variables
            //parser.SetValue("General", "line_break", "\"" + line_break.ToString() + "\"");
            parser.SetValue("General", "log_file", log_file);
            parser.SetValue("General", "contribution_comment_date", contribution_comment_date);
            parser.SetValue("General", "contribution_comment_tag", contribution_comment_tag);
            parser.SetValue("General", "contribution_comment_location", contribution_comment_location);
            parser.SetValue("General", "designidea_date_desc", designidea_date_desc);
            parser.SetValue("General", "designidea_num_desc", designidea_num_desc);
            parser.SetValue("General", "users_date_desc", users_date_desc);
            parser.SetValue("General", "users_num_desc", users_num_desc);
            parser.SetValue("General", "users_no_date", users_no_date);
            parser.SetValue("General", "activities_date_desc", activities_date_desc);
            parser.SetValue("General", "activities_num_desc", activities_num_desc);

            // Parameters
            parser.SetValue("Parameters", "high_contrast", high_contrast);
            parser.SetValue("Parameters", "top_most", top_most);
            parser.SetValue("Parameters", "show_update_label", show_update_label);
            parser.SetValue("Parameters", "response_to_mouse_clicks", response_to_mouse_clicks);
            parser.SetValue("Parameters", "max_num_content_update", max_num_content_update);
            parser.SetValue("Parameters", "max_signup_frame", max_signup_frame);
            parser.SetValue("Parameters", "max_collection_frame", max_collection_frame);
            parser.SetValue("Parameters", "max_image_display_frame", max_image_display_frame);
            parser.SetValue("Parameters", "max_design_ideas_frame", max_design_ideas_frame);
            parser.SetValue("Parameters", "max_activity_frame", max_activity_frame);
            parser.SetValue("Parameters", "max_activity_frame_title_chars", max_activity_frame_title_chars);
            //parser.SetValue("Parameters", "thumbnail_pixel_width",thumbnail_pixel_width);
            parser.SetValue("Parameters", "thumbnail_pixel_height", thumbnail_pixel_height);
            parser.SetValue("Parameters", "thumbnail_video_span_seconds", thumbnail_video_span.Seconds);
            parser.SetValue("Parameters", "use_existing_thumbnails", use_existing_thumbnails);
            parser.SetValue("Parameters", "drag_dy_dx_factor", drag_dy_dx_factor);
            //parser.SetValue("Parameters", "drag_dx_dy_factor",drag_dx_dy_factor);
            parser.SetValue("Parameters", "drag_collection_theta", drag_collection_theta);
            parser.SetValue("Parameters", "scroll_scale_factor", scroll_scale_factor);
            parser.SetValue("Parameters", "min_touch_points", min_touch_points);
            parser.SetValue("Parameters", "max_consecutive_drag_points", max_consecutive_drag_points);
            parser.SetValue("Parameters", "tap_error", tap_error);
            parser.SetValue("Parameters", "use_avatar_drag", use_avatar_drag);
            parser.SetValue("Parameters", "tab_width_percentage", tab_width_percentage);
            parser.SetValue("Parameters", "manual_scroll", manual_scroll);
            parser.SetValue("Parameters", "manual_tap", manual_tap);
            parser.SetValue("Parameters", "right_panel_drag", right_panel_drag);
            parser.SetValue("Parameters", "whole_item_drag", whole_item_drag);

            // Google Drive
            parser.SetValue("GoogleDrive", "googledrive_directory_id", googledrive_directory_id);
            parser.SetValue("GoogleDrive", "googledrive_client_id", googledrive_client_id);
            parser.SetValue("GoogleDrive", "googledrive_client_secret", googledrive_client_secret);
            parser.SetValue("GoogleDrive", "googledrive_storage", googledrive_storage);
            parser.SetValue("GoogleDrive", "googledrive_key", googledrive_key);
            parser.SetValue("GoogleDrive", "googledrive_refresh_token", googledrive_refresh_token);
            parser.SetValue("GoogleDrive", "googledrive_lastchange", googledrive_lastchange);
            parser.SetValue("GoogleDrive", "googledrive_userfilename", googledrive_userfilename);
            parser.SetValue("GoogleDrive", "googledrive_userfiletitle", googledrive_userfiletitle);
            parser.SetValue("GoogleDrive", "googledrive_ideafilename", googledrive_ideafilename);
            parser.SetValue("GoogleDrive", "googledrive_ideafiletitle", googledrive_ideafiletitle);
            parser.SetValue("GoogleDrive", "googledrive_update_min", googledrive_update_min);
            parser.SetValue("GoogleDrive", "googledrive_update_sec", googledrive_update_sec);
            parser.SetValue("GoogleDrive", "download_buffer_size", download_buffer_size); // 10KB = 10 * 1024

            // Paths
            parser.SetValue("Paths", "image_path", image_path);
            parser.SetValue("Paths", "avatar_path", avatar_path);
            parser.SetValue("Paths", "thumbnails_path", thumbnails_path);
            parser.SetValue("Paths", "contributions_path", contributions_path);

            // Files
            parser.SetValue("Files", "background_pic", background_pic);
            parser.SetValue("Files", "drop_avatar_pic", drop_avatar_pic);
            parser.SetValue("Files", "loading_image_pic", loading_image_pic);
            parser.SetValue("Files", "empty_image_pic", empty_image_pic);
            parser.SetValue("Files", "not_found_image_pic", not_found_image_pic);
            parser.SetValue("Files", "sound_image_pic", sound_image_pic);
            parser.SetValue("Files", "video_image_pic", video_image_pic);
            parser.SetValue("Files", "keyboard_pic", keyboard_pic);
            parser.SetValue("Files", "close_icon", close_icon);
            parser.SetValue("Files", "change_view_list_icon", change_view_list_icon);
            parser.SetValue("Files", "change_view_stack_icon", change_view_stack_icon);
            parser.SetValue("Files", "collection_window_icon", collection_window_icon);
            parser.SetValue("Files", "signup_icon", signup_icon);
            parser.SetValue("Files", "signup_window_icon", signup_window_icon);
            parser.SetValue("Files", "submit_idea_icon", submit_idea_icon);
            parser.SetValue("Files", "thumbs_up_icon", thumbs_up_icon);
            parser.SetValue("Files", "thumbs_down_icon", thumbs_down_icon);
            parser.SetValue("Files", "keyboard_click_wav", keyboard_click_wav);
            parser.SetValue("Files", "drag_icon", drag_icon);

            // Frame
            parser.SetValue("Frame", "frame_width", frame_width);
            parser.SetValue("Frame", "frame_title_bar_height", frame_title_bar_height);
            parser.SetValue("Frame", "frame_icon_width", frame_icon_width);

            // Item
            parser.SetValue("Item", "toolbar_item_width", toolbar_item_width);

            parser.Save(GetAbsoluteConfigFilePath());
        }

        public static void SaveChangeID()
        {
            iniparser parser = new iniparser(GetAbsoluteConfigFilePath());
            parser.SetValue("GoogleDrive", "googledrive_lastchange", configurations.googledrive_lastchange);
            parser.Save(GetAbsoluteConfigFilePath());
        }

        public static int GetNumberFromItemGeneric(nature_net.user_controls.item_generic_v2 item)
        {
            //string text = item.number.Text.Substring(1);
            //string[] parts = text.Split(new Char[] { ' ' });
            //if (parts.Count() != 2) return 0;
            int a = 0;
            //try { a = Convert.ToInt32(parts[0]); }
            //catch (Exception) { return 0; }
            a = item.top_value;
            return a;
        }

        public static bool IsFirstItemGreaterThanSecond(nature_net.user_controls.item_generic_v2 first, nature_net.user_controls.item_generic_v2 second, bool atoz, bool top, bool recent, int len_number_prefix, int len_date_prefix, bool asc)
        {
            if (atoz)
                if (first.title.Text.CompareTo(second.title.Text) > 0) return !asc;
                else return asc;
            if (top)
            {
                if (GetNumberFromItemGeneric(first) > GetNumberFromItemGeneric(second)) return asc;
                else return !asc;
            }
            if (recent)
            {
                if (!GetDate_FromFormatted(first.user_info_date.Text.ToString()).HasValue) return !asc;
                if (!GetDate_FromFormatted(second.user_info_date.Text.ToString()).HasValue) return asc;
                if (GetDate_FromFormatted(first.user_info_date.Text.ToString()).Value.CompareTo(GetDate_FromFormatted(second.user_info_date.Text.ToString()).Value) > 0)
                    return asc;
                else
                    return !asc;
            }
            return false;
        }

        public static void SortItemGenericList(System.Windows.Controls.ItemCollection _list, bool atoz, bool top, bool recent, int len_number_prefix, int len_date_prefix, bool asc, bool consider_first_item)
        {
            List<nature_net.user_controls.item_generic_v2> new_list = new List<user_controls.item_generic_v2>();
            int init = 0; int j = 0;
            if (!consider_first_item) 
            {
                init = 1;
                new_list.Add((nature_net.user_controls.item_generic_v2)_list[0]);
                j=1;
            }
            int i = init;
            new_list.Add((nature_net.user_controls.item_generic_v2)_list[j]); j++;
            for (; j < _list.Count; j++)
            {
                nature_net.user_controls.item_generic_v2 current = (nature_net.user_controls.item_generic_v2)_list[j];
                i = init;
                while (i < new_list.Count && IsFirstItemGreaterThanSecond(current, (nature_net.user_controls.item_generic_v2)new_list[i], atoz, top, recent, len_number_prefix, len_date_prefix, !asc))
                    i++;
                if (i < new_list.Count)
                    new_list.Insert(i, current);
                else
                    new_list.Add(current);
            }
            _list.Clear();
            foreach (object o in new_list)
                _list.Add(o);

            //for (int i = init; i < _list.Count; i++)
            //    for (int j = i + 1; j < _list.Count; j++)
            //        if (IsFirstItemGreaterThanSecond((nature_net.user_controls.item_generic)_list[j], (nature_net.user_controls.item_generic)_list[i], atoz, top, recent, len_number_prefix, len_date_prefix, asc))
            //        {
            //            nature_net.user_controls.item_generic item1 = ((nature_net.user_controls.item_generic)_list[i]).get_clone();
            //            nature_net.user_controls.item_generic item2 = ((nature_net.user_controls.item_generic)_list[j]).get_clone();
            //            _list.Remove(_list[i]);
            //            _list.Insert(i, item2);
            //            _list.Remove(_list[j]);
            //            _list.Insert(j, item1);
            //            //nature_net.user_controls.item_generic item1 = ((nature_net.user_controls.item_generic)_list[i]);
            //            //nature_net.user_controls.item_generic item2 = ((nature_net.user_controls.item_generic)_list[j]).get_clone();
            //            //_list[i] = _list[j];
            //            //_list[j] = item1;
            //            //_list.Remove(_list[i]);
            //            //_list.Insert(i, item2);
            //            //_list.Remove(_list[j]);
            //            //_list.Insert(j, item1);
            //        }
        }
    }
}
