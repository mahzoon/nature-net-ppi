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
using System.ComponentModel;
using System.Windows.Threading;

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for help_window.xaml
    /// </summary>
    public partial class help_window : UserControl
    {
        private System.Threading.Timer window_killer_timer;
        private bool timer_enabled = true;
        private ImageBrush pushpin_icon;
        private ImageBrush pushpin_selected_icon;

        public help_window()
        {
            InitializeComponent();

            //Static Configuration Values:
            this.the_item.Width = configurations.frame_width;
            this.the_item.Height = 300;
            this.the_content.Width = configurations.frame_width;
            this.the_media.Margin = new Thickness(0, -1 * configurations.frame_title_bar_height, 0, 0);
            this.metadata_panel.Background = Brushes.LightGray;

            this.title_bar.Height = configurations.frame_title_bar_height;
            var b1 = new ImageBrush();
            b1.ImageSource = configurations.img_close_icon;
            this.close.Background = b1;

            pushpin_icon = new ImageBrush();
            pushpin_icon.ImageSource = configurations.img_pushpin_icon;
            pushpin_selected_icon = new ImageBrush();
            pushpin_selected_icon.ImageSource = configurations.img_pushpin_selected_icon;
            this.pushpin.Background = pushpin_icon;

            this.close.PreviewTouchDown += new EventHandler<TouchEventArgs>(close_Click);
            this.pushpin.PreviewTouchDown += new EventHandler<TouchEventArgs>(pushpin_Click);

            RenderOptions.SetBitmapScalingMode(the_item, configurations.scaling_mode);

            this.PreviewTouchDown += new EventHandler<TouchEventArgs>(help_window_PreviewTouchDown);
        }

        void help_window_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            this.postpone_killer_timer(true);
            e.Handled = false;
        }

        public void view_help(string filename, string caption)
        {
            this.metadata1.Text = caption;
            the_media.Source = new Uri(configurations.GetAbsoluteImagePath() + filename);
            the_media.Play();
            the_media.MediaOpened += new RoutedEventHandler(the_media_MediaOpened);
            the_media.MediaEnded += new RoutedEventHandler(the_media_MediaEnded);
            //the_media.Loaded += new RoutedEventHandler(the_media_Loaded);
        }

        void the_media_MediaOpened(object sender, RoutedEventArgs e)
        {
            the_media.Visibility = System.Windows.Visibility.Visible;
            the_item.Background = Brushes.White;

            double h = the_media.NaturalVideoHeight;//window_manager.contributions[(int)e.Result].Height;
            double w = the_media.NaturalVideoWidth;//window_manager.contributions[(int)e.Result].Width;
            the_item.Height = (h / w) * the_item.Width;
            //the_media.Play();
            window_manager.UpdateZOrder(this, true);
        }

        void the_media_MediaEnded(object sender, RoutedEventArgs e)
        {
            the_media.Position = new TimeSpan(0);
            the_media.Play();
        }

        public void set_kill_timer()
        {
            window_killer_timer = new System.Threading.Timer(new System.Threading.TimerCallback(kill_this_window), null, configurations.kill_window_millisec, System.Threading.Timeout.Infinite);
        }

        public void kill_this_window(Object stateInfo)
        {
            log.WriteInteractionLog(19, "[Auto] help frame closed.", null);
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

        void pushpin_Click(object sender, RoutedEventArgs e)
        {
            bool s = toggle_timer();
            if (s)
                this.pushpin.Background = pushpin_icon;
            else
                this.pushpin.Background = pushpin_selected_icon;
        }

        void close_Click(object sender, RoutedEventArgs e)
        {
            try { log.WriteInteractionLog(19, "help frame closed.", ((TouchEventArgs)e).TouchDevice); }
            catch (Exception) { } // e might not be Touch
            this.window_killer_timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            this.window_killer_timer.Dispose();
            window_manager.close_window(this);
        }
    }
}
