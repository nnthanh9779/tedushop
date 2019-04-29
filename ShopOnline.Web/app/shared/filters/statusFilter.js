/// <reference path="../../../assets/admin/node_modules/angular/angular.js" />

(function (app) {
    app.filter('statusFilter', function () {
        return function (input) {
            if (input) {
                return "Kích hoạt";
            }
            else return "Khoá";
        };
    });
})(angular.module('shoponline.common'));