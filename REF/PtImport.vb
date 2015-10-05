Imports System.Collections.Generic

Imports Grasshopper.Kernel
Imports Rhino.Geometry
Imports SAP2000v16
Imports GH2SAP.GH2SAPIO


Public Class PtImport
    Inherits GH_Component
    ''' <summary>
    ''' Initializes a new instance of the MyComponent1 class.
    ''' </summary>
    Public Sub New()
        MyBase.New("Pt_Import", "PtImport", _
                    "Imports points to SAP2000", _
                    "GH2SAP", "Elements")
    End Sub

    Dim number As Integer = 0

    ''' <summary>
    ''' Registers all the input parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterInputParams(pManager As GH_Component.GH_InputParamManager)
        pManager.AddPointParameter("Point", "P", "Point/s to add to SAP2000", GH_ParamAccess.item)
        pManager.AddTextParameter("Name", "N", "Point name in SAP2000", GH_ParamAccess.item, "pt")
        pManager.AddTextParameter("Restraint", "R", "Point restraint condition", GH_ParamAccess.item)
        pManager.AddBooleanParameter("Start", "S", "Boolean flag to start importing", GH_ParamAccess.item, False)
    End Sub

    ''' <summary>
    ''' Registers all the output parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterOutputParams(pManager As GH_Component.GH_OutputParamManager)
        pManager.AddBooleanParameter("Pass", "P", "Pass value. True when SAP2000 is loaded", GH_ParamAccess.item)
    End Sub

    Private Sub ParamChangeHandler(ByVal sender As Object, ByVal e As GH_SolutionExpiredEventArgs) Handles MyBase.SolutionExpired

        number += 1

        If (number Mod 3 = 0) And number > 0 Then
            'obj.SetAsActiveObject()
            obj.SapModel.InitializeNewModel(6)
            obj.SapModel.File.NewBlank()
            obj.SapModel.View.RefreshView(0, True)
            number = 0
        End If


    End Sub

    ''' <summary>
    ''' This is the method that actually does the work.
    ''' </summary>
    ''' <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
    Protected Overrides Sub SolveInstance(DA As IGH_DataAccess)

        Dim PtRef As New Point3d
        Dim StrRest As String = Nothing
        Dim Name As String = Nothing
        Dim SapModel As cSapModel
        Dim SapObject As SAP2000v16.SapObject
        Dim Restraint(5) As Boolean
        Dim flag As Boolean

        If (Not DA.GetData(0, PtRef)) Then Return
        If (Not DA.GetData(1, Name)) Then Return
        If (Not DA.GetData(2, StrRest)) Then Return
        If (Not DA.GetData(3, flag)) Then Return


        If (flag) Then

            SapObject = obj
            SapObject.SetAsActiveObject()

            SapModel = SapObject.SapModel

            'I have to check the conversion between string and boolean.
            'Perhaps with a conditional statement with '0' or '1' as options it could work.
            For i As Integer = 0 To 5 'Restraint array created using a loop
                Dim intDum = Convert.ToInt32(StrRest(i))
                Restraint(i) = Convert.ToBoolean(intDum)
            Next

            SapModel.PointObj.AddCartesian(PtRef.X, PtRef.Y, PtRef.Z, Name)
            SapModel.PointObj.SetRestraint(Name, Restraint)

        End If

    End Sub

    ''' <summary>
    ''' Provides an Icon for every component that will be visible in the User Interface.
    ''' Icons need to be 24x24 pixels.
    ''' </summary>
    Protected Overrides ReadOnly Property Icon() As System.Drawing.Bitmap
        Get
            'You can add image files to your project resources and access them like this:
            ' return Resources.IconForThisComponent;
            Return My.Resources.Pt
        End Get
    End Property

    ''' <summary>
    ''' Gets the unique ID for this component. Do not change this ID after release.
    ''' </summary>
    Public Overrides ReadOnly Property ComponentGuid() As Guid
        Get
            Return New Guid("{c9f29644-3033-4b8b-86d6-4d399ee41d69}")
        End Get
    End Property
End Class