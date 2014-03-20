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

        public list_header()
        {
            InitializeComponent();
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
            if (top_order != null) top_order();
        }

        private void recent_Checked(object sender, RoutedEventArgs e)
        {
            if (recent_order != null) recent_order();
        }

        private void atoz_Checked(object sender, RoutedEventArgs e)
        {
            if (atoz_order != null) atoz_order();
        }
    }

    public delegate void AToZOrder();
    public delegate void TopOrder();
    public delegate void RecentOrder();
}
