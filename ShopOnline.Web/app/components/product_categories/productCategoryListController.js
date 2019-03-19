(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    //khởi tạo tự động các đối tượng service
    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService'];

    function productCategoryListController($scope, apiService, notificationService) {

        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCategories = getProductCategories;
        $scope.keyword = ''; //keyword searching

        $scope.search = search;

        function search() {
            getProductCategories();
        }
        function getProductCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 2
                }
            };
            apiService.get('/api/productcategory/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning("không có bảng ghi nào được tìm thấy.");
                }
                else {
                    notificationService.displaySuccess("Đã tìm thấy " + result.data.TotalCount + " bản ghi.");
                }
                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log("Load productcategory failed.");
            });
        }

        $scope.getProductCategories();
    }
})(angular.module("shoponline.product_categories"));
