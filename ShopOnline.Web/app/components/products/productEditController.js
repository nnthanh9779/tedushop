
(function (app) {
    app.controller('productEditController', productEditController);
    productEditController.$inject = ['$scope', 'apiService', 'notificationService', '$stateParams', '$state', 'commonService'];

    function productEditController($scope, apiService, notificationService, $stateParams, $state, commonService) {
        $scope.product = {
            UpdateDate: new Date()
        }

        $scope.UpdateProduct = UpdateProduct;
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }
        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function UpdateProduct() {
            apiService.put('/api/product/update', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã cập nhật thành công !');
                $state.go('products'); //chuyển hướng sang trang danh sách 
            }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
            });
        }

        function loadProductDetail() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function loadProductCategory() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.productCategories = result.data;
            }, function (error) {
                console.log('Load product category failed');
            })
        }

        //chon anh Image
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Image = fileUrl; //add vào Imgage
            }
            finder.popup();
        }

        loadProductDetail();
        loadProductCategory();
    }

})(angular.module('shoponline.products'));