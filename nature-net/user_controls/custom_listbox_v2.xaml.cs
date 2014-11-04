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

        public string content_name = "";

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
                this._list.Background = Brushes.Transparent;

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
            if (e.Handled)
                return null;
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
                    {
                        return (UIElement)hitResultsList[hitResultsList.Count - 1];
                    }
                    if (hitResultsList.Count > 1)
                    {
                        for (int counter = 0; counter < hitResultsList.Count - 1; counter++)
                        {
                            Type t1 = hitResultsList[counter].GetType();
                            Type t2 = null;
                            if (!other)
                                t2 = Type.GetType("System.Windows.Controls.Border, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
                            else
                                t2 = Type.GetType("System.Windows.Controls.Image, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
                            if (hitResultsList[counter].GetType() == t2)
                            {
                                FrameworkElement i2 = (FrameworkElement)hitResultsList[counter];
                                if (i2.Name == "right_panel_border" || i2.Name == "drag")
                                    return (UIElement)hitResultsList[hitResultsList.Count - 1];
                            }
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
            item.drag_prefix = drag_prefix;
            log.WriteInteractionLog(4, "start dragging the listbox item: " + item.ToString(), e.TouchDevice);
            Microsoft.Surface.Presentation.SurfaceDragCursor startDragOkay =
                Microsoft.Surface.Presentation.SurfaceDragDrop.BeginDragDrop(
                  this._list,                 // The SurfaceListBox object that the cursor is dragged out from.
                  item,                       // The item object that is dragged from the drag source.
                  cursorVisual,               // The visual element of the cursor.
                  item,                       // The data associated with the cursor.
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
            log.WriteInteractionLog(4, "start dragging the contribution id: " + item._contribution.id, input);
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
                do_deselect = item_selected(item, null);
            if (do_deselect)
                _list.SelectedIndex = -1;
            else
            {
                if (last_selected_index == _list.SelectedIndex)
                    _list.SelectedIndex = -1;
            }
            this.last_selected_index = _list.SelectedIndex;
        }

        private void _list_select_item(object item, TouchEventArgs e)
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
                do_deselect = item_selected(item, e);
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
            if (e.Handled)
                return;
            if (!touch_points.ContainsKey(e.TouchDevice.Id))
            {
                touch_info t = new touch_info();
                t.id = e.TouchDevice.Id; t.is_tap = true; t.points.Add(e.GetTouchPoint(this._list as IInputElement));
                try { t.initial_attended_item = (Control)e.Source; }
                catch (Exception) { }
                this.touch_points.Add(e.TouchDevice.Id, t);
            }
            else
            {

            }
            FadingScrollViewer scroll = configurations.GetDescendantByType(this._list, typeof(FadingScrollViewer)) as FadingScrollViewer;
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
                    touch_points.Remove(e.TouchDevice.Id);
                    return;
                }
            }

            if (configurations.manual_scroll)
            {
                bool r = e.TouchDevice.Capture(this._list as IInputElement, CaptureMode.SubTree);
                e.Handled = true;
                touch_points.Remove(e.TouchDevice.Id);
                return;
            }
            if (touch_points[e.TouchDevice.Id].initial_attended_item != null)
                attend_on_item(true, touch_points[e.TouchDevice.Id].initial_attended_item);
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
            {
                this.touch_points[e.TouchDevice.Id].points.Add(e.GetTouchPoint(this._list as IInputElement));
            }

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
            if (theta < configurations.drag_collection_theta && configurations.whole_item_drag)
            {
                if (touch_points[e.TouchDevice.Id].consecutive_drag_points < configurations.max_consecutive_drag_points)
                {
                    touch_points[e.TouchDevice.Id].consecutive_drag_points++;
                }
                else
                {
                    touch_points[e.TouchDevice.Id].is_tap = false;
                    if (touch_points[e.TouchDevice.Id].initial_attended_item != null)
                        attend_on_item(false, touch_points[e.TouchDevice.Id].initial_attended_item);
                    //if (element == null) element = last_dragged_element;
                    //if (configurations.whole_item_drag)
                    //{
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
                            touch_points.Remove(e.TouchDevice.Id);
                            return;
                        }
                    //}
                }
            }

            FadingScrollViewer scroll = configurations.GetDescendantByType(this._list, typeof(FadingScrollViewer)) as FadingScrollViewer;
            //double dv = touch_points[e.TouchDevice.Id].points[touch_points[e.TouchDevice.Id].points.Count - 1].Position.Y - touch_points[e.TouchDevice.Id].points[touch_points[e.TouchDevice.Id].points.Count - 2].Position.Y;
            //double dh = touch_points[e.TouchDevice.Id].points[touch_points[e.TouchDevice.Id].points.Count - 1].Position.X - touch_points[e.TouchDevice.Id].points[touch_points[e.TouchDevice.Id].points.Count - 2].Position.X;

            double dv = touch_points[e.TouchDevice.Id].points[touch_points[e.TouchDevice.Id].points.Count - 1].Position.Y - touch_points[e.TouchDevice.Id].points[0].Position.Y;
            double dh = touch_points[e.TouchDevice.Id].points[touch_points[e.TouchDevice.Id].points.Count - 1].Position.X - touch_points[e.TouchDevice.Id].points[0].Position.X;
            //if (touch_points[e.TouchDevice.Id].points.Count > 4)
            //    dv++;
            if (Math.Abs(dv) > configurations.tap_error || Math.Abs(dh) > configurations.tap_error)
            {
                touch_points[e.TouchDevice.Id].is_tap = false;
                if (touch_points[e.TouchDevice.Id].initial_attended_item != null)
                    attend_on_item(false, touch_points[e.TouchDevice.Id].initial_attended_item);
            }
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
                    catch (Exception) { }
                    //last_scroll_offset = scroll.VerticalOffset;
                }
            }
        }
        private void _list_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            if (test_thumb_feedback(e))
            {
                e.Handled = true;
                return;
            }
            //double dv = 0;
            //if (touch_points.Count > 0)
            //    dv = e.GetTouchPoint(this._list).Position.Y - touch_points[0].Position.Y;
            bool can_scroll = false;
            if (!touch_points.ContainsKey(e.TouchDevice.Id)) return;

            if (touch_points[e.TouchDevice.Id].is_tap)
            {
                
                //if (touch_points[e.TouchDevice.Id].points.Count < 2) return;
                //double dv = touch_points[e.TouchDevice.Id].points[touch_points[e.TouchDevice.Id].points.Count - 1].Position.Y - touch_points[e.TouchDevice.Id].points[0].Position.Y;
                //double dh = touch_points[e.TouchDevice.Id].points[touch_points[e.TouchDevice.Id].points.Count - 1].Position.X - touch_points[e.TouchDevice.Id].points[0].Position.X;
                //bool should_tap = true;
                //if (is_horizontal && dh > configurations.tap_error)
                //    should_tap = false;
                //if (!is_horizontal && dv > configurations.tap_error)
                //    should_tap = false;

                //if (should_tap)
                //{
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
                                _list_select_item(selected_item, e);
                            }
                            catch (Exception e2) { log.WriteErrorLog(e2); }
                        }
                    }
                //}
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
                if (collection_list)
                    log.WriteInteractionLog(24, this.content_name, e.TouchDevice);
                else if (comment_list)
                    log.WriteInteractionLog(25, this.content_name, e.TouchDevice);
                else
                    log.WriteInteractionLog(1, this.content_name, e.TouchDevice);
            }
            if (touch_points[e.TouchDevice.Id].initial_attended_item != null)
                attend_on_item(false, touch_points[e.TouchDevice.Id].initial_attended_item);
            touch_points[e.TouchDevice.Id].points.Clear();
            touch_points.Remove(e.TouchDevice.Id);
            UIElement element2 = sender as UIElement;
            element2.ReleaseTouchCapture(e.TouchDevice);
        }

        private void attend_on_item(bool select, Control i)
        {
            if (i.GetType() != Type.GetType("nature_net.user_controls.collection_listbox_item")
                && i.GetType() != Type.GetType("nature_net.user_controls.item_generic_v2"))
                return;
            if (select)
            {
                if (i.GetType() != Type.GetType("nature_net.user_controls.collection_listbox_item"))
                    i.Background = Brushes.LightGray;
                else
                    i.Opacity = configurations.click_opacity_on_collection_item;
            }
            else
            {
                i.Background = Brushes.Transparent;
                i.Opacity = 1;
            }
        }

        private bool test_thumb_feedback(TouchEventArgs e)
        {
            TouchPoint pt = e.GetTouchPoint(this._list as IInputElement);
            hitResultsList.Clear();
            //HitTestResult hr = VisualTreeHelper.HitTest(this._list, new Point(pt.Position.X, pt.Position.Y));
            VisualTreeHelper.HitTest(this._list, null, new HitTestResultCallback(HitTestResult_Tap_v2), new PointHitTestParameters(new Point(pt.Position.X, pt.Position.Y)));
            try
            {
                Image i = (Image)hitResultsList[0];
                if (i.Name == "avatar")
                {
                    item_generic_v2 i2 =null;
                    try { i2 = (item_generic_v2)i.Tag; }
                    catch (Exception) { return false; }
                    if (populator.thumbs_up_handler != null)
                    {
                        populator.thumbs_up_handler(i2, e);
                        return true;
                    }
                }
                //if (i.Name == "img_dislike")
                //{
                //    item_generic i2 = (item_generic)i.Tag;
                //    if (populator.thumbs_down_handler != null)
                //        populator.thumbs_down_handler(i2, e);
                //    return true;
                //}
                return false;
            }
            catch (Exception e2) { return false; }
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
        public Thickness items_margins = new Thickness(0);
        public avatar_touch_down_handler avatar_drag;
        public list_header header;
        public thumbs_up thumbs_up_handler;
        public thumbs_down thumbs_down_handler;
        //
        public TextBlock total_number;
        public reply_clicked reply_clicked_handler;
        //
        public bool show_done = true;
        public bool show_not_done = true;
        //
        private readonly BackgroundWorker worker_design_ideas = new BackgroundWorker();
        private readonly BackgroundWorker worker_activities = new BackgroundWorker();
        private readonly BackgroundWorker worker_users = new BackgroundWorker();
        private readonly BackgroundWorker worker_comments = new BackgroundWorker();

        public list_populator()
        {
            worker_design_ideas.DoWork += new DoWorkEventHandler(get_all_design_ideas);
            worker_design_ideas.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_design_ideas);
            worker_activities.DoWork += new DoWorkEventHandler(get_all_activities);
            worker_activities.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_activities);
            worker_users.DoWork += new DoWorkEventHandler(get_all_users);
            worker_users.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_users);
            worker_comments.DoWork += new DoWorkEventHandler(get_all_comments);
            worker_comments.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_comments);
        }

        // for design ideas
        public void list_all_design_ideas()
        {
            if (!worker_design_ideas.IsBusy)
                worker_design_ideas.RunWorkerAsync(null);
        }
        public void get_all_design_ideas(object arg, DoWorkEventArgs e)
        {
            e.Result = (object)(new List<design_idea_item>());
            e.Result = get_all_design_ideas();
        }
        public void display_all_design_ideas(object di, RunWorkerCompletedEventArgs e)
        {
            this._list.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new System.Action(() =>
                {
                    display_all_design_ideas((List<design_idea_item>)e.Result);
                }));
        }
        public void list_all_design_ideas_sync()
        {
            List<design_idea_item> idea_items = get_all_design_ideas();
            display_all_design_ideas(idea_items);
        }
        public List<design_idea_item> get_all_design_ideas()
        {
            try
            {
                naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
                var r = from d in db.Design_Ideas
                        where (d.status != configurations.status_implemented || show_done) &&
                                (d.status == configurations.status_implemented || show_not_done) &&
                                (d.status != configurations.status_deleted)
                        orderby d.date descending
                        select d;
                if (r == null)
                {
                    return new List<design_idea_item>();
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
                    try
                    {
                        ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + d.avatar));
                        src.Freeze();
                        i.img = src;
                    }
                    catch (Exception) { i.img = null; }
                    i.design_idea = d;
                    i.count = num_comments;
                    i.last_date = last_time;
                    i.num_dislike = num_dislike;
                    i.num_like = num_like;

                    i.username = i.design_idea.name;
                    i.affiliation = i.design_idea.affiliation;
                    if (i.design_idea.name == "Default User")
                    {
                        i.username = configurations.default_user_text;
                        //i.affiliation = configurations.default_user_affiliation;
                        if (i.design_idea.web_username != null)
                        {
                            var webusers = from w in db.WebUsers
                                           where w.username == i.design_idea.web_username
                                           select w;
                            if (webusers.Count() == 1)
                            {
                                WebUser webuser = webusers.Single<WebUser>();
                                i.username = webuser.username + configurations.default_user_desc;
                                //i.affiliation = configurations.default_webuser_affiliation;
                                if (webuser.user_id.HasValue)
                                {
                                    var users = from u in db.Users
                                                where u.id == webuser.user_id.Value
                                                select u;
                                    if (users.Count() == 1)
                                    {
                                        User the_user = users.Single<User>();
                                        i.username = the_user.name + configurations.default_user_desc;
                                        try
                                        {
                                            ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + the_user.avatar));
                                            src.Freeze();
                                            i.img = src;
                                        }
                                        catch (Exception) { i.img = null; }
                                        i.affiliation = the_user.affiliation;
                                    }
                                }
                            }
                        }
                    }

                    ideas.Add(i);
                }
                return ideas;
            }
            catch (Exception ex)
            {
                log.WriteErrorLog(ex);
                return new List<design_idea_item>();
            }
        }
        public void display_all_design_ideas(List<design_idea_item> ideas)
        {
            this._list.Items.Clear();
            if (initial_item != null)
                this._list.Items.Add(initial_item);

            foreach (design_idea_item idea in ideas)
            {
                item_generic_v2 i = new item_generic_v2();
                i.Background = Brushes.White;
                i.title.Text = idea.design_idea.note; i.description.Visibility = Visibility.Collapsed;
                TextBlock.SetFontWeight(i.title, FontWeights.Normal);
                i.title.FontSize = configurations.design_idea_item_title_font_size;
                i.user_info.Margin = new Thickness(5);
                if (idea.img == null)
                    i.user_info_icon.Visibility = Visibility.Collapsed;
                else
                    i.user_info_icon.Source = idea.img;
                i.user_info_name.Text = idea.username;
                //i.user_info_date.Text = configurations.GetDate_Formatted(idea.last_date);
                i.user_info_date.Text = configurations.GetDate_Formatted(idea.design_idea.date);
                i.user_info_name.Margin = new Thickness(2, 0, 0, 0); i.user_info_date.Margin = new Thickness(2, 0, 2, 0);
                i.user_info_name.FontSize = configurations.design_idea_item_user_info_font_size; i.user_info_date.FontSize = configurations.design_idea_item_user_info_font_size;
                i.number.Text = idea.count.ToString(); i.number_icon.Visibility = Visibility.Collapsed;
                i.txt_level1.Text = configurations.designidea_num_desc;
                i.txt_level2.Visibility = Visibility.Collapsed; i.txt_level3.Visibility = Visibility.Collapsed;
                i.avatar.Source = configurations.img_thumbs_up_icon; i.num_likes.Content = idea.num_like.ToString();
                i.avatar.Width = configurations.design_idea_item_avatar_width; i.avatar.Height = configurations.design_idea_item_avatar_width; i.avatar.Margin = new Thickness(5,5,5,0); i.avatar.Tag = i;
                i.Tag = idea.design_idea.id;
                i.Margin = items_margins;
                if (item_width != 0) i.Width = item_width;
                if (idea.affiliation != null && idea.affiliation.ToLower() == configurations.affiliation_aces.ToLower())
                {
                    i.affiliation_icon_small.Source = configurations.img_affiliation_icon;
                    i.affiliation_icon_small.Visibility = Visibility.Visible;
                }
                if (idea.design_idea.status != null && idea.design_idea.status.ToLower() == configurations.status_implemented.ToLower())
                {
                    //i.pre_title.Text = configurations.implemented_text;
                    //i.pre_title.FontWeight = FontWeights.Bold;
                    i.affiliation_icon.Height = 15;
                    i.affiliation_icon.Source = configurations.img_implemented_icon;
                    i.affiliation_icon.Visibility = Visibility.Visible;
                    if (item_width != 0) i.title.MaxWidth = configurations.idea_text_scale_factor * item_width;
                }
                i.right_panel.Width = configurations.design_idea_right_panel_width;
                //i.left_panel.VerticalAlignment = VerticalAlignment.Center; DockPanel.SetDock(i.number, Dock.Left); DockPanel.SetDock(i.txt_level1, Dock.Left);
                i.top_value = idea.num_like;
                i.drag_icon_vertical.Source = configurations.img_drag_vertical_icon;
                if (configurations.show_vertical_drag) i.drag_icon_vertical_panel.Visibility = Visibility.Visible;
                if (thumbs_up_handler != null)
                    i.avatar.Tag = i;
                this._list.Items.Add(i);
            }
            if (header.atoz.IsChecked.Value && header.atoz_order != null) header.atoz_order();
            if (header.top.IsChecked.Value && header.top_order != null) header.top_order();
            if (header.recent.IsChecked.Value && header.recent_order != null) header.recent_order();
            this._list.Items.Refresh();
            this._list.UpdateLayout();
        }

        // for users
        public void list_all_users()
        {
            if (!worker_users.IsBusy)
                worker_users.RunWorkerAsync(null);
        }
        public void get_all_users(object arg, DoWorkEventArgs e)
        {
            e.Result = (object)(new List<user_item>());
            e.Result = get_all_users();
        }
        public void display_all_users(object us, RunWorkerCompletedEventArgs e)
        {
            this._list.Items.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new System.Action(() =>
                {
                    display_all_users((List<user_item>)e.Result);
                }));
        }
        public void list_all_users_sync()
        {
            List<user_item> user_items = get_all_users();
            display_all_users(user_items);
        }
        public List<user_item> get_all_users()
        {
            try
            {
                naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
                var r = from u in db.Users
                        where u.id != 0
                        orderby u.name
                        select u;
                if (r == null)
                {
                    return new List<user_item>();
                }
                List<user_item> users = new List<user_item>();
                foreach (User u in r)
                {
                    var n1 = from m in db.Collection_Contribution_Mappings
                             where m.Collection.user_id == u.id
                             && (m.Contribution.status != configurations.status_deleted)
                             orderby m.Contribution.modified_date descending
                             select m.Contribution.modified_date;
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
                            if (n1.First().HasValue)
                            {
                                i.last_date = n1.First().Value;
                                i.has_date = true;
                            }
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
                return users;
            }
            catch (Exception ex)
            {
                log.WriteErrorLog(ex);
                return new List<user_item>();
            }
        }
        public void display_all_users(List<user_item> users)
        {
            this._list.Items.Clear();
            if (initial_item != null)
                this._list.Items.Add(initial_item);

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
                if (u.user.affiliation != null && u.user.affiliation.ToLower() == configurations.affiliation_aces.ToLower())
                {
                    i.affiliation_icon.Source = configurations.img_affiliation_icon;
                    i.affiliation_icon.Visibility = Visibility.Visible;
                }
                i.drag_icon_vertical.Source = configurations.img_drag_vertical_icon;
                if (configurations.show_vertical_drag) i.drag_icon_vertical_panel.Visibility = Visibility.Visible;
                this._list.Items.Add(i);
            }
            if (header.atoz.IsChecked.Value && header.atoz_order != null) header.atoz_order();
            if (header.top.IsChecked.Value && header.top_order != null) header.top_order();
            if (header.recent.IsChecked.Value && header.recent_order != null) header.recent_order();
            this._list.Items.Refresh();
            this._list.UpdateLayout();
        }

        // for comments
        public void list_all_comments(comment_item item)
        {
            if (!worker_comments.IsBusy)
                worker_comments.RunWorkerAsync(item);
        }
        public void get_all_comments(object arg, DoWorkEventArgs e)
        {
            if (e.Argument == null) return;
            e.Result = (object)(new List<comment_item_generic>());
            try
            {
                comment_item item = (comment_item)e.Argument;
                naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
                var r = from c in db.Feedbacks
                        where (c.Feedback_Type.name == "Comment") && (c.object_type == item._object_type.ToString())
                        && (c.object_id == item._object_id) && (c.parent_id == 0)
                        orderby c.date descending
                        select c;
                if (r != null)
                {
                    List<Feedback> comments = r.ToList<Feedback>();
                    List<comment_item_generic> comment_items = new List<comment_item_generic>();
                    List<List<comment_item_generic>> children_items = new List<List<comment_item_generic>>();
                    for (int counter=0;counter<comments.Count; counter++)
                    {
                        Feedback f = comments[counter];
                        int level = 0;
                        List<comment_item_generic> tmp_list = new List<comment_item_generic>();
                        tmp_list.Add(create_comment_item(f, level, db));
                        add_comment_items_for(f, ref level, tmp_list, db);
                        add_children_to_list(tmp_list, children_items);
                    }
                    condense_lists(children_items, comment_items);
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
        private void add_children_to_list(List<comment_item_generic> tmp, List<List<comment_item_generic>> main_list)
        {
            // search through the main_list to find a place for insertion (main list should be kept sorted)
            DateTime max_date_tmp = tmp[0].comment.date;
            for (int t = 0; t < tmp.Count; t++)
                if (tmp[t].comment.date > max_date_tmp)
                    max_date_tmp = tmp[t].comment.date;
            for (int counter = 0; counter < main_list.Count; counter++)
            {
                List<comment_item_generic> candidate = main_list[counter];
                DateTime max_date_candidate = candidate[0].comment.date;
                for (int i = 0; i < candidate.Count; i++)
                    if (candidate[i].comment.date > max_date_candidate)
                        max_date_candidate = candidate[i].comment.date;
                if (max_date_tmp > max_date_candidate)
                {
                    main_list.Insert(counter, tmp);
                    return;
                }
            }
            main_list.Add(tmp);
        }
        private void add_comment_items_for(Feedback c, ref int level, List<comment_item_generic> current_items, naturenet_dataclassDataContext db)
        {
            var r = from c2 in db.Feedbacks
                    where (c2.Feedback_Type.name == "Comment") && (c2.parent_id == c.id)
                    orderby c2.date descending
                    select c2;
            if (r != null)
            {
                List<Feedback> comments = r.ToList<Feedback>();
                level++;
                List<List<comment_item_generic>> children_items = new List<List<comment_item_generic>>();
                foreach (Feedback f in comments)
                {
                    List<comment_item_generic> tmp_list = new List<comment_item_generic>();
                    int new_level = level;
                    tmp_list.Add(create_comment_item(f, new_level, db));
                    add_comment_items_for(f, ref new_level, tmp_list, db);
                    add_children_to_list(tmp_list, children_items);
                }
                condense_lists(children_items, current_items);
            }
        }
        private void condense_lists(List<List<comment_item_generic>> main_list, List<comment_item_generic> return_list)
        {
            for (int counter1 = 0; counter1 < main_list.Count; counter1++)
                for (int counter2 = 0; counter2 < main_list[counter1].Count; counter2++)
                    return_list.Add(main_list[counter1][counter2]);
        }
        public comment_item_generic create_comment_item(Feedback c, int level, naturenet_dataclassDataContext db)
        {
            comment_item_generic cig = new comment_item_generic();
            cig.comment = c; cig.level = level;
            cig.username = cig.comment.User.name;
            cig.avatar = cig.comment.User.avatar;
            cig.affiliation = cig.comment.User.affiliation;
            if (cig.comment.User.name == "Default User")
            {
                cig.username = configurations.default_user_text;
                //cig.avatar = configurations.default_user_avatar;
                //cig.affiliation = configurations.default_user_affiliation;
                if (cig.comment.web_username != null)
                {
                    var webusers = from w in db.WebUsers
                                   where w.username == cig.comment.web_username
                                   select w;
                    if (webusers.Count() == 1)
                    {
                        WebUser webuser = webusers.Single<WebUser>();
                        cig.username = webuser.username + configurations.default_user_desc;
                        //cig.avatar = configurations.default_webuser_avatar;
                        //cig.affiliation = configurations.default_webuser_affiliation;
                        if (webuser.user_id.HasValue)
                        {
                            var users = from u in db.Users
                                        where u.id == webuser.user_id.Value
                                        select u;
                            if (users.Count() == 1)
                            {
                                User the_user =users.Single<User>(); 
                                cig.username = the_user.name + configurations.default_user_desc;
                                cig.avatar = the_user.avatar;
                                cig.affiliation = the_user.affiliation;
                            }
                        }
                    }
                }
            }
                
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
                   if (this.total_number != null)
                       this.total_number.Text = comments.Count.ToString();
                   foreach (comment_item_generic cig in comments)
                   {
                       item_generic i = new item_generic();
                       i.username.Text = "";
                       i.username.Inlines.Add(new Bold(new Run(cig.username + ": ")));
                       i.username.Inlines.Add(cig.comment.note);
                       i.user_desc.Visibility = Visibility.Collapsed; //i.user_desc.Content = configurations.GetDate_Formatted(cig.comment.date);
                       i.number.Text = configurations.GetDate_Formatted(cig.comment.date); //i.number.Visibility = System.Windows.Visibility.Collapsed;
                       i.number.FontSize = configurations.design_idea_item_user_info_font_size;
                       i.desc.Visibility = Visibility.Collapsed;// i.desc.Content = "Commented:";
                       i.topleft_panel.VerticalAlignment = VerticalAlignment.Top;
                       i.top_panel.Margin = new Thickness(5, 10, 5, 10);
                       //i.content.Text = cig.comment.note;
                       i.content.Visibility = Visibility.Collapsed;
                       if (item_width != 0) i.Width = item_width + 2;
                       try { i.avatar.Source = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + cig.avatar)); }
                       catch (Exception) { i.avatar.Visibility = Visibility.Collapsed; }
                       i.Tag = cig.comment.id;
                       if (cig.affiliation != null && cig.affiliation.ToLower() == configurations.affiliation_aces.ToLower())
                       {
                           i.affiliation_icon_small.Source = configurations.img_affiliation_icon;
                           i.affiliation_icon_small.Visibility = Visibility.Visible;
                       }
                       i.avatar.VerticalAlignment = VerticalAlignment.Top;
                       i.affiliation_icon_small.VerticalAlignment = VerticalAlignment.Top;
                       if (configurations.use_avatar_drag) i.set_touchevent(this.avatar_drag);
                       i.Margin = new Thickness(0);
                       i.second_border.Margin = new Thickness(cig.level * 25, 0, 0, 0);
                       i.first_border.BorderBrush = Brushes.Gray; i.first_border.BorderThickness = new Thickness(0, 0, 0, 1);
                       i.second_border.BorderBrush = Brushes.DarkGray; i.second_border.BorderThickness = new Thickness(1, 0, 0, 0);
                       if (this.reply_clicked_handler != null && cig.level < (configurations.max_thread_reply - 1)) i.set_replybutton(this.reply_clicked_handler);
                       this._list.Items.Add(i);
                   }

                   if (this._list.Items.Count == 0)
                       this._list.Height = 0;
                   else
                       this._list.Height = Double.NaN;

                   this._list.Items.Refresh();
                   this._list.Padding = new Thickness(0);
                   this._list.UpdateLayout();
               }));
        }

        // for activities
        public void list_all_activities()
        {
            if (!worker_activities.IsBusy)
                worker_activities.RunWorkerAsync();
        }
        public void get_all_activities(object arg, DoWorkEventArgs e)
        {
            e.Result = (object)(new List<activity_item>());
            try
            {
                naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
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
                                 && (m.Contribution.status != configurations.status_deleted)
                                 orderby m.Contribution.date descending
                                 select new { m.Contribution.date, m.Collection.User.name };
                        int cnt = 0;
                        if (n1 != null)
                            cnt = n1.Count();

                        activity_item ai = new activity_item();
                        ai.activity = a;
                        ai.count = cnt;
                        if (cnt != 0)
                            ai.username = n1.First().name;
                        else
                            ai.username = "";
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
                   display_all_activities((List<activity_item>)e.Result);
               }));
        }
        public void display_all_activities(List<activity_item> activities)
        {
            this._list.Items.Clear();
            if (initial_item != null)
                this._list.Items.Add(initial_item);

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
                i.drag_icon_vertical.Source = configurations.img_drag_vertical_icon;
                if (configurations.show_vertical_drag) i.drag_icon_vertical_panel.Visibility = Visibility.Visible;
                this._list.Items.Add(i);
            }
            if (header.atoz.IsChecked.Value && header.atoz_order != null) header.atoz_order();
            if (header.top.IsChecked.Value && header.top_order != null) header.top_order();
            if (header.recent.IsChecked.Value && header.recent_order != null) header.recent_order();
            this._list.Items.Refresh();
            this._list.UpdateLayout();
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
        public string username;
        public string affiliation;
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
        public string username;
        public string avatar;
        public string affiliation;
    }

    public class touch_info
    {
        public int id;
        public List<System.Windows.Input.TouchPoint> points = new List<TouchPoint>();
        public bool is_tap = false;
        public bool is_drag = false;
        public int consecutive_drag_points;
        public Control initial_attended_item;
    }

    public delegate bool ItemSelected(object i, TouchEventArgs e);
}
