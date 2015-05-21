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
                if (configurations.response_to_mouse_clicks)
                    MouseTouchDevice.RegisterEvents(this);

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
            //configurations.SaveConfigurations(parser);
        }

        void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            //configurations.SaveConfigurations(parser);
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
            string context = data[0].ToLower();
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
            if (context == "design idea type")
            {
                //if (data.Count() < 7) return;
                item_generic_v2 item = (item_generic_v2)e.Cursor.Data;
                log.WriteInteractionLog(16, "design idea type opened by dragging; activity id: " + item.Tag,
                    e.Cursor.GetPosition(null).X, e.Cursor.GetPosition(null).Y);
                //window_manager.open_design_idea_window(data, e.Cursor.GetPosition(sender as IInputElement).X, e.Cursor.GetPosition(sender as IInputElement).Y);
                //window_manager.open_design_idea_window((item_generic_v2)e.Cursor.Data, e.Cursor.GetPosition(sender as IInputElement).X, e.Cursor.GetPosition(sender as IInputElement).Y);
                window_manager.open_activity_window(item.title.Text, Convert.ToInt32(item.Tag), e.Cursor.GetPosition(sender as IInputElement).X, e.Cursor.GetPosition(sender as IInputElement).Y);
                e.Handled = true;
            }
            if (context == "design idea" || context == "birdcounting")
            {
                item_generic_v2 item = configurations.get_item_visuals((collection_item)e.Cursor.Data);
                log.WriteInteractionLog(16, "design idea opened by dragging; contribution id: " + item.Tag,
                    e.Cursor.GetPosition(null).X, e.Cursor.GetPosition(null).Y);
                //window_manager.open_design_idea_window(data, e.Cursor.GetPosition(sender as IInputElement).X, e.Cursor.GetPosition(sender as IInputElement).Y);
                window_manager.open_design_idea_window(item, e.Cursor.GetPosition(sender as IInputElement).X, e.Cursor.GetPosition(sender as IInputElement).Y);
                e.Handled = true;
            }
            if (context == "image" || context == "audio" || context == "video" || context == "media")
            {
                nature_net.user_controls.collection_item ci = (nature_net.user_controls.collection_item)(e.Cursor.Data);
                log.WriteInteractionLog(17, context + " contribution opened by dragging; contribution id: " + ci._contribution.id, e.Cursor.GetPosition(null).X, e.Cursor.GetPosition(null).Y);
                window_manager.open_contribution_window(ci, e.Cursor.GetPosition(sender as IInputElement).X,
                    e.Cursor.GetPosition(sender as IInputElement).Y);
                e.Handled = true;
            }
            if (context == "comment")
            {
                //if (data.Count() < 7) return;
                ////log.WriteInteractionLog(43, context, e.Cursor.GetPosition(null).X, e.Cursor.GetPosition(null).Y);
                //window_manager.open_design_idea_window(data, e.Cursor.GetPosition(sender as IInputElement).X,
                //    e.Cursor.GetPosition(sender as IInputElement).Y, "Comment");
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
            try
            {
                image_frame iframe = (image_frame)element;
                if (iframe.the_image == null) return;
            }
            catch (Exception) { }

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
            window_frame wframe = null;
            help_window hwin = null;
            Matrix matrix2 = ((MatrixTransform)element.LayoutTransform).Matrix;
            try { wframe = (window_frame)element; wframe.postpone_killer_timer(true); }
            catch (Exception) { }
            try { iframe = (image_frame)element; iframe.postpone_killer_timer(true); }
            catch (Exception) { }
            try { hwin = (help_window)element; hwin.postpone_killer_timer(true); }
            catch (Exception) { }

            double x = 0;
            double y = 0;
            
            System.Windows.Point ps1 = element.PointToScreen(new System.Windows.Point(0, 0));
            System.Windows.Point ps2 = element.PointToScreen(new System.Windows.Point(element.Width, 50));
            if (iframe != null && iframe.the_item != null)
            {
                UIElement o = (UIElement)e.ManipulationContainer;
                System.Windows.Point new_point = o.TranslatePoint(e.ManipulationOrigin, iframe.the_item);
                double old_width = iframe.the_item.Width;
                double old_height = iframe.the_item.Height; // height is null for the video

                iframe.the_item.Width = iframe.the_item.Width * deltaManipulation.Scale.X;
                iframe.the_item.Height = iframe.the_item.Height * deltaManipulation.Scale.X;

                x = (new_point.X / old_width) * (iframe.the_item.Width - old_width);
                y = (new_point.Y / old_height) * (iframe.the_item.Height - old_height);

                ps1 = iframe.the_item.PointToScreen(new System.Windows.Point(0, 0));
                ps2 = iframe.the_item.PointToScreen(new System.Windows.Point(iframe.the_item.Width, iframe.the_item.Height));

                iframe.title_bar.Height = configurations.frame_title_bar_height;
                iframe.close.Height = 33;
                iframe.close.Width = 33;
                iframe.UpdateLayout();
            }
            if (hwin != null && hwin.the_item != null)
            {
                UIElement o = (UIElement)e.ManipulationContainer;
                System.Windows.Point new_point = o.TranslatePoint(e.ManipulationOrigin, hwin.the_item);
                double old_width = hwin.the_item.Width;
                double old_height = hwin.the_item.Height; // height is null for the video

                hwin.the_item.Width = hwin.the_item.Width * deltaManipulation.Scale.X;
                hwin.the_item.Height = hwin.the_item.Height * deltaManipulation.Scale.X;

                x = (new_point.X / old_width) * (hwin.the_item.Width - old_width);
                y = (new_point.Y / old_height) * (hwin.the_item.Height - old_height);

                ps1 = hwin.the_item.PointToScreen(new System.Windows.Point(0, 0));
                ps2 = hwin.the_item.PointToScreen(new System.Windows.Point(hwin.the_item.Width, hwin.the_item.Height));

                hwin.title_bar.Height = configurations.frame_title_bar_height;
                hwin.close.Height = 33;
                hwin.close.Width = 33;
                hwin.UpdateLayout();
            }

            matrix.RotateAt(e.DeltaManipulation.Rotation, e.ManipulationOrigin.X, e.ManipulationOrigin.Y);// center.X, center.Y);
            //if (element.PointToScreen(new System.Windows.Point(0, 0)).X > 300 && e.DeltaManipulation.Translation.X > 0)

            if (ps2.X > ps1.X && ps2.Y > ps1.Y)
                matrix.Translate(e.DeltaManipulation.Translation.X - x, e.DeltaManipulation.Translation.Y - y);
            else if (ps2.X < ps1.X && ps2.Y > ps1.Y)
                matrix.Translate(e.DeltaManipulation.Translation.X + x, e.DeltaManipulation.Translation.Y - y);
            else if (ps2.X > ps1.X && ps2.Y < ps1.Y)
                matrix.Translate(e.DeltaManipulation.Translation.X - x, e.DeltaManipulation.Translation.Y + y);
            else if (ps2.X < ps1.X && ps2.Y < ps1.Y)
                matrix.Translate(e.DeltaManipulation.Translation.X + x, e.DeltaManipulation.Translation.Y + y);

            element.RenderTransform = new MatrixTransform(matrix);
                

            num_updates++;
            if (num_updates > configurations.max_num_content_update)
            {
                num_updates = 0;
                try { user_controls.window_frame w1 = (user_controls.window_frame)element; w1.UpdateContents(); }
                catch (Exception) { }
                try { user_controls.image_frame w2 = (user_controls.image_frame)element; w2.UpdateContents(); }
                catch (Exception) { }
                window_manager.UpdateZOrder(element, true);
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

            if (configurations.application_name != "")
            {
                AddAppNameToScreen(80, 30);
                AddAppNameToScreen(max_x - 150 , 30);
                AddAppNameToScreen(80, this.workspace.ActualHeight - 80);
                AddAppNameToScreen(max_x - 150, this.workspace.ActualHeight - 80);
            }
        }

        void tb_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            TextBlock dot = (TextBlock)sender;
            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
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
            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
            var loc = from l in db.Locations
                      where l.id == (int)dot.Tag
                      select l;
            Location location = loc.Single<Location>();
            log.WriteInteractionLog(11, "Location: " + location.id.ToString(), e.TouchDevice);
            window_manager.open_location_collection_window(location.name, location.id, Canvas.GetLeft(dot) + (dot.Width / 2), Canvas.GetTop(dot) + (dot.Height / 2));
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RenderOptions.SetBitmapScalingMode(this.workspace, configurations.scaling_mode);
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

        void AddAppNameToScreen(double x, double y)
        {
            System.Windows.Shapes.Rectangle app_name_rect = new System.Windows.Shapes.Rectangle();
            TextBlock app_name_text = new TextBlock(); app_name_text.Text = configurations.application_name;
            app_name_rect.Width = 100; app_name_rect.Height = 50;
            app_name_text.Width = 100; app_name_text.Height = 50; app_name_text.TextAlignment = TextAlignment.Center;
            app_name_text.FontFamily = new System.Windows.Media.FontFamily("Calibri"); app_name_text.FontSize = 18;
            app_name_rect.Fill = System.Windows.Media.Brushes.Khaki;
            Canvas.SetLeft(app_name_rect, x);
            Canvas.SetTop(app_name_rect, y);
            Canvas.SetLeft(app_name_text, x);
            Canvas.SetTop(app_name_text, y + 15);
            workspace.Children.Add(app_name_rect);
            workspace.Children.Add(app_name_text);
        }
    }
    public delegate void UpdateStatus(bool status);
}
            