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
    /// Interaction logic for list_header.xaml
    /// </summary>
    public partial class list_header : UserControl
    {
        public AToZOrder atoz_order;
        public TopOrder top_order;
        public RecentOrder recent_order;
        public DoneCheckChanged done_checked_changed;
        public NotDoneCheckChanged not_done_checked_changed;

        public list_header()
        {
            InitializeComponent();
            if (configurations.high_contrast)
            {
                this.whole.Background = Brushes.DarkGray;
            }
        }

        public void sort()
        {
            if (atoz.IsChecked.Value && atoz_order != null)
                atoz_order();
            if (recent.IsChecked.Value && recent_order != null)
                recent_order();
            if (top.IsChecked.Value && top_order != null)
                top_order();
        }

        private void top_Checked(object sender, RoutedEventArgs e)
        {
            TouchDevice td = null;
            try { td = ((TouchEventArgs)e).TouchDevice; }
            catch (Exception) { }
            log.WriteInteractionLog(9, (string)(this.title.Content), td);
            if (top_order != null) top_order();
        }

        private void recent_Checked(object sender, RoutedEventArgs e)
        {
            TouchDevice td = null;
            try { td = ((TouchEventArgs)e).TouchDevice; }
            catch (Exception) { }
            log.WriteInteractionLog(10, (string)(this.title.Content), td);
            if (recent_order != null) recent_order();
        }

        private void atoz_Checked(object sender, RoutedEventArgs e)
        {
            TouchDevice td = null;
            try { td = ((TouchEventArgs)e).TouchDevice; }
            catch (Exception) { }
            log.WriteInteractionLog(8, (string)(this.title.Content), td);
            if (atoz_order != null) atoz_order();
        }

        private void done_Checked_Changed(object sender, RoutedEventArgs e)
        {
            if (!done.IsChecked.Value && !not_done.IsChecked.Value)
                not_done.IsChecked = true;
            if (done_checked_changed != null) done_checked_changed();
        }

        private void not_done_Checked_Changed(object sender, RoutedEventArgs e)
        {
            if (!done.IsChecked.Value && !not_done.IsChecked.Value)
                done.IsChecked = true;
            if (not_done_checked_changed != null) not_done_checked_changed();
        }

        public bool is_done_checked()
        {
            return done.IsChecked.Value;
        }
        public bool is_not_done_checked()
        {
            return not_done.IsChecked.Value;
        }
    }

    public delegate void AToZOrder();
    public delegate void TopOrder();
    public delegate void RecentOrder();
    public delegate void DoneCheckChanged();
    public delegate void NotDoneCheckChanged();
}
