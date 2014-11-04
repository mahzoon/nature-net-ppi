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
using System.Media;

namespace nature_net.user_controls
{
    /// <summary>
    /// Interaction logic for virtual_keyboard.xaml
    /// </summary>
    public partial class virtual_keyboard : UserControl
    {
        private int button_height = 40;
        //private int button_width_default = 40;
        private int margin_w = 2;
        private int margin_h = 1;

        bool is_shifted = false;
        bool is_first_shift = false;

        private int[][] rows = new int[5][]{
        new int[14] { 45, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 60 },
        new int[14] { 65, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40 },
        new int[14] { 75, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 75, 0 },
        new int[14] { 95, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 90, 0, 0 },
        new int[14] { 130, 0, 0, 0, 350, 0, 0, 0, 0, 0, 0, 0, 0, 0}};

        KeyAssignments ka = new KeyAssignments();
        KeyAssignment[][] keys;

        private IVirtualKeyboardInjectable target_window;
        private CommandBinding _backspaceCommandBinding;
        private SoundPlayer click_sound;

        private Rectangle capslock_rectangle;
        private Rectangle shift_rectangle;

        private Dictionary<TouchDevice, Rectangle> rectangles = new Dictionary<TouchDevice, Rectangle>();

        public InitialTextCheck init_text_checker;
        public CharacterValidation validation_checker;

        public ContentControl parent_frame;
        public UserControl window_frame;

        public virtual_keyboard()
        {
            InitializeComponent();
            click_sound = new SoundPlayer();
            click_sound.SoundLocation = configurations.GetAbsolutePath() + configurations.keyboard_click_wav;
            click_sound.Load();
            click_sound.Play();
            keys = ka.Assignments;
            keyboard_canvas.TouchDown += new EventHandler<TouchEventArgs>(keyboard_TouchDown);
            keyboard_canvas.TouchUp += new EventHandler<TouchEventArgs>(keyboard_TouchUp);
            //this.keyboard.Background = new ImageBrush(configurations.img_keyboard_pic);
            this.keyboard.Source = configurations.img_keyboard_pic;
            this.Loaded += new RoutedEventHandler(virtual_keyboard_Loaded);

            capslock_rectangle = new Rectangle();
            SolidColorBrush s = new SolidColorBrush();
            s.Color = Colors.LightBlue; s.Opacity = 0.5;
            capslock_rectangle.Fill = s;
            capslock_rectangle.Height = button_height;
            capslock_rectangle.Width = rows[2][0];
            Canvas.SetLeft(capslock_rectangle, 0);
            Canvas.SetTop(capslock_rectangle, 2 * (button_height + margin_h) + margin_h);

            shift_rectangle = new Rectangle();
            shift_rectangle.Fill = s;
            shift_rectangle.Height = button_height;
        }

        void virtual_keyboard_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateLayout();
        }

        void keyboard_TouchUp(object sender, TouchEventArgs e)
        {
            if (rectangles.ContainsKey(e.TouchDevice))
            {
                this.keyboard_canvas.Children.Remove(this.rectangles[e.TouchDevice]);
                this.rectangles.Remove(e.TouchDevice);
            }
            ((Canvas)sender).ReleaseTouchCapture(e.TouchDevice);
        }

