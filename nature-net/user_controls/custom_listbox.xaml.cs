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
using System.ComponentModel;
using System.Windows.Threading;
using Microsoft.Surface.Presentation.Controls;

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for custom_listbox.xaml
    /// </summary>
    public partial class custom_listbox : UserControl
    {
        public bool list_users = false;
        public bool list_design_ideas = false;
        public bool list_comments = false;
        public bool list_activities = false;

        public UserControl parent;

        private readonly BackgroundWorker worker = new BackgroundWorker();

        private List<System.Windows.Input.TouchPoint> touch_points = new List<System.Windows.Input.TouchPoint>();
        private int consecutive_drag_points = 0;
        ListBoxItem last_dragged_element = null;
        double last_scroll_offset = 0;
        private Point drag_direction1 = new Point(1, 0);
        private Point drag_direction2 = new Point(-1, 0);

        private Dictionary<int, bool> is_tap = new Dictionary<int, bool>();

        public item_generic initial_comment = null;
        public Thickness items_margins = new Thickness(0);

        //public Window _topWindow;
        //Canvas _adornerLayer;
        //Dictionary<int, Point> _initialPositions = new Dictionary<int, Point>();
        //Dictionary<int, Point> _delta = new Dictionary<int, Point>();
        //Dictionary<int, Point> _scrollTarget = new Dictionary<int, Point>();

        public custom_listbox()
        {
            InitializeComponent();
            if (!configurations.use_avatar_drag)
            {
                this._list.PreviewTouchDown += new EventHandler<TouchEventArgs>(_list_PreviewTouchDown);
                this._list.PreviewTouchMove += new EventHandler<TouchEventArgs>(_list_PreviewTouchMove);
                this._list.PreviewTouchUp += new EventHandler<TouchEventArgs>(_list_PreviewTouchUp);
                //this._list.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(_list_PreviewTouchDown);
                //this._list.PreviewMouseMove += new MouseEventHandler(_list_PreviewTouchMove);
                //this._list.PreviewMouseUp += new MouseButtonEventHandler(_list_PreviewTouchUp);
            }

            if (configurations.high_contrast)
            {
                this._list.Background = Brushes.DarkGreen;
                //this.Background = Brushes.Black;
                //this.Background = Brushes.Green;
            }
            else
            {
                this._list.Background = Brushes.White;
            }
        }
		
        private void avatar_drag(object sender, TouchEventArgs e)
        {
            FrameworkElement findSource = e.OriginalSource as FrameworkElement;
            ListBoxItem element = null;
            while (element == null && findSource != null)
                if ((element = findSource as ListBoxItem) == null)
                    findSource = VisualTreeHelper.GetParent(findSource) as FrameworkElement;

            avatar_drag(element, e.TouchDevice);
            e.Handled = true;
        }

        private void avatar_drag(ListBoxItem element, TouchDevice touch_device)
        {
            if (element == null)
                return;
            
            item_generic i;
			try { i = (item_generic)element.DataContext;}
			catch(Exception) {return; }

            if (i.Tag == null)
                return;

            if (list_users)
            {
                string username_id = "user;" + ((int)i.Tag).ToString() + ";" + i.avatar.Source.ToString() + ";" + (string)i.username.Text;
                start_drag(element, username_id, touch_device, i.avatar.Source.Clone());
            }
            if (list_design_ideas)
            {
                string idea = "design idea;" + ((int)i.Tag).ToString() + ";" + i.avatar.Source.ToString() + ";" +
                    (string)i.username.Text + ";" + i.user_desc.Content + ";" + i.desc.Content + ";" +
                    i.content.Text;
                start_drag(element, idea, touch_device, i.avatar.Source.Clone());
            }
            if (list_comments)
            {
                string comment = "comment;" + ((int)i.Tag).ToString() + ";" + i.avatar.Source.ToString() + ";" +
                    (string)i.username.Text + ";" + i.user_desc.Content + ";" + i.desc.Content + ";" +
                    i.content.Text;
                start_drag(element, comment, touch_device, i.avatar.Source.Clone());
            }
            if (list_activities)
            {
                string avatar = "";
                if (i.avatar.Source != null)
                    avatar = i.avatar.Source.ToString();
                string activity = "activity;" + ((int)i.Tag).ToString() + ";" + avatar + ";" +
                    (string)i.username.Text + ";" + i.user_desc.Content + ";" + i.desc.Content + ";" +
                    i.content.Text;
                ImageSource img = null;
                if (i.avatar.Source != null)
                    img = i.avatar.Source.Clone();
                start_drag(element, activity, touch_device, img);
            }
        }

        private void _list_PreviewTouchMove(object sender, TouchEventArgs e)
        {
            FrameworkElement findSource = e.OriginalSource as FrameworkElement;
            ListBoxItem element = null;
            while (element == null && findSource != null)
                if ((element = findSource as ListBoxItem) == null)
                    findSource = VisualTreeHelper.GetParent(findSource) as FrameworkElement;

            if (element != null)
                last_dragged_element = element;
            this.touch_points.Add(e.GetTouchPoint(this._list as IInputElement));
            if (touch_points.Count < configurations.min_touch_points) return;
            double dy = touch_points[touch_points.Count - 1].Position.Y - touch_points[touch_points.Count - 2].Position.Y;
            double dx = touch_points[touch_points.Count - 1].Position.X - touch_points[touch_points.Count - 2].Position.X;
            double size_n = Math.Sqrt(dx * dx + dy * dy);
            dx = dx / size_n; dy = dy / size_n;
            if (dx == double.NaN || dy == double.NaN) return;
            double theta1 = Math.Acos(dx * drag_direction1.X + dy * drag_direction1.Y);
            double theta2 = Math.Acos(dx * drag_direction2.X + dy * drag_direction2.Y);
            //convert to degree
            theta1 = theta1 * 180 / Math.PI;
            theta2 = theta2 * 180 / Math.PI;
            double theta = (theta1 < theta2) ? theta1 : theta2;
            if (theta < configurations.drag_collection_theta)
            {
                if (consecutive_drag_points < configurations.max_consecutive_drag_points)
                {
                    consecutive_drag_points++;
                }
                else
                {
                    if (is_tap.ContainsKey(e.TouchDevice.Id)) is_tap[e.TouchDevice.Id] = false;
                    if (element == null) element = last_dragged_element;
                    if (element != null)
                    {
                        avatar_drag(element, e.TouchDevice);
                        touch_points.Clear();
                        consecutive_drag_points = 0;
                        e.Handled = true;
                        return;
                    }
                }
            }
            ScrollViewer scroll = configurations.GetDescendantByType(this._list, typeof(ScrollViewer)) as ScrollViewer;
            double dv = touch_points[touch_points.Count - 1].Position.Y - touch_points[0].Position.Y;
            dx = touch_points[touch_points.Count - 1].Position.X - touch_points[0].Position.X;
            //double new_offset = scroll.HorizontalOffset + (-1 * configurations.scroll_scale_factor * dx);
            if (dv > configurations.tap_error || dx > configurations.tap_error)
                if (is_tap.ContainsKey(e.TouchDevice.Id)) is_tap[e.TouchDevice.Id] = false;
            double new_offset = last_scroll_offset + (-1 * dv);
            try { scroll.ScrollToVerticalOffset(new_offset); }
            catch (Exception) { }
        }

        private void _list_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (!is_tap.ContainsKey(e.TouchDevice.Id))
                this.is_tap.Add(e.TouchDevice.Id, true);
            ScrollViewer scroll = configurations.GetDescendantByType(this._list, typeof(ScrollViewer)) as ScrollViewer;
            last_scroll_offset = scroll.VerticalOffset;
            //scroll.Elasticity = new Vector(0.0, 0.4);
            bool r = e.TouchDevice.Capture(this._list as IInputElement, CaptureMode.SubTree);
            e.Handled = true;
        }

        private void _list_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            double dv = 0;
            if (touch_points.Count > 0)
                dv = e.GetTouchPoint(this._list).Position.Y - touch_points[0].Position.Y;
            bool can_scroll = false;
            if (!is_tap.ContainsKey(e.TouchDevice.Id))
                can_scroll = true;
            else
            {
                if (is_tap[e.TouchDevice.Id])
                {
                    FrameworkElement findSource = e.OriginalSource as FrameworkElement;
                    ListBoxItem element = null;
                    while (element == null && findSource != null)
                        if ((element = findSource as ListBoxItem) == null)
                            findSource = VisualTreeHelper.GetParent(findSource) as FrameworkElement;
                    if (element != null)
                    {
                        _list.SelectedItem = element;
						try 
						{
							_list_SelectionChanged((item_generic)element.DataContext);
						}
                        catch (Exception e2) { log.WriteErrorLog(e2); }
                    }
                }
                else
                    can_scroll = true;
            }
            if (can_scroll)
            {
                ScrollViewer scroll = configurations.GetDescendantByType(this._list, typeof(ScrollViewer)) as ScrollViewer;
                //double dv = e.GetTouchPoint(this.contributions).Position.X - touch_points[touch_points.Count - 1].Position.X;
                try
                {
                    //scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset + (-2 * dv));
                    scroll.ScrollToVerticalOffset(last_scroll_offset + (-1 * dv));
                }
                catch (Exception) { }
                last_scroll_offset = scroll.VerticalOffset;
            }
            is_tap.Remove(e.TouchDevice.Id);
            this.touch_points.Clear();
            consecutive_drag_points = 0;
            UIElement element2 = sender as UIElement;
            element2.ReleaseTouchCapture(e.TouchDevice);
        }

        private void _list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            item_generic item = (item_generic)e.AddedItems[0];
            _list_SelectionChanged(item);
        }

        private void _list_SelectionChanged(item_generic item)
        {
            if (list_design_ideas)
            {
                string[] idea_item = ("design idea;" + item.ToString()).Split(new Char[] { ';' });
                window_manager.open_design_idea_window(idea_item, 0, item.PointToScreen(new Point(0, 0)).Y);
                _list.SelectedIndex = -1;
                return;
            }
            if (list_users)
            {
                window_manager.open_collection_window((string)item.username.Text, (int)item.Tag, 0, item.PointToScreen(new Point(0, 0)).Y);
                ///////window_manager.open_collections_balloon(item.PointToScreen(new Point(0, 0)).Y, (string)item.username.Content);
                _list.SelectedIndex = -1;
                return;
            }
            if (list_comments)
            {
                string[] idea_item = ("comment;" + item.ToString()).Split(new Char[] { ';' });
                //window_manager.open_design_idea_window(idea_item,
                //    configurations.RANDOM((int)(window_manager.main_canvas.ActualWidth - item.ActualWidth) - 20,
                //    (int)(window_manager.main_canvas.ActualWidth - item.ActualWidth)),
                //    item.PointToScreen(new Point(0, 0)).Y);
                _list.SelectedIndex = -1;
                return;
            }
            if (list_activities)
            {
                string[] activity_item = ("activity;" + item.ToString()).Split(new Char[] { ';' });
                window_manager.open_activity_window(activity_item[3], Convert.ToInt32(activity_item[1]), 0, item.PointToScreen(new Point(0, 0)).Y);
                _list.SelectedIndex = -1;
                return;
            }
            _list.SelectedIndex = -1;
        }

        private bool start_drag(ListBoxItem item, string username_id, TouchDevice touch_device, ImageSource i)
        {
            Image i2 = new Image();
            i2.Source = i; i2.Stretch = Stretch.Uniform;
            item_generic i3 = (item_generic)item.Content;
            ContentControl cursorVisual = new ContentControl()
            {
                //Content = i2,
                Content = i3.get_clone(),
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

            FrameworkElement element = item;
            if (configurations.use_avatar_drag)
                element = ((item_generic)(item.DataContext)).avatar;

            SurfaceDragCursor startDragOkay =
                SurfaceDragDrop.BeginDragDrop(
                  this._list,                 // The SurfaceListBox object that the cursor is dragged out from.
                  element,                    // The SurfaceListBoxItem object that is dragged from the drag source.
                  cursorVisual,               // The visual element of the cursor.
                  username_id,                // The data associated with the cursor.
                  devices,                    // The input devices that start dragging the cursor.
                  DragDropEffects.Copy);      // The allowed drag-and-drop effects of the operation.

            return (startDragOkay != null);
        }

        // Custom Behaviors (users or design ideas)

        // for design ideas
        public void list_all_design_ideas()
        {
            this.list_design_ideas = true;
            worker.DoWork += new DoWorkEventHandler(get_all_design_ideas);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_design_ideas);
            if (!worker.IsBusy)
                worker.RunWorkerAsync(null);
        }
        public void get_all_design_ideas(object arg, DoWorkEventArgs e)
        {
            e.Result = (object)(new List<design_idea_item>());
            try
            {
                naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
                var r = from d in db.Design_Ideas
                        orderby d.date descending
                        select d;
                if (r == null)
                {
                    e.Result = (object)(new List<design_idea_item>());
                    return;
                }
                List<design_idea_item> ideas = new List<design_idea_item>();
                foreach (Design_Idea d in r)
                {
                    design_idea_item i = new design_idea_item();
                    ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + d.avatar));
                    src.Freeze();
                    i.img = src;
                    i.design_idea = d;
                    ideas.Add(i);
                }
                e.Result = (object)ideas;
            }
            catch (Exception ex)
            {
                log.WriteErrorLog(ex);
            }
        }
        public void display_all_design_ideas(object di, RunWorkerCompletedEventArgs e)
        {
            this._list.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new System.Action(() =>
                {
                    this._list.Items.Clear();
                    List<design_idea_item> ideas = (List<design_idea_item>)e.Result;
                    foreach (design_idea_item idea in ideas)
                    {
                        item_generic i = new item_generic();
                        i.username.Text = idea.design_idea.name;
                        //i.user_desc.Visibility = System.Windows.Visibility.Collapsed;
                        i.user_desc.Content = configurations.GetDate_Formatted(idea.design_idea.date);
                        i.desc.Content = "Contributed:";
                        i.content.Text = idea.design_idea.note;
                        if (configurations.use_avatar_drag) i.set_touchevent(this.avatar_drag);
                        if (parent != null) i.Width = parent.Width - 10;
                        i.avatar.Source = idea.img;
                        i.Tag = idea.design_idea.id;
                        i.Margin = items_margins;
                        this._list.Items.Add(i);
                    }
                    this._list.Items.Refresh();
                    this._list.UpdateLayout();
                }));
        }

        // for users
        public void list_all_users()
        {
            this.list_users = true;
            worker.DoWork += new DoWorkEventHandler(get_all_users);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_users);
            if (!worker.IsBusy)
                worker.RunWorkerAsync(null);
        }
        public void get_all_users(object arg, DoWorkEventArgs e)
        {
            e.Result = (object)(new List<user_item>());
            try
            {
                naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
                var r = from u in db.Users
                        where u.id != 0
                        orderby u.name
                        select u;
                if (r == null)
                {
                    e.Result = (object)(new List<user_item>());
                    return;
                }
                List<user_item> users = new List<user_item>();
                foreach (User u in r)
                {
                    user_item i = new user_item();
                    ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + u.avatar));
                    src.Freeze();
                    i.img = src;
                    i.user = u;
                    users.Add(i);
                }
                e.Result = (object)users;
            }
            catch (Exception ex)
            {
                log.WriteErrorLog(ex);
            }
        }
        public void display_all_users(object us, RunWorkerCompletedEventArgs e)
        {
            this._list.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new System.Action(() =>
                {
                    this._list.Items.Clear();
                    List<user_item> users = (List<user_item>)e.Result;
                    foreach (user_item u in users)
                    {
                        item_generic i = new item_generic();
                        i.username.Text = u.user.name;
                        //i.user_desc.Content = u.email;
                        i.user_desc.Visibility = System.Windows.Visibility.Collapsed;
                        i.desc.Visibility = System.Windows.Visibility.Collapsed;
                        i.content.Visibility = System.Windows.Visibility.Collapsed;
                        i.avatar.Source = u.img;
                        if (parent != null) i.Width = parent.Width - 10;
                        i.Tag = u.user.id;
                        if (configurations.use_avatar_drag) i.set_touchevent(this.avatar_drag);
                        i.Margin = items_margins;
                        this._list.Items.Add(i);
                    }
                    this._list.Items.Refresh();
                    this._list.UpdateLayout();
                }));
        }

        // for comments
        public void list_all_comments(comment_item item)
        {
            this.list_comments = true;
            worker.DoWork += new DoWorkEventHandler(get_all_comments);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_comments);
            if (!worker.IsBusy)
                worker.RunWorkerAsync(item);
        }
        public void get_all_comments(object arg, DoWorkEventArgs e)
        {
            if (e.Argument == null) return;
            e.Result = (object)(new List<Feedback>());
            try
            {
                comment_item item = (comment_item)e.Argument;
                naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
                var r = from c in db.Feedbacks
                        where (c.Feedback_Type.name == "Comment") && (c.object_type == item._object_type.ToString())
                        && (c.object_id == item._object_id)
                        orderby c.date descending
                        select c;
                if (r != null)
                {
                    List<Feedback> comments = r.ToList<Feedback>();
                    e.Result = (object)comments;
                }
                else
                {
                    e.Result = (object)(new List<Feedback>());
                }
            }
            catch (Exception ex)
            {
                log.WriteErrorLog(ex);
            }
        }
        public void display_all_comments(object c_obj, RunWorkerCompletedEventArgs e)
        {
            this._list.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
               new System.Action(() =>
               {
                   this._list.Items.Clear();
                   if (initial_comment != null)
                       this._list.Items.Add(initial_comment);

                   List<Feedback> comments = (List<Feedback>)e.Result;
                   foreach (Feedback c in comments)
                   {
                       item_generic i = new item_generic();
                       i.username.Text = c.User.name;
                       i.user_desc.Content = configurations.GetDate_Formatted(c.date);
                       //i.user_desc.Visibility = System.Windows.Visibility.Collapsed;
                       i.desc.Content = "Commented:";
                       i.content.Text = c.note;
                       if (parent != null) i.Width = parent.Width - 10;
                       i.avatar.Source = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + c.User.avatar));
                       i.Tag = c.id;
                       if (configurations.use_avatar_drag) i.set_touchevent(this.avatar_drag);
                       i.Margin = items_margins;
                       this._list.Items.Add(i);
                   }
                   this._list.Items.Refresh();
                   this._list.Padding = new Thickness(0);
                   this._list.UpdateLayout();
               }));
        }

        // for activities
        public void list_all_activities()
        {
            this.list_activities = true;
            worker.DoWork += new DoWorkEventHandler(get_all_activities);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_activities);
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
        }
        public void get_all_activities(object arg, DoWorkEventArgs e)
        {
            e.Result = (object)(new List<Activity>());
            try
            {
                naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
                var r = from a in db.Activities
                        where (a.name != "Free Observation") && (a.name != "Design Idea")
                        select a;
                if (r != null)
                {
                    List<Activity> activities = r.ToList<Activity>();
                    e.Result = (object)activities;
                }
                else
                {
                    e.Result = (object)(new List<Activity>());
                }
            }
            catch (Exception ex)
            {
                log.WriteErrorLog(ex);
            }
        }
        public void display_all_activities(object arg, RunWorkerCompletedEventArgs e)
        {
            this._list.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
               new System.Action(() =>
               {
                   this._list.Items.Clear();
                   List<Activity> activities = (List<Activity>)e.Result;
                   foreach (Activity a in activities)
                   {
                       item_generic i = new item_generic();
                       i.username.Text = a.name;
                       i.user_desc.Content = configurations.GetDate_Formatted(a.creation_date);
                       //i.user_desc.Visibility = System.Windows.Visibility.Collapsed;
                       i.desc.Content = "Description:";
                       i.content.Text = a.description;
                       if (parent != null) { i.Width = parent.ActualWidth - 75; }
                       i.username.FontWeight = FontWeights.Bold;
                       i.username.Width = i.username.Width + 30;
                       i.BorderBrush = Brushes.Gray; i.BorderThickness = new Thickness(0, 0, 0, 2);
                       i.avatar.Visibility = System.Windows.Visibility.Collapsed;
                       //i.avatar.Source = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + c.User.avatar));
                       i.Tag = a.id;
                       if (configurations.use_avatar_drag) i.set_touchevent(this.avatar_drag);
                       i.Margin = items_margins;
                       this._list.Items.Add(i);
                   }
                   this._list.Items.Refresh();
                   this._list.UpdateLayout();
               }));
        }
    }
}
