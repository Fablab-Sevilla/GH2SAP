Imports System.Collections.Generic

Imports Grasshopper.Kernel
Imports Rhino.Geometry
Imports SAP2000v16
Imports GH2SAP.Sap2000Util


Public Class GH2SAPIO


    Inherits GH_Component
    ''' <summary>
    ''' Each implementation of GH_Component must provide a public 
    ''' constructor without any arguments.
    ''' Category represents the Tab in which the component will appear, 
    ''' Subcategory the panel. If you use non-existing tab or panel names, 
    ''' new tabs/panels will automatically be created.
    ''' </summary>

    Public Shared obj As SapObject

    Public Sub New()
        MyBase.New("GH2SAP_IO", "IO", _
                "Starts a SAP2000 instance linked to GH", _
                "GH2SAP", "System")
    End Sub

    ''' <summary>
    ''' Registers all the input parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterInputParams(pManager As GH_Component.GH_InputParamManager)
        pManager.AddBooleanParameter("Run SAP", "IO", "Run a clean instance of SAP2000", GH_ParamAccess.item)
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
    ''' <param name="DA">The DA object can be used to retrieve data from input parameters and 
    ''' to store data in output parameters.</param>
    Protected Overrides Sub SolveInstance(DA As IGH_DataAccess)
        Dim util As New Sap2000Util
        Dim IO, Pass As Boolean
        Pass = False

        If (Not DA.GetData(0, IO)) Then Return

        If (IO) Then

            obj = util.StartSap2000()
            Pass = True

        ElseIf (IO = False) Then

            util.StopSap2000()
            Pass = False

        End If

        DA.SetData(0, Pass)

    End Sub

    ''' <summary>
    ''' Provides an Icon for every component that will be visible in the User Interface.
    ''' Icons need to be 24x24 pixels.
    ''' </summary>
    Protected Overrides ReadOnly Property Icon() As System.Drawing.Bitmap
        Get
            'You can add image files to your project resources and access them like this:
            ' return Resources.IconForThisComponent;
            Return My.Resources.CSi_IO

            Return Nothing
        End Get
    End Property

    ''' <summary>
    ''' Each component must have a unique Guid to identify it. 
    ''' It is vital this Guid doesn't change otherwise old ghx files 
    ''' that use the old ID will partially fail during loading.
    ''' </summary>
    Public Overrides ReadOnly Property ComponentGuid() As Guid
        Get
            Return New Guid("{d5bd431e-010d-443c-a7d5-6f0ce528745c}")
        End Get
    End Property
End Class