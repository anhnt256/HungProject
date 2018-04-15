(function () {
    "use strict";

    angular
        .module("MyApp")
        .controller("UploadCtrl", UploadCtrl);

    UploadCtrl.$inject = ['$http', '$scope', '$element', 'Helper', 'DlgLogin'];

    function UploadCtrl($http, $scope, $element, Helper, DlgLogin) {
    }
})();