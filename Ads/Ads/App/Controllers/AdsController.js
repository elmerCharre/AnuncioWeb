"use strict";

app.controller('myAdsController', ['$scope', '$log', 'anuncioService', 'notificationService',
    function ($scope, $log, anuncioService, notificationService) {
        $scope.marca = 0;
        $scope.category = 1;
        $scope.article_id = 0;
        $scope.modelos = [];
        $scope.articleTypes = [];

        $scope.getModelosByMarca = function () {
            getModelos($scope.marca);
        };

        $scope.getArticleTypesByCategory = function () {
            getArticleTypes($scope.category);
        };

        $scope.ModalDelete = function (id) {
            $scope.article_id = id;
            angular.element('#modal-popup').modal('show');
        };

        $scope.deleteArticle = function () {
            anuncioService.deleteArticle($scope.article_id);
        };

        setTimeout(function () {
            $scope.getArticleTypesByCategory();
        }, 0);

        var getModelos = function (marcaId) {
            anuncioService.getModelos(marcaId).then(function (data) {
                $scope.modelos = data;
                if ($scope.modelos.length > 0)
                    notificationService.success("La marca tiene modelos");
                else
                    notificationService.info("La marca no tiene modelos");
            }, function (errorMsg) {
                notificationService.error(errorMsg);
            });
        };

        $scope.getModelosByMarcaforJquery = function () {
            anuncioService.getModelos($scope.marca).then(function (data) {
                $scope.modelos = data;
                var html = '';
                $.each($scope.modelos, function (index, value) {
                    html += '<option value="' + value.Id + '">' + value.Name + '</option>';
                });
                angular.element('#modelo').html(html);
                notificationService.success("La marca tiene modelos");
            }, function (errorMsg) {
                notificationService.error(errorMsg);
            });
        };

        var getArticleTypes = function (categoryId) {
            anuncioService.getArticleTypes(categoryId).then(function (data) {
                $scope.articleTypes = data;
                if ($scope.articleTypes.length > 0)
                    notificationService.success("La categoría tiene tipos");
                else
                    notificationService.info("La categoría no tiene tipos");
            }, function (errorMsg) {
                notificationService.error(errorMsg);
            });
        };

        $scope.submitContact = function () {
            alert(1);
        };
}]);