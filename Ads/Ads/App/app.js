"use strict";

var app = angular.module('myAds', ['ngRoute']);
toastr.options = {
    preventDuplicates: true,
    positionClass: "toast-bottom-right"
}

//app.config(function ($routeProvider, $httpProvider) {
//    $routeProvider.when('/Ads/CreateModel/?categories=1&articleType=Auto',
//    {
//        templateUrl: 'Views/Auto/Create.cshtml',
//        controller: 'myAdsController'
//    });
//});