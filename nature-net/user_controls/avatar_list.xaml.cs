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
    /// Interaction logic for avatar_list.xaml
    /// </summary>
    public partial class avatar_list : UserControl
    {
        public Image return_value;

        public avatar_list()
        {
            InitializeComponent();
            listbox_avatars.Items.Clear();
            foreach (KeyValuePair<string, ImageSource> kvp in window_manager.avatars)
            {
                Image i = new Image();
                i.Source = kvp.Value;
                i.Tag = kvp.Key;
                listbox_avatars.Items.Add(i);
            }
            
            listbox_avatars.Items.Refresh();
            listbox_avatars.UpdateLayout();
            this.UpdateLayout();

            this.listbox_avatars.SelectionChanged += new SelectionChangedEventHandler(listbox_avatars_SelectionChanged);
        }

        void listbox_avatars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Image s = (Image)listbox_avatars.SelectedItem;
            this.Tag = s.Tag;
            this.return_value.Source = s.Source;
        }

        public void MoveAlongWith(UserControl parent)
        {
            if (parent == null) return;
            MatrixTransform parent_matrix = (MatrixTransform)parent.RenderTransform;
            Matrix matrix = new Matrix();
            matrix.M11 = parent_matrix.Matrix.M11; matrix.M12 = parent_matrix.Matrix.M12;
            matrix.M21 = parent_matrix.Matrix.M21; matrix.M22 = parent_matrix.Matrix.M22;
            matrix.OffsetX = parent_matrix.Matrix.OffsetX; matrix.OffsetY = parent_matrix.Matrix.OffsetY;
            double dx = (parent.ActualWidth / 2) - (this.ActualWidth / 2);
            matrix.TranslatePrepend(dx, parent.ActualHeight);
            this.RenderTransform = new MatrixTransform(matrix);
        }

        protected override void OnManipulationBoundaryFeedback(ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
