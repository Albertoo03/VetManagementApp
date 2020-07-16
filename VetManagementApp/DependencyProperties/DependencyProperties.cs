using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace VetManagementApp.DependencyProperties
{
    #region Dependency properties

    // AutoScroll property
    public static class AutoScrollUtility
    {
        // Logs autoscrolling
        public static bool GetLogsAutoScroll(DependencyObject obj)
        {
            return (bool)obj.GetValue(LogsAutoScrollProperty);
        }

        public static void SetLogsAutoScroll(DependencyObject obj, bool value)
        {
            obj.SetValue(LogsAutoScrollProperty, value);
        }

        public static readonly DependencyProperty LogsAutoScrollProperty =
            DependencyProperty.RegisterAttached("LogsAutoScroll", typeof(bool), typeof(AutoScrollUtility), new PropertyMetadata(false, LogsAutoScrollPropertyChanged));

        private static void LogsAutoScrollPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = d as ScrollViewer;

            if (scrollViewer != null && (bool)e.NewValue)
            {
                scrollViewer.ScrollChanged += LogsScrollViewer_ScrollChanged;   // this event is raised when scrolled, or extent or viewport has changed
                scrollViewer.ScrollToBottom();

            }
            else
            {
                scrollViewer.ScrollChanged -= LogsScrollViewer_ScrollChanged;
            }
        }

        private static void LogsScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0)   // check if content height of scrollviewer has changed
            {
                var scrollViewer = sender as ScrollViewer;
                scrollViewer?.ScrollToBottom();
            }

        }
    }


    public class MultiSelectBindableDataGrid : DataGrid
    {
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(IList), typeof(MultiSelectBindableDataGrid), new PropertyMetadata(default(IList)));

        public new IList SelectedItems
        {
            get { return (IList)GetValue(SelectedItemsProperty); }
            set { throw new Exception("This property is read-only. To bind to it you must use 'Mode=OneWayToSource'."); }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            SetValue(SelectedItemsProperty, base.SelectedItems);
        }
    }
    #endregion
}
