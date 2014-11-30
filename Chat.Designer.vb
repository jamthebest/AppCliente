<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Chat
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Chat))
        Me.txtMensaje = New System.Windows.Forms.TextBox()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.LineShape2 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.LineShape1 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.txtHistorial = New System.Windows.Forms.ListBox()
        Me.btnEnviar = New System.Windows.Forms.Button()
        Me.cmdReproducir = New System.Windows.Forms.Button()
        Me.cmdDetener = New System.Windows.Forms.Button()
        Me.cmdGrabar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtMensaje
        '
        Me.txtMensaje.Location = New System.Drawing.Point(13, 389)
        Me.txtMensaje.Multiline = True
        Me.txtMensaje.Name = "txtMensaje"
        Me.txtMensaje.Size = New System.Drawing.Size(262, 43)
        Me.txtMensaje.TabIndex = 0
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape2, Me.LineShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(287, 500)
        Me.ShapeContainer1.TabIndex = 3
        Me.ShapeContainer1.TabStop = False
        '
        'LineShape2
        '
        Me.LineShape2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LineShape2.BorderWidth = 3
        Me.LineShape2.Name = "LineShape2"
        Me.LineShape2.X1 = 9
        Me.LineShape2.X2 = 272
        Me.LineShape2.Y1 = 262
        Me.LineShape2.Y2 = 262
        '
        'LineShape1
        '
        Me.LineShape1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LineShape1.BorderWidth = 3
        Me.LineShape1.Name = "LineShape1"
        Me.LineShape1.X1 = 10
        Me.LineShape1.X2 = 273
        Me.LineShape1.Y1 = 379
        Me.LineShape1.Y2 = 379
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Magneto", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(13, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 20)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Chat con: "
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(35, 354)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Sin Audio"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(215, 354)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(0, 13)
        Me.Label2.TabIndex = 9
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(129, 354)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 13)
        Me.Label3.TabIndex = 10
        '
        'Timer2
        '
        Me.Timer2.Interval = 1000
        '
        'txtHistorial
        '
        Me.txtHistorial.FormattingEnabled = True
        Me.txtHistorial.Location = New System.Drawing.Point(17, 47)
        Me.txtHistorial.Name = "txtHistorial"
        Me.txtHistorial.Size = New System.Drawing.Size(250, 199)
        Me.txtHistorial.TabIndex = 11
        '
        'btnEnviar
        '
        Me.btnEnviar.Image = Global.Proyecto.My.Resources.Resources.send
        Me.btnEnviar.Location = New System.Drawing.Point(116, 439)
        Me.btnEnviar.Name = "btnEnviar"
        Me.btnEnviar.Size = New System.Drawing.Size(55, 55)
        Me.btnEnviar.TabIndex = 1
        Me.btnEnviar.UseVisualStyleBackColor = True
        '
        'cmdReproducir
        '
        Me.cmdReproducir.Enabled = False
        Me.cmdReproducir.Image = Global.Proyecto.My.Resources.Resources.button_play_red
        Me.cmdReproducir.Location = New System.Drawing.Point(212, 282)
        Me.cmdReproducir.Name = "cmdReproducir"
        Me.cmdReproducir.Size = New System.Drawing.Size(55, 55)
        Me.cmdReproducir.TabIndex = 7
        Me.cmdReproducir.UseVisualStyleBackColor = True
        '
        'cmdDetener
        '
        Me.cmdDetener.Enabled = False
        Me.cmdDetener.Image = Global.Proyecto.My.Resources.Resources.button_stop_red
        Me.cmdDetener.Location = New System.Drawing.Point(117, 282)
        Me.cmdDetener.Name = "cmdDetener"
        Me.cmdDetener.Size = New System.Drawing.Size(55, 55)
        Me.cmdDetener.TabIndex = 6
        Me.cmdDetener.UseVisualStyleBackColor = True
        '
        'cmdGrabar
        '
        Me.cmdGrabar.Image = Global.Proyecto.My.Resources.Resources.media_record
        Me.cmdGrabar.Location = New System.Drawing.Point(17, 282)
        Me.cmdGrabar.Name = "cmdGrabar"
        Me.cmdGrabar.Size = New System.Drawing.Size(55, 55)
        Me.cmdGrabar.TabIndex = 5
        Me.cmdGrabar.UseVisualStyleBackColor = True
        '
        'Chat
        '
        Me.AcceptButton = Me.btnEnviar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(287, 500)
        Me.Controls.Add(Me.txtHistorial)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdReproducir)
        Me.Controls.Add(Me.cmdDetener)
        Me.Controls.Add(Me.cmdGrabar)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnEnviar)
        Me.Controls.Add(Me.txtMensaje)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Chat"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Chat"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtMensaje As System.Windows.Forms.TextBox
    Friend WithEvents btnEnviar As System.Windows.Forms.Button
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents LineShape1 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents LineShape2 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents cmdGrabar As System.Windows.Forms.Button
    Friend WithEvents cmdDetener As System.Windows.Forms.Button
    Friend WithEvents cmdReproducir As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents txtHistorial As System.Windows.Forms.ListBox

End Class
