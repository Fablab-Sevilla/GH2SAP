Imports System.Collections.Generic

Imports Grasshopper.Kernel
Imports Rhino.Geometry
Imports SAP2000v16
Imports GH2SAP.GH2SAPIO


Public Class LnImport
    Inherits GH_Component
    ''' <summary>
    ''' Initializes a new instance of the MyComponent1 class.
    ''' </summary>
    Public Sub New()
        MyBase.New("LnImport", "LnImp", _
                    "Import lines as frames", _
                    "GH2SAP", "Elements")
    End Sub

    Dim number As Integer = 0

    ''' <summary>
    ''' Registers all the input parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterInputParams(pManager As GH_Component.GH_InputParamManager)
        pManager.AddBooleanParameter("Run", "R", "Run the importing routine", GH_ParamAccess.item)
        pManager.AddLineParameter("Line", "L", "Line to export as frame", GH_ParamAccess.item)
        pManager.AddTextParameter("Names", "N", "Names given to the imported frames", GH_ParamAccess.item, "GH_Frame")
        pManager.AddTextParameter("Material", "M", "Material to apply to the frame element", GH_ParamAccess.item, "Default")
        pManager.AddTextParameter("Section", "S", "Section to apply to the imported frame", GH_ParamAccess.item, "Default")
    End Sub

    ''' <summary>
    ''' Registers all the output parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterOutputParams(pManager As GH_Component.GH_OutputParamManager)
        pManager.AddBooleanParameter("Pass boolean", "P", "True when component executed", GH_ParamAccess.item)
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

        Dim Ln As Line
        Dim Name As String = Nothing
        Dim Mat As String = Nothing
        Dim Sect As String = Nothing
        Dim SapModel As cSapModel
        Dim SapObject As SAP2000v16.SapObject
        Dim flag As Boolean
        Dim pt1, pt2 As New Point3d

        If (Not DA.GetData(0, flag)) Then Return
        If (Not DA.GetData(1, Ln)) Then Return
        If (Not DA.GetData(2, Name)) Then Return
        If (Not DA.GetData(3, Mat)) Then Return
        If (Not DA.GetData(4, Sect)) Then Return

        pt1 = Ln.From
        pt2 = Ln.To


        'We need to add some system to retrieve or create materials and sections using Mat variable.
        If flag Then

            SapObject = obj
            SapObject.SetAsActiveObject()
            SapModel = SapObject.SapModel
            SapModel.FrameObj.AddByCoord(pt1.X, pt1.Y, pt1.Z, pt2.X, pt2.Y, pt2.Z, Name, Sect)

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
            Return My.Resources.Frame
        End Get
    End Property

    ''' <summary>
    ''' Gets the unique ID for this component. Do not change this ID after release.
    ''' </summary>
    Public Overrides ReadOnly Property ComponentGuid() As Guid
        Get
            Return New Guid("{52514043-b862-46ba-8c1d-8720482cf055}")
        End Get
    End Property
End Class