/// <reference path="../../../assets/admin/node_modules/angular/angular.js" />

(function (app) {
    app.service('apiService', apiService);

    apiService.$inject = ['$http', 'notificationService'];

    function apiService($http, notificationService) {
        return {
            get: get,
            post: post,
            put: put,
            del: del,
        };

        function get(url, params, success, failure) {
            $http.get(url, params).then(function (result) { //Sử lý sau khi gọi hàm get
                success(result);
            }, function (error) {
                failure(error);
            });
        }

        function post(url, data, success, failure) {
            $http.post(url, data).then(function (result) {
                success(result);
            }, function (error) {
                if (error.status === 401){
                    notificationService.displayError('Authenticate is required.'); //error not login
                }
                else if (failure != null) {
                    failure(error);
                }
            });
        }

        function put(url, data, success, failure) {
            $http.put(url, data).then(function (result) {
                success(result);
            }, function (error) {
                if (error.status === 401) {
                    notificationService.displayError('Authenticate is required.'); //error not login
                }
                else if (failure != null) {
                    failure(error);
                }
            });
        }

        function del(url, data, success, failure) {
            $http.delete(url, data).then(function (result) {
                success(result);
            }, function (error) {
                if (error.status === 401) {
                    notificationService.displayError('Authenticate is required.'); //error not login
                }
                else if (failure != null) {
                    failure(error);
                }
            });
        }
    }
})(angular.module('shoponline.common'));