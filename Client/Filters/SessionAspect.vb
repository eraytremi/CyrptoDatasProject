Public Class SessionAspect
    Inherits ActionFilterAttribute

    Public Overrides Sub OnActionExecuting(context As ActionExecutingContext)
        Dim session As HttpSessionStateBase = context.HttpContext.Session
        Dim sessionValue As Object = session("ActivePerson")
        Dim sessionJson As String = If(TryCast(sessionValue, String), String.Empty)

        If String.IsNullOrEmpty(sessionJson) Then
            context.Result = New RedirectResult("~/Authentication/LogIn")
        End If
    End Sub
End Class
