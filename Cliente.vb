Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.IO

Public Class Cliente
#Region "VARIABLES"
    Private funciones As New Funciones
    Private Stm As Stream 'Utilizado para enviar datos al Servidor y recibir datos del mismo 
    Private m_IPDelHost As String 'Direccion del objeto de la clase Servidor 
    Private m_PuertoDelHost As String 'Puerto donde escucha el objeto de la clase Servidor 
    Dim tcpClnt As TcpClient
    Private Usuario As User 'Instancia del Usuario
#End Region

#Region "EVENTOS"
    Public Event ConexionTerminada()
    Public Event DatosRecibidos(ByVal datos As String)
    Public Event RespuestaLogin(ByVal respuesta As String)
    Public Event RespuestaMensaje(ByVal respuesta As String)
    Public Event RespuestaUsers(ByVal respuesta As ArrayList)
#End Region

#Region "PROPIEDADES"
    Public Property IPDelHost() As String
        Get
            IPDelHost = m_IPDelHost
        End Get
        Set(ByVal Value As String)
            m_IPDelHost = Value
        End Set
    End Property

    Public Property PuertoDelHost() As String
        Get
            PuertoDelHost = m_PuertoDelHost
        End Get
        Set(ByVal Value As String)
            m_PuertoDelHost = Value
        End Set
    End Property

    Public Property UserDelHost() As User
        Get
            UserDelHost = Usuario
        End Get
        Set(ByVal Value As User)
            Usuario = Value
        End Set
    End Property
#End Region

#Region "METODOS"
    Public Sub Conectar()
        Dim tcpThd As Thread 'Se encarga de escuchar mensajes enviados por el Servidor 

        tcpClnt = New TcpClient()
        'Me conecto al objeto de la clase Servidor, 
        '  determinado por las propiedades IPDelHost y PuertoDelHost
        tcpClnt.Connect(IPDelHost, 8050)
        Stm = tcpClnt.GetStream()

        'Creo e inicio un thread para que escuche los mensajes enviados por el Servidor 
        tcpThd = New Thread(AddressOf LeerSocket)
        tcpThd.Start()
    End Sub

    Public Sub Desconectar()
        tcpClnt.Close()
    End Sub

    Public Function Estado() As Boolean
        Return tcpClnt.Connected
    End Function

    Public Sub EnviarDatos(ByVal Datos As String)
        Dim mensaje As String = Datos
        While (Encoding.ASCII.GetBytes(mensaje).Length < 1024)
            mensaje = mensaje & "?"
        End While
        Dim BufferDeEscritura() As Byte = New Byte(1023) {}
        BufferDeEscritura = Encoding.ASCII.GetBytes(mensaje)

        If Not (Stm Is Nothing) Then
            'Envio los datos al Servidor
            Stm.Write(BufferDeEscritura, 0, BufferDeEscritura.Length)
        End If
    End Sub
#End Region

#Region "FUNCIONES PRIVADAS"
    Private Sub LeerSocket()
        Dim BufferDeLectura() As Byte
        While True
            Try
                BufferDeLectura = New Byte(1023) {}
                'Me quedo esperando a que llegue algun mensaje 
                Stm.Read(BufferDeLectura, 0, BufferDeLectura.Length)
                'Genero el evento DatosRecibidos, ya que se han recibido datos desde el Servidor 
                Dim men As String = Encoding.ASCII.GetString(BufferDeLectura)
                Dim y As String = men.Last
                Do While y.Equals("?")
                    men = men.Substring(0, men.Length - 1)
                    y = men.Last
                Loop
                Dim x As String = funciones.decryptString(men)
                'Dim md5 As String = x.Substring(x.IndexOf("?XXXJAMXXX?"))
                x = x.Substring(0, x.IndexOf("?XXXJAMXXX?"))
                Dim solicitud As Solicitud = funciones.DesSerializar(x, 1)
                Select Case solicitud.TipoSolicitud
                    Case 1
                        RaiseEvent RespuestaLogin(solicitud.MensajeSolicitud)
                    Case 2
                        Dim mens As String = funciones.decryptString(solicitud.MensajeSolicitud)
                        mens = mens.Substring(0, mens.IndexOf("?XXXJAMXXX?"))
                        Dim mensaje As Mensaje = funciones.DesSerializar(mens)
                        If mensaje.Text.Equals("") Then
                            mensaje.Text = "%% Audio.mp3 %%"
                        ElseIf mensaje.Sound.Any Then
                            mensaje.Text &= "  %% Audio.mp3 %%"
                        End If
                        mensaje.Text &= "   _" & DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss")

                        RaiseEvent DatosRecibidos(mensaje.Text)
                    Case 3
                        RaiseEvent RespuestaUsers(solicitud.ArgumentosSolicitud)
                    Case 4
                    Case 5
                        RaiseEvent RespuestaMensaje(solicitud.MensajeSolicitud)
                    Case Else

                End Select

            Catch e As Exception
                Exit While
            End Try
        End While
        'Finalizo la conexion, por lo tanto genero el evento correspondiente 
        RaiseEvent ConexionTerminada()
    End Sub
#End Region
End Class

