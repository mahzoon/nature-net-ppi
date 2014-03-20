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

        public users_listbox()
        {
            InitializeComponent();

            //Static Configuration Values:
            header.title.Content = "Users";
            //this.Width = 270;
            item_generic signup = new item_generic();
            signup.user_desc.Visibility = System.Windows.Visibility.Collapsed;
            signup.number.Visibility = System.Windows.Visibility.Collapsed;
            signup.content.Visibility = System.Windows.Visibility.Collapsed;
            signup.desc.Visibility = System.Windows.Visibility.Collapsed;
            signup.username.Text = "Sign up";
            //signup.Background = new SolidColorBrush(Colors.LightGreen);
            signup.username.Foreground = new SolidColorBrush(Colors.Black);
            signup.user_desc.Foreground = new SolidColorBrush(Colors.White);
            signup.top_panel.Margin = new Thickness(13, 13, 13, 13);
            signup.avatar.Source = configurations.img_signup_icon;
            signup.PreviewTouchDown += new EventHandler<TouchEventArgs>(signup_PreviewTouchDown);
            signup.avatar.Source = configurations.img_signup_icon;
            signup_panel.Children.Add(signup);

            this.users_list.initialize(false, "user", new ItemSelected(this.item_selected));
            //this.users_list.populator.initial_item = signup;
            header.atoz_order = new AToZOrder(this.atoz_order);
            header.top_order = new TopOrder(this.top_order);
            header.recent_order = new RecentOrder(this.recent_order);
            this.users_list.populator.header = header;
        }

        void signup_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            window_manager.open_signup_window(configurations.RANDOM(0, (int)window_manager.main_canvas.ActualWidth), 0);
        }

        public void list_all_users()
        {
            this.users_list.populator.item_width = this.Width - 3;
            this.users_list.populator.list_all_users();
        }

        bool item_selected(object i)
        {
            item_generic_v2 item = (item_generic_v2)i;
            window_manager.open_collection_window((string)item.title.Text, (int)item.Tag,
                    configurations.RANDOM(20, (int)(window_manager.main_canvas.ActualWidth - item.ActualWidth)),
                    item.PointToScreen(new Point(0, 0)).Y);
            return true;
        }

        void atoz_order()
        {
            configurations.SortItemGenericList(this.users_list._list.Items,
                true, false, false, configurations.users_num_desc.Length, configurations.users_date_desc.Length, true, true);
            //this.users_list._list.Items.Refresh();
        }

        void top_order()
        {
            configurations.SortItemGenericList(this.users_list._list.Items,
                false, true, false, configurations.users_num_desc.Length, configurations.users_date_desc.Length, true, true);
            //this.users_list._list.Items.Refresh();
        }

        void recent_order()
        {
            configurations.SortItemGenericList(this.users_list._list.Items,
                false, false, true, configurations.users_num_desc.Length, configurations.users_date_desc.Length, true, true);
            //this.users_list._list.Items.Refresh();
        }
    }
}
