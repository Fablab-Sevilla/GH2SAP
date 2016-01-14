Imports GH2SAP.GH2SAPIO
Imports SAP2000v17
Imports Grasshopper.Kernel
Imports Rhino.Runtime

'Just a dumb file to test references and other things or create classes or tools

' This Class is necessary to avoid errors importing the SAP2000 dll._
' It prioritizes to load the reference before the plug-in starts.
Public Class AddReferencePriority
    Inherits GH_AssemblyPriority

    Public Overrides Function PriorityLoad() As GH_LoadingInstruction
        Rhino.Runtime.AssemblyResolver.AddSearchFile("C:\Program Files (x86)\Computers and Structures\SAP2000 17\SAP2000v17.dll")
        Return GH_LoadingInstruction.Proceed
    End Function
End Class

'Public Class Util

'    Dim a As cOAPI = GH2SAPIO.mySapObject
'    Dim mySapModel As cSapModel = a.SapModel
'    Dim ret As Integer = 0

'    Sub Pepe()

'        ret = mySapModel.InitializeNewModel

'    End Sub


'End Class


