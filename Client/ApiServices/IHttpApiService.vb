Imports System.Threading.Tasks

Public Interface IHttpApiService
    Function GetDataAsync(Of T)(endPoint As String, Optional token As String = Nothing) As Task(Of T)

    Function PostDataAsync(Of T)(endPoint As String, jsonData As String, Optional token As String = Nothing) As Task(Of T)

    Function DeleteDataAsync(Of T)(endPoint As String, Optional token As String = Nothing) As Task(Of T)

    Function PutDataAsync(Of T)(endPoint As String, jsonData As String, Optional token As String = Nothing) As Task(Of T)



End Interface
