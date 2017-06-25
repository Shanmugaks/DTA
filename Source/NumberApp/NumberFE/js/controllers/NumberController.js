/*
Angular JS Controller - NumberController

*/

// Create Application object
var NumberApp = angular.module('myApp', []);

/* 
NumberController implementation injected with following
1) $scope 
2) $window, 
3) sNumber - This is AngularJS service (defined in NumberService.js) which is the consumer of NumberService Web API
*/
NumberApp.controller("NumberController", function FunctionItem($scope, $window, sNumber) {

    // Placeholders angularJS variable intitalized
    $scope.NamePlaceholdervalue = "Please enter name";
    $scope.AmountPlaceholdervalue = "Please enter valid amount";

    // This method is called on click of process button
    $scope.ProcessInput = function () {
        $scope.flagValidate = false;

        // Validation check - NULL values of Name
        if ($scope.Name == null) {
            var Message = "Input Error(Name)";
            DisplayError(Message);
            return;
        }

        // Validation check - NULL values of Amount
        if ($scope.Amount == null) {
            var Message = "Input Error(Amount)";
            DisplayError(Message);
            return;
        }

        // Calling Angular JS Number Service which in turn calls NumberService Web API
        var response = sNumber.NumberToWords($scope.Amount);

        // Response Handler
        response.then(function (Result) {
            if (Result.data) {
                // On Success
                var Message = $scope.Name + "\n" + "\"" + Result.data  + "\"";
                DisplayValue(Message);
            }
            else {

                // On Error
                var Message = null;
                if (Result.data == null) {
                    Message = "ERROR: Web service server not reachable."
                }
                else {
                    Message = "ERROR: " + Result.data.Message + "\nHttp Status code:" + Result.status + "\nHttp Status message:" + Result.statusText;
                }
                DisplayError(Message);
            }

        }, function (error) {

            // On Error
            var Message = null;
            if (error.data == null) {
                Message = "ERROR: Web service server not reachable."
            }
            else {
                Message = "ERROR: " + error.data.Message + "\nHttp Status code:" + error.status + "\nHttp Status message:" + error.statusText;
            }
            DisplayError(Message);
        });

    }

    /*
    Uses same Text area to display both Error (in RED bg color) & values (in GREEN bg color).
    This method used to display message with green color
    */
    function DisplayValue(message) {
        $scope.OutputPlaceholder = message ;
        document.getElementById('Output').style.backgroundColor = 'lightgreen';
    }

    /*
    Uses same Text area to display both Error (in RED bg color) & values (in GREEN bg color).
    This method used to display message with RED color
    */
    function DisplayError(message) {
        $scope.OutputPlaceholder = message;
        document.getElementById('Output').style.backgroundColor = 'pink';
    }

});