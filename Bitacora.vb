Imports System.IO

Public Class Bitacora

    Public Sub Inicio(ByVal user As String)
        user = "log"
        Dim fileReader As StreamReader = New StreamReader(user & ".txt")
        Try
            'txtBitacora.Text = IO.File.ReadAllText(user & ".txt")
            fileReader = My.Computer.FileSystem.OpenTextFileReader(user & ".txt")
            While fileReader.Peek <> -1
                Me.lstLog.Items.Insert(0, fileReader.ReadLine().ToString)
            End While
            Me.btnSalir.Focus()
        Catch ex As Exception
            MsgBox("Error al abrir bitácora: " & ex.Message)
        Finally
            fileReader.Close()
        End Try
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class