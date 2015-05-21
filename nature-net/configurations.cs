using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using nature_net.user_controls;
using Newtonsoft.Json;
using System.Windows.Controls;

namespace nature_net
{
    public class configurations
    {
        public static string application_name = "";
        static string config_file = "config.ini";
        public static string line_break = "\r\n";
        static string log_file = "log.txt";
        public static string site_name = "aces";
        public static bool write_interaction_log = false;
        public static string contribution_comment_date = "On ";
        public static string contribution_comment_user = "Taken by ";
        public static string contribution_comment_tag = "Tags: ";
        public static string contribution_comment_location = "At ";
        public static string designidea_date_desc = "Last Update: ";
        public static string designidea_num_desc = "replies";
        public static string users_date_desc = "Last Update: ";
        public static string users_num_desc = "Contributions";
        public static string users_no_date = "Just Created"; 
        public static string activities_date_desc = "Last Update: ";
        public static string activities_num_desc = "Contributions";
        public static string authentication_failed_text = "Whoops! That PIN was incorrect. Please try again.";
        public static string frame_title_user_collection = "'s Observations";
        public static string frame_title_location_collection = "Observations";
        public static string frame_title_activity_collection = "";
        public static string affiliation_aces = "ACES";
        public static string status_implemented = "implemented";
        public static string status_deleted = "deleted";
        public static string implemented_text = "[Implemented] ";
        public static string still_uploading_text = "Still Uploading (Tap to retry)";
        public static string status_initial_string = "";
        public static string default_user_text = "Anonymous";
        public static string default_user_desc = " (via web)";
        public static string choose_user_text = "Drag your avatar and drop it here.";
        public static string choose_activity_text = "Drag a design idea category and drop it here.";
        public static string help_text = "Help";

        public static string help_file_drag_user = "help1.mp4";
        public static string help_text_drag_user = "Draggin user to open a collection";
        public static string help_file_drag_designidea = "help1.mp4";
        public static string help_text_drag_designidea = "Draggin user to open a collection";

        public static string signup_item_title = "Sign up";
        public static string submit_idea_item_title = "Submit Idea";
        public static string submit_idea_activity_item_title = "Submit Idea";

        public static string design_idea_listbox_header = "Design Ideas sorted by";
        public static string users_listbox_header = "Users sorted by";
        public static string activities_listbox_header = "Activities sorted by";

        public static string design_idea_lisbox_top_text = "Popular";

        public static string comment_init_text = "Add Comment";
        public static string design_idea_init_text = "Design Idea";

        public static bool high_contrast = false;
        public static bool top_most = false;
        public static bool show_update_label = false;
        public static bool response_to_mouse_clicks = true;
        public static bool show_help = true;
        public static bool enable_single_rotation = true;

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
        public static double idea_text_scale_factor = 0.68;
        //public static double drag_dx_dy_factor = 1.0;
        public static int max_thread_reply = 3;
        public static int kill_window_millisec = 5000;

        public static double drag_collection_theta = 5;
        public static double scroll_scale_factor = 5;
        public static int min_touch_points = 2;
        public static int max_consecutive_drag_points = 5;
        public static double tap_error = 1;
        public static int tab_width_percentage = 17;
        public static double manipulation_pivot_radius = 20.0;
        public static BitmapScalingMode scaling_mode = BitmapScalingMode.Fant;
        public static double click_opacity_on_collection_item = 0.8;
        public static double collection_listbox_background_gradient_point = 0.8;
        //public static int tab_header_width = 65;
        public static bool manual_scroll = false;
        public static bool manual_tap = false;
        public static bool right_panel_drag = true;
        public static bool whole_item_drag = false;
        public static bool center_commentarea_and_keyboard = false;
        public static bool multi_keyboard = false;
        public static bool show_vertical_drag = true;
        public static bool show_empty_metadata = false;
        public static bool show_all_metadata = false;
        public static bool use_avatar_drag = false;
        public static bool use_list_refresher = false;

        public static List<Point> locations = new List<Point>();
        public static int location_dot_diameter = 55;
        public static Brush location_dot_color = Brushes.Crimson;
		public static Brush location_dot_outline_color = Brushes.Crimson;
        public static Brush location_dot_font_color = Brushes.White;
        //public static Brush contribution_view_title_bar_color = Color.FromArgb(

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
        public static int update_period_ms = 20000;

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
        static string keyboard_pic = "NN_Keyboard_v2.png";
        static string keyboard_shift_pic = "NN_Keyboard_v2_shift.png";
        static string keyboard_caps_pic = "NN_Keyboard_v2_caps.png";
        static string keyboard_numpad_pic = "NN_Numpad.png";
        static string choose_avatar_pic = "choose_avatar.png";
        static string choose_user_pic = "choose_user.png";
        static string choose_activity_pic = "choose_activity.png";
        static string close_icon = "close.png";
        static string pushpin_icon = "NN_PinButton.png";
        static string pushpin_selected_icon = "NN_PinButton_selected.png";
        static string change_view_stack_icon = "change_view_stack.png";
        static string collection_window_icon = "collection_window_icon.png";
        static string signup_icon = "signup.png";
        static string signup_window_icon = "signup_window_icon.png";
        static string submit_idea_icon = "submit_idea.png";
        static string thumbs_up_icon = "tu.jpg";
        static string thumbs_down_icon = "td.jpg";
        static string drag_icon = "drag.png";
        static string drag_vertical_icon = "drag_vertical.png";
        static string comment_icon = "comment.png";
        static string reply_icon = "reply.png";
        static string affiliation_icon = "affiliation.png";
        static string implemented_icon = "implemented.png";

