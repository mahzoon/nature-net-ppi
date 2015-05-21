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
    /// Interaction logic for help_button.xaml
    /// </summary>
    public partial class help_button : UserControl
    {
        public string filename { get; set; }
        public string caption { get; set; }

        public help_button()
        {
            InitializeComponent();

            this.button_text.FontFamily = new FontFamily(configurations.title_font_name);
            this.button_text.FontSize = configurations.title_font_size - 5;
            this.button_text.Text = configurations.help_text;

            this.Margin = new Thickness(0);
            this.button_text.Margin = new Thickness(0, 0, 5, 0);
        }

        private void button_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            window_manager.open_help_window(filename, caption,
                this.PointToScreen(new Point(0, 0)).X - window_manager.main_canvas.PointToScreen(new Point(0, 0)).X,
                this.PointToScreen(new Point(0, 0)).Y);
        }
    }
}
