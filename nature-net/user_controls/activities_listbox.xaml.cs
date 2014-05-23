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

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for activities_listbox.xaml
    /// </summary>
    public partial class activities_listbox : UserControl
    {
        bool atoz_asc = true;
        bool top_asc = true;
        bool recent_asc = true;

        public item_generic_v2 submit_idea;
        
        int init_pos_x = 65;
        int last_pos_x = 65;
        int pos_x_increment = 30;
        int max_pos_x = 250;

        public activities_listbox()
        {
            InitializeComponent();

            header.title.Content = configurations.activities_listbox_header;
            create_submit_design_item();

            //this.activities_listbox.parent = this;
            //this.activities_listbox.list_all_activities();
            this.activities_list.initialize(false, "activity", new ItemSelected(this.activity_item_selected));
            header.atoz_order = new AToZOrder(this.atoz_order);
            header.top_order = new TopOrder(this.top_order);
            header.recent_order = new RecentOrder(this.recent_order);
            this.activities_list.populator.header = header;

            this.activities_list.Background = Brushes.White;
        }

        public void list_all_activities()
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
               new System.Action(() =>
               {
                   this.activities_list.populator.item_width = this.Width - 3;
                   this.activities_list.populator.list_all_activities();
               }));
        }

        bool activity_item_selected(object i)
        {
            item_generic_v2 item = (item_generic_v2)i;
            string[] activity_item = ("activity;" + item.ToString()).Split(new Char[] { ';' });
            window_manager.open_activity_window(activity_item[3], Convert.ToInt32(activity_item[1]), 0, item.PointToScreen(new Point(0, 0)).Y);
            return true;
        }

        void atoz_order()
        {
            //this.activities_list._list.Items.SortDescriptions.Clear();
            //this.activities_list._list.Items.SortDescriptions.Add(new SortDescription("username", ListSortDirection.Descending));
            configurations.SortItemGenericList(this.activities_list._list.Items,
                true, false, false, configurations.activities_num_desc.Length, configurations.activities_date_desc.Length, atoz_asc, true);
            //this.activities_list._list.Items.Refresh();
        }

        void top_order()
        {
            //this.activities_list._list.Items.SortDescriptions.Clear();
            //this.activities_list._list.Items.SortDescriptions.Add(new SortDescription("number", ListSortDirection.Ascending));
            configurations.SortItemGenericList(this.activities_list._list.Items,
                false, true, false, configurations.activities_num_desc.Length, configurations.activities_date_desc.Length, top_asc, true);
            //this.activities_list._list.Items.Refresh();
        }

        void recent_order()
        {
            //this.activities_list._list.Items.SortDescriptions.Clear();
            //this.activities_list._list.Items.SortDescriptions.Add(new SortDescription("user_desc", ListSortDirection.Descending));
            configurations.SortItemGenericList(this.activities_list._list.Items,
                false, false, true, configurations.activities_num_desc.Length, configurations.activities_date_desc.Length, recent_asc, true);
            //this.activities_list._list.Items.Refresh();
        }


        void submit_PreviewTouchDown(object sender, TouchEventArgs e)//RoutedEventArgs e)
        {
            submit_idea.Background = Brushes.LightGray;
        }

        void submit_PreviewTouchUp(object sender, TouchEventArgs e)//RoutedEventArgs e)
        {
            submit_idea.Background = Brushes.White;
            window_manager.open_design_idea_window_ext(null, last_pos_x, submit_idea.PointToScreen(new Point(0, 0)).Y);
            last_pos_x = last_pos_x + pos_x_increment;
            if (last_pos_x > max_pos_x) last_pos_x = init_pos_x;
        }

        private void create_submit_design_item()
        {
            submit_idea = new item_generic_v2();
            submit_idea.Background = Brushes.LightGray;
            submit_idea.avatar.Source = configurations.img_submit_idea_icon;
            submit_idea.avatar.Width = configurations.design_idea_item_avatar_width;
            submit_idea.avatar.Height = configurations.design_idea_item_avatar_width; submit_idea.avatar.Margin = new Thickness(5);
            submit_idea.num_likes.Visibility = System.Windows.Visibility.Collapsed;
            submit_idea.title.Text = configurations.submit_idea_activity_item_title;
            //TextBlock.SetFontWeight(submit_idea.title, FontWeights.Normal); submit_idea.title.FontSize = configurations.design_idea_item_title_font_size;
            submit_idea.description.Visibility = System.Windows.Visibility.Collapsed;
            submit_idea.user_info.Visibility = System.Windows.Visibility.Collapsed;
            submit_idea.info_panel.Visibility = System.Windows.Visibility.Collapsed;
            submit_idea.contribution_panel.Visibility = System.Windows.Visibility.Collapsed;
            submit_idea.txt_level1.Visibility = System.Windows.Visibility.Collapsed;
            submit_idea.center_panel.VerticalAlignment = VerticalAlignment.Center;
            submit_idea.Margin = new Thickness(2, 2, 2, 0);
            submit_idea.Height = configurations.user_item_avatar_width; //signup.avatar.Height;
            //submit_idea.right_panel.Width = configurations.design_idea_right_panel_width;

            submit_idea.PreviewTouchDown += new EventHandler<TouchEventArgs>(submit_PreviewTouchDown);
            submit_idea.PreviewTouchUp += new EventHandler<TouchEventArgs>(submit_PreviewTouchUp);
            submit_idea_panel.Children.Add(submit_idea);
        }
    }
}
