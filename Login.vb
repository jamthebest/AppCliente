Imports System.Data.SqlClient
Imports System.Data.Sql

Public Class Login
    Private tool As ToolTip = New ToolTip()
    Private ventanas As ArrayList = New ArrayList()
    Dim func As New Funciones
    Dim WithEvents WinSockCliente As New Cliente()
    'Private texto As String
    Delegate Sub SetTextCallback(ByVal [text1] As String)
    Private demo1 As Threading.Thread = Nothing
    Private demo As Threading.Thread = Nothing

    Private Sub ThreadProcSafe()
        Me.SetText("")
    End Sub

    Private Sub SetText(ByVal [text1] As String)
        If Me.txtPassword.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf SetText)
            Me.Invoke(d, New Object() {[text1]})
        Else
            Dim Principal As New VenCliente
            Principal.Inicio(New User(txtUsuario.Text), WinSockCliente, Me)
            Principal.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub ThreadProcSafe1()
        Me.SetText1("")
    End Sub

    Private Sub SetText1(ByVal [text1] As String)
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If Me.txtPassword.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf SetText1)
            Me.Invoke(d, New Object() {[text1]})
        Else
            Me.txtPassword.Text = text1
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        Dim nuevo As NewUser = New NewUser
        nuevo.Show()
        nuevo.Inicio(WinSockCliente)
        Me.Hide()
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtIP.Text = Me.GetIPAddress()
        txtPuerto.Text = "8050"
        tool.SetToolTip(Me.cmdNew, "Nuevo Usuario")
        tool.SetToolTip(Me.cmdIngresar, "Ingresar")
        Conectar()
    End Sub

    Private Sub Login_Exit(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        WinSockCliente.Desconectar()
    End Sub

    Private Function GetIPAddress() As String
        Dim strHostName As String
        Dim strIPAddress As String

        strHostName = System.Net.Dns.GetHostName()
        strIPAddress = System.Net.Dns.GetHostByName(strHostName).AddressList(0).ToString()
        Return strIPAddress
    End Function

    Private Sub Conectar()
        Try
            With WinSockCliente
                'Determino a donde se quiere conectar el usuario 
                .IPDelHost = txtIP.Text
                .PuertoDelHost = txtPuerto.Text
                'Me conecto 
                .Conectar()
            End With
        Catch e As Exception
            MsgBox("Servidor no encontrado! " & vbCrLf & e.Message)
            WinSockCliente.Desconectar()
        End Try
    End Sub

    Private Sub btnDesplegar_Click(sender As Object, e As EventArgs) Handles btnDesplegar.Click
        If btnDesplegar.Text = "+" Then
            Me.Size = New Size(294, 399)
            btnDesplegar.Text = "-"
        Else
            Me.Size = New Size(294, 333)
            btnDesplegar.Text = "+"
        End If
    End Sub

    Private Sub RespuestaLogin(ByVal mensaje As String) Handles WinSockCliente.RespuestaLogin
        If mensaje.Equals("Exito al hacer login") Then
            func.Bitacora("Éxito al hacer login de " & txtUsuario.Text, txtUsuario.Text)
            Me.demo = New Threading.Thread(New Threading.ThreadStart(AddressOf Me.ThreadProcSafe))
            Me.demo.Start()
        Else
            func.Bitacora("Error al hacer login de " & txtUsuario.Text, txtUsuario.Text)
            MsgBox("Error en el Usuario y/o Contraseña", MsgBoxStyle.Critical)
            Me.demo1 = New Threading.Thread(New Threading.ThreadStart(AddressOf Me.ThreadProcSafe1))
            Me.demo1.Start()
        End If
    End Sub

    Private Sub cmdIngresar_Click(sender As Object, e As EventArgs) Handles cmdIngresar.Click
        If Me.txtUsuario.Text.Equals("") Or Me.txtPassword.Text.Equals("") Then
            MsgBox("Ingrese su usuario y contraseña", MsgBoxStyle.Critical)
            Exit Sub
        End If
        If WinSockCliente.IPDelHost <> txtIP.Text Or WinSockCliente.PuertoDelHost <> txtPuerto.Text Then
            If WinSockCliente.Estado Then
                WinSockCliente.Desconectar()
            Else
                WinSockCliente = New Cliente
            End If
            Conectar()
        ElseIf Not WinSockCliente.Estado Then
            Conectar()
        End If
        Try
            Dim dts As New Datos

            dts.nomusuario = txtUsuario.Text
            dts.passusuario = func.MD5Encrypt(txtPassword.Text)

            func.Bitacora("Intento de Logueo de " & txtUsuario.Text, txtUsuario.Text)
            func.Validar(dts, WinSockCliente)

        Catch ex As Exception
            MsgBox("Error en el Login: " & ex.Message)
        End Try
    End Sub
End Class
