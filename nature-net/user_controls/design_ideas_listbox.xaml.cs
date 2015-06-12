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
    /// Interaction logic for design_ideas_listbox.xaml
    /// </summary>
    public partial class design_ideas_listbox : UserControl
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();

        public design_ideas_listbox parent;
        public thumbs_up like_handler;

        int init_pos_x = 65;
        int last_pos_x = 65;
        int pos_x_increment = 30;
        int max_pos_x = 250;

        public item_generic_v2 submit_idea;

        private bool has_touched_down = false;

        public design_ideas_listbox()
        {
            InitializeComponent();

            header.title.Content = configurations.design_idea_listbox_header;
            header.top.Content = configurations.design_idea_lisbox_top_text;
            create_submit_design_item();
            
            this.design_ideas_list.initialize(false, "design idea type", new ItemSelected(item_selected));
            this.design_ideas_list.content_name = "Design ideas listbox";
            //this.design_ideas_list.populator.initial_item = submit_idea;
            header.atoz_order = new AToZOrder(this.atoz_order);
            header.top_order = new TopOrder(this.top_order);
            header.recent_order = new RecentOrder(this.recent_order);
            header.done_checked_changed = new DoneCheckChanged(filter_checks_changed);
            header.not_done_checked_changed = new NotDoneCheckChanged(filter_checks_changed);
            this.design_ideas_list.populator.header = header;
            like_handler = new thumbs_up(this.like_touched);
            this.design_ideas_list.populator.thumbs_up_handler = like_handler;
            //this.design_ideas_list.populator.thumbs_down_handler = new thumbs_down(this.dislike_touched);

            this.design_ideas_list.Background = Brushes.White;
            
            //this.header.filter_grid.Visibility = System.Windows.Visibility.Visible;
        }

        void submit_PreviewTouchDown(object sender, TouchEventArgs e)//RoutedEventArgs e)
        {
            //submit_idea.Background = Brushes.LightGray;
            submit_idea.Background = Brushes.White;
            has_touched_down = true;
        }

        void submit_PreviewTouchUp(object sender, TouchEventArgs e)//RoutedEventArgs e)
        {
            if (!has_touched_down)
                return;
            has_touched_down = false;
            //submit_idea.Background = Brushes.White;
            submit_idea.Background = Brushes.LightGray;
            if (configurations.high_contrast)
            {
                submit_idea.Background = Brushes.DarkGray;
                submit_idea.right_panel.Background = Brushes.DarkGray;
            }
            log.WriteInteractionLog(7, "", e.TouchDevice);
            window_manager.open_design_idea_window_ext(this, last_pos_x, submit_idea.PointToScreen(new Point(0, 0)).Y);
            last_pos_x = last_pos_x + pos_x_increment;
            if (last_pos_x > max_pos_x) last_pos_x = init_pos_x;
        }

        bool item_selected(object i, TouchEventArgs e)
        {
            item_generic_v2 item = (item_generic_v2)i;
            string[] idea_item = ("design idea;" + item.ToString()).Split(new Char[] { ';' });
            if (e != null)
                log.WriteInteractionLog(16, "tapped the listbox item: " + item.ToString(), e.TouchDevice);
            ////window_manager.open_design_idea_window(idea_item, 65, item.PointToScreen(new Point(0, 0)).Y);
            //window_manager.open_design_idea_window(item, 65, item.PointToScreen(new Point(0, 0)).Y);
            window_manager.open_activity_window(item.title.Text, Convert.ToInt32(item.Tag), 65,
                item.PointToScreen(new Point(0, 0)).Y, true, item.avatar.Source);
            return true;
        }

        public void list_all_design_ideas_sync()
        {
            this.design_ideas_list.populator.item_width = this.Width - 3;
            this.design_ideas_list.populator.list_all_design_ideas_sync();
        }

        public void list_all_design_ideas()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
               new System.Action(() =>
               {
                   //this.design_ideas_list.populator.item_width = this.Width - 3;
                   this.design_ideas_list.populator.list_all_design_ideas();
               }));
        }

        public void list_design_ideas_and_highlight(string info, bool highlight, TabControl tb)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
               new System.Action(() =>
               {
                   if (highlight)
                   {
                       tb.SelectedIndex = 2;
                       this.design_ideas_list.populator.item_width = this.Width - 3;
                       this.design_ideas_list.populator.list_all_design_ideas_sync();
                       //configurations.SortItemGenericList(this.users_list._list.Items, false, false, true, configurations.users_num_desc.Length, configurations.users_date_desc.Length, true, true);
                       //this.header.atoz.IsChecked = false;
                       //this.header.recent.IsChecked = true;
                       //this.header.top.IsChecked = false;
                       //this.design_ideas_list._list.Items.Refresh();
                       //this.design_ideas_list._list.UpdateLayout();
                   }
                   int activity_id = Convert.ToInt32(info.Split(new char[] { ';' })[0]);
                   int contribution_id = Convert.ToInt32(info.Split(new char[] { ';' })[1]);
                   item_generic_v2 i = find_item(activity_id);
                   if (i == null) return;
                   //ListBoxItem lbi = (ListBoxItem)(this.design_ideas_list._list.ItemContainerGenerator.ContainerFromItem(i));
                   if (highlight)
                   {
                       this.design_ideas_list._list.ScrollToCenterOfView(i);
                       i.Background = Brushes.Gray;
                       //double y = lbi.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0)).Y;
                       double y = 0;
                       if (this.design_ideas_list._list.Tag != null)
                           y = (double)this.design_ideas_list._list.Tag;
                       //string[] idea_item = ("design idea;" + i.ToString()).Split(new Char[] { ';' });
                       //window_manager.open_design_idea_window(idea_item, 65, y + 40);//lbi.PointToScreen(new Point(0,0)).Y);
                       window_manager.open_activity_window(i.title.Text, activity_id,
                           65, i.PointToScreen(new Point(0, 0)).Y, true, i.avatar.Source);
                       item_generic_v2 contrib_item = configurations.get_designidea_item_visuals(contribution_id);
                       window_manager.open_design_idea_window(contrib_item, 165, y + 40, i.title.Text);//lbi.PointToScreen(new Point(0,0)).Y);
                   }
                   else
                       i.Background = Brushes.White;
               }));
        }

        private item_generic_v2 find_item(int id)
        {
            for (int counter = 0; counter < this.design_ideas_list._list.Items.Count; counter++)
            {
                item_generic_v2 i = (item_generic_v2)this.design_ideas_list._list.Items[counter];
                if (Convert.ToInt32(i.Tag) == id)
                    return i;
            }
            return null;
        }

        void filter_checks_changed()
        {
            this.design_ideas_list.populator.show_done = header.is_done_checked();
            this.design_ideas_list.populator.show_not_done = header.is_not_done_checked();
            list_all_design_ideas();
        }

        void atoz_order()
        {
            if (this.design_ideas_list._list.Items.Count > 0)
                configurations.SortItemGenericList(this.design_ideas_list._list.Items,
                    true, false, false, configurations.designidea_num_desc.Length, configurations.designidea_date_desc.Length, true, true);
            //this.design_ideas_list._list.Items.Refresh();
        }

        void top_order()
        {
            if (this.design_ideas_list._list.Items.Count > 0)
                configurations.SortItemGenericList(this.design_ideas_list._list.Items,
                    false, true, false, configurations.designidea_num_desc.Length, configurations.designidea_date_desc.Length, true, true);
            //this.design_ideas_list._list.Items.Refresh();
        }

        void recent_order()
        {
            if (this.design_ideas_list._list.Items.Count > 0)
                configurations.SortItemGenericList(this.design_ideas_list._list.Items,
                    false, false, true, configurations.designidea_num_desc.Length, configurations.designidea_date_desc.Length, true, true);
            //this.design_ideas_list._list.Items.Refresh();
        }

        void like_touched(object sender, TouchEventArgs te)
        {
            item_generic_v2 i = (item_generic_v2)sender;
            i.like_touched(null, te);
            //this.design_ideas_list._list.Items.Refresh();
            //this.design_ideas_list.populator.list_all_design_ideas();
        }

        private void create_submit_design_item()
        {
            submit_idea = new item_generic_v2();
            submit_idea.Background = Brushes.LightGray;
            submit_idea.avatar.Source = configurations.img_submit_idea_icon;
            submit_idea.avatar.Width = configurations.design_idea_item_avatar_width;
            submit_idea.avatar.Height = configurations.design_idea_item_avatar_width; submit_idea.avatar.Margin = new Thickness(5);
            submit_idea.num_likes.Visibility = System.Windows.Visibility.Collapsed;
            submit_idea.title.Text = configurations.submit_idea_item_title;
            //TextBlock.SetFontWeight(submit_idea.title, FontWeights.Normal);
            //submit_idea.title.FontSize = configurations.design_idea_item_title_font_size;
            submit_idea.description.Visibility = System.Windows.Visibility.Collapsed;
            submit_idea.user_info.Visibility = System.Windows.Visibility.Collapsed;
            submit_idea.info_panel.Visibility = System.Windows.Visibility.Collapsed;
            submit_idea.contribution_panel.Visibility = System.Windows.Visibility.Collapsed;
            submit_idea.txt_level1.Visibility = System.Windows.Visibility.Collapsed;
            submit_idea.center_panel.VerticalAlignment = VerticalAlignment.Center;
            submit_idea.Margin = new Thickness(2, 2, 2, 0);
            submit_idea.Height = configurations.user_item_avatar_width; //signup.avatar.Height;
            //submit_idea.right_panel.Width = configurations.design_idea_right_panel_width;

            if (configurations.high_contrast)
            {
                submit_idea.Background = Brushes.DarkGray;
                submit_idea.right_panel.Background = Brushes.DarkGray;
            }

            submit_idea.PreviewTouchDown += new EventHandler<TouchEventArgs>(submit_PreviewTouchDown);
            submit_idea.PreviewTouchUp += new EventHandler<TouchEventArgs>(submit_PreviewTouchUp);
            submit_idea_panel.Children.Add(submit_idea);

            //i.user_info.Margin = new Thickness(5);
            //i.user_info_name.Margin = new Thickness(2, 0, 0, 0); i.user_info_date.Margin = new Thickness(2, 0, 2, 0);
            //i.user_info_name.FontSize = configurations.design_idea_item_user_info_font_size; i.user_info_date.FontSize = configurations.design_idea_item_user_info_font_size;

            //submit_idea = new item_generic();
            //submit_idea.Background = Brushes.White;
            //submit_idea.user_desc.Visibility = System.Windows.Visibility.Collapsed;
            //submit_idea.number.Visibility = System.Windows.Visibility.Collapsed;
            //submit_idea.content.Visibility = System.Windows.Visibility.Collapsed;
            //submit_idea.desc.Visibility = System.Windows.Visibility.Collapsed;
            //submit_idea.username.Text = "Submit Idea";
            ////submit_idea.Background = new SolidColorBrush(Colors.LightGreen);
            //submit_idea.username.Foreground = new SolidColorBrush(Colors.Black);
            //submit_idea.user_desc.Foreground = new SolidColorBrush(Colors.White);
            //submit_idea.top_panel.Margin = new Thickness(13, 13, 13, 13);
            //submit_idea.avatar.Source = configurations.img_submit_idea_icon;
            //submit_idea.PreviewTouchDown += new EventHandler<TouchEventArgs>(submit_Click);
            //submit_idea_panel.Children.Add(submit_idea);
        }
    }
}
