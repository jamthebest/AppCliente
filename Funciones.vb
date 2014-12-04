Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Security.Cryptography
Imports System.Xml.Serialization

Public Class Funciones
    Private Key As String 'llave para la encriptación 3DES
    Private IVector() As Byte = {27, 9, 45, 27, 0, 72, 171, 54} 'Vector para encriptación 3DES
    Private solicitud As Solicitud
    

    Function Serializar(ByVal objeto As Mensaje, ByVal ruta As String) As String
        SyncLock Me
            If (IO.File.Exists(ruta & ".xml")) Then
                IO.File.Delete(ruta & ".xml")
            End If
            Dim objStreamWriter As New StreamWriter(ruta & ".xml")
            Dim x As New XmlSerializer(objeto.GetType)
            Try
                x.Serialize(objStreamWriter, objeto)
            Catch ex As NotImplementedException
                MsgBox("Error al Serializar: " & ex.ToString, MsgBoxStyle.Critical, "Error!")
            Finally
                objStreamWriter.Close()
            End Try
            Return FileToString(ruta & ".xml")
        End SyncLock
    End Function

    Function Serializar(ByVal objeto As Solicitud, ByVal ruta As String) As String
        SyncLock Me
            Try
                If (IO.File.Exists(ruta & ".xml")) Then
                    IO.File.Delete(ruta & ".xml")
                End If
                Dim objStreamWriter As New StreamWriter(ruta & ".xml")
                Dim x As New XmlSerializer(objeto.GetType)
                Try
                    x.Serialize(objStreamWriter, objeto)
                Catch ex As NotImplementedException
                    MsgBox("Error al Serializar: " & ex.ToString, MsgBoxStyle.Critical, "Error!")
                Finally
                    objStreamWriter.Close()
                End Try
            Catch e As Exception
                MsgBox("Error al Serializar: " & e.ToString)
            End Try
            Return FileToString(ruta & ".xml")
        End SyncLock
    End Function

    Public Function DesSerializar(ByVal xml As String) As Mensaje
        SyncLock Me
            Dim mensaje As New Mensaje()
            Dim x As New XmlSerializer(mensaje.GetType)
            'Deserialize text file to a new object.
            Dim objStreamReader As New StreamReader(xml)
            Try
                mensaje = x.Deserialize(objStreamReader)
            Catch ex As Exception
                MsgBox("Error al DesSerializar: " & ex.ToString)
            Finally
                objStreamReader.Close()
            End Try
            Return mensaje
        End SyncLock
    End Function

    Public Function DesSerializar(ByVal xml As String, ByVal nada As Integer) As Solicitud
        SyncLock Me
            Dim mensaje As New Solicitud()
            Dim x As New XmlSerializer(mensaje.GetType)
            'Deserialize text file to a new object.
            Dim objStreamReader As New StreamReader(xmlToFile(xml, "Respuesta"))
            Try
                mensaje = x.Deserialize(objStreamReader)
            Catch ex As Exception
                MsgBox("Error al DesSerializar: " & ex.ToString)
            Finally
                objStreamReader.Close()
            End Try
            Return mensaje
        End SyncLock
    End Function

    Public Function xmlToFile(ByVal xml As String, ByVal ruta As String) As String
        Try
            My.Computer.FileSystem.WriteAllText(ruta + ".xml", xml, False)
            Return ruta + ".xml"
        Catch ex As Exception
            MsgBox("Error al crear archivo XML!" & vbCrLf & ex.Message)
        End Try
        Return Nothing
    End Function

    Public Function FileToString(ByVal ruta As String) As String
        SyncLock Me
            Dim objeto As New StreamReader(ruta)
            Dim sLine As String = ""
            Dim Texto As String = ""
            Try
                Do
                    sLine = objeto.ReadLine()
                    If Not sLine Is Nothing Then
                        Texto &= sLine & vbCrLf
                    End If
                Loop Until sLine Is Nothing
            Catch ex As Exception
                MsgBox("Error al convertir XML a String: " & ex.ToString)
            Finally
                objeto.Close()
            End Try
            Return Texto
        End SyncLock
    End Function

    Public Function FileToByteArray(ByVal _FileName As String) As Byte()
        Dim _Buffer() As Byte = Nothing
        Try
            ' Open file for reading
            Dim _FileStream As New System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read)
            ' attach filestream to binary reader
            Dim _BinaryReader As New System.IO.BinaryReader(_FileStream)
            ' get total byte length of the file
            Dim _TotalBytes As Long = New System.IO.FileInfo(_FileName).Length
            Try
                ' read entire file into buffer
                _Buffer = _BinaryReader.ReadBytes(CInt(Fix(_TotalBytes)))
            Catch ex As Exception
                MsgBox("Error al leer archivo!" & vbCrLf & ex.Message)
            Finally
                ' close file reader
                _FileStream.Close()
                _FileStream.Dispose()
                _BinaryReader.Close()
            End Try
        Catch _Exception As Exception
            ' Error
            MsgBox("Error al Obtener Bytes: " & _Exception.Message)
        End Try
        Return _Buffer
        'Return Convert.FromBase64String(System.IO.File.ReadAllBytes(_FileName))
    End Function

    Public Function BytesToFile(ByVal bytDataArray As Byte(), ByVal nombre As String, ByVal numero As Integer) As Boolean
        Try
            If (IO.File.Exists(nombre & numero & ".mp3")) Then
                IO.File.Delete(nombre & numero & ".mp3")
            End If
            Dim length As Integer = bytDataArray.Length
            Dim tempFileName As String = nombre & numero & ".mp3"
            Using fs As New IO.FileStream(tempFileName, IO.FileMode.OpenOrCreate)
                Dim bw As New IO.BinaryWriter(fs)
                bw.Write(bytDataArray, 0, length)
                bw.Flush()
                bw = Nothing
            End Using

            'IO.File.WriteAllBytes(nombre & numero & ".mp3", bytDataArray)
            'My.Computer.FileSystem.WriteAllText(nombre & numero & ".mp3", bytDataArray.ToString, False)
            Return True
        Catch ex As Exception
            MsgBox("Error al crear archivo de audio!" & vbCrLf & ex.Message)
        End Try
        Return False
    End Function

    Public Function BytesToFile2(ByVal bytDataArray As Byte(), ByVal nombre As String, ByVal numero As Integer) As System.IO.FileStream
        SyncLock Me
            Dim fsDataArray As New System.IO.FileStream(nombre & numero & ".mp3", System.IO.FileMode.Create)
            Try
                With fsDataArray
                    '.Write(Convert.FromBase64String(bytDataArray), 0, bytDataArray.Length)
                    .Write(bytDataArray, 0, bytDataArray.Length)
                End With
            Catch ex As Exception
                MsgBox("Error al convertir Bytes en Audio: " & ex.ToString)
            Finally
                With fsDataArray
                    .Close() : .Dispose()
                End With
            End Try
            Return fsDataArray
        End SyncLock
    End Function

    Public Function FileToByteArray2(ByVal _FileName As String) As Byte()
        SyncLock Me
            Dim _Buffer() As Byte = Nothing
            Dim ret As String = ""
            Try
                ' Open file for reading
                Dim _FileStream As New System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read)
                ' attach filestream to binary reader
                Dim _BinaryReader As New System.IO.BinaryReader(_FileStream)
                ' get total byte length of the file
                Dim _TotalBytes As Long = New System.IO.FileInfo(_FileName).Length
                Try
                    ' read entire file into buffer
                    _Buffer = _BinaryReader.ReadBytes(CInt(Fix(_TotalBytes)))
                    ret = Convert.ToBase64String(_Buffer)
                    MsgBox(ret)
                Catch _Exception As Exception
                    MsgBox("Error al convertir Audio a Bytes: " & _Exception.ToString)
                Finally
                    ' close file reader
                    _FileStream.Close()
                    _FileStream.Dispose()
                    _BinaryReader.Close()
                End Try
                Return _Buffer
            Catch ex As Exception
                MsgBox("Error al abrir audio: " & ex.ToString)
            End Try
            Return _Buffer
        End SyncLock
    End Function

    'Función que encripta los datos en MD5
    Public Function MD5Encrypt(ByVal texto As String) As String
        SyncLock Me
            Dim encryt As String = ""
            Try
                Dim md5 As MD5CryptoServiceProvider
                Dim bytValue As Byte()
                Dim bytHash As Byte()
                Dim i As Integer

                md5 = New MD5CryptoServiceProvider
                bytValue = System.Text.Encoding.UTF8.GetBytes(texto)
                bytHash = md5.ComputeHash(bytValue)
                md5.Clear()

                For i = 0 To bytHash.Length - 1
                    encryt &= bytHash(i).ToString("x").PadLeft(2, "0")
                Next
            Catch ex As Exception
                MsgBox("Error al encriptar en MD5: " & ex.ToString)
            End Try
            Return encryt
        End SyncLock
    End Function

    'Función que encripta los datos en 3DES
    Public Function encryptString(ByVal str As String) As String
        SyncLock Me
            Dim ITransform As CryptoAPITransform = Nothing
            Dim byteData() As Byte = Nothing
            Try
                Dim enc As New ASCIIEncoding
                byteData = enc.GetBytes(str)
                Dim tDes As New TripleDESCryptoServiceProvider()
                Dim md5 As New MD5CryptoServiceProvider()

                tDes.Key = md5.ComputeHash(enc.GetBytes("JAM")) 'Elige una llave que es la encriptacion en md5 de la palabra JAM
                tDes.IV = IVector

                ITransform = tDes.CreateEncryptor()
                Return Convert.ToBase64String(ITransform.TransformFinalBlock(byteData, 0, byteData.Length))
            Catch ex As Exception
                MsgBox("Error al encriptar en 3DES: " & ex.ToString)
            End Try
            Return Nothing
        End SyncLock
    End Function

    'Función que desencripta la encriptación 3DES
    Public Function decryptString(ByVal str As String) As String
        SyncLock Me
            Dim ITransform As CryptoAPITransform = Nothing
            Dim encData() As Byte = Nothing
            Try
                Dim enc As New ASCIIEncoding
                Dim md5 As New MD5CryptoServiceProvider()
                Dim byteData() As Byte = enc.GetBytes(str)
                encData = Convert.FromBase64String(str)
                Dim tDes As New TripleDESCryptoServiceProvider()

                tDes.Key = md5.ComputeHash(enc.GetBytes("JAM"))
                tDes.IV = IVector

                ITransform = tDes.CreateDecryptor()
                Return Encoding.ASCII.GetString(ITransform.TransformFinalBlock(encData, 0, encData.Length()))
            Catch ex As Exception
                MsgBox("Error al desencriptar: " & ex.ToString)
            End Try
            Return Nothing
        End SyncLock
    End Function

    Public Sub obtenerClientes(ByVal Usuario As User, ByVal Socket As Cliente)
        Try
            Dim user As ArrayList = New ArrayList()
            user.Add(Usuario.User)
            solicitud = New Solicitud(3, user)
            Dim encriptado As String = Encriptar(solicitud, "Solicitud")
            Socket.EnviarDatos(encriptado)
        Catch ex As Exception
            MsgBox("Error al enviar solicitud para obtener los usuarios!" & vbCrLf & ex.Message)
        End Try
    End Sub

    Public Sub Validar(ByVal Data As Datos, ByVal Socket As Cliente)
        Try
            Dim datos As New ArrayList()
            datos.Add(Data.nomusuario)
            datos.Add(Data.passusuario)
            solicitud = New Solicitud(1, datos)
            Dim encriptado As String = Encriptar(solicitud, "Solicitud")
            Socket.EnviarDatos(encriptado)
        Catch ex As Exception
            MsgBox("Error al enviar solicitud de login!" & vbCrLf & ex.Message)
        End Try
    End Sub

    Public Sub nuevoCliente(ByVal Data As Datos, ByVal Socket As Cliente)
        Dim datos As New ArrayList()
        datos.Add(Data.nomusuario)
        datos.Add(Data.passusuario)
        solicitud = New Solicitud(4, datos)
        Dim encriptado As String = Encriptar(solicitud, "Solicitud")
    End Sub

    Public Sub ObtenerMensajes(ByVal usuario As User, ByVal para As User, ByVal Socket As Cliente)
        Try
            Dim parametros As ArrayList = New ArrayList()
            With parametros
                .Add(usuario.User)
                .Add(para.User)
            End With
            solicitud = New Solicitud(4, parametros)
            Dim encriptado As String = Encriptar(solicitud, "Solicitud")
            Socket.EnviarDatos(encriptado)
        Catch ex As Exception
            MsgBox("Error al recibir historial de mensajes!" & vbCrLf & ex.Message)
        End Try
    End Sub

    Public Function Encriptar(ByVal objeto As Solicitud, ByVal ruta As String) As String
        Try
            Dim txtXML As String = Serializar(objeto, ruta) 'funcion que convierte el mensaje a XML
            Dim md As String = MD5Encrypt(txtXML) 'Se encripta el XML en MD5
            Return encryptString(txtXML & "?XXXJAMXXX?" & md) 'Se encripta el MD5 con el XML en 3DES
        Catch ex As Exception
            MsgBox("Error al encriptar solicitud!" & vbCrLf & ex.Message)
        End Try
        Return ""
    End Function

    Public Sub Bitacora(ByVal descripcion As String) ', ByVal user As String)
        SyncLock Me
            'Dim fileWriter As StreamWriter = Nothing
            Try
                Dim user As String = "log"
                '   fileWriter = New StreamWriter(user & ".txt")
                '  fileWriter = My.Computer.FileSystem.OpenTextFileWriter(user & ".txt", True)
                ' fileWriter.WriteLine(descripcion)
                My.Computer.FileSystem.WriteAllText(user & ".txt", descripcion & "  " & DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") & vbCrLf, True)
            Catch ex As Exception
                MsgBox("Error al escribir en la bitácora: " & ex.Message)
            Finally
                'If Not IsNothing(fileWriter) Then
                'fileWriter.Close()
                'End If
            End Try
        End SyncLock
    End Sub
End Class
