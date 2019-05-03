
(function (app) {
    app.controller('productAddController', productAddController);
    //$state : đối tượng để điều hướng
    productAddController.$inject = ['$scope', 'notificationService', 'apiService', '$state', 'commonService'];

    function productAddController($scope, notificationService, apiService, $state, commonService) {
        $scope.product = {
            CreatedDate: new Date(),
            ViewCount: 0,
            Status: true
        };

        $scope.AddProduct = AddProduct;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        function AddProduct() {
            apiService.post('/api/product/create', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được thêm thành công !");
                $state.go("products");
            }, function (error) {
                notificationService.displayError("Thêm không thành công .");
            })
        }

        function loadProductCategory() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.productCategories = result.data;
            }, function (error) {
                    console.log("load product category failed");
            });
        }


        //chon anh Image
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Image = fileUrl; //add vào Imgage
            }
            finder.popup();
        }

        loadProductCategory();
    }
})(angular.module('shoponline.products'));