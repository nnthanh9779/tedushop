
(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    //khởi tạo tự động các đối tượng service
    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {

        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCategories = getProductCategories;
        $scope.keyword = ''; //keyword searching

        $scope.search = search;
        $scope.deleteProductCategory = deleteProductCategory;

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
            apiService.del('/api/productcategory/deletemulti', config, function (result) {
                notificationService.displaySuccess("Xóa thành công " + result.data + " danh mục sản phẩm.");
                search();
            }, function (error) {
                notificationService.displayError("Xóa không thành công.");
            });
        }

        //chọn và bỏ chọn tất cả check box
        function selectAll() {
            if ($scope.isAll == false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true
                });
                $scope.isAll = true;
            }
            else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false
                });
                $scope.isAll = false;
            }
        }

        //parameter 1: lắng nghe tên của biến, 2: function call back gồm 2 giá trị (mới, cũ)
        $scope.$watch("productCategories", function (news, old) {
            var checked = $filter("filter")(news, { checked: true }); //dùng filter tìm những giá trị mới dược check lưu vào danh sách 'checked'
            if (checked.length) {
                //kiểm tra độ dài checked > 0 nếu có check 
                $scope.slected = checked; //Thêm vào list selected
                $("#btnDelete").removeAttr("disabled");
            } else {
                $("#btnDelete").attr("disabled", "disabled");
            }
        }, true);

        function deleteProductCategory(id) {
            $ngBootbox.confirm("Bạn có chắc muốn xóa ?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/productcategory/delete', config, function (result) {
                    notificationService.displaySuccess("Xóa thành công '" + result.data.Name + " '");
                    search();
                }, function () {
                    notificationService.displayError("Xóa không thành công !");
                })
            });
        }

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