        void keyboard_TouchDown(object sender, TouchEventArgs e)
        {
            e.TouchDevice.Capture(sender as IInputElement);
            Point p = e.GetTouchPoint(sender as IInputElement).Position;
            KeyAssignment key_code = get_key(p.X, p.Y, e.TouchDevice);
            if (key_code == null) return;

            click_sound.Play();
            if (key_code.IsALTKey || key_code.IsControlKey) return;
            if (key_code.IsCAPSLOCK)
            {
                if (!is_shifted)
                    this.keyboard.Source = configurations.img_keyboard_caps_pic;
                else
                    this.keyboard.Source = configurations.img_keyboard_pic;
                is_shifted = !is_shifted;
                return;
            }
            if (key_code.IsSHIFTKey && is_shifted)
            { 
                is_shifted = false; is_first_shift = false;
                this.keyboard.Source = configurations.img_keyboard_pic;
                return;
            }
            if (key_code.IsSHIFTKey && is_first_shift)
            {
                is_shifted = true;
                is_first_shift = false;
                this.keyboard.Source = configurations.img_keyboard_caps_pic;
                return;
            }
            if (key_code.IsSHIFTKey && !is_first_shift)
            {
                is_first_shift = true;
                this.keyboard.Source = configurations.img_keyboard_shift_pic;
                return;
            }
            if (key_code.UnshiftedCodePoint == 0x0008) { this.DoBackspace(); return; }
            if (key_code.UnshiftedCodePoint == 0x0020 || key_code.UnshiftedCodePoint == 0x000A || key_code.UnshiftedCodePoint == 0x0009)
            { this.Inject(((char)key_code.UnshiftedCodePoint).ToString()); }
            else
            {
                if (is_shifted)
                    this.Inject(((char)key_code.ShiftedCodePoint).ToString());
                else
                {
                    if (is_first_shift)
                    {
                        this.Inject(((char)key_code.ShiftedCodePoint).ToString());
                        is_first_shift = false;
                        this.keyboard.Source = configurations.img_keyboard_pic;
                    }
                    else
                        this.Inject(((char)key_code.UnshiftedCodePoint).ToString());
                }
            }
            if (parent_frame != null)
            {
                window_manager.UpdateZOrder(parent_frame, true);
            }
            if (window_frame != null)
            {
                try { ((window_frame)window_frame).postpone_killer_timer(true); }
                catch (Exception) { }
                try { ((image_frame)window_frame).postpone_killer_timer(true); }
                catch (Exception) { }
            }
        }

        private KeyAssignment get_key(double x, double y, TouchDevice td)
        {
            y = y - margin_h - 5; if (y < 0) y = 0;
            int row = (int)(y / (button_height));// + margin_h));
            if (row > rows.Count() - 1) row = rows.Count() - 1;
            int sum = rows[row][0];
            int col = 0;
            while (x > sum)
            {
                col++;
                if (col == rows[row].Count())
                    col--;
                sum = sum + rows[row][col] + margin_w;
            }
            Rectangle r = new Rectangle();
            r.Stroke = new SolidColorBrush(Colors.Black);
            r.StrokeThickness = 4;
            r.Height = button_height; r.Width = rows[row][col];
            Canvas.SetLeft(r, sum - r.Width);
            Canvas.SetTop(r, row * (button_height + margin_h));
            this.keyboard_canvas.Children.Add(r);
            if (!this.rectangles.ContainsKey(td))
                this.rectangles.Add(td, r);

            return keys[row][col];
        }

        protected void Inject(string sWhat)
        {
            if (target_window != null)
            {
                //target_window.ControlToInjectInto.Focus();
                //Microsoft.Surface.Presentation.Controls.SurfaceTextBox txtTarget = TargetWindow.ControlToInjectInto as Microsoft.Surface.Presentation.Controls.SurfaceTextBox;
                TextBox txtTarget = target_window.ControlToInjectInto as TextBox;
                if (txtTarget != null)
                {
                    //txtTarget.Text.Insert(txtTarget.SelectionStart, sWhat);
                    if (validation_checker != null)
                        if (!validation_checker(sWhat))
                            return;
                    if (init_text_checker != null)
                        if (init_text_checker())
                            txtTarget.Text = "";
                    txtTarget.InsertText(sWhat);
                }
                else
                {
                    RichTextBox richTextBox = target_window.ControlToInjectInto as RichTextBox;
                    if (richTextBox != null)
                    {
                        richTextBox.InsertText(sWhat);
                    }
                    else // let's hope it's an IInjectableControl
                    {
                        PasswordBox passbox = target_window.ControlToInjectInto as PasswordBox;
                        if (passbox != null)
                        {
                            passbox.Password += sWhat;
                            passbox.SelectAll();
                        }
                        else
                        {
                            //IInjectableControl targetControl = TargetWindow.ControlToInjectInto as IInjectableControl;
                            //if (targetControl != null)
                            //{
                            //    targetControl.InsertText(sWhat);
                            //}
                        }
                    }
                    //else   // if you have other text-entry controls such as a rich-text box, include them here.
                    //{
                    //    FsWpfControls.FsRichTextBox.FsRichTextBox txtrTarget = TargetWindow.ControlToInjectInto as FsWpfControls.FsRichTextBox.FsRichTextBox;
                    //    if (txtrTarget != null)
                    //    {
                    //        txtrTarget.InsertThis(sWhat);
                    //    }
                }
            }
        }

