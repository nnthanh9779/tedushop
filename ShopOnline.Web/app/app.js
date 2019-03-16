/// <reference path="../assets/admin/libs/angular/angular.js" />

(function () {

    angular.module("shoponline", [
        'shoponline.product_categories',
        'shoponline.products',
        'shoponline.common'
    ]).config(config);

    //2 service nằm trong angular-ui-router
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('home', {
            url: "/admin",
            templateUrl: "/app/components/home/homeView.html",
            controller: "homeController"
        });
        //Nếu không phải các trường hợp ở trên sẽ trả về admin
        $urlRouterProvider.otherwise('/admin');
    }
})();