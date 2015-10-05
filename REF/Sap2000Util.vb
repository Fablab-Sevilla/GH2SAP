Imports SAP2000v16
Imports System.Collections.Generic

''' <summary>
''' Class that compile utilities to deal with SAP2000 instances
''' and create a common plug-in environment.
''' </summary>
''' <remarks></remarks>
''' 

Public Class Sap2000Util

    Dim obj As New SapObject
    Dim ret As Long

    Public Function StartSap2000() As SapObject
        
        obj.ApplicationStart(6, True, "GH2SAP")
        obj.SapModel.InitializeNewModel(6)
        ret = obj.SapModel.File.NewBlank
        Return obj

    End Function

    Public Function StopSap2000()

        ret = obj.SetAsActiveObject
        ret = obj.ApplicationExit(False)
        obj.SapModel = Nothing
        obj = Nothing
        Return obj

    End Function


    'Public Sub StartSap2000()

    'Dim ret As Long

    'SapObject.ApplicationStart()
    'SapModel = SapObject.SapModel

    'ret = SapModel.InitializeNewModel
    'ret = SapModel.File.NewBlank
    'ret = SapModel.SetPresentUnits(eUnits.kN_m_C)

    'End Sub

    'Public Sub StopSap2000()
    '  ret = SapObject.ApplicationExit(False)
    '   SapModel = Nothing
    '    SapObject = Nothing
    'End Sub

    'Public Sub ResetSap2000()
    '    ret = SapModel.InitializeNewModel
    '    ret = SapModel.File.NewBlank
    '    ret = SapModel.SetPresentUnits(eUnits.kN_m_C)
    'End Sub

End Class
