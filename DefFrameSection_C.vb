Imports System.Collections.Generic

Imports Grasshopper.Kernel
Imports Rhino.Geometry
Imports GH2SAP.GH2SAPIO
Imports SAP2000v17


Public Class DefFrameSection_C
    Inherits GH_Component
    ''' <summary>
    ''' Initializes a new instance of the DefFrameSection class.
    ''' </summary>
    Public Sub New()
        MyBase.New("FrameSection", "FS", _
                    "Defines a new circular frame section in SAP2000 or modifies and existing one", _
                    "GH2SAP", "Define")
    End Sub

    ''' <summary>
    ''' Registers all the input parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterInputParams(pManager As GH_Component.GH_InputParamManager)

        pManager.AddTextParameter("Name", "N", "The name of an existing or new frame section property", GH_ParamAccess.item, "CHS100")
        pManager.AddTextParameter("MatProp", "M", "The name of the material property for the section", GH_ParamAccess.item, "A992Fy50")
        pManager.AddNumberParameter("Outside diameter", "T3", "The section total diameter", GH_ParamAccess.item, 0.1)
        pManager.AddNumberParameter("WallThick", "TW", "The wall thickness", GH_ParamAccess.item, 0.005)
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
        Dim dT3, dTW As Double
        Dim intColor, intTest As Integer
        Dim bFlag As Boolean
        Dim sapModel As cSapModel


        'Getting values from inputs
        If (Not DA.GetData(0, strName)) Then Return
        If (Not DA.GetData(1, strMatProp)) Then Return
        If (Not DA.GetData(2, dT3)) Then Return
        If (Not DA.GetData(3, dTW)) Then Return
        If (Not DA.GetData(4, intColor)) Then Return
        If (Not DA.GetData(5, bFlag)) Then Return

        If bFlag Then

            sapModel = mySapObject.SapModel

            intTest = sapModel.PropFrame.SetPipe(strName, strMatProp, dT3, dTW, intColor)

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
            Return My.Resources.Frame_O
        End Get
    End Property

    ''' <summary>
    ''' Gets the unique ID for this component. Do not change this ID after release.
    ''' </summary>
    Public Overrides ReadOnly Property ComponentGuid() As Guid
        Get
            Return New Guid("{76d1bc7c-410c-4b11-9240-8af103aed89a}")
        End Get
    End Property
End Class