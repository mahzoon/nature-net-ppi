using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using nature_net.user_controls;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;

namespace nature_net
{
    public class window_manager
    {
        public static Canvas main_canvas;
        public static tab_control left_tab;
        public static tab_control right_tab;

        // contribution id, extension
        public static Dictionary<int, string> downloaded_contributions = new Dictionary<int, string>();

        public static Dictionary<int, ImageSource> thumbnails = new Dictionary<int, ImageSource>();
        //public static Dictionary<int, ImageSource> contributions = new Dictionary<int, ImageSource>();
        public static Dictionary<int, MediaPlayer> media = new Dictionary<int, MediaPlayer>();
        public static List<window_frame> collection_frames = new List<window_frame>();
        public static List<window_frame> signup_frames = new List<window_frame>();
        public static List<window_frame> design_ideas_frames = new List<window_frame>();
        public static List<window_frame> image_display_frames = new List<window_frame>();
        public static List<window_frame> activity_frames = new List<window_frame>();
        public static List<image_frame> image_frames = new List<image_frame>();
        public static List<help_window> help_windows = new List<help_window>();

        public static Dictionary<string, ImageSource> avatars = new Dictionary<string, ImageSource>();

        static System.Threading.Timer highlight_timer;

        public static void open_location_collection_window(string location, int location_id, double pos_x, double pos_y)
        {
            if (window_manager.collection_frames.Count + 1 > configurations.max_collection_frame)
                return;

            window_frame frame = new window_frame();
            window_content content = new window_content();
            collection_listbox c_listbox = new collection_listbox();
            c_listbox.parent = frame;
            c_listbox.list_contributions_in_location(location_id);
            //content.initialize_contents(c_listbox);
            content.initialize_contents(c_listbox, Type.GetType("nature_net.Location"), location_id, frame,
                configurations.frame_title_location_collection + " in " + location_id.ToString() + ": " + location);
            frame.window_content.Content = content;
            
            content.list_all_comments();

            window_manager.collection_frames.Add(frame);
            open_window(frame, pos_x - (frame.Width / 2), pos_y - (c_listbox.Height));
            frame.set_title(configurations.frame_title_location_collection + " in " + location_id.ToString() + ": " + location);
            frame.set_kill_timer();
        }

        public static void open_collection_window(string username, int userid, double pos_x, double pos_y)
        {
            if (window_manager.collection_frames.Count + 1 > configurations.max_collection_frame)
                return;

            window_frame frame = new window_frame();
            window_content content = new window_content();
            collection_listbox c_listbox = new collection_listbox();
            c_listbox.parent = frame;
            c_listbox.list_contributions_for_user(username);
            content.initialize_contents(c_listbox, Type.GetType("nature_net.User"), userid, frame,
                username + configurations.frame_title_user_collection);
            frame.window_content.Content = content;
            content.list_all_comments();

            window_manager.collection_frames.Add(frame);
            open_window(frame, pos_x, pos_y);
            frame.set_title(username + configurations.frame_title_user_collection);
            frame.set_kill_timer();
        }

        public static void open_contribution_window(collection_item citem, double pos_x, double pos_y)
        {
            if (window_manager.image_frames.Count + 1 > configurations.max_image_display_frame)
                return;
            
            //window_frame frame = new window_frame();
            window_content content = new window_content();
            //contribution_view m = new contribution_view();
            //m.view_contribution(citem);
            image_frame iframe = new image_frame();
            iframe.view_contribution(citem);
            content.initialize_comments(citem._contribution);
            iframe.window_content.Content = content;
            if (!configurations.center_commentarea_and_keyboard)
            {
                iframe.the_content.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                content.center_keyboard = false;
            }
            iframe.UpdateLayout();
            content.initialize_contents(null, Type.GetType("nature_net.Contribution"), citem._contribution.id, iframe, iframe.the_content.Width);
            //frame.window_content.Content = content;
            //window_manager.image_display_frames.Add(frame);
            //open_window(frame, pos_x, pos_y);
            //m.center_image();
            //frame.hide_change_view();
            //frame.set_title(ctype);
            main_canvas.Children.Add(iframe);
            iframe.UpdateLayout();
            window_manager.image_frames.Add(iframe);

            double h = iframe.ActualHeight;
            pos_x = pos_x - (iframe.ActualWidth / 2);
            //try { h = ((window_content)(iframe.window_content.Content)).the_item.ActualHeight; }
            //catch (Exception) { }
            if (pos_y > window_manager.main_canvas.ActualHeight - h)
                pos_y = window_manager.main_canvas.ActualHeight - h;
            TranslateTransform m = new TranslateTransform(pos_x, pos_y);
            Matrix matrix = m.Value;
            iframe.RenderTransform = new MatrixTransform(matrix);
            UpdateZOrder(iframe, true);
            iframe.set_kill_timer();
        }

