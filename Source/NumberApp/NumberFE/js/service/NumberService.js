/*

Angular JS Service - NumberService

*/

// This is AngularJS service used to invoke NumberService Web API

NumberApp.service("sNumber", function ($http)
{
    this.NumberToWords = function (_InputNumber)
    {
        var address = "http://localhost:1234/api/Number/" + _InputNumber;
        return $http.get(address);
    };

});

