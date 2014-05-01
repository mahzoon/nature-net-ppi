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
using Microsoft.Surface.Presentation;

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for window_content.xaml
    /// </summary>
    public partial class window_content : UserControl, IVirtualKeyboardInjectable
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private int _object_id;
        private Type _object_type;

        private int comment_user_id;
        bool expand_state = false;

        bool hide_expander = false;

        virtual_keyboard keyboard;
        ContentControl keyboard_frame;
        UserControl parent;

        public window_content()
        {
            InitializeComponent();

            this.submit_comment.Content = "Submit";
            //this.submit_comment.Click += new RoutedEventHandler(submit_comment_Click);
            this.submit_comment.PreviewTouchDown += new EventHandler<TouchEventArgs>(submit_comment_Click);
            this.leave_comment_panel.Visibility = System.Windows.Visibility.Collapsed;

            this.leave_comment_area.AllowDrop = true;
            SurfaceDragDrop.AddPreviewDropHandler(this.leave_comment_area, new EventHandler<SurfaceDragDropEventArgs>(item_droped_on_leave_comment_area));
            if (configurations.response_to_mouse_clicks)
                this.expander.Click += new RoutedEventHandler(expander_Click);
            this.expander.PreviewTouchDown += new EventHandler<TouchEventArgs>(expander_Click);
            
            this.comment_textbox.GotFocus += new RoutedEventHandler(comment_textbox_GotKeyboardFocus);
            //this.comment_textbox.LostFocus += new RoutedEventHandler(comment_textbox_LostKeyboardFocus);
            this.Unloaded += new RoutedEventHandler(window_content_Unloaded);
            this.Loaded += new RoutedEventHandler(window_content_Loaded);

			this.add_comment_img.Source = configurations.img_drop_avatar_pic;
            //this.leave_comment_canvas.PreviewTouchUp += new EventHandler<TouchEventArgs>(leave_comment_canvas_PreviewTouchUp);
            this.comments_listbox.initialize(false, "comment", new ItemSelected(this.item_selected));
            this.comments_listbox.Background = Brushes.White;
            this.comments_listbox.selectable = true;
            this.comments_listbox.comment_list = true;
        }

        void leave_comment_canvas_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            //do stuff with the image of strokes
            //this.leave_comment_canvas.Strokes.Clear();
        }

        void window_content_Loaded(object sender, RoutedEventArgs e)
        {
            if (hide_expander)
                this.expander.Visibility = System.Windows.Visibility.Collapsed;

            if (expand_state)
            {
                if (!hide_expander)
                    this.comments_listbox.Visibility = System.Windows.Visibility.Visible;
                else
                    this.comments_listbox.Visibility = System.Windows.Visibility.Collapsed;
                this.leave_comment_area.Visibility = System.Windows.Visibility.Visible;
                this.comments_listbox.UpdateLayout();
                this.expander.Content = "^";
                //this.leave_comment_canvas.Strokes.Clear();
            }
            else
            {
                this.comments_listbox.Visibility = System.Windows.Visibility.Collapsed;
                this.leave_comment_area.Visibility = System.Windows.Visibility.Collapsed;
                this.comments_listbox.UpdateLayout();
                this.expander.Content = "v";
            }
        }

        void window_content_Unloaded(object sender, RoutedEventArgs e)
        {
            if (keyboard_frame != null)
                window_manager.main_canvas.Children.Remove(keyboard_frame);
        }

        void comment_textbox_LostKeyboardFocus(object sender, RoutedEventArgs e)
        {
            keyboard_frame.Visibility = System.Windows.Visibility.Collapsed;
        }

        void comment_textbox_GotKeyboardFocus(object sender, RoutedEventArgs e)
        {
            if (keyboard_frame == null)
                keyboard_frame = new ContentControl();
            virtual_keyboard.ShowKeyboard(this, ref keyboard);
            keyboard_frame.Visibility = System.Windows.Visibility.Visible;
            if (keyboard != null)
            {
                if (this.keyboard_frame.Content == null)
                {
                    this.keyboard_frame.Content = keyboard;
                    //this.keyboard.Background = new SolidColorBrush(Colors.White);
                    this.keyboard_frame.Background = new SolidColorBrush(Colors.White);
                    window_manager.main_canvas.Children.Add(keyboard_frame);
                }
                keyboard.MoveAlongWith(parent);
            }
        }

        void expander_Click(object sender, RoutedEventArgs e)
        {
            expand_state = !expand_state;
            if (expand_state)
            {
                this.comments_listbox.Visibility = System.Windows.Visibility.Visible;
                this.leave_comment_area.Visibility = System.Windows.Visibility.Visible;
                this.comments_listbox.UpdateLayout();
                this.expander.Content = "^";
            }
            else
            {
                this.comments_listbox.Visibility = System.Windows.Visibility.Collapsed;
                this.leave_comment_area.Visibility = System.Windows.Visibility.Collapsed;
                this.comments_listbox.UpdateLayout();
                this.expander.Content = "v";
                //this.leave_comment_canvas.Strokes.Clear();
                if (keyboard_frame != null) keyboard_frame.Visibility = System.Windows.Visibility.Collapsed;

                this.leave_comment_panel.Visibility = System.Windows.Visibility.Collapsed;
                this.add_comment_img.Visibility = System.Windows.Visibility.Visible;
                //this.leave_comment_canvas.Visibility = System.Windows.Visibility.Visible;
                
                this.comment_user_id = -1;
                //this.avatar.Source = new BitmapImage(new Uri(data[3]));
                //this.comment_textbox.Focus();
            }
        }

        void submit_comment_Click(object sender, RoutedEventArgs e)
        {
            if (this.comment_textbox.Text == "") return;
            bool is_design_idea = hide_expander;
            if (is_design_idea)
            {
                naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
                Contribution idea = new Contribution();
                idea.date = DateTime.Now;
                idea.location_id = 0;
                idea.note = this.comment_textbox.Text;
                db.Contributions.InsertOnSubmit(idea);
                db.SubmitChanges();
                int collection_id = configurations.get_or_create_collection(db, this.comment_user_id, 1, DateTime.Now);
                Collection_Contribution_Mapping map = new Collection_Contribution_Mapping();
                map.collection_id = collection_id;
                map.contribution_id = idea.id;
                map.date = DateTime.Now;
                db.Collection_Contribution_Mappings.InsertOnSubmit(map);
                db.SubmitChanges();

                if (the_item.Content != null)
                    ((design_ideas_listbox)the_item.Content).list_all_design_ideas();
                window_manager.load_design_ideas();
                //if (((design_ideas_listbox)the_item.Content).parent != null)
                //    ((design_ideas_listbox)the_item.Content).parent.list_all_design_ideas();
            }
            else
            {
                Feedback comment = new Feedback();
                comment.date = DateTime.Now;
                comment.note = this.comment_textbox.Text;
                comment.object_id = this._object_id;
                comment.object_type = this._object_type.ToString();
                if (comments_listbox._list.SelectedIndex == -1)
                    comment.parent_id = 0;
                else
                {
                    int p_id = (int)((item_generic)comments_listbox._list.Items[comments_listbox._list.SelectedIndex]).Tag;
                    comment.parent_id = p_id;
                }
                comment.technical_info = "";
                comment.type_id = 1;
                comment.user_id = this.comment_user_id;
                naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
                db.Feedbacks.InsertOnSubmit(comment);
                db.SubmitChanges();
                if (this.the_item.Content != null)
                {
                    try
                    {
                        item_generic_v2 v2 = (item_generic_v2)this.the_item.Content;
                        v2.number.Text = (Convert.ToInt32(v2.number.Text) + 1).ToString();
                    }
                    catch (Exception) { }
                }
                this.list_all_comments();
                if (this._object_type.ToString() == "nature_net.Contribution")
                    window_manager.load_design_ideas();
            }
            this.comment_textbox.SelectAll();
        }

        void item_droped_on_leave_comment_area(object sender, SurfaceDragDropEventArgs e)
        {
            string[] data = ((string)e.Cursor.Data).Split(new Char[] { ';' });
            if (data == null) return;
            if (data.Count() < 4) return;
            string context = data[0];
            if (context == "user")
            {
                string username = data[3];
                int user_id = Convert.ToInt32(data[1]);

                this.add_comment_img.Visibility = System.Windows.Visibility.Collapsed;
                //this.leave_comment_canvas.Visibility = System.Windows.Visibility.Collapsed;
                
                this.leave_comment_panel.Visibility = System.Windows.Visibility.Visible;
                this.comment_user_id = user_id;
                this.avatar.Source = new BitmapImage(new Uri(data[2]));
                this.comment_textbox.Text = "";
                this.comment_textbox.Focus();
            }
            e.Handled = true;
        }

        public void initialize_contents(UserControl uc, Type obj_type, int obj_id, UserControl parent_frame)
        {
            this.the_item.Content = uc;
            this._object_id = obj_id;
            this._object_type = obj_type;
            this.comments_listbox._list.Width = parent_frame.Width;
            this.list_all_comments();
            
            //this.add_comment_img.Source = configurations.img_drop_avatar_pic;
            //var brush = new ImageBrush();
            //brush.ImageSource = configurations.img_drop_avatar_pic;
            //brush.Stretch = Stretch.None;
            //this.leave_comment_canvas.Background = brush;

            this.parent = parent_frame;
        }

        public void initialize_contents(UserControl uc, Type obj_type, int obj_id, UserControl parent_frame, double width)
        {
            this.the_item.Content = uc;
            this._object_id = obj_id;
            this._object_type = obj_type;
            this.comments_listbox._list.Width = width;
            this.list_all_comments();

            //this.add_comment_img.Source = configurations.img_drop_avatar_pic;
            //var brush = new ImageBrush();
            //brush.ImageSource = configurations.img_drop_avatar_pic;
            //brush.Stretch = Stretch.None;
            //this.leave_comment_canvas.Background = brush;

            this.parent = parent_frame;
        }

        public void initialize_contents(UserControl uc, bool is_design, UserControl parent_frame)
        {
            //this.the_item.Content = uc;
            this.hide_expander = is_design;
            
            //this.add_comment_img.Source = configurations.img_drop_avatar_pic;
            //var brush = new ImageBrush();
            //brush.ImageSource = configurations.img_drop_avatar_pic;
            //brush.Stretch = Stretch.None;
            //this.leave_comment_canvas.Background = brush;

            //((design_ideas_listbox)the_item.Content).list_all_design_ideas();
            //((design_ideas_listbox)the_item.Content).desc.Visibility = System.Windows.Visibility.Collapsed;
            //((design_ideas_listbox)the_item.Content).submit_button.Visibility = System.Windows.Visibility.Collapsed;
            //((design_ideas_listbox)the_item.Content).Height = configurations.design_idea_ext_window_width;
            //((design_ideas_listbox)the_item.Content).Background = new SolidColorBrush(Colors.White);
            this.expand_state = true;
            this.parent = parent_frame;
        }

        public void initialize_contents(UserControl uc)
        {
            this.expand_state = false;
            this.hide_expander = true;
            this.the_item.Content = uc;
        }

        // add an initial comment (that's not actually a comment; it is used to show a contribution's user provided attributes)
        public void initialize_comments(Contribution the_contribution)
        {
            string comment = "";
            if (the_contribution.note != null && the_contribution.note != "")
                comment = the_contribution.note + "\r\n";
            comment += configurations.contribution_comment_date;
            comment += the_contribution.date.ToString();
            comment += "\r\n";
            if (the_contribution.tags != "")
                comment = comment + configurations.contribution_comment_tag + the_contribution.tags + "\r\n";
            if (the_contribution.location_id != 0)
                comment = comment + configurations.contribution_comment_location + the_contribution.location_id.ToString() + ": " + the_contribution.Location.name;

            item_generic i = new item_generic();
            i.desc.Visibility = System.Windows.Visibility.Collapsed;
            i.top_panel.Visibility = System.Windows.Visibility.Collapsed;
            i.content.Text = comment;
            i.BorderBrush = Brushes.Gray;
            i.BorderThickness = new Thickness(0, 0, 0, 1);
            i.Margin = new Thickness(5, 7, 5, 0);

            this.comments_listbox.populator.initial_item = i;
        }

        public void list_all_comments()
        {
            comment_item i = new comment_item();
            i._object_id = this._object_id; i._object_type = this._object_type;
            this.comments_listbox.populator.item_width = this.comments_listbox.Width;
            this.comments_listbox.populator.list_all_comments(i);
        }

        bool item_selected(object i)
        {
            return false;
        }

        public void UpdateKeyboardPosition()
        {
            if (keyboard_frame != null)
            {
                if (keyboard != null)
                {
                    if (keyboard_frame.Visibility == System.Windows.Visibility.Visible)
                    {
                        keyboard.MoveAlongWith(parent);
                    }
                }
            }
        }

        public UIElement GetKeyboardFrame()
        {
            return keyboard_frame;
        }

        public Control ControlToInjectInto
        {
            get { return this.comment_textbox; }
        }
    }
}
