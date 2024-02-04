﻿@Code
    Layout = Nothing
End Code

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login Page</title>
    <link rel="stylesheet" href="style.css">
    <link href="~/css/login.css" rel="stylesheet" />
    <link href="~/Content/login/login.css" rel="stylesheet" />
</head>
<body>
    <div class="login-container">
        <form action="/login" method="post">
            <h2>Login</h2>
            <input type="text" placeholder="Username" name="username" required>
            <input type="password" placeholder="Password" name="password" required>
            <button type="submit">Login</button>
        </form>
    </div>
</body>
</html>