        public static void open_design_idea_window(item_generic_v2 idea_item, double pos_x, double pos_y, string title = "Design Idea")
        {
            if (window_manager.design_ideas_frames.Count + 1 > configurations.max_design_ideas_frame)
                return;

            window_frame frame = new window_frame();
            window_content content = new window_content();
            item_generic_v2 idea = idea_item.get_clone();

            idea.Background = new SolidColorBrush(Colors.White);
            idea.description.Visibility = Visibility.Collapsed;
            idea.title.FontSize = 17;
            idea.user_info.Margin = new Thickness(5); 
            idea.user_info_name.Margin = new Thickness(2, 0, 0, 0); idea.user_info_date.Margin = new Thickness(2, 0, 2, 0);
            idea.user_info_name.FontSize = 10; idea.user_info_date.FontSize = 10;
            idea.Width = frame.Width;
            if (idea.affiliation_icon.Visibility == Visibility.Visible)
                idea.title.MaxWidth = 0.6 * frame.Width;

            content.initialize_contents(idea, Type.GetType("nature_net.Contribution"), Convert.ToInt32(idea_item.Tag), frame, idea_item.user_info_name.Text + "'s " + title);
            frame.window_content.Content = content;

            window_manager.design_ideas_frames.Add(frame);
            open_window(frame, pos_x, pos_y);
            frame.set_title(idea_item.user_info_name.Text + "'s " + title);
            frame.set_kill_timer();
        }

        public static void open_design_idea_window_ext(design_ideas_listbox parent, double pos_x, double pos_y)
        {
            if (window_manager.design_ideas_frames.Count + 1 > configurations.max_design_ideas_frame)
                return;

            window_frame frame = new window_frame();
            window_content content = new window_content();
            design_ideas_listbox list = new design_ideas_listbox();
            list.parent = parent;
            content.initialize_contents(list, true, frame);
            frame.window_content.Content = content;

            window_manager.design_ideas_frames.Add(frame);
            open_window(frame, pos_x, pos_y);
            frame.set_title("Submit Design Idea");
            frame.set_kill_timer();
        }

        public static void open_signup_window(double pos_x, double pos_y)
        {
            if (window_manager.signup_frames.Count + 1 > configurations.max_signup_frame)
                return;

            window_frame frame = new window_frame();
            signup s = new signup();
            s.parent = frame;
            s.user_pin.parent = frame;
            s.avatar_list_control.parent_window = frame;
            s.load_window();
            frame.window_content.Content = s;
            
            window_manager.signup_frames.Add(frame);
            open_window(frame, pos_x, pos_y);
            frame.set_title("Sign up");
            frame.set_icon(configurations.img_signup_window_icon);
            frame.set_kill_timer();
        }

        public static void open_activity_window(string activity_name, int activity_id, double pos_x, double pos_y)
        {
            if (window_manager.activity_frames.Count + 1 > configurations.max_activity_frame)
                return;

            window_frame frame = new window_frame();
            window_content content = new window_content();
            collection_listbox c_listbox = new collection_listbox();
            c_listbox.parent = frame;
            c_listbox.list_contributions_in_activity(activity_id);
            content.initialize_contents(c_listbox, Type.GetType("nature_net.Activity"), activity_id, frame,
                activity_name + configurations.frame_title_activity_collection);
            frame.window_content.Content = content;
            content.list_all_comments();

            window_manager.activity_frames.Add(frame);
            open_window(frame, pos_x, pos_y);
            string title = activity_name;
            //if (activity_name.Length > configurations.max_activity_frame_title_chars)
            //    title = activity_name.Substring(0, 10) + "...";
            frame.set_title(title + configurations.frame_title_activity_collection);
            frame.set_kill_timer();
        }

