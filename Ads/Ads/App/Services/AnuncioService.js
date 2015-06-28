"use strict";

app.factory('anuncioService', ['$http', '$q', function ($http, $q) {
    var url = "http://" + window.location.host + "/Ads";

    return {
        getModelos: function(marcaId) {
            var defered = $q.defer();
            $http.get(url + '/GetListModeloByMarca/' + marcaId).success(function (data) {
                defered.resolve(data);
            }).error(function(msg, error) {
                defered.reject(msg);
            });
            return defered.promise;
        },

        getArticleTypes: function (categoryId) {
            var defered = $q.defer();
            $http.get(url + '/GetListArticleTypeByCategory/' + categoryId).success(function (data) {
                defered.resolve(data);
            }).error(function(msg, error) {
                defered.reject(msg);
            });
            return defered.promise;
        },

        deleteArticle: function (articleId) {
            var defered = $q.defer();
            $http.get(url + '/deleteArticle/' + articleId).success(function (data) {
                defered.resolve(data);
                angular.element('#modal-popup').modal('hide');
                angular.element('.article-id-' + articleId).remove();
            }).error(function(msg, error) {
                defered.reject(msg);
            });
            return defered.promise;
        }
    };
}]);