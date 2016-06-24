var app = angular.module('booking', ['ngRoute'])
.constant("baseUrl", "http://localhost:4588/api/");

app.config(function ($routeProvider) {

    $routeProvider.when("/", {
        controller: "bookingCtrl",
        templateUrl: "/app/views/booking.html"
    });

    $routeProvider.otherwise({ redirectTo: "/" });

});
