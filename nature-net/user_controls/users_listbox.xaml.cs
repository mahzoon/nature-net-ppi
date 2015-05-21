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
    /// Interaction logic for users_listbox.xaml
    /// </summary>
    public partial class users_listbox : UserControl
    {
        //private readonly BackgroundWorker worker = new BackgroundWorker();
        public item_generic_v2 signup;

        int init_pos_x = 65;
        int last_pos_x = 65;
        int pos_x_increment = 30;
        int max_pos_x = 250;

        bool has_touched_down = false;

        public users_listbox()
        {
            InitializeComponent();

            header.title.Content = configurations.users_listbox_header;
            create_signup_item();
            
            this.users_list.initialize(false, "user", new ItemSelected(this.item_selected));
            this.users_list.content_name = "Users listbox";
            //this.users_list.populator.initial_item = signup;
            header.atoz_order = new AToZOrder(this.atoz_order);
            header.top_order = new TopOrder(this.top_order);
            header.recent_order = new RecentOrder(this.recent_order);
            this.users_list.populator.header = header;

            this.users_list.Background = Brushes.White;
        }

        void signup_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            //signup.Background = Brushes.LightGray;
            signup.Background = Brushes.White;
            has_touched_down = true;
        }

        void signup_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            if (!has_touched_down)
                return;
            has_touched_down = false;
            //signup.Background = Brushes.White;
            signup.Background = Brushes.LightGray;

            log.WriteInteractionLog(5, "", e.TouchDevice);
            window_manager.open_signup_window(last_pos_x, signup.PointToScreen(new Point(0, 0)).Y);
            last_pos_x = last_pos_x + pos_x_increment;
            if (last_pos_x > max_pos_x) last_pos_x = init_pos_x;
        }
        
        public void list_all_users()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
               new System.Action(() =>
               {
                   //this.users_list.populator.item_width = this.Width - 3;
                   this.users_list.populator.list_all_users();
               }));
        }

        public void list_users_and_highlight(string username, bool highlight, TabControl tb)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
               new System.Action(() =>
               {
                   if (highlight)
                   {
                       tb.SelectedIndex = 0;
                       this.users_list.populator.item_width = this.Width - 3;
                       this.users_list.populator.list_all_users_sync();
                       //configurations.SortItemGenericList(this.users_list._list.Items, false, false, true, configurations.users_num_desc.Length, configurations.users_date_desc.Length, true, true);
                       //this.header.atoz.IsChecked = false;
                       //this.header.recent.IsChecked = true;
                       //this.header.top.IsChecked = false;
                       //this.users_list._list.Items.Refresh();
                       //this.users_list._list.UpdateLayout();
                   }

                   item_generic_v2 i = find_item(username);
                   if (i == null) return;
                   ListBoxItem lbi = (ListBoxItem)(this.users_list._list.ItemContainerGenerator.ContainerFromItem(i));
                   if (highlight)
                   {
                       this.users_list._list.ScrollToCenterOfView(i);
                       i.Background = Brushes.Gray;
                       //double y = lbi.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0)).Y;
                       double x = 0;
                       if (this.users_list._list.Tag != null)
                           x = (double)this.users_list._list.Tag;
                       window_manager.open_collection_window((string)i.title.Text, (int)i.Tag, 65, x + 30);//lbi.PointToScreen(new Point(0,0)).Y);
                   }
                   else
                       i.Background = Brushes.White;
               }));
        }

        private item_generic_v2 find_item(string title)
        {
            for (int counter = 0; counter < this.users_list._list.Items.Count; counter++)
            {
                item_generic_v2 i = (item_generic_v2)this.users_list._list.Items[counter];
                if (i.title.Text == title)
                    return i;
            }
            return null;
        }

        bool item_selected(object i, TouchEventArgs e)
        {
            item_generic_v2 item = (item_generic_v2)i;
            if (e != null)
                log.WriteInteractionLog(14, "tapped the listbox item: " + item.ToString(), e.TouchDevice);
            window_manager.open_collection_window((string)item.title.Text, (int)item.Tag, 65, item.PointToScreen(new Point(0, 0)).Y);
            return true;
        }

        void atoz_order()
        {
            if (this.users_list._list.Items.Count > 0)
                configurations.SortItemGenericList(this.users_list._list.Items,
                    true, false, false, configurations.users_num_desc.Length, configurations.users_date_desc.Length, true, true);
            //this.users_list._list.Items.Refresh();
        }

        void top_order()
        {
            if (this.users_list._list.Items.Count > 0)
                configurations.SortItemGenericList(this.users_list._list.Items,
                    false, true, false, configurations.users_num_desc.Length, configurations.users_date_desc.Length, true, true);
            //this.users_list._list.Items.Refresh();
        }

        void recent_order()
        {
            if (this.users_list._list.Items.Count > 0)
                configurations.SortItemGenericList(this.users_list._list.Items,
                    false, false, true, configurations.users_num_desc.Length, configurations.users_date_desc.Length, true, true);
            //this.users_list._list.Items.Refresh();
        }

        private void create_signup_item()
        {
            signup = new item_generic_v2();
            signup.Background = Brushes.LightGray;
            signup.avatar.Source = configurations.img_signup_icon;
            signup.num_likes.Visibility = System.Windows.Visibility.Collapsed;
            signup.title.Text = configurations.signup_item_title;
            signup.description.Visibility = System.Windows.Visibility.Collapsed;
            signup.user_info.Visibility = System.Windows.Visibility.Collapsed;
            signup.info_panel.Visibility = System.Windows.Visibility.Collapsed;
            signup.contribution_panel.Visibility = System.Windows.Visibility.Collapsed;
            signup.txt_level1.Visibility = System.Windows.Visibility.Collapsed;
            signup.center_panel.VerticalAlignment = VerticalAlignment.Center;
            signup.avatar.Width = configurations.user_item_avatar_width;
            signup.Margin = new Thickness(2, 2, 2, 0);
            signup.Height = configurations.user_item_avatar_width; //signup.avatar.Height;

            signup.PreviewTouchDown += new EventHandler<TouchEventArgs>(signup_PreviewTouchDown);
            signup.PreviewTouchUp += new EventHandler<TouchEventArgs>(signup_PreviewTouchUp);
            signup_panel.Children.Add(signup);

            //signup = new item_generic();
            //signup.Background = Brushes.White;
            //signup.user_desc.Visibility = System.Windows.Visibility.Collapsed;
            //signup.number.Visibility = System.Windows.Visibility.Collapsed;
            //signup.content.Visibility = System.Windows.Visibility.Collapsed;
            //signup.desc.Visibility = System.Windows.Visibility.Collapsed;
            //signup.username.Text = "Sign up";
            ////signup.Background = new SolidColorBrush(Colors.LightGreen);
            //signup.username.Foreground = new SolidColorBrush(Colors.Black);
            //signup.user_desc.Foreground = new SolidColorBrush(Colors.White);
            //signup.top_panel.Margin = new Thickness(13, 13, 13, 13);
            //signup.avatar.Source = configurations.img_signup_icon;
            //signup.PreviewTouchDown += new EventHandler<TouchEventArgs>(signup_PreviewTouchDown);
            //signup.avatar.Source = configurations.img_signup_icon;
            //signup_panel.Children.Add(signup);
        }
    }
}
