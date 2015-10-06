Imports GH2SAP.GH2SAPIO
Imports SAP2000v17

'Just a dumb file to test references and other things or create classes or tools

Public Class Util

    Dim a As cOAPI = GH2SAPIO.mySapObject
    Dim mySapModel As cSapModel = a.SapModel
    Dim ret As Integer = 0

    Sub Pepe()

        ret = mySapModel.InitializeNewModel

    End Sub


End Class
