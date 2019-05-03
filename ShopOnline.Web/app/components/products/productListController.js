/// <reference path="../../../assets/admin/node_modules/angular/angular.js" />

(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];
    function productListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.products = [];
        $scope.keyword = '';
        $scope.page = 0;
        $scope.getProducts = getProducts;
        $scope.search = search;
        $scope.deleteProduct = deleteProduct;

        $scope.selectAll = selectAll;
        $scope.isAll = false;
        $scope.deleteMultiple = deleteMultiple;

        //Xóa tất cả danh mục đã chọn 
        function deleteMultiple() {
            var listId = [];
            $.each($scope.slected, function (i, item) {
                listId.push(item.ID);
            })
            var config = {
                params: {
                    listItem: JSON.stringify(listId) //chuyển sang json
                }
            }
            apiService.del('/api/product/deletemulti', config, function (result) {
                notificationService.displaySuccess("Xóa thành công " + result.data + " sản phẩm.");
                search();
            }, function (error) {
                notificationService.displayError("Xóa không thành công.");
            });
        }

        //parameter 1: lắng nghe tên của biến, 2: function call back gồm 2 giá trị (mới, cũ)
        $scope.$watch("products", function (news, old) {
            var checked = $filter("filter")(news, { checked: true }); //dùng filter tìm những giá trị mới dược check lưu vào danh sách 'checked'
            if (checked.length) {
                //kiểm tra độ dài checked > 0 nếu có check 
                $scope.slected = checked; //Thêm vào list selected
                $("#btnDelete").removeAttr("disabled");
            } else {
                $("#btnDelete").attr("disabled", "disabled");
            }
        }, true);

        function selectAll() {
            if ($scope.isAll == false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });

                $scope.isAll = true;
            } else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });

                $scope.isAll = false;
            }
        }

        function getProducts(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 2
                }
            }

            apiService.get('/api/product/getall', config, function (result) {
                if (result.data.TotalRow == 0) {
                    notificationService.displayWarning("Không có bản ghi nào được tìm tháy .")
                }
                $scope.products = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function (error) {
                console.log("Load Product Faild.");
            });
        }

        function deleteProduct(id) {
            $ngBootbox.confirm("Bạn có chắc muốn xóa ?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }

                apiService.del('/api/product/delete', config, function (result) {
                    notificationService.displaySuccess('Xóa ' + result.data.Name + ' thành công !');
                    search();
                }, function (error) {
                    notificationService.displayError('Xóa không thành công .');
                    console.log(error.data);
                });
            });

        }
        function search() {
            getProducts();
        }

        $scope.getProducts();
    }
})(angular.module('shoponline.products'));