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
    /// Interaction logic for custom_listbox_v2.xaml
    /// </summary>
    public partial class custom_listbox_v2 : UserControl
    {
        public string drag_prefix;
        public bool is_horizontal;
        public list_populator populator;
        public ItemSelected item_selected;
        public ListBox _list;
        public bool selectable = false;
        public bool collection_list = false;
        public bool comment_list = false;

        private double last_scroll_offset = 0;
        private Point drag_direction1;
        private Point drag_direction2;
        private Dictionary<int, touch_info> touch_points = new Dictionary<int, touch_info>();
        private int last_selected_index = -1;

        private double debug_var = 10;
        private Canvas debug_canvas;
        private List<DependencyObject> hitResultsList = new List<DependencyObject>();

        public custom_listbox_v2()
        {
            InitializeComponent();
        }

        public void initialize(bool horizontal, string prefix, ItemSelected i)
        {
            this.drag_prefix = prefix;
            this.is_horizontal = horizontal;
            this.item_selected = i;
            if (is_horizontal)
            {
                drag_direction1 = new Point(0, 1);
                drag_direction2 = new Point(0, -1);
                _list = this._list_horizontal;
                _list_vertical.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                drag_direction1 = new Point(1, 0);
                drag_direction2 = new Point(-1, 0);
                _list = _list_vertical;
                _list_horizontal.Visibility = System.Windows.Visibility.Collapsed;
            }
            populator = new list_populator();
            populator.avatar_drag = this.avatar_drag;
            populator._list = this._list;

            if (!configurations.use_avatar_drag)
            {
                this._list.PreviewTouchDown += new EventHandler<TouchEventArgs>(_list_PreviewTouchDown);
                this._list.PreviewTouchMove += new EventHandler<TouchEventArgs>(_list_PreviewTouchMove);
                this._list.PreviewTouchUp += new EventHandler<TouchEventArgs>(_list_PreviewTouchUp);

                //this._list.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(_list_PreviewMouseLeftButtonDown);
            }

            if (configurations.high_contrast)
                this._list.Background = Brushes.DarkGreen;
            else
                this._list.Background = Brushes.White;

            //if (configurations.response_to_mouse_clicks)
                this._list.SelectionChanged += new SelectionChangedEventHandler(_list_SelectionChanged);
        }

        void _list_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition((UIElement)sender);
            hitResultsList.Clear();

            VisualTreeHelper.HitTest(this._list, null,
                new HitTestResultCallback(HitTestResult_Tap_v2),
                new PointHitTestParameters(pt));

            if (hitResultsList.Count > 0)
            {
                item_generic selected = null;
                try { selected = (item_generic)hitResultsList[hitResultsList.Count - 1]; }
                catch (Exception)
                {
                    //try { selected = (Image)hitResultsList[0]; }
                    //catch (Exception) { return; }
                }
                //start_drag(selected, e.Device);
                e.Handled = true;
                return;
            }
        }

        UIElement HitTestOverItem(TouchEventArgs e, bool drag, bool v2, bool v1, bool other)
        {
            TouchPoint pt = e.GetTouchPoint(this._list as IInputElement);
            hitResultsList.Clear();

            if (v2)
                VisualTreeHelper.HitTest(this._list, null,
                    new HitTestResultCallback(HitTestResult_Tap_v2),
                    new PointHitTestParameters(new Point(pt.Position.X, pt.Position.Y)));
            if (v1)
                VisualTreeHelper.HitTest(this._list, null,
                    new HitTestResultCallback(HitTestResult_Tap_v1),
                    new PointHitTestParameters(new Point(pt.Position.X, pt.Position.Y)));
            if (other)
                VisualTreeHelper.HitTest(this._list, null,
                    new HitTestResultCallback(HitTestResult_Tap_other),
                    new PointHitTestParameters(new Point(pt.Position.X, pt.Position.Y)));

            if (hitResultsList.Count > 0)
            {
                Type t_item = null;
                if (other) t_item = Type.GetType("nature_net.user_controls.collection_listbox_item");
                if (v1) t_item = Type.GetType("nature_net.user_controls.item_generic");
                if (v2) t_item = Type.GetType("nature_net.user_controls.item_generic_v2");
                if (t_item == null) return null;
                if (hitResultsList[hitResultsList.Count - 1].GetType() == t_item)
                {
                    if (!drag)
                        return (UIElement)hitResultsList[hitResultsList.Count - 1];
                    if (hitResultsList.Count > 1)
                    {
                        Type t1 = hitResultsList[hitResultsList.Count - 2].GetType();
                        Type t2 = null;
                        if (!other)
                            t2 = Type.GetType("System.Windows.Controls.DockPanel, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
                        else
                            t2 = Type.GetType("System.Windows.Controls.Image, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
                        if (hitResultsList[hitResultsList.Count - 2].GetType() == t2)
                        {
                            FrameworkElement i2 = (FrameworkElement)hitResultsList[hitResultsList.Count - 2];
                            if (i2.Name == "right_panel" || i2.Name == "drag")
                                return (UIElement)hitResultsList[hitResultsList.Count - 1];
                        }
                    }
                }
            }
            return null;
        }
        public HitTestResultBehavior HitTestResult_Tap_v1(HitTestResult result)
        {
            hitResultsList.Add(result.VisualHit);
            Type t1 = result.VisualHit.GetType();
            Type t2 = Type.GetType("nature_net.user_controls.item_generic");
            if (t1 == t2)
                return HitTestResultBehavior.Stop;
            return HitTestResultBehavior.Continue;
        }
        public HitTestResultBehavior HitTestResult_Tap_v2(HitTestResult result)
        {
            hitResultsList.Add(result.VisualHit);
            Type t1 = result.VisualHit.GetType();
            Type t2 = Type.GetType("nature_net.user_controls.item_generic_v2");
            if (t1 == t2)
                return HitTestResultBehavior.Stop;
            return HitTestResultBehavior.Continue;
        }
        public HitTestResultBehavior HitTestResult_Tap_other(HitTestResult result)
        {
            hitResultsList.Add(result.VisualHit);
            Type t1 = result.VisualHit.GetType();
            Type t2 = Type.GetType("nature_net.user_controls.collection_listbox_item");
            if (t1 == t2)
                return HitTestResultBehavior.Stop;
            return HitTestResultBehavior.Continue;
        }

        void _list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!configurations.manual_tap && !selectable)
            {
                if (e.AddedItems.Count > 0)
                    _list_select_item(e.AddedItems[0]);
            }
            if (!selectable)
                _list.SelectedIndex = -1;
            this.last_selected_index = _list.SelectedIndex;
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
            try { i = (item_generic)element.DataContext; }
            catch (Exception)
            {
                Image i2;
                try { i2 = (Image)element.DataContext; }
                catch (Exception) { return; }
                if (i2.Tag == null) return;
                collection_item item = (collection_item)i2.Tag;
                if (i2.Source != null)
                    start_drag(element, item, touch_device, i2.Source.Clone());
                return;
            }

            if (i.Tag == null)
                return;

            string avatar = "";
            if (i.avatar.Source != null)
                avatar = i.avatar.Source.ToString();
            string data = drag_prefix + ";" + ((int)i.Tag).ToString() + ";" + avatar + ";" +
                (string)i.username.Text + ";" + i.user_desc.Content + ";" + i.desc.Content + ";" + i.content.Text;
            ImageSource img = null;
            if (i.avatar.Source != null)
                img = i.avatar.Source.Clone();
            start_drag(element, data, touch_device, img);
        }
        private bool start_drag(ListBoxItem item, object data, TouchDevice touch_device, ImageSource i)
        {
            Image i2 = new Image();
            i2.Source = i; i2.Stretch = Stretch.Uniform;
            item_generic i3 = null;
            try { i3 = (item_generic)item.Content; }
            catch (Exception) { }
            ContentControl cursorVisual = new ContentControl();
            if (i3 != null)
                cursorVisual.Content = i3.get_clone();
            else
                cursorVisual.Content = i2;
            cursorVisual.Style = (FindResource("CursorStyle") as Style);

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

            Microsoft.Surface.Presentation.SurfaceDragCursor startDragOkay =
                Microsoft.Surface.Presentation.SurfaceDragDrop.BeginDragDrop(
                  this._list,                 // The SurfaceListBox object that the cursor is dragged out from.
                  element,                    // The SurfaceListBoxItem object that is dragged from the drag source.
                  cursorVisual,               // The visual element of the cursor.
                  data,                       // The data associated with the cursor.
                  devices,                    // The input devices that start dragging the cursor.
                  DragDropEffects.Copy);      // The allowed drag-and-drop effects of the operation.

            return (startDragOkay != null);
        }
        private bool start_drag(item_generic_v2 item, TouchEventArgs e)
        {
            if (item == null) return false;
            ContentControl cursorVisual = new ContentControl();
            cursorVisual.Content = item.get_clone();
            cursorVisual.Style = (FindResource("CursorStyle") as Style);

            List<TouchDevice> devices = new List<TouchDevice>();
            devices.Add(e.TouchDevice);
            foreach (TouchDevice touch in item.TouchesCapturedWithin)
            {
                if (touch != e.TouchDevice)
                {
                    devices.Add(touch);
                }
            }

            if (item.Tag == null) return false;

            //string avatar = "";
            //if (item.avatar.Source != null)
            //    avatar = item.avatar.Source.ToString();
            //string data = drag_prefix + ";" + ((int)item.Tag).ToString() + ";" + avatar + ";" +
            //    (string)item.title.Text + ";" + item.description.Text + ";" + "" + ";" + "";
            string data = drag_prefix + ";" + item.ToString();

            Microsoft.Surface.Presentation.SurfaceDragCursor startDragOkay =
                Microsoft.Surface.Presentation.SurfaceDragDrop.BeginDragDrop(
                  this._list,                 // The SurfaceListBox object that the cursor is dragged out from.
                  item,                       // The item object that is dragged from the drag source.
                  cursorVisual,               // The visual element of the cursor.
                  data,                       // The data associated with the cursor.
                  devices,                    // The input devices that start dragging the cursor.
                  DragDropEffects.Copy);      // The allowed drag-and-drop effects of the operation.

            return (startDragOkay != null);
        }
        private bool start_drag(collection_listbox_item img, TouchDevice input)
        {
            if (img == null) return false;
            Image i2 = new Image();
            i2.Source = img.img.Source; i2.Stretch = Stretch.Uniform;
            ContentControl cursorVisual = new ContentControl();
            cursorVisual.Content = i2;
            cursorVisual.Style = (FindResource("CursorStyle") as Style);

            List<TouchDevice> devices = new List<TouchDevice>();
            devices.Add(input);
            foreach (TouchDevice touch in _list.TouchesCapturedWithin)
            {
                if (touch != input)
                {
                    devices.Add(touch);
                }
            }

            if (img.img.Tag == null) return false;

            collection_item item = (collection_item)img.img.Tag;

            Microsoft.Surface.Presentation.SurfaceDragCursor startDragOkay =
                Microsoft.Surface.Presentation.SurfaceDragDrop.BeginDragDrop(
                  this._list,                 // The SurfaceListBox object that the cursor is dragged out from.
                  img,                        // The item object that is dragged from the drag source.
                  cursorVisual,               // The visual element of the cursor.
                  item,                       // The data associated with the cursor.
                  devices,                    // The input devices that start dragging the cursor.
                  DragDropEffects.Copy);      // The allowed drag-and-drop effects of the operation.

            return (startDragOkay != null);
        }

        private void _list_select_item(object item)
        {
            //if (list_comments)
            //{
            //    string[] idea_item = ("comment;" + item.ToString()).Split(new Char[] { ';' });
            //    //window_manager.open_design_idea_window(idea_item,
            //    //    configurations.RANDOM((int)(window_manager.main_canvas.ActualWidth - item.ActualWidth) - 20,
            //    //    (int)(window_manager.main_canvas.ActualWidth - item.ActualWidth)),
            //    //    item.PointToScreen(new Point(0, 0)).Y);
            //    _list.SelectedIndex = -1;
            //    return;
            //}
            bool do_deselect = true;
            if (item_selected != null)
                do_deselect = item_selected(item);
            if (do_deselect)
                _list.SelectedIndex = -1;
            else
            {
                if (last_selected_index == _list.SelectedIndex)
                    _list.SelectedIndex = -1;
            }
            this.last_selected_index = _list.SelectedIndex;
        }

        private void _list_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (test_thumb_feedback(e))
            {
                e.Handled = true;
                return;
            }
            if (!touch_points.ContainsKey(e.TouchDevice.Id))
            {
                touch_info t = new touch_info();
                t.id = e.TouchDevice.Id; t.is_tap = true; t.points.Add(e.GetTouchPoint(this._list as IInputElement));
                this.touch_points.Add(e.TouchDevice.Id, t);
            }
            ScrollViewer scroll = configurations.GetDescendantByType(this._list, typeof(ScrollViewer)) as ScrollViewer;
            if (is_horizontal)
                last_scroll_offset = scroll.HorizontalOffset;
            else
                last_scroll_offset = scroll.VerticalOffset;
            //scroll.Elasticity = new Vector(0.0, 0.4);

            if (configurations.right_panel_drag)
            {
                UIElement dragged_item = null;
                if (collection_list)
                    dragged_item = HitTestOverItem(e, true, false, false, true);
                else
                    dragged_item = HitTestOverItem(e, true, true, false, false);
                if (dragged_item != null)
                {
                    //avatar_drag(element, e.TouchDevice);
                    bool result = false;
                    if (dragged_item.GetType() == Type.GetType("nature_net.user_controls.item_generic_v2"))
                        result = start_drag((item_generic_v2)dragged_item, e);
                    if (dragged_item.GetType() == Type.GetType("nature_net.user_controls.collection_listbox_item"))
                        result = start_drag((collection_listbox_item)dragged_item, e.TouchDevice);

                    touch_points[e.TouchDevice.Id].is_drag = true;
                    touch_points[e.TouchDevice.Id].points.Clear();
                    touch_points[e.TouchDevice.Id].consecutive_drag_points = 0;
                    e.Handled = true;
                    return;
                }
            }

            if (configurations.manual_scroll)
            {
                bool r = e.TouchDevice.Capture(this._list as IInputElement, CaptureMode.SubTree);
                e.Handled = true;
            }
        }
        private void _list_PreviewTouchMove(object sender, TouchEventArgs e)
        {
            //if (debug_canvas == null)
            //{
            //    debug_canvas = new Canvas();
            //    debug_canvas.Width = window_manager.main_canvas.ActualWidth;
            //    debug_canvas.Height = window_manager.main_canvas.ActualHeight;
            //    window_manager.main_canvas.Children.Add(debug_canvas);
            //}

            IInputElement ie = e.TouchDevice.Captured;
            FrameworkElement findSource = e.Source as FrameworkElement;
            ListBoxItem element = null;
            while (element == null && findSource != null)
                if ((element = findSource as ListBoxItem) == null)
                    findSource = VisualTreeHelper.GetParent(findSource) as FrameworkElement;

            if (!touch_points.ContainsKey(e.TouchDevice.Id))
            {
                touch_info t = new touch_info();
                t.id = e.TouchDevice.Id; t.is_tap = false; t.points.Add(e.GetTouchPoint(this._list as IInputElement));
                this.touch_points.Add(e.TouchDevice.Id, t);
            }
            else
                this.touch_points[e.TouchDevice.Id].points.Add(e.GetTouchPoint(this._list as IInputElement));

            if (touch_points[e.TouchDevice.Id].is_drag == true) return;
            //TextBlock t1 = new TextBlock(); t1.Foreground = Brushes.White;
            //Canvas.SetLeft(t1, 80); Canvas.SetTop(t1, debug_var);
            //t1.Text =  e.GetTouchPoint(this._list as IInputElement).Position.X + ", " + e.GetTouchPoint(this._list as IInputElement).Position.Y;
            //t1.FontSize = 14; t1.FontWeight = FontWeights.Bold;
            //debug_canvas.Children.Add(t1);
            //debug_var = debug_var + 30;
            //if (debug_var > 1200) { debug_var = 10; debug_canvas.Children.RemoveRange(0, debug_canvas.Children.Count); }

            if (touch_points[e.TouchDevice.Id].points.Count < configurations.min_touch_points) return;
            double dy = touch_points[e.TouchDevice.Id].points[touch_points[e.TouchDevice.Id].points.Count - 1].Position.Y - touch_points[e.TouchDevice.Id].points[touch_points[e.TouchDevice.Id].points.Count - 2].Position.Y;
            double dx = touch_points[e.TouchDevice.Id].points[touch_points[e.TouchDevice.Id].points.Count - 1].Position.X - touch_points[e.TouchDevice.Id].points[touch_points[e.TouchDevice.Id].points.Count - 2].Position.X;
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
                if (touch_points[e.TouchDevice.Id].consecutive_drag_points < configurations.max_consecutive_drag_points)
                {
                    touch_points[e.TouchDevice.Id].consecutive_drag_points++;
                }
                else
                {
                    touch_points[e.TouchDevice.Id].is_tap = false;
                    //if (element == null) element = last_dragged_element;
                    if (configurations.whole_item_drag)
                    {
                        UIElement dragged_item = HitTestOverItem(e, true, true, false, false);
                        if (dragged_item != null)
                        {
                            //avatar_drag(element, e.TouchDevice);
                            bool result = false;
                            if (dragged_item.GetType() == Type.GetType("nature_net.user_controls.item_generic_v2"))
                                result = start_drag((item_generic_v2)dragged_item, e);
                            //else
                            //    result = start_drag((Image)dragged_item, e.TouchDevice);

                            touch_points[e.TouchDevice.Id].is_drag = true;
                            touch_points[e.TouchDevice.Id].points.Clear();
                            touch_points[e.TouchDevice.Id].consecutive_drag_points = 0;
                            e.Handled = true;
                            return;
                        }
                    }
                }
            }
            
            ScrollViewer scroll = configurations.GetDescendantByType(this._list, typeof(ScrollViewer)) as ScrollViewer;
            double dv = touch_points[e.TouchDevice.Id].points[touch_points[e.TouchDevice.Id].points.Count - 1].Position.Y - touch_points[e.TouchDevice.Id].points[touch_points[e.TouchDevice.Id].points.Count - 2].Position.Y;
            double dh = touch_points[e.TouchDevice.Id].points[touch_points[e.TouchDevice.Id].points.Count - 1].Position.X - touch_points[e.TouchDevice.Id].points[touch_points[e.TouchDevice.Id].points.Count - 2].Position.X;

            if (dv > configurations.tap_error || dh > configurations.tap_error)
                touch_points[e.TouchDevice.Id].is_tap = false;
            if (!selectable) this._list.SelectedIndex = -1;
            if (configurations.manual_scroll)
            {
                if (is_horizontal)
                {
                    double new_offset = scroll.HorizontalOffset + (-1 * dh);
                    try { scroll.ScrollToHorizontalOffset(new_offset); }
                    catch (Exception) { }
                    //last_scroll_offset = scroll.HorizontalOffset;
                }
                else
                {
                    double new_offset = scroll.VerticalOffset + (-1 * dv);
                    try { scroll.ScrollToVerticalOffset(new_offset); }
                    catch (Exception ex) { }
                    //last_scroll_offset = scroll.VerticalOffset;
                }
            }
        }
        private void _list_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            //double dv = 0;
            //if (touch_points.Count > 0)
            //    dv = e.GetTouchPoint(this._list).Position.Y - touch_points[0].Position.Y;
            bool can_scroll = false;
            if (!touch_points.ContainsKey(e.TouchDevice.Id)) return;

            if (touch_points[e.TouchDevice.Id].is_tap)
            {
                if (touch_points[e.TouchDevice.Id].points.Count < 2) return;
                double dv = touch_points[e.TouchDevice.Id].points[touch_points[e.TouchDevice.Id].points.Count - 1].Position.Y - touch_points[e.TouchDevice.Id].points[0].Position.Y;
                double dh = touch_points[e.TouchDevice.Id].points[touch_points[e.TouchDevice.Id].points.Count - 1].Position.X - touch_points[e.TouchDevice.Id].points[0].Position.X;
                bool should_tap = true;
                if (is_horizontal && dh > configurations.tap_error)
                    should_tap = false;
                if (!is_horizontal && dv > configurations.tap_error)
                    should_tap = false;

                if (should_tap)
                {
                    //FrameworkElement findSource = e.OriginalSource as FrameworkElement;
                    //ListBoxItem element = null;
                    //while (element == null && findSource != null)
                    //    if ((element = findSource as ListBoxItem) == null)
                    //        findSource = VisualTreeHelper.GetParent(findSource) as FrameworkElement;
                    ////
                    if (configurations.manual_tap || selectable)
                    {
                        UIElement selected_item = null;
                        if (collection_list)
                            selected_item = HitTestOverItem(e, false, false, false, true);
                        else if (comment_list)
                            selected_item = HitTestOverItem(e, false, false, true, false);
                        else
                            selected_item = HitTestOverItem(e, false, true, false, false);
                        if (selected_item != null)
                        {
                            //_list.SelectedItem = element;
                            try
                            {
                                _list_select_item(selected_item);
                            }
                            catch (Exception e2) { log.WriteErrorLog(e2); }
                        }
                    }
                }
            }
            else
                can_scroll = true;

            if (can_scroll)
            {
                //ScrollViewer scroll = configurations.GetDescendantByType(this._list, typeof(ScrollViewer)) as ScrollViewer;
                ////double dv = e.GetTouchPoint(this.contributions).Position.X - touch_points[touch_points.Count - 1].Position.X;
                //try
                //{
                //    //scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset + (-2 * dv));
                //    scroll.ScrollToVerticalOffset(last_scroll_offset + (-1 * dv));
                //}
                //catch (Exception) { }
                //last_scroll_offset = scroll.VerticalOffset;
            }
            touch_points[e.TouchDevice.Id].points.Clear();
            touch_points.Remove(e.TouchDevice.Id);
            UIElement element2 = sender as UIElement;
            element2.ReleaseTouchCapture(e.TouchDevice);
        }

        private bool test_thumb_feedback(TouchEventArgs e)
        {
            TouchPoint pt = e.GetTouchPoint(this._list as IInputElement);
            HitTestResult hr = VisualTreeHelper.HitTest(this._list, new Point(pt.Position.X, pt.Position.Y));
            try
            {
                Image i = (Image)hr.VisualHit;
                if (i.Name == "avatar")
                {
                    item_generic_v2 i2 =null;
                    try { i2 = (item_generic_v2)i.Tag; }
                    catch (Exception) { return false; }
                    if (populator.thumbs_up_handler != null)
                        populator.thumbs_up_handler(i2, e);
                    return true;
                }
                //if (i.Name == "img_dislike")
                //{
                //    item_generic i2 = (item_generic)i.Tag;
                //    if (populator.thumbs_down_handler != null)
                //        populator.thumbs_down_handler(i2, e);
                //    return true;
                //}
            }
            catch (Exception) { return false; }
            return false;
        }

        protected override void OnManipulationBoundaryFeedback(ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }

    public class list_populator
    {
        public item_generic initial_item = null;
        public double item_width;
        public ListBox _list;
        private readonly BackgroundWorker worker = new BackgroundWorker();
        public Thickness items_margins = new Thickness(0);
        public avatar_touch_down_handler avatar_drag;
        public list_header header;
        public thumbs_up thumbs_up_handler;
        public thumbs_down thumbs_down_handler;

        // for design ideas
        public void list_all_design_ideas()
        {
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
                    DateTime last_time = d.date;
                    var n1 = from f in db.Feedbacks
                             where (f.object_type == "nature_net.Contribution") && (f.object_id == d.id)
                             orderby f.date descending
                             select f;
                    int cnt = 0; int num_like = 0; int num_dislike = 0; int num_comments = 0;
                    if (n1 != null)
                        cnt = n1.Count();
                    if (cnt != 0)
                    {
                        last_time = n1.First().date;
                        foreach (Feedback f2 in n1)
                        {
                            if (f2.Feedback_Type.name == "Like")
                                if (Convert.ToBoolean(f2.note))
                                    num_like++;
                                else
                                    num_dislike++;
                            if (f2.Feedback_Type.name == "Comment")
                                num_comments++;
                        }
                    }
                    design_idea_item i = new design_idea_item();
                    ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + d.avatar));
                    src.Freeze();
                    i.img = src;
                    i.design_idea = d;
                    i.count = num_comments;
                    i.last_date = last_time;
                    i.num_dislike = num_dislike;
                    i.num_like = num_like;
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
                    if (initial_item != null)
                        this._list.Items.Add(initial_item);

                    List<design_idea_item> ideas = (List<design_idea_item>)e.Result;
                    foreach (design_idea_item idea in ideas)
                    {
                        item_generic_v2 i = new item_generic_v2();
                        i.title.Text = idea.design_idea.note; i.description.Visibility = Visibility.Collapsed;
                        TextBlock.SetFontWeight(i.title, FontWeights.Normal); 
                        i.title.FontSize = configurations.design_idea_item_title_font_size;
                        i.user_info.Margin = new Thickness(5);
                        i.user_info_name.Text = idea.design_idea.name; i.user_info_date.Text = configurations.GetDate_Formatted(idea.last_date);
                        i.user_info_name.Margin = new Thickness(2, 0, 0, 0); i.user_info_date.Margin = new Thickness(2, 0, 2, 0);
                        i.user_info_name.FontSize = configurations.design_idea_item_user_info_font_size; i.user_info_date.FontSize = configurations.design_idea_item_user_info_font_size;
                        i.user_info_icon.Source = idea.img; i.number.Text = idea.count.ToString(); i.number_icon.Visibility = Visibility.Collapsed;
                        i.txt_level1.Text = configurations.designidea_num_desc;
                        i.txt_level2.Visibility = Visibility.Collapsed; i.txt_level3.Visibility = Visibility.Collapsed;
                        i.avatar.Source = configurations.img_thumbs_up_icon; i.num_likes.Content = idea.num_like.ToString();
                        i.avatar.Width = configurations.design_idea_item_avatar_width; i.avatar.Height = configurations.design_idea_item_avatar_width; i.avatar.Margin = new Thickness(5); i.avatar.Tag = i;
                        i.Tag = idea.design_idea.id;
                        i.Margin = items_margins;
                        if (item_width != 0) i.Width = item_width;
                        i.right_panel.Width = configurations.design_idea_right_panel_width;
                        //i.left_panel.VerticalAlignment = VerticalAlignment.Center; DockPanel.SetDock(i.number, Dock.Left); DockPanel.SetDock(i.txt_level1, Dock.Left);
                        i.top_value = idea.num_like;
                        if (thumbs_up_handler != null)
                            i.avatar.Tag = i;
                        this._list.Items.Add(i);
                    }
                    if (header.atoz.IsChecked.Value && header.atoz_order != null) header.atoz_order();
                    if (header.top.IsChecked.Value && header.top_order != null) header.top_order();
                    if (header.recent.IsChecked.Value && header.recent_order != null) header.recent_order();
                    this._list.Items.Refresh();
                    this._list.UpdateLayout();
                }));
        }

        // for users
        public void list_all_users()
        {
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
                    var n1 = from m in db.Collection_Contribution_Mappings
                             where m.Collection.user_id == u.id
                             orderby m.Contribution.date descending
                             select m.Contribution.date;
                    //var n2 = from f in db.Feedbacks
                    //         where f.user_id == u.id
                    //         orderby f.date descending
                    //         select f.date;
                    List<DateTime> n2 = null;
                    int cnt = 0;
                    if (n1 != null)
                        cnt = n1.Count();
                    if (n2 != null)
                        cnt = cnt + n2.Count();
                    user_item i = new user_item();
                    ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + u.avatar));
                    src.Freeze();
                    i.img = src;
                    i.user = u;
                    i.count = cnt;
                    i.has_date = false;
                    if (n1 != null)
                    {
                        if (n1.Count() > 0)
                        {
                            i.last_date = n1.First();
                            i.has_date = true;
                        }
                    }
                    if (n2 != null)
                    {
                        if (n2.Count() > 0)
                        {
                            if (i.has_date)
                            {
                                if (i.last_date.CompareTo(n2.First()) < 0)
                                    i.last_date = n2.First();
                            }
                            else
                            {
                                i.last_date = n2.First();
                                i.has_date = true;
                            }
                        }
                    }
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
                    if (initial_item != null)
                        this._list.Items.Add(initial_item);

                    List<user_item> users = (List<user_item>)e.Result;
                    foreach (user_item u in users)
                    {
                        item_generic_v2 i = new item_generic_v2();
                        if (u.has_date)
                            i.txt_level2.Text = configurations.GetDate_Formatted(u.last_date);
                        else
                            i.txt_level2.Text = configurations.users_no_date;
                        i.title.Text = u.user.name; i.avatar.Source = u.img; i.Tag = u.user.id;
                        i.number.Text = u.count.ToString();
                        if (item_width != 0) i.Width = item_width;
                        i.Margin = items_margins; i.txt_level2.Margin = new Thickness(0, 0, 0, 10);
                        i.num_likes.Visibility = Visibility.Collapsed; i.txt_level1.Visibility = Visibility.Collapsed;
                        i.txt_level3.Visibility = Visibility.Collapsed; i.description.Visibility = Visibility.Collapsed;
                        i.center_panel.VerticalAlignment = VerticalAlignment.Center;
                        i.avatar.Width = configurations.user_item_avatar_width; i.avatar.Height = configurations.user_item_avatar_width;
                        i.user_info.Visibility = Visibility.Collapsed; i.user_info_date.Text = i.txt_level2.Text;
                        i.top_value = u.count;
                        this._list.Items.Add(i);
                    }
                    if (header.atoz.IsChecked.Value && header.atoz_order != null) header.atoz_order();
                    if (header.top.IsChecked.Value && header.top_order != null) header.top_order();
                    if (header.recent.IsChecked.Value && header.recent_order != null) header.recent_order();
                    this._list.Items.Refresh();
                    this._list.UpdateLayout();
                }));
        }

        // for comments
        public void list_all_comments(comment_item item)
        {
            worker.DoWork += new DoWorkEventHandler(get_all_comments);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_comments);
            if (!worker.IsBusy)
                worker.RunWorkerAsync(item);
        }
        public void get_all_comments(object arg, DoWorkEventArgs e)
        {
            if (e.Argument == null) return;
            e.Result = (object)(new List<comment_item_generic>());
            try
            {
                comment_item item = (comment_item)e.Argument;
                naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
                var r = from c in db.Feedbacks
                        where (c.Feedback_Type.name == "Comment") && (c.object_type == item._object_type.ToString())
                        && (c.object_id == item._object_id) && (c.parent_id == 0)
                        orderby c.date descending
                        select c;
                if (r != null)
                {
                    List<Feedback> comments = r.ToList<Feedback>();
                    List<comment_item_generic> comment_items = new List<comment_item_generic>();
                    foreach (Feedback f in comments)
                    {
                        int level = 0;
                        comment_items.Add(create_comment_item(f, level));
                        add_comment_items_for(f, ref level, comment_items, db);
                    }
                    e.Result = (object)comment_items;
                }
                else
                {
                    e.Result = (object)(new List<comment_item_generic>());
                }
            }
            catch (Exception ex)
            {
                log.WriteErrorLog(ex);
            }
        }
        public void add_comment_items_for(Feedback c, ref int level, List<comment_item_generic> current_items, naturenet_dataclassDataContext db)
        {
            var r = from c2 in db.Feedbacks
                    where (c2.Feedback_Type.name == "Comment") && (c2.parent_id == c.id)
                    orderby c2.date descending
                    select c2;
            if (r != null)
            {
                List<Feedback> comments = r.ToList<Feedback>();
                level++;
                foreach (Feedback f in comments)
                {
                    int new_level = level;
                    current_items.Add(create_comment_item(f, new_level));
                    add_comment_items_for(f, ref new_level, current_items, db);
                }
            }
        }
        public comment_item_generic create_comment_item(Feedback c, int level)
        {
            comment_item_generic cig = new comment_item_generic();
            cig.comment = c; cig.level = level;
            return cig;
        }
        public void display_all_comments(object c_obj, RunWorkerCompletedEventArgs e)
        {
            this._list.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
               new System.Action(() =>
               {
                   this._list.Items.Clear();
                   if (initial_item != null)
                       this._list.Items.Add(initial_item);

                   List<comment_item_generic> comments = (List<comment_item_generic>)e.Result;
                   foreach (comment_item_generic cig in comments)
                   {
                       item_generic i = new item_generic();
                       i.username.Text = cig.comment.User.name;
                       i.user_desc.Content = configurations.GetDate_Formatted(cig.comment.date);
                       i.number.Visibility = System.Windows.Visibility.Collapsed;
                       i.desc.Content = "Commented:";
                       i.content.Text = cig.comment.note;
                       if (item_width != 0) i.Width = item_width + 2;
                       i.avatar.Source = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + cig.comment.User.avatar));
                       i.Tag = cig.comment.id;
                       if (configurations.use_avatar_drag) i.set_touchevent(this.avatar_drag);
                       i.Margin = new Thickness(0);
                       i.second_border.Margin = new Thickness(cig.level * 25, 0, 0, 0);
                       i.first_border.BorderBrush = Brushes.Gray; i.first_border.BorderThickness = new Thickness(0, 0, 0, 1);
                       i.second_border.BorderBrush = Brushes.DarkGray; i.second_border.BorderThickness = new Thickness(1, 0, 0, 0);
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
            worker.DoWork += new DoWorkEventHandler(get_all_activities);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_activities);
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
        }
        public void get_all_activities(object arg, DoWorkEventArgs e)
        {
            e.Result = (object)(new List<activity_item>());
            try
            {
                naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
                var r = from a in db.Activities
                        where (a.name != "Free Observation") && (a.name != "Design Idea")
                        select a;
                if (r != null)
                {
                    //List<Activity> activities = r.ToList<Activity>();
                    List<activity_item> activity_items = new List<activity_item>();
                    foreach (Activity a in r)
                    {
                        DateTime last_time = a.creation_date;
                        var n1 = from m in db.Collection_Contribution_Mappings
                                 where m.Collection.activity_id == a.id
                                 orderby m.Contribution.date descending
                                 select new { m.Contribution.date, m.Collection.User.name };
                        int cnt = 0;
                        if (n1 != null)
                            cnt = n1.Count();

                        activity_item ai = new activity_item();
                        ai.activity = a;
                        ai.count = cnt;
                        ai.username = n1.First().name;
                        if (cnt != 0)
                            last_time = n1.First().date;
                        ai.last_date = last_time;
                        activity_items.Add(ai);
                    }
                    e.Result = (object)activity_items;
                }
                else
                {
                    e.Result = (object)(new List<activity_item>());
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
                   if (initial_item != null)
                       this._list.Items.Add(initial_item);

                   List<activity_item> activities = (List<activity_item>)e.Result;
                   foreach (activity_item a in activities)
                   {
                       item_generic_v2 i = new item_generic_v2();
                       i.title.Text = a.activity.name; i.title.Margin = new Thickness(5);
                       i.description.Text = a.activity.description; i.description.Margin = new Thickness(5);
                       i.txt_level2.Text = configurations.GetDate_Formatted(a.last_date);
                       i.txt_level3.Text = a.username; i.number.Text = a.count.ToString();
                       i.Tag = a.activity.id;
                       i.txt_level1.Visibility = Visibility.Collapsed;
                       i.left_panel.Visibility = Visibility.Collapsed;
                       if (item_width != 0) i.Width = item_width;
                       i.Margin = items_margins;
                       i.user_info.Visibility = Visibility.Collapsed; i.user_info_date.Text = i.txt_level2.Text;
                       i.top_value = a.count;
                       this._list.Items.Add(i);
                   }
                   if (header.atoz.IsChecked.Value && header.atoz_order != null) header.atoz_order();
                   if (header.top.IsChecked.Value && header.top_order != null) header.top_order();
                   if (header.recent.IsChecked.Value && header.recent_order != null) header.recent_order();
                   this._list.Items.Refresh();
                   this._list.UpdateLayout();
               }));
        }
    }

    public class design_idea_item
    {
        public ImageSource img;
        public Design_Idea design_idea;
        public int count;
        public DateTime last_date;
        public int num_like;
        public int num_dislike;
    }

    public class user_item
    {
        public ImageSource img;
        public User user;
        public int count;
        public DateTime last_date;
        public bool has_date;
    }

    public class activity_item
    {
        public Activity activity;
        public int count;
        public DateTime last_date;
        public string username;
    }

    public class comment_item
    {
        public int _object_id;
        public Type _object_type;
    }

    public class comment_item_generic
    {
        public Feedback comment;
        public int level;
    }

    public class touch_info
    {
        public int id;
        public List<System.Windows.Input.TouchPoint> points = new List<TouchPoint>();
        public bool is_tap = false;
        public bool is_drag = false;
        public int consecutive_drag_points;
    }

    public delegate bool ItemSelected(object i);
}
