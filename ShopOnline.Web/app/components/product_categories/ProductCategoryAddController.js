(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    //$state : đối tượng để điều hướng
    productCategoryAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state'];

    function productCategoryAddController($scope, apiService, notificationService, $state) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        };

        $scope.AddProductCategory = AddProductCategory;

        function AddProductCategory() {
            apiService.post('/api/productcategory/create', $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được thêm mới.");
                //return to the list page when successful
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError('Thêm không thành công.');
            });
        }
        function loadParentCategory() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.productCategories = result.data;
            }, function () {
                console.log('cannot get list parent');
            });
        }

        loadParentCategory();
    }

})(angular.module('shoponline.product_categories'));