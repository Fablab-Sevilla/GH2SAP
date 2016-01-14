Imports System.Collections.Generic

Imports Grasshopper.Kernel
Imports Rhino.Geometry
Imports SAP2000v17


Public Class GH2SAPIO
    Inherits GH_Component
    ''' <summary>
    ''' Initializes a new instance of the GH2SAPIO class.
    ''' </summary>
    ''' 
    Public Shared mySapObject As cOAPI

    Public Sub New()
        MyBase.New("IO", "IO", _
                    "Starts or link to an existing SAP2000 session", _
                    "GH2SAP", "Tools")
    End Sub

    ''' <summary>
    ''' Registers all the input parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterInputParams(pManager As GH_Component.GH_InputParamManager)


        Dim dir As String
        dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        dir = System.IO.Path.Combine(dir, "GH2SAP\DocumentName.sdb")

        pManager.AddBooleanParameter("IO", "IO", "If True, starts SAP2000 and/or links with it", GH_ParamAccess.item, False)
        pManager.AddIntegerParameter("Mode", "M", "Selects the link mode: 0 -> Static, 1 -> Dynamic...", GH_ParamAccess.item, 0)
        pManager.AddBooleanParameter("Link", "L", "If true the GH model links to an existing SAP2000 session, otherwise it starts a new or existing file", GH_ParamAccess.item, True)
        pManager.AddTextParameter("Path", "P", "Establish the filepath to an existing SAP2000 file or to a new one", GH_ParamAccess.item, dir)

    End Sub

    ''' <summary>
    ''' Registers all the output parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterOutputParams(pManager As GH_Component.GH_OutputParamManager)

        pManager.AddTextParameter("Message", "M", "Output message", GH_ParamAccess.item)
        pManager.AddBooleanParameter("Flag", "F", "Flag that indicates when the component has finished", GH_ParamAccess.item)

    End Sub

    ''' <summary>
    ''' This is the method that actually does the work.
    ''' </summary>
    ''' <param name="DA">The DA object can be used to retrieve data from input parameters and 
    ''' to store data in output parameters.</param>
    Protected Overrides Sub SolveInstance(DA As IGH_DataAccess)

        'Declaring general variables

        Dim bIO, bLink, bFlag As Boolean
        Dim intMode As Integer
        Dim strPath As String = Nothing
        Dim strMessage As String = Nothing

        'Passing values from component inputs to variables
        If (Not DA.GetData(0, bIO)) Then Return
        If (Not DA.GetData(1, intMode)) Then Return
        If (Not DA.GetData(2, bLink)) Then Return
        If (Not DA.GetData(3, strPath)) Then Return

        bFlag = False
        strMessage = "Waiting..."

        If bLink And bIO Then

            Try
                mySapObject = DirectCast(System.Runtime.InteropServices.Marshal.GetActiveObject("CSI.SAP2000.API.SapObject"), cOAPI)
                strMessage = "Connected!"
                bFlag = True

            Catch ex As Exception

                strMessage = "No running instance of the program found or failed to link"
                bFlag = False

            End Try

        Else

            strMessage = "File mode not implemented yet"
            bFlag = False

        End If

        DA.SetData(0, strMessage)
        DA.SetData(1, bFlag)


    End Sub

    ''' <summary>
    ''' Provides an Icon for every component that will be visible in the User Interface.
    ''' Icons need to be 24x24 pixels.
    ''' </summary>
    Protected Overrides ReadOnly Property Icon() As System.Drawing.Bitmap
        Get
            'You can add image files to your project resources and access them like this:
            ' return Resources.IconForThisComponent;
            Return My.Resources.CSiIO

        End Get
    End Property

    ''' <summary>
    ''' Gets the unique ID for this component. Do not change this ID after release.
    ''' </summary>
    Public Overrides ReadOnly Property ComponentGuid() As Guid
        Get
            Return New Guid("{8cd5633d-6ec0-490b-9afc-ea2164e16751}")
        End Get
    End Property
End Class