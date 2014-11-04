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
using System.Security.Cryptography;

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for signup.xaml
    /// </summary>
    public partial class signup : UserControl, IVirtualKeyboardInjectable
    {
        virtual_keyboard keyboard;
        ContentControl keyboard_frame;
        Control focused_textbox;
        public UserControl parent;
        ContentControl avatar_frame;
        public avatar_list avatar_list_control;

        private string valid_email_string = "qwertyuiopasdfghjklmnbvcxz1234567890@_-.+!#$%&'*/=?^`{|}~";
        private string valid_name_string = "qwertyuiopasdfghjklmnbvcxz1234567890";

        public signup()
        {
            InitializeComponent();

            textbox_name.GotFocus += new RoutedEventHandler(textbox_GotFocus);
            textbox_email.GotFocus += new RoutedEventHandler(textbox_GotFocus);
            //textbox_password.GotFocus += new RoutedEventHandler(textbox_GotFocus);
            user_pin.GotFocus += new RoutedEventHandler(user_pin_GotFocus);

            if (!configurations.multi_keyboard)
            {
                textbox_name.LostFocus += new RoutedEventHandler(textbox_LostFocus);
                textbox_email.LostFocus += new RoutedEventHandler(textbox_LostFocus);
                //textbox_password.LostFocus += new RoutedEventHandler(textbox_LostFocus);
                user_pin.LostFocus += new RoutedEventHandler(user_pin_LostFocus);
                avatar_image.LostFocus += new RoutedEventHandler(avatar_image_LostFocus);
            }
            avatar_image.GotFocus += new RoutedEventHandler(avatar_image_GotFocus);

            this.button_submit.PreviewTouchDown += new EventHandler<TouchEventArgs>(button_submit_Click);
            this.button_next1.PreviewTouchDown += new EventHandler<TouchEventArgs>(button_next1_Click);
            this.button_next2.PreviewTouchDown += new EventHandler<TouchEventArgs>(button_next2_Click);
            this.button_back1.PreviewTouchDown += new EventHandler<TouchEventArgs>(button_back1_Click);
            this.button_back2.PreviewTouchDown += new EventHandler<TouchEventArgs>(button_back2_Click);
            this.Unloaded += new RoutedEventHandler(signup_Unloaded);

            this.checkbox_agreement1.PreviewTouchDown += new EventHandler<TouchEventArgs>(checkbox_agreement_PreviewTouchDown);
            this.checkbox_agreement2.PreviewTouchDown += new EventHandler<TouchEventArgs>(checkbox_agreement_PreviewTouchDown);
            this.checkbox_agreement3.PreviewTouchDown += new EventHandler<TouchEventArgs>(checkbox_agreement_PreviewTouchDown);
            this.checkbox_agreement4.PreviewTouchDown += new EventHandler<TouchEventArgs>(checkbox_agreement_PreviewTouchDown);

            this.avatar_image.PreviewTouchDown += new EventHandler<TouchEventArgs>(avatar_image_PreviewTouchDown);
            
            //ScrollViewer scroll = configurations.GetDescendantByType(this.listbox_avatars, typeof(ScrollViewer)) as ScrollViewer;
            this.Height = 490;
            consent_form_1.Visibility = System.Windows.Visibility.Visible;
            //this.form1.Visibility = System.Windows.Visibility.Visible;
            avatar_list_control = new avatar_list();
            avatar_list_control.return_value = this.avatar_image;
            avatar_list_control.Background = Brushes.White;
            avatar_frame = new ContentControl();
            this.avatar_frame.Content = avatar_list_control;
            avatar_list_control.parent_frame = avatar_frame;
            window_manager.main_canvas.Children.Add(avatar_frame);
            avatar_frame.Visibility = System.Windows.Visibility.Hidden;
            avatar_image.Source = configurations.img_choose_avatar_pic;
        }

        void user_pin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!user_pin.numpad_frame.IsFocused)
                if (user_pin.numpad_frame != null)
                    user_pin.numpad_frame.Visibility = System.Windows.Visibility.Collapsed;
        }

        void user_pin_GotFocus(object sender, RoutedEventArgs e)
        {
            avatar_image_LostFocus(null, null);
            textbox_LostFocus(null, null);
            if (user_pin.numpad_frame != null)
                user_pin.numpad_frame.Visibility = System.Windows.Visibility.Visible;
            else
                user_pin.Reset(true);
            user_pin.numpad.MoveAlongWith(parent, true);
        }

        void avatar_image_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!avatar_frame.IsFocused)
                if (avatar_frame != null)
                    avatar_frame.Visibility = System.Windows.Visibility.Collapsed;
        }

        void avatar_image_GotFocus(object sender, RoutedEventArgs e)
        {
            user_pin_LostFocus(null, null);
            textbox_LostFocus(null, null);
            avatar_frame.Visibility = System.Windows.Visibility.Visible;
            avatar_list_control.MoveAlongWith(parent);
        }

        void avatar_image_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            avatar_frame.Visibility = System.Windows.Visibility.Visible;
            avatar_list_control.MoveAlongWith(parent);
            avatar_image.Focus();
        }

        protected override void OnManipulationBoundaryFeedback(ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        void checkbox_agreement_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            c.IsChecked = !c.IsChecked;
        }

        void signup_Unloaded(object sender, RoutedEventArgs e)
        {
            if (keyboard_frame != null)
                window_manager.main_canvas.Children.Remove(keyboard_frame);
            if (avatar_frame != null)
                window_manager.main_canvas.Children.Remove(avatar_frame);
        }

        public void UpdateKeyboardLayout()
        {
            if (keyboard_frame != null)
            {
                if (keyboard != null)
                    if (keyboard_frame.Visibility == System.Windows.Visibility.Visible)
                        keyboard.MoveAlongWith(parent, true);
            }
            if (avatar_frame != null)
            {
                if (avatar_list_control != null)
                    if (avatar_frame.Visibility == System.Windows.Visibility.Visible)
                        avatar_list_control.MoveAlongWith(parent);
            }
            user_pin.UpdateKeyboardPosition();
        }

        public UIElement GetKeyboardFrame()
        {
            return keyboard_frame;
        }

        public UIElement GetNumpadFrame()
        {
            return user_pin.numpad_frame;
        }

        public UIElement GetAvatarFrame()
        {
            return avatar_frame;
        }

        void textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!textbox_email.IsFocused && !textbox_name.IsFocused)// && !textbox_password.IsFocused)
                if (keyboard_frame != null)
                    keyboard_frame.Visibility = System.Windows.Visibility.Collapsed;
        }

        void textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            avatar_image_LostFocus(null, null);
            user_pin_LostFocus(null, null);
            focused_textbox = (Control)sender;
            if (keyboard_frame == null)
                keyboard_frame = new ContentControl();
            virtual_keyboard.ShowKeyboard(this, ref keyboard);
            keyboard.parent_frame = keyboard_frame;
            if (keyboard.validation_checker == null) keyboard.validation_checker = new CharacterValidation(this.ValidateString);
            keyboard_frame.Visibility = System.Windows.Visibility.Visible;
            if (keyboard != null)
            {
                if (this.keyboard_frame.Content == null)
                {
                    this.keyboard_frame.Content = keyboard;
                    //this.keyboard.Background = new SolidColorBrush(Colors.White);
                    this.keyboard_frame.Background = new SolidColorBrush(Colors.White);
                    window_manager.main_canvas.Children.Add(keyboard_frame);
                }
                keyboard.MoveAlongWith(parent, true);
            }
        }

        public void load_window()
        {
            // load avtar listbox
            
            //listbox_avatars.Items.Clear();
            //foreach (KeyValuePair<string, ImageSource> kvp in window_manager.avatars)
            //{
            //    Image i = new Image();
            //    i.Source = kvp.Value;
            //    i.Tag = kvp.Key;
            //    listbox_avatars.Items.Add(i);
            //}
            reset();
        }

        public Control ControlToInjectInto
        {
            get { return this.focused_textbox; }
        }

        private void button_next1_Click(object sender, RoutedEventArgs e)
        {
            reset();
            this.consent_form_1.Visibility = System.Windows.Visibility.Collapsed;
            //this.form1.Visibility = System.Windows.Visibility.Collapsed;
            this.step1.FontWeight = FontWeights.Normal;
            this.step2.FontWeight = FontWeights.ExtraBold;
            this.step3.FontWeight = FontWeights.Normal;

            log.WriteInteractionLog(32, "", ((TouchEventArgs)e).TouchDevice);
        }

        private void button_next2_Click(object sender, RoutedEventArgs e)
        {
            reset();
            this.user_pin.Reset(false);
            if (this.checkbox_agreement1.IsChecked.Value && this.checkbox_agreement2.IsChecked.Value)// && checkbox_agreement3.IsChecked.Value && checkbox_agreement4.IsChecked.Value)
            {
                this.consent_form_2.Visibility = System.Windows.Visibility.Collapsed;
                this.step1.FontWeight = FontWeights.Normal;
                this.step2.FontWeight = FontWeights.Normal;
                this.step3.FontWeight = FontWeights.ExtraBold;
                log.WriteInteractionLog(34, "", ((TouchEventArgs)e).TouchDevice);
            }
            else
            {
                if (!checkbox_agreement1.IsChecked.Value)
                {
                    checkbox_agreement1.BorderBrush = Brushes.Red;
                    checkbox_agreement1.BorderThickness = new Thickness(5);
                    required1.Foreground = Brushes.Red;
                    required1.FontWeight = FontWeights.Bold;
                    log.WriteInteractionLog(33, "Checkbox1 was not checked.", ((TouchEventArgs)e).TouchDevice);
                    return;
                }
                if (!checkbox_agreement2.IsChecked.Value)
                {
                    checkbox_agreement2.BorderBrush = Brushes.Red;
                    checkbox_agreement2.BorderThickness = new Thickness(5);
                    required2.Foreground = Brushes.Red;
                    required2.FontWeight = FontWeights.Bold;
                    log.WriteInteractionLog(33, "Checkbox2 was not checked.", ((TouchEventArgs)e).TouchDevice);
                    return;
                }
            }
        }

        private void button_back1_Click(object sender, RoutedEventArgs e)
        {
            reset();
            this.consent_form_1.Visibility = System.Windows.Visibility.Visible;
            //this.form1.Visibility = System.Windows.Visibility.Visible;
            this.step1.FontWeight = FontWeights.ExtraBold;
            this.step2.FontWeight = FontWeights.Normal;
            this.step3.FontWeight = FontWeights.Normal;
            log.WriteInteractionLog(35, "", ((TouchEventArgs)e).TouchDevice);
        }

        private void button_back2_Click(object sender, RoutedEventArgs e)
        {
            reset();
            this.user_pin.Reset(false);
            if (configurations.multi_keyboard)
            {
                if (keyboard_frame != null)
                    keyboard_frame.Visibility = System.Windows.Visibility.Collapsed;
            }
            this.button_back2.Focus();
            this.consent_form_2.Visibility = System.Windows.Visibility.Visible;
            this.step1.FontWeight = FontWeights.Normal;
            this.step2.FontWeight = FontWeights.ExtraBold;
            this.step3.FontWeight = FontWeights.Normal;
            log.WriteInteractionLog(36, "", ((TouchEventArgs)e).TouchDevice);
        }

        private void button_submit_Click(object sender, RoutedEventArgs e)
        {
            reset();
            desc.Visibility = System.Windows.Visibility.Visible;
            if (textbox_name.Text == "")
            {
                textbox_name.BorderBrush = Brushes.Red;
                textbox_name.BorderThickness = new Thickness(5);
                desc.Text = "Name is empty.";
                textbox_name.Focus();
                log.WriteInteractionLog(37, "Name was empty.", ((TouchEventArgs)e).TouchDevice);
                return;
            }
            if (textbox_email.Text == "")
            {
                textbox_email.BorderBrush = Brushes.Red;
                textbox_email.BorderThickness = new Thickness(5);
                desc.Text = "Email is empty.";
                textbox_email.Focus();
                log.WriteInteractionLog(37, "Email was empty.", ((TouchEventArgs)e).TouchDevice);
                return;
            }
            if (!IsValid(textbox_email.Text))
            {
                textbox_email.BorderBrush = Brushes.Red;
                textbox_email.BorderThickness = new Thickness(5);
                desc.Text = "Enter a valid email address.";
                textbox_email.Focus();
                log.WriteInteractionLog(37, "Email was not valid.", ((TouchEventArgs)e).TouchDevice);
                return;
            }
            if (user_pin.IsEmpty())
            {
                user_pin.BorderBrush = Brushes.Red;
                user_pin.BorderThickness = new Thickness(5);
                desc.Text = "Choose a PIN.";
                user_pin.Focus();
                log.WriteInteractionLog(37, "PIN was empty.", ((TouchEventArgs)e).TouchDevice);
                return;
            }
            if (avatar_list_control.Tag == null)
            {
                avatar_border.BorderBrush = Brushes.Red;
                avatar_border.BorderThickness = new Thickness(5);
                desc.Text = "Please select an avatar.";
                avatar_image.Focus();
                log.WriteInteractionLog(37, "Avatar was empty.", ((TouchEventArgs)e).TouchDevice);
                return;
            }
            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
            List<string> usernames = new List<string>();
            var r = from us in db.Users
                    select us.name;
            if (r != null)
                usernames = r.ToList<string>();
            if (UserExists(usernames, textbox_name.Text))
            {
                textbox_name.BorderBrush = Brushes.Red;
                textbox_name.BorderThickness = new Thickness(5);
                desc.Text = "This name has already been taken, choose another.";
                log.WriteInteractionLog(37, "User exists.", ((TouchEventArgs)e).TouchDevice);
                return;
            }

            User u = new User();
            u.name = textbox_name.Text;
            u.email = textbox_email.Text;
            u.avatar = (string)(avatar_list_control.Tag);
            u.password = user_pin.pin_string;

            //UnicodeEncoding encode = new UnicodeEncoding();
            //byte[] pass_byte = encode.GetBytes(textbox_password.Password);
            //SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            //byte[] pass_hash = sha1.ComputeHash(pass_byte);

            string consent_checkboxes = "";
            if (checkbox_agreement1.IsChecked.Value)
                consent_checkboxes = consent_checkboxes + configurations.GetTextBlockText((TextBlock)(checkbox_agreement1.Content)) + ";" ;
            if (checkbox_agreement2.IsChecked.Value)
                consent_checkboxes = consent_checkboxes + configurations.GetTextBlockText((TextBlock)(checkbox_agreement2.Content)) + ";";
            if (checkbox_agreement3.IsChecked.Value)
                consent_checkboxes = consent_checkboxes + configurations.GetTextBlockText((TextBlock)(checkbox_agreement3.Content)) + ";";
            if (checkbox_agreement4.IsChecked.Value)
                consent_checkboxes = consent_checkboxes + configurations.GetTextBlockText((TextBlock)(checkbox_agreement4.Content));

            u.technical_info = textbox_name.Text + " signed the consent form on " + DateTime.Now.ToString() + "; " + consent_checkboxes;
            u.affiliation = "";
            try
            {
                database_manager.InsertUser(u);
                desc.Text = "Congratulations!";
                //file_manager.add_user_to_googledrive(u.id, u.name, u.avatar);
                window_manager.close_signup_window((window_frame)parent, u.name);
                log.WriteInteractionLog(38, "Username=" + u.name, ((TouchEventArgs)e).TouchDevice);
            }
            catch (Exception ex) { desc.Text = "Could not complete the operation."; log.WriteErrorLog(ex); }
        }

        private void reset()
        {
            textbox_name.BorderBrush = Brushes.LightGray;
            textbox_name.BorderThickness = new Thickness(2);
            textbox_email.BorderBrush = Brushes.LightGray;
            textbox_email.BorderThickness = new Thickness(2);
            user_pin.BorderBrush = Brushes.LightGray;
            user_pin.BorderThickness = new Thickness(2);
            checkbox_agreement1.BorderBrush = Brushes.Gray;
            checkbox_agreement1.BorderThickness = new Thickness(2);
            checkbox_agreement2.BorderBrush = Brushes.Gray;
            checkbox_agreement2.BorderThickness = new Thickness(2);
            avatar_border.BorderBrush = Brushes.LightGray;
            avatar_border.BorderThickness = new Thickness(0);
            desc.Visibility = System.Windows.Visibility.Hidden;
            desc.Text = "Welcome";
            required1.Foreground = Brushes.Red;
            required2.Foreground = Brushes.Red;
            required1.FontWeight = FontWeights.Normal;
            required2.FontWeight = FontWeights.Normal;
            if (avatar_frame.Visibility == System.Windows.Visibility.Visible)
                avatar_frame.Visibility = System.Windows.Visibility.Hidden;
        }

        

        private bool IsValid(string emailaddress)
        {
            try
            {
                System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool ValidateString(string s)
        {
            if (focused_textbox.Name == "textbox_name")
            {
                if (valid_name_string.Contains(s.ToLower()))
                    return true;
                return false;
            }
            if (focused_textbox.Name == "textbox_email")
            {
                if (valid_email_string.Contains(s.ToLower()))
                    return true;
                return false;
            }
            return false;
        }

        public bool UserExists(List<string> current_users, string new_user)
        {
            for (int counter = 0; counter < current_users.Count; counter++)
                if (current_users[counter].ToLower() == new_user.ToLower())
                    return true;
            return false;
        }
    }
}
