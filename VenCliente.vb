Imports System
Imports System.ComponentModel
Imports System.Threading
Imports System.Windows.Forms
Imports Proyecto.Chat

Public Class VenCliente
    Private tool As ToolTip = New ToolTip()
    Private log As Login
    Private ventanas As ArrayList = New ArrayList()
    Private yo As User
    Private funcion As New Funciones
    Private hilo As Thread
    Private clientes As ArrayList = New ArrayList
    Private texto As String
    Private demo As Threading.Thread = Nothing
    Dim WithEvents WinSockCliente As New Cliente
    Delegate Sub SetTextCallback(ByVal [text1] As String)

    Public Sub Inicio(ByVal usuario As User, ByVal socket As Cliente, ByVal login As Login)
        yo = usuario
        Me.Text &= ": " & usuario.User
        WinSockCliente = socket
        Me.log = login
    End Sub

    Public Sub Fin()
        For Each ventana As Chat In ventanas
            ventana.Close()
        Next
    End Sub

    Private Sub btnChat_Click(sender As Object, e As EventArgs) Handles btnChat.Click
        If lstClients.SelectedIndex > 0 Then
            Dim seleccionado As String = lstClients.Items.Item(lstClients.SelectedIndex).ToString
            If seleccionado.Equals("Conectados") Or seleccionado.Equals("Desconectados") Then
                MsgBox("Seleccione un usuario correcto", MsgBoxStyle.Critical, "Error Chat")
            Else
                funcion.Bitacora("El Usuario " & yo.User & " abrió chat con " & seleccionado, yo.User)
                Dim chat As Chat = New Chat
                ventanas.Add(chat)
                chat.User(New User(yo.User), New User(seleccionado), WinSockCliente)
                chat.Show()
            End If
        End If
    End Sub

    Private Sub getClientes()
        While True
            Dim clientes2 As ArrayList = Nothing 'funcion.obtenerClientes(yo, WinSockCliente)
            Dim pasa As Boolean = False

            If clientes2.Count = clientes.Count And clientes2.Count > 0 Then
                SyncLock Me
                    clientes2.Sort()
                    clientes.Sort()
                End SyncLock
                For i = 0 To clientes2.Count - 1
                    If (Not clientes2.Item(i).ToString.Equals(clientes.Item(i).ToString)) Then
                        pasa = True
                        End
                    End If
                Next
            ElseIf clientes2.Count <> clientes.Count Then
                pasa = True
            End If

            If pasa Then
                SyncLock Me
                    Me.clientes = New ArrayList(clientes2.ToArray)
                    Call actualizarLista(clientes)
                End SyncLock
            End If
        End While
    End Sub

    Private Sub actualizarLista(ByVal clientes As ArrayList)
        Me.eliminarItems(yo.User)
        'MsgBox("Nuevo Usuario Conectado")
        For Each client In clientes
            Me.SetText(client.ToString)
        Next
    End Sub

    Private Sub eliminarItems(ByVal [text] As String)
        If Me.lstClients.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf eliminarItems)
            Me.Invoke(d, New Object() {[text]})
        Else
            Me.lstClients.Items.Clear()
        End If
    End Sub

    Private Sub VenCliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tool.SetToolTip(Me.cmdRefresh, "Actualizar Lista")
        tool.SetToolTip(Me.cmdSalir, "Salir")
        tool.SetToolTip(Me.btnChat, "Iniciar Chat")
        tool.SetToolTip(Me.btnBitacora, "Ver Bitácora")
    End Sub

    Private Sub VenCliente_Exit(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        Try
            funcion.Bitacora("El Usuario " & yo.User & " cerró sesión", yo.User)
            Me.Fin()
            WinSockCliente.Desconectar()
            Me.log.Close()
        Catch ex As Exception
            MsgBox("Error al Cerrar Form: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdSalir_Click(sender As Object, e As EventArgs) Handles cmdSalir.Click
        Try
            WinSockCliente.Desconectar()
            Me.Close()
        Catch ex As Exception
            MsgBox("Error al Cerrar Form: " & ex.Message)
        End Try
    End Sub

    Public Sub RespuestaUsers(ByVal Users As ArrayList) Handles WinSockCliente.RespuestaUsers
        If (lstClients.Items.Count > 0) Then
            Me.eliminarItems(yo.User)
        End If
        For Each User As String In Users
            SyncLock Me
                'MsgBox(User)
                'texto = User
                Me.SetText(User)
                'Me.demo = New Threading.Thread(New Threading.ThreadStart(AddressOf Me.ThreadProcSafe))
                'Me.demo.Start()
            End SyncLock
        Next
    End Sub

    Private Sub ThreadProcSafe()
        Me.SetText(texto)
    End Sub

    Private Sub SetText(ByVal [text1] As String)
        If Me.lstClients.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf SetText)
            Me.Invoke(d, New Object() {[text1]})
        Else
            Me.lstClients.Items.Add(text1)
        End If
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        funcion.Bitacora("El Usuario " & yo.User & " actualizó el listado de usuarios", yo.User)
        funcion.obtenerClientes(yo, WinSockCliente)
    End Sub

    Private Sub lstClients_DoubleClick(sender As Object, e As EventArgs) Handles lstClients.DoubleClick
        Dim seleccionado As String = lstClients.Items.Item(lstClients.SelectedIndex).ToString
        If seleccionado.Equals("Conectados") Or seleccionado.Equals("Desconectados") Then
            MsgBox("Seleccione un usuario correcto", MsgBoxStyle.Critical, "Error Chat")
        Else
            funcion.Bitacora("El Usuario " & yo.User & " abrió chat con " & seleccionado, yo.User)
            Dim chat As Chat = New Chat
            ventanas.Add(chat)
            chat.User(New User(yo.User), New User(seleccionado), WinSockCliente)
            chat.Show()
        End If
    End Sub

    Private Sub btnBitacora_Click(sender As Object, e As EventArgs) Handles btnBitacora.Click
        Dim bitacora As New Bitacora()
        bitacora.Show()
        bitacora.Inicio(yo.User)
    End Sub
End Class