        public static string keyboard_click_wav = "click.wav";

        public static ImageSource img_background_pic;
        public static ImageSource img_drop_avatar_pic;
        public static ImageSource img_loading_image_pic;
        public static ImageSource img_empty_image_pic;
        public static ImageSource img_not_found_image_pic;
        public static ImageSource img_sound_image_pic;
        public static ImageSource img_video_image_pic;
        public static ImageSource img_keyboard_pic;
        public static ImageSource img_keyboard_shift_pic;
        public static ImageSource img_keyboard_caps_pic;
        public static ImageSource img_keyboard_numpad_pic;
        public static ImageSource img_choose_avatar_pic;
        public static ImageSource img_choose_user_pic;
        public static ImageSource img_choose_activity_pic;
        public static ImageSource img_close_icon;
        public static ImageSource img_pushpin_icon;
        public static ImageSource img_pushpin_selected_icon;
        public static ImageSource img_change_view_stack_icon;
        public static ImageSource img_collection_window_icon;
        public static ImageSource img_signup_window_icon;
        public static ImageSource img_signup_icon;
        public static ImageSource img_submit_idea_icon;
        public static ImageSource img_thumbs_up_icon;
        public static ImageSource img_thumbs_down_icon;
        public static ImageSource img_drag_icon;
        public static ImageSource img_drag_vertical_icon;
        public static ImageSource img_comment_icon;
        public static ImageSource img_reply_icon;
        public static ImageSource img_affiliation_icon;
        public static ImageSource img_implemented_icon;

        public static int frame_width = 300;
        public static int frame_title_bar_height = 40;
        public static double frame_image_title_bar_height_percentage = 0.05;
        public static int frame_icon_width = 40;
        public static int collection_listbox_height = 170;

        public static int toolbar_item_width = 30;
        public static double title_font_size = 20;
        public static string title_font_name = "Calibri";
        public static Brush right_panel_background = Brushes.LightGray;
        public static Brush right_panel_border_color = Brushes.Gray;
        public static double right_panel_width = 90;
        public static double user_item_avatar_width = 60;
        public static double comment_item_avatar_width = 20;
        public static double design_idea_item_title_font_size = 17;
        public static double design_idea_item_user_info_font_size = 10;
        public static double design_idea_item_avatar_width = 30;
        public static double design_idea_right_panel_width = 55;

