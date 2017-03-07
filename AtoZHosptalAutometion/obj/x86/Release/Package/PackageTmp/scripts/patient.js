/// <reference path="angular.min.js" />
var apps = angular.module("demo", [])
.controller("studentDetailsController", function ($http, $routeParams) {
    var mv = this;
    $http({
        url: "/Services/StudentService.asmx/GetStudent",
        params: { id: $routeParams.id },
        method:"get"
    }).then(function (response) {
        mv.student = response.data;
    })
})