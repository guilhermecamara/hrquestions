(function (angular) {
    "use strict";

    angular
        .module("todoApp")
        .directive("todoPaginatedList", [todoPaginatedList])
        .directive("pagination", [pagination]);

    /**
     * Directive definition function of 'todoPaginatedList'.
     * 
     * TODO: correctly parametrize scope (inherited? isolated? which properties?)
     * TODO: create appropriate functions (link? controller?) and scope bindings
     * TODO: make appropriate general directive configuration (support transclusion? replace content? EAC?)
     * 
     * @returns {} directive definition object
     */
    function todoPaginatedList() {
        var directive = {
            restrict: "E", // example setup as an element only
            templateUrl: "app/templates/todo.list.paginated.html",
            scope: {}, // example empty isolate scope
            controller: ["$scope", "$http", controller]
        };

        function controller($scope, $http) { // example controller creating the scope bindings
            $scope.todos = [];
            $scope.currentPage = 1;
            $scope.showLoading = false;
            $scope.orderBy = null;
            $scope.reverse = false;

            $scope.onCurrentPageChange = function () {
                $scope.getData();
            }

            $scope.onOrderByChange = function (column) {
                
                if (column == $scope.orderBy) {
                    $scope.reverse = !$scope.reverse;
                } else {
                    $scope.orderBy = column;
                    $scope.reverse = false;
                }

                if ($scope.currentPage == 1)
                    $scope.getData();

                $scope.currentPage = 1;
            }

            $scope.getData = function () {
                $scope.showLoading = true;

                var options = {
                    numPerPage: $scope.numPerPage,
                    currentPage: $scope.currentPage,
                    orderBy: $scope.orderBy,
                    reverse: $scope.reverse
                }

                $http({
                    url: "api/Todo/Todos",
                    method: "POST",
                    data: options
                }).then(function (response) {
                    var result = response.data
                    $scope.todos = result.data;
                    $scope.totalPages = result.totalPages;
                    $scope.totalItems = result.totalItems;

                    $scope.showLoading = false;
                });
            }

            $scope.$watch('numPerPage', function (newValue, oldValue) {
                if (oldValue && newValue != oldValue) {
                    $scope.currentPage = 1;
                    $scope.onCurrentPageChange();
                }                
            });
        }        

        return directive;
    }

    /**
     * Directive definition function of 'pagination' directive.
     * 
     * TODO: make it a reusable component (i.e. usable by any list of objects not just the Models.Todo model)
     * TODO: correctly parametrize scope (inherited? isolated? which properties?)
     * TODO: create appropriate functions (link? controller?) and scope bindings
     * TODO: make appropriate general directive configuration (support transclusion? replace content? EAC?)
     * 
     * @returns {} directive definition object
     */
    function pagination() {
        var directive = {
            restrict: "E",
            templateUrl: "app/templates/pagination.html",
            link: link,
            transclude: true
        };

        function link(scope, element, attrs) {
            scope.numPerPage = "20";

            scope.setPage = function (page) {
                scope.currentPage = page;
            }

            scope.$watch("currentPage", function () {                
                if (!scope.currentPage || scope.currentPage > scope.totalPages)
                    scope.currentPage = scope.totalPages;

                if (scope.onCurrentPageChange)
                    scope.onCurrentPageChange();
            });
        }

        return directive;
    }

})(angular);

