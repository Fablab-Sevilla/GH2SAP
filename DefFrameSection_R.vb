Imports System.Collections.Generic

Imports Grasshopper.Kernel
Imports Rhino.Geometry
Imports GH2SAP.GH2SAPIO
Imports SAP2000v17


Public Class DefFrameSection_R
    Inherits GH_Component
    ''' <summary>
    ''' Initializes a new instance of the DefFrameSection class.
    ''' </summary>
    Public Sub New()
        MyBase.New("FrameSection", "FS", _
                    "Defines a new rectangular frame section in SAP2000 or modifies and existing one", _
                    "GH2SAP", "Define")
    End Sub

    ''' <summary>
    ''' Registers all the input parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterInputParams(pManager As GH_Component.GH_InputParamManager)

        pManager.AddTextParameter("Name", "N", "The name of an existing or new frame section property", GH_ParamAccess.item, "IPE300")
        pManager.AddTextParameter("MatProp", "M", "The name of the material property for the section", GH_ParamAccess.item, "A992Fy50")
        pManager.AddNumberParameter("Height", "T3", "The section height", GH_ParamAccess.item, 0.1)
        pManager.AddNumberParameter("TFWidth", "T2", "Top flange width", GH_ParamAccess.item, 0.05)
        pManager.AddNumberParameter("TFThick", "TF", "Flange thickness", GH_ParamAccess.item, 0.005)
        pManager.AddNumberParameter("WThick", "TW", "Web thickness", GH_ParamAccess.item, 0.005)
        pManager.AddNumberParameter("BFWidth", "T2B", "Bottom flange width", GH_ParamAccess.item, 0.15)
        pManager.AddNumberParameter("BFThick", "TFB", "Bottom flange thickness", GH_ParamAccess.item, 0.0107)
        pManager.AddIntegerParameter("Color", "C", "Display color assigned to the section", GH_ParamAccess.item, -1)
        pManager.AddBooleanParameter("Start", "S", "Boolean flag to start importing", GH_ParamAccess.item, False)

        'We should add Notes and GUID as input in future revisions.

    End Sub

    ''' <summary>
    ''' Registers all the output parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterOutputParams(pManager As GH_Component.GH_OutputParamManager)

        pManager.AddBooleanParameter("Pass", "P", "Pass value. True when the area objects has been loaded into SAP2000", GH_ParamAccess.item)

    End Sub

    ''' <summary>
    ''' This is the method that actually does the work.
    ''' </summary>
    ''' <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
    Protected Overrides Sub SolveInstance(DA As IGH_DataAccess)

        'Declaring variables
        Dim strName As String = Nothing
        Dim strMatProp As String = Nothing
        Dim dT3, dT2, dTF, dTW As Double
        Dim intColor, intTest As Integer
        Dim bFlag As Boolean
        Dim sapModel As cSapModel

        'Getting values from inputs
        If (Not DA.GetData(0, strName)) Then Return
        If (Not DA.GetData(1, strMatProp)) Then Return
        If (Not DA.GetData(2, dT3)) Then Return
        If (Not DA.GetData(3, dT2)) Then Return
        If (Not DA.GetData(4, dTF)) Then Return
        If (Not DA.GetData(5, dTW)) Then Return
        If (Not DA.GetData(8, intColor)) Then Return
        If (Not DA.GetData(9, bFlag)) Then Return

        'Main check
        If bFlag Then

            sapModel = mySapObject.SapModel

            intTest = sapModel.PropFrame.SetTube(strName, strMatProp, dT3, dT2, dTF, dTW, intColor)

        End If

        'Passing true when profile properly created.
        If intTest = 0 Then DA.SetData(0, True)

    End Sub

    ''' <summary>
    ''' Provides an Icon for every component that will be visible in the User Interface.
    ''' Icons need to be 24x24 pixels.
    ''' </summary>
    Protected Overrides ReadOnly Property Icon() As System.Drawing.Bitmap
        Get
            'You can add image files to your project resources and access them like this:
            ' return Resources.IconForThisComponent;
            Return My.Resources.Frame_R
        End Get
    End Property

    ''' <summary>
    ''' Gets the unique ID for this component. Do not change this ID after release.
    ''' </summary>
    Public Overrides ReadOnly Property ComponentGuid() As Guid
        Get
            Return New Guid("{03905594-8d2c-44d7-9a7d-963853d983d6}")
        End Get
    End Property
End Class