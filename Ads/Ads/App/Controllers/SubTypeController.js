"use strict";

app.controller('typeController', ['$scope', '$log', 'anuncioService', 'notificationService',
    function ($scope, $log, anuncioService, notificationService) {

        $scope.test = function () {
            alert(1);
        };
}]);