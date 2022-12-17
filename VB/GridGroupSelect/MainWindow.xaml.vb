Imports System.Windows

Namespace dxSample

    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            DataContext = SampleDataRow.CreateRows()
        End Sub
    End Class
End Namespace
