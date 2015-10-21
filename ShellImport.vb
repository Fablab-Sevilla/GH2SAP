Imports System.Collections.Generic

Imports Grasshopper.Kernel
Imports Rhino.Geometry


Public Class ShellImport
    Inherits GH_Component
    ''' <summary>
    ''' Initializes a new instance of the MyComponent1 class.
    ''' </summary>
    Public Sub New()
        MyBase.New("ShellImport", "SI", _
                    "Import shell elements into SAP2000", _
                    "GH2SAP", "Elements")
    End Sub

    ''' <summary>
    ''' Registers all the input parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterInputParams(pManager As GH_Component.GH_InputParamManager)
        pManager.AddMeshParameter("Mesh", "M", "Mesh element to export", GH_ParamAccess.item)
        pManager.AddTextParameter("Area Section", "AS", "Area section to apply to the imported element", GH_ParamAccess.item, "Default")

        'We should create some criteria about how to assign names to elements and how to deal with not user assigned names to elements
        pManager.AddTextParameter("Name", "N", "Area object name", GH_ParamAccess.item, "AreaElemt")

        pManager.AddBooleanParameter("Start", "S", "Boolean flag to start importing", GH_ParamAccess.item, False)

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

        Dim meshArea As New Mesh
        Dim strAreaSect As String = Nothing
        Dim strName As String = Nothing
        Dim bFlag As New Boolean

        'Getting values from inputs
        If (Not DA.GetData(0, meshArea)) Then Return
        If (Not DA.GetData(1, strAreaSect)) Then Return
        If (Not DA.GetData(2, strName)) Then Return
        If (Not DA.GetData(3, bFlag)) Then Return

        'Checking toggle
        If bFlag Then




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
            'You can add image files to your project resources and access them like this:
            ' return Resources.IconForThisComponent;
            Return My.Resources.Area
        End Get
    End Property

    ''' <summary>
    ''' Gets the unique ID for this component. Do not change this ID after release.
    ''' </summary>
    Public Overrides ReadOnly Property ComponentGuid() As Guid
        Get
            Return New Guid("{6dca9a07-57f7-4e97-8cd3-8f2a6a6e9900}")
        End Get
    End Property
End Class