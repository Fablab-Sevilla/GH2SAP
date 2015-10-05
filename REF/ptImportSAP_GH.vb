Option Strict Off
Option Explicit On

Imports Rhino
Imports Rhino.Geometry
Imports Rhino.DocObjects
Imports Rhino.Collections

Imports GH_IO
Imports GH_IO.Serialization
Imports Grasshopper
Imports Grasshopper.Kernel
Imports Grasshopper.Kernel.Data
Imports Grasshopper.Kernel.Types

Imports System
Imports System.IO
Imports System.Xml
Imports System.Xml.Linq
Imports System.Linq
Imports System.Data
Imports System.Drawing
Imports System.Reflection
Imports System.Collections
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Runtime.InteropServices


Imports SAP2000v17



''' <summary>
''' This class will be instantiated on demand by the Script component.
''' </summary>
Public Class Script_Instance
  Inherits GH_ScriptInstance

  #Region "Utility functions"
  ''' <summary>Print a String to the [Out] Parameter of the Script component.</summary>
  ''' <param name="text">String to print.</param>
  Private Sub Print(ByVal text As String)
    __out.Add(text)
  End Sub
  ''' <summary>Print a formatted String to the [Out] Parameter of the Script component.</summary>
  ''' <param name="format">String format.</param>
  ''' <param name="args">Formatting parameters.</param>
  Private Sub Print(ByVal format As String, ByVal ParamArray args As Object())
    __out.Add(String.Format(format, args))
  End Sub
  ''' <summary>Print useful information about an object instance to the [Out] Parameter of the Script component. </summary>
  ''' <param name="obj">Object instance to parse.</param>
  Private Sub Reflect(ByVal obj As Object)
    __out.Add(GH_ScriptComponentUtilities.ReflectType_VB(obj))
  End Sub
  ''' <summary>Print the signatures of all the overloads of a specific method to the [Out] Parameter of the Script component. </summary>
  ''' <param name="obj">Object instance to parse.</param>
  Private Sub Reflect(ByVal obj As Object, ByVal method_name As String)
    __out.Add(GH_ScriptComponentUtilities.ReflectType_VB(obj, method_name))
  End Sub
#End Region
  
