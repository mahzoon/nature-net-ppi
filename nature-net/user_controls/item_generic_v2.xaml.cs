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
    /// Interaction logic for item_generic_v2.xaml
    /// </summary>
    public partial class item_generic_v2 : UserControl
    {
        public int top_value;// it is used for sorting

        public item_generic_v2()
        {
            InitializeComponent();

            this.title.FontSize = configurations.title_font_size; this.title.FontFamily = new FontFamily(configurations.title_font_name);
            this.number_icon.Source = configurations.img_change_view_stack_icon;
            this.number_icon.Margin = new Thickness(5, 5, 0, 0);
            this.number.Margin = new Thickness(0, 5, 0, 0);
            right_panel_border.BorderBrush = configurations.right_panel_border_color;
            right_panel_border.BorderThickness = new Thickness(1, 0, 0, 0);
            this.title.Margin = new Thickness(7, 0, 7, 0);
            this.item_border.BorderThickness = new Thickness(0, 0, 0, 1);
            this.right_panel_border.Background = configurations.right_panel_background;
            right_panel.Width = configurations.right_panel_width;
        }

        public void set_like_handler()
        {
            thumbs_up tu = new thumbs_up(this.like_touched);
            this.avatar.PreviewTouchDown += new EventHandler<TouchEventArgs>(tu);
        }

        public void set_touchevent(avatar_touch_down_handler touch_handler)
        {
            this.avatar.PreviewTouchDown += new EventHandler<TouchEventArgs>(touch_handler);
        }

        public override string ToString()
        {
            string id = "-1";
            if (this.Tag != null)
                id = ((int)this.Tag).ToString();
            string c = "";
            if (this.description.Text != null)
                c = this.description.Text;
            string source = "";
            if (this.user_info_icon.Source != null)
                source = this.user_info_icon.Source.ToString();
            else
                if (this.avatar.Source != null)
                    source = this.avatar.Source.ToString();
            string result = id + ";" + source + ";" + this.title.Text +
                ";" + this.txt_level2.Text + ";" + this.user_info_name.Text + ";" + c + ";" + this.number.Text + ";" + this.num_likes.Content.ToString();
            return result;
        }

        public item_generic_v2 get_clone()
        {
            item_generic_v2 i = new item_generic_v2();
            i.avatar.Source = this.avatar.Source; i.avatar.Visibility = this.avatar.Visibility; i.avatar.Width = this.avatar.Width; i.avatar.Height = this.avatar.Height; i.avatar.Tag = this.avatar.Tag;
            i.title.Text = this.title.Text; i.description.Text = this.description.Text; i.description.Visibility = this.description.Visibility;
            i.title.FontFamily = this.title.FontFamily; i.title.FontSize = this.title.FontSize; i.FontWeight = this.FontWeight;
            i.number.Text = this.number.Text; i.number.Visibility = this.number.Visibility;
            i.number_icon.Source = this.number_icon.Source; i.number_icon.Visibility = this.number_icon.Visibility;
            i.Width = this.Width; i.Tag = this.Tag;
            i.BorderThickness = this.BorderThickness;
            i.BorderBrush = this.BorderBrush;
            i.txt_level1.Text = this.txt_level1.Text; i.txt_level1.Visibility = this.txt_level1.Visibility; i.txt_level1.Margin = this.txt_level1.Margin;
            i.txt_level2.Text = this.txt_level2.Text; i.txt_level2.Visibility = this.txt_level2.Visibility; i.txt_level2.Margin = this.txt_level2.Margin;
            i.txt_level3.Text = this.txt_level3.Text; i.txt_level3.Visibility = this.txt_level3.Visibility; i.txt_level3.Margin = this.txt_level3.Margin;
            i.center_panel.VerticalAlignment = this.center_panel.VerticalAlignment;
            i.left_panel.Visibility = this.left_panel.Visibility;
            i.right_panel.Width = this.right_panel.Width;
            i.title.Margin = this.title.Margin;
            i.description.Margin = this.description.Margin;
            i.user_info.Visibility = this.user_info.Visibility;
            i.user_info_date.Text = this.user_info_date.Text; i.user_info_date.Margin = this.user_info_date.Margin;
            i.user_info_icon.Source = this.user_info_icon.Source; i.user_info_icon.Margin = this.user_info_icon.Margin;
            i.user_info_name.Text = this.user_info_name.Text;i.user_info_name.Margin = this.user_info_name.Margin;
            i.num_likes.Content = this.num_likes.Content; i.num_likes.Tag = this.num_likes.Tag;
            i.top_value = this.top_value;
            return i;
        }

        protected override System.Windows.Media.HitTestResult HitTestCore(System.Windows.Media.PointHitTestParameters hitTestParameters)
        {
            return new PointHitTestResult(this, hitTestParameters.HitPoint);
        }

        public void like_touched(object sender, TouchEventArgs te)
        {
            this.num_likes.Content = (Convert.ToInt32(this.num_likes.Content) + 1).ToString();
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            Feedback f = new Feedback();
            f.note = "true"; f.date = DateTime.Now; f.type_id = 2; f.user_id = 0; f.parent_id = 0;
            f.object_type = "nature_net.Contribution"; f.object_id = Convert.ToInt32((string)this.Tag);
            db.Feedbacks.InsertOnSubmit(f);
            db.SubmitChanges();
            window_manager.load_design_ideas();
            te.Handled = true;
        }
    }
}
