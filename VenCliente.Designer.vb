<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VenCliente
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VenCliente))
        Me.lstClients = New System.Windows.Forms.ListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.cmdSalir = New System.Windows.Forms.Button()
        Me.btnChat = New System.Windows.Forms.Button()
        Me.btnBitacora = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lstClients
        '
        Me.lstClients.FormattingEnabled = True
        Me.lstClients.Location = New System.Drawing.Point(56, 41)
        Me.lstClients.Name = "lstClients"
        Me.lstClients.Size = New System.Drawing.Size(159, 212)
        Me.lstClients.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Monotype Corsiva", 14.25!, CType((System.Drawing.FontStyle.Italic Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(58, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(155, 22)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Usuarios Registrados"
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Image = Global.Proyecto.My.Resources.Resources.refresh
        Me.cmdRefresh.Location = New System.Drawing.Point(77, 259)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(55, 55)
        Me.cmdRefresh.TabIndex = 12
        Me.cmdRefresh.UseVisualStyleBackColor = True
        '
        'cmdSalir
        '
        Me.cmdSalir.Image = Global.Proyecto.My.Resources.Resources.Apagar1
        Me.cmdSalir.Location = New System.Drawing.Point(199, 259)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(55, 55)
        Me.cmdSalir.TabIndex = 11
        Me.cmdSalir.UseVisualStyleBackColor = True
        '
        'btnChat
        '
        Me.btnChat.Image = Global.Proyecto.My.Resources.Resources.chat
        Me.btnChat.Location = New System.Drawing.Point(16, 259)
        Me.btnChat.Name = "btnChat"
        Me.btnChat.Size = New System.Drawing.Size(55, 55)
        Me.btnChat.TabIndex = 10
        Me.btnChat.UseVisualStyleBackColor = True
        '
        'btnBitacora
        '
        Me.btnBitacora.Image = Global.Proyecto.My.Resources.Resources.log
        Me.btnBitacora.Location = New System.Drawing.Point(138, 259)
        Me.btnBitacora.Name = "btnBitacora"
        Me.btnBitacora.Size = New System.Drawing.Size(55, 55)
        Me.btnBitacora.TabIndex = 13
        Me.btnBitacora.UseVisualStyleBackColor = True
        '
        'VenCliente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(274, 328)
        Me.Controls.Add(Me.btnBitacora)
        Me.Controls.Add(Me.cmdRefresh)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.btnChat)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lstClients)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "VenCliente"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cliente"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstClients As System.Windows.Forms.ListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnChat As System.Windows.Forms.Button
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents btnBitacora As System.Windows.Forms.Button

End Class
