app.controller('empcontroller', function ($scope, empservice) {
    //use this method to load data
    loademployees();
    function loademployees() {
        var emprecords = empservice.GetAllEmployees();
        emprecords.then(function (d) {
            $scope.Employees = d.data;
        },
        function () {
            alert("sorry");
        });
    }
    //end load method

    //save method 

    $scope.Save = function () {
        var Employee =
          {
              EmpName: $scope.EmpName,
              Salary: $scope.Salary
          };
        var saverecords = empservice.Save(Employee);
        saverecords.then(function (d)
        {
            if (d.data.success == true)
            {
                loademployees();
                alert("Employee Added Success");
                $scope.resetsave();
            }
            else
            {
                alert("Employee not added");
            }
        },
        function()
        {
            alert("Some Error Occured");
        });
    }
    //reset control after save function 
    $scope.resetsave = function () {
        $scope.EmpName = '';
        $scope.Salary = '';
    }
    //save method end


    //update method
    $scope.Update = function () {
        var Employee =
          {
              EmpName: $scope.EmpName,
              Salary: $scope.Salary
          };
        var updaterecords = empservice.Update(Employee);
        updaterecords.then(function (d) {
            if (d.data.success == true) {
                loademployees();
                alert("Employee Added Success");
                $scope.resetsave();
            }
            else {
                alert("Employee not added");
            }
        },
        function () {
            alert("Some Error Occured");
        });
    }

    //update method end
});