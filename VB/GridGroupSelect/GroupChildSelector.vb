﻿Imports Microsoft.VisualBasic
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Windows
Imports System.Windows.Input

Namespace dxSample
	Public Class GroupChildSelector
		Inherits DependencyObject
		Private Shared ReadOnly ModeProperty As DependencyProperty = DependencyProperty.RegisterAttached("Mode", GetType(ChildSelectionMode), GetType(GroupChildSelector), New PropertyMetadata(ChildSelectionMode.None, New PropertyChangedCallback(AddressOf OnModeChanged)))

		Public Shared Function GetMode(ByVal obj As DependencyObject) As ChildSelectionMode
			Return CType(obj.GetValue(ModeProperty), ChildSelectionMode)
		End Function
		Public Shared Sub SetMode(ByVal obj As DependencyObject, ByVal value As ChildSelectionMode)
			obj.SetValue(ModeProperty, value)
		End Sub

		Private Shared Sub OnModeChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
			If Not(TypeOf d Is TableView) Then
				Return
			End If
			Dim view As TableView = (TryCast(d, TableView))
			AddHandler view.PreviewMouseLeftButtonUp, AddressOf OnPreviewMouseLeftButtonUp
			AddHandler view.Grid.GroupRowExpanding, AddressOf OnGroupRowExpanding
		End Sub
		Private Shared Sub OnGroupRowExpanding(ByVal sender As Object, ByVal e As RowAllowEventArgs)
			Dim view As TableView = (TryCast(e.Source, TableView))
			view.BeginSelection()
			SelectChild(view, e.RowHandle)
			view.EndSelection()
		End Sub
		Private Shared Sub OnPreviewMouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
			Dim view As TableView = (TryCast(e.Source, TableView))
                                          If view IsNot Nothing Then			
                                                 Dim hitInfo As TableViewHitInfo = view.CalcHitInfo(TryCast(e.OriginalSource, DependencyObject))
			       If hitInfo.InRow AndAlso view.Grid.IsGroupRowHandle(hitInfo.RowHandle) Then
				view.BeginSelection()
				SelectChild(view, hitInfo.RowHandle)
				view.EndSelection()
			       End If
                                          End If
		End Sub
		Private Shared Sub SelectChild(ByVal view As TableView, ByVal groupRowHandle As Integer)
			Dim childRowCount As Integer = view.Grid.GetChildRowCount(groupRowHandle)
			view.BeginSelection()
			For i As Integer = 0 To childRowCount - 1
				Dim childRowHandle As Integer = view.Grid.GetChildRowHandle(groupRowHandle, i)
				If GetMode(view) = ChildSelectionMode.Hierarchical AndAlso view.Grid.IsGroupRowHandle(childRowHandle) AndAlso view.Grid.IsGroupRowExpanded(childRowHandle) Then
					SelectChild(view, childRowHandle)
				End If
				view.SelectRow(childRowHandle)
			Next i
			view.EndSelection()
		End Sub
	End Class

	Public Enum ChildSelectionMode
		None
		Child
		Hierarchical
	End Enum
End Namespace
