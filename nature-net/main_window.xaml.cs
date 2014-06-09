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
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using System.IO;
using System.Drawing;
using nature_net.user_controls;
using System.Threading;

namespace nature_net
{
    /// <summary>
    /// Interaction logic for main_window.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double debug_var = 10;
        private Canvas debug_canvas = new Canvas();
        private int num_updates = 0;
        iniparser parser;
        Timer update_change_timer;

        public MainWindow()
        {
            try
            {
                parser = new iniparser(configurations.GetAbsoluteConfigFilePath());
                configurations.SetSettingsFromConfig(parser);
                configurations.LoadIconImages();
                window_manager.load_avatars();
                window_manager.refresh_downloaded_contributions();
                if (configurations.use_existing_thumbnails)
                    window_manager.refresh_thumbnails();

                InitializeComponent();

                this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
                this.Unloaded += new RoutedEventHandler(MainWindow_Unloaded);
                this.Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);

				this.workspace.ManipulationStarting += new EventHandler<ManipulationStartingEventArgs>(workspace_ManipulationStarting);
				this.workspace.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(workspace_ManipulationDelta);
                this.workspace.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(workspace_ManipulationCompleted);
				///this.workspace.ManipulationBoundaryFeedback += new EventHandler<ManipulationBoundaryFeedbackEventArgs>(workspace_ManipulationBoundaryFeedback);

                this.workspace.AllowDrop = true;
                SurfaceDragDrop.AddDropHandler(this.workspace, new EventHandler<SurfaceDragDropEventArgs>(item_droped_on_workspace));

                application_panel.PreviewTouchDown += new EventHandler<TouchEventArgs>(application_panel_PreviewTouchDown);
                application_panel.PreviewTouchUp += new EventHandler<TouchEventArgs>(application_panel_PreviewTouchUp);
                //application_panel.PreviewMouseDown += new MouseButtonEventHandler(application_panel_PreviewMouseDown);

                update_change_timer = new Timer(new TimerCallback(update_changes));
                update_change_timer.Change(configurations.update_period_ms, Timeout.Infinite);

                change_update_status(false);
                this.Topmost = configurations.top_most;
            }
            catch (Exception e) { MessageBox.Show("Exception in starting the application:\r\n" + e.StackTrace, "Error"); }
        }

        private void change_update_status(bool red)
        {
            if (label_update.Dispatcher.CheckAccess())
            {
                if (red)
                    label_update.Background = System.Windows.Media.Brushes.Red;
                else
                    label_update.Background = System.Windows.Media.Brushes.Green;
            }
            else
            {
                label_update.Dispatcher.Invoke(new UpdateStatus(change_update_status), new object[] { red });
            }
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            configurations.SaveConfigurations(parser);
        }

        void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            configurations.SaveConfigurations(parser);
        }

        void update_changes(Object stateInfo)
        {
            try
            {
                change_update_status(true);
                //file_manager.retrieve_and_process_media_changes_from_googledrive();
                this.left_tab.load_users();
                this.left_tab.load_design_ideas();
				this.left_tab.load_activities();
                //this.right_tab.load_users();
                //this.right_tab.load_design_ideas();
                change_update_status(false);
                update_change_timer.Change(configurations.update_period_ms, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                log.WriteErrorLog(ex);
            }
        }

        void application_panel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //debug things!
            // if (e.RightButton == MouseButtonState.Pressed)
            // {
                // UIElement[] elements = new UIElement[window_manager.main_canvas.Children.Count];
                // window_manager.main_canvas.Children.CopyTo(elements, 0);
                // foreach (UIElement element in elements)
                // {
                    // try
                    // {
                        // Shape shape = element as Shape;
                        // window_manager.main_canvas.Children.Remove(shape);
                    // }
                    // catch (Exception) { continue; }
                // }
            // }
        }

        void application_panel_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            //TouchPoint tp = e.GetTouchPoint(sender as IInputElement);
            ////Point p = tp.Position;
            ////p.X = p.X - 245;

            //TextBlock tb = new TextBlock();
            //tb.Text = tp.Position.X.ToString() + ", " + tp.Position.Y.ToString();
            //Canvas.SetLeft(tb, 200); Canvas.SetTop(tb, debug_var);
            //tb.FontSize = 16; tb.FontWeight = FontWeights.Bold;
            //workspace.Children.Add(tb);
            //debug_var = debug_var + 30;
            //if (debug_var > 700) { debug_var = 10; debug_canvas.Children.RemoveRange(0, debug_canvas.Children.Count); }
        }

        void application_panel_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            this.left_tab.users_listbox.signup.Background = System.Windows.Media.Brushes.LightGray;
            this.left_tab.design_ideas_listbox.submit_idea.Background = System.Windows.Media.Brushes.LightGray;
            this.left_tab.activities_listbox.submit_idea.Background = System.Windows.Media.Brushes.LightGray;
        }

        void item_droped_on_workspace(object sender, SurfaceDragDropEventArgs e)
        {
            string[] data = (e.Cursor.Data.ToString()).Split(new Char[] { ';' });
            if (data == null) return;
            string context = data[0];
            if (context == "user")
            {
                if (data.Count() < 4) return;
                string username = data[3];
                int user_id = Convert.ToInt32(data[1]);
                log.WriteInteractionLog(14, "user collection opened by dragging; user id: " + user_id, e.Cursor.GetPosition(null).X, e.Cursor.GetPosition(null).Y);
                window_manager.open_collection_window(username, user_id,
                    e.Cursor.GetPosition(sender as IInputElement).X, e.Cursor.GetPosition(sender as IInputElement).Y);
                e.Handled = true;
            }
            if (context == "design idea")
            {
                if (data.Count() < 7) return;
                log.WriteInteractionLog(16, "design idea opened by dragging; contribution id: " + data[1], e.Cursor.GetPosition(null).X, e.Cursor.GetPosition(null).Y);
                window_manager.open_design_idea_window(data, e.Cursor.GetPosition(sender as IInputElement).X,
                    e.Cursor.GetPosition(sender as IInputElement).Y);
                e.Handled = true;
            }
            if (context == "Image" || context == "Audio" || context == "Video" || context == "Media")
            {
                nature_net.user_controls.collection_item ci = (nature_net.user_controls.collection_item)(e.Cursor.Data);
                log.WriteInteractionLog(17, context + " contribution opened by dragging; contribution id: " + ci._contribution.id, e.Cursor.GetPosition(null).X, e.Cursor.GetPosition(null).Y);
                window_manager.open_contribution_window(ci, e.Cursor.GetPosition(sender as IInputElement).X,
                    e.Cursor.GetPosition(sender as IInputElement).Y, context);
                e.Handled = true;
            }
            if (context == "comment")
            {
                if (data.Count() < 7) return;
                //log.WriteInteractionLog(43, context, e.Cursor.GetPosition(null).X, e.Cursor.GetPosition(null).Y);
                window_manager.open_design_idea_window(data, e.Cursor.GetPosition(sender as IInputElement).X,
                    e.Cursor.GetPosition(sender as IInputElement).Y, "Comment");
                e.Handled = true;
            }
            if (context == "activity")
            {
                if (data.Count() < 7) return;
                log.WriteInteractionLog(15, "activity collection opened by dragging; activity id: " + data[1], e.Cursor.GetPosition(null).X, e.Cursor.GetPosition(null).Y);
                window_manager.open_activity_window(data[3], Convert.ToInt32(data[1]), e.Cursor.GetPosition(sender as IInputElement).X,
                    e.Cursor.GetPosition(sender as IInputElement).Y);
                e.Handled = true;
            }
            log.WriteInteractionLog(43, context, e.Cursor.GetPosition(null).X, e.Cursor.GetPosition(null).Y);
        }

        void workspace_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this.workspace;
            e.Mode = ManipulationModes.All;
            FrameworkElement element = (FrameworkElement)e.Source;
            if (element == null) return;

            if (configurations.enable_single_rotation)
            {
                //if (e.Manipulators.Count() > 0)
                //{
                //    TouchDevice td = (TouchDevice)(e.Manipulators.First());
                //    TouchPoint tp = td.GetTouchPoint(element);
                //    e.Pivot = new ManipulationPivot(new Point(tp.Position.X, tp.Position.Y), 48);
                //}
                System.Windows.Point center = new System.Windows.Point(element.ActualWidth / 2, element.ActualHeight / 2);
                center = element.TranslatePoint(center, this.workspace);
                e.Pivot = new ManipulationPivot(center, configurations.manipulation_pivot_radius);
            }
            
            window_manager.UpdateZOrder(element, true);
            try
            {
                user_controls.window_frame f = (user_controls.window_frame)element;
                try { window_content c = (window_content)f.window_content.Content; window_manager.UpdateZOrder(c.GetKeyboardFrame(), true); }
                catch (Exception) { }
                try { signup s = (signup)f.window_content.Content; window_manager.UpdateZOrder(s.GetKeyboardFrame(), true); window_manager.UpdateZOrder(s.GetAvatarFrame(), true); window_manager.UpdateZOrder(s.GetNumpadFrame(), true); }
                catch (Exception) { }
            }
            catch (Exception) { }
        }

        void workspace_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)e.Source;
            if (element == null) return;
            Matrix matrix = ((MatrixTransform)element.RenderTransform).Matrix;
            ManipulationDelta deltaManipulation = e.DeltaManipulation;
            System.Windows.Point center = new System.Windows.Point(element.ActualWidth / 2, element.ActualHeight / 2);
            center = matrix.Transform(center);
            image_frame iframe = null;
            Matrix matrix2 = ((MatrixTransform)element.LayoutTransform).Matrix;
            try
            {
                iframe = (image_frame)element;
                matrix2.ScaleAt(deltaManipulation.Scale.X, deltaManipulation.Scale.Y, e.ManipulationOrigin.X, e.ManipulationOrigin.Y);
            }
            catch (Exception) { }

            matrix.RotateAt(e.DeltaManipulation.Rotation, e.ManipulationOrigin.X, e.ManipulationOrigin.Y);// center.X, center.Y);
            //if (element.PointToScreen(new System.Windows.Point(0, 0)).X > 300 && e.DeltaManipulation.Translation.X > 0)
                matrix.Translate(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);
            element.RenderTransform = new MatrixTransform(matrix);
            if (iframe != null)
            {
                //iframe.Width = iframe.Width * deltaManipulation.Scale.X;
                //iframe.Height = iframe.Height * deltaManipulation.Scale.X;
                iframe.the_item.Width = iframe.the_item.Width * deltaManipulation.Scale.X;
                iframe.the_item.Height = iframe.the_item.Height * deltaManipulation.Scale.X;
                //iframe.whole.Height = iframe.whole.Height * deltaManipulation.Scale.X;

                iframe.title_bar.Height = configurations.frame_title_bar_height;
                iframe.close.Height = 33;
                iframe.close.Width = 33;
                iframe.UpdateLayout();
            }
            num_updates++;
            if (num_updates > configurations.max_num_content_update)
            {
                num_updates = 0;
                try { user_controls.window_frame w1 = (user_controls.window_frame)element; w1.UpdateContents(); }
                catch (Exception) { }
                try { user_controls.image_frame w2 = (user_controls.image_frame)element; w2.UpdateContents(); }
                catch (Exception) { }
            }
        }

        void workspace_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)e.Source;
            if (element == null) return;
            try
            {
                user_controls.window_frame w1 = (user_controls.window_frame)element; w1.UpdateContents();
                log.WriteInteractionLog(18, "frame title: " + w1.title.Text, (TouchDevice)e.Device);
            }
            catch (Exception) { }
            try
            {
                user_controls.image_frame w2 = (user_controls.image_frame)element; w2.UpdateContents();
                log.WriteInteractionLog(21, "contribution id: " + w2.item._contribution.id, (TouchDevice)e.Device);
            }
            catch (Exception) { }
        }

        void load_locations_on_map(int screen_x)
        {
            BitmapImage background = (BitmapImage)configurations.img_background_pic;
            System.Windows.Point pw = this.workspace.PointToScreen(new System.Windows.Point(0, 0));
            //double max_x = this.workspace.PointFromScreen(new System.Windows.Point(this.Width - pw.X, 0)).X;
            double max_x = screen_x - (screen_x * configurations.tab_width_percentage / 100) - workspace.Margin.Left;
            int i = 1;
            foreach (System.Windows.Point p in configurations.locations)
            {
                double x = (p.X / background.PixelWidth) * max_x;
                double y = (p.Y / background.PixelHeight) * this.workspace.ActualHeight;

                Ellipse e = new Ellipse();
                e.Fill = configurations.location_dot_color;
                e.Stroke = configurations.location_dot_outline_color;
                e.Width = configurations.location_dot_diameter;
                e.Height = configurations.location_dot_diameter;
                Canvas.SetLeft(e, x - (configurations.location_dot_diameter / 2));
                Canvas.SetTop(e, y - (configurations.location_dot_diameter / 2));
                e.Tag = i;
                e.PreviewTouchDown += new EventHandler<TouchEventArgs>(reddot_PreviewTouchDown);
                TextBlock tb = new TextBlock();
                tb.Text = i.ToString(); tb.FontWeight = FontWeights.Bold; tb.FontSize = 24; tb.Foreground = configurations.location_dot_font_color;
                tb.Tag = i;
                tb.PreviewTouchDown += new EventHandler<TouchEventArgs>(tb_PreviewTouchDown);
                if (i > 9)
                {
                    Canvas.SetLeft(tb, x - 12);
                    Canvas.SetTop(tb, y - 15);
                }
                else
                {
                    Canvas.SetLeft(tb, x - 8);
                    Canvas.SetTop(tb, y - 15);
                }
                workspace.Children.Add(e);
                workspace.Children.Add(tb);
                i++;
            }
            Canvas.SetLeft(this.label_update, max_x - this.label_update.Width);
            Canvas.SetTop(this.label_update, 0);
        }

        void tb_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            TextBlock dot = (TextBlock)sender;
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var loc = from l in db.Locations
                      where l.id == (int)dot.Tag
                      select l;
            Location location = loc.Single<Location>();
            log.WriteInteractionLog(11, "Location: " + location.id.ToString(), e.TouchDevice);
            window_manager.open_location_collection_window(location.name, location.id, Canvas.GetLeft(dot), Canvas.GetTop(dot));
        }

        void reddot_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            Ellipse dot = (Ellipse)sender;
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var loc = from l in db.Locations
                      where l.id == (int)dot.Tag
                      select l;
            Location location = loc.Single<Location>();
            log.WriteInteractionLog(11, "Location: " + location.id.ToString(), e.TouchDevice);
            window_manager.open_location_collection_window(location.name, location.id, Canvas.GetLeft(dot) + (dot.Width / 2), Canvas.GetTop(dot) + (dot.Height / 2));
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var b = new ImageBrush();
            b.ImageSource = configurations.img_background_pic;
            this.workspace.Background = b;
            int screen_x = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

            window_manager.main_canvas = this.workspace;
            window_manager.left_tab = left_tab;
            //window_manager.right_tab = right_tab;

            this.left_tab.load_control(true, 0, screen_x);
            //this.right_tab.load_control(false, 2);
            //this.right_tab.load_control(true, 2);
            
            debug_canvas.Width = window_manager.main_canvas.ActualWidth;
            debug_canvas.Height = window_manager.main_canvas.ActualHeight;
            window_manager.main_canvas.Children.Add(debug_canvas);

            this.load_locations_on_map(screen_x);
        }
    }
    public delegate void UpdateStatus(bool status);
}
            