/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function () {
    angular.module("shoponline.products", ['shoponline.common']).config(config);

    //2 service nằm trong angular-ui-router
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products', {
            url: "/products",
            templateUrl: "/app/components/products/productListView.html",
            controller: "productListController"
        }).state('product_add', {
            url: "/product_add",
            templateUrl: "/app/components/products/productAddView.html",
            controller: "productAddController"
        });
    }
})();//() cho biet module  nay thuộc module nào, nếu k co để trống