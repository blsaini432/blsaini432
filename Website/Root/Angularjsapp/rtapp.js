var app = angular.module('app', []);
app.controller('pageCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.getData = function () {
        var httpreq = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
                   },
        }
        $http(httpreq).success(function (response)
        {
            $scope.data = response.d;
        })
    };

    $scope.search = function ()
    {
        $scope.criteria = angular.copy($scope.criteria1);
    }


}]);
