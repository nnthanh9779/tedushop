/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function (app) {
    app.service('apiService', apiService);

    apiService.$inject = ['$http'];

    function apiService($http) {
        return {
            get: get
        };

        function get(url, params, success, failure) {
            $http.get(url, params).then(function (result) { //Sử lý sau khi gọi hàm get
                success(result);
            }, function (error) {
                failure(error);
            });
        }
    }
})(angular.module('shoponline.common'));