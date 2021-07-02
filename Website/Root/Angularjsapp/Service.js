app.service('empservice', function ($http) {

    //get employee
    this.GetAllEmployees=function()
    {
        var httpreq = {
            method: 'POST',
            url: '../Administrator/View.aspx/BindCustomers',
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'dataType': 'json'
            },
            data: {}
        }
    }
    //add new employee
    this.Save=function(Employee)
    {
        var request = $http
        ({
            method: 'post',
            url: '/Home/Insert',
            data: Employee
        });
        return request;
    }


    //update new employee
    this.Update = function (Employee) {
        var request = $http
        ({
            method: 'post',
            url: '/Home/Update',
            data: Employee
        });
        return request;
    }
});