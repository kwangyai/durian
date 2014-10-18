Imports System
Imports System.Text
Imports System.Collections
Imports System.Data
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

' HashDataCollection (Extended HashTable Class) .................................................................
Public Class DataCollectionX
    Inherits System.Collections.Hashtable
    Private CaseInsensitive As Boolean = False
    Public SeparatorCh As Char = ","c

    Public Sub New()
        CaseInsensitive = True
    End Sub

    Public Sub New(ByVal CaseInsen As Boolean)
        CaseInsensitive = CaseInsen
    End Sub

    Public Overrides Function ToString() As String
        Dim Res As String = "DataCollection {" + vbCrLf
        For Each Key As Object In Me.Keys
            Dim KeyName As String = Key.ToString()
            Dim Value As Object = GetValue(Key, "<undef>")
            If (Value Is Nothing) Then
                Value = "<null>"
            End If
            Res += String.Concat(Key.ToString(), " = ", Value.ToString(), " , ", vbCrLf)
        Next
        Return Res + "}" + vbCrLf
    End Function

    Public Function ToDataCollection() As DataCollection
        Return Me
    End Function


    Public Function AddDataCollection(ByVal Inp As DataCollection)
        For Each Key As Object In Inp.Keys
            SetValue(Key, Inp(Key))
        Next
        Return True
    End Function

    Public Function AddDataTable(ByVal Inp As DataTable, ByVal ColKeyName As String, ByVal ColValName As String) As Integer
        Dim Cnt As Integer = -1
        Try
            Dim ColKeyIndex As Integer = Inp.Columns.IndexOf(ColKeyName)
            Dim ColValIndex As Integer = Inp.Columns.IndexOf(ColValName)
            For Cnt = 0 To Inp.Rows.Count - 1


                Dim CurRow As DataRow = Inp.Rows(Cnt)

                Me.SetValue(CurRow(ColKeyIndex).ToString(), CurRow(ColValIndex))
            Next
        Catch
        End Try
        Return Cnt
    End Function

    Public Function AddParamPair(ByVal ParamArray Inp() As Object) As Integer
        Return AddArrayPair(Inp)
    End Function

    Public Function AddArrayPair(ByVal Inp() As Object) As Integer
        Dim Index As Integer = 0
        Dim Cnt As Integer = 0
        Dim PropName As String
        Dim PropVal As Object

        Try
            While (Index + 2 <= Inp.Length)

                PropName = Inp(Index).ToString()
                Index += 1

                PropVal = Inp(Index)
                Index += 1

                SetValue(PropName, PropVal)
                Cnt += 1
            End While
        Catch
        End Try

        Return Cnt
    End Function




    Public Function GetValue(ByVal Key As String) As Object
        Return GetValue(Key, Nothing)

    End Function

    Public Function GetValue(ByVal Key As String, ByVal Def As Object) As Object
        If CaseInsensitive Then
            Key = Key.ToUpper()
        End If

        If (Me.ContainsKey(Key)) Then
            Return Me(Key)
        End If
        Return Def
    End Function


    Public Function GetSubValueCount(ByVal Key As String) As Integer
        Dim Value As String = GetString(Key)
        Dim Cnt As Integer = 0
        For Index As Integer = 0 To Value.Length - 1
            If (Value(Index) = SeparatorCh) Then
                Cnt = Cnt + 1
            End If
        Next
        Return Cnt
    End Function


    Public Function GetString(ByVal Key As String) As String
        Return GetString(Key, "")
    End Function

    Public Function GetString(ByVal Key As String, ByVal Def As String) As String
        Dim Res As Object = GetValue(Key, Def)
        If ((Res IsNot Nothing)) Then
            If Res.GetType() Is GetType(Boolean) Then
                Return IIf(CBool(Res), "TRUE", "FALSE")
            End If
            Return Res.ToString()
        End If
        Return Def
    End Function

    Public Function GetInteger(ByVal Key As String, ByVal Def As Integer) As Integer
        Try
            Dim Res As Object = GetValue(Key, Def)
            If (TypeOf Res Is Boolean) Then
                Return IIf(CBool(Res), 1, 0)
            ElseIf (TypeOf Res Is String) Then
                Return Integer.Parse(Res.ToString())
            End If
            Return CInt(Res)
        Catch
        End Try
        Return Def
    End Function

    Public Function GetFloat(ByVal Key As String, ByVal Def As Single) As Single
        Try
            Dim Res As Object = GetValue(Key, Def)
            If (TypeOf Res Is Boolean) Then
                Return IIf(CBool(Res), 1, 0)
            ElseIf (TypeOf Res Is String) Then
                Return Single.Parse(Res.ToString())
            End If
            Return CType(Res, Single)
        Catch
        End Try
        Return Def
    End Function

    Public Function GetDouble(ByVal Key As String, ByVal Def As Double) As Double
        Try
            Dim Res As Object = GetValue(Key, Def)
            If (TypeOf Res Is Boolean) Then
                Return IIf(CBool(Res), 1, 0)
            ElseIf (TypeOf Res Is String) Then
                Return Double.Parse(Res.ToString())
            End If
            Return CType(Res, Double)
        Catch
        End Try
        Return Def
    End Function

    Public Function GetDecimal(ByVal Key As String, ByVal Def As Double) As Double
        Try
            Dim Res As Object = GetValue(Key, Def)
            If (TypeOf Res Is Boolean) Then
                Return IIf(CBool(Res), 1, 0)
            ElseIf (TypeOf Res Is String) Then
                Return Decimal.Parse(Res.ToString())
            End If
            Return CType(Res, Decimal)
        Catch
        End Try
        Return Def
    End Function


    Public Function GetBoolean(ByVal Key As String, ByVal Def As Boolean) As Boolean
        Try
            Dim DefStr As String = (IIf(Def, "TRUE", ""))
            Dim ResStr As String = GetString(Key, DefStr).ToUpper().Trim()

            If (ResStr.Length > 0) Then
                If ((ResStr.IndexOf("TRUE") >= 0) Or (ResStr.IndexOf("YES") >= 0) Or (ResStr.IndexOf("ON") >= 0) Or (ResStr.IndexOf("T") >= 0) Or (ResStr.IndexOf("Y") >= 0) Or (ResStr = "1")) Then
                    Return True
                ElseIf ((ResStr.IndexOf("FALSE") >= 0) Or (ResStr.IndexOf("NO") >= 0) Or (ResStr.IndexOf("OFF") >= 0) Or (ResStr.IndexOf("F") >= 0) Or (ResStr.IndexOf("N") >= 0) Or (ResStr = "0")) Then
                    Return False
                ElseIf (Integer.Parse(ResStr) > 0) Then
                    Return True
                End If

            End If
            Return False
        Catch
        End Try
        Return Def
    End Function


    Public Function SetValue(ByVal Key As String, ByVal Value As Object) As Boolean
        Return SetValue(Key, Value, False)
    End Function

    Public Function SetValue(ByVal Key As String, ByVal Value As Object, ByVal SkipExists As Boolean) As Boolean
        If CaseInsensitive Then
            Key = Key.ToUpper()
        End If
        If (Me.ContainsKey(Key)) Then
            If (SkipExists) Then
                Return False
            End If
            Me(Key) = Value
            Return True
        End If
        Me.Add(Key, Value)
        Return True
    End Function

    Public Function SetString(ByVal Key As String, ByVal Val As String) As Boolean
        Return SetValue(Key, Val)
    End Function

    Public Function SetBoolean(ByVal Key As String, ByVal Val As Boolean) As Boolean
        Return SetValue(Key, Val)
    End Function

    Public Function SetInteger(ByVal Key As String, ByVal Val As Integer) As Boolean
        Return SetValue(Key, Val)
    End Function

    Public Function SetFloat(ByVal Key As String, ByVal Val As Single) As Boolean
        Return SetValue(Key, Val)
    End Function

    Public Function SetDouble(ByVal Key As String, ByVal Val As Double) As Boolean
        Return SetValue(Key, Val)
    End Function

    Public Function SetDecimal(ByVal Key As String, ByVal Val As Decimal) As Boolean
        Return SetValue(Key, Val)
    End Function


    Public Function GetFontFromString(ByVal Key As String, ByVal Def As System.Drawing.Font) As System.Drawing.Font
        Dim StrRes As String = GetString(Key, Nothing)
        If ((StrRes IsNot Nothing)) Then
            Dim Face As String = Def.Name
            Dim Size As Single = Def.Size
            Dim Params As String() = StrRes.Split(","c)
            Dim Style As System.Drawing.FontStyle = System.Drawing.FontStyle.Regular

            Face = Params(0).Trim()
            If (Params.Length >= 2) Then

                Try
                    Size = Single.Parse(Params(1).Trim())
                Catch
                End Try


                If (Params.Length >= 3) Then
                    StrRes = Params(2).ToUpper()

                    If (StrRes.IndexOf("BOLD") >= 0) Then
                        Style = System.Drawing.FontStyle.Bold
                    ElseIf (StrRes.IndexOf("ITALIC") >= 0) Then
                        Style = System.Drawing.FontStyle.Italic
                    ElseIf (StrRes.IndexOf("UNDERLINE") >= 0) Then
                        Style = System.Drawing.FontStyle.Underline
                    ElseIf (StrRes.IndexOf("STRIKEOUT") >= 0) Then
                        Style = System.Drawing.FontStyle.Strikeout
                    End If
                End If
            End If
            Return New System.Drawing.Font(Face, Size, Style)
        End If
        Return Def
    End Function

    Public Sub SetValueFromColor(ByVal Key As String, ByVal Src As System.Drawing.Color)

        SetString(Key, Src.R.ToString() + "," + Src.G.ToString() + "," + Src.B.ToString() + "," + Src.A.ToString())
    End Sub

    Public Function GetColorFromString(ByVal Key As String, ByVal Def As System.Drawing.Color) As System.Drawing.Color
        Dim StrRes As String = GetString(Key, "")
        If ((StrRes IsNot Nothing)) Then
            Dim StrChl As String() = StrRes.Split(","c)

            If (StrChl.Length >= 3) Then
                Try
                    If (StrChl.Length >= 4) Then
                        Return System.Drawing.Color.FromArgb(Integer.Parse(StrChl(3)), Integer.Parse(StrChl(0)), Integer.Parse(StrChl(1)), Integer.Parse(StrChl(2)))
                    End If

                    Return System.Drawing.Color.FromArgb(Integer.Parse(StrChl(0)), Integer.Parse(StrChl(1)), Integer.Parse(StrChl(2)))
                Catch
                    Return Def
                End Try
            End If

            Return System.Drawing.ColorTranslator.FromHtml(StrRes)
        End If
        Return Def
    End Function

    Public Shared Function FromHashTable(ByVal Inp As Hashtable) As DataCollection
        Dim Res As New DataCollection
        If (Not Inp Is Nothing) Then
            For Each Key As Object In Inp.Keys
                If (TypeOf (Inp(Key)) Is Hashtable) Then
                    Res.SetValue(Key.ToString(), FromHashTable(Inp(Key)))
                Else
                    Res.SetValue(Key.ToString(), Inp(Key))
                End If
            Next
        End If
        Return Res
    End Function

    Public Shared Function FromParams(ByVal ParamArray Inp() As Object) As DataCollection
        Dim Res As New DataCollection
        Res.AddArrayPair(Inp)
        Return Res
    End Function

    Public Shared Function FromDataTable(ByVal Src As DataTable, ByVal ColKeyName As String, ByVal ColValName As String)
        If ((Not Src Is Nothing) And (Not ColKeyName Is Nothing) And (Not ColValName Is Nothing)) Then
            Dim Res As New DataCollection
            Res.AddDataTable(Src, ColKeyName, ColValName)
            Return Res
        End If
        Return Nothing
    End Function

    Public Shared Function FromDataRow(ByVal Src As DataRow) As DataCollection
        If (Not Src Is Nothing) Then
            Dim Res As New DataCollection
            For Each Col As DataColumn In Src.Table.Columns
                Dim Val As Object = IIf(Src.IsNull(Col), Nothing, Src(Col))
                Res.SetValue(Col.ColumnName, Val)
            Next
            Return Res
        End If
        Return Nothing
    End Function

