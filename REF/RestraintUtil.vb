Imports System.Collections.Generic

Imports Grasshopper.Kernel
Imports Rhino.Geometry


Public Class RestraintUtil
    Inherits GH_Component
    ''' <summary>
    ''' Initializes a new instance of the MyComponent1 class.
    ''' </summary>
    Public Sub New()
        MyBase.New("RestraintUtil", "RUtil", _
                    "Generates a code that match point restraint in SAP2000", _
                    "GH2SAP", "System")
    End Sub

    ''' <summary>
    ''' Registers all the input parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterInputParams(pManager As GH_Component.GH_InputParamManager)
        pManager.AddBooleanParameter("U1", "U1", "Displacement1 restraint", GH_ParamAccess.item, False)
        pManager.AddBooleanParameter("U2", "U1", "Displacement2 restraint", GH_ParamAccess.item, False)
        pManager.AddBooleanParameter("U3", "U1", "Displacement3 restraint", GH_ParamAccess.item, False)
        pManager.AddBooleanParameter("R1", "U1", "Rotation1 restraint", GH_ParamAccess.item, False)
        pManager.AddBooleanParameter("R2", "U1", "Rotation2 restraint", GH_ParamAccess.item, False)
        pManager.AddBooleanParameter("R3", "U1", "Rotation3 restraint", GH_ParamAccess.item, False)

    End Sub

    ''' <summary>
    ''' Registers all the output parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterOutputParams(pManager As GH_Component.GH_OutputParamManager)
        pManager.Register_StringParam("Restraint", "R", "String containing restraint information", GH_ParamAccess.item)
    End Sub

    ''' <summary>
    ''' This is the method that actually does the work.
    ''' </summary>
    ''' <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
    Protected Overrides Sub SolveInstance(DA As IGH_DataAccess)
        Dim resultString As String
        Dim B0, B1, B2, B3, B4, B5 As New Boolean

        'Assigning data to variables and checking availability.
        If (Not DA.GetData(0, B0)) Then Return
        If (Not DA.GetData(1, B1)) Then Return
        If (Not DA.GetData(2, B2)) Then Return
        If (Not DA.GetData(3, B3)) Then Return
        If (Not DA.GetData(4, B4)) Then Return
        If (Not DA.GetData(5, B5)) Then Return

        resultString = Convert.ToInt32(B0) & Convert.ToInt32(B1) & Convert.ToInt32(B2) & Convert.ToInt32(B3) & _
        Convert.ToInt32(B4) & Convert.ToInt32(B5)

        DA.SetData(0, resultString)

    End Sub

    ''' <summary>
    ''' Provides an Icon for every component that will be visible in the User Interface.
    ''' Icons need to be 24x24 pixels.
    ''' </summary>
    Protected Overrides ReadOnly Property Icon() As System.Drawing.Bitmap
        Get
            'You can add image files to your project resources and access them like this:
            ' return Resources.IconForThisComponent;
            Return My.Resources.RestraintUtil
        End Get
    End Property

    ''' <summary>
    ''' Gets the unique ID for this component. Do not change this ID after release.
    ''' </summary>
    Public Overrides ReadOnly Property ComponentGuid() As Guid
        Get
            Return New Guid("{9af835b3-a22c-47d3-87f9-eff9ab03fb61}")
        End Get
    End Property
End Class