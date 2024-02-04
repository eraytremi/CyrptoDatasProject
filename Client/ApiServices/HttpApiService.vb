Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text.Json
Imports System.Threading.Tasks
Imports Microsoft.Extensions.Configuration
Imports Newtonsoft.Json

Public Class HttpApiService
    Implements IHttpApiService
    Private ReadOnly _configuration As IConfiguration
    Private ReadOnly _httpClientFactory As IHttpClientFactory
    Public Sub New(configuration As IConfiguration)
        _configuration = configuration
    End Sub

    Public Async Function GetDataAsync(Of T)(endPoint As String, Optional token As String = Nothing) As Task(Of T) Implements IHttpApiService.GetDataAsync
        Dim client = _httpClientFactory.CreateClient()
        Dim requestMessage As New HttpRequestMessage With {
        .Method = HttpMethod.Get,
        .RequestUri = New Uri($"{_configuration("ServiceUrl:BaseAddress")}{endPoint}")
    }

        ' Header ekleme
        requestMessage.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

        ' Token kontrolü ve ekleme
        If Not String.IsNullOrEmpty(token) Then
            requestMessage.Headers.Authorization = New AuthenticationHeaderValue("Bearer", token)
        End If

        Dim responseMessage = Await client.SendAsync(requestMessage)
        Dim jsonResponse = Await responseMessage.Content.ReadAsStringAsync()
        Dim response = Json.JsonSerializer.Deserialize(Of T)(jsonResponse, New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True})

        Return response
    End Function

    Public Async Function PostDataAsync(Of T)(endPoint As String, jsonData As String, Optional token As String = Nothing) As Task(Of T) Implements IHttpApiService.PostDataAsync
        Dim client = _httpClientFactory.CreateClient()
        Dim requestMessage As New HttpRequestMessage With {
        .Method = HttpMethod.Post,
        .RequestUri = New Uri($"{_configuration("ServiceUrl:BaseAddress")}{endPoint}"),
        .Content = New StringContent(jsonData, Encoding.UTF8, "application/json")
    }

        ' JSON kabul edildiğini belirtmek için Header ekle
        requestMessage.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

        ' Token kontrolü ve ekleme
        If Not String.IsNullOrEmpty(token) Then
            requestMessage.Headers.Authorization = New AuthenticationHeaderValue("Bearer", token)
        End If

        Dim responseMessage = Await client.SendAsync(requestMessage)
        Dim jsonResponse = Await responseMessage.Content.ReadAsStringAsync()
        Dim response = Json.JsonSerializer.Deserialize(Of T)(jsonResponse, New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True})

        Return response
    End Function

    Public Async Function DeleteDataAsync(Of T)(endPoint As String, Optional token As String = Nothing) As Task(Of T) Implements IHttpApiService.DeleteDataAsync
        Dim client = _httpClientFactory.CreateClient()
        Dim requestMessage As New HttpRequestMessage With {
        .Method = HttpMethod.Delete,
        .RequestUri = New Uri($"{_configuration("ServiceUrl:BaseAddress")}{endPoint}")
    }

        ' JSON kabul edildiğini belirtmek için Header ekle
        requestMessage.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

        ' Token kontrolü ve ekleme
        If Not String.IsNullOrEmpty(token) Then
            requestMessage.Headers.Authorization = New AuthenticationHeaderValue("Bearer", token)
        End If

        Dim responseMessage = Await client.SendAsync(requestMessage)
        Dim jsonResponse = Await responseMessage.Content.ReadAsStringAsync()
        Dim response = Json.JsonSerializer.Deserialize(Of T)(jsonResponse, New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True})

        Return response
    End Function

    Public Async Function PutDataAsync(Of T)(endPoint As String, jsonData As String, Optional token As String = Nothing) As Task(Of T) Implements IHttpApiService.PutDataAsync
        Dim client = _httpClientFactory.CreateClient()
        Dim requestMessage As New HttpRequestMessage With {
        .Method = HttpMethod.Put,
        .RequestUri = New Uri($"{_configuration("ServiceUrl:BaseAddress")}{endPoint}"),
        .Content = New StringContent(jsonData, Encoding.UTF8, "application/json")
    }

        ' JSON kabul edildiğini belirtmek için Header ekle
        requestMessage.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

        ' Token kontrolü ve ekleme
        If Not String.IsNullOrEmpty(token) Then
            requestMessage.Headers.Authorization = New AuthenticationHeaderValue("Bearer", token)
        End If

        Dim responseMessage = Await client.SendAsync(requestMessage)
        Dim jsonResponse = Await responseMessage.Content.ReadAsStringAsync()
        Dim response = Json.JsonSerializer.Deserialize(Of T)(jsonResponse, New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True})

        Return response
    End Function
End Class
