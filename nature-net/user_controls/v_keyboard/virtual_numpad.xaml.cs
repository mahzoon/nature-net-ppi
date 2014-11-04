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

namespace nature_net.user_controls.v_keyboard
{
    /// <summary>
    /// Interaction logic for virtual_numpad.xaml
    /// </summary>
    public partial class virtual_numpad : UserControl
    {
        private int button_height = 40;
        //private int button_width_default = 52;
        private int margin_w = 2;
        private int margin_h = 1;

        bool is_shifted = false;

        private int[][] rows = new int[4][]{
        new int[3] { 40, 40, 40 },
        new int[3] { 40, 40, 40 },
        new int[3] { 40, 40, 40 },
        new int[2] { 80, 40 }};

        KeyAssignments ka = new KeyAssignments();
        KeyAssignment[][] keys;

        private IVirtualKeyboardInjectable target_window;
        private CommandBinding _backspaceCommandBinding;
        private SoundPlayer click_sound;

        private Dictionary<TouchDevice, Rectangle> rectangles = new Dictionary<TouchDevice, Rectangle>();

        public number_hit number_hit_handler;
        public backspace_hit backspace_hit_handler;
        public submit_hit submit_hit_handler;

        public ContentControl parent_frame;
        public UserControl window_frame;

        public virtual_numpad()
        {
            InitializeComponent();
            click_sound = new SoundPlayer();
            click_sound.SoundLocation = configurations.GetAbsolutePath() + configurations.keyboard_click_wav;
            click_sound.Load();
            click_sound.Play();
            keys = ka.NumpadAssignments;
            keyboard_canvas.TouchDown += new EventHandler<TouchEventArgs>(keyboard_TouchDown);
            keyboard_canvas.TouchUp += new EventHandler<TouchEventArgs>(keyboard_TouchUp);
            this.keyboard.Source = configurations.img_keyboard_numpad_pic;
            this.Loaded += new RoutedEventHandler(virtual_numpad_Loaded);
        }

        void virtual_numpad_Loaded(object sender, RoutedEventArgs e)
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
            if (key_code.UnshiftedCodePoint == 0x0008) { this.DoBackspace(); return; }
            if (key_code.UnshiftedCodePoint == 0x000A && this.submit_hit_handler != null) { submit_hit_handler(); return; }
            if (is_shifted)
                this.Inject(((char)key_code.ShiftedCodePoint).ToString());
            else
                this.Inject(((char)key_code.UnshiftedCodePoint).ToString());
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
            else
            {
                this.keyboard_canvas.Children.Remove(this.rectangles[td]);
                this.rectangles.Remove(td);
            }
            return keys[row][col];
        }

        protected void Inject(string sWhat)
        {
            if (number_hit_handler != null)
            {
                number_hit_handler(Convert.ToInt32(sWhat));
                return;
            }
            if (target_window != null)
            {
                //target_window.ControlToInjectInto.Focus();
                //Microsoft.Surface.Presentation.Controls.SurfaceTextBox txtTarget = TargetWindow.ControlToInjectInto as Microsoft.Surface.Presentation.Controls.SurfaceTextBox;
                TextBox txtTarget = target_window.ControlToInjectInto as TextBox;
                if (txtTarget != null)
                {
                    //txtTarget.Text.Insert(txtTarget.SelectionStart, sWhat);
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

        public static void ShowKeyboard(IVirtualKeyboardInjectable targetWindow, ref virtual_numpad myPointerToIt)
        {
            if (myPointerToIt != null)
            {
                
            }
            else
            {
                myPointerToIt = new virtual_numpad();
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
            if (backspace_hit_handler != null)
            {
                backspace_hit_handler();
                return;
            }
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
            double dx = 0;
            if (center)
                dx = (parent.ActualWidth / 2) - (this.ActualWidth / 2);
            matrix.TranslatePrepend(dx, parent.ActualHeight);
            this.RenderTransform = new MatrixTransform(matrix);
            if (parent_frame != null)
                window_manager.UpdateZOrder(parent_frame, true);
        }
    }

    public delegate void backspace_hit();
    public delegate void number_hit(int number);
    public delegate void submit_hit();
}
