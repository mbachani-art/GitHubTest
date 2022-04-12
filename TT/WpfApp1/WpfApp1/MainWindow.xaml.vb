Class MainWindow
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim name As String
        name = txtName.Text
        MessageBox.Show("Hello world " & name)
    End Sub


End Class
