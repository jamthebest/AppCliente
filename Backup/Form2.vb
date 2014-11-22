Public Class frmServer
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Sock As AxMSWinsockLib.AxWinsock
    Friend WithEvents LblStatus As System.Windows.Forms.Label
    Friend WithEvents ServerAcciones As System.Windows.Forms.ListBox
    Friend WithEvents Txtarrive As System.Windows.Forms.TextBox
    Friend WithEvents txtSend As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmServer))
        Me.Sock = New AxMSWinsockLib.AxWinsock()
        Me.ServerAcciones = New System.Windows.Forms.ListBox()
        Me.LblStatus = New System.Windows.Forms.Label()
        Me.Txtarrive = New System.Windows.Forms.TextBox()
        Me.txtSend = New System.Windows.Forms.TextBox()
        CType(Me.Sock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Sock
        '
        Me.Sock.Enabled = True
        Me.Sock.Location = New System.Drawing.Point(8, 8)
        Me.Sock.Name = "Sock"
        Me.Sock.OcxState = CType(resources.GetObject("Sock.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Sock.Size = New System.Drawing.Size(28, 28)
        Me.Sock.TabIndex = 0
        '
        'ServerAcciones
        '
        Me.ServerAcciones.Location = New System.Drawing.Point(40, 24)
        Me.ServerAcciones.Name = "ServerAcciones"
        Me.ServerAcciones.Size = New System.Drawing.Size(672, 95)
        Me.ServerAcciones.TabIndex = 1
        '
        'LblStatus
        '
        Me.LblStatus.Location = New System.Drawing.Point(48, 8)
        Me.LblStatus.Name = "LblStatus"
        Me.LblStatus.TabIndex = 2
        Me.LblStatus.Text = "Actions:"
        '
        'Txtarrive
        '
        Me.Txtarrive.Location = New System.Drawing.Point(40, 128)
        Me.Txtarrive.Multiline = True
        Me.Txtarrive.Name = "Txtarrive"
        Me.Txtarrive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Txtarrive.Size = New System.Drawing.Size(672, 104)
        Me.Txtarrive.TabIndex = 3
        Me.Txtarrive.Text = ""
        '
        'txtSend
        '
        Me.txtSend.Location = New System.Drawing.Point(40, 240)
        Me.txtSend.Name = "txtSend"
        Me.txtSend.Size = New System.Drawing.Size(672, 20)
        Me.txtSend.TabIndex = 4
        Me.txtSend.Text = ""
        '
        'frmServer
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(744, 286)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.txtSend, Me.Txtarrive, Me.ServerAcciones, Me.Sock, Me.LblStatus})
        Me.Name = "frmServer"
        Me.Text = "Server"
        CType(Me.Sock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmServer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Sock.LocalPort = 2909
        Sock.Listen()
        ServerAcciones.Items.Add("Server is listening...")

    End Sub

    Private Sub Sock_ConnectionRequest(ByVal sender As Object, ByVal e As AxMSWinsockLib.DMSWinsockControlEvents_ConnectionRequestEvent) Handles Sock.ConnectionRequest

        Sock.Close()
        ServerAcciones.Items.Add("Attemp of Connection...")
        Sock.Accept(e.requestID)
        ServerAcciones.Items.Add("Connected!!!...")

        Sock.SendData("Wellcome to Server!!!, U can chat whit me now!")

    End Sub


    Private Sub txtSend_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSend.KeyPress
        If Asc(e.KeyChar) = 13 Then


            Sock.SendData(txtSend.Text)
            Txtarrive.Text = String.Concat(Txtarrive.Text, vbNewLine & txtSend.Text)
            txtSend.Text = ""
            ServerAcciones.Items.Add("Msg sent!")

        End If
    End Sub

    Private Sub Sock_DataArrival(ByVal sender As Object, ByVal e As AxMSWinsockLib.DMSWinsockControlEvents_DataArrivalEvent) Handles Sock.DataArrival
        Dim A As Object
        Dim NewString As String
        Dim Character As Byte

        ServerAcciones.Items.Add("Msg arrival!")
        Sock.GetData(A)

        'BECAUSE THIS VERSION OF WINSOCK IS DIFFERENT THAN VB6 WINSOCK
        'Only Bytes can be sent with winsock, so we'll going to reseive a lot of bytes, and we 
        'must Cast them to Chars

        For Each Character In A
            NewString = String.Concat(NewString, Chr(Character))
        Next

        'After that, concat into txtArrive textBox
        Txtarrive.Text = String.Concat(Txtarrive.Text, vbNewLine, NewString)

    End Sub

    Private Sub Sock_ConnectEvent(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Sock.ConnectEvent

    End Sub
End Class
