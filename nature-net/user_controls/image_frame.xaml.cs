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
    /// Interaction logic for image_frame.xaml
    /// </summary>
    public partial class image_frame : UserControl
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
        public collection_item item;

        public ImageSource the_image;
        //public window_content win_content;

        public image_frame()
        {
            InitializeComponent();
            //this.title_bar.Background = configurations.contribution_view_title_bar_color;

            //Static Configuration Values:
            this.the_item.Width = configurations.frame_width;
            this.the_item.Height = 300;
            this.the_content.Width = configurations.frame_width;
            this.the_media.Margin = new Thickness(0, -1 * configurations.frame_title_bar_height, 0, 0);

            //this.title_bar.Background = new SolidColorBrush(Color.;
            //this.frame.BorderBrush = new SolidColorBrush(Color.;
            this.title_bar.Height = configurations.frame_title_bar_height;
            var b1 = new ImageBrush();
            b1.ImageSource = configurations.img_close_icon;
            this.close.Background = b1;

            if (configurations.response_to_mouse_clicks)
                this.close.Click += new RoutedEventHandler(close_Click);
            //this.change_view.Click += new RoutedEventHandler(change_view_Click);
            this.close.PreviewTouchDown += new EventHandler<TouchEventArgs>(close_Click);

            RenderOptions.SetBitmapScalingMode(the_item, configurations.scaling_mode);
        }

        public void view_contribution(collection_item i)
        {
            //ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteContributionPath() + contribution_id.ToString() + ".jpg"));
            //window_manager.contributions.Add(contribution_id, src);
            //the_image.Source = src;
            this.item = i;
            //the_media.Visibility = System.Windows.Visibility.Collapsed;
            //the_image.Visibility = System.Windows.Visibility.Visible;
            if (i.is_image || i.is_audio)
            {
                if (i.is_audio)
                {
                    the_item.Background = new ImageBrush(configurations.img_sound_image_pic);
                    the_item.UpdateLayout();
                    string fname = i._contribution.media_url;
                    string ext = fname.Substring(fname.Length - 4, 4);
                    the_media.Source = new Uri(configurations.GetAbsoluteContributionPath() + item._contribution.id.ToString() + ext);
                    the_media.Play();
                    return;
                }
                //if (window_manager.contributions.ContainsKey(i._contribution.id))
                //{
                //    double h = window_manager.contributions[i._contribution.id].Height;
                //    double w = window_manager.contributions[i._contribution.id].Width;
                //    the_item.Height = (h / w) * the_item.Width;
                //    the_item.Background = new ImageBrush(window_manager.contributions[i._contribution.id]);
                //    the_item.UpdateLayout();
                //}
                //else
                //{
                    worker.DoWork += new DoWorkEventHandler(load_image);
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(show_image);
                    worker.RunWorkerAsync((object)i._contribution.id);
                //}

            }
            if (i.is_video)
            {
                if (window_manager.thumbnails.ContainsKey(i._contribution.id))
                {
                    the_item.Background = new ImageBrush(window_manager.thumbnails[i._contribution.id]);
                    the_item.UpdateLayout();
                }
                string fname = i._contribution.media_url;
                string ext = fname.Substring(fname.Length - 4, 4);
                the_media.Source = new Uri(configurations.GetAbsoluteContributionPath() + item._contribution.id.ToString() + ext);
                the_media.Play();
                the_media.MediaOpened += new RoutedEventHandler(the_media_MediaOpened);
                //the_media.Loaded += new RoutedEventHandler(the_media_Loaded);
            }

        }

        public void load_image(object arg, DoWorkEventArgs e)
        {
            int contribution_id = (int)e.Argument;
            if (!window_manager.downloaded_contributions.Contains(contribution_id))
            {
                naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
                var result1 = from c in db.Contributions
                              where c.id == contribution_id
                              select c;
                if (result1.Count() != 0)
                {
                    Contribution contrib = result1.First<Contribution>();
                    bool result = file_manager.download_file_from_googledirve(contrib.media_url, contribution_id);
                    if (result) window_manager.downloaded_contributions.Add(contribution_id);
                }
            }
            try
            {
                ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteContributionPath() + contribution_id.ToString() + ".jpg"));
                src.Freeze();
                the_image = src;
                //window_manager.contributions.Add(contribution_id, src);
                e.Result = (object)contribution_id;
            }
            catch (Exception)
            {
                /// write log
                e.Result = -1;
            }
        }

        public void show_image(object us, RunWorkerCompletedEventArgs e)
        {
            this.the_item.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new System.Action(() =>
                {
                    if ((int)e.Result == -1)
                        the_item.Background = new ImageBrush(configurations.img_not_found_image_pic);
                    else
                    {
                        double h = the_image.Height;//window_manager.contributions[(int)e.Result].Height;
                        double w = the_image.Width;//window_manager.contributions[(int)e.Result].Width;
                        the_item.Height = (h / w) * the_item.Width;
                        the_item.Background = new ImageBrush(the_image);//window_manager.contributions[(int)e.Result]);
                    }
                    the_item.UpdateLayout();
                }));
        }

        void the_media_MediaOpened(object sender, RoutedEventArgs e)
        {
            the_media.Visibility = System.Windows.Visibility.Visible;
            the_item.Background = Brushes.White;
            the_item.Height = the_media.Height;
            //the_media.Play();
        }

        //void the_media_Loaded(object sender, RoutedEventArgs e)
        //{
        //    the_media.Visibility = System.Windows.Visibility.Visible;
        //    the_item.Background = Brushes.White;
        //    the_item.Height = the_media.NaturalVideoHeight;
        //    the_media.Play();
        //}

        void close_Click(object sender, RoutedEventArgs e)
        {
            window_manager.close_window(this);
        }

        public void UpdateContents()
        {
            try { window_content w = (window_content)this.window_content.Content; w.UpdateKeyboardPosition(); }
            catch (Exception) { }
        }
    }
}
