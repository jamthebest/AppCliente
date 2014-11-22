Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Public Class Chat
    Private s, m As String 'variables de minutos y segundos
    Private x, minutos As Integer 'variables utilizadas al contabilizar el tiempo
    Private yo As User 'Usuario al que le pertenece el chat
    Private dest As User 'Usuario con el cual se está chateando
    Dim WithEvents WinSockCliente As New Cliente
    Private demo As Threading.Thread = Nothing
    Dim IP1 As String
    Dim port1 As String
    Dim Texto As String
    Dim contador As Integer = 0 'Lleva el indice del audio
    Dim grabado As Boolean = False 'Bandera para reproducir archivo grabado u obtenido
    Delegate Sub SetTextCallback(ByVal [text1] As String)

    'Esta función se utiliza para grabar audio
    Private Declare Function mciSendString Lib "winmm.dll" Alias "mciSendStringA" (ByVal lpstrCommand As String, ByVal lpstrReturnString As String, ByVal uReturnLength As Integer, ByVal hwndCallback As Integer) As Integer

    Private Sub ThreadProcSafe()
        Me.SetText(Texto)
    End Sub

    Private Sub SetText(ByVal [text1] As String)
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If Me.txtMensaje.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf SetText)
            Me.Invoke(d, New Object() {[text1]})
        Else
            Me.txtHistorial.Items.Add(dest.User + ": " & text1 & vbCrLf)
        End If
    End Sub

    Private Sub WinSockCliente_DatosRecibidos(ByVal datos As String) Handles WinSockCliente.DatosRecibidos
        'txtMensaje.Text = txtMensaje.Text + datos
        Texto = datos
        Me.demo = New Threading.Thread(New Threading.ThreadStart(AddressOf Me.ThreadProcSafe))
        Me.demo.Start()

        'MsgBox("El servidor envio el siguiente mensaje: " & datos)
    End Sub

    Private Sub WinSockCliente_ConexionTerminada() Handles WinSockCliente.ConexionTerminada
        
    End Sub

    Private Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        Dim texto As String = txtMensaje.Text
        Dim txtXML As String
        Dim funciones As New Funciones

        yo = New User("Jam") 'Se borra
        Dim xx As User = New User("XX") 'Se borra
        grabado = False
        Dim tDes As String = ""

        If texto.Equals("") And Label2.Text = "" Then
            MsgBox("No Escribió Nada en el Mensaje", MsgBoxStyle.Critical, "Error al Enviar")
        Else
            Dim mensaje As Mensaje 'Se crea la variable para el mensaje nuevo
            If texto.Equals("") Then 'Se envía un mensaje de audio
                Dim audio As String = funciones.FileToByteArray(yo.User & contador & ".mp3") 'Se convierte el audio a un arreglo de bytes
                mensaje = New Mensaje("", audio, xx, yo) 'Se crea el mensaje de Solo de sonido
            ElseIf Label2.Text.Equals("") Then 'Se envía un mensaje de texto
                mensaje = New Mensaje(texto, xx, yo) 'Se crea el mensaje de Solo de texto
            Else 'Se envia un mensaje de Audio y Texto
                Dim audio As String = funciones.FileToByteArray(yo.User & contador & ".mp3") 'Se convierte el audio
                mensaje = New Mensaje(texto, audio, xx, yo) 'Se crea el mensaje de De texto y audio
            End If
            contador += 1
            txtXML = funciones.Serializar(mensaje, yo.User) 'funcion que convierte el mensaje a XML
            Dim md As String = funciones.MD5Encrypt(txtXML) 'Se encripta el XML en MD5
            tDes = funciones.encryptString(txtXML & "?XXXJAMXXX?" & md) 'Se encripta el MD5 con el XML en 3DES

            If texto.Equals("") Then
                texto = "%% Audio.mp3 %%"
            ElseIf Not Label2.Text.Equals("") Then
                texto &= "  %% Audio.mp3 %%"
            End If
            texto &= "   _" & DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss")
            txtHistorial.Items.Add("Yo: " + texto + vbCrLf + vbCrLf) 'Se escribe el mensaje en el historial

            Dim solicitud As Solicitud = New Solicitud(2, tDes)
            Dim encriptado As String = funciones.Encriptar(solicitud, "Solicitud")
            WinSockCliente.EnviarDatos(encriptado) 'Se envía el mensaje al servidor
        End If
        Call BotonesGrabar()
        Label1.Text = ""
        Label2.Text = ""
        txtMensaje.Text = ""
    End Sub

    'Se inicializan los usuarios
    Public Function User(ByVal usuario As User, ByVal destinatario As User, ByVal Socket As Cliente) As Boolean
        yo = usuario
        dest = destinatario
        Label4.Text &= dest.User
        Me.Text &= " de: " + yo.User
        WinSockCliente = Socket
        Return True
    End Function

    Private Sub cmdGrabar_Click(sender As Object, e As EventArgs) Handles cmdGrabar.Click
        If txtHistorial.SelectedIndex >= 0 Then
            txtHistorial.SetSelected(txtHistorial.SelectedIndex, False)
        End If
        Call BotonesGrabando()
        grabado = True
        Label2.Text = "00:00"
        Call TimerOn() 'Inicia el timer

        mciSendString("open new Type waveaudio Alias recsound", "", 0, 0) 'Inicia una instancia para grabar audio
        mciSendString("record recsound", "", 0, 0) 'Inicia a grabar el audio

        Label1.Text = "Grabando..."
    End Sub

    'Función que inicia el timer
    Private Sub TimerOn()
        Timer1.Enabled = True
    End Sub

    'Función que finaliza el timer
    Private Sub TimerOff()
        Timer1.Enabled = False
        Call Resetear()
    End Sub

    Private Sub cmdDetener_Click(sender As Object, e As EventArgs) Handles cmdDetener.Click
        Call BotonesDetenido()
        Call TimerOff()

        yo = New User("Jam") 'Se borra
        Dim comando As String = "save recsound " & yo.User & contador & ".mp3"
        mciSendString(comando, "", 0, 0) 'Guarda el audio grabado en un archivo
        mciSendString("close recsound", "", 0, 0) 'Cierra la instancia que graba el audio
        MsgBox("Audio Grabado", MsgBoxStyle.Information, "Mensaje") 'Emite un mensaje que indica que se grabó el audio

        Label1.Text = "Detenido..."
    End Sub

    Private Sub Reproducir(ByVal numero As Integer, ByVal tipo As Integer)
        If (IO.File.Exists(yo.User & numero & ".mp3")) Then
            If tipo = 1 Then
                cmdGrabar.Enabled = False
                Label1.Text = "Reproduciendo..."
                cmdReproducir.Text = "Detener"
                Label3.Text = "00:00"
                Timer2.Enabled = True
                My.Computer.Audio.Play(yo.User & numero & ".mp3", AudioPlayMode.Background) 'Reproduce el audio grabado
            Else
                My.Computer.Audio.Play(yo.User & numero & ".mp3", AudioPlayMode.WaitToComplete) 'Reproduce el audio grabado
            End If

        Else
            MsgBox("Audio no encontrado", MsgBoxStyle.Critical, "Error al Reproducir")
        End If
    End Sub

    Private Sub cmdReproducir_Click(sender As Object, e As EventArgs) Handles cmdReproducir.Click
        If cmdReproducir.Text = "Reproducir" Then
            If grabado Then
                Call Reproducir(contador, 1)
            Else
                Call Reproducir(txtHistorial.SelectedIndex, 0)
            End If
        Else
            Call DetenerReproduccion()
            Call Resetear()
        End If
    End Sub

    'Función detiene la reproducción del audio
    Private Sub DetenerReproduccion()
        cmdGrabar.Enabled = True
        Label1.Text = "Detenido..."
        cmdReproducir.Text = "Reproducir"
        Label3.Text = ""
        Timer2.Enabled = False
        My.Computer.Audio.Stop() 'Detiene la reproducción en backround
    End Sub

    Private Sub BotonesDetenido()
        cmdReproducir.Enabled = True
        cmdDetener.Enabled = False
        cmdGrabar.Enabled = True
    End Sub

    Private Sub BotonesGrabando()
        cmdReproducir.Enabled = False
        cmdDetener.Enabled = True
        cmdGrabar.Enabled = False
    End Sub

    Private Sub BotonesGrabar()
        cmdReproducir.Enabled = False
        cmdDetener.Enabled = False
        cmdGrabar.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If cmdGrabar.Enabled = False And cmdDetener.Enabled = True And cmdReproducir.Enabled = False Then
            CalcularTiempo(2)
            Label2.Text = m + ":" + s
        End If

        If cmdGrabar.Enabled = False And cmdDetener.Enabled = True And cmdReproducir.Enabled = True Then
            CalcularTiempo(2)
            Label2.Text = m + ":" + s
        End If

        If cmdDetener.Enabled = False Then
            Label2.Text = m + ":" + s
            Call Resetear()
        End If
    End Sub

    Private Sub Resetear()
        x = 0
        m = 0
        s = 0
        minutos = 0
    End Sub

    'Calcula el tiempo y es llamado cada segundo por el timer
    Private Sub CalcularTiempo(ByVal label As Integer)
        x = x + 1
        If x = 60 Then
            minutos = minutos + 1
            x = 0
        End If
        m = Trim(Str(minutos))
        s = Trim(Str(x))

        If minutos < 10 Then m = "0" + m
        If x < 10 Then s = "0" + s

        If label = 2 Then
            Label2.Text = m + ":" + s
        Else
            Label3.Text = m + ":" + s
        End If

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If cmdReproducir.Enabled = True And cmdReproducir.Text.Equals("Detener") Then
            CalcularTiempo(3)
            Label3.Text = m + ":" + s
        End If

        If cmdReproducir.Enabled = True And cmdReproducir.Text.Equals("Reproducir") Then
            Label3.Text = m + ":" + s
            Call Resetear()
        End If

        If Label2.Text.Equals(Label3.Text) Then
            Call DetenerReproduccion()
            Call Resetear()
        End If
    End Sub

    Private Sub Chat_Exit(sender As Object, e As EventArgs) Handles MyBase.FormClosed
        Call WinSockCliente_ConexionTerminada()
    End Sub

    Private Sub txtHistorial_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtHistorial.SelectedIndexChanged
        'MsgBox(txtHistorial.SelectedIndex)
        If (IO.File.Exists(yo.User & txtHistorial.SelectedIndex & ".mp3")) Then
            Call BotonesDetenido()
        Else
            Call BotonesGrabar()
        End If
    End Sub

    Private Sub txtMensaje_GotFocus(sender As Object, e As EventArgs) Handles txtMensaje.GotFocus
        If txtHistorial.SelectedIndex >= 0 Then
            txtHistorial.SetSelected(txtHistorial.SelectedIndex, False)
            Call BotonesGrabar()
        End If
    End Sub

    Private Sub Chat_FormClosing(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        For i = 0 To contador
            If (IO.File.Exists(yo.User & i & ".mp3")) Then
                IO.File.Delete(yo.User & i & ".mp3")
            End If
        Next
    End Sub
End Class
