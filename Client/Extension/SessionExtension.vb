Imports System.Runtime.CompilerServices
Imports System.Text.Json
Imports Newtonsoft.Json


Public Module SessionExtension

    <Extension()>
    Public Sub SetObject(session As HttpSessionState, key As String, data As Object)
        session(key) = JsonConvert.SerializeObject(data)
    End Sub

    <Extension()>
    Public Function GetObject(Of T)(session As HttpSessionState, key As String) As T
        Dim sessionData As String = TryCast(session(key), String)
        If Not String.IsNullOrEmpty(sessionData) Then
            Return JsonConvert.DeserializeObject(Of T)(sessionData)
        End If
        Return Nothing
    End Function


End Module
