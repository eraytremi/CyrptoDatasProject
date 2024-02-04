Imports System.Text.Json
Imports System.Web.Mvc

Namespace Controllers
    Public Class LoginController
        Inherits Controller
        Private ReadOnly _httpApiService As IHttpApiService

        Public Sub New(httpApiService As IHttpApiService)
            _httpApiService = httpApiService
        End Sub

        <HttpGet>
        Function Index() As ActionResult
            Return View()
        End Function

        <HttpPost>
        Async Function Index(dto As LoginVm) As Threading.Tasks.Task(Of ActionResult)
            Dim response = Await _httpApiService.PostDataAsync(Of GetUserItem)($"/User", JsonSerializer.Serialize(dto))

            If response IsNot Nothing Then
                SessionExtension.Session.SetObject("ActivePerson", response)
                Return Json(New With {.IsSuccess = True, .Messages = "Başarılı giriş yapıldı."})
            End If
            Return View()
        End Function


    End Class
End Namespace