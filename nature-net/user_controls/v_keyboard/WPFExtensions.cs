using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Controls.Primitives;

namespace nature_net.user_controls
{
    public static class WPFExtensions
    {

        #region TextBox extension method InsertText
        /// <summary>
        /// Insert the given text into this TextBox at the current CaretIndex, and replacing any already-selected text.
        /// </summary>
        /// <param name="textbox">The TextBox to insert the new text into</param>
        /// <param name="sTextToInsert">The text to insert into this TextBox</param>
        public static void InsertText(this System.Windows.Controls.TextBox textbox, string sTextToInsert)
        {
            int iCaretIndex = textbox.CaretIndex;
            int iOriginalSelectionLength = textbox.SelectionLength;
            string sOriginalContent = textbox.Text;
            textbox.SelectedText = sTextToInsert;
            if (iOriginalSelectionLength > 0)
            {
                textbox.SelectionLength = 0;
            }
            textbox.CaretIndex = iCaretIndex + 1;
        }
        #endregion

        #region RichTextBox extension method InsertText
        /// <summary>
        /// Insert the given text into this RichTextBox at the current CaretPosition, and replacing any already-selected text.
        /// </summary>
        /// <param name="richTextBox">The RichTextBox to insert the new text into</param>
        /// <param name="sTextToInsert">The text to insert into this RichTextBox</param>
        public static void InsertText(this System.Windows.Controls.RichTextBox richTextBox, string sTextToInsert)
        {
            if (!String.IsNullOrEmpty(sTextToInsert))
            {
                richTextBox.BeginChange();
                if (richTextBox.Selection.Text != string.Empty)
                {
                    richTextBox.Selection.Text = string.Empty;
                }
                TextPointer tp = richTextBox.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
                richTextBox.CaretPosition.InsertTextInRun(sTextToInsert);
                richTextBox.CaretPosition = tp;
                richTextBox.EndChange();
                Keyboard.Focus(richTextBox);
            }
        }
        #endregion

        #region TextBox extension method TextTrimmed
        /// <summary>
        /// This is just a convenience extension-method to simplify the getting of strings
        /// from a WPF TextBox.
        /// It was a pain in da butt, having to remember to test for nulls, whitespace, etc.
        /// Now, all you have to do is check the .Length
        /// </summary>
        /// <param name="textbox">The WPF TextBox to get the Text from</param>
        /// <returns>If the TextBox was empty, then "" (empty string) otherwise the Text with leading and trailing whitespace trimmed</returns>
        public static string TextTrimmed(this System.Windows.Controls.TextBox textbox)
        {
            string sText = textbox.Text;
            if (String.IsNullOrEmpty(sText))
            {
                return "";
            }
            else
            {
                return sText.Trim();
            }
        }
        #endregion

        #region Window extension method MoveAsAGroup

        public static void MoveAsAGroup(this Window me,
                                        double desiredXDisplacement, double desiredYDisplacement,
                                        ref bool isIgnoringLocationChangedEvent)
        {
            // Ensure we don't recurse when we reposition.
            isIgnoringLocationChangedEvent = true;

            Window windowToMoveWith = me.Owner;

            // Try to prevent me from sliding off the screen horizontally.
            double bitToShow = 32;
            double leftLimit = SystemParameters.VirtualScreenLeft - me.Width + bitToShow;
            double rightLimit = SystemParameters.VirtualScreenWidth - bitToShow;
            bool notTooMuchXDisplacement = Math.Abs(me.Left - windowToMoveWith.Left) < Math.Abs(desiredXDisplacement);
            if (me.Left >= rightLimit && notTooMuchXDisplacement)
            {
                // bumping against the right.
                me.Left = rightLimit;
            }
            else if (me.Left <= leftLimit && notTooMuchXDisplacement)
            {
                // bumping against the left.
                me.Left = leftLimit;
            }
            else // it's cool - just slide along with the other window.
            {
                me.Left = windowToMoveWith.Left + desiredXDisplacement;
            }

            // Try to prevent me from sliding off the screen vertically.
            double topLimit = SystemParameters.VirtualScreenTop - me.Height + bitToShow;
            double bottomLimit = SystemParameters.VirtualScreenTop + SystemParameters.VirtualScreenHeight - bitToShow;
            bool notTooMuchYDisplacement = Math.Abs(me.Top - windowToMoveWith.Top) < Math.Abs(desiredYDisplacement);
            if (me.Top <= topLimit && notTooMuchYDisplacement)
            {
                // bumping up against the top.
                //Console.WriteLine("setting to topLimit of " + topLimit);
                me.Top = topLimit;
            }
            else if (me.Top >= bottomLimit && notTooMuchYDisplacement)
            {
                // bumping against the bottom.
                me.Top = bottomLimit;
            }
            else // it's cool - just slide along with the other window.
            {
                me.Top = windowToMoveWith.Top + desiredYDisplacement;
            }

            // Reset the handler for the LocationChanged event.
            isIgnoringLocationChangedEvent = false;
        }
        #endregion
    }

