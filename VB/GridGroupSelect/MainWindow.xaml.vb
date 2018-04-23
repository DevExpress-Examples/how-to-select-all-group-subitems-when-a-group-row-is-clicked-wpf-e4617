Imports System.Windows

Namespace dxSample
    Partial Public Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
            DataContext = SampleDataRow.CreateRows()
        End Sub
    End Class
End Namespace
