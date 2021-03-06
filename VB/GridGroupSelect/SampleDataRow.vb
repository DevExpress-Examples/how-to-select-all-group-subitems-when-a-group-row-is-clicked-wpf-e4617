﻿Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows

Namespace dxSample
    Friend Class SampleDataRow
        Inherits DependencyObject

        Public Property Id() As Integer
            Get
                Return DirectCast(GetValue(IdProperty), Integer)
            End Get
            Set(ByVal value As Integer)
                SetValue(IdProperty, value)
            End Set
        End Property
        Public Property Group() As String
            Get
                Return DirectCast(GetValue(GroupProperty), String)
            End Get
            Set(ByVal value As String)
                SetValue(GroupProperty, value)
            End Set
        End Property
        Public Property Name() As String
            Get
                Return DirectCast(GetValue(NameProperty), String)
            End Get
            Set(ByVal value As String)
                SetValue(NameProperty, value)
            End Set
        End Property
        Public Property HasFlag() As Boolean
            Get
                Return DirectCast(GetValue(HasFlagProperty), Boolean)
            End Get
            Set(ByVal value As Boolean)
                SetValue(HasFlagProperty, value)
            End Set
        End Property

        Public Shared ReadOnly HasFlagProperty As DependencyProperty = DependencyProperty.Register("HasFlag", GetType(Boolean), GetType(SampleDataRow), New PropertyMetadata(False))
        Public Shared ReadOnly NameProperty As DependencyProperty = DependencyProperty.Register("Name", GetType(String), GetType(SampleDataRow), New PropertyMetadata(""))
        Public Shared ReadOnly GroupProperty As DependencyProperty = DependencyProperty.Register("Group", GetType(String), GetType(SampleDataRow), New PropertyMetadata(""))
        Public Shared ReadOnly IdProperty As DependencyProperty = DependencyProperty.Register("Id", GetType(Integer), GetType(SampleDataRow), New PropertyMetadata(0))

        Public Shared Function CreateRows() As IList(Of SampleDataRow)
            Dim result As IList(Of SampleDataRow) = New List(Of SampleDataRow)()
            For i As Integer = 0 To 99
                result.Add(New SampleDataRow() With { _
                    .Id = i, _
                    .Group = String.Format("group {0}", i Mod 10), _
                    .Name = String.Format("name {0}", i Mod 3), _
                    .HasFlag = (i Mod 3 = 0) _
                })
            Next i
            Return result
        End Function
    End Class
End Namespace
