var myApp = angular.module("Admin", ['angularUtils.directives.dirPagination']);
myApp.controller("AdminMedia", function ($scope, $http, $window) {

    $scope.loader = {
        loading: false,
    };
    $scope.Read = function (id) {
    
        ReadRecord(id);
    }
    
    $scope.kyc = function (id) {
       
        Readkyc(id);
    }
    $scope.Referesh = function () {
        loadRecords();
    }
     $scope.Edit = function (id) {
        ReadEdit(id);
     }
     $scope.Add = function () {
         AddMember();
     }
    $scope.Service = function (id) {
        ReadService(id);
    }
    $scope.Cancel = function () {
        $('#exampleModalLongDetail').addClass('modal fade');
        $('#exampleModalLong').addClass('modal fade');
        $('#exampleModalkyc').addClass('modal fade');
    }
    function ReadRecord(id) {
        var ID = id;
        $http({
            url: '/Admin/Admin/_MemberDetail',
            method: "POST",
            data: { msrno: id }
        }).success(function (response) {
            $(".Spinner").hide();
            $('#partialDetail').html(response);
        })
    };
    function Readkyc(id) {
  
        var ID = id;
        $http({
            url: '/Admin/Admin/_Memberkyc',
            method: "POST",
            data: { msrno: id }
        }).success(function (response) {
            $('#partialkyc').html(response);
        })
    };
    function ReadService(id) {
        var ID = id;
        $http({
            url: '/Admin/Admin/_MemberServices',
            method: "POST",
            data: { msrno: id }
        }).success(function (response) {
            $('#partial').html(response);
        })
    };
    function ReadEdit(id) {
        var ID = id;
        $http({
            url: '/Admin/Admin/_MemberAddEdit',
            method: "POST",
            data: { msrno: id }
        }).success(function (response) {
            $scope.data = response.data;
            $('#divData').css({
                'display': ''
            });
            $('#divData').html(response);
            $('#exampleModalLongDetail').addClass('modal fade');
            $('#exampleModalLong').addClass('modal fade');
            $(".Spinner").hide();
            $(document).ready(function () {
                alert(divscroll);
                $('#divscroll').animate({
                    scrollTop: 100000
                }, 1453);
                return false;
            });
        })
    };
    function AddMember() {
        $http({
            url: '/Admin/Admin/_MemberAddEdit',
            method: "POST",
            data: { msrno: 0 }
        }).success(function (response) {
            $scope.data = response.data;
            $('#divData').css({
                'display': ''
            });
            $('#divData').html(response);
            $('#exampleModalLongDetail').addClass('modal fade');
            $('#exampleModalLong').addClass('modal fade');
            $(".Spinner").hide();
            $(document).ready(function () {
                alert(divscroll);
                $('#divscroll').animate({
                    scrollTop: 100000
                }, 1453);
                return false;
            });
        })
    };
    $scope.dataList = [];
    $scope.loader.loading = true;
    loadRecords();
    $scope.isdellist = [];
    function loadRecords() {
      
        $http({
            url: '/Admin/Admin/LoadMemberData',
            method: "POST",
            data: {}
        }).success(function (response) {
            $scope.dataList = response.data;
            $(".Spinner").hide();
            $('#tblData').css({
                  'display': ''
        });
        })
    };

    $scope.search = function () {
        $scope.isdellist = [];
        loadRecords();
    }

    $scope.delete = function (id, url) {
        swal({
            title: "Are you sure?",
            text: "Delete this Product!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel Please!",
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    deleteRecord(id, url);
                } else {
                    swal("Cancelled", "Your Product is safe :)", "error");
                }
            });
    }

    function dropdownFill(id) {
        $http({
            url: '/Admin/Admin/GetSubCategory',
            method: "POST",
            dataType: 'json',
            data: { Category: id },
            success: function (states) {
                $.each(states, function (i, state) {
                    $("#SubCategoryUID").append('<option value="' + state.Value + '">' +
                        state.Text + '</option>');
                });
            },
            error: function (ex) {
                $("#SubCategoryUID").append('<option value="' + "" + '">' + "<< Select >>" + '</option>');
            }
        });
    }
    function deleteRecord(id, url) {
        $http({
            url: '/Seller/Seller/DeleteSellerMediaAngularJs',
            method: "POST",
            data: { UID: id, MediaUrl: url }
        }).success(function (response) {
            $scope.loader.loading = false;
            swal("Deleted!", "Your Product has been Deleted.", "success");
            location.reload();
        })
    };
    $scope.currentPage = 1;
    $scope.pageSize = 20;
    $scope.pageChangeHandler = function (num) {
        console.log('meals page changed to ' + num);
    };
    $(document).ready(function () {
        $scope.pageChangeHandler = function (num) {
            console.log('going to page ' + num);
        };
    });
})
