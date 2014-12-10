Imports System.IO

Public Class Bitacora

    Public Sub Inicio(ByVal user As String)
        Dim fileContents As String
        fileContents = My.Computer.FileSystem.ReadAllText(user & ".txt")
        Dim bit As Array = fileContents.Split(",")
        For Each texto As String In bit
            Me.lstLog.Items.Insert(0, texto)
        Next
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class