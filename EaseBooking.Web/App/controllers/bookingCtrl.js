'use strict';
app.controller("bookingCtrl", ['$scope', 'apiClientService',
    function ($scope, apiClientService) {

        $scope.errorMessage = '';
        $scope.pages = {
            step1: 0,
            step2: 1,
            step3: 2,
            step4: 3
        };


        $scope.start = function () {
            $scope.progressBarIsShown = true;
            switchPage($scope.pages.step1, 'Step 1: Select product');
            initializeDataFromApi('products');           
        }

        $scope.selectProduct = function (product) {
            switchPage($scope.pages.step2, 'Step 2: Select available slot');
            $scope.product = product;
            initializeDataFromApi('genders');
            initializeDataFromApi('healthproblems');
        }

        $scope.selectSlot = function (slot) {
            slot.IsAvailable = false;

            switchPage($scope.pages.step3, 'Step 3: Fill your data');

            $scope.order = {
                ProductId: $scope.product.Id,
                TimeSlot: slot,
                HealthProblems: []
            }
        }

        $scope.addProblem = function(problem) {
            for (var i = 0; i < $scope.order.HealthProblems.length; i++) {
                if ($scope.order.HealthProblems[i].Id === problem.Id) {
                    return;
                }
            }
            $scope.order.HealthProblems.push(problem);
        }

        $scope.removeProblem = function(problem) {
            var problems = [];
            for (var i = 0; i < $scope.order.HealthProblems.length; i++) {
                if ($scope.order.HealthProblems[i].Id === problem.Id) {
                    continue;
                }
                problems.push($scope.order.HealthProblems[i]);
            }


            $scope.order.HealthProblems = problems;
        }

        $scope.book = function() {
            $scope.errorMessage = '';
            if (!validate()) return;
            $scope.progressBarIsShown = true;
            apiClientService.post("orders", $scope.order)
                .success(function (data) {
                    switchPage($scope.pages.step4, 'Step 4: Comfirmation');
                    $scope.progressBarIsShown = false;
                })
                .error(function (data) {
                    $scope.errorMessage = data.Message;
                    $scope.progressBarIsShown = false;
                });
        }

        function validate() {
            if (!$scope.order) return false;

            var errors = [];

            if (!$scope.order.ClientName) {
                errors.push("Your name is required");
            }

            if (!$scope.order.Age) {
                errors.push("Age is required");
            }

            if (!$scope.order.Weight) {
                errors.push("Weight is required");
            }

            if (!$scope.order.GenderId) {
                errors.push("Gender is required");
            }

            if (!errors.length) {
                return true;
            }

            $scope.errorMessage = errors.join(', ');
            return false;
        }

        function initializeDataFromApi(url) {
            apiClientService.get(url)
                .success(function (data) {

                    switch (url) {
                        case 'products': $scope.products = data; break;
                        case 'genders': $scope.genders = data; break;
                        case 'healthproblems': $scope.healthProblems = data; break;                 
                    }
                    $scope.progressBarIsShown = false;
                })
                .error(function (data) {
                    $scope.errorMessage = data.Message;
                    $scope.progressBarIsShown = false;
                });

        }

        function switchPage(page, title) {
            $scope.pageTitle = title;
            $scope.page = page;
        }

    }]);