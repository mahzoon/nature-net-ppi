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

        public activities_listbox()
        {
            InitializeComponent();

            header.title.Content = "Activities";
            //this.activities_listbox.parent = this;
            //this.activities_listbox.list_all_activities();
            this.activities_list.initialize(false, "activity", new ItemSelected(this.activity_item_selected));
            header.atoz_order = new AToZOrder(this.atoz_order);
            header.top_order = new TopOrder(this.top_order);
            header.recent_order = new RecentOrder(this.recent_order);
            this.activities_list.populator.header = header;
        }

        public void list_all_activities()
        {
            this.activities_list.populator.item_width = this.Width - 3;
            this.activities_list.populator.list_all_activities();
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
    }
}
