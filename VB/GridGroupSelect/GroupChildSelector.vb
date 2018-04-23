Imports DevExpress.Xpf.Grid
Imports System
Imports System.Windows
Imports System.Windows.Input

Namespace dxSample
    Public Class GroupChildSelector
        Inherits DependencyObject

        Private Shared ReadOnly ModeProperty As DependencyProperty = DependencyProperty.RegisterAttached("Mode", GetType(ChildSelectionMode), GetType(GroupChildSelector), New PropertyMetadata(ChildSelectionMode.None, New PropertyChangedCallback(AddressOf OnModeChanged)))

        Public Shared Function GetMode(ByVal obj As DependencyObject) As ChildSelectionMode
            Return DirectCast(obj.GetValue(ModeProperty), ChildSelectionMode)
        End Function
        Public Shared Sub SetMode(ByVal obj As DependencyObject, ByVal value As ChildSelectionMode)
            obj.SetValue(ModeProperty, value)
        End Sub

        Private Shared Sub OnModeChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            If Not (TypeOf d Is TableView) Then
                Return
            End If
            Dim view As TableView = (TryCast(d, TableView))
            AddHandler view.PreviewMouseLeftButtonUp, AddressOf OnPreviewMouseLeftButtonUp
            AddHandler view.Grid.GroupRowExpanding, AddressOf OnGroupRowExpanding
        End Sub
        Private Shared Sub OnGroupRowExpanding(ByVal sender As Object, ByVal e As RowAllowEventArgs)
            Dim grid = DirectCast(sender, GridControl)
            grid.BeginSelection()
            SelectChild(grid, e.RowHandle)
            grid.EndSelection()
        End Sub
        Private Shared Sub OnPreviewMouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim view As TableView = (TryCast(e.Source, TableView))
            If view IsNot Nothing Then
                Dim hitInfo As TableViewHitInfo = view.CalcHitInfo(TryCast(e.OriginalSource, DependencyObject))
                If hitInfo.InRow AndAlso view.Grid.IsGroupRowHandle(hitInfo.RowHandle) Then
                    view.Grid.BeginSelection()
                    SelectChild(view.Grid, hitInfo.RowHandle)
                    view.Grid.EndSelection()
                End If
            End If
        End Sub
        Private Shared Sub SelectChild(ByVal grid As GridControl, ByVal groupRowHandle As Integer)
            Dim childRowCount As Integer = grid.GetChildRowCount(groupRowHandle)
            grid.BeginSelection()
            For i As Integer = 0 To childRowCount - 1
                Dim childRowHandle As Integer = grid.GetChildRowHandle(groupRowHandle, i)
                If GetMode(grid.View) = ChildSelectionMode.Hierarchical AndAlso grid.IsGroupRowHandle(childRowHandle) AndAlso grid.IsGroupRowExpanded(childRowHandle) Then
                    SelectChild(grid, childRowHandle)
                End If
                grid.SelectItem(childRowHandle)
            Next i
            grid.EndSelection()
        End Sub
    End Class

    Public Enum ChildSelectionMode
        None
        Child
        Hierarchical
    End Enum
End Namespace
