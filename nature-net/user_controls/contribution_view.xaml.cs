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
using System.Windows.Threading;
using System.ComponentModel;

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for image_view.xaml
    /// </summary>
    public partial class contribution_view : UserControl
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();

        public collection_item item;

        public contribution_view()
        {
            InitializeComponent();

            this.contribution_canvas.Height = 250;
            //this.contribution_canvas.IsManipulationEnabled = true;
            this.contribution_canvas.ManipulationStarting += new EventHandler<ManipulationStartingEventArgs>(image_canvas_ManipulationStarting);
            this.contribution_canvas.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(image_canvas_ManipulationDelta);

            this.the_media.TouchDown += new EventHandler<TouchEventArgs>(the_media_TouchDown);
            this.the_image.TouchDown += new EventHandler<TouchEventArgs>(the_image_TouchDown);
            the_media.LoadedBehavior = MediaState.Manual;
            the_media.ScrubbingEnabled = true;
            the_media.Volume = 100;
        }

        void the_image_TouchDown(object sender, TouchEventArgs e)
        {
            if (item.is_audio || item.is_video)
            {
                the_media.Position = new TimeSpan(0);
                the_media.Play();
            }
        }

        void the_media_TouchDown(object sender, TouchEventArgs e)
        {
            if (item.is_video)
            {
                the_media.Position = new TimeSpan(0);
                the_media.Play();
            }
        }

        public void view_contribution(collection_item i)
        {
            //ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteContributionPath() + contribution_id.ToString() + ".jpg"));
            //window_manager.contributions.Add(contribution_id, src);
            //the_image.Source = src;
            this.item = i;
            the_media.Visibility = System.Windows.Visibility.Collapsed;
            the_image.Visibility = System.Windows.Visibility.Visible;
            if (i.is_image || i.is_audio)
            {
                if (i.is_audio)
                {
                    the_image.Source = configurations.img_sound_image_pic;
                    the_image.UpdateLayout();
                    //string fname = i._contribution.media_url;
                    //string ext = fname.Substring(fname.Length - 4, 4);
                    the_media.Source = new Uri(configurations.GetAbsoluteContributionPath() + item._contribution.id.ToString());
                    the_media.Play();
                    return;
                }
                //if (window_manager.contributions.ContainsKey(i._contribution.id))
                //{
                //    the_image.Source = window_manager.contributions[i._contribution.id];
                //    the_image.UpdateLayout();
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
                    the_image.Source = window_manager.thumbnails[i._contribution.id];
                    the_image.UpdateLayout();
                }
                //string fname = i._contribution.media_url;
                //string ext = fname.Substring(fname.Length - 4, 4);
                the_media.Source = new Uri(configurations.GetAbsoluteContributionPath() + item._contribution.id.ToString());
                //the_media.Visibility = System.Windows.Visibility.Visible;
                //the_image.Visibility = System.Windows.Visibility.Collapsed;
                //string fname = i._contribution.media_url;
                //string ext = fname.Substring(fname.Length - 4, 4);
                //the_media.Source = new Uri(configurations.GetAbsoluteContributionPath() + item._contribution.id.ToString() + ext);
                the_media.Loaded += new RoutedEventHandler(the_media_Loaded);
            }

        }


        void the_media_Loaded(object sender, RoutedEventArgs e)
        {
            the_media.Visibility = System.Windows.Visibility.Visible;
            the_image.Visibility = System.Windows.Visibility.Collapsed;
            the_media.Play();
        }

        public void center_image()
        {
            if (the_image.Source != null)
            {
                the_image.UpdateLayout();
                contribution_canvas.UpdateLayout();
                var matrix = ((MatrixTransform)contribution_canvas.RenderTransform).Matrix;
                matrix.OffsetX = matrix.OffsetX + (contribution_canvas.ActualWidth / 2) - (the_image.ActualWidth / 2);
                matrix.OffsetY = matrix.OffsetY + (contribution_canvas.ActualHeight / 2) - (the_image.ActualHeight / 2);
                the_image.RenderTransform = new MatrixTransform(matrix);
            }
            //if (item.is_video)
            //{
                //the_media.UpdateLayout();
                //contribution_canvas.UpdateLayout();
                //var matrix = ((MatrixTransform)contribution_canvas.RenderTransform).Matrix;
                //matrix.OffsetX = matrix.OffsetX + (contribution_canvas.ActualWidth / 2) - (the_media.Width / 2);
                //matrix.OffsetY = matrix.OffsetY + (contribution_canvas.ActualHeight / 2) - (the_media.Height / 2);
                //the_media.RenderTransform = new MatrixTransform(matrix);
            //}
        }

        public void load_image(object arg, DoWorkEventArgs e)
        {
            int contribution_id = (int)e.Argument;
            if (!window_manager.downloaded_contributions.ContainsKey(contribution_id))
            {
                naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
                var result1 = from c in db.Contributions
                              where c.id == contribution_id
                              select c;
                if (result1.Count() != 0)
                {
                    Contribution contrib = result1.First<Contribution>();
                    //bool result = file_manager.download_file_from_googledirve(contrib.media_url, contribution_id);
                    bool result = file_manager.download_file(contrib.media_url, contribution_id);
                    if (result) window_manager.downloaded_contributions.Add(contribution_id, file_manager.get_extension(contrib.media_url));
                }
            }
            try
            {
                ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteContributionPath() + contribution_id.ToString() + ".jpg"));
                src.Freeze();
                the_image.Source = src;
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
            this.the_image.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new System.Action(() =>
                {
                    if ((int)e.Result == -1)
                        the_image.Source = configurations.img_not_found_image_pic;
                    //else
                    //    the_image.Source = window_manager.contributions[(int)e.Result];
                    the_image.UpdateLayout();


                    var matrix = ((MatrixTransform)contribution_canvas.RenderTransform).Matrix;
                    matrix.OffsetX = matrix.OffsetX + (contribution_canvas.ActualWidth / 2) - (the_image.ActualWidth / 2);
                    matrix.OffsetY = matrix.OffsetY + (contribution_canvas.ActualHeight / 2) - (the_image.ActualHeight / 2);
                    the_image.RenderTransform = new MatrixTransform(matrix);
                }));
        }

        void image_canvas_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this.contribution_canvas;
            e.Mode = ManipulationModes.All;
        }

        void image_canvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)e.Source;
            if (element == null) return;
            var deltaManipulation = e.DeltaManipulation;
            var matrix = ((MatrixTransform)element.RenderTransform).Matrix;
            Point center = new Point(element.ActualWidth / 2, element.ActualHeight / 2);
            center = matrix.Transform(center);
            //double scale_x_old = matrix.M11;
            //double scale_y_old = matrix.M22;
            if (!item.is_audio)
                //if (the_image.ActualHeight < 300 && the_image.ActualHeight > 100 && the_image.ActualWidth < 300 && the_image.ActualWidth > 100)
                //    if (deltaManipulation.Scale.X >= 0.5 && deltaManipulation.Scale.X <= 2.5
                //        && deltaManipulation.Scale.Y >= 0.5 && deltaManipulation.Scale.Y <= 2.5)
                        matrix.ScaleAt(deltaManipulation.Scale.X, deltaManipulation.Scale.Y, center.X, center.Y);
            //if (matrix.M11 < 0.5) matrix.M11 = 0.5; if (matrix.M11 > 2.0) matrix.M11 = 2.0;
            //if (matrix.M22 < 0.5) matrix.M22 = 0.5; if (matrix.M22 > 2.0) matrix.M22 = 2.0;
            if (!item.is_audio)
                matrix.RotateAt(e.DeltaManipulation.Rotation, center.X, center.Y);
            //matrix.Translate(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);

            //matrix.OffsetX = ((MatrixTransform)image_canvas.RenderTransform).Matrix.OffsetX + (image_canvas.ActualWidth / 2) - (the_image.ActualWidth / 2);
            //matrix.OffsetY = ((MatrixTransform)image_canvas.RenderTransform).Matrix.OffsetY + (image_canvas.ActualHeight / 2) - (the_image.ActualHeight / 2);

            element.RenderTransform = new MatrixTransform(matrix);

            e.Handled = true;
        }
    }
}
