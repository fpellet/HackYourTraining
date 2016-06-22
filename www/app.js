(function() {

    var app = angular.module("hackYourTraining", []);

    app.controller("hackYourTrainingController", function($scope, $http) {
        var self = this;

        self.text = "";
        self.interestedTrainees = [];

        var load = function() {
            $http.get('/interestedTrainees').success(function(data) {
                self.interestedTrainees = data.interestedTrainees;
            });
        };

        load();
    });
})();