        public static bool activity_icons_loaded = false;

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
            img_keyboard_shift_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + keyboard_shift_pic));
            img_keyboard_caps_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + keyboard_caps_pic));
            img_keyboard_numpad_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + keyboard_numpad_pic));
            img_choose_avatar_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + choose_avatar_pic));
            img_choose_user_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + choose_user_pic));
            img_choose_activity_pic = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + choose_activity_pic));
            img_close_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + close_icon));
            img_collection_window_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + collection_window_icon));
            img_signup_window_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + signup_window_icon));
            img_pushpin_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + pushpin_icon));
            img_pushpin_selected_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + pushpin_selected_icon));
            img_change_view_stack_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + change_view_stack_icon));
            img_signup_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + signup_icon));
            img_submit_idea_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + submit_idea_icon));
            img_thumbs_up_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + thumbs_up_icon));
            img_thumbs_down_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + thumbs_down_icon));
            img_thumbs_down_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + thumbs_down_icon));
            img_drag_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + drag_icon));
            img_drag_vertical_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + drag_vertical_icon));
            img_comment_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + comment_icon));
            img_reply_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + reply_icon));
            img_affiliation_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + affiliation_icon));
            img_implemented_icon = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + implemented_icon));
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
            catch (Exception exc)
            {
                // could not create thumbnail -- reason: filenotfound or currupt download or ...
                log.WriteErrorLog(exc);
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

        public static int get_or_create_collection(int user_id, int activity_id, DateTime dt)
        {
            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
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
            database_manager.InsertCollection(cl);
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
                return 0;
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
            database_manager.InsertCollection(cl);
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
            Point p1 = new Point(parser.GetValue("Locations", "P1X", 0), parser.GetValue("Locations", "P1Y", 0)); locations.Add(p1);
            Point p2 = new Point(parser.GetValue("Locations", "P2X", 0), parser.GetValue("Locations", "P2Y", 0)); locations.Add(p2);
            Point p3 = new Point(parser.GetValue("Locations", "P3X", 0), parser.GetValue("Locations", "P3Y", 0)); locations.Add(p3);
            Point p4 = new Point(parser.GetValue("Locations", "P4X", 0), parser.GetValue("Locations", "P4Y", 0)); locations.Add(p4);
            Point p5 = new Point(parser.GetValue("Locations", "P5X", 0), parser.GetValue("Locations", "P5Y", 0)); locations.Add(p5);
            Point p6 = new Point(parser.GetValue("Locations", "P6X", 0), parser.GetValue("Locations", "P6Y", 0)); locations.Add(p6);
            Point p7 = new Point(parser.GetValue("Locations", "P7X", 0), parser.GetValue("Locations", "P7Y", 0)); locations.Add(p7);
            Point p8 = new Point(parser.GetValue("Locations", "P8X", 0), parser.GetValue("Locations", "P8Y", 0)); locations.Add(p8);
            Point p9 = new Point(parser.GetValue("Locations", "P9X", 0), parser.GetValue("Locations", "P9Y", 0)); locations.Add(p9);
            Point p10 = new Point(parser.GetValue("Locations", "P10X", 0), parser.GetValue("Locations", "P10Y", 0)); locations.Add(p10);
            Point p11 = new Point(parser.GetValue("Locations", "P11X", 0), parser.GetValue("Locations", "P11Y", 0)); locations.Add(p11);

            //Point p1 = new Point(parser.GetValue("Locations", "P1X", 800), parser.GetValue("Locations", "P1Y", 1383)); locations.Add(p1);
            //Point p2 = new Point(parser.GetValue("Locations", "P2X", 925), parser.GetValue("Locations", "P2Y", 1499)); locations.Add(p2);
            //Point p3 = new Point(parser.GetValue("Locations", "P3X", 1795), parser.GetValue("Locations", "P3Y", 844)); locations.Add(p3);
            //Point p4 = new Point(parser.GetValue("Locations", "P4X", 2437), parser.GetValue("Locations", "P4Y", 1077)); locations.Add(p4);
            //Point p5 = new Point(parser.GetValue("Locations", "P5X", 2457), parser.GetValue("Locations", "P5Y", 1711)); locations.Add(p5);
            //Point p6 = new Point(parser.GetValue("Locations", "P6X", 2921), parser.GetValue("Locations", "P6Y", 2111)); locations.Add(p6);
            //Point p7 = new Point(parser.GetValue("Locations", "P7X", 3120), parser.GetValue("Locations", "P7Y", 2430)); locations.Add(p7);
            //Point p8 = new Point(parser.GetValue("Locations", "P8X", 2837), parser.GetValue("Locations", "P8Y", 2670)); locations.Add(p8);
            //Point p9 = new Point(parser.GetValue("Locations", "P9X", 2533), parser.GetValue("Locations", "P9Y", 2668)); locations.Add(p9);
            //Point p10 = new Point(parser.GetValue("Locations", "P10X", 1151), parser.GetValue("Locations", "P10Y", 1931)); locations.Add(p10);
            //Point p11 = new Point(parser.GetValue("Locations", "P11X", 969), parser.GetValue("Locations", "P11Y", 2016)); locations.Add(p11);

            location_dot_diameter = parser.GetValue("Locations", "location_dot_diameter", 55);
            location_dot_color = parser.GetValue("Locations", "location_dot_color", Brushes.Crimson);
            location_dot_outline_color = parser.GetValue("Locations", "location_dot_outline_color", Brushes.Crimson);
            location_dot_font_color = parser.GetValue("Locations", "location_dot_font_color", Brushes.White);

            // General variables
            //line_break = parser.GetValue("General", "line_break", "\r\n");
            application_name = parser.GetValue("General", "name", "");
            site_name = parser.GetValue("General", "site_name", "aces");
            write_interaction_log = parser.GetValue("General", "write_interaction_log", false);
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
            frame_title_user_collection = parser.GetValue("General", "frame_title_user_collection", " Observations");
            frame_title_location_collection = parser.GetValue("General", "frame_title_location_collection", "");
            frame_title_activity_collection = parser.GetValue("General", "frame_title_activity_collection", " Contributions");
            affiliation_aces = parser.GetValue("General", "affiliation_aces", "ACES");
            status_implemented = parser.GetValue("General", "status_implemented", "implemented");
            status_deleted = parser.GetValue("General", "status_deleted", "deleted");
            implemented_text = parser.GetValue("General", "implemented_text", "[Implemented] ");
            still_uploading_text = parser.GetValue("General", "still_uploading_text", "Still Uploading (Tap to retry)");
            status_initial_string = parser.GetValue("General", "status_initial_string", "");
            default_user_text = parser.GetValue("General", "default_user_text", "Anonymous");
            default_user_desc = parser.GetValue("General", "default_user_desc", default_user_desc);
            choose_user_text = parser.GetValue("General", "choose_user_text", choose_user_text);
            choose_activity_text = parser.GetValue("General", "choose_activity_text", choose_activity_text);

            // Help
            show_help = parser.GetValue("Help", "show_help", true);
            help_text = parser.GetValue("Help", "help_text", help_text);
            help_file_drag_user = parser.GetValue("Help", "help_file_drag_user", help_file_drag_user);
            help_text_drag_user = parser.GetValue("Help", "help_text_drag_user", help_text_drag_user);
            help_file_drag_designidea = parser.GetValue("Help", "help_file_drag_designidea", help_file_drag_designidea);
            help_text_drag_designidea = parser.GetValue("Help", "help_text_drag_designidea", help_text_drag_designidea);

            // Parameters
            high_contrast = parser.GetValue("Parameters", "high_contrast", false);
            top_most = parser.GetValue("Parameters", "top_most", false);
            show_update_label = parser.GetValue("Parameters", "show_update_label", false);
            response_to_mouse_clicks = parser.GetValue("Parameters", "response_to_mouse_clicks", true);
            enable_single_rotation = parser.GetValue("Parameters", "enable_single_rotation", true);
            max_num_content_update = parser.GetValue("Parameters", "max_num_content_update", 12);
            max_signup_frame = parser.GetValue("Parameters", "max_signup_frame", 5);
            max_collection_frame = parser.GetValue("Parameters", "max_collection_frame", 10);
            max_image_display_frame = parser.GetValue("Parameters", "max_image_display_frame", 10);
            max_design_ideas_frame = parser.GetValue("Parameters", "max_design_ideas_frame", 10);
            max_activity_frame = parser.GetValue("Parameters", "max_activity_frame", 10);
            max_thread_reply = parser.GetValue("Parameters", "max_thread_reply", 3);
            max_activity_frame_title_chars = parser.GetValue("Parameters", "max_activity_frame_title_chars", 10);
            //thumbnail_pixel_width = parser.GetValue("Parameters", "thumbnail_pixel_width",100);
            thumbnail_pixel_height = parser.GetValue("Parameters", "thumbnail_pixel_height", 100);
            thumbnail_video_span = new TimeSpan(0, 0, parser.GetValue("Parameters", "thumbnail_video_span_seconds", 2));
            use_existing_thumbnails = parser.GetValue("Parameters", "use_existing_thumbnails", true);
            drag_dy_dx_factor = parser.GetValue("Parameters", "drag_dy_dx_factor", 2.1);
            idea_text_scale_factor = parser.GetValue("Parameters", "idea_text_scale_factor", 0.68);
            //drag_dx_dy_factor = parser.GetValue("Parameters", "drag_dx_dy_factor",1.0);
            drag_collection_theta = parser.GetValue("Parameters", "drag_collection_theta", 5);
            scroll_scale_factor = parser.GetValue("Parameters", "scroll_scale_factor", 5);
            min_touch_points = parser.GetValue("Parameters", "min_touch_points", 2);
            max_consecutive_drag_points = parser.GetValue("Parameters", "max_consecutive_drag_points", 5);
            tap_error = parser.GetValue("Parameters", "tap_error", 5);
            manipulation_pivot_radius = parser.GetValue("Parameters", "manipulation_pivot_radius", 20.0);
            use_avatar_drag = parser.GetValue("Parameters", "use_avatar_drag", false);
            tab_width_percentage = parser.GetValue("Parameters", "tab_width_percentage", 17);
            manual_scroll = parser.GetValue("Parameters", "manual_scroll", false);
            manual_tap = parser.GetValue("Parameters", "manual_tap", false);
            right_panel_drag = parser.GetValue("Parameters", "right_panel_drag", true);
            whole_item_drag = parser.GetValue("Parameters", "whole_item_drag", false);
            center_commentarea_and_keyboard = parser.GetValue("Parameters", "center_commentarea_and_keyboard", false);
            multi_keyboard = parser.GetValue("Parameters", "multi_keyboard", false);
            show_vertical_drag = parser.GetValue("Parameters", "show_vertical_drag", true);
            show_empty_metadata = parser.GetValue("Parameters", "show_empty_metadata", false);
            show_all_metadata = parser.GetValue("Parameters", "show_all_metadata", false);
            use_list_refresher = parser.GetValue("Parameters", "use_list_refresher", false);
            update_period_ms = parser.GetValue("Parameters", "update_period_ms", 20000);
            scaling_mode = parser.GetValue("Parameters", "scaling_mode", BitmapScalingMode.Fant);
            click_opacity_on_collection_item = parser.GetValue("Parameters", "click_opacity_on_collection_item", 0.8);
            kill_window_millisec = parser.GetValue("Parameters", "kill_window_millisec", 5000);

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
            keyboard_pic = parser.GetValue("Files", "keyboard_pic", "NN_Keyboard_v2.png");
            keyboard_shift_pic = parser.GetValue("Files", "keyboard_shift_pic", "NN_Keyboard_v2_shift.png");
            keyboard_caps_pic = parser.GetValue("Files", "keyboard_caps_pic", "NN_Keyboard_v2_caps.png");
            keyboard_numpad_pic = parser.GetValue("Files", "keyboard_numpad_pic", "NN_Numpad.png");
            choose_avatar_pic = parser.GetValue("Files", "choose_avatar_pic", "choose_avatar.png");
            choose_activity_pic = parser.GetValue("Files", "choose_activity_pic", "choose_activity.png");
            choose_user_pic = parser.GetValue("Files", "choose_user_pic", "choose_user.png");
            close_icon = parser.GetValue("Files", "close_icon", "close.png");
            pushpin_icon = parser.GetValue("Files", "pushpin_icon", "NN_PinButton.png");
            pushpin_selected_icon = parser.GetValue("Files", "pushpin_selected_icon", "NN_PinButton_selected.png");
            change_view_stack_icon = parser.GetValue("Files", "change_view_stack_icon", "change_view_stack.png");
            collection_window_icon = parser.GetValue("Files", "collection_window_icon", "collection_window_icon.png");
            signup_icon = parser.GetValue("Files", "signup_icon", "signup.png");
            signup_window_icon = parser.GetValue("Files", "signup_window_icon", "signup_window_icon.png");
            submit_idea_icon = parser.GetValue("Files", "submit_idea_icon", "submit_idea.png");
            thumbs_up_icon = parser.GetValue("Files", "thumbs_up_icon", "tu.jpg");
            thumbs_down_icon = parser.GetValue("Files", "thumbs_down_icon", "td.jpg");
            drag_icon = parser.GetValue("Files", "drag_icon", "drag.png");
            drag_vertical_icon = parser.GetValue("Files", "drag_vertical_icon", "drag_vertical.png");
            comment_icon = parser.GetValue("Files", "comment_icon", "comment.png");
            reply_icon = parser.GetValue("Files", "reply_icon", "reply.png");
            affiliation_icon = parser.GetValue("Files", "affiliation_icon", "affiliation.png");
            implemented_icon = parser.GetValue("Files", "implemented_icon", "implemented.png");
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

            // Parameters
            parser.SetValue("Parameters", "high_contrast", high_contrast);
            parser.SetValue("Parameters", "top_most", top_most);
            parser.SetValue("Parameters", "show_update_label", show_update_label);
            parser.SetValue("Parameters", "response_to_mouse_clicks", response_to_mouse_clicks);
            parser.SetValue("Parameters", "enable_single_rotation", enable_single_rotation);
            parser.SetValue("Parameters", "max_num_content_update", max_num_content_update);
            parser.SetValue("Parameters", "max_signup_frame", max_signup_frame);
            parser.SetValue("Parameters", "max_collection_frame", max_collection_frame);
            parser.SetValue("Parameters", "max_image_display_frame", max_image_display_frame);
            parser.SetValue("Parameters", "max_design_ideas_frame", max_design_ideas_frame);
            parser.SetValue("Parameters", "max_activity_frame", max_activity_frame);
            parser.SetValue("Parameters", "max_thread_reply", max_thread_reply);
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
            parser.SetValue("Parameters", "manipulation_pivot_radius", manipulation_pivot_radius);
            parser.SetValue("Parameters", "use_avatar_drag", use_avatar_drag);
            parser.SetValue("Parameters", "tab_width_percentage", tab_width_percentage);
            parser.SetValue("Parameters", "manual_scroll", manual_scroll);
            parser.SetValue("Parameters", "manual_tap", manual_tap);
            parser.SetValue("Parameters", "right_panel_drag", right_panel_drag);
            parser.SetValue("Parameters", "whole_item_drag", whole_item_drag);
            parser.SetValue("Parameters", "center_commentarea_and_keyboard", center_commentarea_and_keyboard);
            parser.SetValue("Parameters", "multi_keyboard", multi_keyboard);
            parser.SetValue("Parameters", "show_vertical_drag", show_vertical_drag);
            parser.SetValue("Parameters", "show_empty_metadata", show_empty_metadata);
            parser.SetValue("Parameters", "show_all_metadata", show_all_metadata);
            parser.SetValue("Parameters", "use_list_refresher", use_list_refresher);
            parser.SetValue("Parameters", "update_period_ms", update_period_ms);
            parser.SetValue("Parameters", "scaling_mode", scaling_mode);
            parser.SetValue("Parameters", "click_opacity_on_collection_item", click_opacity_on_collection_item);

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

        public static string GetTextBlockText(System.Windows.Controls.TextBlock tb)
        {
            StringBuilder s = new StringBuilder();
            foreach (var line in tb.Inlines)
            {
                if (line is System.Windows.Documents.LineBreak)
                {
                    s.Append("\r\n");
                }
                else if (line is System.Windows.Documents.Run)
                {
                    System.Windows.Documents.Run text = (System.Windows.Documents.Run)line;
                    s.Append(text.Text);
                }
            }
            return s.ToString();
        }

        public static string GetTextBlockText2(System.Windows.Controls.TextBlock tb)
        {
            Drawing textBlockDrawing = VisualTreeHelper.GetDrawing(tb);
            var sb = new StringBuilder();
            WalkDrawingForText(sb, textBlockDrawing);
            return sb.ToString();
        }

        private static void WalkDrawingForText(StringBuilder sb, Drawing d)
        {
            var glyphs = d as GlyphRunDrawing;
            if (glyphs != null)
            {
                sb.Append(glyphs.GlyphRun.Characters.ToArray());
            }
            else
            {
                var g = d as DrawingGroup;
                if (g != null)
                {
                    foreach (Drawing child in g.Children)
                    {
                        WalkDrawingForText(sb, child);
                    }
                }
            }
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

        public static User find_user_of_contribution(Contribution c)
        {
            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
            var users = from mappings in db.Collection_Contribution_Mappings
                           where mappings.contribution_id == c.id
                           select mappings.Collection.User;
            if (users == null) return null;
            if (users.Count() == 0) return null;
            return users.First<User>();
        }

        public static Activity find_activity_of_contribution(Contribution c)
        {
            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
            var activities = from mappings in db.Collection_Contribution_Mappings
                             where mappings.contribution_id == c.id
                             select mappings.Collection.Activity;
            if (activities == null) return null;
            if (activities.Count() == 0) return null;
            return activities.First<Activity>();
        }

        public static string get_display_text_for_birdcounting(string note_content, string activity_info)
        {
            string text_to_display = "";
            try
            {
                NoteContent nc = JsonConvert.DeserializeObject<NoteContent>(note_content);
                Extras ex = JsonConvert.DeserializeObject<Extras>(activity_info);
                foreach (BirdInfo b_a in ex.birds)
                {
                    foreach (BirdInfo b_n in nc.birds)
                    {
                        if (b_n.name == b_a.name)
                            text_to_display = text_to_display + "\r\n" + b_a.name + ": " + b_n.count;
                    }
                }
                if (text_to_display.Length > 2)
                    text_to_display = text_to_display.Substring(2);
            }
            catch (Exception) { } // try to make sense of what is stored in note content but if couldnt no worries
            if (text_to_display == "")
            {
                // default text
                try
                {
                    Extras ex = JsonConvert.DeserializeObject<Extras>(activity_info);
                    foreach (BirdInfo b_a in ex.birds)
                        text_to_display = text_to_display + "\r\n" + b_a.name + ": 0";
                    if (text_to_display.Length > 2)
                        text_to_display.Substring(2);
                }
                catch (Exception) { }
            }
            return text_to_display;
        }

        public static List<Contribution> get_contributions_for_activity(int activity_id, bool include_birdcounting)
        {
            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
            List<Contribution> list_a = new List<Contribution>();
            var result_a = from c0 in db.Collection_Contribution_Mappings
                           where c0.Collection.activity_id == activity_id
                           && (c0.Contribution.status != configurations.status_deleted)
                           && (c0.Contribution.tags != "BirdCounting")
                           select c0.Contribution;
            if (result_a != null)
                list_a = result_a.ToList<Contribution>();
            List<Contribution> list_b = new List<Contribution>();
            if (include_birdcounting)
            {
                var result_b = from c0 in db.Collection_Contribution_Mappings
                               where c0.Collection.activity_id == activity_id
                               && (c0.Contribution.status != configurations.status_deleted)
                               && (c0.Contribution.tags == "BirdCounting")
                               orderby c0.Contribution.date descending
                               group c0 by new { c0.Contribution.date.Date, c0.Collection.User.name } into groups
                               select groups.OrderByDescending(p => p.Contribution.date).First().Contribution;
                if (result_b != null)
                    list_b = result_b.ToList<Contribution>();
            }
            list_a.AddRange(list_b);
            list_a = list_a.OrderByDescending(p => p.date).ToList<Contribution>();
            return list_a;
        }

        public static List<Contribution> get_contributions_for_user(string username, bool include_birdcounting, bool include_designideas)
        {
            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
            List<Contribution> list_a = new List<Contribution>();
            var result_a = from c0 in db.Collection_Contribution_Mappings
                           where (c0.Collection.User.name == username)
                           && (c0.Contribution.status != configurations.status_deleted)
                           && (c0.Contribution.tags != "BirdCounting")
                           && (c0.Contribution.tags != "Design Idea")
                           select c0.Contribution;
            if (result_a != null)
                list_a = result_a.ToList<Contribution>();
            
            List<Contribution> list_b = new List<Contribution>();
            if (include_birdcounting)
            {
                var result_b = from c0 in db.Collection_Contribution_Mappings
                               where (c0.Collection.User.name == username)
                                && (c0.Contribution.status != configurations.status_deleted)
                                && (c0.Contribution.tags == "BirdCounting")
                               orderby c0.Contribution.date descending
                               group c0 by new { c0.Contribution.date.Date, c0.Collection.User.name } into groups
                               select groups.OrderByDescending(p => p.Contribution.date).First().Contribution;
                if (result_b != null)
                    list_b = result_b.ToList<Contribution>();
            }
            list_a.AddRange(list_b);

            List<Contribution> list_c = new List<Contribution>();
            if (include_designideas)
            {
                var result_c = from c0 in db.Collection_Contribution_Mappings
                               where (c0.Collection.User.name == username)
                                && (c0.Contribution.status != configurations.status_deleted)
                                && (c0.Contribution.tags == "Design Idea")
                               select c0.Contribution;
                if (result_c != null)
                    list_c = result_c.ToList<Contribution>();
            }
            list_a.AddRange(list_c);

            list_a = list_a.OrderByDescending(p => p.date).ToList<Contribution>();
            return list_a;
        }

        public static List<Contribution> get_contributions_for_location(int location_id, bool include_birdcounting, bool include_designideas)
        {
            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
            List<Contribution> list_a = new List<Contribution>();
            var result_a = from c0 in db.Collection_Contribution_Mappings
                           where (c0.Contribution.location_id == location_id)
                           && (c0.Contribution.status != configurations.status_deleted)
                           && (c0.Contribution.tags != "BirdCounting")
                           && (c0.Contribution.tags != "Design Idea")
                           select c0.Contribution;
            if (result_a != null)
                list_a = result_a.ToList<Contribution>();

            List<Contribution> list_b = new List<Contribution>();
            if (include_birdcounting)
            {
                var result_b = from c0 in db.Collection_Contribution_Mappings
                               where (c0.Contribution.location_id == location_id)
                                && (c0.Contribution.status != configurations.status_deleted)
                                && (c0.Contribution.tags == "BirdCounting")
                               orderby c0.Contribution.date descending
                               group c0 by new { c0.Contribution.date.Date, c0.Collection.User.name } into groups
                               select groups.OrderByDescending(p => p.Contribution.date).First().Contribution;
                if (result_b != null)
                    list_b = result_b.ToList<Contribution>();
            }
            list_a.AddRange(list_b);

            List<Contribution> list_c = new List<Contribution>();
            if (include_designideas)
            {
                var result_c = from c0 in db.Collection_Contribution_Mappings
                               where (c0.Contribution.location_id == location_id)
                                && (c0.Contribution.status != configurations.status_deleted)
                                && (c0.Contribution.tags == "Design Idea")
                               select c0.Contribution;
                if (result_c != null)
                    list_c = result_c.ToList<Contribution>();
            }
            list_a.AddRange(list_c);

            list_a = list_a.OrderByDescending(p => p.date).ToList<Contribution>();
            return list_a;
        }

        public static collection_item create_collection_item_from_contribution(Contribution c)
        {
            collection_item ci = new collection_item();
            ci._contribution = c;
            ci.text_to_display = c.note;

            if (c.tags != null)
            {
                if (c.tags.Contains("Photo"))
                    ci.is_image = true;
                if (c.tags.Contains("Video"))
                    ci.is_video = true;
                if (c.tags.Contains("Audio"))
                    ci.is_audio = true;
                if (c.tags.Contains("Design Idea"))
                {
                    ci.should_have_media = false;
                }
                if (c.tags.Contains("BirdCounting"))
                {
                    ci.should_have_media = false;
                }
                ci.contribution_type = c.tags;
            }

            if (ci.contribution_type == "BirdCounting")
            {
                Activity a = configurations.find_activity_of_contribution(c);
                string[] activity_info = a.technical_info.Split(new char[] { ';' });
                if (activity_info.Count() > 2)
                {
                    ci.text_to_display = configurations.get_display_text_for_birdcounting(ci._contribution.note, activity_info[2]);
                    try
                    {
                        Extras ex = JsonConvert.DeserializeObject<Extras>(activity_info[2]);
                        if (ex.birds.Count > 0)
                        {
                            if (!File.Exists(configurations.GetAbsoluteImagePath() + ex.birds[0].name + ".png"))
                            {
                                System.IO.FileStream file_stream = new System.IO.FileStream(configurations.GetAbsoluteImagePath() + ex.birds[0].name + ".png", System.IO.FileMode.Create);
                                file_stream.Close();
                                System.Net.WebClient client = new System.Net.WebClient();
                                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                                client.DownloadFile(ex.birds[0].image, configurations.GetAbsoluteImagePath() + ex.birds[0].name + ".png");
                            }
                            ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteImagePath() + ex.birds[0].name + ".png"));
                            src.Freeze();
                            ci.default_contrib_img = src;
                        }
                    }
                    catch (Exception ex) { log.WriteErrorLog(ex); }
                }
            }

            if (c.media_url == null || c.media_url == "") return ci;

            file_manager.try_downloading_contribution(ci, false, false);
            return ci;
        }

        public static item_generic_v2 get_designidea_item_visuals(int id)
        {
            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
            var contributions = from c in db.Contributions
                                where c.id == id
                                select c;
            if (contributions.Count() != 1) return null;
            Contribution contribution = contributions.Single<Contribution>();
            collection_item ci = configurations.create_collection_item_from_contribution(contribution);
            item_generic_v2 i = configurations.get_item_visuals(ci);
            return i;
        }

        public static item_generic_v2 get_item_visuals(collection_item ci)
        {
            Contribution c = ci._contribution;
            item_generic_v2 i = new item_generic_v2();
            i.user_info_icon.Visibility = Visibility.Collapsed;
            //i.user_info_name.Text = c.web_username;

            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
            var us = from cc in db.Collection_Contribution_Mappings
                     where cc.contribution_id == c.id
                     select cc.Collection.User;
            if (us.Count() > 0)
            {
                User u = us.First<User>();
                i.user_info_name.Text = u.name;
                if (u.affiliation != null && u.affiliation.ToLower() == configurations.affiliation_aces.ToLower())
                {
                    i.affiliation_icon_small.Source = configurations.img_affiliation_icon;
                    i.affiliation_icon_small.Visibility = Visibility.Visible;
                }
                try
                {
                    ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + u.avatar));
                    src.Freeze();
                    i.user_info_icon.Source = src;
                    i.user_info_icon.Visibility = Visibility.Visible;
                }
                catch (Exception) { }
            }
            else
            {
                if (c.web_username != null)
                {
                    var webusers = from w in db.WebUsers
                                   where w.username == c.web_username
                                   select w;
                    if (webusers.Count() == 1)
                    {
                        WebUser webuser = webusers.Single<WebUser>();
                        i.user_info_name.Text = webuser.username + configurations.default_user_desc;
                        //i.affiliation = configurations.default_webuser_affiliation;
                        if (webuser.user_id.HasValue)
                        {
                            var users = from u in db.Users
                                        where u.id == webuser.user_id.Value
                                        select u;
                            if (users.Count() == 1)
                            {
                                User the_user = users.Single<User>();
                                i.user_info_name.Text = the_user.name + configurations.default_user_desc;
                                try
                                {
                                    ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + the_user.avatar));
                                    src.Freeze();
                                    i.user_info_icon.Source = src;
                                    i.user_info_icon.Visibility = Visibility.Visible;
                                }
                                catch (Exception) { }
                                if (the_user.affiliation != null && the_user.affiliation.ToLower() == configurations.affiliation_aces.ToLower())
                                {
                                    i.affiliation_icon_small.Source = configurations.img_affiliation_icon;
                                    i.affiliation_icon_small.Visibility = Visibility.Visible;
                                }
                            }
                        }
                    }
                }
            }

            DateTime last_time = c.date;
            var n1 = from f in db.Feedbacks
                     where (f.object_type == "nature_net.Contribution") && (f.object_id == c.id)
                     orderby f.date descending
                     select f;
            int cnt = 0; int num_like = 0; int num_dislike = 0; int num_comments = 0;
            if (n1 != null)
                cnt = n1.Count();
            if (cnt != 0)
            {
                last_time = n1.First().date;
                foreach (Feedback f2 in n1)
                {
                    if (f2.Feedback_Type.name == "Like")
                        if (Convert.ToBoolean(f2.note))
                            num_like++;
                        else
                            num_dislike++;
                    if (f2.Feedback_Type.name == "Comment")
                        num_comments++;
                }
            }

            i.Background = Brushes.White;
            i.title.Text = ci.text_to_display;
            i.description.Visibility = Visibility.Collapsed;
            TextBlock.SetFontWeight(i.title, FontWeights.Normal);
            i.title.FontSize = configurations.design_idea_item_title_font_size;
            i.user_info.Margin = new Thickness(5);
            i.user_info_date.Text = configurations.GetDate_Formatted(c.date);
            i.user_info_name.Margin = new Thickness(2, 0, 0, 0); i.user_info_date.Margin = new Thickness(2, 0, 2, 0);
            i.user_info_name.FontSize = configurations.design_idea_item_user_info_font_size; i.user_info_date.FontSize = configurations.design_idea_item_user_info_font_size;
            i.number.Text = num_comments.ToString();
            i.number_icon.Visibility = Visibility.Collapsed;
            i.txt_level1.Text = configurations.designidea_num_desc;
            i.txt_level2.Visibility = Visibility.Collapsed; i.txt_level3.Visibility = Visibility.Collapsed;
            i.avatar.Source = configurations.img_thumbs_up_icon;
            i.num_likes.Content = num_like.ToString();
            i.avatar.Width = configurations.design_idea_item_avatar_width; i.avatar.Height = configurations.design_idea_item_avatar_width; i.avatar.Margin = new Thickness(5, 5, 5, 0); i.avatar.Tag = i;
            i.Tag = c.id;
            if (c.status != null && c.status.ToLower() == configurations.status_implemented.ToLower())
            {
                //i.pre_title.Text = configurations.implemented_text;
                //i.pre_title.FontWeight = FontWeights.Bold;
                i.affiliation_icon.Height = 15;
                i.affiliation_icon.Source = configurations.img_implemented_icon;
                i.affiliation_icon.Visibility = Visibility.Visible;
            }
            i.right_panel.Width = configurations.design_idea_right_panel_width;
            i.top_value = num_like;
            i.drag_icon_vertical.Source = configurations.img_drag_vertical_icon;
            if (configurations.show_vertical_drag) i.drag_icon_vertical_panel.Visibility = Visibility.Visible;
            //if (thumbs_up_handler != null)
            i.avatar.Tag = i;
            return i;
        }
    }
}
