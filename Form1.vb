Public Class Form1
    Inherits Windows.Forms.Form

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

End Class

Public Class NumericUpDownUnits
    Inherits Windows.Forms.NumericUpDown

    Public unit As String

    Public Sub New()
    End Sub

    Protected Overrides Sub UpdateEditText()
        MyBase.Text = Str(Me.Value) + unit
    End Sub

End Class