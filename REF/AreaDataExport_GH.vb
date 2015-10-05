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
  Private Sub RunScript(ByVal LC As List(Of String), ByVal P As Object, ByRef SMaxT As Object, ByRef SMinT As Object, ByRef SMaxB As Object, ByRef SMinB As Object, ByRef ptList As Object, ByRef NumberResults As Object, ByRef LoadCase As Object) 
    Dim ret As Long = Nothing
    'Dim NumberResults As Long = Nothing
    Dim Obj() As String = Nothing
    Dim Elm() As String = Nothing
    Dim PointElm() As String = Nothing
    'Dim LoadCase() As String = Nothing
    Dim StepType() As String = Nothing
    Dim StepNum() As Double = Nothing
    Dim S11Top() As Double = Nothing
    Dim S22Top() As Double = Nothing
    Dim S12Top() As Double = Nothing
    Dim SMaxTop() As Double = Nothing
    Dim SMinTop() As Double = Nothing
    Dim SAngleTop() As Double = Nothing
    Dim SVMTop() As Double = Nothing
    Dim S11Bot() As Double = Nothing
    Dim S22Bot() As Double = Nothing
    Dim S12Bot() As Double = Nothing
    Dim SMaxBot() As Double = Nothing
    Dim SMinBot() As Double = Nothing
    Dim SAngleBot() As Double = Nothing
    Dim SVMBot() As Double = Nothing
    Dim S13Avg() As Double = Nothing
    Dim S23Avg() As Double = Nothing
    Dim SMaxAvg() As Double = Nothing
    Dim SAngleAvg() As Double = Nothing

    Dim ptList0 As New List(Of Point3d)
    Dim pt As Point3d
    Dim xCoord As Double = Nothing
    Dim yCoord As Double = Nothing
    Dim zCoord As Double = Nothing

    If P = True Then
      Dim mySapObject As cOAPI = Nothing
      mySapObject = DirectCast(System.Runtime.InteropServices.Marshal.GetActiveObject("CSI.SAP2000.API.SapObject"), cOAPI)

      Dim mySapModel As cSapModel = mySapObject.SapModel
      
      mySapModel.Results.Setup.DeselectAllCasesAndCombosForOutput
      mySapModel.Results.Setup.SetComboSelectedForOutput("ELUPerEnvol")

      mySapModel.Results.AreaStressShell("", 3, NumberResults, Obj, Elm, PointElm, LoadCase, _
        StepType, StepNum, S11Top, S22Top, S12Top, SMaxTop, SMinTop, SAngleTop, SVMTop, S11Bot, _
        S22Bot, S12Bot, SMaxBot, SMinBot, SAngleBot, SVMBot, S13Avg, S23Avg, SMaxAvg, SAngleAvg)

      SMaxT = SMaxTop
      SMinT = SMinTop
      SMaxB = SMaxBot
      SMinB = SMinBot

      For i As Integer = 0 To PointElm.Count - 1

        mySapModel.PointObj.GetCoordCartesian(PointElm(i), pt.X, pt.Y, pt.Z)
        Print(LoadCase(i))
        'If LoadCase(i) = LC Then

          ptList0.Add(pt)

        'End If

      Next

      ptList = ptList0

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
    Dim LC As List(Of String) = Nothing
    If (inputs(0) IsNot Nothing) Then
      LC = GH_DirtyCaster.CastToList(Of String)(inputs(0))
    End If

    Dim P As System.Object = Nothing
    If (inputs(1) IsNot Nothing) Then
      P = DirectCast(inputs(1), System.Object)
    End If



    '3. Declare output parameters
  Dim SMaxT As System.Object = Nothing
  Dim SMinT As System.Object = Nothing
  Dim SMaxB As System.Object = Nothing
  Dim SMinB As System.Object = Nothing
  Dim ptList As System.Object = Nothing
  Dim NumberResults As System.Object = Nothing
  Dim LoadCase As System.Object = Nothing


    '4. Invoke RunScript
    Call RunScript(LC, P, SMaxT, SMinT, SMaxB, SMinB, ptList, NumberResults, LoadCase)

    Try
      '5. Assign output parameters to component...
      If (SMaxT IsNot Nothing) Then
        If (GH_Format.TreatAsCollection(SMaxT)) Then
          Dim __enum_SMaxT As IEnumerable = DirectCast(SMaxT, IEnumerable)
          DA.SetDataList(1, __enum_SMaxT)
        Else
          If (TypeOf SMaxT Is Grasshopper.Kernel.Data.IGH_DataTree) Then
            'merge tree
            DA.SetDataTree(1, DirectCast(SMaxT, Grasshopper.Kernel.Data.IGH_DataTree))
          Else
            'assign direct
            DA.SetData(1, SMaxT)
          End If
        End If
      Else
        DA.SetData(1, Nothing)
      End If
      If (SMinT IsNot Nothing) Then
        If (GH_Format.TreatAsCollection(SMinT)) Then
          Dim __enum_SMinT As IEnumerable = DirectCast(SMinT, IEnumerable)
          DA.SetDataList(2, __enum_SMinT)
        Else
          If (TypeOf SMinT Is Grasshopper.Kernel.Data.IGH_DataTree) Then
            'merge tree
            DA.SetDataTree(2, DirectCast(SMinT, Grasshopper.Kernel.Data.IGH_DataTree))
          Else
            'assign direct
            DA.SetData(2, SMinT)
          End If
        End If
      Else
        DA.SetData(2, Nothing)
      End If
      If (SMaxB IsNot Nothing) Then
        If (GH_Format.TreatAsCollection(SMaxB)) Then
          Dim __enum_SMaxB As IEnumerable = DirectCast(SMaxB, IEnumerable)
          DA.SetDataList(3, __enum_SMaxB)
        Else
          If (TypeOf SMaxB Is Grasshopper.Kernel.Data.IGH_DataTree) Then
            'merge tree
            DA.SetDataTree(3, DirectCast(SMaxB, Grasshopper.Kernel.Data.IGH_DataTree))
          Else
            'assign direct
            DA.SetData(3, SMaxB)
          End If
        End If
      Else
        DA.SetData(3, Nothing)
      End If
      If (SMinB IsNot Nothing) Then
        If (GH_Format.TreatAsCollection(SMinB)) Then
          Dim __enum_SMinB As IEnumerable = DirectCast(SMinB, IEnumerable)
          DA.SetDataList(4, __enum_SMinB)
        Else
          If (TypeOf SMinB Is Grasshopper.Kernel.Data.IGH_DataTree) Then
            'merge tree
            DA.SetDataTree(4, DirectCast(SMinB, Grasshopper.Kernel.Data.IGH_DataTree))
          Else
            'assign direct
            DA.SetData(4, SMinB)
          End If
        End If
      Else
        DA.SetData(4, Nothing)
      End If
      If (ptList IsNot Nothing) Then
        If (GH_Format.TreatAsCollection(ptList)) Then
          Dim __enum_ptList As IEnumerable = DirectCast(ptList, IEnumerable)
          DA.SetDataList(5, __enum_ptList)
        Else
          If (TypeOf ptList Is Grasshopper.Kernel.Data.IGH_DataTree) Then
            'merge tree
            DA.SetDataTree(5, DirectCast(ptList, Grasshopper.Kernel.Data.IGH_DataTree))
          Else
            'assign direct
            DA.SetData(5, ptList)
          End If
        End If
      Else
        DA.SetData(5, Nothing)
      End If
      If (NumberResults IsNot Nothing) Then
        If (GH_Format.TreatAsCollection(NumberResults)) Then
          Dim __enum_NumberResults As IEnumerable = DirectCast(NumberResults, IEnumerable)
          DA.SetDataList(6, __enum_NumberResults)
        Else
          If (TypeOf NumberResults Is Grasshopper.Kernel.Data.IGH_DataTree) Then
            'merge tree
            DA.SetDataTree(6, DirectCast(NumberResults, Grasshopper.Kernel.Data.IGH_DataTree))
          Else
            'assign direct
            DA.SetData(6, NumberResults)
          End If
        End If
      Else
        DA.SetData(6, Nothing)
      End If
      If (LoadCase IsNot Nothing) Then
        If (GH_Format.TreatAsCollection(LoadCase)) Then
          Dim __enum_LoadCase As IEnumerable = DirectCast(LoadCase, IEnumerable)
          DA.SetDataList(7, __enum_LoadCase)
        Else
          If (TypeOf LoadCase Is Grasshopper.Kernel.Data.IGH_DataTree) Then
            'merge tree
            DA.SetDataTree(7, DirectCast(LoadCase, Grasshopper.Kernel.Data.IGH_DataTree))
          Else
            'assign direct
            DA.SetData(7, LoadCase)
          End If
        End If
      Else
        DA.SetData(7, Nothing)
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