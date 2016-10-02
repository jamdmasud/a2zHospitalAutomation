<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="AtoZHosptalAutometion.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" />
    <script src="/scripts/jquery-3.1.0.js"></script>
    <script src="/scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script>
        $(function () {
            //$("#datepicker").datepicker();
            $('#dob').datepicker();
        });
    </script>
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
        .control-label{text-align: left !important}
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
                                    <label for="name" class="col-sm-3 control-label">Name</label>
                                    <div class="col-sm-9">
                                        <input type="text" runat="server" class="form-control" id="name" name="name" placeholder="Full Name" required="" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="gender" class="col-sm-3 control-label">Sex</label>
                                    <div class="col-sm-9">
                                        <select runat="server" ID="gender" name="gender" class="form-control">
                                            <option value="">..Select..</option>
                                            <option value="Male">Male</option>
                                            <option value="Female">Female</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="phone" class="col-sm-3 control-label">Phone</label>
                                    <div class="col-sm-9">
                                        <input type="text" runat="server" name="phone" class="form-control" id="phone" placeholder="Phone" required="" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="dob" class="col-sm-3 control-label">Date of Birth</label>
                                    <div class="col-sm-9">
                                        <input type="text" runat="server" class="form-control" id="dob" name="dob" placeholder="Date of birth" required="" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="email" class="col-sm-3 control-label">Email</label>
                                    <div class="col-sm-9">
                                        <input type="email" runat="server" class="form-control" id="email" name="email" placeholder="Email" required="" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="password" class="col-sm-3 control-label">Password</label>
                                    <div class="col-sm-9">
                                        <input type="password" runat="server" class="form-control" id="password" name="password" placeholder="Password" required="" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="confirmPassword" class="col-sm-3 control-label">Password</label>
                                    <div class="col-sm-9">
                                        <input type="password" runat="server" class="form-control" id="confirmPassword" name="confirmPassword" placeholder="Confirm Password" required="" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="rule" class="col-sm-3 control-label">Role</label>
                                    <div class="col-sm-9">
                                         <select runat="server" ID="rule" name="rule" class="form-control">
                                            <option value="">..Select..</option>
                                             <option value="Manager">Manager</option>
                                            <option value="Receiption">Receiption</option>
                                            <option value="Pharmacy">Pharmacy</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                </div>
                                <div class="form-group last">
                                    <div class="col-sm-offset-3 col-sm-9">
                                        <button type="submit" class="btn btn-success btn-sm">Sign in</button>
                                        <button type="reset" class="btn btn-default btn-sm">Reset</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer">
                            Are you member? <a href="/Login.aspx" class="">Sign in</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <link href="Content/screen.css" rel="stylesheet" />
    <script src="scripts/jquery.validate.js"></script>
    <script>
        
        $("#form1").validate({
            rules: {
                name: "required",
                email: {
                    required: true,
                    minlength: 2
                },
                password: {
                    required: true,
                    minlength: 5
                },
                confirmPassword: {
                    required: true,
                    minlength: 5,
                    equalTo: "#password"
                },
                dob: "required",
                gender: "required",
                phone: "required"
            },
            messages: {
                name: "Please enter a valid name",
                username: {
                    required: "Please enter a username",
                    minlength: "Your username must consist of at least 2 characters"
                },
                password: {
                    required: "Please provide a password",
                    minlength: "Your password must be at least 5 characters long"
                },
                confirm_password: {
                    required: "Please provide a password",
                    minlength: "Your password must be at least 5 characters long",
                    equalTo: "Please enter the same password as above"
                },
                gender: "Please Select gender",
                phone: "Please Enter valid phone number"
            }
        });
    </script>
</body>
</html>