#Region "Members"
  ''' <summary>Gets the current Rhino document.</summary>
  Private RhinoDocument As RhinoDoc
  ''' <summary>Gets the Grasshopper document that owns this script.</summary>
  Private GrasshopperDocument as GH_Document
  ''' <summary>Gets the Grasshopper script component that owns this script.</summary>
  Private Component As IGH_Component
  ''' <summary>
  ''' Gets the current iteration count. The first call to RunScript() is associated with Iteration=0.
  ''' Any subsequent call within the same solution will increment the Iteration count.
  ''' </summary>
  Private Iteration As Integer
#End Region

  ''' <summary>
  ''' This procedure contains the user code. Input parameters are provided as ByVal arguments, 
  ''' Output parameter are ByRef arguments. You don't have to assign output parameters, 
  ''' they will have default values.
  ''' </summary>
  Private Sub RunScript(ByVal ptList As DataTree(Of Point3d), ByVal P As Object, ByRef A As Object) 

    If P = True Then
      Dim mySapObject As cOAPI = Nothing
      mySapObject = DirectCast(System.Runtime.InteropServices.Marshal.GetActiveObject("CSI.SAP2000.API.SapObject"), cOAPI)

      Dim mySapModel As cSapModel = mySapObject.SapModel
      Dim ret As Integer = 0
      Dim diaName As String = Nothing
      Dim bodyName As String = Nothing
      Dim ptName As String = Nothing
      Dim test As Long = Nothing
      Dim bodyConstrain() As Boolean = {True,True,True,True,True,True}

      For i As Integer = 0 To ptList.BranchCount - 1

        'diaName = "DIA_" + convert.ToString(i)
        'mySapModel.ConstraintDef.SetDiaphragm(diaName, 4)
        bodyName = "BOD_" + convert.ToString(i)
        mySapModel.ConstraintDef.SetBody(bodyName, bodyConstrain)

        For j As Integer = 0 To ptList.Branch(i).Count - 1

          ptName = "Point_" + convert.ToString(i) + "_" + convert.ToString(j)

          mySapModel.PointObj.AddCartesian(ptList.Branch(i).item(j).X, ptList.Branch(i).item(j).Y, ptList.Branch(i).item(j).Z, ptName, ptName)
          test = mySapModel.PointObj.SetConstraint(ptName, bodyName, 0, True)
          'print(ptName + " -> " + diaName)
          'print(test)

        Next
      Next
    End If

  End Sub 

  '<Custom additional code> 

  '</Custom additional code> 

  Private __err As New List(Of String)
  Private __out As New List(Of String)
  Private doc As RhinoDoc = RhinoDoc.ActiveDoc            'Legacy field.
  Private owner As Grasshopper.Kernel.IGH_ActiveObject    'Legacy field.
  Private runCount As Int32                               'Legacy field.
  
  Public Overrides Sub InvokeRunScript(ByVal owner As IGH_Component, _
                                       ByVal rhinoDocument As Object, _
                                       ByVal iteration As Int32, _
                                       ByVal inputs As List(Of Object), _
                                       ByVal DA As IGH_DataAccess) 
    'Prepare for a new run...
    '1. Reset lists
    Me.__out.Clear()
    Me.__err.Clear()

    'Current field assignments.
    Me.Component = owner
    Me.Iteration = iteration
    Me.GrasshopperDocument = owner.OnPingDocument()
    Me.RhinoDocument = TryCast(rhinoDocument, Rhino.RhinoDoc)

    'Legacy field assignments
    Me.owner = Me.Component
    Me.runCount = Me.Iteration
    Me.doc = Me.RhinoDocument

    '2. Assign input parameters
    Dim ptList As DataTree(Of Point3d) = Nothing
    If (inputs(0) IsNot Nothing) Then
      ptList = GH_DirtyCaster.CastToTree(Of Point3d)(inputs(0))
    End If

    Dim P As System.Object = Nothing
    If (inputs(1) IsNot Nothing) Then
      P = DirectCast(inputs(1), System.Object)
    End If



    '3. Declare output parameters
  Dim A As System.Object = Nothing


    '4. Invoke RunScript
    Call RunScript(ptList, P, A)

    Try
      '5. Assign output parameters to component...
      If (A IsNot Nothing) Then
        If (GH_Format.TreatAsCollection(A)) Then
          Dim __enum_A As IEnumerable = DirectCast(A, IEnumerable)
          DA.SetDataList(1, __enum_A)
        Else
          If (TypeOf A Is Grasshopper.Kernel.Data.IGH_DataTree) Then
            'merge tree
            DA.SetDataTree(1, DirectCast(A, Grasshopper.Kernel.Data.IGH_DataTree))
          Else
            'assign direct
            DA.SetData(1, A)
          End If
        End If
      Else
        DA.SetData(1, Nothing)
      End If

    Catch ex As Exception
      __err.Add(String.Format("Script exception: {0}", ex.Message))
    Finally
      'Add errors and messages...
      If (owner.Params.Output.Count > 0) Then
        If (TypeOf owner.Params.Output(0) Is Grasshopper.Kernel.Parameters.Param_String) Then
          Dim __errors_plus_messages As New List(Of String)
          If (Me.__err IsNot Nothing) Then __errors_plus_messages.AddRange(Me.__err)
          If (Me.__out IsNot Nothing) Then __errors_plus_messages.AddRange(Me.__out)
          If (__errors_plus_messages.Count > 0) Then
            DA.SetDataList(0, __errors_plus_messages)
          End If
        End If
      End If
    End Try
  End Sub 
End Class