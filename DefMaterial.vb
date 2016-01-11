Imports System.Collections.Generic

Imports Grasshopper.Kernel
Imports Rhino.Geometry
Imports GH2SAP.GH2SAPIO
Imports SAP2000v17


Public Class DefMaterial
    Inherits GH_Component
    ''' <summary>
    ''' Initializes a new instance of the DefMaterial class.
    ''' </summary>
    Public Sub New()
        MyBase.New("DefMaterial", "DefMat",
                    "Define a new material",
                    "GH2SAP", "Define")
    End Sub

    ''' <summary>
    ''' Registers all the input parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterInputParams(pManager As GH_Component.GH_InputParamManager)
        pManager.AddTextParameter("Material", "MatName", "Material name", GH_ParamAccess.item, "New_Mat")
        pManager.AddTextParameter("Type", "MatType", "Material type", GH_ParamAccess.item, "MATERIAL_STEEL")
        pManager.AddBooleanParameter("Start", "S", "Boolean flag to start importing", GH_ParamAccess.item, False)
    End Sub

    ''' <summary>
    ''' Registers all the output parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterOutputParams(pManager As GH_Component.GH_OutputParamManager)
    End Sub

    ''' <summary>
    ''' This is the method that actually does the work.
    ''' </summary>
    ''' <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
    Protected Overrides Sub SolveInstance(DA As IGH_DataAccess)
        'dimension variables
        Dim SapModel As cSapModel
        Dim ret As Long
        Dim Name As String

        'initialize new material property
        ret = SapModel.PropMaterial.SetMaterial("Steel", MATERIAL_STEEL)

        'assign isotropic mechanical properties
        ret = SapModel.PropMaterial.SetMPIsotropic("Steel", 29500, 0.25, 0.000006)

        'assign other properties
        ret = SapModel.PropMaterial.SetOSteel_1("Steel", 55, 68, 60, 70, 1, 2, 0.02, 0.1, 0.2, -0.1)
        'SetOSteel_1(Name,Fy,Fu,eFy,eFu,SSType,SSHysType,StrainAtHardening,StrainAtMaxStress,StrainAtRupture,FinalSlope, Optional Temp)

    End Sub

    ''' <summary>
    ''' Provides an Icon for every component that will be visible in the User Interface.
    ''' Icons need to be 24x24 pixels.
    ''' </summary>
    Protected Overrides ReadOnly Property Icon() As System.Drawing.Bitmap
        Get
            'You can add image files to your project resources and access them like this:
            ' return Resources.IconForThisComponent;
            Return Nothing
        End Get
    End Property

    ''' <summary>
    ''' Gets the unique ID for this component. Do not change this ID after release.
    ''' </summary>
    Public Overrides ReadOnly Property ComponentGuid() As Guid
        Get
            Return New Guid("{51342ea1-e213-4ed3-ab15-6121e66f078d}")
        End Get
    End Property
End Class