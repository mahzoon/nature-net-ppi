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
        private bool timer_enabled = true;
        private ImageBrush pushpin_icon;
        private ImageBrush pushpin_selected_icon;

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
            pushpin_icon = new ImageBrush();
            pushpin_icon.ImageSource = configurations.img_pushpin_icon;
            pushpin_selected_icon = new ImageBrush();
            pushpin_selected_icon.ImageSource = configurations.img_pushpin_selected_icon;
            this.pushpin.Background = pushpin_icon;
            this.window_icon.Source = configurations.img_collection_window_icon;

            if (configurations.response_to_mouse_clicks)
            {
                this.close.Click += new RoutedEventHandler(close_Click);
                this.pushpin.Click += new RoutedEventHandler(pushpin_Click);
            }
            this.close.PreviewTouchDown += new EventHandler<TouchEventArgs>(close_Click);
            this.pushpin.PreviewTouchDown += new EventHandler<TouchEventArgs>(pushpin_Click);

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

        public void kill_this_window(Object stateInfo)
        {
            log.WriteInteractionLog(19, "[Auto] frame closed with title: " + (string)stateInfo, null);
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new System.Action(() => { window_manager.close_window(this); }));
        }

        public void postpone_killer_timer(bool same_thread)
        {
            if (same_thread)
            {
                if (timer_enabled)
                    window_killer_timer.Change(configurations.kill_window_millisec, System.Threading.Timeout.Infinite);
            }
            else
                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new System.Action(() => { if (timer_enabled) window_killer_timer.Change(configurations.kill_window_millisec, System.Threading.Timeout.Infinite); }));
        }

        void close_Click(object sender, RoutedEventArgs e)
        {
            log.WriteInteractionLog(19, "frame closed with title: " + this.title.Text, ((TouchEventArgs)e).TouchDevice);
            this.window_killer_timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            this.window_killer_timer.Dispose();
            window_manager.close_window(this);
        }

        void pushpin_Click(object sender, RoutedEventArgs e)
        {
            bool s = toggle_timer();
            if (s)
                this.pushpin.Background = pushpin_icon;
            else
                this.pushpin.Background = pushpin_selected_icon;
        }

        public void disable_timer(bool same_thread)
        {
            if (same_thread)
            {
                window_killer_timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                timer_enabled = false;
            }
            else
                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new System.Action(() => { window_killer_timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite); timer_enabled = false; }));
        }

        public void enable_timer(bool same_thread)
        {
            timer_enabled = true;
            postpone_killer_timer(same_thread);
        }

        // should be fired in the same thread
        private bool toggle_timer()
        {
            if (timer_enabled)
                disable_timer(true);
            else
                enable_timer(true);
            return timer_enabled;
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
