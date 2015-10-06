Imports System.Collections.Generic

Imports Grasshopper.Kernel
Imports Rhino.Geometry


Public Class PtImport
    Inherits GH_Component
    ''' <summary>
    ''' Initializes a new instance of the PtImport class.
    ''' </summary>
    Public Sub New()
        MyBase.New("PtImport", "PtImp", _
                "Import points into SAP2000", _
                "GH2SAP", "Elements")
    End Sub

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

    ''' <summary>
    ''' This is the method that actually does the work.
    ''' </summary>
    ''' <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
    Protected Overrides Sub SolveInstance(DA As IGH_DataAccess)
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
            Return New Guid("{5148c815-9144-46eb-8140-67845d6183cf}")
        End Get
    End Property
End Class