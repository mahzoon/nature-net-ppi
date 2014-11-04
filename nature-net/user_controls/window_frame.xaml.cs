using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for window_frame.xaml
    /// </summary>
    public partial class window_frame : UserControl
    {
        private System.Threading.Timer window_killer_timer;

        public window_frame()
        {
            InitializeComponent();

            //Static Configuration Values:
            this.Width = configurations.frame_width;
            //this.title_bar.Background = new SolidColorBrush(Color.;
            //this.frame.BorderBrush = new SolidColorBrush(Color.;
            this.title_bar.Height = configurations.frame_title_bar_height;
            this.window_icon.Width = configurations.frame_icon_width;
            var b1 = new ImageBrush();
            b1.ImageSource = configurations.img_close_icon;
            this.close.Background = b1;
            var b2 = new ImageBrush();
            b2.ImageSource = configurations.img_change_view_stack_icon;
            this.change_view.Background = b2;
            this.window_icon.Source = configurations.img_collection_window_icon;

            if (configurations.response_to_mouse_clicks)
                this.close.Click += new RoutedEventHandler(close_Click);
            //this.change_view.Click += new RoutedEventHandler(change_view_Click);
            this.close.PreviewTouchDown += new EventHandler<TouchEventArgs>(close_Click);

            this.PreviewTouchDown += new EventHandler<TouchEventArgs>(window_frame_PreviewTouchDown);
        }

        void window_frame_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            this.postpone_killer_timer(true);
            e.Handled = false;
        }

        public void set_title(string t)
        {
            this.title.Text = t;
        }

        public string get_title()
        {
            return this.title.Text;
        }

        public void set_kill_timer()
        {
            window_killer_timer = new System.Threading.Timer(new System.Threading.TimerCallback(kill_this_window), this.title.Text, configurations.kill_window_millisec, System.Threading.Timeout.Infinite);
        }

        public void set_icon(ImageSource ico)
        {
            this.window_icon.Source = ico;
        }

        public void hide_change_view()
        {
            this.change_view.Visibility = System.Windows.Visibility.Collapsed;
        }

        void change_view_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public void kill_this_window(Object stateInfo)
        {
            log.WriteInteractionLog(19, "[Auto] frame closed with title: " + (string)stateInfo, null);
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new System.Action(() => { window_manager.close_window(this); }));
        }

        public void postpone_killer_timer(bool same_thread)
        {
            if (same_thread)
                window_killer_timer.Change(configurations.kill_window_millisec, System.Threading.Timeout.Infinite);
            else
                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new System.Action(() => { window_killer_timer.Change(configurations.kill_window_millisec, System.Threading.Timeout.Infinite); }));
        }

        void close_Click(object sender, RoutedEventArgs e)
        {
            log.WriteInteractionLog(19, "frame closed with title: " + this.title.Text, ((TouchEventArgs)e).TouchDevice);
            this.window_killer_timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            this.window_killer_timer.Dispose();
            window_manager.close_window(this);
        }

        public void UpdateContents()
        {
            try { window_content w = (window_content)this.window_content.Content; w.UpdateKeyboardPosition(); }
            catch (Exception) { }
            try { signup s = (signup)this.window_content.Content; s.UpdateKeyboardLayout(); }
            catch (Exception) { }
        }
    }
}
