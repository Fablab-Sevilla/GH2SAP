Imports System.Collections.Generic

Imports Grasshopper.Kernel
Imports Rhino.Geometry
Imports GH2SAP.GH2SAPIO
Imports SAP2000v17


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

        'General variables
        Dim ptRef As New Point3d
        Dim strRest As String = Nothing
        Dim strName As String = Nothing
        'Dim sapObject As cOAPI
        Dim sapModel As cSapModel
        Dim bRestraint(5), bFlag As Boolean
        Dim intDum As Integer

        'Getting values from inputs
        If (Not DA.GetData(0, ptRef)) Then Return
        If (Not DA.GetData(1, Name)) Then Return
        If (Not DA.GetData(2, strRest)) Then Return
        If (Not DA.GetData(3, bFlag)) Then Return

        If bFlag Then

            sapModel = mySapObject.SapModel

            'Converting the restraint string into a boolean list
            For i As Integer = 0 To 5
                intDum = Convert.ToInt32(strRest(i))
                bRestraint(i) = Convert.ToBoolean(intDum)
            Next

            sapModel.PointObj.AddCartesian(ptRef.X, ptRef.Y, ptRef.Z, Name)
            sapModel.PointObj.SetRestraint(Name, bRestraint)


        End If

    End Sub

    ''' <summary>
    ''' Provides an Icon for every component that will be visible in the User Interface.
    ''' Icons need to be 24x24 pixels.
    ''' </summary>
    Protected Overrides ReadOnly Property Icon() As System.Drawing.Bitmap
        Get
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