End Class


Public Class DelimitString

    Public Separator As Char = ","c
    Public Value As String
    Public Values As String()

    Public Sub New()
    End Sub

    Public Sub New(ByVal Inp As String)
        Parse(Inp)
    End Sub

    Public Function Parse() As Integer
        Values = Value.Split(Separator)
        Return Values.Length
    End Function

    Public Function Parse(ByVal Inp As String) As Integer
        Value = Inp
        Return Parse()
    End Function

    Public ReadOnly Property Count() As Integer
        Get
            If (Value Is Nothing) Then
                Return 0
            End If
            Return Values.Length
        End Get
    End Property



    Public Function GetKey(ByVal Index As Integer, ByVal Def As String) As String
        Dim CurVal As String = GetString(Index, Def)
        Dim SubIndex As Integer = CurVal.IndexOf("=")
        If (SubIndex > 0) Then
            Return CurVal.Substring(0, SubIndex)
        End If
        Return Def
    End Function


    Public Function GetString(ByVal Index As Integer, ByVal Def As String) As String
        If ((Values IsNot Nothing)) Then
            If (Index < Values.Length) Then
                Return Values(Index)
            End If
        End If
        Return Def
    End Function


    Public Function GetString(ByVal Key As String, ByVal Def As String) As String
        If ((Values IsNot Nothing)) Then
            For Cnt As Integer = 0 To Values.Length - 1
                Dim CurVal As String = GetString(Cnt, "")
                If (CurVal.Length > 0) Then
                    Dim SubIndex As Integer = CurVal.IndexOf("=")
                    If (SubIndex > 0) Then
                        Dim PropName As String = CurVal.Substring(0, SubIndex)
                        If (PropName = Key) Then
                            Return CurVal.Substring(SubIndex + 1)
                        End If
                    End If
                End If
            Next
        End If
        Return Def
    End Function

    Public Function GetInteger(ByVal Index As Integer, ByVal Def As Integer) As Integer
        Try
            Dim Val As String = GetString(Index, "0")
            If (Val.Length > 0) Then
                Return Integer.Parse(Val)
            End If
        Catch
        End Try
        Return Def
    End Function

End Class

Public Class DataCollection
    Inherits DataCollectionX


End Class