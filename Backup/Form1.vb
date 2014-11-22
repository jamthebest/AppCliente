Public Class frmClient
    Inherits System.Windows.Forms.Form
    'Made By FUCCI, LEONARDO HERNAN
    'Alias UvChenko!

    'visit www.meetfinder.ar.tc
    'Please vote for this code....!!!!

    'You can steal this code, I give it free to you!!!, because on this web I found a lot
    'of free code that help me a lot, and I want to give back the Favor... Do it Yourself Two!!!
    'Share your knowledge!!!

    'UNDERSUN Studios. (r)
    'check www.MeetFinder.ar.tc

    'MEETFINDER (r)

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
    Friend WithEvents txtSend As System.Windows.Forms.TextBox
    Friend WithEvents Txtarrive As System.Windows.Forms.TextBox
    Friend WithEvents BtnConnect As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmClient))
        Me.Sock = New AxMSWinsockLib.AxWinsock()
        Me.txtSend = New System.Windows.Forms.TextBox()
        Me.Txtarrive = New System.Windows.Forms.TextBox()
        Me.BtnConnect = New System.Windows.Forms.Button()
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
        Me.Sock.TabIndex = 1
        '
        'txtSend
        '
        Me.txtSend.Location = New System.Drawing.Point(48, 120)
        Me.txtSend.Name = "txtSend"
        Me.txtSend.Size = New System.Drawing.Size(672, 20)
        Me.txtSend.TabIndex = 6
        Me.txtSend.Text = ""
        '
        'Txtarrive
        '
        Me.Txtarrive.Location = New System.Drawing.Point(48, 8)
        Me.Txtarrive.Multiline = True
        Me.Txtarrive.Name = "Txtarrive"
        Me.Txtarrive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Txtarrive.Size = New System.Drawing.Size(672, 104)
        Me.Txtarrive.TabIndex = 5
        Me.Txtarrive.Text = ""
        '
        'BtnConnect
        '
        Me.BtnConnect.Location = New System.Drawing.Point(624, 160)
        Me.BtnConnect.Name = "BtnConnect"
        Me.BtnConnect.Size = New System.Drawing.Size(96, 23)
        Me.BtnConnect.TabIndex = 7
        Me.BtnConnect.Text = "Connect now"
        '
        'frmClient
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(784, 214)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.BtnConnect, Me.txtSend, Me.Txtarrive, Me.Sock})
        Me.Name = "frmClient"
        Me.Text = "Connector"
        CType(Me.Sock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmClient_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim frmServer = New frmServer()

        frmServer.Show()


    End Sub

    Private Sub txtSend_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSend.KeyPress
        If Asc(e.KeyChar) = 13 Then

            Sock.SendData(txtSend.Text)
            Txtarrive.Text = String.Concat(Txtarrive.Text, vbNewLine & txtSend.Text)
            txtSend.Text = ""

        End If
    End Sub

    Private Sub Sock_DataArrival(ByVal sender As Object, ByVal e As AxMSWinsockLib.DMSWinsockControlEvents_DataArrivalEvent) Handles Sock.DataArrival
        Dim A As Object
        Dim NewString As String
        Dim Character As Byte

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

    Private Sub BtnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnConnect.Click
        BtnConnect.Enabled = False

        Sock.RemotePort = 2909
        Sock.RemoteHost = Sock.LocalIP  ' Or localhost
        'If you are connected to a LAN, you must put your ipaddress

        Sock.Connect()

    End Sub
End Class
