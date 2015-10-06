Imports Grasshopper.Kernel

Public Class GH2SAPInfo
    Inherits GH_AssemblyInfo

    Public Overrides ReadOnly Property Name() As String
        Get
            Return "GH2SAP"
        End Get
    End Property
    Public Overrides ReadOnly Property Icon As System.Drawing.Bitmap
        Get
            Return My.Resources.GH2SAP
        End Get
    End Property
    Public Overrides ReadOnly Property Description As String
        Get
            'Return a short string describing the purpose of this GHA library.
            Return "Connects Gh3d to SAP2000"
        End Get
    End Property
    Public Overrides ReadOnly Property Id As System.Guid
        Get
            Return New System.Guid("f3f21f9a-3d37-4fe1-8634-4399adb8de19")
        End Get
    End Property

    Public Overrides ReadOnly Property AuthorName As String
        Get
            'Return a string identifying you or your company.
            Return "Enrique Vázquez, Angel Linares. FabLab Sevilla"""
        End Get
    End Property
    Public Overrides ReadOnly Property AuthorContact As String
        Get
            'Return a string representing your preferred contact details.
            Return "Enrique Vázquez, Angel Linares. FabLab Sevilla"
        End Get
    End Property
End Class
