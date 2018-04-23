using DevExpress.Xpf.Grid;
using System;
using System.Windows;
using System.Windows.Input;

namespace dxSample {
    public class GroupChildSelector : DependencyObject {
        static readonly DependencyProperty ModeProperty = DependencyProperty.RegisterAttached("Mode", typeof(ChildSelectionMode), typeof(GroupChildSelector), new PropertyMetadata(ChildSelectionMode.None, new PropertyChangedCallback(OnModeChanged)));

        public static ChildSelectionMode GetMode(DependencyObject obj) {
            return (ChildSelectionMode)obj.GetValue(ModeProperty);
        }
        public static void SetMode(DependencyObject obj, ChildSelectionMode value) {
            obj.SetValue(ModeProperty, value);
        }

        static void OnModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if(!(d is TableView)) return;
            TableView view = (d as TableView);
            view.PreviewMouseLeftButtonUp += OnPreviewMouseLeftButtonUp;
            view.Grid.GroupRowExpanding += OnGroupRowExpanding;
        }
        static void OnGroupRowExpanding(object sender, RowAllowEventArgs e) {
            TableView view = (e.Source as TableView);
            view.BeginSelection();
            SelectChild(view, e.RowHandle);
            view.EndSelection();
        }
        static void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            TableView view = (e.Source as TableView);
            if(view!=null) {
                TableViewHitInfo hitInfo = view.CalcHitInfo(e.OriginalSource as DependencyObject);
                if(hitInfo.InRow && view.Grid.IsGroupRowHandle(hitInfo.RowHandle)) {
                   view.BeginSelection();
                   SelectChild(view, hitInfo.RowHandle);
                   view.EndSelection();
                }
            }
        }
        static void SelectChild(TableView view, int groupRowHandle) {
            int childRowCount = view.Grid.GetChildRowCount(groupRowHandle);
            view.BeginSelection();
            for(int i = 0; i < childRowCount; i++) {
                int childRowHandle = view.Grid.GetChildRowHandle(groupRowHandle, i);
                if(GetMode(view) == ChildSelectionMode.Hierarchical && view.Grid.IsGroupRowHandle(childRowHandle) && view.Grid.IsGroupRowExpanded(childRowHandle)) {
                    SelectChild(view, childRowHandle);
                }
                view.SelectRow(childRowHandle);
            }
            view.EndSelection();
        }
    }

    public enum ChildSelectionMode {
        None,
        Child,
        Hierarchical,
    }
}
