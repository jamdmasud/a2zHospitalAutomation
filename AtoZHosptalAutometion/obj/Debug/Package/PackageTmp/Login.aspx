<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AtoZHosptalAutometion.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <style>
        body {
            background: url(Content/images/login.jpg) no-repeat center center fixed;
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;
        }

        .panel-default {
            opacity: 0.9;
            margin-top: 30px;
        }

        .form-group.last {
            margin-bottom: 0px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-md-4 col-md-offset-4">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong class="">Login</strong>

                        </div>
                        <div class="panel-body">
                            <div class="form-horizontal" role="form">
                                <div class="form-group">
                                    <label for="usernames" class="col-sm-3 control-label">Email</label>
                                    <div class="col-sm-9">
                                        <input type="email" runat="server" class="form-control" ID="usernames" name="usernames" placeholder="Email" required="" />

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="passwords" class="col-sm-3 control-label">Password</label>
                                    <div class="col-sm-9">
                                        <input type="password" runat="server" class="form-control" name="passwords" id="passwords" placeholder="Password" required="" />
                                    </div>
                                </div>
                                <div class="form-group">
                                </div>
                                <div class="form-group last">
                                    <div class="col-sm-offset-3 col-sm-9">
                                        <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="Sign In" ID="singInButton" OnClick="singInButton_Click"/>
                                        <asp:Button runat="server" CssClass="btn btn-default btn-sm" Text="Reset" ID="resetButton"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <asp:Label ID="failLabel" runat="server" Visible="False"></asp:Label>
                            Not Registered? <a href="/Register.aspx" class="">Register here</a>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <link href="Content/screen.css" rel="stylesheet" />
    <script src="scripts/jquery-3.1.0.js"></script>
    <script src="scripts/jquery.validate.js"></script>
    <script>
        $("#form1").validate({
            rules: {
                usernames: {
                    required: true,
                    minlength: 2
                },
                passwords: {
                    required: true,
                    minlength: 5
                }
            },
            messages: {
                usernames: {
                    required: "Please enter a username",
                    minlength: "Your username must consist of at least 2 characters"
                },
                passwords: {
                    required: "Please provide a password",
                    minlength: "Your password must be at least 5 characters long"
                }
            }
        });
    </script>
</body>
</html>
