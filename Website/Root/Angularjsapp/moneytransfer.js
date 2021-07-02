var app = angular.module("moneyapp", ['angularUtils.directives.dirPagination']);
app.controller("mydmrs", function ($scope, $http) {
    $scope.FillLogin = function ()
    {
        alert($scope.inputValue);
        var httpreq =
            {
            method: 'POST',
            url: '../Retailer/dmr.aspx/RemitLogin',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: { mobile: $scope.inputValue}
        }
        $http(httpreq).success(function (response)
        {
            $scope.Employees = response.d;
        })
    };
});