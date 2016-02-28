angular.module('portalApp').directive('notEqualTo', function () {

    return {
        require: "ngModel",
        scope: {
            otherModelValue: "=notEqualTo"
        },
        link: function (scope, element, attributes, ngModel) {

            ngModel.$validators.notEqualTo = function (modelValue) {
                return modelValue !== scope.otherModelValue;
            };

            scope.$watch("otherModelValue", function () {
                ngModel.$validate();
            });
        }
    };
});