        public static void open_help_window(string filename, string caption, double pos_x, double pos_y)
        {
            if (window_manager.help_windows.Count + 1 > configurations.max_image_display_frame)
                return;

            help_window hwin = new help_window();
            hwin.view_help(filename, caption);
            main_canvas.Children.Add(hwin);
            window_manager.help_windows.Add(hwin);

            double h = hwin.ActualHeight;
            pos_x = pos_x - (hwin.ActualWidth / 2);
            if (pos_y > window_manager.main_canvas.ActualHeight - h)
                pos_y = window_manager.main_canvas.ActualHeight - h;
            TranslateTransform m = new TranslateTransform(pos_x, pos_y);
            Matrix matrix = m.Value;
            hwin.RenderTransform = new MatrixTransform(matrix);
            hwin.set_kill_timer();
        }

        private static void open_window(window_frame frame, double pos_x, double pos_y)
        {
            main_canvas.Children.Add(frame);
            frame.IsManipulationEnabled = true;
            frame.UpdateLayout();

            double h = frame.ActualHeight;
            try { h = ((window_content)(frame.window_content.Content)).the_item.ActualHeight; }
            catch (Exception) { }
            double w = frame.ActualWidth;
            try { w = ((window_content)(frame.window_content.Content)).the_item.ActualWidth; }
            catch (Exception) { }
            if (pos_y > window_manager.main_canvas.ActualHeight - h)
                pos_y = window_manager.main_canvas.ActualHeight - h;
            if (pos_x > window_manager.main_canvas.ActualWidth - w)
                pos_x = window_manager.main_canvas.ActualWidth - w;
            TranslateTransform m = new TranslateTransform(pos_x, pos_y);
            Matrix matrix = m.Value;
            frame.RenderTransform = new MatrixTransform(matrix);
            UpdateZOrder(frame, true);
        }

        public static void contribution_collection_opened(string username)
        {
            //left_tab.highlight_user_and_open_collection(username, false);
        }

        public static void highlight_callback_f(Object stateInfo)
        {
            left_tab.highlight_user_and_open_collection((string)stateInfo, false);
        }

        public static void highlight_callback_t(Object stateInfo)
        {
            left_tab.highlight_user_and_open_collection((string)stateInfo, false);
        }

        public static void close_window(window_frame frame)
        {
            collection_frames.Remove(frame);
            activity_frames.Remove(frame);
            image_display_frames.Remove(frame);
            design_ideas_frames.Remove(frame);
            signup_frames.Remove(frame);
            main_canvas.Children.Remove(frame);
        }

        public static void close_window(image_frame frame)
        {
            image_frames.Remove(frame);
            main_canvas.Children.Remove(frame);
        }

        public static void close_window(help_window frame)
        {
            help_windows.Remove(frame);
            main_canvas.Children.Remove(frame);
        }

        public static void close_signup_window(window_frame frame, string username)
        {
            signup_frames.Remove(frame);
            main_canvas.Children.Remove(frame);
            highlight_timer = new System.Threading.Timer(new System.Threading.TimerCallback(highlight_callback_users_t), username, 100, System.Threading.Timeout.Infinite);
        }

        public static void close_submit_design_idea_window(window_frame frame, int activity_id, int contribution_id)
        {
            design_ideas_frames.Remove(frame);
            main_canvas.Children.Remove(frame);
            highlight_timer = new System.Threading.Timer(new System.Threading.TimerCallback(highlight_callback_design_ideas_t),
                activity_id + ";" + contribution_id, 100, System.Threading.Timeout.Infinite);
        }

        public static Point move_window(UIElement frame, Point last_position, System.Windows.Input.MouseEventArgs e)
        {
            if (last_position.X == 0 && last_position.Y == 0) last_position = e.GetPosition(main_canvas);
            Matrix matrix = ((MatrixTransform)frame.RenderTransform).Matrix;
            //System.Windows.Point center = new System.Windows.Point(frame.ActualWidth / 2, frame.ActualHeight / 2);
            //center = matrix.Transform(center);
            matrix.Translate(e.GetPosition(main_canvas).X - last_position.X, e.GetPosition(main_canvas).Y - last_position.Y);
            frame.RenderTransform = new MatrixTransform(matrix);
            return e.GetPosition(main_canvas);
        }

        public static void refresh_downloaded_contributions()
        {
            DirectoryInfo d = new DirectoryInfo(configurations.GetAbsoluteContributionPath());
            FileInfo[] files = d.GetFiles();
            window_manager.downloaded_contributions.Clear();
            foreach (FileInfo f in files)
            {
                try
                {
                    window_manager.downloaded_contributions.Add(Convert.ToInt32(f.Name.Split(new char[] { '.' })[0]), file_manager.get_extension(f.Name));
                }
                catch (Exception e3)
                {
                    log.WriteErrorLog(e3);
                }
            }
        }

