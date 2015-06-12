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

        private int activity_id = -1;
        private int comment_user_id = -1;
        
        bool expand_state = true;

        bool is_submit_design_idea = false;
        public bool is_idea_collection = false;

        virtual_keyboard keyboard;
        ContentControl keyboard_frame;
        UserControl parent;

        bool is_reply = false;
        int reply_id = 0;

        System.Threading.Timer error_timer;
        
        public bool center_keyboard = true;

        private bool should_show_metadata3 = true;
        private bool should_show_metadata4 = true;

        public window_content()
        {
            InitializeComponent();

            this.comment_icon.Source = configurations.img_comment_icon;
            this.leave_comment_area_default.Visibility = System.Windows.Visibility.Visible;
            this.leave_comment_area_activity.Visibility = System.Windows.Visibility.Collapsed;
            this.leave_comment_area_reply.Visibility = System.Windows.Visibility.Collapsed;
            this.leave_comment_area_auth.Visibility = System.Windows.Visibility.Collapsed;

            this.submit_comment_default.PreviewTouchDown += new EventHandler<TouchEventArgs>(submit_comment_default_clicked);
            this.submit_activity.PreviewTouchDown += new EventHandler<TouchEventArgs>(submit_activity_PreviewTouchDown);
            this.cancel_activity.PreviewTouchDown += new EventHandler<TouchEventArgs>(cancel_activity_PreviewTouchDown);
            this.submit_comment_reply.PreviewTouchDown += new EventHandler<TouchEventArgs>(submit_comment_reply_clicked);
            this.cancel_comment_reply.PreviewTouchDown += new EventHandler<TouchEventArgs>(cancel_comment_reply_clicked);
            this.submit_comment_auth.PreviewTouchDown += new EventHandler<TouchEventArgs>(submit_comment_auth_clicked);
            this.cancel_comment_auth.PreviewTouchDown += new EventHandler<TouchEventArgs>(cancel_comment_auth_clicked);

            this.selected_user.AllowDrop = true;
            this.selected_activity.AllowDrop = true;
            SurfaceDragDrop.AddPreviewDropHandler(this.selected_user, new EventHandler<SurfaceDragDropEventArgs>(item_dropped_on_leave_comment_area_auth));
            SurfaceDragDrop.AddPreviewDropHandler(this.selected_activity, new EventHandler<SurfaceDragDropEventArgs>(item_dropped_on_leave_comment_area_activity));

            this.expander.PreviewTouchDown += new EventHandler<TouchEventArgs>(expander_Click);
            
            this.comment_textbox_default.GotFocus += new RoutedEventHandler(comment_textbox_GotKeyboardFocus);
            this.comment_textbox_reply.GotFocus += new RoutedEventHandler(comment_textbox_GotKeyboardFocus);

            if (!configurations.multi_keyboard)
            {
                comment_textbox_default.LostFocus += new RoutedEventHandler(textbox_LostFocus);
                comment_textbox_reply.LostFocus += new RoutedEventHandler(textbox_LostFocus);
                pin.LostFocus += new RoutedEventHandler(pin_LostFocus);
            }

            this.Unloaded += new RoutedEventHandler(window_content_Unloaded);
            this.Loaded += new RoutedEventHandler(window_content_Loaded);

			//this.add_comment_img.Source = configurations.img_drop_avatar_pic;
            
            this.comments_listbox.initialize(false, "comment", new ItemSelected(this.item_selected));
            this.comments_listbox.Background = Brushes.White;

            this.comments_listbox.selectable = false;
            this.comments_listbox.comment_list = true;

            keyboard_frame = new ContentControl();
            pin.center = center_keyboard;

            this.comment_textbox_default.SizeChanged += new SizeChangedEventHandler(comment_textbox_SizeChanged);
            this.comment_textbox_reply.SizeChanged += new SizeChangedEventHandler(comment_textbox_SizeChanged);
            this.comments_listbox.SizeChanged += new SizeChangedEventHandler(comment_textbox_SizeChanged);
        }

        void comment_textbox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (keyboard != null)
                keyboard.MoveAlongWith(parent, center_keyboard);
        }

        void window_content_Loaded(object sender, RoutedEventArgs e)
        {
            ToggleCommentsSection();
        }

        void window_content_Unloaded(object sender, RoutedEventArgs e)
        {
            if (keyboard_frame != null)
                window_manager.main_canvas.Children.Remove(keyboard_frame);
        }

        void pin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pin.numpad_frame != null)
                pin.numpad_frame.Visibility = System.Windows.Visibility.Collapsed;
        }

        void textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!comment_textbox_reply.IsFocused && !comment_textbox_default.IsFocused)// && !textbox_password.IsFocused)
                keyboard_frame.Visibility = System.Windows.Visibility.Collapsed;
        }

        protected override void OnManipulationBoundaryFeedback(ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        void comment_textbox_GotKeyboardFocus(object sender, RoutedEventArgs e)
        {
            virtual_keyboard.ShowKeyboard(this, ref keyboard);
            keyboard.parent_frame = keyboard_frame;
            if (keyboard.init_text_checker == null) keyboard.init_text_checker = new InitialTextCheck(this.CheckInitialText);
            keyboard_frame.Visibility = System.Windows.Visibility.Visible;
            CheckInitialText();
            if (keyboard != null)
            {
                if (this.keyboard_frame.Content == null)
                {
                    this.keyboard_frame.Content = keyboard;
                    //this.keyboard.Background = new SolidColorBrush(Colors.White);
                    this.keyboard_frame.Background = new SolidColorBrush(Colors.White);
                    this.keyboard.window_frame = this.parent;
                    window_manager.main_canvas.Children.Add(keyboard_frame);
                }
                keyboard.MoveAlongWith(parent, center_keyboard);
            }
        }

        void expander_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (expand_state) log.WriteInteractionLog(23, "object_type: " + _object_type + "; object_id: " + _object_id.ToString(), ((TouchEventArgs)e).TouchDevice);
                else log.WriteInteractionLog(22, "object_type: " + _object_type + "; object_id: " + _object_id.ToString(), ((TouchEventArgs)e).TouchDevice);
            }
            catch (Exception) { } // e might not be Touch
            ToggleCommentsSection();
        }

        void submit_comment_default_clicked(object sender, RoutedEventArgs e)
        {
            if (is_submit_design_idea) // design idea
            {
                log.WriteInteractionLog(28, "Idea; Text: " + this.GetActiveTextBox().Text, ((TouchEventArgs)e).TouchDevice);
                GoToSelectActMode();
            }
            else // comment
            {
                log.WriteInteractionLog(27, "Comment; Text: " + this.GetActiveTextBox().Text + "; object_type: " + _object_type + "; object_id: " + _object_id.ToString(), ((TouchEventArgs)e).TouchDevice);
                GotoAuthMode();
            }
        }

        void submit_activity_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            GotoAuthMode();
        }

        void cancel_activity_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            this.submit_activity.IsEnabled = false;
            if (activity_id != -1)
            {
                activity_id = -1;
                GoToSelectActMode();
            }
            else
                GotoDefaultMode();
        }

        void submit_comment_reply_clicked(object sender, RoutedEventArgs e)
        {
            log.WriteInteractionLog(27, "Reply id: " + reply_id + "; Text: " + this.GetActiveTextBox().Text + "; object_type: " + _object_type + "; object_id: " + _object_id.ToString(), ((TouchEventArgs)e).TouchDevice);
            GotoAuthMode();
        }

        void cancel_comment_reply_clicked(object sender, RoutedEventArgs e)
        {
            log.WriteInteractionLog(39, "Reply id: " + reply_id + "; Text: " + this.GetActiveTextBox().Text + "; object_type: " + _object_type + "; object_id: " + _object_id.ToString(), ((TouchEventArgs)e).TouchDevice);
            GotoDefaultMode();
        }

        void submit_comment_auth_clicked(object sender, RoutedEventArgs e)
        {
            if (this.GetActiveTextBox().Text == "")
            {
                this.error_desc.Visibility = System.Windows.Visibility.Visible;
                this.error_desc.Content = "Comment text is empty.";
                if (is_reply)
                    log.WriteInteractionLog(41, (is_submit_design_idea?"Idea":"Comment") + "; Reply id: " + reply_id + "; Text is empty; object_type: " + _object_type + "; object_id: " + _object_id.ToString(), ((TouchEventArgs)e).TouchDevice);
                else
                    log.WriteInteractionLog(41, (is_submit_design_idea?"Idea":"Comment") + "; Text is empty; object_type: " + _object_type + "; object_id: " + _object_id.ToString(), ((TouchEventArgs)e).TouchDevice);
                return;
            }
            // authenticate
            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
            var auth_user = from u in db.Users
                            where (u.name == this.selected_user.title.Text) && (u.password == this.pin.pin_string)
                            select u;
            if (auth_user.Count() == 1)
            {
                if (is_reply)
                    log.WriteInteractionLog(31, (is_submit_design_idea ? "Idea" : "Comment") + "; Reply id: " + reply_id + "; Text: " + this.GetActiveTextBox().Text + "; object_type: " + _object_type + "; object_id: " + _object_id.ToString() + "; Username=" + this.selected_user.title.Text, ((TouchEventArgs)e).TouchDevice);
                else
                    log.WriteInteractionLog(31, (is_submit_design_idea ? "Idea" : "Comment") + "; Text: " + this.GetActiveTextBox().Text + "; object_type: " + _object_type + "; object_id: " + _object_id.ToString() + "; Username=" + this.selected_user.title.Text, ((TouchEventArgs)e).TouchDevice);
                submit_text(e);
            }
            else
            {
                if (is_reply)
                    log.WriteInteractionLog(30, (is_submit_design_idea ? "Idea" : "Comment") + "; Reply id: " + reply_id + "; Text: " + this.GetActiveTextBox().Text + "; object_type: " + _object_type + "; object_id: " + _object_id.ToString() + "; Username=" + this.selected_user.title.Text, ((TouchEventArgs)e).TouchDevice);
                else
                    log.WriteInteractionLog(30, (is_submit_design_idea ? "Idea" : "Comment") + "; Text: " + this.GetActiveTextBox().Text + "; object_type: " + _object_type + "; object_id: " + _object_id.ToString() + "; Username=" + this.selected_user.title.Text, ((TouchEventArgs)e).TouchDevice);
                this.error_desc.Visibility = System.Windows.Visibility.Visible;
                this.error_desc.Content = configurations.authentication_failed_text;
                pin.Reset(true);
                error_timer = new System.Threading.Timer(new System.Threading.TimerCallback(this.fade_error), null, 5000, System.Threading.Timeout.Infinite);
            }
        }

        void cancel_comment_auth_clicked(object sender, RoutedEventArgs e)
        {
            this.submit_comment_auth.IsEnabled = false;
            this.error_desc.Visibility = System.Windows.Visibility.Collapsed;
            this.error_desc.Content = "";
            this.pin_area.Visibility = System.Windows.Visibility.Collapsed;
            this.pin.Reset(false);
            CheckTextBoxText();
            if (comment_user_id != -1)
            {
                comment_user_id = -1;
                GotoAuthMode();
            }
            else
            {
                if (is_reply)
                {
                    if (e != null) log.WriteInteractionLog(40, "Reply id was: " + reply_id + "; Text: " + this.GetActiveTextBox().Text + "; object_type: " + _object_type + "; object_id: " + _object_id.ToString(), ((TouchEventArgs)e).TouchDevice);
                    GotoReplyMode();
                }
                else
                {
                    if (is_submit_design_idea) // submit design idea
                    {
                        //GoToSelectActMode();
                        this.leave_comment_area_auth.Visibility = System.Windows.Visibility.Collapsed;
                        this.leave_comment_area_activity.IsEnabled = true;
                        this.leave_comment_area_activity.Visibility = System.Windows.Visibility.Visible;
                        this.buttons_activity.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        if (e != null) log.WriteInteractionLog(40, "Text: " + this.GetActiveTextBox().Text + "; object_type: " + _object_type + "; object_id: " + _object_id.ToString(), ((TouchEventArgs)e).TouchDevice);
                        GotoDefaultMode();
                    }
                }
            }
        }

        void submit_text(RoutedEventArgs e)
        {
            if (is_submit_design_idea)
            {
                Contribution idea = new Contribution();
                idea.date = DateTime.Now;
                idea.location_id = 0;
                idea.note = this.GetActiveTextBox().Text;
                idea.tags = "Design Idea";
                idea.status = "to do";
                database_manager.InsertDesignIdea(idea, this.comment_user_id);
                int collection_id = configurations.get_or_create_collection(this.comment_user_id, activity_id, DateTime.Now);
                Collection_Contribution_Mapping map = new Collection_Contribution_Mapping();
                map.collection_id = collection_id;
                map.contribution_id = idea.id;
                map.date = DateTime.Now;
                database_manager.InsertCollectionContributionMapping(map);

                if (the_item.Content != null)
                    ((design_ideas_listbox)the_item.Content).list_all_design_ideas();
                //window_manager.load_design_ideas();
                log.WriteInteractionLog(42, "Idea" + "; Text: " + this.GetActiveTextBox().Text + "; object_type: " + _object_type + "; object_id: " + _object_id.ToString(), ((TouchEventArgs)e).TouchDevice);
                window_manager.close_submit_design_idea_window((window_frame)this.parent, activity_id, idea.id);
                //if (((design_ideas_listbox)the_item.Content).parent != null)
                //    ((design_ideas_listbox)the_item.Content).parent.list_all_design_ideas();
            }
            else
            {
                Feedback comment = new Feedback();
                comment.date = DateTime.Now;
                comment.note = this.GetActiveTextBox().Text;
                comment.object_id = this._object_id;
                comment.object_type = this._object_type.ToString();
                //if (comments_listbox._list.SelectedIndex == -1)
                //    comment.parent_id = 0;
                //else
                //{
                    //int p_id = (int)((item_generic)comments_listbox._list.Items[comments_listbox._list.SelectedIndex]).Tag;
                if (is_reply)
                    comment.parent_id = reply_id;
                else
                    comment.parent_id = 0;
                //}
                comment.technical_info = "";
                comment.type_id = 1;
                comment.user_id = this.comment_user_id;
                database_manager.InsertFeedback(comment);
                if (is_reply)
                    log.WriteInteractionLog(42, "Comment; Reply id: " + reply_id + "; Text: " + this.GetActiveTextBox().Text + "; object_type: " + _object_type + "; object_id: " + _object_id.ToString(), ((TouchEventArgs)e).TouchDevice);
                else
                    log.WriteInteractionLog(42, "Comment; Text: " + this.GetActiveTextBox().Text + "; object_type: " + _object_type + "; object_id: " + _object_id.ToString(), ((TouchEventArgs)e).TouchDevice);

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
                    window_manager.load_design_ideas_sync();
            }
            //this.GetActiveTextBox().SelectAll();
            cancel_comment_auth_clicked(null, null);
            GotoDefaultMode();
            comment_textbox_default.Text = "";
            comment_textbox_reply.Text = "";
            CheckTextBoxText();
        }

        void item_dropped_on_leave_comment_area_auth(object sender, SurfaceDragDropEventArgs e)
        {
            string[] data = (e.Cursor.Data.ToString()).Split(new Char[] { ';' });
            if (data == null) return;
            if (data.Count() < 4) return;
            string context = data[0].ToLower();
            if (context == "user")
            {
                string username = data[3];
                int user_id = Convert.ToInt32(data[1]);
                this.selected_user.title.Text = username;
                this.comment_user_id = user_id;
                this.selected_user.avatar.Source = new BitmapImage(new Uri(data[2]));
                this.selected_user.more_info.Visibility = System.Windows.Visibility.Collapsed;
                this.submit_comment_auth.IsEnabled = true;
                this.pin_area.Visibility = System.Windows.Visibility.Visible;
                if (keyboard_frame != null) keyboard_frame.Visibility = System.Windows.Visibility.Collapsed;
                this.pin.Reset(true);
                this.submit_comment_auth.Content = "Submit";
                if (is_reply)
                    log.WriteInteractionLog(29, (is_submit_design_idea ? "Idea" : "Comment") + "; user id: " + user_id + "; reply id: " + reply_id + "; Text: " + this.GetActiveTextBox().Text + "; object_type: " + _object_type + "; object_id: " + _object_id.ToString(), e.Cursor.GetPosition(null).X, e.Cursor.GetPosition(null).Y);
                else
                    log.WriteInteractionLog(29, (is_submit_design_idea ? "Idea" : "Comment") + "; user id: " + user_id + "; Text: " + this.GetActiveTextBox().Text + "; object_type: " + _object_type + "; object_id: " + _object_id.ToString(), e.Cursor.GetPosition(null).X, e.Cursor.GetPosition(null).Y);
            }
            e.Handled = true;
        }

        void item_dropped_on_leave_comment_area_activity(object sender, SurfaceDragDropEventArgs e)
        {
            string[] data = (e.Cursor.Data.ToString()).Split(new Char[] { ';' });
            if (data == null) return;
            if (data.Count() < 4) return;
            string context = data[0].ToLower();
            if (context == "design idea type")
            {
                this.activity_id = Convert.ToInt32(((item_generic_v2)e.Cursor.Data).Tag);
                this.selected_activity.title.Text = data[3];
                this.selected_activity.avatar.Source = new BitmapImage(new Uri(data[2]));
                this.selected_activity.more_info.Visibility = System.Windows.Visibility.Collapsed;
                this.submit_activity.IsEnabled = true;
                if (keyboard_frame != null) keyboard_frame.Visibility = System.Windows.Visibility.Collapsed;
                //log.WriteInteractionLog(29, (hide_expander ? "Idea" : "Comment") + "; user id: " + user_id + "; Text: " + this.GetActiveTextBox().Text + "; object_type: " + _object_type + "; object_id: " + _object_id.ToString(), e.Cursor.GetPosition(null).X, e.Cursor.GetPosition(null).Y);
            }
            e.Handled = true;
        }

        public void initialize_contents(UserControl uc, Type obj_type, int obj_id, UserControl parent_frame, string listbox_name)
        {
            this.the_item.Content = uc;
            this._object_id = obj_id;
            this._object_type = obj_type;
            this.comments_listbox._list.Width = parent_frame.Width;
            this.comments_listbox.content_name = listbox_name + "; " + "object id = " + obj_id;
            this.list_all_comments();
            
            //this.add_comment_img.Source = configurations.img_drop_avatar_pic;
            //var brush = new ImageBrush();
            //brush.ImageSource = configurations.img_drop_avatar_pic;
            //brush.Stretch = Stretch.None;
            //this.leave_comment_canvas.Background = brush;

            if (is_idea_collection)
            {
                this.comment_textbox_default.Text = configurations.submit_idea_item_title;
                this.comment_textbox_reply.Text = configurations.submit_idea_item_title;
                this.comment_icon.Source = configurations.img_submit_idea_comment;
            }
            else
            {
                this.comment_textbox_default.Text = configurations.comment_init_text;
                this.comment_textbox_reply.Text = configurations.comment_init_text;
            }
            this.parent = parent_frame;
            this.pin.parent = parent_frame;
        }

        public void initialize_contents(UserControl uc, Type obj_type, int obj_id, UserControl parent_frame, double width)
        {
            this.comments_listbox.content_name = "object id = " + obj_id;
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
            if (is_idea_collection)
            {
                this.comment_textbox_default.Text = configurations.submit_idea_item_title;
                this.comment_textbox_reply.Text = configurations.submit_idea_item_title;
                this.comment_icon.Source = configurations.img_submit_idea_comment;
            }
            else
            {
                this.comment_textbox_default.Text = configurations.comment_init_text;
                this.comment_textbox_reply.Text = configurations.comment_init_text;
            }
            this.parent = parent_frame;
            this.pin.parent = parent_frame;
        }

        public void initialize_contents(UserControl uc, bool is_design, UserControl parent_frame)
        {
            //this.the_item.Content = uc;
            this.is_submit_design_idea = is_design;
            //this.expander.Visibility = System.Windows.Visibility.Collapsed;
            this.expander_metadata_panel.Visibility = System.Windows.Visibility.Collapsed;
            this.comments_listbox.Height = 0;
            this.comment_textbox_default.Text = configurations.design_idea_init_text;
            this.comment_textbox_reply.Text = configurations.design_idea_init_text;
            
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
            this.expand_state = false;
            this.parent = parent_frame;
            this.pin.parent = parent_frame;
        }

        // add an initial comment (that's not actually a comment; it is used to show a contribution's user provided attributes)
        public void initialize_comments(Contribution the_contribution)
        {
            //string comment = "";
            //if (the_contribution.note != null && the_contribution.note != "")
            //    comment = the_contribution.note + "\r\n";
            //comment += configurations.contribution_comment_date;
            //comment += the_contribution.date.ToString();
            //comment += "\r\n";
            //if (the_contribution.tags != "")
            //    comment = comment + configurations.contribution_comment_tag + the_contribution.tags + "\r\n";
            //if (the_contribution.location_id != 0)
            //    comment = comment + configurations.contribution_comment_location + the_contribution.location_id.ToString() + ": " + the_contribution.Location.name;
            this.expander_metadata_panel.Background = Brushes.LightGray;
            //Note
            if (the_contribution.note != null && the_contribution.note != "")
                metadata1.Text = the_contribution.note;
            else
            {
                if (configurations.show_empty_metadata)
                    metadata1.Text = "[Empty]";
                else
                    metadata1.Visibility = System.Windows.Visibility.Collapsed;
            }
            //metadata.Inlines.Add(new LineBreak());

            //User and date
            User u = configurations.find_user_of_contribution(the_contribution);
            string date1 = configurations.GetDate_Formatted(the_contribution.date);
            string date2 = the_contribution.date.ToString("hh:mm tt");
            if (u == null)
            {
                //metadata.Inlines.Add("Date: ");
                metadata2.Inlines.Add(new Bold(new Run(date1 + " " + date2)));
            }
            else
            {
                metadata2.Inlines.Add(configurations.contribution_comment_user);
                metadata2.Inlines.Add(new Bold(new Run(u.name)));
                metadata2.Inlines.Add(" " + configurations.contribution_comment_date);
                metadata2.Inlines.Add(new Bold(new Run(date1 + " " + date2)));
            }
            //metadata.Inlines.Add(new LineBreak());

            //location
            if (the_contribution.location_id != 0)
            {
                metadata3.Inlines.Add(configurations.contribution_comment_location);
                metadata3.Inlines.Add(new Bold(new Run(the_contribution.Location.name)));
            }
            else
            {
                if (configurations.show_empty_metadata)
                    metadata3.Inlines.Add("Location not specified.");
                else
                {
                    metadata3.Visibility = System.Windows.Visibility.Collapsed;
                    should_show_metadata3 = false;
                }
            }
            //metadata.Inlines.Add(new LineBreak());

            //tags
            if (the_contribution.tags != "")
            {
                metadata4.Inlines.Add(configurations.contribution_comment_tag);
                metadata4.Inlines.Add(new Bold(new Run(the_contribution.tags)));
            }
            else
            {
                if (configurations.show_empty_metadata)
                    metadata4.Inlines.Add(configurations.contribution_comment_tag + "[Empty]");
                else
                {
                    metadata4.Visibility = System.Windows.Visibility.Collapsed;
                    should_show_metadata4 = false;
                }
            }
            
            //item_generic i = new item_generic();
            //i.desc.Visibility = System.Windows.Visibility.Collapsed;
            //i.top_panel.Visibility = System.Windows.Visibility.Collapsed;
            //i.content.Text = comment;
            //i.BorderBrush = Brushes.Gray;
            //i.BorderThickness = new Thickness(0, 0, 0, 1);
            //i.Margin = new Thickness(5, 7, 5, 0);

            //this.comments_listbox.populator.initial_item = i;
        }

        public void list_all_comments()
        {
            comment_item i = new comment_item();
            i._object_id = this._object_id; i._object_type = this._object_type;
            this.comments_listbox.populator.item_width = this.comments_listbox._list.Width - 4;
            this.comments_listbox.populator.total_number = this.number_comments;
            this.comments_listbox.populator.reply_clicked_handler = new reply_clicked(this.replybutton_clicked);
            this.comments_listbox.populator.list_all_comments(i);
        }

        bool item_selected(object i, TouchEventArgs e)
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
                        this.UpdateLayout();
                        keyboard.MoveAlongWith(parent, center_keyboard);
                    }
                }
            }
            pin.UpdateKeyboardPosition();
        }

        public UIElement GetKeyboardFrame()
        {
            return keyboard_frame;
        }

        public Control ControlToInjectInto
        {
            get { return this.GetActiveTextBox(); }
        }

        private TextBox GetActiveTextBox()
        {
            if (is_reply)
                return comment_textbox_reply;
            else
                return comment_textbox_default;
        }

        private void ToggleCommentsSection()
        {
            expand_state = !expand_state;
            if (expand_state)
            {
                this.comments_section.Visibility = System.Windows.Visibility.Visible;
                //this.comments_listbox.UpdateLayout();
                if (should_show_metadata3) metadata3.Visibility = System.Windows.Visibility.Visible;
                if (should_show_metadata4) metadata4.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.comments_section.Visibility = System.Windows.Visibility.Collapsed;
                if (!configurations.show_all_metadata)
                {
                    metadata3.Visibility = System.Windows.Visibility.Collapsed;
                    metadata4.Visibility = System.Windows.Visibility.Collapsed;
                }
                //this.comments_listbox.UpdateLayout();
                if (keyboard_frame != null) keyboard_frame.Visibility = System.Windows.Visibility.Collapsed;

                this.comment_user_id = -1;

                CheckTextBoxText();
            }
        }

        private void PrepareForReply()
        {
            this.submit_comment_reply.Width = this.ActualWidth / 2;
            this.cancel_comment_reply.Width = this.ActualWidth / 2;
            this.comment_textbox_reply.Focus();
            CheckInitialText();
            //if (keyboard_frame != null) keyboard_frame.Visibility = System.Windows.Visibility.Collapsed;
            UpdateKeyboardPosition();
        }

        private void PrepareForAuth()
        {
            if (keyboard_frame != null) keyboard_frame.Visibility = System.Windows.Visibility.Collapsed;
            this.selected_user.title.Text = configurations.choose_user_text;
            this.selected_user.center_panel.VerticalAlignment = VerticalAlignment.Center;
            this.selected_user.avatar.Source = configurations.img_choose_user_pic;
            this.selected_user.num_likes.Visibility = System.Windows.Visibility.Collapsed;
            this.selected_user.description.Visibility = System.Windows.Visibility.Collapsed;
            this.selected_user.user_info.Visibility = System.Windows.Visibility.Collapsed;
            this.selected_user.right_panel.Visibility = System.Windows.Visibility.Collapsed;
            this.selected_user.Background = Brushes.White;
            this.submit_comment_auth.Width = this.ActualWidth/2;
            this.submit_comment_auth.Content = "Continue";
            this.cancel_comment_auth.Width = this.ActualWidth / 2;
            this.submit_comment_auth.IsEnabled = false;

            if (configurations.show_help)
            {
                this.selected_user.more_info.Visibility = System.Windows.Visibility.Visible;
                this.selected_user.help.Margin = new Thickness(10, 5, 0, 0);
                this.selected_user.more_info.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                help_button hb = new help_button();
                hb.filename = configurations.help_file_drag_user;
                hb.caption = configurations.help_text_drag_user;
                this.selected_user.help.Content = hb;
            }
        }

        private void PrepareForSelectAct()
        {
            if (keyboard_frame != null) keyboard_frame.Visibility = System.Windows.Visibility.Collapsed;
            this.selected_activity.title.Text = configurations.choose_activity_text;
            this.selected_activity.center_panel.VerticalAlignment = VerticalAlignment.Center;
            this.selected_activity.avatar.Source = configurations.img_choose_activity_pic;
            this.selected_activity.num_likes.Visibility = System.Windows.Visibility.Collapsed;
            this.selected_activity.description.Visibility = System.Windows.Visibility.Collapsed;
            this.selected_activity.user_info.Visibility = System.Windows.Visibility.Collapsed;
            this.selected_activity.right_panel.Visibility = System.Windows.Visibility.Collapsed;
            this.selected_activity.Background = Brushes.White;
            this.submit_activity.Width = this.ActualWidth / 2;
            this.cancel_activity.Width = this.ActualWidth / 2;
            this.submit_activity.IsEnabled = false;

            if (configurations.show_help)
            {
                this.selected_activity.more_info.Visibility = System.Windows.Visibility.Visible;
                this.selected_activity.help.Margin = new Thickness(0, -20, 5, 0);
                help_button hb = new help_button();
                hb.filename = configurations.help_file_drag_designidea;
                hb.caption = configurations.help_text_drag_designidea;
                this.selected_activity.help.Content = hb;
            }
        }

        private void CheckTextBoxText()
        {
            if (GetActiveTextBox().Text == "")
            {
                GetActiveTextBox().Foreground = Brushes.Gray;
                if (is_submit_design_idea)
                    GetActiveTextBox().Text = configurations.design_idea_init_text;
                else if (is_idea_collection)
                    GetActiveTextBox().Text = configurations.submit_idea_item_title;
                else
                    GetActiveTextBox().Text = configurations.comment_init_text;
            }
        }

        private void replybutton_clicked(object sender, EventArgs e)
        {
            item_generic i = (item_generic)(((Button)sender).Tag);
            this.reply_id = (int)i.Tag;
            this.is_reply = true;
            log.WriteInteractionLog(26, "Reply id: " + reply_id + " ; Text: " + this.GetActiveTextBox().Text + "; object_type: " + _object_type + "; object_id: " + _object_id.ToString(), ((TouchEventArgs)e).TouchDevice);

            reply_item.Background = Brushes.White;
            reply_item.username.Text = "";
            string text = configurations.GetTextBlockText2(i.username);
            string[] texts = text.Split(new char[] { ':' });
            reply_item.username.Inlines.Add(new Bold(new Run(texts[0] + ": ")));
            reply_item.username.Inlines.Add(text.Substring(texts[0].Length + 1));
            reply_item.affiliation_icon_small.Visibility = i.affiliation_icon_small.Visibility;
            reply_item.affiliation_icon_small.Source = i.affiliation_icon_small.Source;
            reply_item.user_desc.Visibility = Visibility.Collapsed; //i.user_desc.Content = configurations.GetDate_Formatted(cig.comment.date);
            reply_item.number.Text = i.number.Text; //i.number.Visibility = System.Windows.Visibility.Collapsed;
            reply_item.number.FontSize = configurations.design_idea_item_user_info_font_size;
            reply_item.desc.Visibility = Visibility.Collapsed;// i.desc.Content = "Commented:";
            reply_item.topleft_panel.VerticalAlignment = VerticalAlignment.Top;
            reply_item.top_panel.Margin = new Thickness(5, 10, 5, 10);
            //reply_item.content.Text = i.content.Text;
            reply_item.content.Visibility = System.Windows.Visibility.Collapsed;
            //if (item_width != 0) i.Width = item_width + 2;
            reply_item.avatar.Source = i.avatar.Source;
            reply_item.Tag = i.Tag;
            //reply_item.second_border.Margin = new Thickness(cig.level * 25, 0, 0, 0);
            reply_item.first_border.BorderBrush = Brushes.Gray; reply_item.first_border.BorderThickness = new Thickness(3, 3, 3, 3);
            reply_item.second_border.BorderBrush = Brushes.DarkGray; reply_item.second_border.BorderThickness = new Thickness(0, 0, 0, 0);

            this.comment_textbox_reply.Text = comment_textbox_default.Text;
            this.comment_textbox_reply.Foreground = comment_textbox_default.Foreground;

            GotoReplyMode();
        }

        private void GotoDefaultMode()
        {
            this.leave_comment_area_default.IsEnabled = true;
            this.submit_comment_default.Visibility = System.Windows.Visibility.Visible;

            this.leave_comment_area_default.Visibility = System.Windows.Visibility.Visible;
            this.leave_comment_area_activity.Visibility = System.Windows.Visibility.Collapsed;
            this.leave_comment_area_reply.Visibility = System.Windows.Visibility.Collapsed;
            this.leave_comment_area_auth.Visibility = System.Windows.Visibility.Collapsed;
            if (is_reply)
            {
                this.comment_textbox_default.Text = comment_textbox_reply.Text;
                this.comment_textbox_default.Foreground = comment_textbox_reply.Foreground;
            }
            this.is_reply = false;
            this.reply_id = 0;
            this.comment_textbox_default.Focus();
            CheckInitialText();
            //if (keyboard_frame != null) keyboard_frame.Visibility = System.Windows.Visibility.Collapsed;
            UpdateKeyboardPosition();
            //CheckTextBoxText();
        }

        private void GoToSelectActMode()
        {
            this.leave_comment_area_default.IsEnabled = false;
            this.submit_comment_default.Visibility = System.Windows.Visibility.Collapsed;
            this.leave_comment_area_auth.Visibility = System.Windows.Visibility.Collapsed;

            this.leave_comment_area_activity.Visibility = System.Windows.Visibility.Visible;
            this.buttons_activity.Visibility = System.Windows.Visibility.Visible;
            this.leave_comment_area_activity.IsEnabled = true;

            PrepareForSelectAct();
        }

        private void GotoReplyMode()
        {
            this.leave_comment_area_reply.IsEnabled = true;
            this.buttons_reply.Visibility = System.Windows.Visibility.Visible;

            this.leave_comment_area_default.Visibility = System.Windows.Visibility.Collapsed;
            this.leave_comment_area_reply.Visibility = System.Windows.Visibility.Visible;
            this.leave_comment_area_auth.Visibility = System.Windows.Visibility.Collapsed;
            PrepareForReply();
            //CheckTextBoxText();
        }

        private void GotoAuthMode()
        {
            if (is_reply)
            {
                this.leave_comment_area_reply.IsEnabled = false;
                this.buttons_reply.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                if (is_submit_design_idea) // submit design idea
                {
                    this.leave_comment_area_activity.IsEnabled = false;
                    this.buttons_activity.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    this.leave_comment_area_default.IsEnabled = false;
                    this.submit_comment_default.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
            //this.leave_comment_area_default.Visibility = System.Windows.Visibility.Collapsed;
            //this.leave_comment_area_reply.Visibility = System.Windows.Visibility.Collapsed;
            this.leave_comment_area_auth.Visibility = System.Windows.Visibility.Visible;
            PrepareForAuth();
        }

        public bool CheckInitialText()
        {
            if (this.GetActiveTextBox().Foreground == Brushes.Gray)
            {
                this.GetActiveTextBox().Foreground = Brushes.Black;
                GetActiveTextBox().Text = "";
                return true;
            }
            else
                return false;
        }

        public void fade_error(Object stateInfo)
        {
            this.error_desc.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
               new System.Action(() =>
               {
                   this.error_desc.Content = "";
                   this.error_desc.Visibility = System.Windows.Visibility.Collapsed;
               }));
        }
    }
}
