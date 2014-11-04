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
using System.IO;
using Microsoft.Surface;
using Microsoft.Surface.Core;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for collection_listbox.xaml
    /// </summary>
    public partial class collection_listbox : UserControl
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private readonly BackgroundWorker worker_retry = new BackgroundWorker();

        private Point drag_direction1 = new Point(0, 1);
        private Point drag_direction2 = new Point(0, -1);

        public window_frame parent;
        private double debug_var = 10;
        private Canvas debug_canvas = new Canvas();
        
        private List<collection_listbox_item> upload_failed_items = new List<collection_listbox_item>();
        private collection_listbox_item upload_failed_cli = new collection_listbox_item();

        private List<System.Windows.Input.TouchPoint> touch_points = new List<System.Windows.Input.TouchPoint>();
        private int consecutive_drag_points = 0;
        ListBoxItem last_dragged_element = null;
        double last_scroll_offset = 0;

        string collection_username;

        public collection_listbox()
        {
            InitializeComponent();
            contributions.initialize(true, "", new ItemSelected(this.item_selected));

            collection_listbox_item cli = new collection_listbox_item();
            cli.img.Source = configurations.img_loading_image_pic;
            cli.img.Tag = null;
            //cli.percentage.Visibility = System.Windows.Visibility.Visible;
            cli.drag.Visibility = System.Windows.Visibility.Collapsed;
            this.contributions._list.Items.Add(cli);
            
            debug_canvas.Width = window_manager.main_canvas.ActualWidth;
            debug_canvas.Height = window_manager.main_canvas.ActualHeight;
            window_manager.main_canvas.Children.Add(debug_canvas);
            this.contributions.collection_list = true;
            this.Height = configurations.collection_listbox_height;

            this.Loaded += new RoutedEventHandler(collection_listbox_Loaded);
            //this.contributions.PreviewTouchUp += new EventHandler<System.Windows.Input.TouchEventArgs>(contributions_PreviewTouchUp);
            //this.contributions.PreviewTouchMove += new EventHandler<System.Windows.Input.TouchEventArgs>(contributions_PreviewTouchMove);
            //this.contributions.PreviewTouchDown += new EventHandler<System.Windows.Input.TouchEventArgs>(contributions_TouchDown);
        }

        void collection_listbox_Loaded(object sender, RoutedEventArgs e)
        {
            //GradientStopCollection gsc = new GradientStopCollection();
            //gsc.Add(new GradientStop(Colors.LightGray, 0.0));
            //gsc.Add(new GradientStop(Colors.LightGray, (this.Height - 18) / this.Height - 0.01));
            //gsc.Add(new GradientStop(Colors.Transparent, (this.Height - 18) / this.Height + 0.02));
            //gsc.Add(new GradientStop(Colors.Transparent, 1.0));

            //LinearGradientBrush myBrush = new LinearGradientBrush(gsc, 90);

            //this.contributions.Background = myBrush;
            this.contributions.Background = Brushes.LightGray;
        }

        void contributions_PreviewTouchMove(object sender, System.Windows.Input.TouchEventArgs e)
        {
            FrameworkElement findSource = e.OriginalSource as FrameworkElement;
            ListBoxItem element = null;
            while (element == null && findSource != null)
                if ((element = findSource as ListBoxItem) == null)
                    findSource = VisualTreeHelper.GetParent(findSource) as FrameworkElement;

            if (element != null)
                last_dragged_element = element;

            //TextBlock tm = new TextBlock(); tm.Foreground = Brushes.White;
            //Canvas.SetLeft(tm, 200); Canvas.SetTop(tm, debug_var);
            //tm.Text = "MOVED"; tm.FontSize = 16; tm.FontWeight = FontWeights.Bold;
            ////window_manager.main_canvas.Children.Add(tm);
            //debug_canvas.Children.Add(tm);
            //debug_var = debug_var + 30;
            //if (debug_var > 600) { debug_var = 10; debug_canvas.Children.RemoveRange(0, debug_canvas.Children.Count); }

            this.touch_points.Add(e.GetTouchPoint(this.contributions as IInputElement));

            //TouchPointCollection points = e.GetIntermediateTouchPoints(sender as IInputElement);
            //if (points.Count < 2) return;
            if (touch_points.Count < configurations.min_touch_points) return;

            //MatrixTransform trans_mat = (MatrixTransform)parent.RenderTransform;
            //Matrix mat = trans_mat.Matrix;
            //MatrixTransform mat2 = new MatrixTransform(mat.M11, mat.M12, mat.M21, mat.M22, 0, 0);

            //Point drag_dir1 = mat2.Transform(drag_direction1);
            //Point drag_dir2 = mat2.Transform(drag_direction2);
            //double size1 = Math.Sqrt(drag_dir1.X * drag_dir1.X + drag_dir1.Y * drag_dir1.Y);
            //double size2 = Math.Sqrt(drag_dir2.X * drag_dir2.X + drag_dir2.Y * drag_dir2.Y);
            //drag_dir1.X = drag_dir1.X / size1; drag_dir1.Y = drag_dir1.Y / size1;
            //drag_dir2.X = drag_dir2.X / size2; drag_dir2.Y = drag_dir2.Y / size2;

            //double dy = points[points.Count - 1].Position.Y - points[0].Position.Y;
            //double dx = points[points.Count - 1].Position.X - points[0].Position.X;
            //double dy = touch_points[touch_points.Count - 1].Position.Y - touch_points[0].Position.Y;
            //double dx = touch_points[touch_points.Count - 1].Position.X - touch_points[0].Position.X;
            double dy = touch_points[touch_points.Count - 1].Position.Y - touch_points[touch_points.Count - 2].Position.Y;
            double dx = touch_points[touch_points.Count - 1].Position.X - touch_points[touch_points.Count - 2].Position.X;
            double size_n = Math.Sqrt(dx * dx + dy * dy);
            dx = dx / size_n; dy = dy / size_n;
            if (dx == double.NaN || dy == double.NaN) return;
            //Point dxy = mat2.Transform(new Point(dx, dy));
            //dx = Math.Abs(dx); dy = Math.Abs(dy);

            //double theta1 = Math.Acos(dx * drag_dir1.X + dy * drag_dir1.Y);
            //double theta2 = Math.Acos(dx * drag_dir2.X + dy * drag_dir2.Y);
            double theta1 = Math.Acos(dx * drag_direction1.X + dy * drag_direction1.Y);
            double theta2 = Math.Acos(dx * drag_direction2.X + dy * drag_direction2.Y);

            //convert to degree
            theta1 = theta1 * 180 / Math.PI;
            theta2 = theta2 * 180 / Math.PI;
            double theta = (theta1 < theta2) ? theta1 : theta2;

            /////////
            //double radius = 75;
            //Ellipse ell = new Ellipse();
            //ell.Fill = Brushes.White;
            //ell.Width = radius*2;
            //ell.Height = radius*2;
            //double center_x = (window_manager.main_canvas.ActualWidth / 2);
            //double center_y = (window_manager.main_canvas.ActualHeight / 2);
            //double left = center_x - radius;
            //double top = center_y - radius;
            //Canvas.SetLeft(ell, left);
            //Canvas.SetTop(ell, top);
            //window_manager.main_canvas.Children.Add(ell);
            //Line l1 = new Line();
            //l1.X1 = left; l1.X2 = left + ell.Width;
            //l1.Y1 = center_y; l1.Y2 = center_y;
            //l1.Stroke = Brushes.Black; l1.StrokeThickness = 3;
            //window_manager.main_canvas.Children.Add(l1);
            //Line l2 = new Line();
            //l2.X1 = center_x; l2.X2 = center_x;
            //l2.Y1 = top; l2.Y2 = top + ell.Height;
            //l2.Stroke = Brushes.Black; l2.StrokeThickness = 3;
            //window_manager.main_canvas.Children.Add(l2);
            //////Line l_drag1 = new Line();
            //////l_drag1.X1 = center_x; l_drag1.X2 = (drag_dir1.X*radius) + center_x;
            //////l_drag1.Y1 = center_y; l_drag1.Y2 = (drag_dir1.Y*radius) + center_y;
            //////l_drag1.Stroke = Brushes.Red; l_drag1.StrokeThickness = 5;
            //////window_manager.main_canvas.Children.Add(l_drag1);
            //////Line l_drag2 = new Line();
            //////l_drag2.X1 = center_x; l_drag2.X2 = (drag_dir2.X*radius) + center_x;
            //////l_drag2.Y1 = center_y; l_drag2.Y2 = (drag_dir2.Y*radius) + center_y;
            //////l_drag2.Stroke = Brushes.Red; l_drag2.StrokeThickness = 5;
            //////window_manager.main_canvas.Children.Add(l_drag2);
            //Line l_dxy = new Line();
            //l_dxy.X1 = center_x; l_dxy.X2 = (dx * radius) + center_x;
            //l_dxy.Y1 = center_y; l_dxy.Y2 = (dy * radius) + center_y;
            //l_dxy.Stroke = Brushes.Green; l_dxy.StrokeThickness = 5;
            //window_manager.main_canvas.Children.Add(l_dxy);
            //TextBlock t1 = new TextBlock(); t1.Foreground = Brushes.White;
            //Canvas.SetLeft(t1, 80); Canvas.SetTop(t1, debug_var);
            //t1.Text = theta1.ToString(); t1.FontSize = 14; t1.FontWeight = FontWeights.Bold;
            //debug_canvas.Children.Add(t1);
            //TextBlock t2 = new TextBlock(); t2.Foreground = Brushes.White;
            //Canvas.SetLeft(t2, 80 + (2*radius)); Canvas.SetTop(t2, debug_var);
            //t2.Text = theta2.ToString(); t2.FontSize = 14; t2.FontWeight = FontWeights.Bold;
            //debug_canvas.Children.Add(t2);
            //debug_var = debug_var + 30;
            //if (debug_var > 600) { debug_var = 10; debug_canvas.Children.RemoveRange(0, debug_canvas.Children.Count); }

            //draw points
            //double radius = 2;
            //Ellipse ell = new Ellipse();
            //ell.Fill = Brushes.White;
            //ell.Width = radius * 2;
            //ell.Height = radius * 2;
            //double center_x = e.GetTouchPoint(window_manager.main_canvas).Position.X;
            //double center_y = e.GetTouchPoint(window_manager.main_canvas).Position.Y;
            //double left = center_x - radius;
            //double top = center_y - radius;
            //Canvas.SetLeft(ell, left);
            //Canvas.SetTop(ell, top);
            //window_manager.main_canvas.Children.Add(ell);

            /////////

            if (theta < configurations.drag_collection_theta)
            {
                if (consecutive_drag_points < configurations.max_consecutive_drag_points)
                {
                    consecutive_drag_points++;
                }
                else
                {
                    if (element == null) element = last_dragged_element;
                    if (element != null)
                    {
                        Image i = (Image)element.DataContext;
                        if (i.Tag == null) return;
                        collection_item item = (collection_item)i.Tag;
                        if (i.Source != null)
                        {
                            start_drag(element, item, e.TouchDevice, i.Source.Clone());
                            touch_points.Clear();
                            consecutive_drag_points = 0;
                            e.Handled = true;
                            return;
                        }
                    }
                }
            }
            ScrollViewer scroll = configurations.GetDescendantByType(this.contributions, typeof(ScrollViewer)) as ScrollViewer;
            double dv = touch_points[touch_points.Count - 1].Position.X - touch_points[0].Position.X;
            //double new_offset = scroll.HorizontalOffset + (-1 * configurations.scroll_scale_factor * dx);
            double new_offset = last_scroll_offset + (-1 * dv);
            try { scroll.ScrollToHorizontalOffset(new_offset); }
            catch (Exception) { }
        }

        void contributions_TouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            ScrollViewer scroll = configurations.GetDescendantByType(this.contributions, typeof(ScrollViewer)) as ScrollViewer;
            last_scroll_offset = scroll.HorizontalOffset;
            bool r = e.TouchDevice.Capture(this.contributions as IInputElement, CaptureMode.SubTree);
            //if (!r)
            //{
            //    TextBlock t2 = new TextBlock(); t2.Foreground = Brushes.White;
            //    Canvas.SetLeft(t2, 200); Canvas.SetTop(t2, debug_var);
            //    t2.Text = "FAILED"; t2.FontSize = 16; t2.FontWeight = FontWeights.Bold;
            //    //window_manager.main_canvas.Children.Add(t2);
            //    debug_canvas.Children.Add(t2);
            //    debug_var = debug_var + 30;
            //    if (debug_var > 600) { debug_var = 10; debug_canvas.Children.RemoveRange(0, debug_canvas.Children.Count); }
            //}
            //else
            //{
            //    TextBlock t2 = new TextBlock(); t2.Foreground = Brushes.White;
            //    Canvas.SetLeft(t2, 200); Canvas.SetTop(t2, debug_var);
            //    t2.Text = "CAPTURED"; t2.FontSize = 16; t2.FontWeight = FontWeights.Bold;
            //    //window_manager.main_canvas.Children.Add(t2);
            //    debug_canvas.Children.Add(t2);
            //    debug_var = debug_var + 30;
            //    if (debug_var > 600) { debug_var = 10; debug_canvas.Children.RemoveRange(0, debug_canvas.Children.Count); }
            //}
            e.Handled = true;
        }

        void contributions_PreviewTouchUp(object sender, System.Windows.Input.TouchEventArgs e)
        {
            //TextBlock tm = new TextBlock(); tm.Foreground = Brushes.White;
            //Canvas.SetLeft(tm, 200); Canvas.SetTop(tm, debug_var);
            //tm.Text = "TOUCH UP"; tm.FontSize = 16; tm.FontWeight = FontWeights.Bold;
            ////window_manager.main_canvas.Children.Add(tm);
            //debug_canvas.Children.Add(tm);
            //debug_var = debug_var + 30;
            //if (debug_var > 600) { debug_var = 10; debug_canvas.Children.RemoveRange(0, debug_canvas.Children.Count); }
            if (touch_points.Count > 0)
            {
                ScrollViewer scroll = configurations.GetDescendantByType(this.contributions, typeof(ScrollViewer)) as ScrollViewer;
                //double dv = e.GetTouchPoint(this.contributions).Position.X - touch_points[touch_points.Count - 1].Position.X;
                double dv = e.GetTouchPoint(this.contributions).Position.X - touch_points[0].Position.X;
                try
                {
                    //scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset + (-2 * dv));
                    scroll.ScrollToHorizontalOffset(last_scroll_offset + (-1 * dv));
                }
                catch (Exception) { }
                last_scroll_offset = scroll.HorizontalOffset;
            }

            this.touch_points.Clear();
            consecutive_drag_points = 0;
            UIElement element = sender as UIElement;
            element.ReleaseTouchCapture(e.TouchDevice);
            //e.Handled = false;
        }

        public void list_contributions_in_location(int location)
        {
            this.contributions.content_name = "contributions in location: " + location.ToString();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(get_contributions_in_location);
            worker.ProgressChanged += new ProgressChangedEventHandler(progress_changed);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_contributions);
            if (!worker.IsBusy)
                worker.RunWorkerAsync((object)location);
        }

        public void list_contributions_in_activity(int activity_id)
        {
            this.contributions.content_name = "contributions in activity: " + activity_id.ToString();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(get_contributions_in_activity);
            worker.ProgressChanged += new ProgressChangedEventHandler(progress_changed);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_contributions);
            if (!worker.IsBusy)
                worker.RunWorkerAsync((object)activity_id);
        }

        public void list_all_contributions(string username)
        {
            this.collection_username = username;
            this.contributions.content_name = "contributions for user: " + username;
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(get_all_contributions);
            worker.ProgressChanged += new ProgressChangedEventHandler(progress_changed);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_contributions);
            if (!worker.IsBusy)
                worker.RunWorkerAsync((object)username);
        }

        public void get_all_contributions(object arg, DoWorkEventArgs e)
        {
            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
            var result1 = from c in db.Collection_Contribution_Mappings
                          where (c.Collection.User.name == (string)e.Argument) && (c.Collection.activity_id != 1)
                          && (c.Contribution.status != configurations.status_deleted)
                          select c.Contribution;
            if (result1 == null)
            {
                e.Result = (object)(new List<collection_item>());
                return;
            }
            //List<int> contribution_ids = result1.ToList<int>();
            //var result2 = from m in db.Contributions
            //              where contribution_ids.Contains(m.id)
            //              select m;
            List<Contribution> medias = result1.ToList<Contribution>();
            List<collection_item> loaded_items = new List<collection_item>();
            loading_progress lp0 = new loading_progress();
            lp0.current_progress = 0; lp0.total = medias.Count;
            lp0.loaded_items = loaded_items;
            worker.ReportProgress(0, lp0);
            // download the image if there is no image;
            // create thumbnail if there is no thumbnail
            List<collection_item> items = new List<collection_item>();
            
            for (int counter = 0; counter < medias.Count; counter++)
            {
                collection_item ci = create_collection_item_from_contribution(medias[counter]);
                items.Add(ci);
                loaded_items.Add(ci);
                loading_progress lp = new loading_progress();
                lp.current_progress = counter + 1;
                lp.total = medias.Count;
                lp.loaded_items = loaded_items; 
                worker.ReportProgress(counter + 1, lp);
            }
            e.Result = (object)items;
        }

        void progress_changed(object sender, ProgressChangedEventArgs e)
        {
            this.contributions._list.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
               new System.Action(() =>
               {
                   try
                   {
                       loading_progress progress = (loading_progress)e.UserState;
                       //collection_listbox_item cli = (collection_listbox_item)this.contributions._list.Items[this.contributions._list.Items.Count - 1];
                       string progress_text = " (loading " + (e.ProgressPercentage).ToString() + " of " + progress.total + ")";
                       string[] title = this.parent.get_title().Split(new string[] { " (" }, StringSplitOptions.RemoveEmptyEntries);
                       this.parent.set_title(title[0] + progress_text);

                       if (progress.current_progress == 0)
                       {
                           this.contributions._list.Items.Clear();
                           for (int counter = 0; counter < progress.total; counter++)
                           {
                               collection_listbox_item c = new collection_listbox_item();
                               c.img.Source = configurations.img_loading_image_pic;
                               //c.img.GifSource = configurations.GetAbsoluteImagePath() + "loading1.gif";
                               c.img.Tag = null;
                               //c.percentage.Visibility = System.Windows.Visibility.Collapsed;
                               //c.percentage.Text = (e.ProgressPercentage).ToString() + " of " + progress.total + " contribution(s) downloaded.";
                               c.drag.Visibility = System.Windows.Visibility.Collapsed;
                               this.contributions._list.Items.Add(c);
                           }
                       }
                       else
                       {
                           ////load the item
                           collection_item i = progress.loaded_items[progress.current_progress - 1];
                           collection_listbox_item cli2 = new collection_listbox_item();

                           //Image img = new Image();
                           if (window_manager.thumbnails.ContainsKey(i._contribution.id))
                           {
                               cli2.img.Source = window_manager.thumbnails[i._contribution.id];
                               cli2.img.Tag = i;
                               cli2.drag.Source = configurations.img_drag_icon;
                           }
                           else
                           {
                               cli2.img.Source = configurations.img_not_found_image_pic;
                               cli2.img.Tag = null;
                               //cli2.Width = 0;
                               cli2.Visibility = System.Windows.Visibility.Collapsed;
                               upload_failed_items.Add(cli2);
                           }
                           cli2.collection_item = i;
                           this.contributions._list.Items.RemoveAt(progress.current_progress - 1);
                           this.contributions._list.Items.Insert(progress.current_progress - 1, cli2);
                       }

                       this.contributions._list.Items.Refresh();
                   }
                   catch (Exception) { }
               }));
        }

        public void display_all_contributions(object c_obj, RunWorkerCompletedEventArgs e)
        {
            this.contributions._list.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
               new System.Action(() =>
               {
                   //this.contributions._list.Items.Clear();
                   List<collection_item> items;
                   if (e.Result == null)
                       items = new List<collection_item>();
                   else
                       items = (List<collection_item>)e.Result;

                   int item_count = items.Count + upload_failed_items.Count;
                   if (item_count == 0)
                   {
                       this.contributions._list.Items.Clear();
                       Image img = new Image();
                       img.Source = configurations.img_empty_image_pic;
                       img.Tag = null;
                       this.contributions._list.Items.Add(img);

                       string[] title = this.parent.get_title().Split(new string[] { " (" }, StringSplitOptions.RemoveEmptyEntries);
                       this.parent.set_title(title[0]);
                       this.contributions.content_name = this.contributions.content_name + " ;" + title[0] + " (" + item_count + ")";
                   }
                   else
                   {
                       string[] title = this.parent.get_title().Split(new string[] { " (" }, StringSplitOptions.RemoveEmptyEntries);
                       this.parent.set_title(title[0] + " (" + item_count + ")");
                       this.contributions.content_name = this.contributions.content_name + " ;" + title[0] + " (" + item_count + ")";
                   }
                   if (upload_failed_items.Count != 0)
                   {
                       upload_failed_cli = new collection_listbox_item();
                       upload_failed_cli.Width = 150;
                       //upload_failed_cli.img.Source = configurations.img_not_found_image_pic;
                       upload_failed_cli.upload_failed_count.FontFamily = new System.Windows.Media.FontFamily("Calibri");
                       upload_failed_cli.upload_failed_count.FontSize = 24;
                       upload_failed_cli.upload_failed_count.Foreground = Brushes.DarkSlateGray;
                       upload_failed_cli.upload_failed_count.TextWrapping = TextWrapping.Wrap;
                       upload_failed_cli.upload_failed_count.Text = configurations.still_uploading_text;
                       upload_failed_cli.img.Tag = null;
                       upload_failed_cli.upload_failed_count.Text += " (x" + this.upload_failed_items.Count + ")";
                       this.contributions._list.Items.Add(upload_failed_cli);
                   }
                   this.contributions._list.Items.Refresh();
                   window_manager.contribution_collection_opened(this.collection_username);
               }));
        }

        public void get_contributions_in_location(object arg, DoWorkEventArgs e)
        {
            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
            var result1 = from c in db.Collection_Contribution_Mappings
                          where (c.Contribution.location_id == (int)e.Argument) && (c.Collection.activity_id != 1)
                          && (c.Contribution.status != configurations.status_deleted)
                          select c.Contribution;
            //var result1 = from c in db.Contributions
            //              where c.location_id == (int)e.Argument
            //              select c;
            if (result1 == null)
            {
                e.Result = (object)(new List<collection_item>());
                return;
            }
            List<Contribution> medias = result1.ToList<Contribution>();
            List<collection_item> loaded_items = new List<collection_item>();
            loading_progress lp0 = new loading_progress();
            lp0.current_progress = 0; lp0.total = medias.Count;
            lp0.loaded_items = loaded_items;
            worker.ReportProgress(0, lp0);

            List<collection_item> items = new List<collection_item>();
            for (int counter = 0; counter < medias.Count; counter++)
            {
                collection_item ci = create_collection_item_from_contribution(medias[counter]);
                items.Add(ci);
                loaded_items.Add(ci);
                loading_progress lp = new loading_progress();
                lp.current_progress = counter + 1;
                lp.total = medias.Count;
                lp.loaded_items = loaded_items;
                worker.ReportProgress(counter + 1, lp);
            }
            e.Result = (object)items;
        }

        public void get_contributions_in_activity(object arg, DoWorkEventArgs e)
        {
            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
            var result0 = from c0 in db.Collection_Contribution_Mappings
                          where c0.Collection.activity_id == (int)e.Argument
                          && (c0.Contribution.status != configurations.status_deleted)
                          orderby c0.Contribution.date descending
                          select c0.Contribution;
            if (result0 == null)
            {
                e.Result = (object)(new List<collection_item>());
                return;
            }
            List<Contribution> medias = result0.ToList<Contribution>();
            List<collection_item> loaded_items = new List<collection_item>();
            loading_progress lp0 = new loading_progress();
            lp0.current_progress = 0; lp0.total = medias.Count;
            lp0.loaded_items = loaded_items;
            worker.ReportProgress(0, lp0);

            List<collection_item> items = new List<collection_item>();
            for (int counter = 0; counter < medias.Count; counter++)
            {
                collection_item ci = create_collection_item_from_contribution(medias[counter]);
                items.Add(ci);
                loaded_items.Add(ci);
                loading_progress lp = new loading_progress();
                lp.current_progress = counter + 1;
                lp.total = medias.Count;
                lp.loaded_items = loaded_items;
                worker.ReportProgress(counter + 1, lp);
            }
            e.Result = (object)items;
        }

        public collection_item create_collection_item_from_contribution(Contribution c)
        {
            collection_item ci = new collection_item();
            ci._contribution = c;

            if (c.tags != null)
            {
                if (c.tags.Contains("Photo"))
                    ci.is_image = true;
                if (c.tags.Contains("Video"))
                    ci.is_video = true;
                if (c.tags.Contains("Audio"))
                    ci.is_audio = true;
            }

            if (c.media_url == null || c.media_url == "") return ci;
            //string fname = c.media_url;
            //string ext = fname.Substring(fname.Length - 4, 4);
            //ext = ext.ToLower();
            //if (ext == ".jpg" || ext == ".bmp" || ext == ".png")
            //    ci.is_image = true;
            //if (ext == ".wmv" || ext == ".mpg" || ext == "mpeg" || ext == ".avi" || ext == ".mp4" || ext == ".3gp" || ext == ".mov")
            //    ci.is_video = true;
            //if (ext == ".wav" || ext == ".mp3")
            //    ci.is_audio = true;

            try_downloading_contribution(ci, false, false);
            return ci;
        }

        private void try_downloading_contribution(collection_item ci, bool force_download, bool force_create_thumbnail)
        {
            int i = ci._contribution.id;
            if (!window_manager.downloaded_contributions.Contains(i) || force_download)
            {
                //bool result = file_manager.download_file_from_googledirve(c.media_url, i);
                bool result = file_manager.download_file(ci._contribution.media_url, i);
                if (result) window_manager.downloaded_contributions.Add(i);
            }

            if (!window_manager.thumbnails.ContainsKey(i) || force_create_thumbnail)
            {
                ImageSource img = null;
                if (ci.is_image)
                    img = configurations.GetThumbnailFromImage(i.ToString(), configurations.thumbnail_pixel_height);
                if (ci.is_video)
                    img = configurations.GetThumbnailFromVideo(i.ToString(), configurations.thumbnail_video_span, configurations.thumbnail_pixel_height);
                if (ci.is_audio)
                    img = configurations.img_sound_image_pic;
                if (img == null)
                    return;

                //if (!window_manager.thumbnails.ContainsKey(i))
                //{
                // save the thumbnail
                try
                {
                    BitmapSource bs = img as BitmapSource;
                    if (!ci.is_audio)
                    {
                        configurations.SaveThumbnail(bs, i.ToString());
                    }
                    img = new BitmapImage(new Uri(configurations.GetAbsoluteThumbnailPath() + i.ToString() + ".jpg"));
                    img.Freeze();
                    if (!window_manager.thumbnails.ContainsKey(i) || force_create_thumbnail)
                        window_manager.thumbnails.Add(i, img);
                    else
                        window_manager.thumbnails[i] = img;
                }
                catch (Exception) { }   // not a problem
                //}
            }
        }

        bool item_selected(object i, System.Windows.Input.TouchEventArgs e)
        {
            collection_listbox_item item = (collection_listbox_item)i;
            collection_item ci = (collection_item)item.img.Tag;
            if (ci != null)
            {
                log.WriteInteractionLog(17, "tapped the collection item, contribution id: " + ci._contribution.id, e.TouchDevice);
                window_manager.open_contribution_window(ci,
                    item.PointToScreen(new Point(0, 0)).X - window_manager.main_canvas.PointToScreen(new Point(0, 0)).X,
                    item.PointToScreen(new Point(0, 0)).Y, ci.ToString());
            }
            else
            {
                log.WriteInteractionLog(17, "tapped the collection item (retry item)", e.TouchDevice);
                worker_retry.DoWork += new DoWorkEventHandler(retry_downloading);
                //worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_contributions);
                if (!worker_retry.IsBusy)
                    worker_retry.RunWorkerAsync();
            }
            return true;
        }

        void retry_downloading(object sender, DoWorkEventArgs e)
        {
            if (upload_failed_items.Count == 0) return;
            this.contributions._list.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    new System.Action(() =>
                    { upload_failed_cli.upload_failed_count.Text = "(retrying...)"; }));
            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
            List<collection_listbox_item> failed_items = new List<collection_listbox_item>();
            foreach (collection_listbox_item cli in upload_failed_items)
            {
                collection_item ci = (collection_item)cli.collection_item;
                if (ci == null) continue;
                var contribs = from c in db.Contributions
                               where ci._contribution.id == c.id
                               select c;
                if (contribs.Count() != 1) continue;
                ci._contribution = contribs.Single<Contribution>();
                try_downloading_contribution(ci, true, true);
                if (!window_manager.thumbnails.ContainsKey(ci._contribution.id))
                { failed_items.Add(cli); }           
            }

            this.contributions._list.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new System.Action(() =>
                {
                    foreach (collection_listbox_item cli2 in upload_failed_items)
                    {
                        collection_item ci2 = (collection_item)cli2.collection_item;
                        if (ci2 == null) continue;
                        if (window_manager.thumbnails.ContainsKey(ci2._contribution.id))
                        {
                            cli2.img.Source = window_manager.thumbnails[ci2._contribution.id];
                            cli2.img.Tag = ci2;
                            cli2.drag.Source = configurations.img_drag_icon;
                            cli2.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            cli2.img.Source = configurations.img_not_found_image_pic;
                            cli2.img.Tag = null;
                            //cli2.Width = 0;
                            cli2.Visibility = System.Windows.Visibility.Collapsed;
                        }
                    }
                    if (failed_items.Count == 0)
                        this.contributions._list.Items.Remove(upload_failed_cli);
                    else
                    {
                        //upload_failed_cli.img.Source = configurations.img_not_found_image_pic;
                        upload_failed_cli.upload_failed_count.Text = configurations.still_uploading_text;
                        upload_failed_cli.upload_failed_count.Text += " (x" + failed_items.Count + ")";
                        this.contributions._list.Items.Refresh();
                        this.contributions._list.UpdateLayout();
                    }
                    upload_failed_items = failed_items;
                }));
        }

        public bool start_drag(ListBoxItem item, collection_item contribution_item, TouchDevice touch_device, ImageSource i)
        {
            Image i2 = new Image();
            i2.Source = i;
            ContentControl cursorVisual = new ContentControl()
            {
                Content = i2,
                Style = FindResource("CursorStyle") as Style
            };

            //SurfaceDragDrop.AddTargetChangedHandler(cursorVisual, OnTargetChanged);

            List<InputDevice> devices = new List<InputDevice>();
            devices.Add(touch_device);
            foreach (TouchDevice touch in item.TouchesCapturedWithin)
            {
                if (touch != touch_device)
                {
                    devices.Add(touch);
                }
            }

            SurfaceDragCursor startDragOkay =
                SurfaceDragDrop.BeginDragDrop(
                  this.contributions,         // The SurfaceListBox object that the cursor is dragged out from.
                  item,                       // The SurfaceListBoxItem object that is dragged from the drag source.
                  cursorVisual,               // The visual element of the cursor.
                  contribution_item,          // The data associated with the cursor.
                  devices,                    // The input devices that start dragging the cursor.
                  DragDropEffects.Copy);      // The allowed drag-and-drop effects of the operation.

            return (startDragOkay != null);
        }
    }

    public class collection_item
    {
        public Contribution _contribution;
        public bool is_audio = false;
        public bool is_image = false;
        public bool is_video = false;

        public override string ToString()
        {
            if (is_audio) return "Audio";
            if (is_image) return "Image";
            if (is_video) return "Video";
            return "Media";
        }
    }

    public class loading_progress
    {
        public int current_progress;
        public int total;
        public List<collection_item> loaded_items;
    }

}
