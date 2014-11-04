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
using nature_net.user_controls.v_keyboard;

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for pin_box.xaml
    /// </summary>
    public partial class pin_box : UserControl
    {
        public virtual_numpad numpad;
        public ContentControl numpad_frame;

        public submit_hit submit_clicked;

        int num_digits = 0;
        public int pin;
        public string pin_string = "";
        public bool auto_submit = false;

        public UserControl parent;
        public bool center = true;

        public pin_box()
        {
            InitializeComponent();

            this.Unloaded += new RoutedEventHandler(pin_box_Unloaded);

            digit1.GotFocus += new RoutedEventHandler(digit1_GotFocus);
            //digit1.TextChanged += new TextChangedEventHandler(digit1_TextChanged);
            //digit2.TextChanged += new TextChangedEventHandler(digit2_TextChanged);
            //digit3.TextChanged += new TextChangedEventHandler(digit3_TextChanged);
            //digit4.TextChanged += new TextChangedEventHandler(digit4_TextChanged);
        }

        void pin_box_Unloaded(object sender, RoutedEventArgs e)
        {
            if (numpad_frame != null)
                window_manager.main_canvas.Children.Remove(numpad_frame);
        }

        //void digit1_TextChanged(object sender, TextChangedEventArgs e)
        //{
            
        //}

        public void Reset(bool visible)
        {
            num_digits = 0;
            pin = 0;
            digit1.Text = ""; digit2.Text = ""; digit3.Text = ""; digit4.Text = "";
            pin_string = "";
            if (numpad == null || numpad_frame == null)
                CreateNumpadKeyboard();
            else
                this.UpdateLayout();
            if (visible)
                numpad_frame.Visibility = System.Windows.Visibility.Visible;
            else
                numpad_frame.Visibility = System.Windows.Visibility.Collapsed;
            numpad.MoveAlongWith(parent, center);
        }

        void digit1_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        void number_entered(int n)
        {
            if (num_digits < 4)
            {
                pin = pin * 10 + n;
                pin_string = pin_string + n.ToString();
                num_digits++;
                UpdateFocus(true, true, n.ToString());
            }
        }

        void submit()
        {
            if (numpad_frame != null)
            {
                window_manager.main_canvas.Children.Remove(numpad_frame);
                numpad_frame = null;
                numpad = null;
            }
            if (submit_clicked != null) submit_clicked();
        }

        void backspace()
        {
            if (num_digits > 0)
            {
                num_digits--;
                pin = pin / 10;
                if (pin_string.Length == 1)
                    pin_string = "";
                else
                    pin_string = pin_string.Substring(0, pin_string.Length - 1);
            }
            UpdateFocus(false, true, "");
        }

        public void UpdateFocus(bool forward, bool type, string n)
        {
            int a = num_digits + 1;
            //if (forward) a = a + 1;
            //if (a == 0)
            //{
            //    digit1.Focus();
            //    digit1.SelectAll();
            //    if (!forward && type) digit1.Text = "";
            //}
            if (a == 1)
            {
                digit1.Focus();
                digit1.SelectAll();
                if (!forward && type) digit1.Text = "";
            }
            if (a == 2)
            {
                digit2.Focus();
                digit2.SelectAll();
                if (type && forward) digit1.Text = n;
                if (!forward && type) digit2.Text = "";
            }
            if (a == 3)
            {
                digit3.Focus();
                digit3.SelectAll();
                if (type && forward) digit2.Text = n;
                if (!forward && type) digit3.Text = "";
            }
            if (a == 4)
            {
                digit4.Focus();
                digit4.SelectAll();
                if (type && forward) digit3.Text = n;
                if (!forward && type) digit4.Text = "";
            }
            if (a == 5 && type && forward)
            {
                digit4.Text = n;
                if (auto_submit) this.submit();
            }
        }

        public void UpdateKeyboardPosition()
        {
            if (numpad_frame != null)
            {
                if (numpad != null)
                {
                    if (numpad_frame.Visibility == System.Windows.Visibility.Visible)
                    {
                        this.UpdateLayout();
                        numpad.MoveAlongWith(parent, center);
                    }
                }
            }
        }

        public bool IsEmpty()
        {
            if (num_digits != 4 || pin_string == "")
                return true;
            return false;
        }

        private void CreateNumpadKeyboard()
        {
            numpad_frame = new ContentControl();
            numpad = new virtual_numpad();
            numpad.number_hit_handler = new number_hit(number_entered);
            numpad.submit_hit_handler = new submit_hit(submit);
            numpad.backspace_hit_handler = new backspace_hit(backspace);
            this.numpad_frame.Content = numpad;
            numpad.parent_frame = numpad_frame;
            numpad.window_frame = this.parent;
            this.numpad_frame.Background = new SolidColorBrush(Colors.White);
            window_manager.main_canvas.Children.Add(numpad_frame);
            this.UpdateLayout();
            this.numpad_frame.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
