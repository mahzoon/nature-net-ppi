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

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for tab_control.xaml
    /// </summary>
    public partial class tab_control : UserControl
    {
        public tab_control()
        {
            InitializeComponent();

            //Static Configuration Values:
            
        }

        public void load_control(bool left, int selected_tab_id, double screen_width)
        {
            this.Width = screen_width * configurations.tab_width_percentage / 100;
            
            this.users_listbox.Width = this.Width - 65;
            this.design_ideas_listbox.Width = this.Width - 65;
            this.activities_listbox.Width = this.Width - 65;

            this.tab.BorderThickness = new Thickness(0, 0, 2, 0);
            if (!left)
            {
                foreach (TabItem ti in this.tab.Items)
                {
                    Transform t = new RotateTransform(-90);
                    ((TextBlock)ti.Header).LayoutTransform = t;
                }
                this.tab.TabStripPlacement = Dock.Left;
                this.tab.BorderThickness = new Thickness(2, 0, 0, 0);
            }
            this.tab.SelectedIndex = selected_tab_id;
            this.users_listbox.users_list.populator.item_width = users_listbox.Width - 3;
            this.users_listbox.list_all_users();
            this.activities_listbox.activities_list.populator.item_width = activities_listbox.Width - 3;
            this.activities_listbox.list_all_activities();
            this.design_ideas_listbox.design_ideas_list.populator.item_width = design_ideas_listbox.Width - 3;
            this.design_ideas_listbox.list_all_design_ideas();
        }

        public void highlight_user_and_open_collection(string username, bool highlight)
        {
            this.users_listbox.list_users_and_highlight(username, highlight, this.tab);
        }

        public void highlight_design_idea_and_open_it(string info, bool highlight)
        {
            this.design_ideas_listbox.list_design_ideas_and_highlight(info, highlight, this.tab);
        }

        public void load_users()
        {
            this.users_listbox.list_all_users();
        }

        public void load_design_ideas()
        {
            this.design_ideas_listbox.list_all_design_ideas();
        }

        public void load_activities()
        {
            this.activities_listbox.list_all_activities();
        }

        public void load_design_ideas_sync()
        {
            this.design_ideas_listbox.list_all_design_ideas_sync();
        }

        private void TabItem_TouchDown(object sender, TouchEventArgs e)
        {
            TabItem tab = sender as TabItem;
            TabControl control = tab.Parent as TabControl;
            TextBlock tb1 = (TextBlock)(tab).Header;
            TextBlock tb2 = (TextBlock)((TabItem)control.SelectedItem).Header;
            log.WriteInteractionLog(2, "from " + tb2.Text + " to " + tb1.Text, e.TouchDevice);
            control.SelectedItem = tab;
            
            //e.Handled = true;
        }
    }
}
