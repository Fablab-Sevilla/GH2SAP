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
        pManager.AddTextParameter("Type", "MatType", "Material type", GH_ParamAccess.item, "STEEL")
        pManager.AddTextParameter("SymType", "MatSymType", "Material directional symmetry type", GH_ParamAccess.item, "ISOTROPIC")
        pManager.AddTextParameter("Color", "MatColor", "Material color", GH_ParamAccess.item, "RED")
        pManager.AddTextParameter("Notes", "MatNotes", "Material notes", GH_ParamAccess.item, "Europe EN 1993-1-1 per EN 10025-2 S275")
        pManager.AddTextParameter("Weight", "MatWight", "Material Wight", GH_ParamAccess.item, "76,9729")
        pManager.AddTextParameter("E1", "MatE1", "Material Modulus of Elasticity", GH_ParamAccess.item, "2,100E+08")
        pManager.AddTextParameter("U12", "MatU1", "Material Poisson", GH_ParamAccess.item, "0,3")
        pManager.AddTextParameter("A1", "MatA1", "Material coefficient of thermal expansion", GH_ParamAccess.item, "1,170E-05")
        pManager.AddTextParameter("Fy", "MatFy", "Material minimum yield stress", GH_ParamAccess.item, "275000")
        pManager.AddTextParameter("Fu", "MatFu", "Material minimum tensile stress", GH_ParamAccess.item, "430000")
        pManager.AddTextParameter("Fc", "MatFc", "Material specified concrete compressive strength", GH_ParamAccess.item, "25000")
        pManager.AddBooleanParameter("Start", "S", "Boolean flag to start importing", GH_ParamAccess.item, True)

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
        Dim sapModel As cSapModel
        Dim strMatName As String = Nothing
        Dim strMatType As String = Nothing
        Dim MatType As eMatType
        Dim strMatSymType As String = Nothing
        Dim strMatColor As String = Nothing
        Dim strMatNotes As String = Nothing
        Dim strMatWight As String = Nothing
        Dim strMatE1 As String = Nothing
        Dim strMatU1 As String = Nothing
        Dim strMatA1 As String = Nothing
        Dim strMatFy As String = Nothing
        Dim strMatFu As String = Nothing
        Dim strMatFc As String = Nothing
        Dim bFlag As Boolean = True
        Dim ret As Integer

        If (Not DA.GetData(0, strMatName)) Then Return
        If (Not DA.GetData(1, strMatType)) Then Return
        If (Not DA.GetData(2, strMatSymType)) Then Return
        If (Not DA.GetData(3, strMatColor)) Then Return
        If (Not DA.GetData(4, strMatNotes)) Then Return
        If (Not DA.GetData(5, strMatWight)) Then Return
        If (Not DA.GetData(6, strMatE1)) Then Return
        If (Not DA.GetData(7, strMatU1)) Then Return
        If (Not DA.GetData(8, strMatA1)) Then Return
        If (Not DA.GetData(9, strMatFy)) Then Return
        If (Not DA.GetData(10, strMatFu)) Then Return
        If (Not DA.GetData(11, strMatFc)) Then Return
        If (Not DA.GetData(12, bFlag)) Then Return


        Select Case strMatType
            Case "STEEL"
                MatType = eMatType.Steel
            Case "CONCRETE"
                MatType = eMatType.Concrete
            Case "NODESIGN"
                MatType = eMatType.NoDesign
            Case "ALUMINUM"
                MatType = eMatType.Aluminum
            Case "COLDFORMED"
                MatType = eMatType.ColdFormed
            Case "REBAR"
                MatType = eMatType.Rebar
            Case "TENDON"
                MatType = eMatType.Tendon
            Case "MASONRY"
                MatType = eMatType.Masonry
        End Select


        If bFlag Then

            sapModel = mySapObject.SapModel

            'initialize new material property
            sapModel.PropMaterial.SetMaterial(strMatName, MatType)

            'assign isotropic mechanical properties
            sapModel.PropMaterial.SetMPIsotropic(strMatName, 29500, 0.25, 0.000006)

            'assign other properties
            'sapModel.PropMaterial.SetOSteel_1(strMatName, 55, 68, 60, 70, 1, 2, 0.02, 0.1, 0.2, -0.1)
            'SetOSteel_1(Name,Fy,Fu,eFy,eFu,SSType,SSHysType,StrainAtHardening,StrainAtMaxStress,StrainAtRupture,FinalSlope, Optional Temp)

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