        public static void ShowKeyboard(IVirtualKeyboardInjectable targetWindow, ref virtual_keyboard myPointerToIt)
        {
            if (myPointerToIt != null)
            {
                
            }
            else
            {
                myPointerToIt = new virtual_keyboard();
                myPointerToIt.ShowIt(targetWindow);
            }
        }

        private void ShowIt(IVirtualKeyboardInjectable targetWindow)
        {
            this.target_window = targetWindow;
            if (target_window != null)
                target_window.ControlToInjectInto.Focus();
        }

        public void DoBackspace()
        {
            if (target_window != null)
            {
                target_window.ControlToInjectInto.Focus();
                TextBox txtTarget = target_window.ControlToInjectInto as TextBox;
                if (txtTarget != null)
                {
                    // Use the built-in Backspace command. Create it only once, and remove it when this VirtualKeyboard window is closing.
                    if (_backspaceCommandBinding == null)
                    {
                        _backspaceCommandBinding = new CommandBinding(EditingCommands.Backspace);
                        txtTarget.CommandBindings.Add(_backspaceCommandBinding);
                    }
                    _backspaceCommandBinding.Command.Execute(null);

                    // This was how I did the Backspace operation without using the command..
                    //int iCaretIndex = txtTarget.CaretIndex;
                    //if (iCaretIndex > 0)
                    //{
                    //    string sOriginalContent = txtTarget.Text;
                    //    int nLength = sOriginalContent.Length;
                    //    if (iCaretIndex >= nLength)
                    //    {
                    //        txtTarget.Text = sOriginalContent.Substring(0, nLength - 1);
                    //    }
                    //    else
                    //    {
                    //        string sPartBeforeCaret = sOriginalContent.Substring(0, iCaretIndex - 1);
                    //        string sPartAfterCaret = sOriginalContent.Substring(iCaretIndex, nLength - iCaretIndex);
                    //        txtTarget.Text = sPartBeforeCaret + sPartAfterCaret;
                    //    }
                    //    txtTarget.CaretIndex = iCaretIndex - 1;
                    //}
                }
                else // let's hope it's an IInjectableControl
                {
                    PasswordBox txtTarget_p = target_window.ControlToInjectInto as PasswordBox;
                    if (txtTarget_p != null)
                    {
                        // Use the built-in Backspace command. Create it only once, and remove it when this VirtualKeyboard window is closing.
                        if (_backspaceCommandBinding == null)
                        {
                            _backspaceCommandBinding = new CommandBinding(EditingCommands.Backspace);
                            txtTarget_p.CommandBindings.Add(_backspaceCommandBinding);
                        }
                        _backspaceCommandBinding.Command.Execute(null);
                    }
                    else
                    {
                        //IInjectableControl targetControl = TargetWindow.ControlToInjectInto as IInjectableControl;
                        //if (targetControl != null)
                        //{
                        //    targetControl.Backspace();
                        //}
                        //else
                        //{
                        //    FsWpfControls.FsRichTextBox.FsRichTextBox txtrTarget = TargetWindow.ControlToInjectInto as FsWpfControls.FsRichTextBox.FsRichTextBox;
                        //    if (txtrTarget != null)
                        //    {
                        //        txtrTarget.InsertThis(chWhat.ToString());
                        //    }
                    }
                }
            }
            //textbox.Text = sOriginalContent.Insert(iCaretIndex, sStuffToInsert);
        }

        public void MoveAlongWith(UserControl parent, bool center)
        {
            if (parent == null) return;
            MatrixTransform parent_matrix = (MatrixTransform)parent.RenderTransform;
            Matrix matrix = new Matrix();
            matrix.M11 = parent_matrix.Matrix.M11; matrix.M12 = parent_matrix.Matrix.M12;
            matrix.M21 = parent_matrix.Matrix.M21; matrix.M22 = parent_matrix.Matrix.M22;
            matrix.OffsetX = parent_matrix.Matrix.OffsetX; matrix.OffsetY = parent_matrix.Matrix.OffsetY;
            double dx =0;
            if (center)
                dx = (parent.ActualWidth / 2) - (this.Width / 2);
            matrix.TranslatePrepend(dx, parent.ActualHeight);
            this.RenderTransform = new MatrixTransform(matrix);
            if (parent_frame != null)
                window_manager.UpdateZOrder(parent_frame, true);
        }
    }

    public delegate bool InitialTextCheck();
    public delegate bool CharacterValidation(string c);
}