        public static void refresh_thumbnails()
        {
            DirectoryInfo d = new DirectoryInfo(configurations.GetAbsoluteThumbnailPath());
            FileInfo[] files = d.GetFiles();
            window_manager.thumbnails.Clear();
            foreach (FileInfo f in files)
            {
                try
                {
                    window_manager.thumbnails.Add(Convert.ToInt32(f.Name.Split(new char[] { '.' })[0]),
                        new BitmapImage(new Uri(configurations.GetAbsoluteThumbnailPath() + f.Name)));
                }
                catch (Exception) { }
            }
        }

        public static void load_avatars()
        {
            DirectoryInfo d = new DirectoryInfo(configurations.GetAbsoluteAvatarPath());
            FileInfo[] files = d.GetFiles();
            window_manager.avatars.Clear();
            foreach (FileInfo f in files)
            {
                try
                {
                    ImageSource img = new BitmapImage(new Uri(f.FullName));
                    //avatars.Add(f.Name.Split(new char[] { '.' })[0], img);
                    avatars.Add(f.Name, img);
                }
                catch (Exception ex) { log.WriteErrorLog(ex); }
            }
        }

        public static void load_users()
        {
            if (left_tab != null)
                left_tab.load_users();
            if (right_tab != null)
                right_tab.load_users();
            ///
        }

        public static void load_activities()
        {
            if (left_tab != null)
                left_tab.load_activities();
            if (right_tab != null)
                right_tab.load_activities();
        }

        public static void load_design_ideas()
        {
            if (left_tab != null)
                left_tab.load_design_ideas();
            if (right_tab != null)
                right_tab.load_design_ideas();
        }

        public static void load_design_ideas_sync()
        {
            if (left_tab != null)
                left_tab.load_design_ideas_sync();
            if (right_tab != null)
                right_tab.load_design_ideas_sync();
        }

        public static void UpdateZOrder(UIElement element, bool bringToFront)
        {
            if (element == null) return;
            if (!main_canvas.Children.Contains(element)) return;

            // Determine the Z-Index for the target UIElement.
            int elementNewZIndex = -1;
            if (bringToFront)
            {
                foreach (UIElement elem in main_canvas.Children)
                    if (elem.Visibility != Visibility.Collapsed)
                        ++elementNewZIndex;
            }
            else
            {
                elementNewZIndex = 0;
            }

            // Determine if the other UIElements' Z-Index 
            // should be raised or lowered by one. 
            int offset = (elementNewZIndex == 0) ? +1 : -1;
            int elementCurrentZIndex = Canvas.GetZIndex(element);

            // Update the Z-Index of every UIElement in the Canvas.
            foreach (UIElement childElement in main_canvas.Children)
            {
                if (childElement == element)
                    Canvas.SetZIndex(element, elementNewZIndex);
                else
                {
                    int zIndex = Canvas.GetZIndex(childElement);

                    // Only modify the z-index of an element if it is  
                    // in between the target element's old and new z-index.
                    if (bringToFront && elementCurrentZIndex < zIndex ||
                        !bringToFront && zIndex < elementCurrentZIndex)
                    {
                        Canvas.SetZIndex(childElement, zIndex + offset);
                    }
                }
            }
        }

        public static void highlight_callback_users_f(Object stateInfo)
        {
            left_tab.highlight_user_and_open_collection((string)stateInfo, false);
        }

        public static void highlight_callback_users_t(Object stateInfo)
        {
            left_tab.highlight_user_and_open_collection((string)stateInfo, true);
            highlight_timer = new System.Threading.Timer(new System.Threading.TimerCallback(highlight_callback_users_f), stateInfo, 5000, System.Threading.Timeout.Infinite);
        }

        public static void highlight_callback_design_ideas_f(Object stateInfo)
        {
            left_tab.highlight_design_idea_and_open_it((string)stateInfo, false);
        }

        public static void highlight_callback_design_ideas_t(Object stateInfo)
        {
            left_tab.highlight_design_idea_and_open_it((string)stateInfo, true);
            highlight_timer = new System.Threading.Timer(new System.Threading.TimerCallback(highlight_callback_design_ideas_f), stateInfo, 5000, System.Threading.Timeout.Infinite);
        }
    }

    public partial class Collection : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public override string ToString()
        {
            return this.name;
        }
    }
}
