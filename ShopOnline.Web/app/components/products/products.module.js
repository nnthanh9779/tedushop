/// <reference path="../../../assets/admin/node_modules/angular/angular.js" />

(function () {
    angular.module("shoponline.products", ['shoponline.common']).config(config);

    //2 service nằm trong angular-ui-router
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products', {
            url: "/products",
            templateUrl: "/app/components/products/productListView.html",
            controller: "productListController"
        }).state('add_product', {
            url: "/add_product",
            templateUrl: "/app/components/products/productAddView.html",
            controller: "productAddController"
        }).state('edit_product', {
            url: "/edit_product/:id",
            templateUrl: "/app/components/products/productEditView.html",
            controller: "productEditController"
        });
    }
})();//() cho biet module  nay thuộc module nào, nếu k co để trống