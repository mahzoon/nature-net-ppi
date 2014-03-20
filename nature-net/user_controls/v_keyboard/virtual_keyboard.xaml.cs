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

        private int[][] rows = new int[5][]{
        new int[14] { 35, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 79 },
        new int[14] { 65, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 49 },
        new int[14] { 75, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 81, 0 },
        new int[14] { 90, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 108, 0, 0 },
        new int[14] { 65, 10, 50, 10, 350, 10, 50, 10, 65, 0, 0, 0, 0, 0}};

        KeyAssignments ka = new KeyAssignments();
        KeyAssignment[][] keys;

        private IVirtualKeyboardInjectable target_window;
        private CommandBinding _backspaceCommandBinding;
        private SoundPlayer click_sound;

        private Dictionary<TouchDevice, Rectangle> rectangles = new Dictionary<TouchDevice, Rectangle>();

        public virtual_keyboard()
        {
            InitializeComponent();
            click_sound = new SoundPlayer();
            click_sound.SoundLocation = configurations.GetAbsolutePath() + configurations.keyboard_click_wav;
            click_sound.Load();
            click_sound.Play();
            keys = ka.Assignments;
            keyboard.TouchDown += new EventHandler<TouchEventArgs>(keyboard_TouchDown);
            keyboard.TouchUp += new EventHandler<TouchEventArgs>(keyboard_TouchUp);
            //this.keyboard.Background = new ImageBrush(configurations.img_keyboard_pic);
            this.keyboard.Source = configurations.img_keyboard_pic;
            this.Loaded += new RoutedEventHandler(virtual_keyboard_Loaded);
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
            ((Image)sender).ReleaseTouchCapture(e.TouchDevice);
        }

        void keyboard_TouchDown(object sender, TouchEventArgs e)
        {
            e.TouchDevice.Capture(sender as IInputElement);
            Point p = e.GetTouchPoint(sender as IInputElement).Position;
            KeyAssignment key_code = get_key(p.X, p.Y, e.TouchDevice);
            if (key_code == null) return;

            click_sound.Play();
            if (key_code.IsALTKey || key_code.IsControlKey) return;
            if (key_code.IsSHIFTKey || key_code.IsCAPSLOCK) { is_shifted = !is_shifted; return; }
            if (key_code.UnshiftedCodePoint == 0x0008) { this.DoBackspace(); return; }
            if (is_shifted)
                this.Inject(((char)key_code.ShiftedCodePoint).ToString());
            else
                this.Inject(((char)key_code.UnshiftedCodePoint).ToString());
        }

        private KeyAssignment get_key(double x, double y, TouchDevice td)
        {
            int row = (int)(y / (button_height + margin_h));
            int sum = rows[row][0];
            int col = 0;
            while (x > sum) { col++; sum = sum + rows[row][col] + margin_w; }
            Rectangle r = new Rectangle();
            r.Stroke = new SolidColorBrush(Colors.Black);
            r.StrokeThickness = 4;
            r.Height = button_height; r.Width = rows[row][col];
            Canvas.SetLeft(r, sum - r.Width);
            Canvas.SetTop(r, row * (button_height + margin_h));
            this.keyboard_canvas.Children.Add(r);
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

        public void MoveAlongWith(UserControl parent)
        {
            if (parent == null) return;
            MatrixTransform parent_matrix = (MatrixTransform)parent.RenderTransform;
            Matrix matrix = new Matrix();
            matrix.M11 = parent_matrix.Matrix.M11; matrix.M12 = parent_matrix.Matrix.M12;
            matrix.M21 = parent_matrix.Matrix.M21; matrix.M22 = parent_matrix.Matrix.M22;
            matrix.OffsetX = parent_matrix.Matrix.OffsetX; matrix.OffsetY = parent_matrix.Matrix.OffsetY;
            double dx = (parent.ActualWidth / 2) - (this.Width / 2);
            matrix.TranslatePrepend(dx, parent.ActualHeight);
            this.RenderTransform = new MatrixTransform(matrix);
        }
    }


}