    public static class ItemsControlExtensions
    {
        public static void ScrollToCenterOfView(this ItemsControl itemsControl, object item)
        {
            // Scroll immediately if possible
            if (!itemsControl.TryScrollToCenterOfView(item))
            {
                // Otherwise wait until everything is loaded, then scroll
                if (itemsControl is ListBox) ((ListBox)itemsControl).ScrollIntoView(item);
                itemsControl.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new System.Action(() =>
                {
                    itemsControl.TryScrollToCenterOfView(item);
                }));
            }
        }

        private static bool TryScrollToCenterOfView(this ItemsControl itemsControl, object item)
        {
            // Find the container
            var container = itemsControl.ItemContainerGenerator.ContainerFromItem(item) as UIElement;
            if (container == null) return false;

            // Find the ScrollContentPresenter
            ScrollContentPresenter presenter = null;
            for (Visual vis = container; vis != null && vis != itemsControl; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if ((presenter = vis as ScrollContentPresenter) != null)
                    break;
            if (presenter == null) return false;

            // Find the IScrollInfo
            var scrollInfo =
                !presenter.CanContentScroll ? presenter :
                presenter.Content as IScrollInfo ??
                FirstVisualChild(presenter.Content as ItemsPresenter) as IScrollInfo ??
                presenter;

            // Compute the center point of the container relative to the scrollInfo
            Size size = container.RenderSize;
            Point center = container.TransformToAncestor((Visual)scrollInfo).Transform(new Point(size.Width / 2, size.Height / 2));
            center.Y += scrollInfo.VerticalOffset;
            center.X += scrollInfo.HorizontalOffset;

            // Adjust for logical scrolling
            if (scrollInfo is StackPanel || scrollInfo is VirtualizingStackPanel)
            {
                double logicalCenter = itemsControl.ItemContainerGenerator.IndexFromContainer(container) + 0.5;
                Orientation orientation = scrollInfo is StackPanel ? ((StackPanel)scrollInfo).Orientation : ((VirtualizingStackPanel)scrollInfo).Orientation;
                if (orientation == Orientation.Horizontal)
                    center.X = logicalCenter;
                else
                    center.Y = logicalCenter;
            }
            itemsControl.Tag = center.Y - CenteringOffset(center.Y, scrollInfo.ViewportHeight, scrollInfo.ExtentHeight);
            // Scroll the center of the container to the center of the viewport
            if (scrollInfo.CanVerticallyScroll) scrollInfo.SetVerticalOffset(CenteringOffset(center.Y, scrollInfo.ViewportHeight, scrollInfo.ExtentHeight));
            if (scrollInfo.CanHorizontallyScroll) scrollInfo.SetHorizontalOffset(CenteringOffset(center.X, scrollInfo.ViewportWidth, scrollInfo.ExtentWidth));
            return true;
        }

        private static double CenteringOffset(double center, double viewport, double extent)
        {
            return Math.Min(extent - viewport, Math.Max(0, center - viewport / 2));
        }
        private static DependencyObject FirstVisualChild(Visual visual)
        {
            if (visual == null) return null;
            if (VisualTreeHelper.GetChildrenCount(visual) == 0) return null;
            return VisualTreeHelper.GetChild(visual, 0);
        }
    }
}
