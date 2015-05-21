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
    /// Interaction logic for item_generic.xaml
    /// </summary>
    public partial class item_generic : UserControl
    {

        public item_generic()
        {
            InitializeComponent();

            //Static Configuration Values:
            //this.top_panel.Margin = new Thickness(10, 5, 10, 5);
            this.avatar.Width = configurations.comment_item_avatar_width;
            this.avatar.Height = configurations.comment_item_avatar_width;
            //this.topright_panel.Margin = new Thickness(10, 0, 10, 0);
            //this.desc.Margin = new Thickness(10, 0, 10, 0);
            //this.content.Margin = new Thickness(10, 0, 10, 10);
            this.username.FontFamily = new FontFamily("Segoe UI"); this.username.FontSize = 12;
            this.user_desc.FontFamily = new FontFamily("Segoe UI"); this.user_desc.FontSize = 12;
            this.desc.FontFamily = new FontFamily("Segoe UI"); this.desc.FontSize = 12;
            this.content.FontFamily = new FontFamily("Segoe UI"); this.content.FontSize = 12;

            if (configurations.high_contrast)
            {
                this.Background = Brushes.Green;
            }
            this.img_like.Width = configurations.toolbar_item_width; this.img_dislike.Width = configurations.toolbar_item_width;
            //var brush1 = new ImageBrush();
            //brush1.ImageSource = configurations.img_thumbs_up_icon;
            //this.img_like.Background = brush1;
            //var brush2 = new ImageBrush();
            //brush2.ImageSource = configurations.img_thumbs_down_icon;
            //this.img_dislike.Background = brush2;
            img_like.Source = configurations.img_thumbs_up_icon;
            img_dislike.Source = configurations.img_thumbs_down_icon;
            this.toolbar.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void set_touchevent(avatar_touch_down_handler touch_handler)
        {
            this.avatar.PreviewTouchDown += new EventHandler<TouchEventArgs>(touch_handler);
        }

        public void set_replybutton(reply_clicked reply_handler)
        {
            this.right_panel_border.Visibility = System.Windows.Visibility.Visible;
            this.reply_icon.Source = configurations.img_reply_icon;
            this.reply_button.PreviewTouchDown += new EventHandler<TouchEventArgs>(reply_handler);
            this.reply_button.Tag = this;
        }

        public void set_number(int num, string num_desc)
        {
            this.number.Text = "(" + num.ToString() + " " + num_desc + ")";
        }

        public override string ToString()
        {
            string id = "-1";
            if (this.Tag != null)
                id = ((int)this.Tag).ToString();
            string c = "";
            if (this.content.Text != null)
                c = this.content.Text;
            string source = "";
            if (this.avatar.Source != null)
                source = this.avatar.Source.ToString();
            string result = id + ";" + source + ";" + this.username.Text +
                ";" + this.user_desc.Content + ";" + this.desc.Content + ";" + c + ";" + this.number;
            return result;
        }

        public item_generic get_clone()
        {
            item_generic i = new item_generic();
            i.avatar.Source = this.avatar.Source; i.avatar.Visibility = this.avatar.Visibility;
            i.username.Text = this.username.Text; i.user_desc.Content = this.user_desc.Content;
            i.username.TextDecorations = this.username.TextDecorations;
            i.number.Text = this.number.Text; i.number.Visibility = this.number.Visibility;
            i.user_desc.Visibility = this.user_desc.Visibility;
            i.desc.Content = this.desc.Content; i.content.Text = this.content.Text;
            i.desc.Visibility = this.desc.Visibility;
            i.content.Visibility = this.content.Visibility;
            i.Width = this.Width; i.Tag = this.Tag;
            i.BorderThickness = this.BorderThickness;
            i.BorderBrush = this.BorderBrush;
            i.toolbar.Visibility = this.toolbar.Visibility;
            i.label_like.Content = this.label_like.Content; i.label_dislike.Content = this.label_dislike.Content;
            i.img_like.Tag = this.img_like.Tag; i.img_dislike.Tag = this.img_dislike.Tag;
            return i;
        }

        protected override System.Windows.Media.HitTestResult HitTestCore(System.Windows.Media.PointHitTestParameters hitTestParameters)
        {
            return new PointHitTestResult(this, hitTestParameters.HitPoint);
        }
    }
}
