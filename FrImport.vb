Imports System.Collections.Generic

Imports Grasshopper.Kernel
Imports Rhino.Geometry
Imports GH2SAP.GH2SAPIO
Imports SAP2000v17


Public Class FrImport
    Inherits GH_Component
    ''' <summary>
    ''' Initializes a new instance of the FrImport class.
    ''' </summary>
    Public Sub New()
        MyBase.New("FrImport", "FrImp", _
                    "Imports frames from line geometry", _
                    "GH2SAP", "Elements")
    End Sub

    ''' <summary>
    ''' Registers all the input parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterInputParams(pManager As GH_Component.GH_InputParamManager)
        pManager.AddLineParameter("Frame", "Fr", "Frame element to export", GH_ParamAccess.item)
        pManager.AddTextParameter("Frame Section", "FS", "Frame section to apply to the imported element", GH_ParamAccess.item, "Default")
        pManager.AddTextParameter("Name", "N", "Frame object name", GH_ParamAccess.item, "FrameElemt")
        pManager.AddBooleanParameter("Start", "S", "Boolean flag to start importing", GH_ParamAccess.item, False)
    End Sub

    ''' <summary>
    ''' Registers all the output parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterOutputParams(pManager As GH_Component.GH_OutputParamManager)
        pManager.AddBooleanParameter("Pass", "P", "Pass value. True when the frame objects has been loaded into SAP2000", GH_ParamAccess.item)
    End Sub

    ''' <summary>
    ''' This is the method that actually does the work.
    ''' </summary>
    ''' <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
    Protected Overrides Sub SolveInstance(DA As IGH_DataAccess)

        Dim Frame As New Line
        Dim strFrameSect As String = Nothing
        Dim strName As String = Nothing
        Dim bFlag As New Boolean
        Dim VtxNumber As Integer
        Dim sapModel As cSapModel
        Dim pt0, pt1 As Point3d
        Dim x(), y(), z() As Double

        'Getting values from inputs
        If (Not DA.GetData(0, Frame)) Then Return
        If (Not DA.GetData(1, strFrameSect)) Then Return
        If (Not DA.GetData(2, strName)) Then Return
        If (Not DA.GetData(3, bFlag)) Then Return

        'Checking toggle
        If bFlag Then

            'Initializing sapModel
            sapModel = mySapObject.SapModel


            VtxNumber = 2
            pt0 = Frame.From
            pt0 = Frame.To

            x(0) = pt0.X
            y(0) = pt0.Y
            z(0) = pt0.Z
            x(1) = pt1.X
            y(1) = pt1.Y
            z(1) = pt1.Z

            'Turning pass value True
            DA.SetData(0, True)
        Else

            'Turning pass value False
            DA.SetData(0, False)

        End If

    End Sub

    ''' <summary>
    ''' Provides an Icon for every component that will be visible in the User Interface.
    ''' Icons need to be 24x24 pixels.
    ''' </summary>
    Protected Overrides ReadOnly Property Icon() As System.Drawing.Bitmap
        Get
            Return My.Resources.Frame
        End Get
    End Property

    ''' <summary>
    ''' Gets the unique ID for this component. Do not change this ID after release.
    ''' </summary>
    Public Overrides ReadOnly Property ComponentGuid() As Guid
        Get
            Return New Guid("{4bc2e720-755b-407e-afa8-665291f7d47f}")
        End Get
    End Property
End Class