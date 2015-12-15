Imports System.Collections.Generic

Imports Grasshopper.Kernel
Imports Rhino.Geometry
Imports GH2SAP.GH2SAPIO
Imports SAP2000v17

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
        Dim VtxNumber As Integer
        Dim sapModel As cSapModel
        Dim pt0, pt1, pt2, pt3 As Point3d
        Dim x(), y(), z() As Double


        'Getting values from inputs
        If (Not DA.GetData(0, meshArea)) Then Return
        If (Not DA.GetData(1, strAreaSect)) Then Return
        If (Not DA.GetData(2, strName)) Then Return
        If (Not DA.GetData(3, bFlag)) Then Return

        'Checking toggle
        If bFlag Then

            'Initializing sapModel
            sapModel = mySapObject.SapModel

            'Iterating through the mesh faces
            For Each face As Rhino.Geometry.MeshFace In meshArea.Faces

                'Getting the number of vertex in every face.
                If face.IsTriangle Then
                    'Dim x(2), y(2), z(2) As Double
                    VtxNumber = 3
                    pt0 = meshArea.Vertices.Item(face.A)
                    pt1 = meshArea.Vertices.Item(face.B)
                    pt2 = meshArea.Vertices.Item(face.C)

                    x(0) = pt0.X
                    y(0) = pt0.Y
                    z(0) = pt0.Z
                    x(1) = pt1.X
                    y(1) = pt1.Y
                    z(1) = pt1.Z
                    x(2) = pt2.X
                    y(2) = pt2.Y
                    z(2) = pt2.Z


                ElseIf face.IsQuad Then
                    'Dim x(3), y(3), z(3) As Double
                    VtxNumber = 4
                    pt0 = meshArea.Vertices.Item(face.A)
                    pt1 = meshArea.Vertices.Item(face.B)
                    pt2 = meshArea.Vertices.Item(face.C)
                    pt2 = meshArea.Vertices.Item(face.D)

                    x(0) = pt0.X
                    y(0) = pt0.Y
                    z(0) = pt0.Z
                    x(1) = pt1.X
                    y(1) = pt1.Y
                    z(1) = pt1.Z
                    x(2) = pt2.X
                    y(2) = pt2.Y
                    z(2) = pt2.Z
                    x(3) = pt3.X
                    y(3) = pt3.Y
                    z(3) = pt3.Z

                End If

                sapModel.AreaObj.AddByCoord(VtxNumber, x, y, z, strName, , strName)

            Next

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