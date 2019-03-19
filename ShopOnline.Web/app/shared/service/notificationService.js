(function (app) {
    app.factory('notificationService', notificationService);

    function notificationService() {
        toastr.options = {
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "fadeIn": 300, //show 0.3s
            "fadeOut": 1000, //hide 1s
            "timeOut": 3000, //show time 3s
            "extendedTimeOut": 1000
        };

        return {
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayWarning: displayWarning,
            displayInfo: displayInfo
        };

        function displaySuccess(message) {
            toastr.success(message);
        }
        function displayWarning(message) {
            toastr.warning(message);
        }
        function displayError(error) {
            if (Array.isArray(error)) {
                error.forEach(function (err) {
                    toastr.error(err);
                });
            }
            toastr.error(error);
        }
        function displayInfo(message) {
            toastr.info(message);
        }
    }
})(angular.module('shoponline.common'));