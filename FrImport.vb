Imports System.Collections.Generic

Imports Grasshopper.Kernel
Imports Rhino.Geometry


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
        pManager.AddLineParameter("Mesh", "M", "Mesh element to export", GH_ParamAccess.item)
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

        Dim meshLine As New LineCurve
        Dim strLineSect As String = Nothing
        Dim strName As String = Nothing
        Dim bFlag As New Boolean
        Dim VtxNumber As Integer

        'Getting values from inputs
        If (Not DA.GetData(0, meshLine)) Then Return
        If (Not DA.GetData(1, strFrameSect)) Then Return
        If (Not DA.GetData(2, strName)) Then Return
        If (Not DA.GetData(3, bFlag)) Then Return



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